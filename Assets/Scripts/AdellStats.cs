using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AdellStats : MonoBehaviour {

	public Animator anim;
	public Slider healthbar;

	int maxHealth = 40;
	int startingHealth = 40;
	public int currentHealth;
	
	[SerializeField]
	public int currentLevel;
	
	int baseAttack = 7;
	int baseDefense = 3;
	int equipAttack = 2;
	int equipDefense = 3;
	int effectiveness = 1;
	int damageDealt;
	float maxMovement = 5f;
	float timeToShowDamage = 1f;
	//public Slider healthSlider;
	//public Image damageImage;
	//public AudioClip deathClip;
	public float flashSpeed = 5f;
	public Color flashColor = new Color(1f, 0f, 0f, 0.1f);
	//public Text playerDamageText;
	
	//AudioSource playerAudio;
	//CharMovementBattle playerMovement;
	//GameObject enemy;
	GameObject combat;
	GameObject closest;
	GameObject[] enemies;
	CharMovementBattle movement;
	EnemyHealth enemyHealth;
	TurnBasedCombat state;
	EnemyManager manager;
	
	int attack;
	int defense;
	bool isDead;
	bool damaged;
	bool attacked;
	bool done = false;
	bool inRange = true;
	float timer;
	float attackRadius1 = 1f;
	float attackRadius2 = 2f;
	
	void Start () {
		//anim = GetComponent <Animator> ();
		//playerAudio = GetComponent <AudioSource> ();
		//enemy = GameObject.Find("Laharl");
		//enemyHealth = enemy.GetComponent <EnemyHealth> ();
		currentHealth = startingHealth;
		combat = GameObject.Find("Combat");
		state = combat.GetComponent <TurnBasedCombat> ();
		manager = combat.GetComponent <EnemyManager> ();
		movement = GetComponent <CharMovementBattle> ();
		attack =
				Mathf.RoundToInt((Mathf.Pow((float)baseAttack,(1+0.05f*currentLevel))
				+ equipAttack) * effectiveness);
		defense =
				Mathf.RoundToInt((Mathf.Pow((float)baseDefense,(1+0.05f*currentLevel))
				+ equipDefense) * effectiveness);

		healthbar.value = CalculateHealth ();
		anim = GetComponent<Animator> ();
	}
	
	void Update () {
		timer += Time.deltaTime;

		if (Input.GetKeyDown("r")) {
			anim.Play ("Front_Punch");
		}
		if (damaged) {
			
		} else if (isDead) {
			//damageImage.color = flashColor;
			Destroy (gameObject, 1f);
		} else {
			//damageImage.color = Color.Lerp (damageImage.color, Color.cldar, flashSpeed * Time.deltaTime);
		}
		if (timer <= timeToShowDamage) {
			//playerDamageText.text = "" + attack;
		} else {
			//playerDamageText.text = "";
		}
		damaged = false;
		if (state.currentState == TurnBasedCombat.BattleStates.ENEMYTURN) {
			attacked = false;
			done = false;
		} else {
			CheckDone();
		}
	}

	void RecalcAttack ()
	{
		attack =
				Mathf.RoundToInt((Mathf.Pow((float)baseAttack,(1+0.05f*enemyHealth.currentLevel))
				+ equipAttack) * effectiveness);
	}
	
	public void Attack (GameObject enemy)
	{
		//RecalcAttack ();
		enemyHealth = enemy.GetComponent <EnemyHealth> ();
		if (enemyHealth.currentHealth > 0 && !attacked) {
			enemyHealth.TakeDamage (attack);
			//anim.Play ("Front_Punch");
			attacked = true;
			StartCoroutine (Check());
			if (effectiveness > 1) {
			Debug.Log("It was Super Effective!!");
			}
		}
		timer = 0f;
		effectiveness = 1;
	}
	
	public void TakeDamage (int amount)
	{
		amount -= defense;
		Debug.Log("Player lost " + amount + " HP");
		damaged = true;
		currentHealth -= amount;

		//int amount = 3;
		FloatingTextController.CreateFloatingText (amount.ToString (), transform);
		healthbar.value = CalculateHealth ();
		//anim.SetTrigger ("Hurt");
		//healthSlider.value = currentHealth;
		//playerAudio.Play ();
		if (currentHealth <= 0 && !isDead) {
			Death ();
		}
	}

	float CalculateHealth()
	{
		return currentHealth;
	}

	void Death ()
	{
		Debug.Log("Character Died.");
		isDead = true;
		//playerAudio.clip = deathClip;
		//playerAudio.Play ();
		//playerMovement.enabled = false;
		//state.currentState = TurnBasedCombat.BattleStates.LOST;
	}
	
	IEnumerator Check () {
		yield return new WaitForSeconds(1.5f);
		manager.CheckEnemies();
	}
	
	public void CheckDone () {
		if (movement.GetMoved() && !inRange || attacked) {
			done = true;
		}
		/*
		if (EndPlayerTurn) {
			done = true;
		}
		*/
	}
	
	public void ClosestEnemy () {
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		closest = null;
		float distance = Mathf.Infinity;
		foreach (GameObject enemy in enemies) {
			Vector3 diff = enemy.transform.position - transform.position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) {
				closest = enemy;
				distance = curDistance;
			}
		}
		CheckInRange();
	}
	
	void CheckInRange () {
		if (closest != null) {
			Vector3 diff = closest.transform.position - transform.position;
			float atkDistance = diff.sqrMagnitude;
			if (atkDistance <= attackRadius1 * 3f) {
				inRange = true;
			} else {
				inRange = false;
			}
		}
	}
	
	public float GetMaxMovement () { return maxMovement; }
	
	public bool GetAttacked () { return attacked; }
	
	public bool GetIfDone () { return done; }
}

