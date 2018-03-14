using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DadTrigger : MonoBehaviour {

	public GameObject DadStats;

	void OnTriggerEnter(Collider other)
	{
		DadStats.SetActive(true);
		Debug.Log ("object enter");
	}
	void OnTriggerStay(Collider other)
	{
		DadStats.SetActive(true);
		Debug.Log ("object stay");
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
