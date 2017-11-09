using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour {

	Animator animator;
	
	[SerializeField]
	float moveSpeed = 10f;
	float jumpSpeed = 14f;
	
	Vector3 forward, right;
	
	void Start () {
		forward = Camera.main.transform.forward;
		forward.y = 0;
		forward = Vector3.Normalize(forward);
		right = Quaternion.Euler(new Vector3(0,90,0)) * forward;

		animator = GetComponent<Animator>();
	}
	
	void Update () {
		int h = (int)Input.GetAxis("HorizontalKey");
		int v = (int)Input.GetAxis("VerticalKey");
		
		animator.SetInteger("Horiz", h);
		animator.SetInteger("Vert", v);
		
		if (Input.anyKey)
			Move();
	}
	
	void Move()
	{
		Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("HorizontalKey");
		Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("VerticalKey");
		
		Vector3 heading = Vector3.Normalize(rightMovement + upMovement);
		
		transform.forward = heading;
		transform.position += rightMovement;
		transform.position -= upMovement;
		transform.Translate(Vector3.up * jumpSpeed * Time.deltaTime * Input.GetAxis("JumpKey"));
	}

}

