using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AdellStats : MonoBehaviour {
	
	public int startingHealth = 40;
	public int currentHealth;
	public int baseAttack = 5;
	public int equipAttack = 0;
	public int effectiveness = 1;
	public int damageDealt;
	public float maxMovement = 5f;
	public float remainingMovement;
	public float timeToShowDamage = 1f;
	//public Slider healthSlider;
	//public Image damageImage;
	//public AudioClip deathClip;
	public float flashSpeed = 5f;
	public Color flashColor = new Color(1f, 0f, 0f, 0.1f);
	public Text damageText;
	
	Animator anim;
	//AudioSource playerAudio;
	//CharMovementBattle playerMovement;
	GameObject enemy;
	GameObject self;
	EnemyAttack enemyHealth;
	
	int attackDamage;
	int damageReceived;
	bool isDead;
	bool damaged;
	bool attacking;
	float timer;
	
	void Start () {
		anim = GetComponent <Animator> ();
		//playerAudio = GetComponent <AudioSource> ();
		self = GameObject.Find("Player");
		enemy = GameObject.Find("Enemy");
		enemyHealth = enemy.GetComponent <EnemyAttack> ();
		currentHealth = startingHealth;
		remainingMovement = maxMovement;
		attackDamage = (baseAttack + equipAttack) * effectiveness;
	}
	
	void Update () {
		timer += Time.deltaTime;
		if (Input.GetKeyDown("f")) {
			attacking = true;
		}
		if (damaged) {
			
		} else if (attacking) {
			Attack ();
		} else if (isDead) {
			//damageImage.color = flashColor;
			Destroy (gameObject, 1f);
		} else {
			//damageImage.color = Color.Lerp (damageImage.color, Color.cldar, flashSpeed * Time.deltaTime);
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
		if (enemyHealth.currentHealth > 0) {
			enemyHealth.TakeDamage (attackDamage);
		}
		timer = 0f;
		attacking = false;
	}
	
	public void TakeDamage (int amount)
	{
		Debug.Log(amount);
		damageReceived = amount;
		damaged = true;
		currentHealth -= amount;
		//anim.SetTrigger ("Hurt");
		//healthSlider.value = currentHealth;
		//playerAudio.Play ();
		if (currentHealth <= 0 && !isDead) {
			Death ();
		}
	}
	
	void Death ()
	{
		Debug.Log("Character Died.");
		isDead = true;
		//playerAudio.clip = deathClip;
		//playerAudio.Play ();
		//playerMovement.enabled = false;
	}
}

