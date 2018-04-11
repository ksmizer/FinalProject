using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour {
	public Animator animator;
	public Text damageText;
	//public GameObject myObject;
	//private GameObject popupText;

	void Start()
	{
		AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo (0);
		Destroy (gameObject, clipInfo[0].clip.length);
		damageText = animator.GetComponent<Text> ();
	}

	public void SetText(string text) 
	{
		//Debug.Log (text);
		damageText.text = text;
		//damageText = text.GetComponent();
	}
}

