using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MomTrigger : MonoBehaviour {

	public GameObject MomStats;
	public GameObject HospitalScreen;
	public Button backButton;
	public bool backBool;
	public bool canvasBool = false;

	void OnTriggerEnter(Collider other)
	{
		MomStats.gameObject.SetActive(true);
		Debug.Log ("object enter");
	}

	void OnTriggerStay(Collider other)
	{
		//if (!canvasBool)
			//MomStats.gameObject.SetActive (true);
	
		if (Input.GetKeyUp (KeyCode.I)) {
			MomStats.gameObject.SetActive (false);
			canvasBool = true;
			HospitalScreen.SetActive (true);
			Button btn = backButton.GetComponent<Button> ();
			btn.onClick.AddListener (TaskOnClick);
		} else if (!canvasBool) {
			MomStats.gameObject.SetActive (true);
		}


		//MomStats.gameObject.SetActive (true);
		//Time.timeScale = 1;
		//canvasBool = false;
		//backBool = false;
		Debug.Log ("object stay");
	}

	void TaskOnClick()
	{
		MomStats.gameObject.SetActive (true);
		HospitalScreen.SetActive(false);
		backBool = true;
		canvasBool = false;
	}

	void OnTriggerExit(Collider other)
	{
		MomStats.SetActive(false);
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
