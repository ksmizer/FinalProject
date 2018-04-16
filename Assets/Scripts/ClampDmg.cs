using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClampDmg : MonoBehaviour {

	public Text damageText;

	void Update () {
		Vector3 dmgPos = Camera.main.WorldToScreenPoint (this.transform.position);
		damageText.transform.position = dmgPos;
	}
}
