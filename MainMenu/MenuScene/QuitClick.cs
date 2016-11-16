using UnityEngine;
using System.Collections;
// This is a simple script that quits the game when you press the quit button.
public class QuitClick : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	// If the quit button is clicked, quit the game.
	void OnMouseUp() {
		Application.Quit();
	}
}
