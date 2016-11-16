using UnityEngine;
using System.Collections;

public class TaskScript : MonoBehaviour
{
	/*
	 * This script handles all the tasks in game and moves the story along.
	 * */
    public bool canMove; //Whether or not player can move
    public bool canClick; //Whether or not player can click
    public GUISkin textSkin = null;
    GameObject FT, BS, BS2, main;

    public int clicks = 0; //Clicks counter to determine place in story
    Renderer rend;
    public float fadeSpeed = 1.5f; //Fading speed of blackscreen
    Color color;

	//Each of the task scripts and their respective game objects
    GUICubeScript GCS;
	SecondTask ST;
    GameObject second;
    ThirdTask TT;
	GameObject third;
    ForthTask F4T;
    GameObject forth;
    FifthTask F5T;
    GameObject fifth;
    Vector2 scrollViewVector = Vector2.zero;
    public bool editingCube = false, editingLadder = false, editingHorse = false, editingFlowers = false, editingStorm = false; //What we are editing at the time
    // Use this for initialization
    void Start()
    {
        canMove = false;
        canClick = true;
        BS = GameObject.Find("GameObject/MirrorCamera/BlockScreen");
        BS2 = GameObject.Find("GameObject/MirrorCamera/BlockScreen2");
        BS2.SetActive(false);
        FT = GameObject.Find("TheTasks/FirstTask");
        GCS = FT.GetComponentInChildren<GUICubeScript>();

        FT.SetActive(false);
        rend = BS.GetComponent<Renderer>();
        rend.enabled = true;
        rend.material.shader = Shader.Find("Transparent/Diffuse");
        color = rend.material.color;

        
        main = GameObject.Find("GameObject");
        second = GameObject.Find("TheTasks/SecondTask");
		ST = second.GetComponent<SecondTask> ();
        second.SetActive(false);
		third = GameObject.Find ("TheTasks/ThirdTask");
        TT = third.GetComponent<ThirdTask>();
		third.SetActive (false);
        forth = GameObject.Find("TheTasks/ForthTask");
        F4T = forth.GetComponent<ForthTask>();
        forth.SetActive(false);

        fifth = GameObject.Find("TheTasks/FifthTask");
        F5T = fifth.GetComponent<FifthTask>();
        fifth.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
		//If we can click, left click will move counter up
        if (Input.GetMouseButtonDown(0) && canClick)
        {
            clicks++;
        }
		//Once click counter enters here, we fade black screen.
        if (clicks == 5)
        {
            //BS.SetActive(false);
            //SF.FadeToClear();
            canClick = false;
            color.a -= .0025f;
            //rend.material.color = Color.Lerp(Color.black,Color.clear, fadeSpeed * (Time.fixedDeltaTime * 50));

            if (color.a < .3)
            {
                color.a = 0;
                canClick = true;
                clicks++;
                BS.SetActive(false);
            }
            rend.material.color = color;

        }
		//These keep track of when things can be clicked, when we can move, and when new tasks are set active
        if (clicks == 7)
        {
            canMove = true;
        }
        if (clicks == 10)
        {
            canMove = false;
        }
        if (clicks == 13)
        {
            FT.SetActive(true);
            canClick = false;
            canMove = true;
        }
        if (clicks == 17)
        {

            canClick = false;
            canMove = true;
            second.SetActive(true);
        }
		if (clicks == 20)
		{
			
			canClick = false;
			canMove = true;
			third.SetActive(true);
		}
        if (clicks == 28)
        {
            canClick = false;
            canMove = true;
            forth.SetActive(true);
        }
        if (clicks == 32)
        {
            canClick = false;
            canMove = true;
            fifth.SetActive(true);
        }
        if (clicks == 34)
        {
            canClick = false;
            canMove = true;
			//Setting playerprefs at the end of the game
            if (Input.GetKeyDown(KeyCode.Return))
            {
                //Cube Player Prefs to be Used. 
                PlayerPrefs.SetInt("Cube Size", (int)(FT.transform.localScale.x * 100));
                PlayerPrefs.SetInt("Cube Position", (int)((-(1 - FT.transform.localScale.x) / 2) * 10) + 1);
                if (GCS.texT == 0)
                {
                    PlayerPrefs.SetString("Cube Texture", "Plain");
                    PlayerPrefs.SetInt("Cube Transparancy", GCS.tValue + 1);
                }
                else if (GCS.texT == 1)
                {
                    PlayerPrefs.SetString("Cube Texture", "Metal Box");
                    PlayerPrefs.SetInt("Cube Transparancy", GCS.tValue + 1);
                }
                else if (GCS.texT == 2)
                {
                    PlayerPrefs.SetString("Cube Texture", "Wooden Box");
                    PlayerPrefs.SetInt("Cube Transparancy", GCS.tValue + 1);
                }
                else if (GCS.texT == 3)
                {
                    PlayerPrefs.SetString("Cube Texture", "Mirror");
                    PlayerPrefs.SetInt("Cube Transparancy", 100);
                }
                else if (GCS.texT == 4)
                {
                    PlayerPrefs.SetString("Cube Texture", "Dice");
                    PlayerPrefs.SetInt("Cube Transparancy", 100);
                }
                else if (GCS.texT == 5 || GCS.texT == 6)
                {
                    PlayerPrefs.SetString("Cube Texture", "Rubix Cube");
                    PlayerPrefs.SetInt("Cube Transparancy", 100);
                }
                Debug.Log(PlayerPrefs.GetString("Cube Texture"));
                if (GCS.glowOn)
                {
                    PlayerPrefs.SetInt("Cube Glow", 2);
                }
                else
                {
                    PlayerPrefs.SetInt("Cube Glow", 1);
                }

                //Ladder Player Prefs to be used
                if (ST.testIfClose()) {
                    PlayerPrefs.SetInt("Ladder Leaning", 2);
                 }
                else
                {
                    PlayerPrefs.SetInt("Ladder Leaning", 1);
                }
                if (ST.mString == 0 || ST.mString == 1 || ST.mString == 3)
                {
                    PlayerPrefs.SetInt("Ladder Strength", 2);
                }
                else
                {
                    PlayerPrefs.SetInt("Ladder Strength", 1);
                }

                //Horse Player Prefs to be used
                float distanceBetween = Vector3.Distance(TT.horse.transform.position, FT.transform.position);
                if (distanceBetween < 10)
                {
                    PlayerPrefs.SetString("Horse Distance", "Close");
                }
                else if (distanceBetween < 30)
                {
                    PlayerPrefs.SetString("Horse Distance", "Medium");
                }
                else if (distanceBetween > 30)
                {
                    PlayerPrefs.SetString("Horse Distance", "Far");
                }
                if (TT.setNum == 0)
                {
                    PlayerPrefs.SetInt("Horse Mythicality", 1);
                }
                else
                {
                    PlayerPrefs.SetInt("Horse Mythicality", 2);
                }

                if (TT.desNum == 0)
                {
                    PlayerPrefs.SetString("Horse Description", TT.descriptions[0]);
                }
                else if (TT.desNum == 1)
                {
                    PlayerPrefs.SetString("Horse Description", TT.descriptions[1]);
                }
                else if (TT.desNum == 2)
                {
                    PlayerPrefs.SetString("Horse Description", TT.descriptions[2]);
                }
                else if (TT.desNum == 3)
                {
                    PlayerPrefs.SetString("Horse Description", TT.descriptions[3]);
                }

                if (TT.yesSaddle)
                {
                    PlayerPrefs.SetInt("Horse Saddle", 2);
                }
                else
                {
                    PlayerPrefs.SetInt("Horse Saddle", 1);
                }

                //Flower Player Prefs to be used
                GameObject[] flowerCollection = GameObject.FindGameObjectsWithTag("Flowers");
                int amt = 0, aliveAmt = 0, deadAmt = 0;
                foreach (GameObject f in flowerCollection)
                {
                    if (!f.layer.Equals("Pot"))
                    {
                        amt++;
                        if (f.layer == 12)
                        {
                            aliveAmt++;
                        }
                        else if (f.layer == 13)
                        {
                            deadAmt++;
                        }
                    }
                    
                }
                Debug.Log(amt);
                Debug.Log(aliveAmt);
                Debug.Log(deadAmt);
                if (amt == 0)
                {
                    PlayerPrefs.SetString("Flower Amount", "None");
                }
                else if (amt <= 5)
                {
                    PlayerPrefs.SetString("Flower Amount", "Small");
                }
                else if (amt <= 20)
                {
                    PlayerPrefs.SetString("Flower Amount", "Medium");
                }
                else if (amt > 20)
                {
                    PlayerPrefs.SetString("Flower Amount", "Large");
                }

                if (aliveAmt > deadAmt)
                {
                    PlayerPrefs.SetInt("Flower Vitality", 2);
                }
                else if (deadAmt >= aliveAmt)
                {
                    PlayerPrefs.SetInt("Flower Vitality", 1);
                }

                //Storm Player Prefs
                if (F5T.stormTypeNum == 0)
                {
                       if (F5T.isApproaching)
                    {
                        PlayerPrefs.SetInt("Storm Threat", 2);
                    }
                       else
                    {
                        PlayerPrefs.SetInt("Storm Threat", 1);
                    }
                }
                else if (F5T.stormTypeNum > 0)
                {
                    PlayerPrefs.SetInt("Storm Threat", 3);
                }

                if (F5T.cubeAffected)
                {
                    PlayerPrefs.SetInt("Cube Affected", 2);
                }
                else
                {
                    PlayerPrefs.SetInt("Cube Affected", 1);
                }
                if (F5T.ladderAffected)
                {
                    PlayerPrefs.SetInt("Ladder Affected", 2);
                }
                else
                {
                    PlayerPrefs.SetInt("Ladder Affected", 1);
                }
                if (F5T.horseAffected)
                {
                    PlayerPrefs.SetInt("Horse Affected", 2);
                }
                else
                {
                    PlayerPrefs.SetInt("Horse Affected", 1);
                }
                if (F5T.flowersAffected)
                {
                    PlayerPrefs.SetInt("Flowers Affected", 2);
                }
                else
                {
                    PlayerPrefs.SetInt("Flowers Affected", 1);
                }
				if (PlayerPrefs.GetInt("Games Played") == 0 || PlayerPrefs.GetInt("Games Played") == 2 || PlayerPrefs.GetInt("Games Played") == 5 || PlayerPrefs.GetInt("Games Played") == 7)
                {
                    PlayerPrefs.SetInt("Games Played", PlayerPrefs.GetInt("Games Played") + 1);
                }
                

                BS2.SetActive(true);
                
                    canClick = true;
                    clicks++;
                FT.SetActive(false);
                second.SetActive(false);
                third.SetActive(false);
                forth.SetActive(false);
                fifth.SetActive(false);
                
                
            }
        }
        if (clicks == 38)
        {
            canClick = false;
            if (Input.GetKeyDown(KeyCode.Return))
            {
				//Save playerprefs and load main menu
                PlayerPrefs.Save();
                Application.LoadLevel(1);
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log(ST.testIfClose());
        }
		//Move location of player when finished editing things
        if (GCS.doneFirst)
        {
            if (clicks == 13)
            {
                main.transform.position = new Vector3(FT.transform.position.x, FT.transform.position.y + 1.5f, FT.transform.position.z - (5 * FT.transform.localScale.x));
                main.transform.eulerAngles = Vector3.zero;
                //main.transform.LookAt(FT.transform);
                canClick = true;
                canMove = false;
                clicks++;
            }
        }  
		if (ST.doneSecond)
		{
			if (clicks == 17)
			{
				main.transform.position = new Vector3(ST.ladders[ST.texL].transform.position.x, ST.ladders[ST.texL].transform.position.y + 2f, ST.ladders[ST.texL].transform.position.z - (3 * ST.ladders[ST.texL].transform.localScale.x));
                main.transform.eulerAngles = Vector3.zero;
                //main.transform.LookAt(ST.transform);
                canClick = true;
				canMove = false;
				clicks++; 
			}
		}
        if (TT.doneThird)
        {
            if (clicks == 20)
            {
                main.transform.position = new Vector3(TT.horse.transform.position.x, TT.horse.transform.position.y + 2, TT.horse.transform.position.z - (6 * TT.horse.transform.localScale.x));
                main.transform.eulerAngles = Vector3.zero;
                //main.transform.LookAt(TT.transform);
                canClick = true;
                canMove = false;
                clicks++;
            }
        }
        if (F4T.doneForth && clicks == 28)
        {
            main.transform.position = new Vector3(F4T.GS.transform.position.x, F4T.GS.transform.position.y + 2f, F4T.GS.transform.position.z - (2 * F4T.GS.transform.localScale.x));
             main.transform.eulerAngles = Vector3.zero;
            //main.transform.LookAt(F4T.transform);
            canClick = true;
            canMove = false;
            clicks++;
        }
        if (F5T.doneFifth && clicks == 32)
        {
            main.transform.position = new Vector3(FT.transform.position.x, FT.transform.position.y + 1.5f, FT.transform.position.z - (5 * FT.transform.localScale.x));
            main.transform.eulerAngles = Vector3.zero;
            //main.transform.LookAt(FT.transform);
            canClick = true;
            canMove = false;
            clicks++;
        }  

    }
    void OnGUI()
    {
		//Scale GUI elements based of off resolution
        float rX, rY;
        float scale_width, scale_height;
        scale_width = 1296;
        scale_height = 729;
        rX = Screen.width / scale_width;
        rY = Screen.height / scale_height;
        //Debug.Log(rX);
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rX, rY, 1));
        if (textSkin != null)
        {
            GUI.skin = textSkin;
        }
        //GUI.skin.label.fontSize = Mathf.RoundToInt(55 * Screen.width / (float)(scale_width * 1.0));

		//Display text in game based off click counter
        if (clicks == 0)
        {
            GUI.Label(new Rect(30, 30, 1296 - 60, 729 - 60), "We're going to play a little game");
            GUI.Label(new Rect(30, (729 / 2) + 100, 1296 - 60, 120), "(Left-Click mouse to continue)");
        }
        if (clicks == 1)
        {
            GUI.Label(new Rect(30, 30, 1296 - 60, 729 - 60), "I want you to imagine the things I tell you in your head. You will then be able to create" +
                " what you imagined.");

        }
        if (clicks == 2)
        {
            GUI.Label(new Rect(30, 30, 1296 - 60, 729 - 60), "It is imperative that you create something most accurately to what was in your head.");
        }
        if (clicks == 3)
        {
            GUI.Label(new Rect(30, 30, 1296 - 60, 729 - 60), "We will do our best to guide you through this process.");
        }
        if (clicks == 4)
        {
            GUI.Label(new Rect(30, 30, 1296 - 60, 729 - 60), "Okay, we are now ready to begin. First, imagine you are in a desert. There's nothing but " +
                "sand surrounding you.");
        }
        if (clicks == 6)
        {
            GUI.Box(new Rect(30, 729 - 100, 1296 - 60, 90), "Here we are. Let's go over the controls");
        }
        if (clicks == 7)
        {
            GUI.Box(new Rect(30, 729 - 100, 1296 - 60, 90), "Use the W-A-S-D buttons or arrow keys to to move.");
        }
        if (clicks == 8)
        {
            GUI.Box(new Rect(30, 729 - 200, 1296 - 60, 190), "Click the middle mouse button and drag to control the camera.");
        }
        if (clicks == 9)
        {
            GUI.Box(new Rect(30, 729 - 100, 1296 - 60, 90), "Press shift to move up, and ctrl to move down.");
        }
        if (clicks == 10)
        {
            GUI.Box(new Rect(30, 729 - 100, 1296 - 60, 90), "Let's move on to your first task.");
        }
        if (clicks == 11)
        {
            GUI.Box(new Rect(30, 729 - 100, 1296 - 60, 90), "I want you to imagine a cube.");
        }
        if (clicks == 12)
        {
            GUI.Box(new Rect(30, 729 - 100, 1296 - 60, 90), "Once that is in your head, click to continue.");
        }
        if (clicks == 13)
        {
            GUI.Box(new Rect(30, 729 - 100, 1296 - 60, 90), "Create the cube");
        }
        if (clicks == 14)
        {
            GUI.Box(new Rect(30, 729 - 100, 1296 - 60, 90), "Congratulations. You are done with the fist step.");
        }
        if (clicks == 15)
        {
            GUI.Box(new Rect(30, 729 - 100, 1296 - 60, 90), "Next, I want you to imagine a ladder.");
        }
        if (clicks == 16)
        {
            GUI.Box(new Rect(30, 729 - 100, 1296 - 60, 90), "Once that's in your head, click to continue");
        }
        if (clicks == 17)
        {
            GUI.Box(new Rect(30, 729 - 100, 1296 - 60, 90), "Create the ladder");
        }
		if (clicks == 18) {
			GUI.Box(new Rect(30, 729 - 100, 1296 - 60, 90), "The next step is to imagine a horse");
		}
		if (clicks == 19) {
			GUI.Box(new Rect(30, 729 - 100, 1296 - 60, 90), "Once that's in your head, click to continue");
		}
		if (clicks == 20)
		{
			GUI.Box(new Rect(30, 729 - 100, 1296 - 60, 90), "Create the horse");
		}
        if (clicks == 21)
        {
            GUI.Box(new Rect(30, 729 - 100, 1296 - 60, 90), "You're almost finished! Two more tasks to go!");
        }
        if (clicks == 22)
        {
            GUI.Box(new Rect(30, 729 - 100, 1296 - 60, 90), "Your semi-final task is to imagine a flower");
        }
        if (clicks == 23)
        {
            GUI.Box(new Rect(30, 729 - 100, 1296 - 60, 90), "Once that's in your head, click to continue");
        }
        if (clicks == 24)
        {
            GUI.Box(new Rect(30, 729 - 100, 1296 - 60, 90), "The controls for the flowers are a little different.");
        }
        if (clicks == 25)
        {
            GUI.Box(new Rect(30, 729 - 100, 1296 - 60, 90), "There is a circle on the desert floor.");
        }
        if (clicks == 26)
        {
            GUI.Box(new Rect(30, 729 - 280, 1296 - 60, 270), "When the right mouse button is clicked, flowers are either created or destroyed at that spot.");
        }
        if (clicks == 27)
        {
            GUI.Box(new Rect(30, 729 - 280, 1296 - 60, 270), "The type of flower, color, state, and whether or not flowers are created or destroyed in determined by selections in the menu");
        }
        if (clicks == 28)
        {
            GUI.Box(new Rect(30, 729 - 100, 1296 - 60, 90), "Right-Click to create/destroy flowers");
        }
        if (clicks == 29)
        {
            GUI.Box(new Rect(30, 729 - 100, 1296 - 60, 90), "You're almost there. I have one more task for you.");
        }
        if (clicks == 30)
        {
            GUI.Box(new Rect(30, 729 - 100, 1296 - 60, 90), "Coming into this scene is a storm. Imagine the storm");
        }
        if (clicks == 31)
        {
            GUI.Box(new Rect(30, 729 - 100, 1296 - 60, 90), "Once that's in your head, click to continue");
        }
        if (clicks == 32)
        {
            GUI.Box(new Rect(30, 729 - 100, 1296 - 60, 90), "Create the storm");
        }
        if (clicks == 33)
        {
            GUI.Box(new Rect(30, 729 - 100, 1296 - 60, 90), "Congratulations! We are done.");
        }
        if (clicks == 34)
        {
            GUI.Box(new Rect(30, 729 - 190, 1296 - 60, 180), "You may make changes now if you want, press the enter button to move on when you are ready.");
        }
        if (clicks == 35)
        {
            GUI.Label(new Rect(30, 30, 1296 - 60, 729 - 60), "But what does this all mean?");
        }
        if (clicks == 36)
        {
            GUI.Label(new Rect(30, 30, 1296 - 60, 729 - 60), "As you were playing this game, we were testing your personality.");
        }
        if (clicks == 37)
        {
            GUI.Label(new Rect(30, 30, 1296 - 60, 729 - 60), "When asked to describe blank entities, your imagination will tend to project its own identity onto it.");
        }
      
        string text = "It is important that you read all of this text, without skipping."
            + "\r\n\r\nIn the first phase of the game, we used basic elements of nature that are connected to what we perceive in their meaning."
            + "\r\n\r\nThe cube is you.";

        text += "\r\n\r\nThe size is ostensibly your ego: a large cube means you're pretty sure of yourself, a small cube less so.";
        if (PlayerPrefs.GetInt("Cube Size") < 20)
        {
            text += "\r\n\r\nYour cube was very small, meaning you are probably quite insecure about yourself.";
        }
        else if (PlayerPrefs.GetInt("Cube Size") < 60)
        {
            text += "\r\n\r\nYour cube was small, meaning you are probably fairly soft - spoken about yourself.";
        }
        else if (PlayerPrefs.GetInt("Cube Size") < 140)
        {
            text += "\r\n\r\nYour cube was about an average size, meaning you are on the middle ground when it comes to how sure you are about yourself.";
        }
        else if (PlayerPrefs.GetInt("Cube Size") < 220)
        {
            text += "\r\n\r\nYour cube size was large, meaning you are probably pretty confident about yourself.";
        }
        else if (PlayerPrefs.GetInt("Cube Size") > 220)
        {
            text += "\r\n\r\nYour cube was very large, meaning you likely place yourself above all odds.";
        }

        text += "\r\n\r\nThe vertical placement of the cube is how grounded you are.Resting on the sand? You're probably pretty down to earth. Floating in the sky? Your head is in the clouds.";
        if (PlayerPrefs.GetInt("Cube Position") <= 4) {
            text += "\r\n\r\nYour cube was resting on the ground, meaning when it comes to how you go about things you are more practical and realistic.";
        }
        else {
            text += "\r\n\r\nYour cube was floating in the air, meaning you are heavily based around your ideas, fantasies, and what goes on in your head.";
        }
        text += "\r\n\r\nThe cube's material conveys how open you are.";
        if (PlayerPrefs.GetInt("Cube Transparancy") <= 70)
        {
            text += "\r\n\r\nYour cube was transparent, which means you are an open book, transparant, and inviting.";
        }
        else
        {
            text += "\r\n\r\nYour cube was not transparent, which means you are more protective of your mind.";
        }
        if (PlayerPrefs.GetInt("Cube Glow") == 2)
        {
            text += "\r\n\r\nYour cube was glowing, meaning you're likely a positive person, who aims to raise the spirits of others.";
        }
        if (PlayerPrefs.GetString("Cube Texture").Equals("Mirror"))
        {
            text += "\r\n\r\nYour cube was a mirror, which means you reflect the wills of other people.";
        }
        else if (PlayerPrefs.GetString("Cube Texture").Equals("Rubix Cube"))
        {
            text += "\r\n\r\nYour cube was a rubix cube, which means you feel complex and feel not understood.";
        }
        else if (PlayerPrefs.GetString("Cube Texture").Equals("Metal Box"))
        {
            text += "\r\n\r\nYour cube was a metal box, which means you feel that you have secrets locked up.";
        }

        text += "\r\n\r\nThe ladder represents your friends.";
        text += "\r\n\r\nAre your friends leaning on the cube? Your friends depend on you, and are close.";
        if (PlayerPrefs.GetInt("Ladder Leaning") == 2)
        {
            text += "\r\n\r\nYour ladder was leaning on the cube, meaning your friends depend on you.";
        }
        else
        {
            text += "\r\n\r\nYour ladder was not leaning on the cube, meaning your friends are more indepentant, less close, or you feel more dependant on them.";
        }
        text += "\r\n\r\nIs the ladder frail, or robust? Tall or short? Does it lead inside the cube? Or is it cast to one side, lying unloved on the sand? By now you should be able to draw your own conclusions.";
        if (PlayerPrefs.GetInt("Ladder Strength") == 2)
        {
            text += "\r\n\r\nYour ladder was strong, meaning your friendships feel strong and secure.";
        }
        else
        {
            text += "\r\n\r\nYour ladder was weak, meaning your friendships feel weak and falling apart.";
        }

        text += "\r\n\r\nThe horse represents your dream partner.";
        text += "\r\n\r\nThe type of horse reveals a lot about what you yearn for in a partner.";
        text += "\r\n";
        if (PlayerPrefs.GetInt("Horse Mythicality") == 2)
        {
            text += "\r\nYour horse was mythical, which means your ideal current or imaginary significant other is like a dream, and seems impossible.";
        }
        else
        {
            text += "\r\nYour horse was not mythical, which means your ideal current or imaginary significant other is more realistic, steady, and reliable.";
        }
        if (PlayerPrefs.GetString("Horse Description").Equals("Strong, steady, workhorse"))
        {
            text += "\r\nYour horse was a strong, steady, workhorse, meaning your ideal current or imaginary significant other is seen as steady and reliable.";
        }
        else if (PlayerPrefs.GetString("Horse Description").Equals("Battle Horse"))
        {
            text += "\r\nYour horse was a battle horse, meaning your ideal current or imaginary significant other is strong and safe.";
        }
        else if (PlayerPrefs.GetString("Horse Description").Equals("Pretty Show Horse"))
        {
            text += "\r\nYour horse was a pretty show horse, meaning your ideal current or imaginary significant other is cute, nice, and loving.";
        }
        else
        {
            text += "\r\nYour horse was a stallion, meaning your ideal current or imaginary significant other is strong, beautiful, and independant.";
        }
        text += "\r\n\r\nThe distance between the horse and the cube tells us how close you feel to your significant other.";
        if (PlayerPrefs.GetString("Horse Distance").Equals("Close"))
        {
            text += "\r\n\r\n Your horse was close to the cube. You see you and your ideal current or imaginary significant other as very close.";
        }
        else if (PlayerPrefs.GetString("Horse Distance").Equals("Medium"))
        {
            text += "\r\n\r\n Your horse was a medium distance from the cube. You see you and your ideal current or imaginary significant other as some distance apart. You may be drifting apart or coming together, or feel like the love of your life is just around the corner.";
        }
        else
        {
            text += "\r\n\r\n Your horse was far from the cube. You see you and your ideal current or imaginary significant other as very far away. You may feel very distant from your current significant other, or feel like the love of your life is very far away from you right now and it will be a long time before you find them.";
        }
        text += "\r\n\r\nWhether or not your horse was saddled tells us about who you like to have control in a relationship";
        if (PlayerPrefs.GetInt("Horse Saddle") == 2)
        {
            text += "\r\n\r\nYour horse was saddled, meaning you see yourself as being in control with your relationships.";
        }
        else
        {
            text += "\r\n\r\nYour horse was not saddled, meaning you like to give control to your significant other.";
        }
        text += "\r\n\r\nThis can represent a current partner, or an aspirational one, but the results are often a mix of touching and hilarious. Be sure to draw your own conclusions based off of what you imagined.";

        text += "\r\n\r\nThe flowers represent children.";
        text += "\r\n\r\nThe number of flowers relates to how many you imagine having.";
        if (PlayerPrefs.GetString("Flower Amount").Equals("None"))
        {
            text += "\r\n\r\nThere are NO flowers- You don't see yourself having any children.";
        }
        else if (PlayerPrefs.GetString("Flower Amount").Equals("Small"))
        {
            text += "\r\n\r\nYou had a small amount of flowers, meaning you probably wish to have little to few children, if any at all.";
        }
        else if (PlayerPrefs.GetString("Flower Amount").Equals("Medium"))
        {
            text += "\r\n\r\nYou had about an average amount of flowers, meaning you probably wish to have some kids, not too many but not too few.";
        }
        else
        {
            text += "\r\n\r\nYou had lots of flowers, meaning you proabably wish to have a 'large' amount of kids in the future.";
        }
        text += "\r\n\r\nThe colour and vitality of the flowers can speak to their health and presumed prosperity.The placement - particularly in relation to the cube - can reveal interesting relations.";
        if (PlayerPrefs.GetInt("Flower Vitality") == 2)
        {
            text += "\r\n\r\nYour flowers were healthy- You presume your kids will be very healthy.";
        }
        else
        {
            text += "\r\n\r\nYour flowers were wilting- You worry your kids may have some troubles or weaknesses";
        }
        text += "\r\n\r\nFinally, the storm represents threat.";

        text += "\r\n\r\nThis speaks to the current state of the person, and how they perceive risk in their life.";
        text += "\r\n\r\nTherefore, the current threat level of the storm says a lot about you.";
        if (PlayerPrefs.GetInt("Storm Threat") == 0)
        {
            text += "\r\n\r\nYour storm was far away and fading- You see the biggest threat in your mind as being mostly overcome and fading away.";
        }
        else if (PlayerPrefs.GetInt("Storm Threat") == 2)
        {
            text += "\r\n\r\nYour storm was far away but approaching- You see the biggest threat in your mind as coming directly toward you and ready to wreak havok.";
        }
        else
        {
            text += "\r\n\r\nYour storm was in the middle of everything-You see yourself as being in the middle of danger and pain, and potentially have some immediate trauma in your life.";
        }
        text += "\r\n\r\nWhat objects in your scene the storm affects represents what elements of your life are being affected by the threat that you perceive.";
        text += "\r\n";
        if (PlayerPrefs.GetInt("Cube Affected") == 2)
        {
            text += "\r\nYour storm was affecting the cube, meaning that you are being influenced by issues or danger in your life.";
        }
        if (PlayerPrefs.GetInt("Ladder Affected") == 2)
        {
            text += "\r\nYour storm was affecting the ladder, meaning that your friendships are being influenced by issues or danger in your life.";
        }
        if (PlayerPrefs.GetInt("Horse Affected") == 2)
        {
            text += "\r\nYour storm was affecting the horse, meaning that your ideal current or imaginary significant other is being influenced by issues or danger in your life.";
        }
        if (PlayerPrefs.GetInt("Flowers Affected") == 2)
        {
            text += "\r\nYour storm was affecting the flowers, meaning that your current or future children is being influenced by issues or danger in your life.";
        }

        text += "\r\n\r\nNow is everything noted from this section 100% correct? Of course it isn't. You won't be reading any peer-reviewed journals on the soothsaying properties of horses and ladders. This part was just a test based on what you relate and tie different ideas and feelings to certain figures.";

        //text += "\r\n\r\n\r\n<Press Enter To Move Back To Main Menu>";
        text += "\r\n\r\n\r\n<Scroll All The Way Down And Press Button To Move Back To Main Menu>";
        if (clicks == 38)
        {

            scrollViewVector = GUI.BeginScrollView(new Rect(30, 30, 1296 - 60, 729 - 60), scrollViewVector, new Rect(0, 0, 1296 - 80, 17 * 729 ));
            //vScrollbarValue = GUI.BeginScrollView(new Rect(30, 30, 1296 - 60, 729 - 60), vScrollbarValue, new Rect(30,30, 1296 - 60, 729 - 60));
            GUI.TextArea(new Rect(10, 10, 1296 - 90, 729 * 16 - 10 ), text);
            if (GUI.Button(new Rect (10, 729 * 16, 1296 - 90, 729), "Press to Return to the main menu")) {
                PlayerPrefs.Save();
                Application.LoadLevel(1);
            }
            GUI.EndScrollView();
        }

    }
	public void editingWhat (int TaskNumber) {
		if (TaskNumber == 1) {
			editingCube = true;
			editingLadder = false;
			editingHorse = false;
			editingFlowers = false;
			editingStorm = false;
		}
		else if (TaskNumber == 2) {
			editingCube = false;
			editingLadder = true;
			editingHorse = false;
			editingFlowers = false;
			editingStorm = false;
		}
		else if (TaskNumber == 3) {
			editingCube = false;
			editingLadder = false;
			editingHorse = true;
			editingFlowers = false;
			editingStorm = false;
		}
		else if (TaskNumber == 4) {
			editingCube = false;
			editingLadder = false;
			editingHorse = false;
			editingFlowers = true;
			editingStorm = false;
		}
		else if (TaskNumber == 5) {
			editingCube = false;
			editingLadder = false;
			editingHorse = false;
			editingFlowers = false;
			editingStorm = true;
		}
		}
}
