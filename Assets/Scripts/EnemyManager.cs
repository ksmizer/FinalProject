using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	GameObject[] enemies;
	GameObject[] heroes;
	GameObject combat;
	TurnBasedCombat state;
	EnemyMovementBattle checker;
	
	void Start () {
		combat = GameObject.Find("Combat");
		state = combat.GetComponent <TurnBasedCombat> ();
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
	}
	
	void Update () {
		if (state.currentState == TurnBasedCombat.BattleStates.ENEMYTURN) {
			int counter = 0;
			foreach (GameObject enemy in enemies) {
				checker = enemy.GetComponent <EnemyMovementBattle> ();
				if (checker.GetIfDone()) {
					counter++;
				}
			}
			if (counter >= enemies.Length) {
				StartCoroutine(SwapStates());
			}
		}
	}
	
	public void CheckEnemies () {
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		if (enemies.Length < 1) {
			state.currentState = TurnBasedCombat.BattleStates.WON;
		}
	}
	
	public void CheckAllies () {
		heroes = GameObject.FindGameObjectsWithTag("Ally");
		//Debug.Log (heroes.Length);
		if (heroes.Length < 1) {
			state.currentState = TurnBasedCombat.BattleStates.LOST;
		}
	}
	
	IEnumerator SwapStates () {
		yield return new WaitForSeconds(1);
		state.currentState = TurnBasedCombat.BattleStates.PLAYERTURN;
	}
}
