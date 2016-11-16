using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {
	/*
	 * This script teleports you to the next area depending on the story the player is currently at
	 * If the player is not at a point in the story to teleport, hide this object
	 * This also turns off the apartment when at work, and turns off work when at apartment in an effort to reduce lag in-game
	 * */

	GameObject player; //Player 
	PlayerControllerMBTI PCMBTI; //Player Controller (to get place in story)
	MeshRenderer MRend; //Renderer of this object
	Vector3 location, playerLocation; //Location that this object needs to be, as well as the location it needs to teleport the player 
	int personalClicks = 0;

	GameObject apartment, work;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		PCMBTI = player.GetComponent<PlayerControllerMBTI> ();
		MRend = this.gameObject.GetComponent<MeshRenderer> ();
		location = Vector3.zero;

		apartment = GameObject.Find ("Apartment Scene");
		work = GameObject.Find ("Office Scene");
		work.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		//Set location of this object, location to teleport player, disables office/apartment, and turns on/off renderer based on story click counter 
		if (personalClicks != PCMBTI.clicks) {
			if (PCMBTI.clicks == 17) {
				MRend.enabled = true;
				location = new Vector3 (-15.31f, 72.7f, 198.52f);
				playerLocation = new Vector3 (-6.12f, 68.74f, 140.14f);
			} else if (PCMBTI.clicks == 20) {
				MRend.enabled = true;
				location = new Vector3 (-3.38f, 72.16f, 116.5f);


			} else if (PCMBTI.clicks == 27) {
				
				MRend.enabled = true;
				//sceneToLoad = 2;
				location = new Vector3 (-7.56f, 72.16f, 140.91f);
				playerLocation = new Vector3 (-383.14f, 140.8f , 1048);
				work.SetActive (true);
			} else if (PCMBTI.clicks == 29) {
				apartment.SetActive (false);
				MRend.enabled = false;
			} else if (PCMBTI.clicks == 38) {
				apartment.SetActive (true);
				MRend.enabled = true;
				//sceneToLoad = 1;
				location = new Vector3 (-383.14f, 140.8f , 1048);
				playerLocation = new Vector3 (-6.12f, 68.74f, 140.14f);
			} else if (PCMBTI.clicks == 39) {
				work.SetActive (false);
				MRend.enabled = false;
			}
            else if (PCMBTI.clicks == 44)
            {
                MRend.enabled = true;
				location = new Vector3 (-3.38f, 72.16f, 116.5f);

            }
            else {
                MRend.enabled = false;
            }
			personalClicks = PCMBTI.clicks;
		}

		this.gameObject.transform.position = location;

		if (MRend.enabled) {
			//If player is within confines of this cube, set the location to what it needs to be defined above and move clicks forward
			if (player.transform.position.x > this.gameObject.transform.position.x - this.gameObject.transform.lossyScale.x / 2 && player.transform.position.x < this.gameObject.transform.position.x + this.gameObject.transform.lossyScale.x / 2
				&& player.transform.position.z > this.gameObject.transform.position.z - this.gameObject.transform.lossyScale.z / 2 && player.transform.position.z < this.gameObject.transform.position.z + this.gameObject.transform.lossyScale.z / 2) {
				if (PCMBTI.clicks != 20 && PCMBTI.clicks != 44) {
					player.transform.position = playerLocation;
					Camera.main.transform.eulerAngles = Vector3.zero;
					PCMBTI.clicks++;
				} else {
					PCMBTI.clicks++;
				}
			}
		}
	}
}
