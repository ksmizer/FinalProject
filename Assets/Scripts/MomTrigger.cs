using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomTrigger : MonoBehaviour {

	public GameObject MomStats;

	void OnTriggerEnter(Collider other)
	{
		MomStats.SetActive(true);
		Debug.Log ("object enter");
	}
	void OnTriggerStay(Collider other)
	{
		MomStats.SetActive(true);
		Debug.Log ("object stay");
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
