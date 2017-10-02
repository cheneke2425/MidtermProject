using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float moveSpeed = 10f;

	Vector3 moveDirection;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		CharacterController cController = GetComponent<CharacterController>();

		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		moveDirection = new Vector3(-vertical, 0f, horizontal);
		if (moveDirection.magnitude > 1f)
		{
			moveDirection = Vector3.Normalize(moveDirection);
		}

		moveDirection *= moveSpeed;

		cController.Move(moveDirection * Time.deltaTime);

	}
}
