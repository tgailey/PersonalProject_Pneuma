using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
public class Testing : MonoBehaviour {
	/*
	 * This class handles the interaction we can make with the player.
	 * This class determines what should be showing on the GUI at the time of interaction, as well as turn off player controls
	 * When we are not interacting, this class is sending out raycasts in front of the player and seeing if what we hit can be interacted upon.
	 * */

    public GameObject target;
	Animator anim;
    GameObject player;
    public GUISkin dialogSkin, customSkin, journalSkin;
    public bool interacting = false;
    FirstPersonController FPC;
    CharacterController CC;
    PlayerControllerMBTI PCMBTI;
    public int story; //Determines what the dialogue/journal will say based on the part of the story we are at.
    int clicks; //Determines what dialogue will show based on the amount of times people have left clicked.
    int step = 0;
    public bool journal = false, talk = false; //Whether or not the thing we are interacting with is a journal or a person.
    string speaker; //String for whoever's talking
    string speech; //String for what is being said
	string choiceSpeech = "Select your speech"; //String for the text that is shown when making a choice.
    string[] options = new string[5]; //The options that are shown when we make a choice
	bool[] toggleOptions = new bool[5]; //The booleans that can be turned off/on when selecting multiple choices 
	bool canMoveDialogue = false; //Tests if we can move the dialogue or not. When true, left clicking will move counter up.
	bool choice = false, choiceToggle = false; //Determines what kind of dialogue will be displayed. A choice where you select one, a choice where you select many, or none (if they are both false)
	int choose = 0; //What choice we selected for the options in dialogue
	bool run = true; //A boolean that allows us to run through the list of choices to determine how long it is. 
	int y = 190; //How big the choice box is vertically. (VALUE ADDED TO BASED ON THE AMOUNT OF CHOICES WE HAVE AVAILABLE)
	int amountOfChoices = 0, amt = 0, amt2 = 0, amt3 = 0;

    GameObject selected; //GameObject for the object that we selected to interact upon.
    GameObject projects; 

	bool[] none = new bool[2];

	//These bool arrays have all the booleans that test what options we need. Each boolean represents an option that can selected in game.
	//The true FI Ones will increase player FI, FE-FE, and so on.
    bool[] FEOne = new bool[9]; 
    bool[] FIOne = new bool[9];
    bool[] IOne = new bool[7];
    bool[] EOne = new bool[7];

	//These bools are just for obvious things that the player of the game needs to do.
	//For example, if the text says to select 3 things, and the player only selects two, then we would set one of these to true.
    bool obvious = false, obvious2 = false, obvious3 = false, obvious4 = false;

	//Bools for carrer choices and the strings that match up with them 
    bool[] careerChoices = new bool[15];
    string[] careers = new string[15] { "Artist", "Scientist", "Actor", "Engineer", "Musician", "Lawyer", "Counselor", "Entrepreneur", "Teacher", "Manager", "Psychologist", "Computer Programmer / Analyst", "Clergy", "Child Care", "Medical Doctor"};
    
	//The levels of FE, FI, TE, TI, etc that the player has. Default begins them all at zero.
	public float FE = 0, FI = 0, TE = 0, TI = 0, I = 0, E = 0, F = 0, T = 0, NE = 0, NI = 0, S = 0, N = 0;
    
    // string firstFunction, secondFunction, thirdFunction, forthFunction;
    //bool FEbool, TEbool, Ibool;

    bool help = false; //Whether or not we choice to help your roommate.
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        FPC =player.GetComponent<FirstPersonController>();
        CC = player.GetComponent<CharacterController>();
        PCMBTI = player.GetComponent<PlayerControllerMBTI>();
        story = 0;
        clicks = -1;
        speaker = "";

        if (!GameObject.ReferenceEquals(GameObject.Find("Work/Scenario3/Projects"), null))
        {
            projects = GameObject.Find("Work/Scenario3/Projects");
            projects.SetActive(false);
        }
	}

    // Update is called once per frame
    RaycastHit hit;
    void Update () {
		if (PCMBTI.clicks == 18) {
			anim = GameObject.Find ("Apartment/Scenario1/Part1/Girlfriend").GetComponent<Animator> ();
		}
        else if (PCMBTI.clicks == 34)
        {
            anim = GameObject.Find("Work/Scenario5/Stefani").GetComponent<Animator>();
        }


		/*In the update, first we check if we are interacting with something.
		  If we're not we will constantly be sending out raycasts in front of us looking for something to interact with
		  If the thing in front of us can be interacted upon, display the text of the interacting object so that the played knows how to interact upon it.*/
        if (interacting)
        {
			//If we are interacting, we check what we are interacting with to see if we need dialogue or the journal.
			if (!GameObject.ReferenceEquals (selected, null) && PCMBTI.clicks != 49) {
				if (selected.tag == "Talk")
				{
					talk = true;
					//doChairThings();
				}
				else if (selected.tag == "Journal")
				{
					//doJournalThings();
					journal = true;
				}
			}
           	//If we can move the dialogue, left clicking will move it.
            if (Input.GetButtonDown("Fire1") && canMoveDialogue)
            {
                clicks++;
            }
			//If we are talking, call this function. 
			if (talk) {
				doChairThings ();
			}
        }
        else if (Input.GetKeyDown(KeyCode.E) && target && target.GetComponent<Interactable>())
        {
			//If we interact with something that is interactable (we know because it has the Interactable script attached to it), do this

			//Set selected to the given target we are interacting with
            selected = target;
            interacting = true;
            story = selected.GetComponent<Interactable>().storySet; //Set story to int based off what the script attached to the GameObject says
            PCMBTI.canMove = false; //Disable movement
            CC.enabled = false; //Disable collider
        }

        else if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 6))
        {
            //FPC.enabled = true;
            CC.enabled = true;
               if (hit.collider.gameObject.tag != "Player")
                {
                    target = hit.collider.gameObject;
                    //Debug.Log("targetSet");
                }
        }
        else {
            //FPC.enabled = true;
            CC.enabled = true;
            target = null;
        }
      if (PCMBTI.clicks == 38)
        {
            GameObject.Find("Girlfriend").GetComponentInChildren<Interactable>().enabled = true;
        }
        if (talk || (PCMBTI.clicks == 49 || PCMBTI.clicks == 50))
        {
            interacting = true;
        }
    }
    public void doChairThings()
    {
		//Set text to what it needs to be based off place in story, and amount of clicks on the counter
        if (story == 0)
        {
            if (PCMBTI.clicks == 18 || PCMBTI.clicks == 19)
            {
                Vector3 temp = Vector3.zero;
                Vector3 temp2 = target.transform.position;
                temp2.y += .15f;
                player.transform.position = temp2;
				temp.y = 90;
                player.transform.localEulerAngles = temp;
                Vector3 temp3 = Vector3.zero;
                temp3.x = 0;
                Camera.main.transform.localEulerAngles = temp3;


                if (clicks == -1)
                {
                    FPC.enabled = false;
                    PCMBTI.clicks = 19;
                    clicks++;
                    canMoveDialogue = true;
                }
                else if (clicks == 0)
                {
                    //PCMBTI.clicks = 19;
                    //if (!Animator.ReferenceEquals(anim, null)) {
                    anim.SetBool("isTalking", true);
                    //}
                    speaker = "Me";
                    speech = "Sorry I'm so late!\r\n<Left Click to Advance Text>";
                }
                else if (clicks == 1)
                {
                    speaker = "Girlfriend";
                    speech = "It's fine. How are you?";
                }
                else if (clicks == 2)
                {
                    speech = "Did you like any of the rings we saw today?";
                }
                else if (clicks == 3)
                {
                    canMoveDialogue = false;
                    options[0] = "Yeah, I saw a couple of them that I liked";
                    options[1] = "None of them compared to your beauty";
                    options[2] = "I definitely think we should keep looking, I haven't gotten that WOW feeling yet";
                    options[3] = "Like I'd tell you! I gotta keep it a surprise you know?";
                    options[4] = "";
                    speaker = "Me";
                    choice = true;
                }
                else if (clicks == 4)
                {
                    canMoveDialogue = true;
                    speaker = "Girlfriend";
                    speech = "When are you going to ask me?";
                    choice = false;
                }
                else if (clicks == 5)
                {
                    canMoveDialogue = false;
                    speaker = "Me";
                    choice = true;
                    options[0] = "You know, soon.";
                    options[1] = "As soon as I can come up with the best way of asking you.";
                    options[2] = "I just haven't been able to come up with any ideas yet.";
                    options[3] = "Helllllo? Surprise?";
                    options[4] = "";
                }
                else if (clicks == 6)
                {
                    canMoveDialogue = true;
                    speaker = "Girlfriend";
                    choice = false;
                    if (choose == 0)
                    {
                        speech = "Keeping it vague, huh? That's fine.";
                    }
                    else if (choose == 1)
                    {
                        speech = "That's sweet. Any way will be perfect if you're the one asking me.";
                    }
                    else if (choose == 2)
                    {
                        speech = "If you take too long, maybe I'll be the one to ask you.";
                    }
                    else {
                        speech = "Haaa. Fine, I'll leave it alone. Just don't keep me waiting for too long, I can only hold off my multiple suitors for a little longer.";
                    }
                }
                else if (clicks == 7)
                {
                    speaker = "Me";
                    speech = "I love you. Cheers to a long and happy life together.";
                }
                else if (clicks == 8)
                {
                    speaker = "Girlfriend";
                    speech = "I love you too. And I'll toast to that any day.";
                }
                else if (clicks == 9)
                {
                    if (!Animator.ReferenceEquals(anim, null))
                    {
                        anim.SetBool("isTalking", false);
                        anim.SetBool("standingUp", true);
                    }
                    speech = "I think it's time we get to bed. You ready?";
                }
                else if (clicks == 10)
                {
                    interacting = false;
                    talk = false;
                    clicks = -1;
                    PCMBTI.canMove = true;
                    canMoveDialogue = false;
                    PCMBTI.clicks++;
                    FPC.enabled = true;
                }
            }
            else
            {
                interacting = false;
                talk = false;
                clicks = -1;
                PCMBTI.canMove = true;
            }


        }
        else if (story == 1)
        {
            if (PCMBTI.clicks == 25 || PCMBTI.clicks == 26)
            {
                if (clicks == -1)
                {
                    FPC.enabled = false;
                    PCMBTI.clicks = 26;
                    clicks++;
                }
                else if (clicks == 0)
                {
                    //PCMBTI.clicks = 25;
                    canMoveDialogue = true;
                    speaker = "Roommate";
                    speech = "Hey, I was wondering about something, can you lend me an ear?";
                    if (!Animator.Equals(selected.gameObject.GetComponent<Animator>(), null))
                    {
                        selected.gameObject.GetComponent<Animator>().SetBool("isTalking", true);
                    }
                }
                else if (clicks == 1)
                {
                    speaker = "Me";
                    speech = "Yeah man, sure, what is it?";
                }
                else if (clicks == 2)
                {
                    speaker = "Roommate";
                    speech = "You know Mr. Schmidt's biology class we have together, right? You know about that take home test he gave us?";
                }
                else if (clicks == 3)
                {
                    speaker = "Me";
                    speech = "Yeah, it's open book. It should be really easy.";
                }
                else if (clicks == 4)
                {
                    speaker = "Roommate";
                    speech = "Well, I've really been struggling in his class. I don't know if I can pass this exam. I know he said not to get help from others but I really need it.";
                }
                else if (clicks == 5)
                {
                    speech = "I know that we aren't particularly close or anything, but I know you have a good grade in the class.  Could you help me?";
                }
                else if (clicks == 6)
                {
                    speaker = "Me";
                    speech = "You've never asked for my help before, you know.";
                }
                else if (clicks == 7)
                {
                    speaker = "Roommate";
                    speech = "I know. I just didn't really think much on it. But I can't do this without you! With it being worth 20 percent of the grade, this is the difference between passing and failing!";
                }
                else if (clicks == 8)
                {
                    speech = "I've worked on it all weekend and I don't get any of it, and I'm starting to freak out! Please?";
                }
                else if (clicks == 9)
                {
                    choice = true;
                    canMoveDialogue = false;
                    choiceSpeech = "Do you help your roommate?\r\n*NOTE* You are aware that you have no possibility of getting caught by your professor.";
                    options[0] = "I'll help you.";
                    options[1] = "I won't help you.";
                    options[2] = "";
                    options[3] = "";
                    options[4] = "";
                }
                else if (clicks == 10)
                {
                    //choiceToggle = true;
                    //choice = false;
                    if (choose == 0)
                    {
                        help = true;
                    }
                    else
                    {
                        help = false;
                    }
                    choiceSpeech = "Why? <Select Most Applicable>";
                    //choiceSpeech = "Why? <Select All That Apply>";
                    if (help)
                    {

                        options[0] = "It's the right thing to do. I personally believe helping people is more important than rules.";
                        options[1] = "I have difficulty ignoring people who need help/I don't like to say no to people.";
                        options[2] = "While I don't like cheating, I want to help my roommate not fail the class, it's a big deal.";
                        options[3] = "I want to, simple as that.";
                        options[4] = "I love helping people/I always help someone in need/I like the way it makes me feel";
                        //options[1] = 
                    }
                    else
                    {

                        options[0] = "Principle - I'm not a cheater.";
                        options[1] = "I don't want to, simple as that.";
                        options[2] = "I wouldn't help him as this actually hurts him in the future if he never learns to study.";
                        options[3] = "While I would want to help him really badly, it goes against my values and sense of justice.";
                        options[4] = "Because I respect the professor and the rules that he set forth.";
                    }
                }
                else if (clicks == 11)
                {
                    choice = false;
                    canMoveDialogue = true;
                    speaker = "Roommate";
                    if (help)
                    {
                        speech = "Thank you so much! I don't deserve your kindness.";
                    }
                    else
                    {
                        speech = "I understand,  I should have paid more attention during class, it's my fault. I deserve this.";
                    }
                }
                else if (clicks == 12)
                {
                    /*interacting = false;
                    talk = false;
                    if (!Animator.ReferenceEquals(selected.gameObject.GetComponent<Animator>(), null)) {
                        selected.gameObject.GetComponent<Animator>().SetBool("isTalking", true);
                    }
                    Debug.Log("The amount of FE you have is: " + FE);
                    Debug.Log("The amount of FI you have is: " + FI);
                    clicks = -1;
                    PCMBTI.canMove = true;*/
                    speaker = "Me";
                    if (help)
                    {
                        speech = "Alright man I hope that helps, I need to go to work now.";
                    }
                    else
                    {
                        speech = "I'm real sorry man, I wish you luck. I'm off to work.";
                    }
                }
                else if (clicks == 13)
                {
                    if (help)
                    {
                        if (choose == 0)
                        {
                            FI += 4;
                        }
                        else if (choose == 1)
                        {
                            FE += 4;
                        }
                        else if (choose == 2)
                        {
                            FE += 4;
                        }
                        else if (choose == 3)
                        {
                            FI += 4;
                        }
                        else if (choose == 4)
                        {
                            FE += 4;
                        }


                    }
                    else {
                        if (choose == 0)
                        {
                            FI += 4;
                        }
                        else if (choose == 1)
                        {
                            FI += 4;
                        }
                        else if (choose == 2)
                        {
                            FE += 4;
                        }
                        else if (choose == 3)
                        {
                            FI += 4;
                        }
                        else if (choose == 4)
                        {
                            FE += 4;
                        }
                    }
                    interacting = false;
                    talk = false;
                    if (!Animator.ReferenceEquals(selected.gameObject.GetComponent<Animator>(), null))
                    {
                        selected.gameObject.GetComponent<Animator>().SetBool("isTalking", false);
                    }
                    Debug.Log("The amount of FE you have is: " + FE);
                    Debug.Log("The amount of FI you have is: " + FI);
                    clicks = -1;
                    PCMBTI.canMove = true;
                    canMoveDialogue = false;
                    PCMBTI.clicks++;
                    FPC.enabled = true;
                }
            }
            else
            {
                PCMBTI.canMove = true;
                canMoveDialogue = false;
                clicks = -1;
                interacting = false;
                talk = false;
            }
        }
        else if (story == 2)
        {
            if (PCMBTI.clicks == 30 || PCMBTI.clicks == 31)
            {
                Vector3 temp = Vector3.zero;
                Vector3 temp2 = target.transform.position;
                temp2.y += .75f;
                player.transform.position = temp2;
                temp.y = -90;
                player.transform.localEulerAngles = temp;
                Vector3 temp3 = Vector3.zero;
                temp3.x = 0;
                Camera.main.transform.localEulerAngles = temp3;
                if (clicks == -1)
                {
                    FPC.enabled = false;
                    PCMBTI.clicks = 31;
                    clicks++;
                }
                else if (clicks == 0)
                {
                    //PCMBTI.clicks = 30;
                    canMoveDialogue = true;
                    speaker = "Boss";
                    speech = "Sorry to bother you, but I have a job for you.";
                }
                else if (clicks == 1)
                {
                    speech = "You've done well with this company, and I'm going to entrust you with an important project for the company.";
                }
                else if (clicks == 2)
                {
                    speech = "I have two major projects that need to get done, and I am giving you the choice between the two.";
                }
                else if (clicks == 3)
                {
                    speech = "I'm only going to go over this once so make sure to pay close attention.";
                }
                else if (clicks == 4)
                {
                    speech = "The first project is a broad and expansive project covering multiple areas of company operations. It will have a significant effect on our company.";
                }
                else if (clicks == 5)
                {
                    speech = "However this would require a large collective effort and an extensive amount of group work to be done where you would be logically thinking through the project together.";
                }
                else if (clicks == 6)
                {
                    speech = "The second project has a much more specific and narrow focus. This project will require a significant amount of in depth individual analysis to work through the problem.";
                }
                else if (clicks == 7)
                {
                    speech = "You would be working alone and the completion of the project may or may not have much impact on company operations.";
                }
                else if (clicks == 8)
                {
                    speech = "However, after complete, the process and problem you were working on will be fundamentally understood.";
                }
                else if (clicks == 9)
                {
                    speech = "You can reread the overview of these projects by reading the paper here on my desk.";
                }
                else if (clicks == 10)
                {
                    speech = "Sit down in the chair again to talk to me when you are ready to make a decision.";
                }
                else if (clicks == 11)
                {
                    clicks = -1;
                    PCMBTI.canMove = true;
                    canMoveDialogue = false;
                    interacting = false;
                    talk = false;
                    GameObject.Find("Work/Scenario3/Sit Down (1)").GetComponent<Interactable>().storySet = 3;
                    projects.SetActive(true);
                    PCMBTI.clicks++;
                    FPC.enabled = true;
                }
            }
            else
            {
                interacting = false;
                talk = false;
                clicks = -1;
                PCMBTI.canMove = true;
                canMoveDialogue = false;
                //PCMBTI.clicks = 33;
            }
        }
        else if (story == 3)
        {
            //Debug.Log ("The amount of clicks is: " + clicks);
            if (PCMBTI.clicks == 32 || PCMBTI.clicks == 33)
            {
                Vector3 temp = Vector3.zero;
                Vector3 temp2 = target.transform.position;
                temp2.y += .75f;
                temp.y = -90;
                player.transform.position = temp2;
                player.transform.localEulerAngles = temp;
                Vector3 temp3 = Vector3.zero;
                temp3.x = 0;
                Camera.main.transform.localEulerAngles = temp3;

                if (clicks == -1)
                {
                    FPC.enabled = false;
                    PCMBTI.clicks = 33;
                    clicks++;
                }
                else if (clicks == 0)
                {
                    canMoveDialogue = true;
                    //PCMBTI.clicks = 32;
                    speaker = "Boss";
                    speech = "Have you made your decision?";
                }
                else if (clicks == 1)
                {
                    canMoveDialogue = false;
                    choice = true;
                    choiceSpeech = "Have you made your decision?";
                    options[0] = "Yes";
                    options[1] = "No";
                    options[2] = "";
                    options[3] = "";
                    options[4] = "";
                }
                else if (clicks == 2)
                {
                    if (choose == 1)
                    {
                        clicks = -1;
                        interacting = false;
                        talk = false;
                        PCMBTI.canMove = true;
                        canMoveDialogue = false;
                        choice = false;
						FPC.enabled = true;
                    }
                    else {
						choice = true;
                        choiceSpeech = "What project do you choose?";
                        options[0] = "Project 1";
                        options[1] = "Project 2";
                    }
                }
                else if (clicks == 3)
                {
                    choiceSpeech = "Why? <Select those that apply THE MOST>";
                    choice = false;
                    choiceToggle = true;
                    if (choose == 1)
                    {
                        options[0] = "I don't like working/being with people.";
                        options[1] = "The expansive nature of the project is intimadating.";
                        options[2] = "Past experience with group members not pulling their weight.";
                        options[3] = "I love digging into problems and being left alone to think.";
                        options[4] = "I like the idea of digging into something to fundamentally understand it.";
                    }
                    else {
                        options[0] = "I like working/being with people.";
                        options[1] = "I want my work to matter and effect the company in a positive way.";
                        options[2] = "I enjoy working through problems with a lot of outside influence. I like collaborating a lot of ideas together.";
                        options[3] = "The socialization aspect and group decision making sounds fun.";
                        options[4] = "I enjoy being part of a group of people who are all tossing in ideas and thoughts to find what's the best way of doing something.";
                    }
                }
                else if (clicks == 4)
                {
                    choice = false;
                    choiceToggle = false;
                    canMoveDialogue = true;
                    if (choose == 1)
                    {
                        if (toggleOptions[0])
                        {
                            I += 3;
                        }
                        if (toggleOptions[1])
                        {
                            //TE += 2;
                        }
                        if (toggleOptions[2])
                        {

                        }
                        if (toggleOptions[3])
                        {
                            TI += 4;
                        }
                        if (toggleOptions[4])
                        {
                            TI += 4;
                        }
                        TI += 2;
                    }
                    else {
                        if (toggleOptions[0])
                        {
                            E += 3;
                        }
                        if (toggleOptions[1])
                        {
                            TE += 4;
                        }
                        if (toggleOptions[2])
                        {
                            TE += 4;
                        }
                        if (toggleOptions[3])
                        {
                            //TI += 2;
                            E += 1.5f;
                        }
                        if (toggleOptions[4])
                        {
                            TE += 2;
                        }
                        TE += 2;
                    }
                    for (int i = 0; i < toggleOptions.Length; i++)
                    {
                        toggleOptions[i] = false;
                    }
                    clicks++;
                }
                else if (clicks == 5)
                {
                    speaker = "Boss";
                    speech = "Okay then. Thank you for your time. We will get this info for this project to you sometime in the near future.";
                }
                else if (clicks == 6)
                {
                    speaker = "Boss";
                    speech = "Oh and by the way, the Human Resources lady wanted to talk to you, something about career counseling. She's in her office all the way to the right.";
                }
                else if (clicks == 7)
                {
                    interacting = false;
                    talk = false;
                    Debug.Log("The amount of TE you have is: " + TE);
                    Debug.Log("The amount of TI you have is: " + TI);
                    Debug.Log("The amount of E you have is: " + E);
                    Debug.Log("The amount of I you have is: " + I);
                    clicks = -1;
                    PCMBTI.canMove = true;
                    canMoveDialogue = false;
                    PCMBTI.clicks++;
                    FPC.enabled = true;

                }
            }
            else
            {
                interacting = false;
                talk = false;
                clicks = -1;
                PCMBTI.canMove = true;
                canMoveDialogue = false;
                //PCMBTI.clicks = 33;
            }
        }
        else if (story == 4)
        {
            //Debug.Log ("The amount of clicks is: " + clicks);
            if (PCMBTI.clicks == 34 || PCMBTI.clicks == 35)
            {
                Vector3 temp = Vector3.zero;
                Vector3 temp2 = target.transform.position;
                temp2.y += .75f;
                player.transform.position = temp2;
                temp.y = -90;
                player.transform.localEulerAngles = temp;
                Vector3 temp3 = Vector3.zero;
                temp3.x = 0;
                Camera.main.transform.localEulerAngles = temp3;
                PCMBTI.canMove = false;
                if (clicks == -1)
                {
                    FPC.enabled = false;
                    PCMBTI.clicks = 35;
                    clicks++;
                    anim.SetBool("isTalking", true);
                }
                else if (clicks == 0)
                {
                    canMoveDialogue = true;
                    //PCMBTI.clicks = 34;
                    speaker = "Human Resources";
                    speech = "Hello! Thank you for joining me here today.";
                }
                else if (clicks == 1)
                {
                    speech = "We offer this program to young employees with promise. It's a simple yet effective test for your future career. We want to see where you fit in our company.";
                }
                else if (clicks == 2)
                {
                    speech = "I'm going to give you a list of careers, and I want you to pick your top 3.";
                }
                else if (clicks == 3)
                {
                    speech = "However, I want you to take money out of the equation. Please imagine that all these careers received equal compensation.";
                }
                else if (clicks == 4)
                {
                    speech = "Focus instead on where you would truly feel most happy and fulfilled.";
                }
                else if (clicks == 5)
                {
                    talk = false;
                    journal = true;
                    canMoveDialogue = false;
                    //clicks++;
                }
                else if (clicks == 6)
                {
                    choiceSpeech = "Why these careers <Select those that apply>";
                    choiceToggle = true;
                    options[0] = "I want my career to be thought-provoking";
                    options[1] = "I like doing things that pertain to analysis and logical thought";
                    options[2] = "I want to express myself.";
                    options[3] = "I want to personally touch people's lives with my work.";
                    options[4] = "I want the things I do to matter.";
                }
                else if (clicks == 7)
                {
                    canMoveDialogue = true;
                    choiceToggle = false;
                    speaker = "Human Resources";
                    speech = "With that, you're free to leave. And it looks like your shift is done. We'll see you tomorrow.";
                    anim.SetBool("isTalking", false);

                }
                else if (clicks == 8)
                {
                    if (toggleOptions[0])
                    {
                        T += 3;
                    }
                    if (toggleOptions[1])
                    {
                        T += 3;
                    }
                    if (toggleOptions[2])
                    {
                        F += 3;
                    }
                    if (toggleOptions[3])
                    {
                        F += 3;
                    }
                    if (toggleOptions[4])
                    {
                        F += 2;
                        T += 1f;
                    }
                    interacting = false;
                    talk = false;
                    Debug.Log("The amount of T you have is: " + T);
                    Debug.Log("The amount of F you have is: " + F);
                    clicks = -1;
                    PCMBTI.canMove = true;
                    canMoveDialogue = false;
                    PCMBTI.clicks++;
                    FPC.enabled = true;
                }
            }
            else
            {
                interacting = false;
                talk = false;
                clicks = -1;
                PCMBTI.canMove = true;
                canMoveDialogue = false;
                //PCMBTI.clicks = 33;
            }
        }
        else if (story == 5)
        {
            //Debug.Log ("The amount of clicks is: " + clicks);
            if (PCMBTI.clicks == 39 || PCMBTI.clicks == 40)
            {
                /*Vector3 temp = Vector3.zero;
                Vector3 temp2 = target.transform.position;
                temp2.y += .75f;
                player.transform.position = temp2;
                player.transform.localEulerAngles = temp;
                Vector3 temp3 = Vector3.zero;
                temp3.x = 0;
                Camera.main.transform.localEulerAngles = temp3;*/
                
                if (clicks == -1)
                {
                    FPC.enabled = false;
                    //PCMBTI.canMove = false;
                    PCMBTI.clicks = 40;
                    clicks++;
                }
                else if (clicks == 0)
                {
                    canMoveDialogue = true;
                    //PCMBTI.clicks = 39;
                    speaker = "Girlfriend";
                    speech = "Hi! Are you free? We could go do something.";
                }
                else if (clicks == 1)
                {
                    speaker = "Me";
                    speech = "Unfortunately not, I have a group project from school to do.";
                }
                else if (clicks == 2)
                {
                    speaker = "Girlfriend";
                    speech = "That sucks. Are you interested in the topic?";
                }
                else if (clicks == 3)
                {
                    speaker = "Me";
                    speech = "Yeah, it's pretty interesting.";
                }
                else if (clicks == 4)
                {
                    speaker = "Girlfriend";
                    speech = "Do you know and like the people you are working with?";
                }
                else if (clicks == 5)
                {
                    speaker = "Me";
					speech = "Yeah, all three of them want to make it as good as it can be. ";
                }
                else if (clicks == 6)
                {
                    speaker = "Girlfriend";
                    speech = "Well that's good at least. Good luck!";
                }
                else if (clicks == 7) {
                    interacting = false;
                    talk = false;
                    clicks = -1;
                    PCMBTI.canMove = true;
                    FPC.enabled = true;
                    canMoveDialogue = false;
                    PCMBTI.clicks++;
                }
            }
            else
            {
                interacting = false;
                talk = false;
                clicks = -1;
                PCMBTI.canMove = true;
                canMoveDialogue = false;
                //PCMBTI.clicks = 33;
            }
        }
        else if (story == 6)
        {
            if (PCMBTI.clicks == 41 || PCMBTI.clicks == 42)
            {
                Vector3 temp3 = Vector3.zero;
                Camera.main.transform.localEulerAngles = temp3;
                player.transform.LookAt(GameObject.Find("Scenario4/Classmates/Shae").transform);
                PCMBTI.canMove = false;
                if (clicks == -1)
                {
                    FPC.enabled = false;
                    PCMBTI.clicks = 42;
                    clicks++;
                    canMoveDialogue = true;
                }
                else if (clicks == 0)
                {
                    //PCMBTI.clicks = 41;
                    
                    speaker = "Me";
                    speech = "Alright so we are here to come up with ideas for the project.";
                }
                else if (clicks == 1)
                {
                    speech = "Does anybody have any ideas on the direction you want to take this?";
                }
                else if (clicks == 2) {
                    speaker = "Classmate";
                    speech = "Oh I do!!! I think we should....";
                }
                else if (clicks == 3) {
                    choice = true;
                    canMoveDialogue = false;
                    choiceSpeech = "As you are listening, are you more likely to...?";
                    options[0] = "Actively share my own ideas and concerns on this project and the direction we are going to take it.";
                    options[1] = "Silently observe and bring together everyone's ideas in my head.";
                    options[2] = "";
                    options[3] = "";
                    options[4] = "";

                }
                else if (clicks == 4) {
                    if (choose == 0) {
                        NE += 6;
                    }
                    else {
                        NI += 6;
                    }
                    clicks++;
                }
                else if (clicks == 5) {
                    choiceSpeech = "As this meeting is going on, what is your primary concern?";
                    options[0] = "Thinking about the ideas and the many possibilities and directions the project may have.";
                    options[1] = "Thinking about the practical realities of the project. i.e. Thinking about the time, money, resources, and ability to do it.";
                    options[2] = "";
                    options[3] = "";
                    options[4] = "";
                
                }
                else if (clicks == 6) {
                   if (choose == 0) {
                        N += 4;
                    }
                    else {
                        S += 4;
                    }
                    clicks++;
                }
                else if (clicks == 7) {
                    choiceSpeech = "Would you rather....";
                    options[0] = "Be carefully carrying out the plans of the project with precision.";
                    options[1] = "Generate possibilities, plans, and ideas for how to carry out the ideas they came up with today.";
                    options[2] = "";
                    options[3] = "";
                    options[4] = "";
                }
                else if (clicks == 8) {
                   if (choose == 0) {
                        S += 4;
                    }
                    else {
                        N += 4;
                    }
                    clicks++;
                }
                else if (clicks == 9) {
                    choice = false;
                    canMoveDialogue = true;
                    speaker = "Internal Mind";
                    speech = "Together, you and the group come up with two ideas. One is very exciting to you, but would require a risk in learning new skills and experimenting with the project.";
                }
                else if (clicks == 10)
                {
                    speech = "The other is practical and reliable, but much less interesting to you.";
                }
                else if (clicks == 11) {
                    canMoveDialogue = false;
                    choice = true;
                    choiceSpeech = "What idea do you choose?";
                    options[0] = "The imaginative, experimental one.";
                    options[1] = "The practical and reliable one.";
                    options[2] = "";
                    options[3] = "";
                    options[4] = "";
                }
               else if (clicks == 12) {
                   if (choose == 0) {
                        N += 2.5f;
                    }
                    else {
                        S += 2.5f;
                    }
                    clicks++;
                }
                else if (clicks == 13) {
                    choice = false;
                    canMoveDialogue = true;
                    speaker = "Me";
                    speech = "It looks like we are done. We got a lot of ideas you guys, good job today. See you guys soon.";

                }
                else if (clicks == 14) {
                    speaker = "Classmates";
                    speech = "Later!";
                }
                else if (clicks == 15) {
                    interacting = false;
                    talk = false;
                    clicks = -1;
                    PCMBTI.canMove = true;
                    canMoveDialogue = false;
                    PCMBTI.clicks++;
                    FPC.enabled = true;
                }
                
            }
            else
            {
                interacting = false;
                talk = false;
                clicks = -1;
                PCMBTI.canMove = true;
                canMoveDialogue = false;
                //PCMBTI.clicks = 33;
            }
        }
        else if (story == 7)
        {
			//Debug.Log ("Don't know if it's working");
            if (clicks == -1)
            {
                FPC.enabled = false;
                //PCMBTI.clicks = 42;
                clicks++;
                canMoveDialogue = true;
            }
            else if (clicks == 0)
            {
                //choiceSpeech = "What kind of tasks do you prefer?";
                //options[0]
                speaker = "Narrator";
                speech = "As you are about to do these things, you notice that your girlfriend is no where to be seen. You wonder why she left.";
            }
            else if (clicks == 1)
            {
                speech = "As you question, you notice a note on the dresser. You should read it.";
            } 
            else if (clicks == 2)
            {
                interacting = false;
                talk = false;
                clicks = -1;
                PCMBTI.canMove = true;
                canMoveDialogue = false;
                PCMBTI.clicks++;
                FPC.enabled = true;
            }
        }
            }
    public void doJournalThings()
    {
        if (story == 0)
        {
            if (step == 0)
            {

            }
        }
    }
    void OnGUI()
    {
        GUI.skin = dialogSkin;
        if (interacting)
        {

            float rX, rY;
            float scale_width, scale_height;
            scale_width = 1316;
            scale_height = 740;
            rX = Screen.width / scale_width;
            rY = Screen.height / scale_height;
            GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rX, rY, 1));

			//If we are talking, show text based on what we set on the doChairThings function
            if (talk)
            {
                GUI.skin = customSkin;
                // if (story == 0)
                //{
				//If we are not making a choice of anykind, simply display text based on speaker and dialogue
                if (!choice && !choiceToggle)
                {
                    GUI.Box(new Rect(200, 560, 1316 - 400, 140), speech, "Dialog");
                    GUI.Box(new Rect(250, 520, 200, 60), speaker, "Speaker");
                }
				//If making a normal choice (one selection)
                else if (choice)
                {
					//Run through this to determine length of box.
					//Box length will be determined by the amount of options
					if (run && PCMBTI.clicks != 42) {
						foreach (string s in options) {
							if (!string.Equals (s, "")) {
								y += 100;
							}
							amountOfChoices++;
							if (amountOfChoices == options.Length) {
								run = false;
							}
						}
					} else if (PCMBTI.clicks == 42) {
						y = 390;
					}

					//display choice box (outline) with speech set in doChairThings
                    GUI.Box(new Rect(200, 50, 1316 - 400, y), choiceSpeech, "ChoiceBox");
					//Buttons all move click forward, reset y, and set choose to the option selected. This means we can test what is selected based on what choose equals
                    if (GUI.Button(new Rect(250, 200, 1316 - 500, 90), options[0], "Choice"))
                    {
                        choose = 0;
                        clicks++;
                        y = 190;
                        amountOfChoices = 0;
                        run = true;
                    }
                    if (GUI.Button(new Rect(250, 300, 1316 - 500, 90), options[1], "Choice"))
                    {
                        choose = 1;
                        clicks++;
                        y = 190;
                        amountOfChoices = 0;
                        run = true;

                    }
                    if (!string.Equals(options[2], ""))
                    {
                        if (GUI.Button(new Rect(250, 400, 1316 - 500, 90), options[2], "Choice"))
                        {
                            choose = 2;
                            clicks++;
                            y = 190;
                            amountOfChoices = 0;
                            run = true;
                        }
                    }
                    if (!string.Equals(options[3], ""))
                    {
                        if (GUI.Button(new Rect(250, 500, 1316 - 500, 90), options[3], "Choice"))
                        {
                            choose = 3;
                            clicks++;
                            y = 190;
                            amountOfChoices = 0;
                            run = true;
                        }
                    }
                    if (!string.Equals(options[4], ""))
                    {
                        if (GUI.Button(new Rect(250, 600, 1316 - 500, 90), options[4], "Choice"))
                        {
                            choose = 4;
                            clicks++;
                            y = 190;
                            amountOfChoices = 0;
                            run = true;
                        }
                    }

                }
                else if (choiceToggle)
                {
                        GUI.Box(new Rect(200, 50, 1316 - 400, 690), choiceSpeech, "ChoiceBox");
						//Set toggle options booleans to what are selected
						toggleOptions[0] = GUI.Toggle(new Rect(250, 150, 1316 - 500, 90), toggleOptions[0], options[0], "Choice");
						toggleOptions[1] = GUI.Toggle(new Rect(250, 250, 1316 - 500, 90), toggleOptions[1], options[1], "Choice");                        
						toggleOptions[2] = GUI.Toggle(new Rect(250, 350, 1316 - 500, 90), toggleOptions[2], options[2], "Choice");
						toggleOptions[3] = GUI.Toggle(new Rect(250, 450, 1316 - 500, 90), toggleOptions[3], options[3], "Choice");
						toggleOptions[4] = GUI.Toggle(new Rect(250, 550, 1316 - 500, 90), toggleOptions[4], options[4], "Choice");

                        if (GUI.Button(new Rect(250, 650, 1316 - 500, 50), "I'm done selecting all that apply"))
                        {
                            clicks++;
                        }						
                }
            }
            else if (journal)
            {
				//Set journal background on with the skin necessary
                GUI.skin = journalSkin;
                GUI.Box(new Rect(0, 00, 1316 - 0, 740 - 0), "", "JournalBackground");
				//Based on amount of clicks on counter and place in story, depends on what kind of journal functionality 
                if (story == 0)
                {
                    if (step == 0)
                    {
						journalSkin.label.fontSize = 22;

                        GUI.Label(new Rect(140, 15, 1316 / 2 - 160, 240), "   Well, today has not been good. My love just ended our 2 year relationship abruptly with no explanation. This is the person I wanted to marry, and last week we even went to look at rings together! However, I recently just heard from her family that she has been diagnosed with stage four terminal cancer.");
                        GUI.Label(new Rect(165, 220, 400, 50), "I would.... <Select 5 That Apply>");

                        FEOne[0] = GUI.Toggle(new Rect(125, 250, 1316 / 2 - 120, 50), FEOne[0], "Be very confused, overwhelmed, shell shocked, and/or lost. Wouldn't know what to do.");
                        //FEOne[1] = GUI.Toggle(new Rect(125, 310, 1316 / 2 - 120, 50), FEOne[1], "Try to respect their wishes if they didn't want me around, but it'd make me feel like complete crap to do it."); 
                        //FEOne[2] = GUI.Toggle(new Rect(150, 450, 100, 60), FEOne[2], "Immediately worried that something was wrong with the other person.");
                        //FEOne[3] = GUI.Toggle(new Rect(150, 450, 100, 60), FEOne[3], "Assume she's angry or disappointed and deeply enough to not even wanna confront me.");
                        //FEOne[4] = GUI.Toggle(new Rect(150, 450, 100, 60), FEOne[4], "Assume she did something wrong, like cheating on me, and she wanted to escape.");
                        FEOne[1] = GUI.Toggle(new Rect(125, 310, 1316 / 2 - 120, 50), FEOne[1], "Make everything about her.");
                        //FEOne[3] = GUI.Toggle(new Rect(125, 430, 1316 / 2 - 120, 50), FEOne[3], "Hope that our time before the engagement wasn't a waste for him.");
                        FEOne[2] = GUI.Toggle(new Rect(125, 370, 1316 / 2 - 120, 50), FEOne[2], "Find out want she wants, and do that.");
                        FEOne[3] = GUI.Toggle(new Rect(125, 430, 1316 / 2 - 120, 50), FEOne[3], "Tell her I will be there for her but if she prefers to be alone, that is a necessity.");
                        FEOne[4] = GUI.Toggle(new Rect(125, 490, 1316 / 2 - 120, 50), FEOne[4], "Understand her feelings and how she didn't want to hurt me.");
                        //FEOne[7] = GUI.Toggle(new Rect(125, 600, 1316 / 2 - 120, 50), FEOne[7], "Rip myself apart if I couldn't be at their side to see them off.");
                        FEOne[5] = GUI.Toggle(new Rect(125, 550, 1316 / 2 - 120, 50), FEOne[5], "Forgive her for not answering my phone calls; it would suddenly seem trivial.");
                        FEOne[6] = GUI.Toggle(new Rect(125, 610, 1316 / 2 - 120, 50), FEOne[6], "Be unable to stop all of my emotion from manifesting very suddenly and strongly.");
						//FEOne[7] = GUI.Toggle(new Rect(1316 / 2 + 40, 620, 1316 / 2 - 120, 50), FEOne[7], "");
                        FEOne[8] = GUI.Toggle(new Rect(1316 / 2 + 40, 25, 1316 / 2 - 120, 50), FEOne[8], "Be more focused on the her and what she are thinking.");


                        //obvious = GUI.Toggle(new Rect(1316 / 2 + 40, 90, 1316 / 2 - 120, 60), obvious, "Be very shocked and hurt.");
                        FIOne[0] = GUI.Toggle(new Rect(1316 / 2 + 40, 140, 1316 / 2 - 120, 60), FIOne[0], "Be unable to imagine how she was feeling.");
                        FIOne[1] = GUI.Toggle(new Rect(1316 / 2 + 40, 200, 1316 / 2 - 120, 60), FIOne[1], "Be thankful that she broke up with me so that I would stop wasting/devoting my time with her.");
						FIOne[2] = GUI.Toggle(new Rect(1316 / 2 + 40, 260, 1316 / 2 - 120, 60), FIOne[2], "Unable to do anything except what I think is the right thing.");
                        FIOne[3] = GUI.Toggle(new Rect(1316 / 2 + 40, 320, 1316 / 2 - 120, 60), FIOne[3], "Feel cheated by the universe that I didn't get to spend the precious time I deserve with her.");
                        //FIOne[4] = GUI.Toggle(new Rect(1316 / 2 + 40, 440, 1316 / 2 - 120, 60), FIOne[4], "I'd wonder why he didn't tell me.");
                        //FIOne[5] = GUI.Toggle(new Rect(1316 / 2 + 40, 450, 1316 / 2 - 120, 60), FIOne[5], "I might even be mad at him for keeping something so significantly, even though deep down, I'd know I'd probably do the same thing.");
                        //FIOne[6] = GUI.Toggle(new Rect(1316 / 2 + 40, 450, 1316 / 2 - 120, 60), FIOne[6], "Want to chase after her, but realize I'm being selfish, when it's obvious that for whatever reason, she doesn't want me with her, and who am I to get in the way of what he wants for himself when he's dying?");
                        FIOne[4] = GUI.Toggle(new Rect(1316 / 2 + 40, 380, 1316 / 2 - 120, 60), FIOne[4], "Always be thinking of how much I'll miss her.");
                        FIOne[5] = GUI.Toggle(new Rect(1316 / 2 + 40, 440, 1316 / 2 - 120, 60), FIOne[5], "Find her and tell her that even though she may die, we're still committed to each other.");
                        FIOne[6] = GUI.Toggle(new Rect(1316 / 2 + 40, 500, 1316 / 2 - 120, 60), FIOne[6], "Feel crushed because I didn't find out from her.");
                        FIOne[7] = GUI.Toggle(new Rect(1316 / 2 + 40, 560, 1316 / 2 - 120, 60), FIOne[7], "Feel the desire to run away from the situation or from her.");
                        FIOne[8] = GUI.Toggle(new Rect(1316 / 2 + 40, 80, 1316 / 2 - 120, 60), FIOne[8], "Be more focused on myself and what I perceive to be the right thing.");

                        if (GUI.Button(new Rect(1316 / 2 + 80, 630, 1316 / 2 - 200, 40), "I'm done describing my feelings."))
                        {
                            foreach (bool b in FEOne)
                            {
                                if (b)
                                {
									amt3++;
                                }
                            }
                            foreach (bool b in FIOne)
                            {
                                if (b)
                                {
									amt3++;
                                }
                            }
                           // if (FE != 0 || FI != 0)
							if (amt3 == 5)
                            {
                                journal = false;
                                interacting = false;
                                //PCMBTI.canMove = true;
                                obvious = false;
								PCMBTI.clicks++;
								amt3 = 0;
								foreach (bool b in FEOne)
								{
									if (b)
									{
										FE++;
									}
								}
								foreach (bool b in FIOne)
								{
									if (b)
									{
										FI++;
									}
								}
                            }
                            else
                            {
                                obvious = true;
								amt3 = 0;
                            }
                            Debug.Log("The amount of FE you have is: " + FE);
                            Debug.Log("The amount of FI you have is: " + FI);
                            //PCMBTI.clicks++;

                        }
                        if (obvious)
                        {
                            GUI.Label(new Rect(1316 / 2 + 100, 670, 1316 / 2 - 140, 50), "You have to select 5 feelings.");
                        }
                    }
                }
                else if (story == 1)
                {
					journalSkin.label.fontSize = 35;
                    GUI.Label(new Rect(140, 15, 1316 / 2 - 160, 600), "Project 1 is a rather broad, expansive project covering multiple areas of company operations. It has the potential to have a very significant impact on company operations but it would require a collective effort and an extensive amount of group work where you would be logically thinking through the project together with the group of individuals your boss has also assigned to it.");
                    GUI.Label(new Rect(1316 / 2 + 50, 15, 1316 / 2 - 160, 600), "Project 2 has a much more specific and narrow focus and would require a significant amount of in depth individual analysis to work through the problem. You would be working alone and the completion of the project may or may not have much impact on company operations. However, after complete, the process and problem you were working on will be fundamentally understood.");
					if (GUI.Button(new Rect(180, 630, 1316 - 320, 40), "I'm done overviewing the projects."))
                    {
                        journal = false;
                        interacting = false;
                        PCMBTI.canMove = true;
                    }
                }
                else if (story == 4)
                {
                    journalSkin.label.fontSize = 25;
                    GUI.Label(new Rect(140, 15, 1316 / 2 - 160, 240), "Select TOP 3 Careers you are interested in.");
                    for (int i = 0; i < 8; i++)
                    {
                        //careerChoices[i] = GUI.Toggle(new Rect(125, y, 0, 0), careerChoices[i], careers[i]);
                        //if (GUI.Toggle(new Rect(125, y, 1316 / 2 - 120, 50), careerChoices[i], careers[i]))
                        if (careerChoices[i])
                        {
                            if (GUI.Button(new Rect(125, y, 20, 20), careers[i], "ToggleButtonOn"))
                            {

                                amt--;
                                careerChoices[i] = false;
                            }
                        }
                        else
                        {
                            if (GUI.Button(new Rect(125, y, 20, 20), careers[i], "ToggleButtonOff"))
                            {
                                if (amt < 3)
                                {
                                    amt++;
                                    careerChoices[i] = true;
                                }
                                else
                                {
                                    obvious2 = true;
                                }
                            }
                        }

                        y += 60;
                    }
                    
                    y = 100;
                    for (int i = 8; i < 15; i++)
                    {
                    if (careerChoices[i])
                    {
                        if (GUI.Button(new Rect(1316 / 2 + 40, y, 20, 20), careers[i], "ToggleButtonOn"))
                        {

                            amt--;
                            careerChoices[i] = false;
                        }
                    }
                    else
                    {
                        if (GUI.Button(new Rect(1316 / 2 + 40   , y, 20, 20), careers[i], "ToggleButtonOff"))
                        {
                            if (amt < 3)
                            {
                                amt++;
                                careerChoices[i] = true;
                            }
                            else
                            {
                                obvious2 = true;
                            }
                        }
                    }
                    y += 60;
                }
                    y = 100;
                    if (obvious2)
                    {
                        GUI.Label(new Rect(1316 / 2 + 100, 670, 1316 / 2 - 140, 50), "You can only select top 3 careers.");
                    }
                    else if (obvious3)
                    {
                        GUI.Label(new Rect(1316 / 2 + 100, 670, 1316 / 2 - 140, 50), "You have to select 3 careers.");
                    }
                    if (GUI.Button(new Rect(1316 / 2 + 80, 630, 1316 / 2 - 200, 40), "I'm done picking my careers."))
                    {
                        if (amt == 3)
                        {
                            obvious2 = false;
                            obvious3 = false;
                            clicks++;
                            talk = true;
                            journal = false;
                            if (careerChoices[0])
                            {
                                F += 2;
                            }
                            if (careerChoices[1])
                            {
                                T += 2;
                            }
                           if (careerChoices[2])
                            {
                                F += 2;
                            }
                            if (careerChoices[3])
                            {
                                T += 2;
                            }
                            if (careerChoices[4])
                            {
                                F += 2;
                            }
                            if (careerChoices[5])
                            {
                                T += 2;
                                F += 1;
                            }
                            if (careerChoices[6])
                            {
                                F += 2;
                            }
                            if (careerChoices[7])
                            {
                                T += 1.5f;
                                F += 1.5f;
                            }
                            if (careerChoices[8])
                            {
                                F += 2;
                            }
                            if (careerChoices[9])
                            {
                                T += 2;
                            }
                           if (careerChoices[10])
                            {
                                F += 2;
                                T += 1;
                            }
                            if (careerChoices[11])
                            {
                                T += 2;
                            }
                            if (careerChoices[12])
                            {
                                F += 2;
                            }
                            if (careerChoices[13])
                            {
                                F += 2;
                            }
                            if (careerChoices[14])
                            {
                                T += 2;
                                F += 1;
                            }

                        }
                        else
                        {
                            obvious2 = false;
                            obvious3 = true;
                        }
                    }
                }
                else if (story == 5)
                {
                        GUI.Label(new Rect(140, 15, 1316 / 2 - 160, 190), "Select how you would like to spend your Saturday and would find enjoyable or relaxing when stressed.");
                        //GUI.Label(new Rect(165, 220, 400, 50), "I would.... <Select All That Apply>");

                        IOne[0] = GUI.Toggle(new Rect(125, 150, 1316 / 2 - 120, 50), IOne[0], "Read a good book.");
                        IOne[1] = GUI.Toggle(new Rect(125, 200, 1316 / 2 - 120, 50), IOne[1], "Watch a movie by myself.");
                      	IOne[2] = GUI.Toggle(new Rect(125, 250, 1316 / 2 - 120, 50), IOne[2], "Write something.");
                        IOne[3] = GUI.Toggle(new Rect(125, 300, 1316 / 2 - 120, 50), IOne[3], "Play video games.");
                        IOne[4] = GUI.Toggle(new Rect(125, 350, 1316 / 2 - 120, 50), IOne[4], "Hang out with a close-knit friend group/girlfriend alone.");
                       	IOne[5] = GUI.Toggle(new Rect(125, 400, 1316 / 2 - 120, 50), IOne[5], "Draw art.");
                        IOne[6] = GUI.Toggle(new Rect(125, 450, 1316 / 2 - 120, 50), IOne[6], "Goof off on computer.");
						

						none[0] = GUI.Toggle(new Rect(125, 500, 1316 / 2 - 120, 50), none[0], "Spend intimate time with people I love (friends and family)");
						none[1] = GUI.Toggle(new Rect(1316 / 2 + 40, 500, 1316 / 2 - 120, 50), none[1], "Spend multiple hours coding a video game for FBLA Nationals.");

                        EOne[0] = GUI.Toggle(new Rect(1316 / 2 + 40, 150, 1316 / 2 - 120, 50), EOne[0], "Go to a party/club/bar.");
                        EOne[1] = GUI.Toggle(new Rect(1316 / 2 + 40, 200, 1316 / 2 - 120, 50), EOne[1], "Watch a movie with friends.");
                        EOne[2] = GUI.Toggle(new Rect(1316 / 2 + 40, 250, 1316 / 2 - 120, 50), EOne[2], "Go to something. Anything. Just get me out of here.");
                        EOne[3] = GUI.Toggle(new Rect(1316 / 2 + 40, 300, 1316 / 2 - 120, 50), EOne[3], "Hang out/Go out with group of friends.");
                        EOne[4] = GUI.Toggle(new Rect(1316 / 2 + 40, 350, 1316 / 2 - 120, 50), EOne[4], "Go to a sporting event or concert.");
                        EOne[5] = GUI.Toggle(new Rect(1316 / 2 + 40, 400, 1316 / 2 - 120, 50), EOne[5], "Go out to a nice restaurant and eat!");
                        EOne[6] = GUI.Toggle(new Rect(1316 / 2 + 40, 450, 1316 / 2 - 120, 50), EOne[6], "Watch a play.");

                       

                        if (GUI.Button(new Rect(1316 / 2 + 80, 630, 1316 / 2 - 200, 40), "I'm done selecting my activities."))
                        {
                            foreach (bool b in IOne)
                            {
                                if (b)
                                {
                                    I+= 1.5f;
                                    amt2++;
                                }
                            }
                            foreach (bool b in EOne)
                            {
                                if (b)
                                {
                                    E+=1.5f;
                                    amt2++;
                                }
                            }
                            if (amt2 != 0)
                            {
							//interacting = false;
							PCMBTI.canMove = false;
							obvious4 = false;
							PCMBTI.clicks++;
							story = 7;  
							talk = true;
							clicks = -1;
                              journal = false;
                            }
                            else
                            {
                                obvious4 = true;
                            }
                            Debug.Log("The amount of E you have is: " + E);
                            Debug.Log("The amount of I you have is: " + I);

                        }
                        if (obvious4)
                        {
                            GUI.Label(new Rect(1316 / 2 + 50, 670, 1316 / 2 - 140, 50), "You have to select at least one activity.");
                            
                        }
                }
                else if (story == 6)
                {
                    journalSkin.label.fontSize = 35;
                    GUI.Label(new Rect(140, 15, 1316 / 2 - 160, 600), "This is goodbye forever. Don't follow me.");
                    if (GUI.Button(new Rect(1316 / 2 + 80, 630, 1316 / 2 - 200, 40), "WHAT?!!?"))
                    {
                        journal = false;
                        interacting = false;
                        PCMBTI.canMove = true;
                        PCMBTI.clicks++;
                    }
                }
            }

        }
		//If we are not interacting, but we can interact with a target, display the text showing how to interact with that object (which is set in interactable)
        else if (target && target.GetComponent<Interactable>())
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(target.transform.position + target.GetComponent<Interactable>().nametagOffset);
            GUIContent text = new GUIContent(target.GetComponent<Interactable>().interactingText);
            Rect rect = GUILayoutUtility.GetRect(text, "interactingText");
            GUI.Label(new Rect(screenPos.x - rect.width * 0.5f, Screen.height - screenPos.y - rect.height * 0.5f, rect.width, rect.height), text, "interactingText");
        }
    }
}
