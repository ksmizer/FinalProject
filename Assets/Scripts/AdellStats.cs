using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AdellStats : MonoBehaviour {
	//public GameObject CBTprefab;

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
	public Text playerDamageText;
	
	//Animator anim                                                                                                                                                                                                                                                                                                                                                                                                                            ;
	//AudioSource playerAudio;
	//CharMovementBattle playerMovement;
	GameObject enemy;
	GameObject combat;
	CharMovementBattle movementEnabled;
	EnemyHealth enemyHealth;
	TurnBasedCombat state;
	
	int attack;
	int defense;
	bool isDead;
	bool damaged;
	bool attacking;
	float timer;
	
	void Start () {
		
		//anim = GetComponent <Animator> ();
		//playerAudio = GetComponent <AudioSource> ();
		enemy = GameObject.Find("Laharl");
		enemyHealth = enemy.GetComponent <EnemyHealth> ();
		currentHealth = startingHealth;
		combat = GameObject.Find("Combat");
		state = combat.GetComponent <TurnBasedCombat> ();
		attack =
				Mathf.RoundToInt((Mathf.Pow((float)baseAttack,(1+0.05f*currentLevel))
				+ equipAttack) * effectiveness);
		defense =
				Mathf.RoundToInt((Mathf.Pow((float)baseDefense,(1+0.05f*currentLevel))
				+ equipDefense) * effectiveness);
	}
	
	void Update () {
		timer += Time.deltaTime;
		if (Input.GetKeyDown("f")) {
			attacking = true;
			//int amount = Random.Range (0, 100);
			int amount = 5;
			//string amountText = amount.ToString ();
			//Debug.Log (amountText);
			FloatingTextController.CreateFloatingText(amount.ToString(), transform);
		}
		if (Input.GetKeyDown("g")) {
			attacking = true;
			effectiveness = 2;
			RecalcAttack ();
		}
		if (Input.GetKeyDown("r")) {
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
			//playerDamageText.text = "" + attack;
		} else {
			//playerDamageText.text = "";
		}
		damaged = false;
	}

	void RecalcAttack ()
	{
		attack =
				Mathf.RoundToInt((Mathf.Pow((float)baseAttack,(1+0.05f*enemyHealth.currentLevel))
				+ equipAttack) * effectiveness);
	}
	
	void Attack ()
	{
		if (enemyHealth.currentHealth > 0) {
			enemyHealth.TakeDamage (attack);
			if (effectiveness > 1) {
			Debug.Log("It was Super Effective!!");
			}
		}
		timer = 0f;
		attacking = false;
		effectiveness = 1;
	}
	
	public void TakeDamage (int amount)
	{
		//InitCBT (amount.ToString());i
		//amount =  10;
		//FloatingTextController.CreateFloatingText(amount.ToString(), transform);

		amount -= defense;
		Debug.Log("Player lost " + amount + " HP");


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
		state.currentState = TurnBasedCombat.BattleStates.LOST;
	}
	
	public float GetMaxMovement () { return maxMovement; }

	/*void InitCBT(string text)
	{
		GameObject temp = Instantiate (CBTprefab) as GameObject;
		RectTransform tempRect = temp.GetComponent<RectTransform> ();
		temp.transform.SetParent (transform.FindChild ("PopUpCanvas"));
		tempRect.transform.localPosition = CBTprefab.transform.localPosition;
		tempRect.transform.localScale = CBTprefab.transform.localScale;
		tempRect.transform.localRotation = CBTprefab.transform.localRotation;

		temp.GetComponent<Text>().text = text;
		Destroy (temp.gameObject, 2);
	}*/
}

