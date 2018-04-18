using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorControllerBattle : MonoBehaviour {

	public Animator anim;
	//Animator animator;
	public GameObject moveButton;
	public GameObject attackButton;
	public GameObject moveCanvasHere;
	public Animation animationFight;
	bool moveButtonFlag = false;
	bool animationFlag = false;
	
	[SerializeField]
	float moveDistance = 1f;
	/* only use if using grid system
	float oldY = 0;
	float newX = 0;
	float newZ = 0;
	*/
	
	GameObject combat;
	TurnBasedCombat state;
	EnemyManager manager;

	public bool posSelect = false;
	bool initialized = false;
	public Vector3 targetPosition = new Vector3(0,0,0);
	public Canvas moveCanvas;
	
	//game object that is within trigger for cursor
	GameObject charFocus; GameObject charSelected;
	SphereCollider attack1; SphereCollider attack2;
	bool menuSelected = false;	bool atkSelected = false;
	bool moveFocus = false;		bool atkFocus = false;
	bool attackOption = false;	bool moveOption = false;
	bool moved = false;			bool attacked = false;
	
	AdellStats allyStats;
	EnemyHealth enemyStats; 
	CharMovementBattle movement;
	
	Vector3 centerPos; Vector3 currentPos;
	Vector3 distToCursor; Vector3 previousPos;
	float distance; float radius;

	[Range(0,50)]
	public int segments = 50;
	LineRenderer line;
	[SerializeField]
	Color c1 = new Color(0.0f, 1f, 0.0f, 1);

	void Start () {
		combat = GameObject.Find("Combat");
		state = combat.GetComponent <TurnBasedCombat> ();
		manager = combat.GetComponent<EnemyManager> ();
	}
	
	void Update () {
		if (state.currentState == TurnBasedCombat.BattleStates.PLAYERTURN) {
			if (Input.anyKey) {
				if (!moveCanvas.gameObject.activeSelf) {
					Move();
				}
				//placeholder for attack button selection
				Button attackbtn = attackButton.GetComponent<Button> ();
				attackbtn.onClick.AddListener (AttackButtonChosen);
				/*if (Input.GetKeyDown("n")) {
					attackOption = true;
					attack1 = charSelected.gameObject.GetComponentInChildren <SphereCollider> ();
					moveCanvas.gameObject.SetActive(false);
					movement = charSelected.GetComponent <CharMovementBattle> ();
					movement.SetSelected(false);
				}*/
				//placeholder for move button
				Button movebtn = moveButton.GetComponent<Button> ();
				movebtn.onClick.AddListener (MoveButtonChosen);
				/*if (Input.GetKeyDown("m")) {
					moveOption = true;
					moveCanvas.gameObject.SetActive(false);
				}*/
				if (Input.GetKeyDown("enter") || Input.GetKeyDown(KeyCode.Return)) {
					//move character and clear memory
					if (menuSelected && moveOption) {
						initialized = false;
						menuSelected = false;
						charSelected = null;
						moveOption = false;
						moved = true;
						ClearPoints ();
					}
					//select character that is focused on & bring up menu
					if (moveFocus && !attackOption) {
						movement = charFocus.GetComponent <CharMovementBattle> ();
						movement.SetSelected(true);
						menuSelected = true;
						charSelected = charFocus;
						moveFocus = false;
						moveCanvas.gameObject.SetActive(true);
					}
					//attack character selected
					if (atkFocus && attackOption) {
						allyStats = charSelected.GetComponent <AdellStats> ();
						allyStats.Attack(charFocus);
						Debug.Log ("im here");
						anim.Play ("Front_Punch");
						//int amount = 6;
						//FloatingTextController.CreateFloatingText (amount.ToString (), transform);
						attackOption = false;
						atkFocus = false;
						charFocus = null;
						charSelected = null;
						menuSelected = false;
						attacked = true;
						ClearPoints ();
						StartCoroutine(Check());
					}
				}
				//used for initializing line renderer to show radius around
				//character for movement & attack
				if (menuSelected) {	
					if (moveOption) {
						if (!initialized)
							InitRadius ();
						currentPos = transform.position;
						distance = Vector3.Distance(currentPos, centerPos);
						if (distance > radius) {
							distToCursor = currentPos - centerPos;
							distToCursor *= radius/distance;
							currentPos = centerPos + distToCursor;
							transform.position = currentPos;
						}
					}
					if (attackOption) {
						if (!initialized)
							InitRadius ();
						currentPos = transform.position;
						distance = Vector3.Distance(currentPos, centerPos);
						if (distance > radius) {
							distToCursor = currentPos - centerPos;
							distToCursor *= radius/distance;
							currentPos = centerPos + distToCursor;
							transform.position = currentPos;
						}
					}
				}
				//cancel selection
				if (Input.GetKeyDown("c")) {
					if (moveCanvas.gameObject.activeSelf) {
						moveCanvas.gameObject.SetActive(false);
					} else if (menuSelected && moved && !attackOption) {
						movement = charSelected.GetComponent <CharMovementBattle> ();
						movement.SetSelected(false);
						menuSelected = false;
						initialized = false;
						transform.position = charSelected.transform.position;
						moveCanvas.gameObject.SetActive(true);
						ClearPoints ();
					} else if (menuSelected && attackOption && !attacked) {
						atkSelected = false;
						attackOption = false;
						initialized = false;
						transform.position = charSelected.transform.position;
						moveCanvas.gameObject.SetActive(true);
						ClearPoints ();
					}
				}
			}
			//only use if we're going with grid system
			/*else if (transform.position.x % 1 > 0 || transform.position.z % 1 > 0){
				newX = transform.position.x;
				oldY = transform.position.y;
				newZ = transform.position.z;
				if (transform.position.x % 1 > 0) {
					newX = Mathf.Round(transform.position.x);
				}
				if (transform.position.z % 1 > 0) {
					newZ = Mathf.Round(transform.position.z);
				}
				transform.position = new Vector3 (newX, oldY, newZ);
			}*/
			//if cursor stopped within boundary of a trigger
			else if (charFocus != null && !menuSelected) {
				transform.position = charFocus.transform.position;
			}
			//if cursor stopped within boundary of a trigger that isn't self
			else if (charFocus != null && atkFocus) {
				if (transform.position != charSelected.transform.position) {
					transform.position = charFocus.transform.position;
				}
			}
		//reset character movement and attack options
		} else if (state.currentState == TurnBasedCombat.BattleStates.ENEMYTURN) {
			moved = false;
			attacked = false;
		}
	}

	void AttackButtonChosen() 
	{
		//animationFlag = true;
		//if (animationFlag)
		//	animationFight.Play(
		attackOption = true;
		attack1 = charSelected.gameObject.GetComponentInChildren <SphereCollider> ();
		moveCanvas.gameObject.SetActive(false);
		movement = charSelected.GetComponent <CharMovementBattle> ();
		movement.SetSelected(false);
	}

	void MoveButtonChosen() 
	{
		//moveButtonFlag = true;
		moveOption = true;
		moveCanvasHere.gameObject.SetActive(false);
	}

	void Move()
	{
		Vector3 rightMovement = Vector3.right * moveDistance * Time.deltaTime * 15f * Input.GetAxis("HorizontalKey");
		Vector3 upMovement = Vector3.forward * moveDistance * Time.deltaTime * 15f * Input.GetAxis("VerticalKey");
		
		rightMovement.y = 0;
		upMovement.y = 0;
		
		Vector3 heading = Vector3.Normalize(rightMovement + upMovement);
		
		if (upMovement != Vector3.zero || rightMovement != Vector3.zero) {
			transform.forward = heading;
		}
		transform.position += rightMovement;
		transform.position -= upMovement;
	}
	
	void OnTriggerEnter (Collider other) {
		if (!menuSelected) {
			charFocus = other.gameObject;
			moveFocus = true;
		}
		if (attackOption) {
			charFocus = other.gameObject;
			atkFocus = true;
		}
	}
	
	void OnTriggerExit () {
		if (!menuSelected) {
			charFocus = null;
			moveFocus = false;
		}
		if (attackOption) {
			charFocus = null;
			atkFocus = false;
		}
	}
	
	void InitRadius () {
		initialized = true;
		allyStats = charSelected.GetComponent <AdellStats> ();
		if (moveOption) {
			radius = allyStats.GetMaxMovement ();
		} else if (attackOption) {
			radius = attack1.radius * 3f;
			//Debug.Log(radius);
		}
		centerPos = transform.position;
		line = charSelected.GetComponent <LineRenderer> ();
		line.positionCount = segments+1;
		line.useWorldSpace = false;
		line.material = new Material(Shader.Find("Particles/Additive"));
		line.startColor = c1;
		line.endColor = c1;
		CreatePoints ();
	}
	
	void CreatePoints () {
		float x; float z;
		float angle = 20f;
		for (int i = 0; i < (segments+1); i++) {
			x = Mathf.Sin (Mathf.Deg2Rad * angle) * radius/2;
			z = Mathf.Cos (Mathf.Deg2Rad * angle) * radius/2;
			line.SetPosition (i, new Vector3(x,0,z));
			angle += (360f / segments);
		}
	}
	
	void ClearPoints () {
		for (int i = 0; i < (segments+1); i++) {
			line.SetPosition (i, new Vector3(0,0,0));
		}
	}
	
	IEnumerator Check () {
		yield return new WaitForSeconds(1);
		manager.CheckEnemies();
		manager.CheckAllies ();
	}
	
	public void SetPrevPos (Vector3 previous) {
		previousPos = previous;
	}
	
	public Vector3 GetPrevPos () { return previousPos; }
}

