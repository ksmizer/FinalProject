using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageStartTimerOld : MonoBehaviour {

	public float time = 2;
	public GameObject StageStartCanvas;
	public GameObject MoveCanvas;

	IEnumerator Start()
	{
		yield return new WaitForSeconds (time);
		StageStartCanvas.SetActive (false);
		MoveCanvas.SetActive (true);
	}
}
