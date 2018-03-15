using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DadTrigger : MonoBehaviour {

	public GameObject DadStats;
	public GameObject TravelScreen;
	public Button backButton;
	public bool backBool;
	public bool canvasBool = false;

	void OnTriggerEnter(Collider other)
	{
		DadStats.gameObject.SetActive(true);
		Debug.Log ("object enter");
	}

	void OnTriggerStay(Collider other)
	{
		if (Input.GetKeyUp (KeyCode.I)) {
			DadStats.gameObject.SetActive (false);
			canvasBool = true;
			TravelScreen.SetActive (true);
			Button btn = backButton.GetComponent<Button> ();
			btn.onClick.AddListener (TaskOnClick);
		} else if (!canvasBool) {
			DadStats.gameObject.SetActive (true);
		}
		Debug.Log ("object stay");
	}

	void TaskOnClick()
	{
		DadStats.gameObject.SetActive (true);
		TravelScreen.SetActive(false);
		backBool = true;
		canvasBool = false;
	}

	void OnTriggerExit(Collider other)
	{
		DadStats.SetActive(false);
		Debug.Log ("object exit");
	}

	/*void testing(Collider other) {
		while(true) {
			OnTriggerEnter(other);
			OnTriggerStay(other);
			OnTriggerExit(other);
		}
	}*/
}
