using UnityEngine;
using System.Collections;
// Once this script is executed, all the text objects on the main menu begin transitioning to their designated destinations using each of their own unique velocity and acceleration equations.
public class MenuStartMovement : MonoBehaviour {
	// Initializing the initial velocity of various text objects
	float currentVelocity = 2.0f; // Velocity of the "Cube Game" text, "View" text, "Info" text. 
	float currentVelocity2 = -2.4f;  // Velocity of the "Her House" text, "Profile" text, and "Controls" text.
	float currentVelocity3 = 1.2f; // Velocity of the "My" text.
	float currentVelocity4 = -1.2f;  // Velocity of the "Pneuma" text. 
	float currentVelocity5 = 1.2f; // Velocity of the "Quit" text.
	float currentVelocity6 = -2.2f; // Velocity of the "College Life" text.
	float decelerationRate = 1.4098099f; // Deceleration rate of all text objects

	float timerthing = 2.5f; // Initializing a timer that lasts 2.5 seconds

	// Initializing the Box Colliders of the text objects
	public BoxCollider cubeGame;
	public BoxCollider collegeLife;
	public BoxCollider herHouse;
	public BoxCollider info;
	public BoxCollider view;
	public BoxCollider controls;
	public BoxCollider quit;

	// Initializing boolean variables for each of the text objects to tell the program to apply the respected velocities to them.
	public bool isPneuma = false;
	public bool isCubeGame = false;
	public bool isCollegeLife = false;
	public bool isHerHouse = false;
	public bool isInfo = false;
	public bool isView = false;
	public bool isMy = false;
	public bool isProfile = false;
	public bool isControls = false;
	public bool isQuit = false;
	// Use this for initialization
	void Start () {

	}
	// Update is called once per frame
	void Update()
	{
		// Starting the timer right away which lasts 2.5 seconds. After the timer reaches zero, enable the box colliders of all the text objects so that they can be clicked.
		if (timerthing > 0)
		{
			timerthing -= Time.deltaTime;
		}
		else
		{
			cubeGame.enabled = true;
			collegeLife.enabled = true;
			herHouse.enabled = true;
			info.enabled = true;
			view.enabled = true;
			controls.enabled = true;
			quit.enabled = true;
		}
		//Every text object is exactly 160 units from its destination and needs to move to its respected destination in 2.5 seconds.
		// Since each object varies in font size, character length, and orientation; each text object has its own unique equation.
		//isCubeGame is on the left side of the screen initially and moves right (positive velocity on x axis), so it uses the positive currentVelocity
		//isHerHouse is on the right side of the screen initially and moves to the left (negative velocity on x axis), so it uses the negative currentVelocity2
		//isMy is at the bottom of the screen initially and moves up (positive velocity on y axis), so it uses the positive currentVelocity3
		//isPneuma is at the top of the screen initially and moves down (negative velocity on y axis), so it uses the negative currentVelocity4
		if (isCubeGame && transform.position.x < -115)
		{
			currentVelocity = currentVelocity - (decelerationRate * decelerationRate/160);
			transform.Translate(currentVelocity, 0, 0);
		}   

		if (isCollegeLife && transform.position.y > 16)
		{
			currentVelocity6 = currentVelocity6 + (decelerationRate * decelerationRate / 148);
			transform.Translate(0, currentVelocity6, 0);
		}

		if (isHerHouse && transform.position.x > 15)
		{
			currentVelocity2 = currentVelocity2 + (decelerationRate * decelerationRate / 140);
			transform.Translate(currentVelocity2, 0, 0);
		}

		if (isView && transform.position.x < -115)
		{
			currentVelocity = currentVelocity - (decelerationRate * decelerationRate / 160);
			transform.Translate(currentVelocity, 0, 0);
		}

		if (isInfo && transform.position.x < -115)
		{
			currentVelocity = currentVelocity - (decelerationRate * decelerationRate/160);
			transform.Translate(currentVelocity, 0, 0);
		}

		if (isProfile && transform.position.x > 15)
		{
			currentVelocity2 = currentVelocity2 + (decelerationRate * decelerationRate / 140);
			transform.Translate(currentVelocity2, 0, 0);
		}

		if (isMy && transform.position.y < -51)
		{
			currentVelocity3 = currentVelocity3 - (decelerationRate * decelerationRate / 270);
			transform.Translate(0, currentVelocity3, 0);
		}

		if (isPneuma && transform.position.y > 62.62815)
		{
			currentVelocity4 = currentVelocity4 + (decelerationRate * decelerationRate / 270);
			transform.Translate(0, currentVelocity4, 0);
		}

		if (isControls && transform.position.x > 35.5)
		{
			currentVelocity2 = currentVelocity2 + (decelerationRate * decelerationRate / 125);
			transform.Translate(currentVelocity2, 0, 0);
		}

		if (isQuit && transform.position.y < -28)
		{
			currentVelocity5 = currentVelocity5 - (decelerationRate * decelerationRate / 271);
			transform.Translate(0, currentVelocity5, 0);
		}

	}
}