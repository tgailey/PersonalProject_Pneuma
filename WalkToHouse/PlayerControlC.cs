using UnityEngine;
using System.Collections;

public class PlayerControlC : MonoBehaviour {
	/*
	 * Third and last player control for Her House Game
	 * */
    public GameObject lookShort, lookLong; //GameObjects on the long and short routes to Look at
    public Texture2D rightArrow, leftArrow; //Textures for left and right arrow.

    string[] returnPaths = { "Short", "Long" }; //Names of the two paths
    int returnPathNum = 0; //Which path is chosen
    CharacterMotor CM; //Motor
    FPSInputController FPS; //Input Controller
    MouseLook[] ML; //Mouse Looks
    bool turnedOn;
    GameObject player;
    public GUISkin textSkin;
    float speed = 2.5f; //How fast we can turn

    int clicks = 0; //Click counter
    Vector2 scrollViewVector; //Blank scroll view for later scrolling
    GameObject BS; //Black screen
    // Use this for initialization
    void Start () {
        if (!GameObject.ReferenceEquals(GameObject.Find("GameHandler"), null))
        {
            if (GameObject.Find("GameHandler").GetComponent<StayOnForever>().inside == 1)
            {
                player = GameObject.FindGameObjectWithTag("Player");
                turnedOn = false;
                switchMovement();

                BS = GameObject.Find("PlayerCapsule/Main Camera/BlockScreen");
                BS.SetActive(false);
            }
        }
	}

    // Update is called once per frame
    void Update() {
        if (!GameObject.ReferenceEquals(GameObject.Find("GameHandler"), null))
        {
            if (GameObject.Find("GameHandler").GetComponent<StayOnForever>().inside == 1)
            {
                if (clicks == 0)
                {
                    switchMovement();
                    clicks++;
                }
                if (clicks == 1)
                {
					//Whether or not we picked long or short path
                    if (returnPathNum == 0)
                    {
                        Vector3 dir = lookShort.transform.position - player.transform.position;
                        dir.y = 0; // keep the direction strictly horizontal
                        Quaternion rot = Quaternion.LookRotation(dir);
                        // slerp to the desired rotation over time
                        player.transform.rotation = Quaternion.Slerp(player.transform.rotation, rot, speed * Time.deltaTime);
                    }
                    else
                    {
                        Vector3 dir = lookLong.transform.position - player.transform.position;
                        dir.y = 0; // keep the direction strictly horizontal
                        Quaternion rot = Quaternion.LookRotation(dir);
                        // slerp to the desired rotation over time
                        player.transform.rotation = Quaternion.Slerp(player.transform.rotation, rot, speed * Time.deltaTime);
                    }
                }
                else if (clicks == 2)
                {
					//Once path is clicked, we set BS to true and display text analysis
                    BS.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.Return))
                    {
						if (PlayerPrefs.GetInt ("Games Player") == 0 || PlayerPrefs.GetInt ("Games Player") == 1 || PlayerPrefs.GetInt ("Games Player") == 5 || PlayerPrefs.GetInt ("Games Player") == 6) {
							PlayerPrefs.SetInt ("Games Played", PlayerPrefs.GetInt ("Games Played") + 2);
						}
                        PlayerPrefs.Save();
                        //Application.LoadLevel(1);
                        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
                    }

                }
            }
        }
    }
    void OnGUI()
    {
		//Scales GUI based off of resolution
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
		//Text displayed based off of click counter
        if (clicks == 1)
        {
            GUI.Box(new Rect(60, 729 - 200, 1296 - 120, 90), "What path do you choose on the way home?");
            GUI.Box(new Rect(120, 729 - 100, 1296 - 470, 90), returnPaths[returnPathNum]);
            if (GUI.Button(new Rect(1296 - 345, 729 - 75, 50, 50), rightArrow))
            {
                if (returnPathNum == 1)
                    returnPathNum = 0;
                else
                    returnPathNum++;
            }
            if (GUI.Button(new Rect(65, 729 - 75, 50, 50), leftArrow))
            {
                if (returnPathNum != 0)
                    returnPathNum--;
                else
                    returnPathNum = 1;
            }
            if (GUI.Button(new Rect(1296 - 280, 729 - 100, 260, 90), "This Path!"))
            {
                //switchMovement();

                if (returnPathNum == 0)
                {
                    PlayerPrefs.SetInt("Second Path Chosen", 1);

                    clicks++;
                }
                else
                {
                    PlayerPrefs.SetInt("Second Path Chosen", 2);
                    clicks++;
                }
            }
        }
        else if (clicks == 2) {
            string text = "Though many of the actions taken in this scenario that obviously tested you on how you act / react in relationships, each one had a lot more meaning than the last."
+ "\r\n\r\nThe first option of taking one of the two roads to your girlfriend’s / boyfriend’s house represents how you are in falling into relationships.";
            if (PlayerPrefs.GetInt("First Path Chosen") == 2) {
                text += "\r\n\r\nYou chose the long path, meaning you take your time and do not fall in love easily.";
            }
            else {
                text += "\r\n\r\nYou took the short path, meaning you fall in love quickly and easily.";
            }
            text += "\r\n\r\nThe rose bushes represented how much you plan to put into a relationship or how much you expect to be put out by your partner. The amount of red roses represented how much you expect to put into a relationship.The amount of white roses represented how much you expect your partner to put into a relationship.";
            text += "\r\n\r\nYou chose " + PlayerPrefs.GetInt("Red Flowers").ToString() + " red flowers and " + PlayerPrefs.GetInt("White Flowers").ToString() + " white flowers, meaning you expect to give " + (PlayerPrefs.GetFloat("Red Flower Percentage")-1).ToString() + "%, and receive " + (PlayerPrefs.GetFloat("White Flower Percentage")-1).ToString() + "% in relationships.";
            if (PlayerPrefs.GetInt("Get Sig Other") == 1) {
                text += "\r\n\r\nYou asked the maid to get your loved one, then you may beat around the bush, maybe asking a third party to intervene. Avoidance of problems runs high.";
            }
            else
            {
                text += "\r\n\r\nIf you went and got your loved one yourself, then you are pretty direct.If there is a problem, you confront it and deal with it.You want to work it out right away.";
            }

            text += "\r\n\r\nThe placement of the roses indicates how often you’d like to see your boyfriend/ girlfriend.";
            if (PlayerPrefs.GetInt("Place Chosen") == 1)
            {
                text += "\r\n\r\nYou placed the roses on the bed means you need lots of reassurance in the relationship, and you’d want to see your loved one every day, if possible";
            }
            else
            {
                text += "\r\n\r\nPlacing the roses by the window show that you don’t expect or need to see your loved one too often.";
            }
            text += "\r\n\r\nWalking into their room and seeing your partner either awake or asleep represents how you expect your partner to be a relationship.";
            if (PlayerPrefs.GetInt("Girlfriend State") == 1)
            {
                text += "\r\n\r\nYou found them asleep, you don’t expect them to change for you and encourage them to be themselves.";
            }
            else
            {
                text += "\r\n\r\nYou found them awake, you expect them to change for you.";
            }
            text += "\r\n\r\nThe road home represents how usually last in love.";
            if (PlayerPrefs.GetInt("Second Path Chosen") == 1)
            {
                text += "\r\n\r\nYou chose the short path, you fall out of love easily and relationships with you are not long.";
            }
            else {
                text += "\r\n\r\nYou chose the long path, you tend to stay in love for a long, long time.";
            }
            text += "\r\n\r\n<Scroll All The Way Down And Press Button To Move Back To Main Menu>";
            scrollViewVector = GUI.BeginScrollView(new Rect(30, 30, 1296 - 60, 729 - 60), scrollViewVector, new Rect(0, 0, 1296 - 80, 7 * 729 + 20));
            //vScrollbarValue = GUI.BeginScrollView(new Rect(30, 30, 1296 - 60, 729 - 60), vScrollbarValue, new Rect(30,30, 1296 - 60, 729 - 60));
            GUI.TextArea(new Rect(10, 10, 1296 - 90, 729 * 7 - 170), text);
            if (GUI.Button(new Rect(10, 729 * 7 - 159, 1296 - 90, 180), "Press to Return to the main menu"))
            {
				if (PlayerPrefs.GetInt ("Games Player") == 0 || PlayerPrefs.GetInt ("Games Player") == 1 || PlayerPrefs.GetInt ("Games Player") == 5 || PlayerPrefs.GetInt ("Games Player") == 6) {
					PlayerPrefs.SetInt ("Games Played", PlayerPrefs.GetInt ("Games Played") + 2);
				}
                PlayerPrefs.Save();
                //Application.LoadLevel(1);
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            }
            GUI.EndScrollView();
        }

            
    }
	//Switches movement on/off when called
    void switchMovement()
    {
        CM = player.GetComponent<CharacterMotor>();
        FPS = player.GetComponent<FPSInputController>();
        ML = player.GetComponentsInChildren<MouseLook>();
        if (turnedOn)
        {
            CM.enabled = false;
            FPS.enabled = false;
            foreach (MouseLook ml in ML)
                ml.enabled = false;
            turnedOn = false;
        }
        else
        {
            CM.enabled = true;
            FPS.enabled = true;
            foreach (MouseLook ml in ML)
                ml.enabled = true;
            turnedOn = true;
        }
    }
}
