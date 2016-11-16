using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;

public class PhoneController : MonoBehaviour {
	/*
	 * This class controlls player interaction with the phone
	 * */

	public bool lookingAtPhone = false; //Whether or not we should be looking at phone
	public bool facing = false; //Whether or not we are facing the phone completely
	public bool phoneInteration = false; //Whether or not we are currently interacting with phone
    GameObject mainCamera, phoneCamera, phone, arms, player, forward; //Gameobjects needed to reference to control phone interaction
    float speed = 4; //How fast we look at/away from phone
    PlayerControllerMBTI PCMBTI; //Player controller reference. Needed to stop movement
    FirstPersonController FPC; //First Person Controller reference. Needed to stop movement
    public int screen = 0; //Keeps track of what screen of phone we are in

    public Texture2D[] icons; //Array that holds all the icons displayed on the phone
    public GUISkin phoneSkin, messageSkin, optionsSkin; //Skins needed to display phone screens with desired look
    public Texture2D[] screens; //Array that holds all the screens to be displayed on the phone
    public GameObject controller;
    public GUISkin sendSkin, receiveSkin; //Skins needed for desired affect. Specifically sent and received messages
    public GUISkin mainSkin; //Skin needed to receive desired affect
    Contact girlfriend, roommate, girlfriendsParents, boss, tomClassmate; //All contacts needed in phone throughout the story

    public Renderer[] rends; //Holds all renderers, so they can easily ALL be changed based on the current screen
	public int contactNum = 0; //Keeps track of what contact we are currently viewing when looking at messages
	public Contact vc; // Current contact in it's entirity
    bool unread = false; //Whether or not we have unread messages
	public List<MessageController> messageControllers = new List<MessageController> (); //List of message controllers that will have one for every contact
	public List<string> countedNames = new List<string>();


    //Music Tab
    public AudioSource AS, SEAS; 
    HolderOfAudio HA;


	public Texture2D ffb, rb, pb; //Fast forward, rewind, and play buttons for phone
    float Sens = 2; //Default sensitivity for mouse in needed float format
    int MS = 20, V = 100, SEV = 50; //Default Mouse Sensitivity, Music Volume, and sound Effect volume. All can be changed in options 
    bool quitGame = false, quitMenu = false; //whether or not the quit game or quit menu screens are open
    //FirstPersonController FPC;

    Testing T;

    public int story;


    // Use this for initialization
    void Start () {
		//Initializes and connects above variables and objects 

        player = GameObject.FindGameObjectWithTag("Player");
        T = player.GetComponent<Testing>();
        story = 0;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        phoneCamera = GameObject.FindGameObjectWithTag("Camera");
        phoneCamera.SetActive(false);
        phone = GameObject.Find("FPSController/Phone/Nexus4");
        forward = GameObject.Find("FPSController/Phone/Forward");
        arms = GameObject.Find("FPSController/Phone/Arms");
        arms.SetActive(false);
        rends = phone.GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in rends)
        {
            rend.material.SetTexture("_MainTex", screens[screen]);
        }
        phone.SetActive(false);
        PCMBTI = player.GetComponent<PlayerControllerMBTI>();
        FPC = player.GetComponent<FirstPersonController>();
        girlfriend = new Contact("Girlfriend", controller, sendSkin, receiveSkin);
		 roommate = new Contact("Roommate",controller, sendSkin, receiveSkin);
        girlfriendsParents = new Contact("Girlfriend's Parents", controller, sendSkin, receiveSkin);
        boss = new Contact("Boss", controller, sendSkin, receiveSkin);
        tomClassmate = new Contact("Tom (Classmate)", controller, sendSkin, receiveSkin);
        girlfriend.receive("When will you be home? Dinner's almost ready!");
        girlfriend.receive("Please Hurry");
        //Music
        AS = this.gameObject.GetComponent<AudioSource>();
        SEAS = player.GetComponent<AudioSource>();
        HA = this.GetComponent<HolderOfAudio>();
    }

    // Update is called once per frame
    void Update () {

		#region receivedMessages
		//When we reach a certain point in story, send a desired message from a desired contact
        if (PCMBTI.clicks == 23)
        {
            roommate.receive("Hey man, I need to talk to you. Can you meet me in my room?");
            story++;
            PCMBTI.clicks++;
        }
        else if (PCMBTI.clicks == 28)
        {
            boss.receive("I need you in my office.");
            story++;
            PCMBTI.clicks++;
        }
        else if (PCMBTI.clicks == 36)
        {
            tomClassmate.receive("Don't forget we're meeting at your place today for the project in an hour.");
            story++;
            PCMBTI.clicks++;
        }
        else if (PCMBTI.clicks == 52)
        {
            girlfriendsParents.receive("Hey... you might be wondering where our daughter went.");
			girlfriendsParents.receive("We've just found out that she has been diagnosed with stage 4 terminal cancer.");

			story++;
            PCMBTI.clicks++;
        }
		#endregion
	if (Input.GetKeyDown(KeyCode.Tab))
        {
			//If tab is pressed, and we are not interacting with anything and can move, look at phone.
			//IF already interacting, stop.
            if (!T.interacting)
            {
                if (!lookingAtPhone)
                {
                    if (PCMBTI.canMove || PCMBTI.clicks == 8) {
                        lookingAtPhone = true;
                    }
                    //lookAtPhone();
                }
                else
                {
                    if (facing && PCMBTI.clicks > 15)
                    {
                        lookingAtPhone = false;
                    }
                    //lookAway();
                }
            }
        }
		//If looking at phone is true, call lookatphone.
    if (lookingAtPhone)
        {
            lookAtPhone();
        }
    else if (!lookingAtPhone && facing)
        {
            lookAway();
        }
		//Set each texture in phone to screen texture
        foreach (Renderer rend in rends)
        {
            rend.material.SetTexture("_MainTex", screens[screen]);
        }
		//If a contact is selected, set contact num based on what one is selected. This will be used to test things easily
		if (!Object.ReferenceEquals(vc, null))
		{
            if (string.Equals(vc.getName(), girlfriend.getName()))
			{
				contactNum = 1;
			}
            else if (string.Equals(vc.getName(), roommate.getName()))
			{
				contactNum = 2;
			}
            else if (string.Equals(vc.getName(), girlfriendsParents.getName()))
            {
                contactNum = 3;
            }
            else if (string.Equals(vc.getName(), boss.getName()))
            {
                contactNum = 4;
            }
            else if (string.Equals(vc.getName(), tomClassmate.getName()))
            {
                contactNum = 5;
            }
        }
	}

	//void for looking at phone
    public void lookAtPhone()
    {
		//Set movement to null
        PCMBTI.canMove = false;
        FPC.enabled = false;
		//Turn on phone and arm (that we see when viewing the phone)
        phone.SetActive(true);
        arms.SetActive(true);
		//If we have unread messages, display those instead of going to home screen first
        if (unread)
        {
            if (countedNames.Count == 1)
            {
                lookingAtPhone = true;
                screen = 4;
            }
            else if (countedNames.Count > 1)
            {
                lookingAtPhone = true;
                screen = 3;
            }
            unread = false;
        }
		//If we are not facing the phone, move to face the phone
        if (!facing)
        {
            Vector3 dir = phone.transform.position - mainCamera.transform.position;
            Quaternion rot = Quaternion.LookRotation(dir);
            // slerp to the desired rotation over time
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, rot, speed * Time.deltaTime);
            float angle = .01f;
            if (Vector3.Angle(mainCamera.gameObject.transform.forward, phone.transform.position - mainCamera.gameObject.transform.position) < angle)
                {
                    if (screen == 0)
				screen = 2;
				if (screen == 4) {
                    vc.display();
				}
                if (screen == 3)
                {
                    countedNames.Clear();
                }
                facing = true;
                Vector3 temp2 = mainCamera.transform.localPosition;
                temp2.x = 0;
				temp2.y = .8f;
				temp2.z = 0;
                mainCamera.transform.localPosition = temp2;
                Vector3 temp = phoneCamera.transform.localEulerAngles;
                temp.x = 60.84952f;
				temp.y = 1.301337f;
				temp.z = -0.0003637327f;
                mainCamera.transform.localEulerAngles = temp;
            }
        }
        else
        {
			//When we are facing, set the interaction to true
            phoneInteration = true;
        }
    }
	//called when looking away
    public void lookAway()
    {
       //Close messages first
        foreach (MessageController mc in messageControllers)
        {
            mc.closeMessages();
        }
		//Turn screen to black screen
        screen = 0;   
		//Begin looking away
        Vector3 dir = forward.transform.position - mainCamera.transform.position;
        Quaternion rot = Quaternion.LookRotation(dir);

        mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, rot, speed * Time.deltaTime);
        float angle = .01f;
		//Not interacting anymore
        phoneInteration = false;
		//Once we get to a certain point, enable movement and controller. Arms and phone are then turned off.
        if (Vector3.Angle(mainCamera.gameObject.transform.forward, forward.transform.position - mainCamera.gameObject.transform.position) < angle)
        {
            FPC.enabled = true;
            PCMBTI.canMove = true;
            phone.SetActive(false);
            arms.SetActive(false);
            facing = false;

			//Set settings to what we set them to
            FPC.changeMouseSensitivity(Sens, Sens);
            MS = (int)(Sens * 10);
            V = (int)(AS.volume * 100);
            SEV = (int)(SEAS.volume * 100);
        }
        }
    void OnGUI ()
    {

        //Debug.Log(Screen.height);
		//Scale text based off of resolution
        float rX, rY;
        float scale_width, scale_height;
        scale_width = 1316;
        scale_height = 740;
        rX = Screen.width / scale_width;
        rY = Screen.height / scale_height;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rX, rY, 1));

        if (phoneInteration) {
			//If we are interacting, always display back and home buttons
            if (GUI.Button(new Rect(590, 580, 50, 50), icons[6]))
            {
                foreach (MessageController mc in messageControllers)
                {
                    mc.closeMessages();
                }
                if (screen == 5 || screen == 7)
                {
                    screen = 2;
                }
                else if (screen != 2)
                screen--;
            }
            if (GUI.Button(new Rect(650, 580, 50, 50), icons[7]))
            {
                foreach (MessageController mc in messageControllers)
                {
                    mc.closeMessages();
                }
                screen = 2;
            }

			//On home screen, display apps and music accessory
			if (screen == 2) {
				if (phoneSkin != null) {
					GUI.skin = phoneSkin;
				}
                
                if (GUI.Button (new Rect (530, 520, 50, 50), icons [0])) {
                    screen = 5;
				}
				if (GUI.Button (new Rect (630, 520, 50, 50), icons [2])) {
					screen = 3;
				}
				if (GUI.Button (new Rect (730, 520, 50, 50), icons [4])) {
                    screen = 7;
				}
                if (!string.Equals(HA.songPlaying, ""))
                {
                    GUI.skin = null;
                    GUI.Box(new Rect(520, 220, 280, 60), "Song Currently Playing: \r\n" + HA.songPlaying);
					if (GUI.Button(new Rect(545, 255, 35, 20), rb))
                    {
                        HA.moveMusicBackward();
                    }
					if (GUI.Button(new Rect(645, 255, 35, 20), pb))
                    {
                        HA.stopMusic();
                    }
					if (GUI.Button(new Rect(745, 255, 35, 20), ffb))
                    {
                        HA.moveMusicForward();
                    }
                }
                else {
                    GUI.Box(new Rect(520, 220, 280, 70), "Song Currently Playing: \r\nNone");
                }
            } 
			//If messages screen, display all messagecontrollers. If one is selected, set vc to selected one, and set screen to the individual messages screen
			else if (screen == 3) {
				int y = 175;
				if (messageSkin != null) {
					GUI.skin = messageSkin;
				}
				for (int i = 0; i < messageControllers.Count; i++) {
					if (GUI.Button (new Rect (525, y, 275, 75), messageControllers [i].getName ())) {
						messageControllers [i].displayMessages ();
						screen = 4;
						vc = messageControllers [i].getContact ();
					}
					y += 75;
					Debug.Log ("Working");
				}
			} 
			//If on individual messages screen, display the messages of that contact, if button is clicked on bottom to send message, pull up options to text him"
			else if (screen == 4) {

				if (GUI.Button(new Rect(510, 130, 30,30),"")) {
					foreach (MessageController mc in messageControllers) {
						mc.closeMessages();
					}
					screen = 3;
				}
				GUI.Label(new Rect(550, 130, 300,30), vc.getName());
				if (GUI.Button(new Rect(520, 550, 225, 20), ""))
				{
					vc.pullUpOptions();
				}
				GUI.Label(new Rect(750, 535, 50, 40), "Send", "Button");

            }
			//If on options screen, display slider information editible features.
            else if (screen == 7)
            {
                GUI.skin = optionsSkin;
				//If quit game or quit menu are pressed, display menus for confirmation
				//If yes is pressed, they will quit the game, or quit to menu
				//If no is pressed, they simply go back to options
                if (quitGame)
                {
                    optionsSkin.box.fontSize = 40;
                    GUI.Box(new Rect(60, 60, 1316 - 120, 740 - 120), "Are you sure you want to quit the game?");
                    if (GUI.Button(new Rect(80, 480, 450, 120), "Yes"))
                    {
                        Application.Quit();
                    }
                    if (GUI.Button(new Rect(716, 480, 450, 120), "No"))
                    {
                        quitGame = false;
                    }
                }
                else if (quitMenu)
                {
                    optionsSkin.box.fontSize = 40;
                    GUI.Box(new Rect(60, 60, 1316 - 120, 740 - 120), "Are you sure you want to quit to the main menu?");
                    if (GUI.Button(new Rect(80, 480, 450, 120), "Yes"))
                    {
						UnityEngine.SceneManagement.SceneManager.LoadScene (1);
                        //Application.LoadLevel(1);
                    }
                    if (GUI.Button(new Rect(716, 480, 450, 120), "No"))
                    {
                        quitMenu = false;
                    }
                }
                else
                {
                    optionsSkin.box.fontSize = 15;
                }
                optionsSkin.label.fontSize = 15;
                GUI.Label(new Rect(600, 110, 200, 30), "Options");
                GUI.Label(new Rect(510, 160, 120, 30), "Mouse Sensitivity");
                //int MS = 20;
                MS = (int)GUI.HorizontalSlider(new Rect(645, 168, 130, 30), MS, 0, 100);
                GUI.Label(new Rect(780, 160, 30, 30), MS.ToString());
                GUI.Label(new Rect(510, 200, 120, 30), "Music Volume");
                V = (int)GUI.HorizontalSlider(new Rect(645, 208, 130, 30), V, 0, 100);
                GUI.Label(new Rect(780, 200, 30, 30), V.ToString());
                GUI.Label(new Rect(505, 240, 200, 30), "Sound-Effect Volume");
                SEV = (int)GUI.HorizontalSlider(new Rect(645, 248, 130, 30), SEV, 0, 100);
                GUI.Label(new Rect(780, 240, 30, 30), SEV.ToString());
                
				if (!quitGame && !quitMenu) { 
					if (GUI.Button (new Rect (520, 470, 280, 40), "Quit Game")) {
						if (quitMenu != true)
							quitGame = true;
					}
					if (GUI.Button (new Rect (520, 420, 280, 40), "Return to Main Menu")) {
						if (quitGame != true)
							quitMenu = true;
					}
					if (GUI.Button (new Rect (520, 520, 280, 40), "Finalize settings")) {
						if (quitGame != true && quitMenu != true) {
							Sens = (float)MS / 10;
							Debug.Log (Sens);
							screen = 2;
							AS.volume = (float)V / 100;
							SEAS.volume = (float)SEV / 100;
						}
					}
				} else {
					//If we are in menu for confirmation, make it so buttons cannot be pressed or interacted with
					GUI.Label (new Rect (520, 470, 280, 40), "Quit Game", "Button");					
					GUI.Label (new Rect (520, 420, 280, 40), "Return to Main Menu", "Button");
					GUI.Label (new Rect (520, 520, 280, 40), "Finalize settings", "Button");
				}

                
               
            }
		}
		//If we are not interacting with phone
		else {
			//If we have any unread messages, set unread to true
			foreach (MessageController m in messageControllers) {
				if (m.unreadMessages) {
					unread = true;
				}
					}
			//If we have unread messages, display notification on screen
			if (unread) {
				foreach (MessageController m in messageControllers) {
				if (countedNames.Count == 1) {
                    GUI.skin = phoneSkin;
						if (m.unreadMessages) {

						vc = m.getContact();
							GUI.Label(new Rect(60,50,400,50), "You have an unread message from: " +vc.getName() + "\r\nPress Tab to View", "s");
							GUI.Box(new Rect (60,85,300,50), vc.lastMessage());
						}
				}
					else {
						GUI.Box(new Rect (60,60,300,50), "You have multiple unread messages!\r\nPress Tab to View");
					}
				}
				if (GUI.Button(new Rect(10,60,50,50), icons[2])) {
					if (countedNames.Count == 1) {
						lookingAtPhone = true;
						screen = 4;
					}
					else if (countedNames.Count > 1) {
						lookingAtPhone = true;
						screen = 3;
					}
					unread = false;
				}
			}
					}
    }
	//Creating contact will create a contact with name given and all the controllers we need.
	void createContact (Contact c, string s) {
		c = new Contact(s, controller, sendSkin,receiveSkin);
	}
}
