using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingOffThreshold : MonoBehaviour {

	public GameObject character;

	public float threshold = -10f;

	void FixedUpdate()
	{
		if (character.transform.position.y < threshold) {
			character.transform.position = new Vector3(12, 28, 10);
			//character.transform.position.y = -7;
			//character.transform.position.z = -11;
		}
	}

}
