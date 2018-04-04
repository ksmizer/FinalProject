using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	public int startingHealth = 100;
	public int currentHealth;
	public int startingLevel = 1;
	
	[SerializeField]
	public int currentLevel;
	int baseDefense = 2;
	int equipDefense = 4;
	float maxMovement = 5f;
	
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
	}
	
	// Update is called once per frame
	void Update () {
		if (isDead) {
			Destroy (gameObject, 1f);
		}
	}
	
	public void TakeDamage (int amount) {
		if(isDead)
			return;
		
		//enemyAudio.Play ();

		amount -= (baseDefense + equipDefense);
		Debug.Log("Player lost " + amount + " HP");
		currentHealth -= amount;
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
		state.currentState = TurnBasedCombat.BattleStates.WON;
	}
	
	public float GetMaxMovement () { return maxMovement; }
}
