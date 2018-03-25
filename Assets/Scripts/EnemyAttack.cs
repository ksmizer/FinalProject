using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EnemyAttack : MonoBehaviour {

	[SerializeField]
	float moveSpeed = 1f;

	public int startingHealth = 20;
	public int currentHealth;
	public int baseAttack = 5;
	public int equipAttack = 0;
	public int effectiveness = 1;
	public float maxMovement = 4f;
	public float remainingMovement;
	public float timeToShowDamage = 1f;
	public Text damageText;
	
	Animator anim;
	GameObject player;
	GameObject self;
	AdellStats playerHealth;
	UnityEngine.AI.NavMeshAgent agent;
	
	int attackDamage;
	int damageReceived;
	bool isDead;
	bool damaged;
	bool attacking;
	float timer;
	
	void Start () {
		player = GameObject.Find("Player");
		playerHealth = player.GetComponent <AdellStats> ();
		agent = GetComponent <UnityEngine.AI.NavMeshAgent> ();
		self = GameObject.Find("EnemySprite");
		currentHealth = startingHealth;
		remainingMovement = maxMovement;
		attackDamage = (baseAttack + equipAttack) * effectiveness;
	}
	
	void Update () {
		timer += Time.deltaTime;
		if (Input.GetKeyDown("h")) {
			attacking = true;
		}
		if (damaged) {
			
		} else if (attacking) {
			Attack ();
		} else if (isDead) {
			Destroy (gameObject, 1f);
		} else {
			
		}
		if (timer <= timeToShowDamage) {
			damageText.text = "" + damageReceived;
		} else {
			damageText.text = "";
		}
		damaged = false;
	}
	
	void Attack ()
	{
		if (playerHealth.currentHealth > 0) {
			playerHealth.TakeDamage (attackDamage);
		}
		timer = 0f;
		attacking = false;
	}
	
	public void TakeDamage (int amount)
	{
		damageReceived = amount;
		Debug.Log(amount);
		damaged = true;
		currentHealth -= amount;
		if (currentHealth <= 0 && !isDead) {
			Death ();
		}
	}
	
	void Death ()
	{
		Debug.Log("Enemy Died.");
		isDead = true;
	}
}

