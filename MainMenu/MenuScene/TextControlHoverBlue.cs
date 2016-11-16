using UnityEngine;
using System.Collections;
// This script allows for the text to change to blue when the cursor is hovering over it. It lets the player know that the blue text will be selected when clicked.
public class TextControlHoverBlue : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	// If the cursor is within the box collider of the text object, turn the text's collor blue.
	void OnMouseEnter()
	{

		gameObject.GetComponent<Renderer>().material.color = Color.blue;

	}

	// If the cursor is not withing the box collider of the text object, turn the text's collor white.
	void OnMouseExit()
	{
		gameObject.GetComponent<Renderer>().material.color = Color.white;
	}
}
