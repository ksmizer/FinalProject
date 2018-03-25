using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyMovementBattle : MonoBehaviour {

	//Animator animator;
	[SerializeField]
	float moveSpeed = 1f;
	//float jumpSpeed = 10f;
	GameObject hero;
	GameObject self;
	UnityEngine.AI.NavMeshAgent agent;
	
	public Vector3 targetPosition;
	public Vector3 initialPosition;
	//public bool posSelect;
	public float maxMove;
	
	
	void Start () {
		hero = GameObject.Find("CharSprite");
		targetPosition = hero.transform.position;
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		self = GameObject.Find("EnemySprite");
		initialPosition = self.transform.position;
		Init();
	}
	
	void Update () {
		if (Input.GetKeyDown("e")) {
			Move();
		}
	}
	
	void Init()	{
		maxMove = 6f;
	}
	
	void Move()	{
		//Debug.Log("Return pressed.");
		//targetPosition = hero.transform.position;
		//while (transform.position != targetPosition) {
			//transform.position  = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
		//}
		if (agent.destination != hero.transform.position) {
			//while (Vector3.Distance(initialPosition, self.transform.position) <= maxMove) {
				agent.destination = hero.transform.position;
			//}
		}
	}

}

