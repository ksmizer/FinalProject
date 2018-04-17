using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	GameObject[] enemies;
	GameObject[] heroes;
	GameObject combat;
	TurnBasedCombat state;
	
	void Start () {
		combat = GameObject.Find("Combat");
		state = combat.GetComponent <TurnBasedCombat> ();
	}
	
	void Update () {
		
	}
	
	public void CheckEnemies () {
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		if (enemies == null) {
			state.currentState = TurnBasedCombat.BattleStates.WON;
		}
	}
	
	public void CheckAllies () {
		heroes = GameObject.FindGameObjectsWithTag("Ally");
		Debug.Log (heroes.Length);
		if (heroes.Length < 1) {
			state.currentState = TurnBasedCombat.BattleStates.LOST;
		}
	}
}
