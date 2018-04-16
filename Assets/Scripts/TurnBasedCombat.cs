using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TurnBasedCombat : MonoBehaviour {

	//private float time = 1;
	public GameObject EnemyTurnCanvas;
	public GameObject PlayerTurnCanvas;
	public GameObject StageClearCanvas;
	public GameObject StageLostCanvas;

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

	IEnumerator Wait(float delay, GameObject canvas)
	{
		yield return new WaitForSeconds (delay);
		canvas.SetActive (false);
	}

	void Update () {
		if (Input.GetKeyDown("p")) {
			currentState = BattleStates.PLAYERTURN;
			PlayerTurnCanvas.SetActive (true);
			StartCoroutine(Wait(1.5f, PlayerTurnCanvas));
			shown = false;
		}
		if (Input.GetKeyDown("o")) {
			currentState = BattleStates.ENEMYTURN;

			EnemyTurnCanvas.SetActive (true);
			StartCoroutine(Wait(1.5f, EnemyTurnCanvas));
			//System.Threading.Thread.Sleep (1000);
			//EnemyTurnCanvas.SetActive (false);

			shown = false;
		}
		if (Input.GetKeyDown("i")) {
			currentState = BattleStates.LOST;

			currentState = BattleStates.WON;
			StageClearCanvas.SetActive (true);
			StartCoroutine(Wait(4.0f, StageLostCanvas));
			SceneManager.LoadScene (1);
			shown = false;
		}
		if (Input.GetKeyDown("u")) {
			currentState = BattleStates.WON;
			StageClearCanvas.SetActive (true);
			StartCoroutine(Wait(4.0f, StageClearCanvas));
			SceneManager.LoadScene (1);
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

