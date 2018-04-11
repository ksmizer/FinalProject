using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorControllerBattle : MonoBehaviour {

	//Animator animator;
	
	[SerializeField]
	float moveDistance = 1f;
	/* only use if using grid system
	float oldY = 0;
	float newX = 0;
	float newZ = 0;
	*/
	
	GameObject combat;
	TurnBasedCombat state;
	
	public bool posSelect = false;
	bool initialized = false;
	public Vector3 targetPosition = new Vector3(0,0,0);
	
	//game object that is within trigger for cursor
	GameObject charFocus;
	GameObject charSelected;
	bool selected = false;
	bool focused = false;
	bool attack = false;
	
	AdellStats allyStats;
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
	}
	
	void Update () {
		if (state.currentState == TurnBasedCombat.BattleStates.PLAYERTURN) {
			if (Input.anyKey) {
				Move();
				if (Input.GetKeyDown("enter") || Input.GetKeyDown(KeyCode.Return)) {
					//display options for focused game object
					if (selected /* && moveOption*/) {
						initialized = false;
						selected = false;
						charSelected = null;
						ClearPoints ();
					}
					if (focused) {
						movement = charFocus.GetComponent <CharMovementBattle> ();
						movement.SetSelected(true);
						selected = true;
						charSelected = charFocus;
						focused = false;
					}
				}
				if (selected) {	
					//if (moveOption) {
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
					//}
				}
				//cancel selection
				if (Input.GetKeyDown("c")) {
					if (selected/* && moveOption */) {
						//moveOption = false;
					}
					if (selected) {
						movement = charSelected.GetComponent <CharMovementBattle> ();
						movement.SetSelected(false);
						selected = false;
						initialized = false;
						charSelected = null;
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
			else if (charFocus != null && !selected) {
				transform.position = charFocus.transform.position;
			}
		}
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
		if (!selected) {
			charFocus = other.gameObject;
			focused = true;
		} else {
			
		}
	}
	
	void OnTriggerExit () {
		if (!selected) {
			charFocus = null;
			focused = false;
		} else {
			
		}
	}
	
	void InitRadius () {
		initialized = true;
		allyStats = charSelected.GetComponent <AdellStats> ();
		radius = allyStats.GetMaxMovement ();
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
	
	public void SetPrevPos (Vector3 previous) {
		previousPos = previous;
	}
	
	public Vector3 GetPrevPos () { return previousPos; }
}

