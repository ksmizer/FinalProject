using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TurnBasedCombat : MonoBehaviour {

	public GameObject EnemyTurnCanvas;
	public GameObject PlayerTurnCanvas;
	public GameObject StageClearCanvas;
	public GameObject StageLostCanvas;

	bool shown = false;
	bool start = true;
	bool reached = false;

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
	
	IEnumerator StartToPlayer(float delay)
	{
		yield return new WaitForSeconds (delay);
		currentState = BattleStates.PLAYERTURN;
		shown = false;
	}
	
	IEnumerator Exit(float delay, GameObject canvas)
	{
		yield return new WaitForSeconds (delay);
		canvas.SetActive (false);
		SceneManager.LoadScene(1);
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
			EnemyTurnCanvas.SetActive(true);
			StartCoroutine(Wait(1.5f, EnemyTurnCanvas));
			shown = false;
		}
		if (Input.GetKeyDown("i")) {
			currentState = BattleStates.LOST;
			StageLostCanvas.SetActive(true);
			StartCoroutine(Wait(4.0f, StageLostCanvas));
			SceneManager.LoadScene(1);
			shown = false;
		}
		if (Input.GetKeyDown("u")) {
			currentState = BattleStates.WON;
			StageClearCanvas.SetActive(true);
			StartCoroutine(Wait(4.0f, StageClearCanvas));
			SceneManager.LoadScene(1);
			shown = false;
		}
		if (Input.GetKeyDown("h")) {
			shown = false;
		}
		switch(currentState) {
			case (BattleStates.START):
				//setup BATTLE STATUS
				if (reached == false) {
					StartCoroutine (StartToPlayer (2f));
					reached = true;
				}
				break;
			
			case (BattleStates.PLAYERTURN):
				if (!shown) {
					PlayerTurnCanvas.SetActive (true);
					StartCoroutine (Wait (1.5f, PlayerTurnCanvas));
					shown = true;
				}
				break;
				
			case (BattleStates.ENEMYTURN):
				EnemyTurnCanvas.SetActive(true);
				StartCoroutine(Wait(1.5f, EnemyTurnCanvas));
				break;
				
			case (BattleStates.LOST):
				StageLostCanvas.SetActive(true);
				StartCoroutine(Exit(4.0f, StageLostCanvas));
				//SceneManager.LoadScene(1);
				break;
				
			case (BattleStates.WON):
				StageClearCanvas.SetActive(true);
				StartCoroutine(Exit(4.0f, StageClearCanvas));
				//SceneManager.LoadScene(1);
				break;
		}
		if (!shown) {
			Debug.Log(currentState);
			shown = true;
		}
	}
}
