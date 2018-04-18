using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharMovementBattle : MonoBehaviour {

	//[SerializeField]
	//float moveSpeed = 1f;

	//Animator anim;
	GameObject cursor;
	GameObject combat;
	TurnBasedCombat state;
	CursorControllerBattle cursorController;
	UnityEngine.AI.NavMeshAgent agent;
	AdellStats stats;
	
	bool selected;
	bool moved = false;
	
	void Start () {
		cursor = GameObject.Find("Cursor");
		cursorController = cursor.GetComponent <CursorControllerBattle> ();
		agent = GetComponent <UnityEngine.AI.NavMeshAgent> ();
		selected = false;
		//anim = GetComponent <Animator> ();
		stats = GetComponent <AdellStats> ();
		combat = GameObject.Find("Combat");
		state = combat.GetComponent <TurnBasedCombat> ();
	}
	
	void Update () {
		if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("enter")) && selected && !moved) {
			Move();
			selected = false;
			moved = true;
		}
		if (Input.GetKeyDown("c") && selected && !stats.GetAttacked()) {
			MoveBack();
			moved = false;
		}
		if (state.currentState == TurnBasedCombat.BattleStates.ENEMYTURN) {
			moved = false;
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
	
	public void SetSelected(bool test) { selected = test;}

}

