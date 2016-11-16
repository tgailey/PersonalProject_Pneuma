using UnityEngine;
using System.Collections;

public class ControllerOfTextMessages : MonoBehaviour {
	/*
	 * Controlls text message responses and when we can send a response or not
	 * */

	//New textmessageoptionsholders for each contact
    public TextMessageOptionsHolder girlfriend;
    public TextMessageOptionsHolder roommate;
    public TextMessageOptionsHolder girlfriendsParents;
    public TextMessageOptionsHolder boss;
    public TextMessageOptionsHolder tomClassmate;
    public PhoneController PC;

	//bools deciding whether or not we can send each contact a text message
    bool canSendGirlfriend = true;
    bool canSendRoomate = false;
    bool canSendParents = false;
    bool canSendBoss = false;
    bool canSendClassmate = false;

    int personalStoryTest;
	//bool if we can show text message options
    public bool showTexts = false;
    void Start () {

		//Initializes all textmessageoptionsholders and creates the options
        girlfriend = new TextMessageOptionsHolder();
        girlfriend.newOptions("I'll be right there!", "Be patient, I'm going as fast as I can", "I'll make you wait as much as I want", "I will!");
        girlfriend.newOptions("None (You broke it)", "None (You broke it)", "None (You broke it)", "None (You broke it)");
        girlfriend.newOptions("I miss you!", "Thinking about you baby.", "Ready for another great day with you.", "Hope you have a great day.");
        girlfriend.newOptions("I miss you!", "Thinking about you baby.", "Ready for another great day with you.", "Hope you have a great day.");
        girlfriend.newOptions("Baby come back!", "We need to talk about this.", "I'm sorry.", "I will always cherish you.");
        //girlfriend.newOptions("None (You broke it)", "None (You broke it)", "None (You broke it)", "None (You broke it)");
        
        roommate = new TextMessageOptionsHolder();
        roommate.newOptions("None (You broke it)", "None (You broke it)", "None (You broke it)", "None (You broke it)");
        roommate.newOptions("I'll be there in a minute.", "Okay man.", "K", "Be right there.");
        roommate.newOptions("Good luck on the test.", "I'm sorry I couldn't do more.", "You are going to do great.", "Believe in yourself a little more.");
        roommate.newOptions("None (You broke it)", "None (You broke it)", "None (You broke it)", "None (You broke it)");
        roommate.newOptions("None (You broke it)", "None (You broke it)", "None (You broke it)", "None (You broke it)");
        //roommate.newOptions("None (You broke it)", "None (You broke it)", "None (You broke it)", "None (You broke it)");

        girlfriendsParents = new TextMessageOptionsHolder();
        girlfriendsParents.newOptions("None (You broke it)", "None (You broke it)", "None (You broke it)", "None (You broke it)");
        girlfriendsParents.newOptions("None (You broke it)", "None (You broke it)", "None (You broke it)", "None (You broke it)");
        girlfriendsParents.newOptions("None (You broke it)", "None (You broke it)", "None (You broke it)", "None (You broke it)");
        girlfriendsParents.newOptions("None (You broke it)", "None (You broke it)", "None (You broke it)", "None (You broke it)");
        girlfriendsParents.newOptions("How long has she known?", "I must talk to her.", "Do you know where she is?", "Thanks for informing me.");
        //girlfriendsParents.newOptions("How long has she known?", "I must talk to her.", "Do you know where she is?", "Thanks for informing me. I needed this information.");

        boss = new TextMessageOptionsHolder();
        boss.newOptions("None (You broke it)", "None (You broke it)", "None (You broke it)", "None (You broke it)");
        boss.newOptions("None (You broke it)", "None (You broke it)", "None (You broke it)", "None (You broke it)");
        boss.newOptions("I'll be right there.", "Yes sir.", "On my way.", "Okay.");
        boss.newOptions("None (You broke it)", "None (You broke it)", "None (You broke it)", "None (You broke it)");
        boss.newOptions("None (You broke it)", "None (You broke it)", "None (You broke it)", "None (You broke it)");
       // boss.newOptions("None (You broke it)", "None (You broke it)", "None (You broke it)", "None (You broke it)");


        tomClassmate = new TextMessageOptionsHolder();
        tomClassmate.newOptions("None (You broke it)", "None (You broke it)", "None (You broke it)", "None (You broke it)");
        tomClassmate.newOptions("None (You broke it)", "None (You broke it)", "None (You broke it)", "None (You broke it)");
        tomClassmate.newOptions("None (You broke it)", "None (You broke it)", "None (You broke it)", "None (You broke it)");
        tomClassmate.newOptions("Yeah I'll be there.", "I won't, thanks for the reminder.", "I totally forgot! Thanks for reminding me.", "Text the others too so they won't forget.");
        tomClassmate.newOptions("None (You broke it)", "None (You broke it)", "None (You broke it)", "None (You broke it)");
        //tomClassmate.newOptions("None (You broke it)", "None (You broke it)", "None (You broke it)", "None (You broke it)");

        PC = GameObject.Find("FPSController").GetComponentInChildren<PhoneController>();
        //Personal story is set to story in PhoneController
		personalStoryTest = PC.story;
	}
	

	void Update () {
		//If personalstory is different then master story (meaning the story moved forward) set whether or not we can send messages to contacts based on point in story and sesne
        if (personalStoryTest != PC.story)
        {
            if (PC.story == 2) 
            {
                canSendGirlfriend = true;
            }
			else if (PC.story == 4)
            {
				canSendGirlfriend = true;
            }

            if (PC.story == 1 || PC.story == 2)
            {
                canSendRoomate = true;
            }
            else
            {
                canSendRoomate = false;
            }

            if (PC.story == 2)
            {
                canSendBoss = true;
            }
            else
            {
                canSendBoss = false;
            }

            if (PC.story == 3)
            {
                canSendClassmate = true;
            }
            else
            {
                canSendClassmate = false;
            }
            if (PC.story == 4)
            {
                canSendParents = true;
            }
            else
            {
                canSendParents = false;
            }
            personalStoryTest = PC.story;
        }
	}
    void OnGUI()
    {
		//Resizes text based off of resolution
        float rX, rY;
        float scale_width, scale_height;
        scale_width = 1316;
        scale_height = 740;
        rX = Screen.width / scale_width;
        rY = Screen.height / scale_height;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rX, rY, 1));
		//If we can show texts, and the contact send boolean is true, display our options
		//If boolean is false, we tell them they cannot send message
        if (showTexts)
        {
            if (PC.contactNum == 1)
            {
                if (canSendGirlfriend)
                {
                    string[] options =    girlfriend.getOptions(PC.story);
                    if (GUI.Button(new Rect(300, 350, 250, 25), options[0]))
                    {
                        PC.vc.send(options[0]);
                        showTexts = false;
                        canSendGirlfriend = false;
						if (PC.story == 2 || PC.story == 3) 
                        PC.vc.receive("You are so sweet! I miss you too!");

                    }
                    else if (GUI.Button(new Rect(750, 350, 250, 25), options[1]))
                    {
                        PC.vc.send(options[1]);
                        showTexts = false;
                        canSendGirlfriend = false;
                        if (PC.story == 2 || PC.story == 3) 
                            PC.vc.receive("You are so sweet! I am thinking about you too.");
                    }
                    else if (GUI.Button(new Rect(525, 300, 250, 25), options[2]))
                    {
                        PC.vc.send(options[2]);
                        showTexts = false;
                        canSendGirlfriend = false;
                        if (PC.story == 2 || PC.story == 3) 
                        PC.vc.receive("You are so sweet! Every day is amazing with you.");
                    }
                    else if (GUI.Button(new Rect(525, 400, 250, 25), options[3]))
                    {
                        PC.vc.send(options[3]);
                        showTexts = false;
                        canSendGirlfriend = false;
                        if (PC.story == 2 || PC.story == 3) 
                        PC.vc.receive("You are so sweet! Hope you have a great day as well.");
                    }
                }
                else
                {
                    GUI.Box(new Rect(500, 400, 300, 100), "You have nothing to say at this time.");
                }
            }
            else if (PC.contactNum == 2)
            {
                //string[] options = roommate.getOptions(PC.story);
                if (canSendRoomate)
                {
					string[] options = roommate.getOptions(PC.story);
                    if (GUI.Button(new Rect(300, 350, 250, 25), options[0]))
                    {
                        PC.vc.send(options[0]);
                        showTexts = false;
                        canSendRoomate = false;
                    }
                    else if (GUI.Button(new Rect(750, 350, 250, 25), options[1]))
                    {
                        PC.vc.send(options[1]);
                        showTexts = false;
                        canSendRoomate = false;
                    }
                    else if (GUI.Button(new Rect(525, 300, 250, 25), options[2]))
                    {
                        PC.vc.send(options[2]);
                        showTexts = false;
                        canSendRoomate = false;
                    }
                    else if (GUI.Button(new Rect(525, 400, 250, 25), options[3]))
                    {
                        PC.vc.send(options[3]);
                        showTexts = false;
                        canSendRoomate = false;
                    }
                }
                else
                {
                    GUI.Box(new Rect(500, 400, 300, 100), "You have nothing to say at this time.");
                }
            }
            else if (PC.contactNum == 3)
            {
                //string[] options = roommate.getOptions(PC.story);
                if (canSendParents)
                {
                    string[] options = girlfriendsParents.getOptions(PC.story);
                    if (GUI.Button(new Rect(300, 350, 250, 25), options[0]))
                    {
                        PC.vc.send(options[0]);
                        showTexts = false;
                        canSendParents = false;
                    }
                    else if (GUI.Button(new Rect(750, 350, 250, 25), options[1]))
                    {
                        PC.vc.send(options[1]);
                        showTexts = false;
                        canSendParents = false;
                    }
                    else if (GUI.Button(new Rect(525, 300, 250, 25), options[2]))
                    {
                        PC.vc.send(options[2]);
                        showTexts = false;
                        canSendParents = false;
                    }
                    else if (GUI.Button(new Rect(525, 400, 250, 25), options[3]))
                    {
                        PC.vc.send(options[3]);
                        showTexts = false;
                        canSendParents = false;
                    }
                }
                else
                {
                    GUI.Box(new Rect(500, 400, 300, 100), "You have nothing to say at this time.");
                }
            }
            else if (PC.contactNum == 4)
            {
                //string[] options = roommate.getOptions(PC.story);
                if (canSendBoss)
                {
                    string[] options = boss.getOptions(PC.story);
                    if (GUI.Button(new Rect(300, 350, 250, 25), options[0]))
                    {
                        PC.vc.send(options[0]);
                        showTexts = false;
                        canSendBoss = false;
                    }
                    else if (GUI.Button(new Rect(750, 350, 250, 25), options[1]))
                    {
                        PC.vc.send(options[1]);
                        showTexts = false;
                        canSendBoss = false;
                    }
                    else if (GUI.Button(new Rect(525, 300, 250, 25), options[2]))
                    {
                        PC.vc.send(options[2]);
                        showTexts = false;
                        canSendBoss = false;
                    }
                    else if (GUI.Button(new Rect(525, 400, 250, 25), options[3]))
                    {
                        PC.vc.send(options[3]);
                        showTexts = false;
                        canSendBoss = false;
                    }
                }
                else
                {
                    GUI.Box(new Rect(500, 400, 300, 100), "You have nothing to say at this time.");
                }
            }
            else if (PC.contactNum == 5)
            {
                //string[] options = roommate.getOptions(PC.story);
                if (canSendClassmate)
                {
                    string[] options = tomClassmate.getOptions(PC.story);
                    if (GUI.Button(new Rect(300, 350, 250, 25), options[0]))
                    {
                        PC.vc.send(options[0]);
                        showTexts = false;
                        canSendClassmate = false;
                    }
                    else if (GUI.Button(new Rect(750, 350, 250, 25), options[1]))
                    {
                        PC.vc.send(options[1]);
                        showTexts = false;
                        canSendClassmate = false;
                    }
                    else if (GUI.Button(new Rect(525, 300, 250, 25), options[2]))
                    {
                        PC.vc.send(options[2]);
                        showTexts = false;
                        canSendClassmate = false;
                    }
                    else if (GUI.Button(new Rect(525, 400, 250, 25), options[3]))
                    {
                        PC.vc.send(options[3]);
                        showTexts = false;
                        canSendClassmate = false;
                    }
                }
                else
                {
                    GUI.Box(new Rect(500, 400, 300, 100), "You have nothing to say at this time.");
                }
            }
        }
    }
}
