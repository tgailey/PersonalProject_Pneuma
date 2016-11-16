using UnityEngine;
using System.Collections;
// This script launches the game you selected on the main menu.
public class GameClick : MonoBehaviour {
	// Initializing the booleans that check for what game button is pressed.
	public bool cube, house, college;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnMouseUp() {
		// If the text, "Cube Game", is clicked then load that level.
		if(cube)
			Application.LoadLevel(2);
		// If the text, "Her House", is clicked then load that level.
		if (house)
			Application.LoadLevel(3);
		// If the text, "College Life", is clicked then load that level.
		if (college)
			Application.LoadLevel(5);
	}
}
