using UnityEngine;
using UnityEngine.UI;
using System.Collections;
// The StartQuestionsExit script decides if the PlayerPref "Games Played" is equal to 0 or not. If it is 0, the question menu will be displayed. If it is not 0, the menu will not be displayed and all the text objects will begin moving by executing all of their MenuStartMovement scripts.
public class StartQuestionsExit : MonoBehaviour {
	// Initializing the menu moving scripts on each object.
	public MenuStartMovement menuStartCubeGame;
	public MenuStartMovement menuStartCollegeLife;
	public MenuStartMovement menuStartHerHouse;
	public MenuStartMovement menuStartInfo;
	public MenuStartMovement menuStartView;
	public MenuStartMovement menuStartMy;
	public MenuStartMovement menuStartProfile;
	public MenuStartMovement menuPneuma;
	public MenuStartMovement menuControls;
	public MenuStartMovement menuQuit;

	// Initializing the lightning objects.
	public LineRenderer l1;
	public LineRenderer l2;
	public LineRenderer l3;

	// Initializing the canvas, exitButton, and underline for Pneuma.
	public MeshRenderer underLine;
	public Canvas quitStartMenu;
	public Button exitButton;

	// Initializing a timer that lasts 2.5 seconds
	float timer = 2.5f;

	// Initializing the integer k with a default value of 0.
	int k = 0;
	// Use this for initialization
	void Start () {

		// If the PlayerPref "Games Played" has not been created yet, create it and set it equal to 0.
		if (!PlayerPrefs.HasKey("Games Played"))
		{
			PlayerPrefs.SetInt("Games Played", 0);
		}

		// Assigning the menuStart scripts to their respected objects
		menuStartCubeGame = menuStartCubeGame.GetComponent<MenuStartMovement>();
		menuStartCollegeLife = menuStartCollegeLife.GetComponent<MenuStartMovement>();
		menuStartHerHouse = menuStartHerHouse.GetComponent<MenuStartMovement>();
		menuStartInfo = menuStartInfo.GetComponent<MenuStartMovement>();
		menuStartView = menuStartView.GetComponent<MenuStartMovement>();
		menuStartMy = menuStartMy.GetComponent<MenuStartMovement>();
		menuStartProfile = menuStartProfile.GetComponent<MenuStartMovement>();
		menuPneuma = menuPneuma.GetComponent<MenuStartMovement>();
		menuControls = menuControls.GetComponent<MenuStartMovement>();
		menuQuit = menuQuit.GetComponent<MenuStartMovement>();
		quitStartMenu = quitStartMenu.GetComponent<Canvas>();

		// Assigning the variable exitButton to the save button on the question menu.
		exitButton = exitButton.GetComponent<Button>();

		// If the PlayerPref "Games Played" is equal to 0, then show the question menu.
		if (PlayerPrefs.GetInt(	"Games Played").Equals(0))
		{
			quitStartMenu.enabled = true;
		}
		else // If the PlayerPref "Games Played" is equal to anything other than 0, then don't show the question menu, set k equal to 1, and executes all the menuStart scripts, which allows the text to start moving.
		{
			k = 1;
			quitStartMenu.enabled = false;
			onClick();
		}


	}

	// Update is called once per frame
	void Update()
	{
		// Once k is equal to 1, start the timer.
		if (k == 1) {
			if (timer > 0)
			{
				timer -= Time.deltaTime;
			}
			else // Once the timer reaches 0, enable the lightning and underline objects (which are the only objects that don't move), and set k back to 0 so that this statement stops running.
			{
				l1.enabled = true;
				l2.enabled = true;
				l3.enabled = true;
				underLine.enabled = true;
				k = 0;
			}
		}
	}
	// This function is called when the Save button is pressed. It allows the menu text to begin moving to their positions. It also sets k equal to 1 which starts the timer.
	public void onClick() { 
		quitStartMenu.enabled = false;
		menuStartCubeGame.enabled = true;
		menuStartCollegeLife.enabled = true;
		menuStartHerHouse.enabled = true;
		menuStartInfo.enabled = true;
		menuStartView.enabled = true;
		menuStartMy.enabled = true;
		menuStartProfile.enabled = true;
		menuPneuma.enabled = true;
		menuControls.enabled = true;
		menuQuit.enabled = true;
		k = 1;
	}
}
