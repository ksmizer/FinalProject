using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DadTrigger : MonoBehaviour {

	public GameObject DadStats;
	public GameObject TravelScreen;
	public GameObject TravelIcon;
	public GameObject I_Keyboard;
	public Button backButton;
	public bool backBool;
	public bool canvasBool = false;

	void OnTriggerEnter(Collider other)
	{
		TravelIcon.gameObject.SetActive (false);

		DadStats.gameObject.SetActive(true);
		Debug.Log ("object enter");
	}

	void OnTriggerStay(Collider other)
	{
		TravelIcon.gameObject.SetActive (false);
		I_Keyboard.gameObject.SetActive (true);

		if (Input.GetKeyUp (KeyCode.I)) {
			DadStats.gameObject.SetActive (false);
			I_Keyboard.gameObject.SetActive (false);
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
		TravelIcon.gameObject.SetActive (true);
		I_Keyboard.gameObject.SetActive (false);

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
