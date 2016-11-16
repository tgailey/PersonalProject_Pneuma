using UnityEngine;
using System.Collections;

// This script is used specifically for the "View My Profile" text to turn the entire text blue when the cursor is hovering over it.
public class TextControlViewMyProfileHoverBlue : MonoBehaviour {
	// Initializing the MenuStartMovement scripts for the three separate text objects of "View", "My", and "Profile".	
	public MenuStartMovement menuStartView;
	public MenuStartMovement menuStartMy;
	public MenuStartMovement menuStartProfile;

	// Use this for initialization
	void Start () {
		// Assigning the MenuStartMovement scripts to their respected objects
		menuStartView = menuStartView.GetComponent<MenuStartMovement>();
		menuStartMy = menuStartMy.GetComponent<MenuStartMovement>();
		menuStartProfile = menuStartProfile.GetComponent<MenuStartMovement>();
	}

	// Update is called once per frame
	void Update () {

	}

	void OnMouseEnter()
	{
		// If the cursor is hovering above "View", "My", or "Profile", change the color of all of them to blue.
		menuStartView.GetComponent<Renderer>().material.color = Color.blue;
		menuStartMy.GetComponent<Renderer>().material.color = Color.blue;
		menuStartProfile.GetComponent<Renderer>().material.color = Color.blue;

	}

	void OnMouseExit()
	{
		// If the cursor is hovering above "View", "My", or "Profile", change the color of all of them to white.
		menuStartView.GetComponent<Renderer>().material.color = Color.white;
		menuStartMy.GetComponent<Renderer>().material.color = Color.white;
		menuStartProfile.GetComponent<Renderer>().material.color = Color.white;
	}
}
