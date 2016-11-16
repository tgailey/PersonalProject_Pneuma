using UnityEngine;
using System.Collections;
// What this script does is display info about the game and hide the rest of the menu when pressed. It will hide the info text and show the main menu again once enter is pressed.
public class InfoClick : MonoBehaviour {

	// Initializing an object that includes all the main menu text and the three lightning objects.
	public GameObject Everything;
	public LineRenderer l1, l2, l3;
    public GUISkin _menuSkin;
	// Initiallizing an array of box colliders and an array of mesh renderers
	BoxCollider[] colliders;
	MeshRenderer[] renderers;

	// Initializing the boolean gui and integer vis with their respected default values: false and 0.
	bool gui = false;
	int vis = 0;

	void Start () 
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
	void OnMouseUp() {

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
		if (gui && vis==0) 
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

			//Displaying the info text
			GUI.Label(new Rect(30, 10, 1296 - 60, 729-20), "This is a game that will delve deep into your head and use various techniques and tricks to reveal who you really are to yourself, and potentially others. It allows you to obtain a better understanding of who you are, why you act certain ways, and how you can better your connection with you and fellow human beings. \r\n\r\nThe game may be used as a personal tool, a creative way to study others, or a way for businesses to identify future employees with a deeper understanding. Overall, this is a way for us to teach you about… you." 
				+  "\r\n\r\n\r\nThe outlines of the needed psychology of becoming can be discovered by looking within ourselves; for it is knowledge of our own uniqueness that supplies the first, and probably the best, hints for acquiring orderly knowledge of others. – Gordon W.Allport"
				+ "\r\n<Press Enter to Return to Main Menu>");
		}
	}
}
