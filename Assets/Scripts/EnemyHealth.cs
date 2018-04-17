using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

	public Slider healthbar;

	public int maxHealth = 7;
	public int startingHealth = 7;
	public int currentHealth;
	public int startingLevel = 1;
	
	[SerializeField]
	public int currentLevel;
	int baseDefense = 2;
	int equipDefense = 4;
	float maxMovement = 5f;
	float attackRadius1 = 1f;
	float attackRadius2 = 2f;
	
	//Animator anim;
	//AudioSource enemyAudio;
	//ParticleSystem hitParticles;
	//CapsuleCollider capsuleCollider;
	GameObject combat;
	TurnBasedCombat state;
	bool isDead;

	// Use this for initialization
	void Start () {
		combat = GameObject.Find("Combat");
		state = combat.GetComponent <TurnBasedCombat> ();
		currentHealth = startingHealth;
		currentLevel = startingLevel;

		healthbar.value = CalculateHealth ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isDead) {
			Destroy (gameObject, 0.5f);
		}
	}
	
	public void TakeDamage (int amount) {
		if(isDead)
			return;
		
		//enemyAudio.Play ();

		amount -= (baseDefense + equipDefense);
		Debug.Log("Enemy lost " + amount + " HP");
		currentHealth -= amount;

		healthbar.value = CalculateHealth ();
		//anim.SetTrigger ("Hurt");
		//healthSlider.value = currentHealth;
		//playerAudio.Play ();
		if (currentHealth <= 0 && !isDead) {
			Death ();
		}
	}
	
	void Death () {
		isDead = true;
		
		//anim.SetTrigger("Dead");
		
		//enemyAudio.clip = deathClip;
		//enemyAudio.Play ();
	}

	float CalculateHealth()
	{
		return currentHealth;
	}

	public float GetMaxMovement () { return maxMovement; }
	
	public float GetAttackRadius (int attack) {
		switch (attack) {
			case 1:
				return attackRadius1;
				break;
			case 2:
				return attackRadius2;
				break;
		}
		return 1;
	}
}
