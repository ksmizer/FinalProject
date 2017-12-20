using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour {

	public GameObject canvas;			// Pause canvas

	void Update() {
		if (Input.GetKeyDown(KeyCode.P))
			Pause();
	}

	public void Pause()
	{
		if (canvas.gameObject.activeInHierarchy == false) {
			canvas.gameObject.SetActive (true);
			Time.timeScale = 0;
		} else {
			canvas.gameObject.SetActive (false);
			Time.timeScale = 1;
		}
	}
}
