using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageStartTimerPlayerTurn : MonoBehaviour {

	public float time = 1;
	//public float time2 = 1;
	//public GameObject StageStartCanvas;
	public GameObject PlayerStartCanvas;

	IEnumerator Start()
	{
		yield return new WaitForSeconds (time);
		PlayerStartCanvas.SetActive (false);
		//PlayerStartCanvas.SetActive (true);
		//yield return new WaitForSeconds (time2);
		//PlayerStartCanvas.SetActive (false);
	}
}
