using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyMovementBattle : MonoBehaviour {

	//Animator animator;

	//public Canvas EturnCanvas;
	
	GameObject[] heroes;
	GameObject closest;
	GameObject combat;
	TurnBasedCombat state;
	UnityEngine.AI.NavMeshAgent agent;
	EnemyHealth stats;
	EnemyAttack attack;
	EnemyManager manager;
	
	Vector3 centerPos; Vector3 currentPos;
	Vector3 distToHero;
	float distance; float radius; float atkRadius;
	float atkDistance; float movDistance;
	bool initialized; bool moved;
	bool attacked; bool inRange;
	bool active; bool found;
	bool done;
	
	void Start () {
		combat = GameObject.Find("Combat");
		state = combat.GetComponent <TurnBasedCombat> ();
		manager = combat.GetComponent <EnemyManager> ();
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		stats = GetComponent <EnemyHealth> ();
		attack = GetComponent <EnemyAttack> ();
		moved = false;
		radius = stats.GetMaxMovement();
	}
	
	void Update () {
		if (state.currentState == TurnBasedCombat.BattleStates.ENEMYTURN) {
			if (!moved /*&& !EturnCanvas.gameObject.activeSelf*/) {
				inRange = false;
				Move();
				moved = true;
			}
			if (moved && closest != null) {
				currentPos = transform.position;
				movDistance = Vector3.Distance(currentPos, centerPos);
				if (movDistance > radius || inRange) {
					agent.destination = transform.position;
				}
			}
			if (closest != null) {
				atkRadius = stats.GetAttackRadius(1);
				Vector3 diff = closest.transform.position - transform.position;
				atkDistance = diff.sqrMagnitude;
				if (atkDistance <= atkRadius * 3f) {
					inRange = true;
				} else {
					inRange = false;
				}
			}
			if (!attacked && closest != null && inRange /*&& !EturnCanvas.gameObject.activeSelf*/) {
				attack.Attack(closest);
				attacked = true;
				StartCoroutine(Check());
				//manager.CheckAllies();
			}
		} else {
			moved = false;
			attacked = false;
			done = false;
		}
	}
	
	IEnumerator Check() {
		yield return new WaitForSeconds(1);
		manager.CheckAllies();
	}
	
	void Move()	{
		closest = null;
		centerPos = transform.position;
		float distance = Mathf.Infinity;
		heroes = GameObject.FindGameObjectsWithTag("Ally");
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
	
	void CheckDone () {
		if (attacked) {
			done = true;
		}
		if (moved && !inRange) {
			done = true;
		}
	}
	
	public bool GetIfDone () { return done; }
}

