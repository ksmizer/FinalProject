using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyMovementBattle : MonoBehaviour {

	//Animator animator;

	GameObject hero;
	GameObject combat;
	TurnBasedCombat state;
	UnityEngine.AI.NavMeshAgent agent;
	EnemyHealth stats;
	
	Vector3 centerPos; Vector3 currentPos;
	Vector3 distToCursor;
	float distance; float radius;
	bool initialized;
	
	void Start () {
		combat = GameObject.Find("Combat");
		state = combat.GetComponent <TurnBasedCombat> ();
		hero = GameObject.Find("Adell");
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}
	
	void Update () {
		if (state.currentState == TurnBasedCombat.BattleStates.ENEMYTURN) {
			if (Input.GetKeyDown("e")) {
				Move();
			}
			if (!initialized)
				InitRadius ();
			currentPos = transform.position;
			distance = Vector3.Distance(currentPos, centerPos);
			if (distance > radius) {
				distToCursor = currentPos - centerPos;
				distToCursor *= radius/distance;
				currentPos = centerPos + distToCursor;
				transform.position = currentPos;
				agent.destination = currentPos;
			}
	}
	}

	
	void Move()	{
		if (agent.destination != hero.transform.position) {
			agent.destination = hero.transform.position;
		}
	}
	
	void InitRadius () {
		initialized = true;
		stats = GetComponent <EnemyHealth> ();
		radius = stats.GetMaxMovement ();
		centerPos = transform.position;
	}

}

