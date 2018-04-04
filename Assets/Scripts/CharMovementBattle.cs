using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharMovementBattle : MonoBehaviour {

	//[SerializeField]
	//float moveSpeed = 1f;

	//Animator anim;
	GameObject cursor;
	CursorControllerBattle cursorController;
	UnityEngine.AI.NavMeshAgent agent;
	//AdellStats currentStats;
	
	void Start () {
		cursor = GameObject.Find("Cursor");
		cursorController = cursor.GetComponent <CursorControllerBattle> ();
		agent = GetComponent <UnityEngine.AI.NavMeshAgent> ();
		//anim = GetComponent <Animator> ();
		//currentStats = GetComponent <AdellStats> ();
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("enter")) {
			Move();
		}
		if (Input.GetKeyDown("c")) {
			MoveBack();
		}
	}
	
	void Move()	{
		//Debug.Log("Return pressed.");
		if (agent.destination != cursor.transform.position) {
			cursorController.SetPrevPos (transform.position);
			agent.destination = cursor.transform.position;
		}
	}
	
	void MoveBack()	{
		//Debug.Log("Return pressed.");
		transform.position = cursorController.GetPrevPos ();
		agent.destination = transform.position;
	}

}

