using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelRotation : MonoBehaviour {

	public float rotationSpeed = 5f;

	Vector3 targetPos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		targetPos =  - GetComponentInParent<Rigidbody>().velocity;

		Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetPos, rotationSpeed * Time.deltaTime, 0.0f);
		transform.rotation = Quaternion.LookRotation(newDirection);


		
	}
}
