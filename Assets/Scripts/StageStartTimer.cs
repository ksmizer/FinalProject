using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageStartTimer : MonoBehaviour {

	public float time = 2;
	public float time2 = 1;
	public GameObject StageStartCanvas;
	public GameObject PlayerStartCanvas;

	IEnumerator Start()
	{
		yield return new WaitForSeconds (time);
		StageStartCanvas.SetActive (false);
		PlayerStartCanvas.SetActive (true);
		yield return new WaitForSeconds (time2);
		PlayerStartCanvas.SetActive (false);
	}
}
