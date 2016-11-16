using UnityEngine;
using System.Collections;
// The Timer10 script makes the game move to the next scene once the video is done playing.
public class Timer10 : MonoBehaviour {
	// Initializing the timer for 10 seconds, the length of the intro video.
	float timeRemaining = 10;
	// Use this for initialization
	void Start () {
		// Setting the PlayerPref "Games Played" to 0.
		PlayerPrefs.SetInt ("Games Played", 0);
	}

	// Update is called once per frame
	void Update () {
		// Immediately starts the timer and once the video is done playing, go to the main menu scene.
		if (timeRemaining > 0)
		{
			timeRemaining -= Time.deltaTime;
		}
		else
		{
			Application.LoadLevel(1);
		}
	}
}
