using UnityEngine;
using System.Collections;
// This script displays the controls of the game and hides the rest of the menu when pressed. It will hide the controls and show the main menu again once enter is pressed.
public class ControlsClick : MonoBehaviour {

	// Initializing an object that includes all the main menu text and the three lightning objects.
	public GameObject Everything;
	public LineRenderer l1, l2, l3;

	// Initiallizing an array of box colliders and an array of mesh renderers
	BoxCollider[] colliders;
	MeshRenderer[] renderers;

	// Initializing the boolean gui and integer vis with their respected default values: false and 0.
	bool gui = false;
	int vis = 0;
	public GUISkin _menuSkin;
	Vector2 scrollViewVector;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		// If the return key is pressed then add 1 to the variable vis.
		if (Input.GetKeyDown(KeyCode.Return))
		{                                                                                                                                    
			vis += 1;
			// If vis is equal to 1, enable the lightning and enable the box colliders and mesh renderers of the Game Object Everything, which includes all the text objects on the main menu.
			if (vis == 1)
			{
				colliders = Everything.GetComponentsInChildren<BoxCollider>();
				foreach (BoxCollider bc in colliders) {
					bc.enabled = true;
				}
				renderers = Everything.GetComponentsInChildren <MeshRenderer>();
				foreach (MeshRenderer mr in renderers) {
					mr.enabled = true;
				}

				l1.enabled = true;
				l2.enabled = true;
				l3.enabled = true;
			}
		}
	}

	// If the text object "Controls" is clicked, set vis equal to 0 and gui to true.
	void OnMouseUp()
	{
		vis = 0;
		gui = true;
	}

	// Once controls is clicked, the OnGUI() function is called. 
	void OnGUI()
	{
		// While the controls gui is displayed, scale the objects on the menu on a 3-dimensional plane, based on the aspect ratio of the screen.
		float rX, rY;
		float scale_width, scale_height;
		scale_width = 1296;
		scale_height = 729;
		rX = Screen.width / scale_width;
		rY = Screen.height / scale_height;
		GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rX, rY, 1));
        if (_menuSkin != null)
        {
            GUI.skin = _menuSkin;
        }

		// If gui is true and vis is equal to 0, execute this code.
		if (gui && vis == 0)
		{
			//Set all the box colliders of the objects contained in the Game Object Everything to the array colliders.
			colliders = Everything.GetComponentsInChildren<BoxCollider>();
			foreach (BoxCollider bc in colliders) {
				bc.enabled = false;
			}

			//Set all the mesh renderers of the objects contained in the Game Object Everything to the array renderers.
			renderers = Everything.GetComponentsInChildren <MeshRenderer>();
			foreach (MeshRenderer mr in renderers) {
				mr.enabled = false;
			}

			// Disabling/hiding the lightning
			l1.enabled = false;
			l2.enabled = false;
			l3.enabled = false;

			//Displaying the controls text
			string text = "Pneuma is a game that features many different games within it. "
				+ "Each game tests different aspects of your personality, and requires a different set of controls. "
				+ "Here we will list the controls by game played.\r\n\r\n"
				+ "Cube Game\r\nWASD to move\r\nShift to move up, Control to move down\r\nDrag middle mouse button to move camera\r\nLeft Click to Move Text\r\n\r\n"
				+ "A Walk to Her House\r\nWASD to move\r\nMove mouse to look\r\nLeft Click to Move Text\r\n\r\n"
				+ "College Life\r\nWASD to move\r\nMove mouse to look\r\nTab to open phone\r\nLeft Click to Move Text\r\nE to Interact\r\n\r\n"
				+ "At any time, escape may be pressed to immediately exit the game.\r\n\r\n<Press enter to move back to Main Menu>";
			scrollViewVector = GUI.BeginScrollView(new Rect(30, 30, 1296 - 60, 729 - 60), scrollViewVector, new Rect(0, 0, 1296 - 80, 1250));
			//GUI.Label(new Rect(10, 10, 1296 - 90, 2 * 729 - 240), text);
            GUI.Label(new Rect(10, 10, 1296 - 90, 230), "Pneuma is a game that features many different games within it."
                + "\r\nEach game tests different aspects of your personality, and requires a different set of controls.\r\nHere we will list the controls by game played.\r\n\r\n");
            _menuSkin.label.fontSize = 80;
             GUI.Label(new Rect(10, 180, 1296 - 90, 200), "Cube Game\r\n");
            _menuSkin.label.fontSize = 35;
            GUI.Label(new Rect(10, 300, 1296 - 90, 240), "WASD to move\r\nShift to move up, Control to move down\r\nDrag middle mouse button to move camera\r\nLeft Click to Move Text\r\n\r\n");
            _menuSkin.label.fontSize = 80;
            GUI.Label(new Rect(10, 460, 1296 - 90, 200), "A Walk to Her House\r\n");
            _menuSkin.label.fontSize = 35;
            GUI.Label(new Rect(10, 570, 1296 - 90, 200), "WASD to move\r\nMove mouse to look\r\nLeft Click to Move Text/Initiate Dialogue\r\n\r\n");
            _menuSkin.label.fontSize = 80;
            GUI.Label(new Rect(10, 700, 1296 - 90, 200), "College Life\r\n");
            _menuSkin.label.fontSize = 35;
            GUI.Label(new Rect(10, 800, 1296 - 90, 300), "WASD to move\r\nMove mouse to look\r\nTab to open phone\r\nLeft Click to Move Text\r\nE to Interact\r\n\r\n");
            GUI.Label(new Rect(10, 1000, 1296 - 90, 200), "At any time, escape may be pressed to immediately exit the game.\r\n\r\n<Press enter to move back to Main Menu>");

			GUI.EndScrollView();
		}
	}
}