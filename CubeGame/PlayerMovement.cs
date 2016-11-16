using UnityEngine;
using System.Collections;
using System;

public class PlayerMovement : MonoBehaviour {
	/*
	 * Controlls movement in cubegame
	 * */
	public float movSpeed = 10; //Movement speed
	public float rotSpeed = 10; //Rotation speed
	private Vector3 moveDirection = Vector3.zero;	

	
	// Update is called once per frame
	void Update () {
        TaskScript TS = GameObject.Find("TheTasks").GetComponent<TaskScript>();
		//If we can move, move based on direction on input
		if (TS.canMove) {
		Transform charTransform = GetComponent<Transform> ();
		moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("UD"), Input.GetAxis("Vertical"));
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= movSpeed;
		if (charTransform.position.y < .6 && moveDirection.y < 0)
			moveDirection = new Vector3 (moveDirection.x, 0, moveDirection.z);
		if (charTransform.position.x > 350 && moveDirection.x > 0)
			moveDirection = new Vector3 (0, moveDirection.y, moveDirection.z);
		else if (charTransform.position.x < 150 && moveDirection.x < 0)
			moveDirection = new Vector3 (0, moveDirection.y, moveDirection.z);
		if (charTransform.position.z > 350 && moveDirection.z > 0)
			moveDirection = new Vector3 (moveDirection.x, moveDirection.y, 0);
		else if (charTransform.position.z < 150 && moveDirection.z < 0)
			moveDirection = new Vector3 (moveDirection.x, moveDirection.y, 0);

		charTransform.Translate(moveDirection * Time.deltaTime, Space.World);
		}
		//Debug.Log (clicks);
	}

}
