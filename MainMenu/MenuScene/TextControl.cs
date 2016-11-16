using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//This script is executed once the "View My Profile" button is pressed on the main menu. It displays your Personality Profile.
public class TextControl : MonoBehaviour
{
	// Initializing an object that includes all the main menu text and the three lightning objects.
	public GameObject Everything;
	public LineRenderer l1, l2, l3;

	// Initiallizing an array of box colliders and an array of mesh renderers
	BoxCollider[] colliders;
	MeshRenderer[] renderers;

	// Initializing the boolean gui and integer vis with their respected default values: false and 0.
	private bool gui = false;
	private int vis = 0;

	// Initializing styles for the text in the gui
	public GUISkin styleHeading;
	public GUISkin styleText;
	public GUISkin styleTransition;

	// Initializing the scroll bar
	Vector2 scrollViewVector;

	string typeOverview = "";
	// Use this for initialization
	void Start()
	{
		//PlayerPrefs.SetString ("MBTI", "INTJ");
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
	// If the text object "View My Profile" is clicked, set vis equal to 0 and gui to true. Disable everything in the main menu.
	void OnMouseUp()
	{

		vis = 0;
		gui = true;

		colliders = Everything.GetComponentsInChildren<BoxCollider>();
		foreach (BoxCollider bc in colliders) {
			bc.enabled = false;
		}

		renderers = Everything.GetComponentsInChildren <MeshRenderer>();
		foreach (MeshRenderer mr in renderers) {
			mr.enabled = false;
		}
		l1.enabled = false;
		l2.enabled = false;
		l3.enabled = false;        
	}

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
		// If gui is true and vis is equal to 0, execute this code.
		if (gui)
		{
			// If the PlayerPrefs Player Name, Player Age, and Player Gender exist, execute this code.
			if ((PlayerPrefs.HasKey("Player Name")) && (PlayerPrefs.HasKey("Player Age")) && (PlayerPrefs.HasKey("Player Gender")))
			{
				// If vis is equal to 0 execute the following code.
				if (vis == 0)
				{
					// Change the skin (font/size/color) of the gui to styleHeading and write the text "This is your Personality Profile" at the top of the screen.
					GUI.skin = styleHeading;
					GUI.Label(new Rect(0, 0, Screen.width, 60), "This is your Personality Profile!");
					// Change the skin of the gui to styleText.
					GUI.skin = styleText;
					// Begin the scroll bar view and write the answers to the questions that the player was presented with at the beginning of the game.
					scrollViewVector = GUI.BeginScrollView(new Rect(30, 50, 1296 - 60, 729 - 60), scrollViewVector, new Rect(0, 0, 1296 - 80, 2200));
					GUI.Label(new Rect(0, 30, 1296, 30), "Name: " + PlayerPrefs.GetString("Player Name"));
					GUI.Label(new Rect(0, 60, 1296, 30), "Age: " + PlayerPrefs.GetString("Player Age"));
					GUI.Label(new Rect(0, 90, 1296, 30), "Gender: " + PlayerPrefs.GetString("Player Gender"));

					// Write the PlayerPrefs that determine the player's personality based on their choices in the Cube Game.
					#region CubeGame
					//Cube
					#region Cube
					if (PlayerPrefs.GetInt("Cube Size") == 0)
					{
						GUI.Label(new Rect(0, 150, 1296, 30), "Personal Ego: Not discovered yet. Play the game to find out.");
					}
					else if (PlayerPrefs.GetInt("Cube Size") < 20 && PlayerPrefs.GetInt("Cube Size") > 0)
					{
						GUI.Label(new Rect(0, 150, 1296, 30), "Personal Ego: Quite insecure about yourself.");
					}
					else if (PlayerPrefs.GetInt("Cube Size") < 60)
					{
						GUI.Label(new Rect(0, 150, 1296, 30), "Personal Ego: Fairly soft - spoken about yourself.");
					}
					else if (PlayerPrefs.GetInt("Cube Size") < 140)
					{
						GUI.Label(new Rect(0, 150, 1296, 30), "Personal Ego: On the middle ground when it comes to how sure you are about yourself.");
					}
					else if (PlayerPrefs.GetInt("Cube Size") < 220)
					{
						GUI.Label(new Rect(0, 150, 1296, 30), "Personal Ego: Pretty confident about yourself.");
					}
					else if (PlayerPrefs.GetInt("Cube Size") > 220)
					{
						GUI.Label(new Rect(0, 150, 1296, 30), "Personal Ego: Place yourself above all odds.");
					}
					else
					{
						GUI.Label(new Rect(0, 150, 1296, 30), "Personal Ego: Not discovered yet. Play the game to find out.");
					}
					if (PlayerPrefs.GetInt("Cube Position") <= 4 && PlayerPrefs.GetInt("Cube Position") >= 1)
					{
						GUI.Label(new Rect(0, 180, 1296, 30), "World Focus: Practical and realistic");
					}

					else if (PlayerPrefs.GetInt("Cube Position") > 4)
					{
						GUI.Label(new Rect(0, 180, 1296, 30), "World Focus: Idealistic and head is in the clouds");
					}
					else
					{
						GUI.Label(new Rect(0, 180, 1296, 30), "World Focus: Not discovered yet. Play the game to find out.");
					}

					if (PlayerPrefs.GetString("Cube Texture").Equals("Mirror"))
					{
						GUI.Label(new Rect(0, 210, 1296, 30), "Personal Idealology: You reflect the wills of other people.");
					}

					else if (PlayerPrefs.GetString("Cube Texture").Equals("Rubix Cube"))
					{
						GUI.Label(new Rect(0, 210, 1296, 30), "Personal Idealology: You feel pretty complex and not understood.");
					}

					else if (PlayerPrefs.GetString("Cube Texture").Equals("Metal Box"))
					{
						GUI.Label(new Rect(0, 210, 1296, 30), "Personal Idealology: You have secrets locked up.");
					}
					else if (PlayerPrefs.GetString("Cube Texture").Equals("Wooden Box") || PlayerPrefs.GetString("Cube Texture").Equals("Dice") || PlayerPrefs.GetString("Cube Texture").Equals("Plain"))
					{
						GUI.Label(new Rect(0, 210, 1296, 30), "Personal Idealology: Nothing specific found.");
					}
					else
					{
						GUI.Label(new Rect(0, 210, 1296, 30), "Personal Idealology: Not discovered yet. Play the game to find out.");
					}

					if (PlayerPrefs.GetInt("Cube Transparancy") <= 71 && PlayerPrefs.GetInt("Cube Transparancy") >= 1)
					{
						GUI.Label(new Rect(0, 240, 1296, 30), "Personal Openness: An open book, transparent, and inviting.");
					}

					else if (PlayerPrefs.GetInt("Cube Transparancy") >= 72)
					{
						GUI.Label(new Rect(0, 240, 1296, 30), "Personal Openness: Protective of mind.");
					}
					else
					{
						GUI.Label(new Rect(0, 240, 1296, 30), "Personal Openness: Not discovered yet. Play the game to find out.");
					}

					if (PlayerPrefs.GetInt("Cube Glow") == 2)
					{
						GUI.Label(new Rect(0, 270, 1296, 30), "Personal WorldView: Optimistic and positive person. Aims to raise the spirit of others.");
					}

					else if (PlayerPrefs.GetInt("Cube Glow") == 1)
					{
						GUI.Label(new Rect(0, 270, 1296, 30), "Personal WorldView: Not optimistic, possible mix of optimism and pessimism.");
					}
					else
					{
						GUI.Label(new Rect(0, 270, 1296, 30), "Personal WorldView: Not discovered yet. Play the game to find out.");

					}
					#endregion

					//Ladder
					#region Ladder
					if (PlayerPrefs.GetInt("Ladder Leaning") == 2)
					{
						GUI.Label(new Rect(0, 300, 1296, 30), "Frienship Dependency: Friends dependent on you.");
					}

					else if (PlayerPrefs.GetInt("Ladder Leaning") == 1)
					{
						GUI.Label(new Rect(0, 300, 1296, 30), "Frienship Dependency: Friends are more independent, less close, or you feel more dependent on them.");
					}
					else
					{
						GUI.Label(new Rect(0, 300, 1296, 30), "Frienship Dependency: Not discovered yet. Play the game to find out.");
					}

					if (PlayerPrefs.GetInt("Ladder Strength") == 2)
					{
						GUI.Label(new Rect(0, 330, 1296, 30), "Frienship Strength: Friendships feel strong and secure.");
					}

					else if (PlayerPrefs.GetInt("Ladder Strength") == 1)
					{
						GUI.Label(new Rect(0, 330, 1296, 30), "Frienship Strength: Friendships feel weak and falling apart.");
					}
					else
					{
						GUI.Label(new Rect(0, 330, 1296, 30), "Frienship Strength: Not discovered yet. Play the game to find out.");
					}


					#endregion

					//Horse
					#region Horse
					if (PlayerPrefs.GetString("Horse Distance").Equals("Close"))
					{
						GUI.Label(new Rect(0, 360, 1296, 30), "Romantic Engagement: You see you and your ideal current or imaginary significant other as very close.");
					}

					else if (PlayerPrefs.GetString("Horse Distance").Equals("Medium"))
					{
						GUI.Label(new Rect(0, 360, 1296, 30), "Romantic Engagement: You see you and your ideal current or imaginary significant other as some distance apart. You may be drifting apart or coming together, or feel like the love of your life is just around the corner.");
					}

					else if (PlayerPrefs.GetString("Horse Distance").Equals("Far"))
					{
						GUI.Label(new Rect(0, 360, 1296, 30), "Romantic Engagement: Far distance from the cube- You see you and your ideal current or imaginary significant other as very far away. You may feel very distant from your current significant other, or feel like the love of your life is very far away from you right now and it will be a long time before you find them.");
					}
					else
					{
						GUI.Label(new Rect(0, 360, 1296, 30), "Romantic Engagement: Not discovered yet. Play the game to find out.");

					}

					if (PlayerPrefs.GetInt("Horse Mythicality") == 2)
					{
						GUI.Label(new Rect(0, 390, 1296, 30), "Romantic Viewpoint: Ideal current or imaginary significant other is like a dream, and seems impossible.");
					}

					else if (PlayerPrefs.GetInt("Horse Mythicality") == 1)
					{
						GUI.Label(new Rect(0, 390, 1296, 30), "Romantic Viewpoint: Ideal current or imaginary significant other is more realistic, steady, and reliable.");
					}
					else
					{
						GUI.Label(new Rect(0, 390, 1296, 30), "Romantic Viewpoint: Not discovered yet. Play the game to find out.");
					}

					if (PlayerPrefs.GetString("Horse Description").Equals("Strong, steady, workhorse"))
					{
						GUI.Label(new Rect(0, 420, 1296, 30), "Romantic Desire: Ideal current or imaginary significant other is steady and reliable.");
					}

					else if (PlayerPrefs.GetString("Horse Description").Equals("Battle Horse"))
					{
						GUI.Label(new Rect(0, 420, 1296, 30), "Romantic Desire: Ideal current or imaginary significant other is strong and safe.");
					}

					else if (PlayerPrefs.GetString("Horse Description").Equals("Pretty Show Horse"))
					{
						GUI.Label(new Rect(0, 420, 1296, 30), "Romantic Desire: Ideal current or imaginary significant other is cute, nice, and loving.");
					}

					else if (PlayerPrefs.GetString("Horse Description").Equals("Stallion"))
					{
						GUI.Label(new Rect(0, 420, 1296, 30), "Romantic Desire: Ideal current or imaginary significant other is strong, beautiful, and independent.");
					}
					else
					{
						GUI.Label(new Rect(0, 420, 1296, 30), "Romantic Desire: Not discovered yet. Play the game to find out.");
					}

					if (PlayerPrefs.GetInt("Horse Saddle") == 2)
					{
						GUI.Label(new Rect(0, 450, 1296, 30), "Romantic Control: You see yourself as being in control with your relationships.");
					}

					else if (PlayerPrefs.GetInt("Horse Saddle") == 1)
					{
						GUI.Label(new Rect(0, 450, 1296, 30), "Romantic Control: You like to give control to your significant other.");
					}
					else
					{
						GUI.Label(new Rect(0, 450, 1296, 30), "Romantic Control: Not discovered yet. Play the game to find out.");

					}
					#endregion

					//Flowers
					#region Flowers
					if (PlayerPrefs.GetString("Flower Amount").Equals("None"))
					{
						GUI.Label(new Rect(0, 480, 1296, 30), "Children Amount: You don't see yourself having any children.");
					}

					else if (PlayerPrefs.GetString("Flower Amount").Equals("Small"))
					{
						GUI.Label(new Rect(0, 480, 1296, 30), "Children Amount: You don't see yourself having many, if any at all children.");
					}

					else if (PlayerPrefs.GetString("Flower Amount").Equals("Medium"))
					{
						GUI.Label(new Rect(0, 480, 1296, 30), "Children Amount: You see yourself having an average sized family with a normal amount of kids.");
					}

					else if (PlayerPrefs.GetString("Flower Amount").Equals("Large"))
					{
						GUI.Label(new Rect(0, 480, 1296, 30), "Children Amount: You see yourself having a large family with a lot of kids.");
					}
					else
					{
						GUI.Label(new Rect(0, 480, 1296, 30), "Children Amount: Not discovered yet. Play the game to find out.");
					}



					if (PlayerPrefs.GetInt("Flower Vitality") == 2)
					{
						GUI.Label(new Rect(0, 510, 1296, 30), "Children Health: You presume your kids will be very healthy.");
					}

					else if (PlayerPrefs.GetInt("Flower Vitality") == 1)
					{
						GUI.Label(new Rect(0, 510, 1296, 30), "Children Health: You worry your kids may have some troubles or weaknesses.");
					}
					else
					{
						GUI.Label(new Rect(0, 510, 1296, 30), "Children Health: Not discovered yet. Play the game to find out.");
					}
					#endregion

					//Storm
					#region Storm
					if (PlayerPrefs.GetInt("Storm Threat") == 1)
					{
						GUI.Label(new Rect(0, 540, 1296, 30), "Current Threat: You see the biggest threat in your mind as being mostly overcome and fading away.");
					}

					else if (PlayerPrefs.GetInt("Storm Threat") == 2)
					{
						GUI.Label(new Rect(0, 540, 1296, 30), "Current Threat: You see the biggest threat in your mind as coming directly toward you and ready to wreak havoc.");
					}

					else if (PlayerPrefs.GetInt("Storm Threat") == 3)
					{
						GUI.Label(new Rect(0, 540, 1296, 30), "Current Threat: You see yourself as being in the middle of danger and pain, and potentially have some immediate trauma in your life.");
					}
					else
					{
						GUI.Label(new Rect(0, 540, 1296, 30), "Current Threat: Not discovered yet. Play the game to find out.");
					}

					if (PlayerPrefs.GetInt("Cube Affected") == 2)
					{
						GUI.Label(new Rect(0, 570, 1296, 30), "Personal Threat: You see the danger and pain in your life as affecting yourself and state of mind.");
					}
					else if (PlayerPrefs.GetInt("Cube Affected") == 1)
					{
						GUI.Label(new Rect(0, 570, 1296, 30), "Personal Threat: The danger and pain in your life is not currently bothering you or disabling you.");
					}
					else
					{
						GUI.Label(new Rect(0, 570, 1296, 30), "Personal Threat: Not discovered yet. Play the game to find out.");
					}

					if (PlayerPrefs.GetInt("Ladder Affected") == 2)
					{
						GUI.Label(new Rect(0, 600, 1296, 30), "Friendship Threat: You see the danger and pain in your life as affecting your friendships.");
					}
					else if (PlayerPrefs.GetInt("Ladder Affected") == 1)
					{
						GUI.Label(new Rect(0, 600, 1296, 30), "Friendship Threat: The danger and pain in your life is not currently bothering your friendships.");
					}
					else
					{
						GUI.Label(new Rect(0, 600, 1296, 30), "Friendship Threat: Not discovered yet. Play the game to find out.");
					}

					if (PlayerPrefs.GetInt("Horse Affected") == 2)
					{
						GUI.Label(new Rect(0, 630, 1296, 30), "Romantic Threat: You see the danger and pain in your life as affecting your ideal current or imaginary significant other.");
					}

					else if (PlayerPrefs.GetInt("Horse Affected") == 1)
					{
						GUI.Label(new Rect(0, 630, 1296, 30), "Romantic Threat: The danger and pain in your life is not currently bothering your ideal current or imaginary significant other.");
					}
					else
					{
						GUI.Label(new Rect(0, 630, 1296, 30), "Romantic Threat: Not discovered yet. Play the game to find out.");

					}

					if (PlayerPrefs.GetInt("Flowers Affected") == 2)
					{
						GUI.Label(new Rect(0, 660, 1296, 30), "Children Threat: You see the danger and pain in your life as affecting your current or future children.");
					}

					else if (PlayerPrefs.GetInt("Flowers Affected") == 1)
					{
						GUI.Label(new Rect(0, 660, 1296, 30), "Children Threat: The danger and pain in your life is not currently bothering your current or future children.");
					}
					else
					{
						GUI.Label(new Rect(0, 660, 1296, 30), "Children Threat: Not discovered yet. Play the game to find out.");
					}

					#endregion
					#endregion

					// Write the PlayerPrefs that determine the player's personality based on their choices in the Her House game.
					#region WalkToHouse
					if (PlayerPrefs.GetInt("First Path Chosen") == 2)
					{
						GUI.Label(new Rect(0, 690, 1296, 30), "Romantic Attitude: You take your time and do not fall in love easily.");
					}
					else if (PlayerPrefs.GetInt("First Path Chosen") == 1)
					{
						GUI.Label(new Rect(0, 690, 1296, 30), "Romantic Attitude: You fall in love quickly and easily.");
					}
					else
					{
						GUI.Label(new Rect(0, 690, 1296, 30), "Romantic Attitude: Not discovered yet. Play the game to find out.");
					}

					if (PlayerPrefs.GetInt("Red Flower Percentage") == 0)
					{
						GUI.Label(new Rect(0, 720, 1296, 30), "Romantic Expectations: Not discovered yet. Play the game to find out.");
					}
					else
					{
						GUI.Label(new Rect(0, 720, 1296, 30), "Romantic Expectations: You give roughly " + ((PlayerPrefs.GetFloat("Red Flower Percentage")) - 1).ToString()
							+ "% in your relationships, and expect to receive " + ((PlayerPrefs.GetFloat("Red Flower Percentage") - 1).ToString() + "% back in return."));
					}

					if (PlayerPrefs.GetInt("Get Sig Other") == 2)
					{
						GUI.Label(new Rect(0, 750, 1296, 30), "Romantic Attitude: Pretty direct. If there is a problem, you confront it and deal with it. You want to work it out right away.");
					}
					else if (PlayerPrefs.GetInt("Get Sig Other") == 1)
					{
						GUI.Label(new Rect(0, 750, 1296, 30), "Romantic Attitude: You may beat around the bush, maybe asking a third party to intervene. Avoidance of problems runs high.");
					}
					else
					{
						GUI.Label(new Rect(0, 750, 1296, 30), "Romantic Attitude: Not discovered yet. Play the game to find out.");
					}

					if (PlayerPrefs.GetInt("Place Chosen") == 2)
					{
						GUI.Label(new Rect(0, 780, 1296, 30), "Romantic Reassurance Need: You don't expect or need to see your loved one too often.");
					}
					else if (PlayerPrefs.GetInt("Place Chosen") == 1)
					{
						GUI.Label(new Rect(0, 780, 1296, 30), "Romantic Reassurance Need: You need lots of reassurance in the relationship, and you'd want to see your loved one every day, if possible.");
					}
					else
					{
						GUI.Label(new Rect(0, 780, 1296, 30), "Romantic Reassurance Need: Not discovered yet. Play the game to find out.");
					}
					if (PlayerPrefs.GetInt("Girlfriend State") == 1)
					{
						GUI.Label(new Rect(0, 810, 1296, 30), "Romantic Acceptance: You accept your loved one the way they are.");
					}
					else if (PlayerPrefs.GetInt("Girlfriend State") == 2)
					{
						GUI.Label(new Rect(0,810, 1296, 30), "Romantic Acceptance: You expect him/her to change for you.");
					}
					else
					{
						GUI.Label(new Rect(0, 810, 1296, 30), "Romantic Acceptance: Not discovered yet. Play the game to find out.");

					}
					if (PlayerPrefs.GetInt("Second Path Chosen") == 2)
					{
						GUI.Label(new Rect(0, 840, 1296, 30), "Romantic Lasting: You tend to stay in love for a long time.");
					}
					else if (PlayerPrefs.GetInt("Second Path Chosen") == 1)
					{
						GUI.Label(new Rect(0, 840, 1296, 30), "Romantic Lasting: You fall out of love easily.");
					}
					else
					{
						GUI.Label(new Rect(0, 840, 1296, 30), "Romantic Lasting: Not discovered yet. Play the game to find out.");
					}
					#endregion
					// Write the PlayerPrefs that determine how many games that the player has played in total.
					#region Overall Game
					if (PlayerPrefs.GetInt("Games Played") == 0)
					{
						GUI.Label(new Rect(0, 870, 1296, 30), "Games Played: 0");
					}

					if (PlayerPrefs.GetInt("Games Played") == 1)
					{
						GUI.Label(new Rect(0, 870, 1296, 30), "Games Played: Cube Gamed played");
					}

					if (PlayerPrefs.GetInt("Games Played") == 2)
					{
						GUI.Label(new Rect(0, 870, 1296, 30), "Games Played: Her House played");
					}

					if (PlayerPrefs.GetInt("Games Played") == 3)
					{
						GUI.Label(new Rect(0, 870, 1296, 30), "Games Played: Cube Game and Her House played");
					}

					if (PlayerPrefs.GetInt("Games Played") == 5)
					{
						GUI.Label(new Rect(0, 870, 1296, 30), "Games Played: College Life played");
					}

					if (PlayerPrefs.GetInt("Games Played") == 6)
					{
						GUI.Label(new Rect(0, 870, 1296, 30), "Games Played: College Life and Cube Game Played");
					}

					if (PlayerPrefs.GetInt("Games Played") == 7)
					{
						GUI.Label(new Rect(0, 870, 1296, 30), "Games Played: College Life and Her House played");
					}
					if (PlayerPrefs.GetInt("Games Played") == 8)
					{
						GUI.Label(new Rect(0, 870, 1296, 30), "Games Played: All games played");
					}
					if (PlayerPrefs.GetInt("Mind") == 1) {
						GUI.Label(new Rect(0, 900, 1296, 30), "Mind: Extroverted - You have good social skills and feel recharged after spending time in the company of other people.");
					}
					else if (PlayerPrefs.GetInt("Mind") == 2) {
						GUI.Label(new Rect(0, 900, 1296, 30), "Mind: Introverted - You are self-sufficient, have no desire to make plenty of friends, prefer working with systems rather than people, and have relatively poor social skills.");
					}
					else {
						GUI.Label(new Rect(0, 900, 1296, 30), "Mind: Not discovered yet. Play the game to find out.");
					}
					if (PlayerPrefs.GetInt("Energy") == 1) {
						GUI.Label(new Rect(0, 930, 1296, 30), "Energy: Intuition - You prefer to rely on your imagination, ideas and possibilities.");
					}
					else if (PlayerPrefs.GetInt("Energy") == 2) {
						GUI.Label(new Rect(0, 930, 1296, 30), "Energy: Sensing - You focus on the actual world and things happening around them.");
					}
					else {
						GUI.Label(new Rect(0, 930, 1296, 30), "Energy: Not discovered yet. Play the game to find out.");
					}
					if (PlayerPrefs.GetInt("Nature") == 1) {
						GUI.Label(new Rect(0, 960, 1296, 30), "Nature: Thinking - You trust and prioritize logic, and rely on rational arguments.");
					}
					else if (PlayerPrefs.GetInt("Nature") == 2) {
						GUI.Label(new Rect(0, 960, 1296, 30), "Nature: Feeling - You trust and prioritize feelings, relying on moral and ethical arguments.");
					}
					else {
						GUI.Label(new Rect(0, 960, 1296, 30), "Nature: Not discovered yet. Play the game to find out.");
					}
					if (PlayerPrefs.GetInt("Tactics") == 1) {
						GUI.Label(new Rect(0, 990, 1296, 30), "Tactics: Judging - You are decisive, choose security over freedom to improvise, and usually find it difficult to cope with uncertainty.");
					}
					else if (PlayerPrefs.GetInt("Tactics") == 2) {
						GUI.Label(new Rect(0, 990, 1296, 30), "Tactics: Perceiving - You want to be able to look for alternative options, knowing that there is always a better way.");
					}
					else {
						GUI.Label(new Rect(0, 990, 1296, 30), "Tactics: Not discovered yet. Play the game to find out.");
					}
					if (string.Equals(PlayerPrefs.GetString("MBTI"), "")) {
						GUI.Label(new Rect(0, 1020, 1296, 30), "Your personality type: Not discovered yet. Play the game to find out.");
					}
					else if (string.Equals(PlayerPrefs.GetString("MBTI"), "INTP")) {
						GUI.Label(new Rect(0, 1020, 1296, 30), "Your personality type: INTP.");
						typeOverview = "The INTP personality type is fairly rare, making up only three percent of the population, which is definitely a good thing for them, as there's nothing they'd be more unhappy about than being \"common\". INTPs pride themselves on their inventiveness and creativity, their unique perspective and vigorous intellect. Usually known as the philosopher, the architect, or the dreamy professor, INTPs have been responsible for many scientific discoveries throughout history."
						+"\r\n\r\n" +
						"INTPs are known for their brilliant theories and unrelenting logic – in fact, they are considered the most logically precise of all the personality types."
						+"\r\n\r\n"
						+"They love patterns, and spotting discrepancies between statements could almost be described as a hobby, making it a bad idea to lie to an INTP. This makes it ironic that INTPs' word should always be taken with a grain of salt – it's not that they are dishonest, but people with the INTP personality type tend to share thoughts that are not fully developed, using others as a sounding board for ideas and theories in a debate against themselves rather than as actual conversation partners."
						+"\r\n\r\n"
						+"This may make them appear unreliable, but in reality no one is more enthusiastic and capable of spotting a problem, drilling through the endless factors and details that encompass the issue and developing a unique and viable solution than INTPs – just don't expect punctual progress reports. People who share the INTP personality type aren't interested in practical, day-to-day activities and maintenance, but when they find an environment where their creative genius and potential can be expressed, there is no limit to the time and energy INTPs will expend in developing an insightful and unbiased solution."
						+"\r\n\r\n"
						+"They may appear to drift about in an unending daydream, but INTPs' thought process is unceasing, and their minds buzz with ideas from the moment they wake up. This constant thinking can have the effect of making them look pensive and detached, as they are often conducting full-fledged debates in their own heads, but really INTPs are quite relaxed and friendly when they are with people they know, or who share their interests. However, this can be replaced by overwhelming shyness when INTP personalities are among unfamiliar faces, and friendly banter can quickly become combative if they believe their logical conclusions or theories are being criticized."
						+"\r\n\r\n"
						+"When INTPs are particularly excited, the conversation can border on incoherence as they try to explain the daisy-chain of logical conclusions that led to the formation of their latest idea. Oftentimes, INTPs will opt to simply move on from a topic before it's ever understood what they were trying to say, rather than try to lay things out in plain terms."
						+"\r\n\r\n"
						+"The reverse can also be true when people explain their thought processes to INTPs in terms of subjectivity and feeling. Imagine an immensely complicated clockwork, taking in every fact and idea possible, processing them with a heavy dose of creative reasoning and returning the most logically sound results available – this is how the INTP mind works, and this type has little tolerance for an emotional monkey-wrench jamming their machines."
						+"\r\n\r\n"
						+"Further, with Thinking (T) as one of their governing traits, INTPs are unlikely to understand emotional complaints at all, and their friends won't find a bedrock of emotional support in them. People with the INTP personality type would much rather make a series of logical suggestions for how to resolve the underlying issue, a perspective that is not always welcomed by their Feeling (F) companions. This will likely extend to most social conventions and goals as well, like planning dinners and getting married, as INTPs are far more concerned with originality and efficient results."
						+"\r\n\r\n"
						+"The one thing that really holds INTPs back is their restless and pervasive fear of failure. INTP personalities are so prone to reassessing their own thoughts and theories, worrying that they've missed some critical piece of the puzzle, that they can stagnate, lost in an intangible world where their thoughts are never truly applied. Overcoming this self-doubt stands as the greatest challenge INTPs are likely to face, but the intellectual gifts – big and small – bestowed on the world when they do makes it worth the fight.";
						GUI.Label(new Rect(40, 1050, 1296 - 100, 1110), typeOverview);
					}
					else if (string.Equals(PlayerPrefs.GetString("MBTI"), "ISTP")) {
						GUI.Label(new Rect(0, 1020, 1296, 30), "Your personality type: ISTP.");
						typeOverview = "ISTPs love to explore with their hands and their eyes, touching and examining the world around them with cool rationalism and spirited curiosity. People with this personality type are natural Makers, moving from project to project, building the useful and the superfluous for the fun of it, and learning from their environment as they go. Often mechanics and engineers, ISTPs find no greater joy than in getting their hands dirty pulling things apart and putting them back together, just a little bit better than they were before."
						+ "\r\n\r\n"
						+ "ISTPs explore ideas through creating, troubleshooting, trial and error and first-hand experience. They enjoy having other people take an interest in their projects and sometimes don't even mind them getting into their space. Of course, that's on the condition that those people don't interfere with ISTPs' principles and freedom, and they'll need to be open to ISTPs returning the interest in kind."
						+ "\r\n\r\n"
						+ "ISTPs enjoy lending a hand and sharing their experience, especially with the people they care about, and it's a shame they're so uncommon, making up only about five percent of the population. ISTP women are especially rare, and the typical gender roles that society tends to expect can be a poor fit – they'll often be seen as tomboys from a young age."
						+ "\r\n\r\n"
						+ "While their mechanical tendencies can make them appear simple at a glance, ISTPs are actually quite enigmatic. Friendly but very private, calm but suddenly spontaneous, extremely curious but unable to stay focused on formal studies, ISTP personalities can be a challenge to predict, even by their friends and loved ones. ISTPs can seem very loyal and steady for a while, but they tend to build up a store of impulsive energy that explodes without warning, taking their interests in bold new directions."
						+ "\r\n\r\n"
						+ "Rather than some sort of vision quest though, ISTPs are merely exploring the viability of a new interest when they make these seismic shifts."
						+ "\r\n\r\n"
						+ "ISTPs' decisions stem from a sense of practical realism, and at their heart is a strong sense of direct fairness, a \"do unto others\" attitude, which really helps to explain many of ISTPs' puzzling traits. Instead of being overly cautious though, avoiding stepping on toes in order to avoid having their toes stepped on, ISTPs are likely to go too far, accepting likewise retaliation, good or bad, as fair play."
						+ "\r\n\r\n"
						+ "The biggest issue ISTPs are likely to face is that they often act too soon, taking for granted their permissive nature and assuming that others are the same. They'll be the first to tell an insensitive joke, get overly involved in someone else's project, roughhouse and play around, or suddenly change their plans because something more interesting came up."
						+ "\r\n\r\n"
						+ "ISTPs will come to learn that many other personality types have much more firmly drawn lines on rules and acceptable behavior than they do – they don't want to hear an insensitive joke, and certainly wouldn't tell one back, and they wouldn't want to engage in horseplay, even with a willing party. If a situation is already emotionally charged, violating these boundaries can backfire tremendously."
						+ "\r\n\r\n"
						+ "ISTPs have a particular difficulty in predicting emotions, but this is just a natural extension of their fairness, given how difficult it is to gauge ISTPs' emotions and motivations. However, their tendency to explore their relationships through their actions rather than through empathy can lead to some very frustrating situations. People with the ISTP personality type struggle with boundaries and guidelines, preferring the freedom to move about and color outside the lines if they need to.";
						GUI.Label(new Rect(40, 1050, 1296 - 100, 1110), typeOverview);
					}
					else if (string.Equals(PlayerPrefs.GetString("MBTI"), "INFJ")) {
						GUI.Label(new Rect(0, 1020, 1296, 30), "Your personality type: INFJ.");
						typeOverview = "The INFJ personality type is very rare, making up less than one percent of the population, but they nonetheless leave their mark on the world. As Diplomats (NF), they have an inborn sense of idealism and morality, but what sets them apart is the accompanying Judging (J) trait – INFJs are not idle dreamers, but people capable of taking concrete steps to realize their goals and make a lasting positive impact."
						+ "\r\n\r\n"
						+ "INFJs tend to see helping others as their purpose in life, but while people with this personality type can be found engaging rescue efforts and doing charity work, their real passion is to get to the heart of the issue so that people need not be rescued at all."
						+ "\r\n\r\n"
						+ "INFJs indeed share a very unique combination of traits: though soft-spoken, they have very strong opinions and will fight tirelessly for an idea they believe in. They are decisive and strong-willed, but will rarely use that energy for personal gain – INFJs will act with creativity, imagination, conviction and sensitivity not to create advantage, but to create balance. Egalitarianism and karma are very attractive ideas to INFJs, and they tend to believe that nothing would help the world so much as using love and compassion to soften the hearts of tyrants."
						+ "\r\n\r\n"
						+ "INFJs find it easy to make connections with others, and have a talent for warm, sensitive language, speaking in human terms, rather than with pure logic and fact. It makes sense that their friends and colleagues will come to think of them as quiet Extroverted types, but they would all do well to remember that INFJs need time alone to decompress and recharge, and to not become too alarmed when they suddenly withdraw. INFJs take great care of other’s feelings, and they expect the favor to be returned – sometimes that means giving them the space they need for a few days."
						+ "\r\n\r\n"
							+ "Really though, it is most important for INFJs to remember to take care of themselves. The passion of their convictions is perfectly capable of carrying them past their breaking point and if their zeal gets out of hand, they can find themselves exhausted, unhealthy and stressed. This becomes especially apparent when INFJs find themselves up against conflict and criticism – their sensitivity forces them to do everything they can to evade these seemingly personal attacks, but when the circumstances are unavoidable, they can fight back in highly irrational, unhelpful ways."
						+ "\r\n\r\n"
							+ "To INFJs, the world is a place full of inequity – but it doesn’t have to be. No other personality type is better suited to create a movement to right a wrong, no matter how big or small. INFJs just need to remember that while they’re busy taking care of the world, they need to take care of themselves, too.";
							GUI.Label(new Rect(40, 1050, 1296 - 100, 1110), typeOverview);
					}
					else if (string.Equals(PlayerPrefs.GetString("MBTI"), "ISFJ")) {
						GUI.Label(new Rect(0, 1020, 1296, 30), "Your personality type: ISFJ.");
						typeOverview = "The ISFJ personality type is quite unique, as many of their qualities defy the definition of their individual traits. Though possessing the Feeling (F) trait, ISFJs have excellent analytical abilities though Introverted (I), they have well-developed people skills and robust social relationships and though they are a Judging (J) type, ISFJs are often receptive to change and new ideas. As with so many things, people with the ISFJ personality type are more than the sum of their parts, and it is the way they use these strengths that defines who they are."
						+ "\r\n\r\n"
						+ "ISFJs are true altruists, meeting kindness with kindness-in-excess and engaging the work and people they believe in with enthusiasm and generosity."
						+ "\r\n\r\n"
						+ "There's hardly a better type to make up such a large proportion of the population, nearly 13%. Combining the best of tradition and the desire to do good, ISFJs are found in lines of work with a sense of history behind them, such as medicine, academics and charitable social work."
						+ "\r\n\r\n"
						+ "ISFJ personalities are often meticulous to the point of perfectionism, and though they procrastinate, they can always be relied on to get the job done on time. ISFJs take their responsibilities personally, consistently going above and beyond, doing everything they can to exceed expectations and delight others, at work and at home."
						+ "\r\n\r\n"
						+ "The challenge for ISFJs is ensuring that what they do is noticed. They have a tendency to underplay their accomplishments, and while their kindness is often respected, more cynical and selfish people are likely to take advantage of ISFJs' dedication and humbleness by pushing work onto them and then taking the credit. ISFJs need to know when to say no and stand up for themselves if they are to maintain their confidence and enthusiasm."
						+ "\r\n\r\n"
						+ "Naturally social, an odd quality for Introverts, ISFJs utilize excellent memories not to retain data and trivia, but to remember people, and details about their lives. When it comes to gift-giving, ISFJs have no equal, using their imagination and natural sensitivity to express their generosity in ways that touch the hearts of their recipients. While this is certainly true of their coworkers, whom people with the ISFJ personality type often consider their personal friends, it is in family that their expressions of affection fully bloom."
						+ "\r\n\r\n"
						+ "ISFJ personalities are a wonderful group, rarely sitting idle while a worthy cause remains unfinished. ISFJs' ability to connect with others on an intimate level is unrivaled among Introverts, and the joy they experience in using those connections to maintain a supportive, happy family is a gift for everyone involved. They may never be truly comfortable in the spotlight, and may feel guilty taking due credit for team efforts, but if they can ensure that their efforts are recognized, ISFJs are likely to feel a level of satisfaction in what they do that many other personality types can only dream of.";
						GUI.Label(new Rect(40, 1050, 1296 - 100, 1110), typeOverview);
					}
					else if (string.Equals(PlayerPrefs.GetString("MBTI"), "INFP")) {
						GUI.Label(new Rect(0, 1020, 1296, 30), "Your personality type: INFP.");
						typeOverview = "INFP personalities are true idealists, always looking for the hint of good in even the worst of people and events, searching for ways to make things better. While they may be perceived as calm, reserved, or even shy, INFPs have an inner flame and passion that can truly shine. Comprising just 4% of the population, the risk of feeling misunderstood is unfortunately high for the INFP personality type – but when they find like-minded people to spend their time with, the harmony they feel will be a fountain of joy and inspiration."
						+"\r\n\r\n"
						+"Being a part of the Diplomat (NF) personality group, INFPs are guided by their principles, rather than by logic (Analysts), excitement (Explorers), or practicality (Sentinels). When deciding how to move forward, they will look to honor, beauty, morality and virtue – INFPs are led by the purity of their intent, not rewards and punishments. People who share the INFP personality type are proud of this quality, and rightly so, but not everyone understands the drive behind these feelings, and it can lead to isolation."
						+"\r\n\r\n"
						+"At their best, these qualities enable INFPs to communicate deeply with others, easily speaking in metaphors and parables, and understanding and creating symbols to share their ideas. The strength of this intuitive communication style lends itself well to creative works, and it comes as no surprise that many famous INFPs are poets, writers and actors. Understanding themselves and their place in the world is important to INFPs, and they explore these ideas by projecting themselves into their work."
						+"\r\n\r\n"
						+"INFPs have a talent for self-expression, revealing their beauty and their secrets through metaphors and fictional characters."
						+"\r\n\r\n"
						+"INFPs’ ability with language doesn’t stop with their native tongue, either – as with most people who share the Diplomat personality types, they are considered gifted when it comes to learning a second (or third!) language. Their gift for communication also lends itself well to INFPs’ desire for harmony, a recurring theme with Diplomats, and helps them to move forward as they find their calling."
						+"\r\n\r\n"
						+"Unlike their Extraverted cousins though, INFPs will focus their attention on just a few people, a single worthy cause – spread too thinly, they’ll run out of energy, and even become dejected and overwhelmed by all the bad in the world that they can’t fix. This is a sad sight for INFPs’ friends, who will come to depend on their rosy outlook."
						+"\r\n\r\n"
						+"If they are not careful, INFPs can lose themselves in their quest for good and neglect the day-to-day upkeep that life demands. INFPs often drift into deep thought, enjoying contemplating the hypothetical and the philosophical more than any other personality type. Left unchecked, INFPs may start to lose touch, withdrawing into \"hermit mode\", and it can take a great deal of energy from their friends or partner to bring them back to the real world."
						+"\r\n\r\n"
						+"Luckily, like the flowers in spring, INFP’s affection, creativity, altruism and idealism will always come back, rewarding them and those they love perhaps not with logic and utility, but with a world view that inspires compassion, kindness and beauty wherever they go.";
												GUI.Label(new Rect(40, 1050, 1296 - 100, 1110), typeOverview);
					}
					else if (string.Equals(PlayerPrefs.GetString("MBTI"), "ISFP")) {
						GUI.Label(new Rect(0, 1020, 1296, 30), "Your personality type: ISFP.");
						typeOverview = "ISFP personality types are true artists, but not necessarily in the typical sense where they're out painting happy little trees. Often enough though, they are perfectly capable of this. Rather, it's that they use aesthetics, design and even their choices and actions to push the limits of social convention. ISFPs enjoy upsetting traditional expectations with experiments in beauty and behavior – chances are, they've expressed more than once the phrase \"Don't box me in!\""
						+"\r\n\r\n"
						+"ISFPs live in a colorful, sensual world, inspired by connections with people and ideas. ISFP personalities take joy in reinterpreting these connections, reinventing and experimenting with both themselves and new perspectives. No other type explores and experiments in this way more. This creates a sense of spontaneity, making ISFPs seem unpredictable, even to their close friends and loved ones."
						+"\r\n\r\n"
						+"Despite all this, ISFPs are definitely Introverts (I), surprising their friends further when they step out of the spotlight to be by themselves to recharge. Just because they are alone though, doesn't mean people with the ISFP personality type sit idle – they take this time for introspection, assessing their principles. Rather than dwelling on the past or the future, ISFPs think about who they are. They return from their cloister, transformed."
						+"\r\n\r\n"
						+"ISFPs live to find ways to push their passions. Riskier behaviors like gambling and extreme sports are more common with this personality type than with others. Fortunately their attunement to the moment and their environment allows them to do better than most. ISFPs also enjoy connecting with others, and have a certain irresistible charm."
						+"\r\n\r\n"
						+"ISFPs always know just the compliment to soften a heart that's getting ready to call their risks irresponsible or reckless."
						+"\r\n\r\n"
						+"However, if a criticism does get through, it can end poorly. Some ISFPs can handle kindly phrased commentary, valuing it as another perspective to help push their passions in new directions. But if the comments are more biting and less mature, ISFP personalities can lose their tempers in spectacular fashion."
						+"\r\n\r\n"
						+"ISFPs are sensitive to others' feelings and value harmony. When faced with criticism, it can be a challenge for people with this type to step away from the moment long enough to not get caught up in the heat of the moment. But living in the moment goes both ways, and once the heightened emotions of an argument cool, ISFPs can usually call the past the past and move on as though it never occurred."
						+"\r\n\r\n"
						+"The biggest challenge facing ISFPs is planning for the future. Finding constructive ideals to base their goals on and working out goals that create positive principles is no small task. Unlike Sentinel types, ISFPs don't plan their futures in terms of assets and retirement. Rather, they plan actions and behaviors as contributions to a sense of identity, building a portfolio of experiences, not stocks."
						+"\r\n\r\n"
						+"If these goals and principles are noble, ISFPs can act with amazing charity and selflessness – but it can also happen that people with the ISFP personality type establish a more self-centered identity, acting with selfishness, manipulation and egoism. It's important for ISFPs to remember to actively become the person they want to be. Developing and maintaining a new habit may not come naturally, but taking the time each day to understand their motivations allows ISFPs to use their strengths to pursue whatever they've come to love.";
												GUI.Label(new Rect(40, 1050, 1296 - 120, 1110), typeOverview);
					}
					else if (string.Equals(PlayerPrefs.GetString("MBTI"), "INTJ")) {
						GUI.Label(new Rect(0, 1020, 1296, 30), "Your personality type: INTJ.");
						typeOverview = "It’s lonely at the top, and being one of the rarest and most strategically capable personality types, INTJs know this all too well. INTJs form just two percent of the population, and women of this personality type are especially rare, forming just 0.8% of the population – it is often a challenge for them to find like-minded individuals who are able to keep up with their relentless intellectualism and chess-like maneuvering. People with the INTJ personality type are imaginative yet decisive, ambitious yet private, amazingly curious, but they do not squander their energy."
						+"\r\n\r\n"
						+"With a natural thirst for knowledge that shows itself early in life, INTJs are often given the title of “bookworm” as children. While this may be intended as an insult by their peers, they more than likely identify with it and are even proud of it, greatly enjoying their broad and deep body of knowledge. INTJs enjoy sharing what they know as well, confident in their mastery of their chosen subjects, but owing to their Intuitive (N) and Judging (J) traits, they prefer to design and execute a brilliant plan within their field rather than share opinions on “uninteresting” distractions like gossip."
						+"\r\n\r\n"
						+"A paradox to most observers, INTJs are able to live by glaring contradictions that nonetheless make perfect sense – at least from a purely rational perspective. For example, INTJs are simultaneously the most starry-eyed idealists and the bitterest of cynics, a seemingly impossible conflict. But this is because INTJ types tend to believe that with effort, intelligence and consideration, nothing is impossible, while at the same time they believe that people are too lazy, short-sighted or self-serving to actually achieve those fantastic results. Yet that cynical view of reality is unlikely to stop an interested INTJ from achieving a result they believe to be relevant."
						+"\r\n\r\n"
						+"INTJs radiate self-confidence and an aura of mystery, and their insightful observations, original ideas and formidable logic enable them to push change through with sheer willpower and force of personality. At times it will seem that INTJs are bent on deconstructing and rebuilding every idea and system they come into contact with, employing a sense of perfectionism and even morality to this work. Anyone who doesn’t have the talent to keep up with INTJs’ processes, or worse yet, doesn’t see the point of them, is likely to immediately and permanently lose their respect."
						+"\r\n\r\n"
						+"Rules, limitations and traditions are anathema to the INTJ personality type – everything should be open to questioning and reevaluation, and if they see a way, INTJs will often act unilaterally to enact their technically superior, sometimes insensitive, and almost always unorthodox methods and ideas."
						+"\r\n\r\n"
						+"This isn’t to be misunderstood as impulsiveness – INTJs will strive to remain rational no matter how attractive the end goal may be, and every idea, whether generated internally or soaked in from the outside world, must pass the ruthless and ever-present “Is this going to work?” filter. This mechanism is applied at all times, to all things and all people, and this is often where INTJ personality types run into trouble."
						+"\r\n\r\n"
						+"INTJs are brilliant and confident in bodies of knowledge they have taken the time to understand, but unfortunately the social contract is unlikely to be one of those subjects. White lies and small talk are hard enough as it is for a type that craves truth and depth, but INTJs may go so far as to see many social conventions as downright stupid. Ironically, it is often best for them to remain where they are comfortable – out of the spotlight – where the natural confidence prevalent in INTJs as they work with the familiar can serve as its own beacon, attracting people, romantically or otherwise, of similar temperament and interests."
						+"\r\n\r\n"
						+"INTJs are defined by their tendency to move through life as though it were a giant chess board, pieces constantly shifting with consideration and intelligence, always assessing new tactics, strategies and contingency plans, constantly outmaneuvering their peers in order to maintain control of a situation while maximizing their freedom to move about. This isn’t meant to suggest that INTJs act without conscience, but to many Feeling (F) types, INTJs’ distaste for acting on emotion can make it seem that way, and it explains why many fictional villains (and misunderstood heroes) are modeled on this personality type.";
												GUI.Label(new Rect(40, 1050, 1296 - 120, 1110), typeOverview);
					}
					else if (string.Equals(PlayerPrefs.GetString("MBTI"), "ISTJ")) {
						GUI.Label(new Rect(0, 1020, 1296, 30), "Your personality type: ISTJ.");
						typeOverview = "The ISTJ personality type is thought to be the most abundant, making up around 13% of the population. Their defining characteristics of integrity, practical logic and tireless dedication to duty make ISTJs a vital core to many families, as well as organizations that uphold traditions, rules and standards, such as law offices, regulatory bodies and military. People with the ISTJ personality type enjoy taking responsibility for their actions, and take pride in the work they do – when working towards a goal, ISTJs hold back none of their time and energy completing each relevant task with accuracy and patience."
						+"\r\n\r\n"
						+"ISTJs don't make many assumptions, preferring instead to analyze their surroundings, check their facts and arrive at practical courses of action. ISTJ personalities are no-nonsense, and when they've made a decision, they will relay the facts necessary to achieve their goal, expecting others to grasp the situation immediately and take action. ISTJs have little tolerance for indecisiveness, but lose patience even more quickly if their chosen course is challenged with impractical theories, especially if they ignore key details – if challenges becomes time-consuming debates, ISTJs can become noticeably angry as deadlines tick nearer."
						+"\r\n\r\n"
						+"When ISTJs say they are going to get something done, they do it, meeting their obligations no matter the personal cost, and they are baffled by people who don't hold their own word in the same respect. Combining laziness and dishonesty is the quickest way to get on ISTJs' bad side. Consequently, people with the ISTJ personality type often prefer to work alone, or at least have their authority clearly established by hierarchy, where they can set and achieve their goals without debate or worry over other's reliability."
						+"\r\n\r\n"
						+"ISTJs have sharp, fact-based minds, and prefer autonomy and self-sufficiency to reliance on someone or something. Dependency on others is often seen by ISTJs as a weakness, and their passion for duty, dependability and impeccable personal integrity forbid falling into such a trap."
						+"\r\n\r\n"
						+"This sense of personal integrity is core to ISTJs, and goes beyond their own minds – ISTJ personalities adhere to established rules and guidelines regardless of cost, reporting their own mistakes and telling the truth even when the consequences for doing so could be disastrous. To ISTJs, honesty is far more important than emotional considerations, and their blunt approach leaves others with the false impression that ISTJs are cold, or even robotic. People with this type may struggle to express emotion or affection outwardly, but the suggestion that they don't feel, or worse have no personality at all, is deeply hurtful."
						+"\r\n\r\n"
						+"ISTJs' dedication is an excellent quality, allowing them to accomplish much, but it is also a core weakness that less scrupulous individuals take advantage of. ISTJs seek stability and security, considering it their duty to maintain a smooth operation, and they may find that their coworkers and significant others shift their responsibilities onto them, knowing that they will always take up the slack. ISTJs tend to keep their opinions to themselves and let the facts do the talking, but it can be a long time before observable evidence tells the whole story."
						+"\r\n\r\n"
						+"ISTJs need to remember to take care of themselves – their stubborn dedication to stability and efficiency can compromise those goals in the long term as others lean ever-harder on them, creating an emotional strain that can go unexpressed for years, only finally coming out after it's too late to fix. If they can find coworkers and spouses who genuinely appreciate and complement their qualities, who enjoy the brightness, clarity and dependability that they offer, ISTJs will find that their stabilizing role is a tremendously satisfying one, knowing that they are part of a system that works.";
												GUI.Label(new Rect(40, 1050, 1296 - 120, 1110), typeOverview);
					}
					else if (string.Equals(PlayerPrefs.GetString("MBTI"), "ENFJ")) {
						GUI.Label(new Rect(0, 1020, 1296, 30), "Your personality type: ENFJ.");
						typeOverview = "ENFJs are natural-born leaders, full of passion and charisma. Forming around two percent of the population, they are oftentimes our politicians, our coaches and our teachers, reaching out and inspiring others to achieve and to do good in the world. With a natural confidence that begets influence, ENFJs take a great deal of pride and joy in guiding others to work together to improve themselves and their community."
						+"\r\n\r\n"
						+"People are drawn to strong personalities, and ENFJs radiate authenticity, concern and altruism, unafraid to stand up and speak when they feel something needs to be said. They find it natural and easy to communicate with others, especially in person, and their Intuitive (N) trait helps people with the ENFJ personality type to reach every mind, be it through facts and logic or raw emotion. ENFJs easily see people's motivations and seemingly disconnected events, and are able to bring these ideas together and communicate them as a common goal with an eloquence that is nothing short of mesmerizing."
						+"\r\n\r\n"
						+"The interest ENFJs have in others is genuine, almost to a fault – when they believe in someone, they can become too involved in the other person's problems, place too much trust in them. Luckily, this trust tends to be a self-fulfilling prophesy, as ENFJs' altruism and authenticity inspire those they care about to become better themselves. But if they aren't careful, they can overextend their optimism, sometimes pushing others further than they're ready or willing to go."
						+"\r\n\r\n"
						+"ENFJs are vulnerable to another snare as well: they have a tremendous capacity for reflecting on and analyzing their own feelings, but if they get too caught up in another person's plight, they can develop a sort of emotional hypochondria, seeing other people's problems in themselves, trying to fix something in themselves that isn't wrong. If they get to a point where they are held back by limitations someone else is experiencing, it can hinder ENFJs' ability to see past the dilemma and be of any help at all. When this happens, it's important for ENFJs to pull back and use that self-reflection to distinguish between what they really feel, and what is a separate issue that needs to be looked at from another perspective."
						+"\r\n\r\n"
						+"ENFJs are genuine, caring people who talk the talk and walk the walk, and nothing makes them happier than leading the charge, uniting and motivating their team with infectious enthusiasm."
						+"\r\n\r\n"
						+"People with the ENFJ personality type are passionate altruists, sometimes even to a fault, and they are unlikely to be afraid to take the slings and arrows while standing up for the people and ideas they believe in. It is no wonder that many famous ENFJs are US Presidents – this personality type wants to lead the way to a brighter future, whether it's by leading a nation to prosperity, or leading their little league softball team to a hard-fought victory.";
												GUI.Label(new Rect(40, 1050, 1296 - 120, 1110), typeOverview);
					}
					else if (string.Equals(PlayerPrefs.GetString("MBTI"), "ESFJ")) {
						GUI.Label(new Rect(0, 1020, 1296, 30), "Your personality type: ESFJ.");
						typeOverview = "People who share the ESFJ personality type are, for lack of a better word, popular – which makes sense, given that it is also a very common personality type, making up twelve percent of the population. In high school, ESFJs are the cheerleaders and the quarterbacks, setting the tone, taking the spotlight and leading their teams forward to victory and fame. Later in life, ESFJs continue to enjoy supporting their friends and loved ones, organizing social gatherings and doing their best to make sure everyone is happy."
						+"\r\n\r\n"
						+"At their hearts, ESFJ personalities are social creatures, and thrive on staying up to date with what their friends are doing."
						+"\r\n\r\n"
						+"Discussing scientific theories or debating European politics isn't likely to capture ESFJs' interest for too long. ESFJs are more concerned with fashion and their appearance, their social status and the standings of other people. Practical matters and gossip are their bread and butter, but ESFJs do their best to use their powers for good."
						+"\r\n\r\n"
						+"ESFJs are altruists, and they take seriously their responsibility to help and to do the right thing. Unlike their Diplomat (NF) relatives however, people with the ESFJ personality type will base their moral compass on established traditions and laws, upholding authority and rules, rather than drawing their morality from philosophy or mysticism. It's important for ESFJs to remember though, that people come from many backgrounds and perspectives, and what may seem right to them isn't always an absolute truth."
						+"\r\n\r\n"
						+"ESFJs love to be of service, enjoying any role that allows them to participate in a meaningful way, so long as they know that they are valued and appreciated. This is especially apparent at home, and ESFJs make loyal and devoted partners and parents. ESFJ personalities respect hierarchy, and do their best to position themselves with some authority, at home and at work, which allows them to keep things clear, stable and organized for everyone."
						+"\r\n\r\n"
						+"Supportive and outgoing, ESFJs can always be spotted at a party – they're the ones finding time to chat and laugh with everyone! But their devotion goes further than just breezing through because they have to. ESFJs truly enjoy hearing about their friends' relationships and activities, remembering little details and always standing ready to talk things out with warmth and sensitivity. If things aren't going right, or there's tension in the room, ESFJs pick up on it and to try to restore harmony and stability to the group."
						+"\r\n\r\n"
						+"Being pretty conflict-averse, ESFJs spend a lot of their energy establishing social order, and prefer plans and organized events to open-ended activities or spontaneous get-togethers. People with this personality type put a lot of effort into the activities they've arranged, and it's easy for ESFJs' feelings to be hurt if their ideas are rejected, or if people just aren't interested. Again, it's important for ESFJs to remember that everyone is coming from a different place, and that disinterest isn't a comment about them or the activity they've organized – it's just not their thing."
						+"\r\n\r\n"
						+"Coming to terms with their sensitivity is ESFJs' biggest challenge – people are going to disagree and they're going to criticize, and while it hurts, it's just a part of life. The best thing for ESFJs to do is to do what they do best: be a role model, take care of what they have the power to take care of, and enjoy that so many people do appreciate the efforts they make.";
												GUI.Label(new Rect(40, 1050, 1296 - 120, 1110), typeOverview);
					}
					else if (string.Equals(PlayerPrefs.GetString("MBTI"), "ENTP")) {
						GUI.Label(new Rect(0, 1020, 1296, 30), "Your personality type: ENTP.");
						typeOverview = "The ENTP personality type is the ultimate devil's advocate, thriving on the process of shredding arguments and beliefs and letting the ribbons drift in the wind for all to see. Unlike their more determined Judging (J) counterparts, ENTPs don't do this because they are trying to achieve some deeper purpose or strategic goal, but for the simple reason that it's fun. No one loves the process of mental sparring more than ENTPs, as it gives them a chance to exercise their effortlessly quick wit, broad accumulated knowledge base, and capacity for connecting disparate ideas to prove their points."
						+ "\r\n\r\n"
						+"An odd juxtaposition arises with ENTPs, as they are uncompromisingly honest, but will argue tirelessly for something they don't actually believe in, stepping into another's shoes to argue a truth from another perspective."
						+"\r\n\r\n"
						+"Playing the devil's advocate helps people with the ENTP personality type to not only develop a better sense of others' reasoning, but a better understanding of opposing ideas – since ENTPs are the ones arguing them."
						+"\r\n\r\n"
						+"This tactic shouldn't be confused with the sort of mutual understanding Diplomats (NF) seek – ENTPs, like all Analyst (NT) personality types, are on a constant quest for knowledge, and what better way to gain it than to attack and defend an idea, from every angle, from every side?"
						+"\r\n\r\n"
						+"Taking a certain pleasure in being the underdog, ENTPs enjoy the mental exercise found in questioning the prevailing mode of thought, making them irreplaceable in reworking existing systems or shaking things up and pushing them in clever new directions. However, they'll be miserable managing the day-to-day mechanics of actually implementing their suggestions. ENTP personalities love to brainstorm and think big, but they will avoid getting caught doing the \"grunt work\" at all costs. ENTPs only make up about three percent of the population, which is just right, as it lets them create original ideas, then step back to let more numerous and fastidious personalities handle the logistics of implementation and maintenance."
						+"\r\n\r\n"
						+"ENTPs' capacity for debate can be a vexing one – while often appreciated when it's called for, it can fall painfully flat when they step on others' toes by say, openly questioning their boss in a meeting, or picking apart everything their significant other says. This is further complicated by ENTPs' unyielding honesty, as this type doesn't mince words and cares little about being seen as sensitive or compassionate. Likeminded types get along well enough with people with the ENTP personality type, but more sensitive types, and society in general, are often conflict-averse, preferring feelings, comfort, and even white lies over unpleasant truths and hard rationality."
						+"\r\n\r\n"
						+"This frustrates ENTPs, and they find that their quarrelsome fun burns many bridges, oftentimes inadvertently, as they plow through others' thresholds for having their beliefs questioned and their feelings brushed aside. Treating others as they'd be treated, ENTPs have little tolerance for being coddled, and dislike when people beat around the bush, especially when asking a favor. ENTP personalities find themselves respected for their vision, confidence, knowledge, and keen sense of humor, but often struggle to utilize these qualities as the basis for deeper friendships and romantic relationships."
						+"\r\n\r\n"
						+"ENTPs have a longer road than most in harnessing their natural abilities – their intellectual independence and free-form vision are tremendously valuable when they're in charge, or at least have the ear of someone who is, but getting there can take a level of follow-through that ENTPs struggle with."
						+"\r\n\r\n"
						+"Once they've secured such a position, ENTPs need to remember that for their ideas to come to fruition, they will always depend on others to assemble the pieces – if they've spent more time \"winning\" arguments than they have building consensus, many ENTPs will find they simply don't have the support necessary to be successful. Playing devil's advocate so well, people with this personality type may find that the most complex and rewarding intellectual challenge is to understand a more sentimental perspective, and to argue consideration and compromise alongside logic and progress.";
												GUI.Label(new Rect(40, 1050, 1296 - 120, 1110), typeOverview);
					}
					else if (string.Equals(PlayerPrefs.GetString("MBTI"), "ESTP")) {
						GUI.Label(new Rect(0, 1020, 1296, 30), "Your personality type: ESTP.");
						typeOverview = "ESTP personality types always have an impact on their immediate surroundings – the best way to spot them at a party is to look for the whirling eddy of people flitting about them as they move from group to group. Laughing and entertaining with a blunt and earthy humor, ESTP personalities love to be the center of attention. If an audience member is asked to come on stage, ESTPs volunteer – or volunteer a shy friend."
						+"\r\n\r\n"
						+"Theory, abstract concepts and plodding discussions about global issues and their implications don't keep ESTPs interested for long. ESTPs keep their conversation energetic, with a good dose of intelligence, but they like to talk about what is – or better yet, to just go out and do it. ESTPs leap before they look, fixing their mistakes as they go, rather than sitting idle, preparing contingencies and escape clauses."
						+"\r\n\r\n"
						+"ESTPs are the likeliest personality type to make a lifestyle of risky behavior. They live in the moment and dive into the action – they are the eye of the storm. People with the ESTP personality type enjoy drama, passion, and pleasure, not for emotional thrills, but because it's so stimulating to their logical minds. They are forced to make critical decisions based on factual, immediate reality in a process of rapid-fire rational stimulus response."
						+"\r\n\r\n"
						+"This makes school and other highly organized environments a challenge for ESTPs. It certainly isn't because they aren't smart, and they can do well, but the regimented, lecturing approach of formal education is just so far from the hands-on learning that ESTPs enjoy. It takes a great deal of maturity to see this process as a necessary means to an end, something that creates more exciting opportunities."
						+"\r\n\r\n"
						+"Also challenging is that to ESTPs, it makes more sense to use their own moral compass than someone else's. Rules were made to be broken. This is a sentiment few high school instructors or corporate supervisors are likely to share, and can earn ESTP personalities a certain reputation. But if they minimize the trouble-making, harness their energy, and focus through the boring stuff, ESTPs are a force to be reckoned with."
						+"\r\n\r\n"
						+"With perhaps the most perceptive, unfiltered view of any type, ESTPs have a unique skill in noticing small changes. Whether a shift in facial expression, a new clothing style, or a broken habit, people with this personality type pick up on hidden thoughts and motives where most types would be lucky to pick up anything specific at all. ESTPs use these observations immediately, calling out the change and asking questions, often with little regard for sensitivity. ESTPs should remember that not everyone wants their secrets and decisions broadcast."
						+"\r\n\r\n"
						+"Sometimes ESTPs' instantaneous observation and action is just what's required, as in some corporate environments, and especially in emergencies."
						+"\r\n\r\n"
						+"If ESTPs aren't careful though, they may get too caught in the moment, take things too far, and run roughshod over more sensitive people, or forget to take care of their own health and safety. Making up only four percent of the population, there are just enough ESTPs out there to keep things spicy and competitive, and not so many as to cause a systemic risk."
						+"\r\n\r\n"
						+"ESTPs are full of passion and energy, complemented by a rational, if sometimes distracted, mind. Inspiring, convincing and colorful, they are natural group leaders, pulling everyone along the path less traveled, bringing life and excitement everywhere they go. Putting these qualities to a constructive and rewarding end is ESTPs' true challenge.";
												GUI.Label(new Rect(40, 1050, 1296 - 120, 1110), typeOverview);
					}
					else if (string.Equals(PlayerPrefs.GetString("MBTI"), "ENTJ")) {
						GUI.Label(new Rect(0, 1020, 1296, 30), "Your personality type: ENTJ.");
						typeOverview = "ENTJs are natural-born leaders. People with this personality type embody the gifts of charisma and confidence, and project authority in a way that draws crowds together behind a common goal. But unlike their Feeling (F) counterpart, ENTJs are characterized by an often ruthless level of rationality, using their drive, determination and sharp minds to achieve whatever end they've set for themselves. Perhaps it is best that they make up only three percent of the population, lest they overwhelm the more timid and sensitive personality types that make up much of the rest of the world – but we have ENTJs to thank for many of the businesses and institutions we take for granted every day."
						+"\r\n\r\n"
						+"If there's anything ENTJs love, it's a good challenge, big or small, and they firmly believe that given enough time and resources, they can achieve any goal. This quality makes people with the ENTJ personality type brilliant entrepreneurs, and their ability to think strategically and hold a long-term focus while executing each step of their plans with determination and precision makes them powerful business leaders. This determination is often a self-fulfilling prophecy, as ENTJs push their goals through with sheer willpower where others might give up and move on, and their Extroverted (E) nature means they are likely to push everyone else right along with them, achieving spectacular results in the process."
						+"\r\n\r\n"
						+"At the negotiating table, be it in a corporate environment or buying a car, ENTJs are dominant, relentless, and unforgiving. This isn't because they are coldhearted or vicious per se – it's more that ENTJ personalities genuinely enjoy the challenge, the battle of wits, the repartee that comes from this environment, and if the other side can't keep up, that's no reason for ENTJs to fold on their own core tenet of ultimate victory."
						+"\r\n\r\n"
						+"The underlying thought running through the ENTJ mind might be something like \"I don't care if you call me an insensitive meany, as long as I remain an efficient meany\"."
						+"\r\n\r\n"
						+"If there's anyone ENTJs respect, it's someone who is able to stand up to them intellectually, who is able to act with a precision and quality equal to their own. ENTJ personalities have a particular skill in recognizing the talents of others, and this helps in both their team-building efforts (since no one, no matter how brilliant, can do everything alone), and to keep ENTJs from displaying too much arrogance and condescension. However, they also have a particular skill in calling out others' failures with a chilling degree of insensitivity, and this is where ENTJs really start to run into trouble."
						+"\r\n\r\n"
						+"Emotional expression isn't the strong suit of any Analyst (NT) type, but because of their Extroverted (E) nature, ENTJs' distance from their emotions is especially public, and felt directly by a much broader swath of people. Especially in a professional environment, ENTJs will simply crush the sensitivities of those they view as inefficient, incompetent or lazy. To people with the ENTJ personality type, emotional displays are displays of weakness, and it's easy to make enemies with this approach – ENTJs will do well to remember that they absolutely depend on having a functioning team, not just to achieve their goals, but for their validation and feedback as well, something ENTJs are, curiously, very sensitive to."
						+"\r\n\r\n"
						+"ENTJs are true powerhouses, and they cultivate an image of being larger than life – and often enough they are. They need to remember though, that their stature comes not just from their own actions, but from the actions of the team that props them up, and that it's important to recognize the contributions, talents and needs, especially from an emotional perspective, of their support network. Even if they have to adopt a \"fake it ‘til you make it\" mentality, if ENTJs are able to combine an emotionally healthy focus alongside their many strengths, they will be rewarded with deep, satisfying relationships and all the challenging victories they can handle.";
												GUI.Label(new Rect(40, 1050, 1296 - 120, 1110), typeOverview);
					}
					else if (string.Equals(PlayerPrefs.GetString("MBTI"), "ESTJ")) {
						GUI.Label(new Rect(0, 1020, 1296, 30), "Your personality type: ESTJ.");
						typeOverview = "ESTJs are representatives of tradition and order, utilizing their understanding of what is right, wrong and socially acceptable to bring families and communities together. Embracing the values of honesty, dedication and dignity, people with the ESTJ personality type are valued for their clear advice and guidance, and they happily lead the way on difficult paths. Taking pride in bringing people together, ESTJs often take on roles as community organizers, working hard to bring everyone together in celebration of cherished local events, or in defense of the traditional values that hold families and communities together."
						+"\r\n\r\n"
						+"Demand for such leadership is high in democratic societies, and forming no less than 11% of the population, it's no wonder that many of America's presidents have been ESTJs. Strong believers in the rule of law and authority that must be earned, ESTJ personalities lead by example, demonstrating dedication and purposeful honesty, and an utter rejection of laziness and cheating, especially in work. If anyone declares hard, manual work to be an excellent way to build character, it is ESTJs."
						+"\r\n\r\n"
						+"ESTJs are aware of their surroundings and live in a world of clear, verifiable facts – the surety of their knowledge means that even against heavy resistance, they stick to their principles and push an unclouded vision of what is and is not acceptable. Their opinions aren't just empty talk either, as ESTJs are more than willing to dive into the most challenging projects, improving action plans and sorting details along the way, making even the most complicated tasks seem easy and approachable."
						+"\r\n\r\n"
						+"However, ESTJs don't work alone, and they expect their reliability and work ethic to be reciprocated – people with this personality type meet their promises, and if partners or subordinates jeopardize them through incompetence or laziness, or worse still, dishonesty, they do not hesitate to show their wrath. This can earn them a reputation for inflexibility, a trait shared by all Sentinels (SJ), but it's not because ESTJs are arbitrarily stubborn, but because they truly believe that these values are what make society work."
						+"\r\n\r\n"
						+"ESTJs are classic images of the model citizen: they help their neighbors, uphold the law, and try to make sure that everyone participates in the communities and organizations they hold so dear."
						+"\r\n\r\n"
						+"The main challenge for ESTJs is to recognize that not everyone follows the same path or contributes in the same way. A true leader recognizes the strength of the individual, as well as that of the group, and helps bring those individuals' ideas to the table. That way, ESTJs really do have all the facts, and are able to lead the charge in directions that work for everyone.";
												GUI.Label(new Rect(40, 1050, 1296 - 120, 1110), typeOverview);
					}
					else if (string.Equals(PlayerPrefs.GetString("MBTI"), "ENFP")) {
						GUI.Label(new Rect(0, 1020, 1296, 30), "Your personality type: ENFP.");
						typeOverview = "The ENFP personality is a true free spirit. They are often the life of the party, but unlike Explorers, they are less interested in the sheer excitement and pleasure of the moment than they are in enjoying the social and emotional connections they make with others. Charming, independent, energetic and compassionate, the 7% of the population that they comprise can certainly be felt in any crowd."
						+"\r\n\r\n"
						+"More than just sociable people-pleasers though, ENFPs, like all their Diplomat cousins, are shaped by their Intuitive (N) quality, allowing them to read between the lines with curiosity and energy. They tend to see life as a big, complex puzzle where everything is connected – but unlike Analysts, who tend to see that puzzle as a series of systemic machinations, ENFPs see it through a prism of emotion, compassion and mysticism, and are always looking for a deeper meaning."
						+"\r\n\r\n"
						+"ENFPs are fiercely independent, and much more than stability and security, they crave creativity and freedom."
						+"\r\n\r\n"
						+"Many other types are likely to find these qualities irresistible, and if they've found a cause that sparks their imagination, ENFPs will bring an energy that oftentimes thrusts them into the spotlight, held up by their peers as a leader and a guru – but this isn't always where independence-loving ENFPs want to be. Worse still if they find themselves beset by the administrative tasks and routine maintenance that can accompany a leadership position. ENFPs' self-esteem is dependent on their ability to come up with original solutions, and they need to know that they have the freedom to be innovative – they can quickly lose patience or become dejected if they get trapped in a boring role."
						+"\r\n\r\n"
						+"Luckily, ENFPs know how to relax, and they are perfectly capable of switching from a passionate, driven idealist in the workplace to that imaginative and enthusiastic free spirit on the dance floor, often with a suddenness that can surprise even their closest friends. Being in the mix also gives them a chance to connect emotionally with others, giving them cherished insight into what motivates their friends and colleagues. They believe that everyone should take the time to recognize and express their feelings, and their empathy and sociability make that a natural conversation topic."
						+"\r\n\r\n"
						+"The ENFP personality type needs to be careful, however – if they rely too much on their intuition, assume or anticipate too much about a friend's motivations, they can misread the signals and frustrate plans that a more straightforward approach would have made simple. This kind of social stress is the bugbear that keeps harmony-focused Diplomats awake at night. ENFPs are very emotional and sensitive, and when they step on someone's toes, they both feel it."
						+"\r\n\r\n"
						+"ENFPs will spend a lot of time exploring social relationships, feelings and ideas before they find something that really rings true. But when they finally do find their place in the world, their imagination, empathy and courage are likely to produce incredible results.";
												GUI.Label(new Rect(40, 1050, 1296 - 120, 1110), typeOverview);
					}
					else if (string.Equals(PlayerPrefs.GetString("MBTI"), "ESFP")) {
						GUI.Label(new Rect(0, 1020, 1296, 30), "Your personality type: ESFP.");
						typeOverview = "If anyone is to be found spontaneously breaking into song and dance, it is the ESFP personality type. ESFPs get caught up in the excitement of the moment, and want everyone else to feel that way, too. No other personality type is as generous with their time and energy as ESFPs when it comes to encouraging others, and no other personality type does it with such irresistible style."
						+"\r\n\r\n"
						+"Born entertainers, ESFPs love the spotlight, but all the world's a stage. Many famous people with the ESFP personality type are indeed actors, but they love putting on a show for their friends too, chatting with a unique and earthy wit, soaking up attention and making every outing feel a bit like a party. Utterly social, ESFPs enjoy the simplest things, and there's no greater joy for them than just having fun with a good group of friends."
						+"\r\n\r\n"
						+"It's not just talk either – ESFPs have the strongest aesthetic sense of any personality type. From grooming and outfits to a well-appointed home, ESFP personalities have an eye for fashion. Knowing what's attractive the moment they see it, ESFPs aren't afraid to change their surroundings to reflect their personal style. ESFPs are naturally curious, exploring new designs and styles with ease."
						+"\r\n\r\n"
						+"Though it may not always seem like it, ESFPs know that it's not all about them – they are observant, and very sensitive to others' emotions. People with this personality type are often the first to help someone talk out a challenging problem, happily providing emotional support and practical advice. However, if the problem is about them, ESFPs are more likely to avoid a conflict altogether than to address it head-on. ESFPs usually love a little drama and passion, but not so much when they are the focus of the criticisms it can bring."
						+"\r\n\r\n"
						+"The biggest challenge ESFPs face is that they are often so focused on immediate pleasures that they neglect the duties and responsibilities that make those luxuries possible. Complex analysis, repetitive tasks, and matching statistics to real consequences are not easy activities for ESFPs. They'd rather rely on luck or opportunity, or simply ask for help from their extensive circle of friends. It is important for ESFPs to challenge themselves to keep track of long-term things like their retirement plans or sugar intake – there won't always be someone else around who can help to keep an eye on these things."
						+"\r\n\r\n"
						+"ESFPs recognize value and quality, which on its own is a fine trait. In combination with their tendency to be poor planners though, this can cause them to live beyond their means, and credit cards are especially dangerous. More focused on leaping at opportunities than in planning out long-term goals, ESFPs may find that their inattentiveness has made some activities unaffordable."
						+"\r\n\r\n"
						+"There's nothing that makes ESFPs feel quite as unhappy as realizing that they are boxed in by circumstance, unable to join their friends."
						+"\r\n\r\n"
						+"ESFPs are welcome wherever there's a need for laughter, playfulness, and a volunteer to try something new and fun – and there's no greater joy for ESFP personalities than to bring everyone else along for the ride. ESFPs can chat for hours, sometimes about anything but the topic they meant to talk about, and share their loved ones' emotions through good times and bad. If they can just remember to keep their ducks in a row, they'll always be ready to dive into all the new and exciting things the world has to offer, friends in tow.";
												GUI.Label(new Rect(40, 1050, 1296 - 120, 1110), typeOverview);
					}


					GUI.Label(new Rect(0, 2150, 1296, 30), "Press Enter to return to the Main Menu.");
					#endregion


					// End the scroll bar section.
					GUI.EndScrollView();
				}
			}
		}
	}
}