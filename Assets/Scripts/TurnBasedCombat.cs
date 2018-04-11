using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnBasedCombat : MonoBehaviour {

	bool shown = false;

	public enum BattleStates{
		START,
		PLAYERTURN,
		ENEMYTURN,
		LOST,
		WON
	}
	
	public BattleStates currentState;

	void Start () {
		currentState = BattleStates.START;
	}
	
	void Update () {
		if (Input.GetKeyDown("p")) {
			currentState = BattleStates.PLAYERTURN;
			shown = false;
		}
		// onClick(something) 
		if (Input.GetKeyDown("o")) {
			currentState = BattleStates.ENEMYTURN;
			shown = false;
		}
		if (Input.GetKeyDown("i")) {
			currentState = BattleStates.LOST;
			shown = false;
		}
		if (Input.GetKeyDown("u")) {
			currentState = BattleStates.WON;
			shown = false;
		}
		
		switch(currentState) {
			case (BattleStates.START):
				//setup BATTLE STATUS
				currentState = BattleStates.PLAYERTURN;
				break;
			
			case (BattleStates.PLAYERTURN):
				break;
				
			case (BattleStates.ENEMYTURN):
				break;
				
			case (BattleStates.LOST):
				break;
				
			case (BattleStates.WON):
				break;
		}
		if (!shown) {
			Debug.Log(currentState);
			shown = true;
		}
	}
}
