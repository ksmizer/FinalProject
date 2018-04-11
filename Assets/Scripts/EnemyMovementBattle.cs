using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyMovementBattle : MonoBehaviour {

	//Animator animator;

	GameObject[] heroes;
	GameObject combat;
	TurnBasedCombat state;
	UnityEngine.AI.NavMeshAgent agent;
	EnemyHealth stats;
	
	Vector3 centerPos; Vector3 currentPos;
	Vector3 distToHero;
	float distance; float radius;
	bool initialized;
	
	void Start () {
		combat = GameObject.Find("Combat");
		state = combat.GetComponent <TurnBasedCombat> ();
		//hero = GameObject.Find("Adell");
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		stats = GetComponent <EnemyHealth> ();
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
				distToHero = currentPos - centerPos;
				distToHero *= radius/distance;
				currentPos = centerPos + distToHero;
				transform.position = currentPos;
				agent.destination = currentPos;
			}
		} else {
			initialized = false;
		}
	}

	
	void Move()	{
		GameObject closest = null;
		float distance = Mathf.Infinity;
		if (heroes == null) {
			heroes = GameObject.FindGameObjectsWithTag("Ally");
		}
		foreach (GameObject hero in heroes) {
			Vector3 diff = hero.transform.position - transform.position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) {
				closest = hero;
				distance = curDistance;
			}
		}
		if (agent.destination != closest.transform.position) {
			agent.destination = closest.transform.position;
		}
	}
	
	void InitRadius () {
		initialized = true;
		radius = stats.GetMaxMovement ();
		centerPos = transform.position;
	}

}

