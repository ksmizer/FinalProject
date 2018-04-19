using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	GameObject[] enemies;
	GameObject[] heroes;
	GameObject combat;
	TurnBasedCombat state;
	EnemyMovementBattle enemyChecker;
	AdellStats heroChecker;
	
	void Start () {
		combat = GameObject.Find("Combat");
		state = combat.GetComponent <TurnBasedCombat> ();
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		heroes = GameObject.FindGameObjectsWithTag("Ally");
	}
	
	void Update () {
		if (state.currentState == TurnBasedCombat.BattleStates.ENEMYTURN) {
			int counter = 0;
			foreach (GameObject enemy in enemies) {
				enemyChecker = enemy.GetComponent <EnemyMovementBattle> ();
				if (enemyChecker.GetIfDone()) {
					counter++;
				}
			}
			if (counter >= enemies.Length && enemies.Length != 0) {
				StartCoroutine(SwapStates(0));
			}
		} else if (state.currentState == TurnBasedCombat.BattleStates.PLAYERTURN) {
			int counter = 0;
			foreach (GameObject hero in heroes) {
				heroChecker = hero.GetComponent <AdellStats> ();
				if (heroChecker.GetIfDone()) {
					counter++;
				}
			}
			if (counter >= heroes.Length && heroes.Length != 0) {
				StartCoroutine(SwapStates(1));
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
	
	IEnumerator SwapStates (int swap) {
		yield return new WaitForSeconds(1);
		if (swap == 0 && state.currentState != TurnBasedCombat.BattleStates.LOST) {
			state.currentState = TurnBasedCombat.BattleStates.PLAYERTURN;
		} else if (swap == 1 && state.currentState != TurnBasedCombat.BattleStates.WON) {
			state.currentState = TurnBasedCombat.BattleStates.ENEMYTURN;
		}
	}
}
