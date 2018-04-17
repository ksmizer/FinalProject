using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EnemyAttack : MonoBehaviour {

	//[SerializeField]
	//float moveSpeed = 1f;

	public int expValue = 10;
	int baseAttack = 10;
	int equipAttack = 2;
	int effectiveness = 1;
	float maxMovement = 4f;
	float remainingMovement;
	float timeToShowDamage = 1f;
	public Text enemyDamageText;
	
	Animator anim;
	AdellStats playerHealth;
	EnemyHealth enemyHealth;
	EnemyMovementBattle movementEnabled;
	GameObject combat;
	TurnBasedCombat state;
	//UnityEngine.AI.NavMeshAgent agent;
	
	int attackDamage;
	bool isDead = false;
	bool damaged = false;
	bool attacking = false;
	float timer;
	
	void Start () {
		combat = GameObject.Find("Combat");
		state = combat.GetComponent <TurnBasedCombat> ();
		//temporary until player manager is made
		//player = GameObject.Find("Adell");
		//playerHealth = player.GetComponent <AdellStats> ();
		//self = GameObject.Find("Laharl");
		//agent = GetComponent <UnityEngine.AI.NavMeshAgent> ();
		enemyHealth = GetComponent <EnemyHealth> ();
		attackDamage =
					Mathf.RoundToInt((Mathf.Pow((float)baseAttack,(1+0.05f*enemyHealth.currentLevel))
					+ equipAttack) * effectiveness);
	}
	
	void Update () {
		if (state.currentState == TurnBasedCombat.BattleStates.ENEMYTURN) {
			timer += Time.deltaTime;
			if (Input.GetKeyDown("j")) {
				attacking = true;
			}
			if (Input.GetKeyDown("h")) {
				attacking = true;
				effectiveness = 2;
			}
			if (Input.GetKeyDown("r")) {
				remainingMovement = maxMovement;
			}
			if (attacking) {
				//Attack ();
			} else {
				
			}
			if (timer <= timeToShowDamage) {
				//enemyDamageText.text = "" + attackDamage;
			} else {
				//enemyDamageText.text = "";
			}
			damaged = false;
		}
	}
	
	public void Attack (GameObject player)
	{
		playerHealth = player.GetComponent <AdellStats> ();
		attackDamage = (baseAttack + equipAttack) * effectiveness;
		if (playerHealth.currentHealth > 0) {
			playerHealth.TakeDamage (attackDamage);
			if (effectiveness > 1) {
			Debug.Log("It was Super Effective!!");
			}
		}
		timer = 0f;
		attacking = false;
		effectiveness = 1;
	}
}

