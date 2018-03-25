using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharMovementBattle : MonoBehaviour {

	[SerializeField]
	float moveSpeed = 1f;

	Animator anim;
	GameObject cursor;
	UnityEngine.AI.NavMeshAgent agent;
	AdellStats currentStats;
	
	void Start () {
		cursor = GameObject.Find("Cursor");
		agent = GetComponent <UnityEngine.AI.NavMeshAgent> ();
		anim = GetComponent <Animator> ();
		currentStats = GetComponent <AdellStats> ();
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("enter")) {
			Move();
		}
	}
	
	void Move()
	{
		//Debug.Log("Return pressed.");
		if (agent.destination != cursor.transform.position)
			agent.destination = cursor.transform.position;
	}

}

