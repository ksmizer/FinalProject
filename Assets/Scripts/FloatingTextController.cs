using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextController : MonoBehaviour {
	private static FloatingText popupText;
	private static GameObject canvas;
	//public static GameObject myObject;

	public void Start() 
	{
		Initialize();
	}

	public static void Initialize()
	{
		canvas = GameObject.Find ("PopUpCanvas");
		//if (!popupText)
		popupText = Resources.Load<FloatingText>("Prefabs/PopUpTextParent");
	}

	public static void CreateFloatingText(string text, Transform location)
	{
		//Debug.Log (text);
		FloatingText instance = Instantiate (popupText);
		instance.transform.SetParent (canvas.transform, false);
		instance.SetText (text);
	}

}
