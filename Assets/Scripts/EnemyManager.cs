using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	public enum Level{
		NONE,
		FIRST,
		SECOND,
		THIRD,
		FOURTH
	}

	public GameObject enemy;
	private Level currentLevel;
	
	void Start () {
		currentLevel = Level.NONE;
	}
	
	void Update () {
		switch(currentLevel) {
			case (Level.NONE):
				break;
			case (Level.FIRST):
				break;
			case (Level.SECOND):
				break;
			case (Level.THIRD):
				break;
			case (Level.FOURTH):
				break;
		}
	}
}
