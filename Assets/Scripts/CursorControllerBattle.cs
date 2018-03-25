using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorControllerBattle : MonoBehaviour {

	//Animator animator;
	
	[SerializeField]
	float moveDistance = 1f;
	float oldY = 0;
	float newX = 0;
	float newZ = 0;
	
	public bool posSelect = false;
	public Vector3 targetPosition = new Vector3(0,0,0);

	void Start () {	
	}
	
	void Update () {
		if (Input.anyKey)
			Move();
		else if (transform.position.x % 1 > 0 || transform.position.z % 1 > 0){
			newX = transform.position.x;
			oldY = transform.position.y;
			newZ = transform.position.z;
			if (transform.position.x % 1 > 0) {
				newX = Mathf.Round(transform.position.x);
			}
			if (transform.position.z % 1 > 0) {
				newZ = Mathf.Round(transform.position.z);
			}
			transform.position = new Vector3 (newX, oldY, newZ);
		}
	}
	
	void Move()
	{
		Vector3 rightMovement = Vector3.right * moveDistance * Time.deltaTime * 15f * Input.GetAxis("HorizontalKey");
		Vector3 upMovement = Vector3.forward * moveDistance * Time.deltaTime * 15f * Input.GetAxis("VerticalKey");
		
		rightMovement.y = 0;
		upMovement.y = 0;
		
		Vector3 heading = Vector3.Normalize(rightMovement + upMovement);
		
		transform.forward = heading;
		transform.position += rightMovement;
		transform.position -= upMovement;
	}

}

