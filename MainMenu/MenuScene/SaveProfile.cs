using UnityEngine;
using UnityEngine.UI;
using System.Collections;
// This script is the first script to be executed. It determines whether to show the questions menu or not. If it is showing the questions menu, save the player's input once the save button is pressed.
public class SaveProfile : MonoBehaviour {
	// Initializes the canvas and everything inside of it.
	public Canvas StartCanvas;
	public Button SaveButton;
	public InputField NameInput;
	public InputField AgeInput;
	public Toggle BoyToggle;
	public Toggle GirlToggle;

	//Toggle to save gender
	string gender;
	// Use this for initialization
	void Start () {
		// Setting the canvas and everything inside it to its respected objects.
		StartCanvas = StartCanvas.GetComponent<Canvas> ();
		SaveButton = SaveButton.GetComponent<Button> ();
		NameInput = NameInput.GetComponent<InputField> ();
		AgeInput = AgeInput.GetComponent<InputField> ();
		BoyToggle = BoyToggle.GetComponent<Toggle> ();
		GirlToggle = GirlToggle.GetComponent<Toggle> ();
	}

	// Update is called once per frame
	void Update () {

	}

	public void SaveP(){

		// If the boy toggle is on, set the string, gender, to equal "Boy"
		if (BoyToggle.isOn) {
			gender = "Boy";
		}
		// If the girl toggle is on, set the string, gender, to equal "Girl"
		if (GirlToggle.isOn) {
			gender = "Girl";
		}

		// Saving the player's name, age, and gender that they selected on their first visit to the main menu.
		PlayerPrefs.SetString ("Player Name", NameInput.text);
		PlayerPrefs.SetString ("Player Age", AgeInput.text);
		PlayerPrefs.SetString ("Player Gender", gender);
	}
}
