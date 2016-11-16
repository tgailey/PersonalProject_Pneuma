using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;


public class PlayerControllerMBTI : MonoBehaviour {
	/*
	 * This class handles the text in the game, based on the part of the story we are located
	 * Also calculates personality at end based off values acquired through interaction in Testing script 
	 * */

    Testing T; //Testing script. Handles interaction and holds values for personality
    PhoneController PC; //Script that handles phone interaction
    GameObject phone; //Phone
    public int clicks; //Counter that determines what text will be displayed
	public bool canClick; //Whether or not left clicking will move text (through increasing clicks) or if the player has to do something else to progress the text
    public bool canMove; //Whether or not the player can move or not
    public GUISkin MainSkin; //The skin for all the text in the game

    FirstPersonController FPC; //The controller for the player

    float distanceTravelled = 0; //Distance traveled
    Vector3 lastPosition; 

    int numMessages; //Holds amount of unread messages so that when we view them, we can move the counter.

    GameObject girlfriend;
    GameObject classMates;
    Animator anim;
    GameObject BlackScreen; //The black screen that blocks the screen at varius times
    public Material blackMat; //The black material of the screen
    Renderer rend;
    public float a = 1;
    GameObject Note, Journal;
	Vector2 scrollViewVector;

	//Whether or not the player is extroverted/introverted, intuitive/sensing, thinking/feeling, and perceiving/judging
	bool introverted = false, intuitive = false, thinking = false, perceiving = false;
	bool FeTi = false; //Whether or not the player is FeTi or FiTe (Personality testing)
	bool domF = false, domT = false; //Dominate feeler/thinker. If false, then thinking/feeling are not dominant traits
	float totalFETI, totalFITE; //Total amount of FeTi and FiTe. Determines whether or not the player is a FeTi user or FiTe user.
	string text = "", type = "", typeOverview; //Canvas text for endgame reveal
    // Use this for initialization
    void Start () {
        T = GetComponent<Testing>();
	    PC = GetComponentInChildren<PhoneController>();
        phone = PC.gameObject;
        phone.SetActive(false);
        FPC = GetComponent<FirstPersonController>();
        //FPC.enabled = false;
        clicks = 0;
        canClick = true;
        canMove = false;

        lastPosition = transform.position;
        numMessages = 0;

        //DontDestroyOnLoad(this.gameObject);

        BlackScreen = GameObject.Find("FPSController/FirstPersonCharacter/BlackScreen");
        rend = BlackScreen.GetComponent<Renderer>();

		Note = GameObject.Find("Apartment/Scenario1/Note");
        Note.SetActive(false);
		Journal = GameObject.Find ("Apartment/Scenario1/Journal");
		Journal.SetActive (false);
    }
	
	// Update is called once per frame
	void Update () {
       
		if (!GameObject.ReferenceEquals(GameObject.Find("Apartment/Scenario1/Part1/Girlfriend"), null))
        {
			girlfriend = GameObject.Find("Apartment/Scenario1/Part1/Girlfriend");
            anim = girlfriend.GetComponent<Animator>();
        }
        if (!GameObject.ReferenceEquals(GameObject.Find("Apartment/Scenario4/Classmates"), null))
        {
			classMates = GameObject.Find("Apartment/Scenario4/Classmates");
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
			clicks++;
        }

		//If press left click and we can move text by clicking, move text
        if (Input.GetButtonDown("Fire1") && canClick)
        {
            clicks++;
        }

		//If we can move, and the movement is off, turn it on
        if (canMove && FPC.movementOff)
        {
            FPC.turnOnMovement();
        }
		//If we can move, turn off the cursor
        else if (canMove)
        {
            Cursor.visible = false;
        }
		//If we can't move, and the movement is still on, turn it off
        else if (!FPC.movementOff)
        {
            FPC.turnOffMovement();
        }
		//If we can't move and the movement is successfully turned off, show the cursor
        else
        {
            Cursor.visible = true;
        }


		if (clicks == 4) {
			//At this point, the black screen will fade. Once faded, clicks counter moves forward
			canClick = false;
			if (a >= 0f) {
				a -= .007f;
				//Decreases the value of a, and sets alpha of blackscreen material to new color with this alpha
				rend.material.color = new Color (0, 0, 0, a);
			} else {
				rend.material.color = new Color (0, 0, 0, a);
				rend.enabled = false;
				clicks++;
			}
		} else if (clicks == 5) {
			a = 1;
			canClick = true;
		} else if (clicks == 6) {
			//Sets movement to true for player testing
			canMove = true;
			canClick = false;
			//Distance traveled is calculated by distance between current position and last position, which is set to the current position immediately after.
			distanceTravelled += Vector3.Distance (transform.position, lastPosition);
			lastPosition = transform.position;
			//Once distance traveled reaches certain point, move clicks counter forward.
			if (distanceTravelled >= 12) {
				clicks++;
			}
		} else if (clicks == 7) {
			canMove = false;
			canClick = true;
		} else if (clicks == 8) {
			canClick = false;
			phone.SetActive (true);
			//If facing/looking at phone, move counter
			if (PC.facing) {
				numMessages = PC.vc.myMessages ().messages1.Count; //Set nummessages to amount of messages on contact
				clicks++;
			}
		} else if (clicks == 9) {
			//Once we send a messages (and therefore amount of messages is greater than our value set before, move clicks forward

			if (PC.vc.myMessages ().messages1.Count > numMessages) {
				clicks++;
			}

		} else if (clicks == 10) {
			//Move forward based on screen of phone that is displayed. (Tutorial showing how to use phone)
			if (PC.screen == 2) {
				clicks++;
			}
		} else if (clicks == 11) {
			//Move forward based on screen of phone that is displayed. (Tutorial showing how to use phone)
			if (PC.screen == 5) {
				clicks++;
			}
		} else if (clicks == 12) {
			//Once music is playing, move it forward.
            if (!AudioClip.ReferenceEquals(PC.AS.clip, null))
            {
                clicks++;
            }
        }
        else if (clicks == 13)
        {
			//Move forward based on screen of phone that is displayed. (Tutorial showing how to use phone)
            if (PC.screen == 2)
            {
                clicks++;
            }
        }
        else if (clicks == 14)
        {
			//Move forward based on screen of phone that is displayed. (Tutorial showing how to use phone)
            if (PC.screen == 7)
            {
                clicks++;
            }
        }
        else if (clicks == 15)
        {
			//Move forward based on screen of phone that is displayed. (Tutorial showing how to use phone)
            if (PC.screen == 2)
            {
                clicks++;
            }
        }
        else if (clicks == 16)
        {
			//Move counter forward if not facing phone
            if (!PC.facing)
            {
                clicks++;
            }
        }
        else if (clicks == 17)
        {
			//Clicks moved by external source (Collision with red box)
            canMove = true;
        }
        else if (clicks == 18)
        {
            canClick = false;
            classMates.SetActive(false);
        }
        else if (clicks == 21)
        {
			//Set girlfriend location to bed, and set her animation to sleeping. Display black screen.
            anim.SetBool("isSleeping", true);
            Vector3 temp = Vector3.zero, temp2 = Vector3.zero;
            temp.x = -6.3f;
            temp.y = 76.5f;
            temp.z = 128.1f;
            girlfriend.transform.localPosition = temp;
            temp2.y = 90;
            girlfriend.transform.eulerAngles = temp2;
            rend.enabled = true;
            canClick = true;
            canMove = false;
            rend.material.color = new Color(0, 0, 0, a);
        }
        else if (clicks == 22)
        {
			//Fade black screen and move counter forward
            canClick = false;
            if (a >= 0f)
            {
                a -= .007f;
                rend.material.color = new Color(0, 0, 0, a);
            }
            else {
                rend.material.color = new Color(0, 0, 0, a);
                rend.enabled = false;
                clicks++;
            }
        }
        else if (clicks == 24)
        {
			//Move clicks once message is viewed on phone
            a = 1;
            canMove = true;
            if (PC.screen == 4 && PC.facing && PC.contactNum == 2)
            {
                clicks++;
            }
        }
        else if (clicks == 29)
        {
			//Move clicks once message is viewed on phone
            if (PC.screen == 4 && PC.facing && PC.contactNum == 4)
            {
                clicks++;
            }
        }
        else if (clicks == 37)
        {
			anim.SetBool("isSleeping", false);
			//Move clicks once message is viewed on phone
            if (PC.screen == 4 && PC.facing && PC.contactNum == 5)
            {
                clicks++;
            }
        }
        else if (clicks == 39)
        {
			//move girlfriend to front of the door
			//Turn on classmates

            Vector3 temp = Vector3.zero, temp2 = Vector3.zero;
            temp.x = 7.25f;
            temp.y = 75.5f;
            temp.z = 149.2f;
            girlfriend.transform.localPosition = temp;
            temp2.y = 243;
            girlfriend.transform.eulerAngles = temp2;
            classMates.SetActive(true);
        }
        else if (clicks == 42)
        {
            a = 1;
        }
        else if (clicks == 43)
        {
			//Display black screen and quickly fade it
            canMove = false;
            classMates.SetActive(false);
            anim.SetBool("isSleeping", true);
            Vector3 temp = Vector3.zero, temp2 = Vector3.zero;
			temp.x = -6.3f;
			temp.y = 76.5f;
			temp.z = 128.1f;
            girlfriend.transform.localPosition = temp;
            temp2.y = 90;
            girlfriend.transform.eulerAngles = temp2;
            if (a >= 0f)
            {
                rend.enabled = true;
                a -= .007f;
                rend.material.color = new Color(0, 0, 0, a);

            }
            else {
                rend.enabled = false;
                clicks++;
                rend.material.color = new Color(0, 0, 0, a);

            }
        }
        else if (clicks == 44)
        {
            a = 1;
            canMove = true;
        }
        else if (clicks == 45)
        {
			//Display black screen
            rend.enabled = true;
            rend.material.color = new Color(0, 0, 0, a);
            girlfriend.SetActive(false);

            canMove = false;
            canClick = true;
        }
        else if (clicks == 46)
        {
			//fade black screen
            canClick = false;
            if (a >= 0f)
            {
                rend.enabled = true;
                a -= .007f;
                rend.material.color = new Color(0, 0, 0, a);

            }
            else {
                rend.enabled = false;
                clicks++;
                rend.material.color = new Color(0, 0, 0, a);

            }
        }
        else if (clicks == 47)
        {
            //canMove = false;
            canClick = true;
        }
        else if (clicks == 48)
        {
			//Set jornal to true, meaning the player will be able to write without interacting with something
			//Using this for internal thought process test, where the player will not have to interact with something because it is in his mind.
            T.story = 5;
            T.journal = true;


            //clicks++;
        }
        else if (clicks == 49)
        {
            canClick = false;
            T.interacting = true;
            canMove = false;
        }
        else if (clicks == 50)
        {

            T.interacting = true;
        }
        else if (clicks == 51)
        {
			Note.SetActive(true);
            if (T.story == 6 && T.journal)
            {
                clicks++;
            }
        }
        else if (clicks == 53)
        {
			Journal.SetActive (true);
        }
        else if (clicks == 54)
        {
			//Move clicks once message is viewed on phone
            if (PC.screen == 4 && PC.facing && PC.contactNum == 3)
            {
                clicks++;
            }
        }
        else if (clicks == 55)
        {
            a = 1;
        }
        else if (clicks == 56)
        {
			//Display black screen
            canClick = true;
            rend.material.color = new Color(0, 0, 0, a);
            rend.enabled = true;
            this.gameObject.transform.position = new Vector3(-2.6f, -2.42f, -24.3f);
        }
        else if (clicks == 57)
        {
			//If introversion is bigger than extroversion, set introversion to true
            if (T.I > T.E)
            {
                introverted = true;
            }
			//If thinking is significantly bigger than feeling, then set dominant Thinking to true
            if (T.T - T.F >= 5)
            {
                domT = true;
            }
			//If feeling is significantly bigger than thinking, then set dominant Feeling to true
            else if (T.F - T.T >= 5)
            {
                domF = true;
            }
			//FETI equals Fe + Ti. FITE equals Fi + Te
            totalFETI = T.FE + T.TI;
            totalFITE = T.FI + T.TE;
			//If Fe+Ti is bigger, set those functions to true. (when false, the functions used will be FiTe)
            if (totalFETI > totalFITE)
            {
                FeTi = true;
            }

			typeOverview += "PERSONALITY TYPE OVERVIEW (PAGE 2)\r\n\r\n";
			//If introverted
            if (introverted)
            {
				//And you use Fe and Ti
                if (FeTi)
                {
					//And are a dominant thinker
                    if (domT)
                    {
						//The intuitive people of these types use NE and the sensing ones use NI, so add NE to intuition and NI to sensing to create better total
                        T.N += T.NE;
                        T.S += T.NI;
						//if intuition is greater than sensing
                        if (T.N > T.S)
                        {
							//Set everything to proper bools and write out type overview.
                            introverted = true;
                            intuitive = true;
                            thinking = true;
                            perceiving = true;
                            //RETURNING INTP
							#region INTP type overview
							typeOverview += "INTP PERSONALITY (THE LOGICIAN)";
							typeOverview += "\r\n\r\n";
							typeOverview = "The INTP personality type is fairly rare, making up only three percent of the population, which is definitely a good thing for them, as there's nothing they'd be more unhappy about than being \"common\". INTPs pride themselves on their inventiveness and creativity, their unique perspective and vigorous intellect. Usually known as the philosopher, the architect, or the dreamy professor, INTPs have been responsible for many scientific discoveries throughout history.";
							typeOverview += "\r\n\r\n";
							typeOverview += "INTPs are known for their brilliant theories and unrelenting logic – in fact, they are considered the most logically precise of all the personality types.";
							typeOverview += "\r\n\r\n";
							typeOverview += "They love patterns, and spotting discrepancies between statements could almost be described as a hobby, making it a bad idea to lie to an INTP. This makes it ironic that INTPs' word should always be taken with a grain of salt – it's not that they are dishonest, but people with the INTP personality type tend to share thoughts that are not fully developed, using others as a sounding board for ideas and theories in a debate against themselves rather than as actual conversation partners.";
							typeOverview += "\r\n\r\n";
							typeOverview += "This may make them appear unreliable, but in reality no one is more enthusiastic and capable of spotting a problem, drilling through the endless factors and details that encompass the issue and developing a unique and viable solution than INTPs – just don't expect punctual progress reports. People who share the INTP personality type aren't interested in practical, day-to-day activities and maintenance, but when they find an environment where their creative genius and potential can be expressed, there is no limit to the time and energy INTPs will expend in developing an insightful and unbiased solution.";
							typeOverview += "\r\n\r\n";
							typeOverview += "They may appear to drift about in an unending daydream, but INTPs' thought process is unceasing, and their minds buzz with ideas from the moment they wake up. This constant thinking can have the effect of making them look pensive and detached, as they are often conducting full-fledged debates in their own heads, but really INTPs are quite relaxed and friendly when they are with people they know, or who share their interests. However, this can be replaced by overwhelming shyness when INTP personalities are among unfamiliar faces, and friendly banter can quickly become combative if they believe their logical conclusions or theories are being criticized.";
							typeOverview += "\r\n\r\n";
							typeOverview += "When INTPs are particularly excited, the conversation can border on incoherence as they try to explain the daisy-chain of logical conclusions that led to the formation of their latest idea. Oftentimes, INTPs will opt to simply move on from a topic before it's ever understood what they were trying to say, rather than try to lay things out in plain terms.";
							typeOverview += "\r\n\r\n";
							typeOverview += "The reverse can also be true when people explain their thought processes to INTPs in terms of subjectivity and feeling. Imagine an immensely complicated clockwork, taking in every fact and idea possible, processing them with a heavy dose of creative reasoning and returning the most logically sound results available – this is how the INTP mind works, and this type has little tolerance for an emotional monkey-wrench jamming their machines.";
							typeOverview += "\r\n\r\n";
							typeOverview += "Further, with Thinking (T) as one of their governing traits, INTPs are unlikely to understand emotional complaints at all, and their friends won't find a bedrock of emotional support in them. People with the INTP personality type would much rather make a series of logical suggestions for how to resolve the underlying issue, a perspective that is not always welcomed by their Feeling (F) companions. This will likely extend to most social conventions and goals as well, like planning dinners and getting married, as INTPs are far more concerned with originality and efficient results.";
							typeOverview += "\r\n\r\n";
							typeOverview += "The one thing that really holds INTPs back is their restless and pervasive fear of failure. INTP personalities are so prone to reassessing their own thoughts and theories, worrying that they've missed some critical piece of the puzzle, that they can stagnate, lost in an intangible world where their thoughts are never truly applied. Overcoming this self-doubt stands as the greatest challenge INTPs are likely to face, but the intellectual gifts – big and small – bestowed on the world when they do makes it worth the fight.";
							typeOverview += "\r\n\r\n";
							typeOverview += "";
							#endregion
							PlayerPrefs.SetString ("MBTI", "INTP");
                        }
						//if intuition is less than sensing
                        else {
							//Set everything to proper bools and write out type overview.
                            introverted = true;
                            intuitive = false;
                            thinking = true;
                            perceiving = true;
                            //RETURNING ISTP
							#region ISTP type overview
							typeOverview += "ISTP PERSONALITY (THE VIRTUOSO)";
							typeOverview += "\r\n\r\n";
							typeOverview += "ISTPs love to explore with their hands and their eyes, touching and examining the world around them with cool rationalism and spirited curiosity. People with this personality type are natural Makers, moving from project to project, building the useful and the superfluous for the fun of it, and learning from their environment as they go. Often mechanics and engineers, ISTPs find no greater joy than in getting their hands dirty pulling things apart and putting them back together, just a little bit better than they were before.";
							typeOverview += "\r\n\r\n";
							typeOverview += "ISTPs explore ideas through creating, troubleshooting, trial and error and first-hand experience. They enjoy having other people take an interest in their projects and sometimes don't even mind them getting into their space. Of course, that's on the condition that those people don't interfere with ISTPs' principles and freedom, and they'll need to be open to ISTPs returning the interest in kind.";
							typeOverview += "\r\n\r\n";
							typeOverview += "ISTPs enjoy lending a hand and sharing their experience, especially with the people they care about, and it's a shame they're so uncommon, making up only about five percent of the population. ISTP women are especially rare, and the typical gender roles that society tends to expect can be a poor fit – they'll often be seen as tomboys from a young age.";
							typeOverview += "\r\n\r\n";
							typeOverview += "While their mechanical tendencies can make them appear simple at a glance, ISTPs are actually quite enigmatic. Friendly but very private, calm but suddenly spontaneous, extremely curious but unable to stay focused on formal studies, ISTP personalities can be a challenge to predict, even by their friends and loved ones. ISTPs can seem very loyal and steady for a while, but they tend to build up a store of impulsive energy that explodes without warning, taking their interests in bold new directions.";
							typeOverview += "\r\n\r\n";
							typeOverview += "Rather than some sort of vision quest though, ISTPs are merely exploring the viability of a new interest when they make these seismic shifts.";
							typeOverview += "\r\n\r\n";
							typeOverview += "ISTPs' decisions stem from a sense of practical realism, and at their heart is a strong sense of direct fairness, a \"do unto others\" attitude, which really helps to explain many of ISTPs' puzzling traits. Instead of being overly cautious though, avoiding stepping on toes in order to avoid having their toes stepped on, ISTPs are likely to go too far, accepting likewise retaliation, good or bad, as fair play.";
							typeOverview += "\r\n\r\n";
							typeOverview += "The biggest issue ISTPs are likely to face is that they often act too soon, taking for granted their permissive nature and assuming that others are the same. They'll be the first to tell an insensitive joke, get overly involved in someone else's project, roughhouse and play around, or suddenly change their plans because something more interesting came up.";
							typeOverview += "\r\n\r\n";
							typeOverview += "ISTPs will come to learn that many other personality types have much more firmly drawn lines on rules and acceptable behavior than they do – they don't want to hear an insensitive joke, and certainly wouldn't tell one back, and they wouldn't want to engage in horseplay, even with a willing party. If a situation is already emotionally charged, violating these boundaries can backfire tremendously.";
							typeOverview += "\r\n\r\n";
							typeOverview += "ISTPs have a particular difficulty in predicting emotions, but this is just a natural extension of their fairness, given how difficult it is to gauge ISTPs' emotions and motivations. However, their tendency to explore their relationships through their actions rather than through empathy can lead to some very frustrating situations. People with the ISTP personality type struggle with boundaries and guidelines, preferring the freedom to move about and color outside the lines if they need to.";
							typeOverview += "\r\n\r\n";
							typeOverview += "Finding an environment where they can work with good friends who understand their style and unpredictability, combining their creativity, sense of humor and hands-on approach to build practical solutions and things, will give ISTPs many happy years of building useful boxes – and admiring them from the outside.";
							typeOverview += "\r\n\r\n";
							#endregion
							PlayerPrefs.SetString ("MBTI", "ISTP");

                        }
                    }
					//If NOT a dominant thinker
                    else {
						//The intuitive people of these types use NI and the sensing ones use NE, so add NI to intuition and NE to sensing to create better total
                        T.N += T.NI;
                        T.S += T.NE;
						//if intuition is greater than sensing
                        if (T.N > T.S)
                        {
                            introverted = true;
                            intuitive = true;
                            thinking = false;
                            perceiving = false;
                            //RETURNING INFJ
							#region INFJ type overview
							typeOverview += "INFJ PERSONALITY (THE ADVOCATE)";
							typeOverview += "\r\n\r\n";
							typeOverview += "The INFJ personality type is very rare, making up less than one percent of the population, but they nonetheless leave their mark on the world. As Diplomats (NF), they have an inborn sense of idealism and morality, but what sets them apart is the accompanying Judging (J) trait – INFJs are not idle dreamers, but people capable of taking concrete steps to realize their goals and make a lasting positive impact.";
							typeOverview += "\r\n\r\n";
							typeOverview += "INFJs tend to see helping others as their purpose in life, but while people with this personality type can be found engaging rescue efforts and doing charity work, their real passion is to get to the heart of the issue so that people need not be rescued at all.";
							typeOverview += "\r\n\r\n";
							typeOverview += "INFJs indeed share a very unique combination of traits: though soft-spoken, they have very strong opinions and will fight tirelessly for an idea they believe in. They are decisive and strong-willed, but will rarely use that energy for personal gain – INFJs will act with creativity, imagination, conviction and sensitivity not to create advantage, but to create balance. Egalitarianism and karma are very attractive ideas to INFJs, and they tend to believe that nothing would help the world so much as using love and compassion to soften the hearts of tyrants.";
							typeOverview += "\r\n\r\n";
							typeOverview += "INFJs find it easy to make connections with others, and have a talent for warm, sensitive language, speaking in human terms, rather than with pure logic and fact. It makes sense that their friends and colleagues will come to think of them as quiet Extroverted types, but they would all do well to remember that INFJs need time alone to decompress and recharge, and to not become too alarmed when they suddenly withdraw. INFJs take great care of other’s feelings, and they expect the favor to be returned – sometimes that means giving them the space they need for a few days.";
							typeOverview += "\r\n\r\n";
							typeOverview += "Really though, it is most important for INFJs to remember to take care of themselves. The passion of their convictions is perfectly capable of carrying them past their breaking point and if their zeal gets out of hand, they can find themselves exhausted, unhealthy and stressed. This becomes especially apparent when INFJs find themselves up against conflict and criticism – their sensitivity forces them to do everything they can to evade these seemingly personal attacks, but when the circumstances are unavoidable, they can fight back in highly irrational, unhelpful ways.";
							typeOverview += "\r\n\r\n";
							typeOverview += "To INFJs, the world is a place full of inequity – but it doesn’t have to be. No other personality type is better suited to create a movement to right a wrong, no matter how big or small. INFJs just need to remember that while they’re busy taking care of the world, they need to take care of themselves, too.";
							typeOverview += "\r\n\r\n";
							typeOverview += "";
							#endregion
							PlayerPrefs.SetString ("MBTI", "INFJ");

                        }
						//if intuition is less than sensing
                        else {
                            introverted = true;
                            intuitive = false;
                            thinking = false;
                            perceiving = false;
                            //RETURNING ISFJ
							#region ISFJ type overview
							typeOverview += "ISFJ PERSONALITY (THE DEFENDER)";
							typeOverview += "\r\n\r\n";
							typeOverview += "The ISFJ personality type is quite unique, as many of their qualities defy the definition of their individual traits. Though possessing the Feeling (F) trait, ISFJs have excellent analytical abilities; though Introverted (I), they have well-developed people skills and robust social relationships; and though they are a Judging (J) type, ISFJs are often receptive to change and new ideas. As with so many things, people with the ISFJ personality type are more than the sum of their parts, and it is the way they use these strengths that defines who they are.";
							typeOverview += "\r\n\r\n";
							typeOverview += "ISFJs are true altruists, meeting kindness with kindness-in-excess and engaging the work and people they believe in with enthusiasm and generosity.";
							typeOverview += "\r\n\r\n";
							typeOverview += "There's hardly a better type to make up such a large proportion of the population, nearly 13%. Combining the best of tradition and the desire to do good, ISFJs are found in lines of work with a sense of history behind them, such as medicine, academics and charitable social work.";
							typeOverview += "\r\n\r\n";
							typeOverview += "ISFJ personalities are often meticulous to the point of perfectionism, and though they procrastinate, they can always be relied on to get the job done on time. ISFJs take their responsibilities personally, consistently going above and beyond, doing everything they can to exceed expectations and delight others, at work and at home.";
							typeOverview += "\r\n\r\n";
							typeOverview += "The challenge for ISFJs is ensuring that what they do is noticed. They have a tendency to underplay their accomplishments, and while their kindness is often respected, more cynical and selfish people are likely to take advantage of ISFJs' dedication and humbleness by pushing work onto them and then taking the credit. ISFJs need to know when to say no and stand up for themselves if they are to maintain their confidence and enthusiasm.";
							typeOverview += "\r\n\r\n";
							typeOverview += "Naturally social, an odd quality for Introverts, ISFJs utilize excellent memories not to retain data and trivia, but to remember people, and details about their lives. When it comes to gift-giving, ISFJs have no equal, using their imagination and natural sensitivity to express their generosity in ways that touch the hearts of their recipients. While this is certainly true of their coworkers, whom people with the ISFJ personality type often consider their personal friends, it is in family that their expressions of affection fully bloom.";
							typeOverview += "\r\n\r\n";
							typeOverview += "ISFJ personalities are a wonderful group, rarely sitting idle while a worthy cause remains unfinished. ISFJs' ability to connect with others on an intimate level is unrivaled among Introverts, and the joy they experience in using those connections to maintain a supportive, happy family is a gift for everyone involved. They may never be truly comfortable in the spotlight, and may feel guilty taking due credit for team efforts, but if they can ensure that their efforts are recognized, ISFJs are likely to feel a level of satisfaction in what they do that many other personality types can only dream of.";
							typeOverview += "\r\n\r\n";
							typeOverview += "";
							#endregion
							PlayerPrefs.SetString ("MBTI", "ISFJ");
                        }
                    }
                }
				//If a FiTe user
                else {
					//And a dominant feeler
                    if (domF)
                    {
						//The intuitive people of these types use NE and the sensing ones use NI, so add NE to intuition and NI to sensing to create better total

                        T.N += T.NE;
                        T.S += T.NI;
						//if intuition is greater than sensing

                        if (T.N > T.S)
                        {
                            introverted = true;
                            intuitive = true;
                            thinking = false;
                            perceiving = true;
                            //RETURNING INFP
							#region INFP Personality Overview
							typeOverview += "INFP PERSONALITY (THE MEDIATOR)";
							typeOverview += "\r\n\r\n";
							typeOverview += "INFP personalities are true idealists, always looking for the hint of good in even the worst of people and events, searching for ways to make things better. While they may be perceived as calm, reserved, or even shy, INFPs have an inner flame and passion that can truly shine. Comprising just 4% of the population, the risk of feeling misunderstood is unfortunately high for the INFP personality type – but when they find like-minded people to spend their time with, the harmony they feel will be a fountain of joy and inspiration.";
							typeOverview += "\r\n\r\n";
							typeOverview += "Being a part of the Diplomat (NF) personality group, INFPs are guided by their principles, rather than by logic (Analysts), excitement (Explorers), or practicality (Sentinels). When deciding how to move forward, they will look to honor, beauty, morality and virtue – INFPs are led by the purity of their intent, not rewards and punishments. People who share the INFP personality type are proud of this quality, and rightly so, but not everyone understands the drive behind these feelings, and it can lead to isolation.";
							typeOverview += "\r\n\r\n";
							typeOverview += "At their best, these qualities enable INFPs to communicate deeply with others, easily speaking in metaphors and parables, and understanding and creating symbols to share their ideas. The strength of this intuitive communication style lends itself well to creative works, and it comes as no surprise that many famous INFPs are poets, writers and actors. Understanding themselves and their place in the world is important to INFPs, and they explore these ideas by projecting themselves into their work.";
							typeOverview += "\r\n\r\n";
							typeOverview += "INFPs have a talent for self-expression, revealing their beauty and their secrets through metaphors and fictional characters.";
							typeOverview += "\r\n\r\n";
							typeOverview += "INFPs’ ability with language doesn’t stop with their native tongue, either – as with most people who share the Diplomat personality types, they are considered gifted when it comes to learning a second (or third!) language. Their gift for communication also lends itself well to INFPs’ desire for harmony, a recurring theme with Diplomats, and helps them to move forward as they find their calling.";
							typeOverview += "\r\n\r\n";
							typeOverview += "Unlike their Extraverted cousins though, INFPs will focus their attention on just a few people, a single worthy cause – spread too thinly, they’ll run out of energy, and even become dejected and overwhelmed by all the bad in the world that they can’t fix. This is a sad sight for INFPs’ friends, who will come to depend on their rosy outlook.";
							typeOverview += "\r\n\r\n";
							typeOverview += "If they are not careful, INFPs can lose themselves in their quest for good and neglect the day-to-day upkeep that life demands. INFPs often drift into deep thought, enjoying contemplating the hypothetical and the philosophical more than any other personality type. Left unchecked, INFPs may start to lose touch, withdrawing into \"hermit mode\", and it can take a great deal of energy from their friends or partner to bring them back to the real world.";
							typeOverview += "\r\n\r\n";
							typeOverview += "Luckily, like the flowers in spring, INFP’s affection, creativity, altruism and idealism will always come back, rewarding them and those they love perhaps not with logic and utility, but with a world view that inspires compassion, kindness and beauty wherever they go.";
							typeOverview += "\r\n\r\n";
							typeOverview += "";
							#endregion
							PlayerPrefs.SetString ("MBTI", "INFP");

                        }
						//if intuition is less than sensing

                        else {
                            introverted = true;
                            intuitive = false;
                            thinking = false;
                            perceiving = true;
                            //RETURNING ISFP
							#region ISFP Personality Overview
							typeOverview += "ISFP PERSONALITY (THE ADVENTURER)";
							typeOverview += "\r\n\r\n";
							typeOverview += "ISFP personality types are true artists, but not necessarily in the typical sense where they're out painting happy little trees. Often enough though, they are perfectly capable of this. Rather, it's that they use aesthetics, design and even their choices and actions to push the limits of social convention. ISFPs enjoy upsetting traditional expectations with experiments in beauty and behavior – chances are, they've expressed more than once the phrase \"Don't box me in!\"";
							typeOverview += "\r\n\r\n";
							typeOverview += "ISFPs live in a colorful, sensual world, inspired by connections with people and ideas. ISFP personalities take joy in reinterpreting these connections, reinventing and experimenting with both themselves and new perspectives. No other type explores and experiments in this way more. This creates a sense of spontaneity, making ISFPs seem unpredictable, even to their close friends and loved ones.";
							typeOverview += "\r\n\r\n";
							typeOverview += "Despite all this, ISFPs are definitely Introverts (I), surprising their friends further when they step out of the spotlight to be by themselves to recharge. Just because they are alone though, doesn't mean people with the ISFP personality type sit idle – they take this time for introspection, assessing their principles. Rather than dwelling on the past or the future, ISFPs think about who they are. They return from their cloister, transformed.";
							typeOverview += "\r\n\r\n";
							typeOverview += "ISFPs live to find ways to push their passions. Riskier behaviors like gambling and extreme sports are more common with this personality type than with others. Fortunately their attunement to the moment and their environment allows them to do better than most. ISFPs also enjoy connecting with others, and have a certain irresistible charm.";
							typeOverview += "\r\n\r\n";
							typeOverview += "ISFPs always know just the compliment to soften a heart that's getting ready to call their risks irresponsible or reckless.";
							typeOverview += "\r\n\r\n";
							typeOverview += "However, if a criticism does get through, it can end poorly. Some ISFPs can handle kindly phrased commentary, valuing it as another perspective to help push their passions in new directions. But if the comments are more biting and less mature, ISFP personalities can lose their tempers in spectacular fashion.";
							typeOverview += "\r\n\r\n";
							typeOverview += "ISFPs are sensitive to others' feelings and value harmony. When faced with criticism, it can be a challenge for people with this type to step away from the moment long enough to not get caught up in the heat of the moment. But living in the moment goes both ways, and once the heightened emotions of an argument cool, ISFPs can usually call the past the past and move on as though it never occurred.";
							typeOverview += "\r\n\r\n";
							typeOverview += "The biggest challenge facing ISFPs is planning for the future. Finding constructive ideals to base their goals on and working out goals that create positive principles is no small task. Unlike Sentinel types, ISFPs don't plan their futures in terms of assets and retirement. Rather, they plan actions and behaviors as contributions to a sense of identity, building a portfolio of experiences, not stocks.";
							typeOverview += "\r\n\r\n";
							typeOverview += "If these goals and principles are noble, ISFPs can act with amazing charity and selflessness – but it can also happen that people with the ISFP personality type establish a more self-centered identity, acting with selfishness, manipulation and egoism. It's important for ISFPs to remember to actively become the person they want to be. Developing and maintaining a new habit may not come naturally, but taking the time each day to understand their motivations allows ISFPs to use their strengths to pursue whatever they've come to love.";
							typeOverview += "\r\n\r\n";
							typeOverview += "";
							#endregion
							PlayerPrefs.SetString ("MBTI", "ISFP");

                        }
                    }
					//Not a dominant feeler
                    else {
						//The intuitive people of these types use NI and the sensing ones use NE, so add NI to intuition and NE to sensing to create better total
                        T.N += T.NI;
                        T.S += T.NE;
						//if intuition is greater than sensing

                        if (T.N > T.S)
                        {
                            introverted = true;
                            intuitive = true;
                            thinking = true;
                            perceiving = false;
                            //RETURNING INTJ
							#region INTJ Personality Overview
							typeOverview += "INTJ PERSONALITY (THE ARCHITECT)";
							typeOverview += "\r\n\r\n";
							typeOverview += "It’s lonely at the top, and being one of the rarest and most strategically capable personality types, INTJs know this all too well. INTJs form just two percent of the population, and women of this personality type are especially rare, forming just 0.8% of the population – it is often a challenge for them to find like-minded individuals who are able to keep up with their relentless intellectualism and chess-like maneuvering. People with the INTJ personality type are imaginative yet decisive, ambitious yet private, amazingly curious, but they do not squander their energy.";
							typeOverview += "\r\n\r\n";
							typeOverview += "With a natural thirst for knowledge that shows itself early in life, INTJs are often given the title of “bookworm” as children. While this may be intended as an insult by their peers, they more than likely identify with it and are even proud of it, greatly enjoying their broad and deep body of knowledge. INTJs enjoy sharing what they know as well, confident in their mastery of their chosen subjects, but owing to their Intuitive (N) and Judging (J) traits, they prefer to design and execute a brilliant plan within their field rather than share opinions on “uninteresting” distractions like gossip.";
							typeOverview += "\r\n\r\n";
							typeOverview += "A paradox to most observers, INTJs are able to live by glaring contradictions that nonetheless make perfect sense – at least from a purely rational perspective. For example, INTJs are simultaneously the most starry-eyed idealists and the bitterest of cynics, a seemingly impossible conflict. But this is because INTJ types tend to believe that with effort, intelligence and consideration, nothing is impossible, while at the same time they believe that people are too lazy, short-sighted or self-serving to actually achieve those fantastic results. Yet that cynical view of reality is unlikely to stop an interested INTJ from achieving a result they believe to be relevant.";
							typeOverview += "\r\n\r\n";
							typeOverview += "INTJs radiate self-confidence and an aura of mystery, and their insightful observations, original ideas and formidable logic enable them to push change through with sheer willpower and force of personality. At times it will seem that INTJs are bent on deconstructing and rebuilding every idea and system they come into contact with, employing a sense of perfectionism and even morality to this work. Anyone who doesn’t have the talent to keep up with INTJs’ processes, or worse yet, doesn’t see the point of them, is likely to immediately and permanently lose their respect.";
							typeOverview += "\r\n\r\n";
							typeOverview += "Rules, limitations and traditions are anathema to the INTJ personality type – everything should be open to questioning and reevaluation, and if they see a way, INTJs will often act unilaterally to enact their technically superior, sometimes insensitive, and almost always unorthodox methods and ideas.";
							typeOverview += "\r\n\r\n";
							typeOverview += "This isn’t to be misunderstood as impulsiveness – INTJs will strive to remain rational no matter how attractive the end goal may be, and every idea, whether generated internally or soaked in from the outside world, must pass the ruthless and ever-present “Is this going to work?” filter. This mechanism is applied at all times, to all things and all people, and this is often where INTJ personality types run into trouble.";
							typeOverview += "\r\n\r\n";
							typeOverview += "INTJs are brilliant and confident in bodies of knowledge they have taken the time to understand, but unfortunately the social contract is unlikely to be one of those subjects. White lies and small talk are hard enough as it is for a type that craves truth and depth, but INTJs may go so far as to see many social conventions as downright stupid. Ironically, it is often best for them to remain where they are comfortable – out of the spotlight – where the natural confidence prevalent in INTJs as they work with the familiar can serve as its own beacon, attracting people, romantically or otherwise, of similar temperament and interests.";
							typeOverview += "\r\n\r\n";
							typeOverview += "INTJs are defined by their tendency to move through life as though it were a giant chess board, pieces constantly shifting with consideration and intelligence, always assessing new tactics, strategies and contingency plans, constantly outmaneuvering their peers in order to maintain control of a situation while maximizing their freedom to move about. This isn’t meant to suggest that INTJs act without conscience, but to many Feeling (F) types, INTJs’ distaste for acting on emotion can make it seem that way, and it explains why many fictional villains (and misunderstood heroes) are modeled on this personality type.";
							typeOverview += "\r\n\r\n";
							typeOverview += "";
							#endregion
							PlayerPrefs.SetString ("MBTI", "INTJ");

                        }
						//if intuition is less than sensing

                        else {
                            introverted = true;
                            intuitive = false;
                            thinking = true;
                            perceiving = false;
                            //RETURNING ISTJ
							#region ISTJ Personality Overview
							typeOverview += "ISTJ PERSONALITY (THE LOGISTICIAN)";
							typeOverview += "\r\n\r\n";
							typeOverview += "The ISTJ personality type is thought to be the most abundant, making up around 13% of the population. Their defining characteristics of integrity, practical logic and tireless dedication to duty make ISTJs a vital core to many families, as well as organizations that uphold traditions, rules and standards, such as law offices, regulatory bodies and military. People with the ISTJ personality type enjoy taking responsibility for their actions, and take pride in the work they do – when working towards a goal, ISTJs hold back none of their time and energy completing each relevant task with accuracy and patience.";
							typeOverview += "\r\n\r\n";
							typeOverview += "ISTJs don't make many assumptions, preferring instead to analyze their surroundings, check their facts and arrive at practical courses of action. ISTJ personalities are no-nonsense, and when they've made a decision, they will relay the facts necessary to achieve their goal, expecting others to grasp the situation immediately and take action. ISTJs have little tolerance for indecisiveness, but lose patience even more quickly if their chosen course is challenged with impractical theories, especially if they ignore key details – if challenges becomes time-consuming debates, ISTJs can become noticeably angry as deadlines tick nearer.";
							typeOverview += "\r\n\r\n";
							typeOverview += "When ISTJs say they are going to get something done, they do it, meeting their obligations no matter the personal cost, and they are baffled by people who don't hold their own word in the same respect. Combining laziness and dishonesty is the quickest way to get on ISTJs' bad side. Consequently, people with the ISTJ personality type often prefer to work alone, or at least have their authority clearly established by hierarchy, where they can set and achieve their goals without debate or worry over other's reliability.";
							typeOverview += "\r\n\r\n";
							typeOverview += "ISTJs have sharp, fact-based minds, and prefer autonomy and self-sufficiency to reliance on someone or something. Dependency on others is often seen by ISTJs as a weakness, and their passion for duty, dependability and impeccable personal integrity forbid falling into such a trap.";
							typeOverview += "\r\n\r\n";
							typeOverview += "This sense of personal integrity is core to ISTJs, and goes beyond their own minds – ISTJ personalities adhere to established rules and guidelines regardless of cost, reporting their own mistakes and telling the truth even when the consequences for doing so could be disastrous. To ISTJs, honesty is far more important than emotional considerations, and their blunt approach leaves others with the false impression that ISTJs are cold, or even robotic. People with this type may struggle to express emotion or affection outwardly, but the suggestion that they don't feel, or worse have no personality at all, is deeply hurtful.";
							typeOverview += "\r\n\r\n";
							typeOverview += "ISTJs' dedication is an excellent quality, allowing them to accomplish much, but it is also a core weakness that less scrupulous individuals take advantage of. ISTJs seek stability and security, considering it their duty to maintain a smooth operation, and they may find that their coworkers and significant others shift their responsibilities onto them, knowing that they will always take up the slack. ISTJs tend to keep their opinions to themselves and let the facts do the talking, but it can be a long time before observable evidence tells the whole story.";
							typeOverview += "\r\n\r\n";
							typeOverview += "ISTJs need to remember to take care of themselves – their stubborn dedication to stability and efficiency can compromise those goals in the long term as others lean ever-harder on them, creating an emotional strain that can go unexpressed for years, only finally coming out after it's too late to fix. If they can find coworkers and spouses who genuinely appreciate and complement their qualities, who enjoy the brightness, clarity and dependability that they offer, ISTJs will find that their stabilizing role is a tremendously satisfying one, knowing that they are part of a system that works.";
							typeOverview += "\r\n\r\n";
							typeOverview += "";
							#endregion
							PlayerPrefs.SetString ("MBTI", "ISTJ");

                        }
                    }
                }
            }
			//If extroverted
            else {
				//And FeTi user
                if (FeTi)
                {
					//And dominant feeler
                    if (domF)
                    {
						//The intuitive people of these types use NI and the sensing ones use NE, so add NI to intuition and NE to sensing to create better total
                        T.N += T.NI;
                        T.S += T.NE;
						//if intuition is greater than sensing

                        if (T.N > T.S)
                        {
                            introverted = false;
                            intuitive = true;
                            thinking = false;
                            perceiving = false;
                            //RETURNING ENFJ
							#region ENFJ Personality Overview
							typeOverview += "ENFJ PERSONALITY (THE PROTAGONIST)";
							typeOverview += "\r\n\r\n";
							typeOverview += "ENFJs are natural-born leaders, full of passion and charisma. Forming around two percent of the population, they are oftentimes our politicians, our coaches and our teachers, reaching out and inspiring others to achieve and to do good in the world. With a natural confidence that begets influence, ENFJs take a great deal of pride and joy in guiding others to work together to improve themselves and their community.";
							typeOverview += "\r\n\r\n";
							typeOverview += "People are drawn to strong personalities, and ENFJs radiate authenticity, concern and altruism, unafraid to stand up and speak when they feel something needs to be said. They find it natural and easy to communicate with others, especially in person, and their Intuitive (N) trait helps people with the ENFJ personality type to reach every mind, be it through facts and logic or raw emotion. ENFJs easily see people's motivations and seemingly disconnected events, and are able to bring these ideas together and communicate them as a common goal with an eloquence that is nothing short of mesmerizing.";
							typeOverview += "\r\n\r\n";
							typeOverview += "The interest ENFJs have in others is genuine, almost to a fault – when they believe in someone, they can become too involved in the other person's problems, place too much trust in them. Luckily, this trust tends to be a self-fulfilling prophesy, as ENFJs' altruism and authenticity inspire those they care about to become better themselves. But if they aren't careful, they can overextend their optimism, sometimes pushing others further than they're ready or willing to go.";
							typeOverview += "\r\n\r\n";
							typeOverview += "ENFJs are vulnerable to another snare as well: they have a tremendous capacity for reflecting on and analyzing their own feelings, but if they get too caught up in another person's plight, they can develop a sort of emotional hypochondria, seeing other people's problems in themselves, trying to fix something in themselves that isn't wrong. If they get to a point where they are held back by limitations someone else is experiencing, it can hinder ENFJs' ability to see past the dilemma and be of any help at all. When this happens, it's important for ENFJs to pull back and use that self-reflection to distinguish between what they really feel, and what is a separate issue that needs to be looked at from another perspective.";
							typeOverview += "\r\n\r\n";
							typeOverview += "ENFJs are genuine, caring people who talk the talk and walk the walk, and nothing makes them happier than leading the charge, uniting and motivating their team with infectious enthusiasm.";
							typeOverview += "\r\n\r\n";
							typeOverview += "People with the ENFJ personality type are passionate altruists, sometimes even to a fault, and they are unlikely to be afraid to take the slings and arrows while standing up for the people and ideas they believe in. It is no wonder that many famous ENFJs are US Presidents – this personality type wants to lead the way to a brighter future, whether it's by leading a nation to prosperity, or leading their little league softball team to a hard-fought victory.";
							typeOverview += "\r\n\r\n";
							typeOverview += "";
							#endregion
							PlayerPrefs.SetString ("MBTI", "ENFJ");

                        }
						//if intuition is less than sensing

                        else {
                            introverted = false;
                            intuitive = false;
                            thinking = false;
                            perceiving = false;
                            //RETURNING ESFJ
							#region ESFJ Personality Overview
							typeOverview += "ESFJ PERSONALITY (THE EXECUTIVE)";
							typeOverview += "\r\n\r\n";
							typeOverview += "People who share the ESFJ personality type are, for lack of a better word, popular – which makes sense, given that it is also a very common personality type, making up twelve percent of the population. In high school, ESFJs are the cheerleaders and the quarterbacks, setting the tone, taking the spotlight and leading their teams forward to victory and fame. Later in life, ESFJs continue to enjoy supporting their friends and loved ones, organizing social gatherings and doing their best to make sure everyone is happy.";
							typeOverview += "\r\n\r\n";
							typeOverview += "At their hearts, ESFJ personalities are social creatures, and thrive on staying up to date with what their friends are doing.";
							typeOverview += "\r\n\r\n";
							typeOverview += "Discussing scientific theories or debating European politics isn't likely to capture ESFJs' interest for too long. ESFJs are more concerned with fashion and their appearance, their social status and the standings of other people. Practical matters and gossip are their bread and butter, but ESFJs do their best to use their powers for good.";
							typeOverview += "\r\n\r\n";
							typeOverview += "ESFJs are altruists, and they take seriously their responsibility to help and to do the right thing. Unlike their Diplomat (NF) relatives however, people with the ESFJ personality type will base their moral compass on established traditions and laws, upholding authority and rules, rather than drawing their morality from philosophy or mysticism. It's important for ESFJs to remember though, that people come from many backgrounds and perspectives, and what may seem right to them isn't always an absolute truth.";
							typeOverview += "\r\n\r\n";
							typeOverview += "ESFJs love to be of service, enjoying any role that allows them to participate in a meaningful way, so long as they know that they are valued and appreciated. This is especially apparent at home, and ESFJs make loyal and devoted partners and parents. ESFJ personalities respect hierarchy, and do their best to position themselves with some authority, at home and at work, which allows them to keep things clear, stable and organized for everyone.";
							typeOverview += "\r\n\r\n";
							typeOverview += "Supportive and outgoing, ESFJs can always be spotted at a party – they're the ones finding time to chat and laugh with everyone! But their devotion goes further than just breezing through because they have to. ESFJs truly enjoy hearing about their friends' relationships and activities, remembering little details and always standing ready to talk things out with warmth and sensitivity. If things aren't going right, or there's tension in the room, ESFJs pick up on it and to try to restore harmony and stability to the group.";
							typeOverview += "\r\n\r\n";
							typeOverview += "Being pretty conflict-averse, ESFJs spend a lot of their energy establishing social order, and prefer plans and organized events to open-ended activities or spontaneous get-togethers. People with this personality type put a lot of effort into the activities they've arranged, and it's easy for ESFJs' feelings to be hurt if their ideas are rejected, or if people just aren't interested. Again, it's important for ESFJs to remember that everyone is coming from a different place, and that disinterest isn't a comment about them or the activity they've organized – it's just not their thing.";
							typeOverview += "\r\n\r\n";
							typeOverview += "Coming to terms with their sensitivity is ESFJs' biggest challenge – people are going to disagree and they're going to criticize, and while it hurts, it's just a part of life. The best thing for ESFJs to do is to do what they do best: be a role model, take care of what they have the power to take care of, and enjoy that so many people do appreciate the efforts they make.";
							typeOverview += "\r\n\r\n";
							typeOverview += "";
							#endregion
							PlayerPrefs.SetString ("MBTI", "ESFJ");

                        }
                    }
					//Not a dominant feeler
                    else {
						//The intuitive people of these types use NE and the sensing ones use NI, so add NE to intuition and NI to sensing to create better total

                        T.N += T.NE;
                        T.S += T.NI;
						//if intuition is greater than sensing

                        if (T.N > T.S)
                        {
                            introverted = false;
                            intuitive = true;
                            thinking = true;
                            perceiving = true;
                            //RETURNING ENTP
							#region ENTP Personality Overview
							typeOverview += "ENTP PERSONALITY (THE DEBATOR)";
							typeOverview += "\r\n\r\n";
							typeOverview += "The ENTP personality type is the ultimate devil's advocate, thriving on the process of shredding arguments and beliefs and letting the ribbons drift in the wind for all to see. Unlike their more determined Judging (J) counterparts, ENTPs don't do this because they are trying to achieve some deeper purpose or strategic goal, but for the simple reason that it's fun. No one loves the process of mental sparring more than ENTPs, as it gives them a chance to exercise their effortlessly quick wit, broad accumulated knowledge base, and capacity for connecting disparate ideas to prove their points.";
							typeOverview += "\r\n\r\n";
							typeOverview += "An odd juxtaposition arises with ENTPs, as they are uncompromisingly honest, but will argue tirelessly for something they don't actually believe in, stepping into another's shoes to argue a truth from another perspective.";
							typeOverview += "\r\n\r\n";
							typeOverview += "Playing the devil's advocate helps people with the ENTP personality type to not only develop a better sense of others' reasoning, but a better understanding of opposing ideas – since ENTPs are the ones arguing them.";
							typeOverview += "\r\n\r\n";
							typeOverview += "This tactic shouldn't be confused with the sort of mutual understanding Diplomats (NF) seek – ENTPs, like all Analyst (NT) personality types, are on a constant quest for knowledge, and what better way to gain it than to attack and defend an idea, from every angle, from every side?";
							typeOverview += "\r\n\r\n";
							typeOverview += "Taking a certain pleasure in being the underdog, ENTPs enjoy the mental exercise found in questioning the prevailing mode of thought, making them irreplaceable in reworking existing systems or shaking things up and pushing them in clever new directions. However, they'll be miserable managing the day-to-day mechanics of actually implementing their suggestions. ENTP personalities love to brainstorm and think big, but they will avoid getting caught doing the \"grunt work\" at all costs. ENTPs only make up about three percent of the population, which is just right, as it lets them create original ideas, then step back to let more numerous and fastidious personalities handle the logistics of implementation and maintenance.";
							typeOverview += "\r\n\r\n";
							typeOverview += "ENTPs' capacity for debate can be a vexing one – while often appreciated when it's called for, it can fall painfully flat when they step on others' toes by say, openly questioning their boss in a meeting, or picking apart everything their significant other says. This is further complicated by ENTPs' unyielding honesty, as this type doesn't mince words and cares little about being seen as sensitive or compassionate. Likeminded types get along well enough with people with the ENTP personality type, but more sensitive types, and society in general, are often conflict-averse, preferring feelings, comfort, and even white lies over unpleasant truths and hard rationality.";
							typeOverview += "\r\n\r\n";
							typeOverview += "This frustrates ENTPs, and they find that their quarrelsome fun burns many bridges, oftentimes inadvertently, as they plow through others' thresholds for having their beliefs questioned and their feelings brushed aside. Treating others as they'd be treated, ENTPs have little tolerance for being coddled, and dislike when people beat around the bush, especially when asking a favor. ENTP personalities find themselves respected for their vision, confidence, knowledge, and keen sense of humor, but often struggle to utilize these qualities as the basis for deeper friendships and romantic relationships.";
							typeOverview += "\r\n\r\n";
							typeOverview += "ENTPs have a longer road than most in harnessing their natural abilities – their intellectual independence and free-form vision are tremendously valuable when they're in charge, or at least have the ear of someone who is, but getting there can take a level of follow-through that ENTPs struggle with.";
							typeOverview += "\r\n\r\n";
							typeOverview += "Once they've secured such a position, ENTPs need to remember that for their ideas to come to fruition, they will always depend on others to assemble the pieces – if they've spent more time \"winning\" arguments than they have building consensus, many ENTPs will find they simply don't have the support necessary to be successful. Playing devil's advocate so well, people with this personality type may find that the most complex and rewarding intellectual challenge is to understand a more sentimental perspective, and to argue consideration and compromise alongside logic and progress.";
							typeOverview += "\r\n\r\n";
							typeOverview += "";
							#endregion
							PlayerPrefs.SetString ("MBTI", "ENTP");

                        }
						//if intuition is less than sensing

                        else {
                            introverted = false;
                            intuitive = false;
                            thinking = true;
                            perceiving = true;
                            //RETURNING ESTP
							#region ESTP Personality Overview
							typeOverview += "ESTP PERSONALITY (THE ENTREPRENEUR)";
							typeOverview += "\r\n\r\n";
							typeOverview += "ESTP personality types always have an impact on their immediate surroundings – the best way to spot them at a party is to look for the whirling eddy of people flitting about them as they move from group to group. Laughing and entertaining with a blunt and earthy humor, ESTP personalities love to be the center of attention. If an audience member is asked to come on stage, ESTPs volunteer – or volunteer a shy friend.";
							typeOverview += "\r\n\r\n";
							typeOverview += "Theory, abstract concepts and plodding discussions about global issues and their implications don't keep ESTPs interested for long. ESTPs keep their conversation energetic, with a good dose of intelligence, but they like to talk about what is – or better yet, to just go out and do it. ESTPs leap before they look, fixing their mistakes as they go, rather than sitting idle, preparing contingencies and escape clauses.";
							typeOverview += "\r\n\r\n";
							typeOverview += "ESTPs are the likeliest personality type to make a lifestyle of risky behavior. They live in the moment and dive into the action – they are the eye of the storm. People with the ESTP personality type enjoy drama, passion, and pleasure, not for emotional thrills, but because it's so stimulating to their logical minds. They are forced to make critical decisions based on factual, immediate reality in a process of rapid-fire rational stimulus response.";
							typeOverview += "\r\n\r\n";
							typeOverview += "This makes school and other highly organized environments a challenge for ESTPs. It certainly isn't because they aren't smart, and they can do well, but the regimented, lecturing approach of formal education is just so far from the hands-on learning that ESTPs enjoy. It takes a great deal of maturity to see this process as a necessary means to an end, something that creates more exciting opportunities.";
							typeOverview += "\r\n\r\n";
							typeOverview += "Also challenging is that to ESTPs, it makes more sense to use their own moral compass than someone else's. Rules were made to be broken. This is a sentiment few high school instructors or corporate supervisors are likely to share, and can earn ESTP personalities a certain reputation. But if they minimize the trouble-making, harness their energy, and focus through the boring stuff, ESTPs are a force to be reckoned with.";
							typeOverview += "\r\n\r\n";
							typeOverview += "With perhaps the most perceptive, unfiltered view of any type, ESTPs have a unique skill in noticing small changes. Whether a shift in facial expression, a new clothing style, or a broken habit, people with this personality type pick up on hidden thoughts and motives where most types would be lucky to pick up anything specific at all. ESTPs use these observations immediately, calling out the change and asking questions, often with little regard for sensitivity. ESTPs should remember that not everyone wants their secrets and decisions broadcast.";
							typeOverview += "\r\n\r\n";
							typeOverview += "Sometimes ESTPs' instantaneous observation and action is just what's required, as in some corporate environments, and especially in emergencies.";
							typeOverview += "\r\n\r\n";
							typeOverview += "If ESTPs aren't careful though, they may get too caught in the moment, take things too far, and run roughshod over more sensitive people, or forget to take care of their own health and safety. Making up only four percent of the population, there are just enough ESTPs out there to keep things spicy and competitive, and not so many as to cause a systemic risk.";
							typeOverview += "\r\n\r\n";
							typeOverview += "ESTPs are full of passion and energy, complemented by a rational, if sometimes distracted, mind. Inspiring, convincing and colorful, they are natural group leaders, pulling everyone along the path less traveled, bringing life and excitement everywhere they go. Putting these qualities to a constructive and rewarding end is ESTPs' true challenge.";
							typeOverview += "\r\n\r\n";
							typeOverview += "";
							#endregion
							PlayerPrefs.SetString ("MBTI", "ESTP");

                        }
                    }
                }
				//A FiTe user
                else {
					//And a dominant thinker
                    if (domT)
                    {
						//The intuitive people of these types use NI and the sensing ones use NE, so add NI to intuition and NE to sensing to create better total

                        T.N += T.NI;
                        T.S += T.NE;
						//if intuition is greater than sensing

                        if (T.N > T.S)
                        {
                            introverted = false;
                            intuitive = true;
                            thinking = true;
                            perceiving = false;
                            //RETURNING ENTJ
							#region ENTJ Personality Overview
							typeOverview += "ENTJ PERSONALITY (THE COMMANDER)";
							typeOverview += "\r\n\r\n";
							typeOverview += "ENTJs are natural-born leaders. People with this personality type embody the gifts of charisma and confidence, and project authority in a way that draws crowds together behind a common goal. But unlike their Feeling (F) counterpart, ENTJs are characterized by an often ruthless level of rationality, using their drive, determination and sharp minds to achieve whatever end they've set for themselves. Perhaps it is best that they make up only three percent of the population, lest they overwhelm the more timid and sensitive personality types that make up much of the rest of the world – but we have ENTJs to thank for many of the businesses and institutions we take for granted every day.";
							typeOverview += "\r\n\r\n";
							typeOverview += "If there's anything ENTJs love, it's a good challenge, big or small, and they firmly believe that given enough time and resources, they can achieve any goal. This quality makes people with the ENTJ personality type brilliant entrepreneurs, and their ability to think strategically and hold a long-term focus while executing each step of their plans with determination and precision makes them powerful business leaders. This determination is often a self-fulfilling prophecy, as ENTJs push their goals through with sheer willpower where others might give up and move on, and their Extroverted (E) nature means they are likely to push everyone else right along with them, achieving spectacular results in the process.";
							typeOverview += "\r\n\r\n";
							typeOverview += "At the negotiating table, be it in a corporate environment or buying a car, ENTJs are dominant, relentless, and unforgiving. This isn't because they are coldhearted or vicious per se – it's more that ENTJ personalities genuinely enjoy the challenge, the battle of wits, the repartee that comes from this environment, and if the other side can't keep up, that's no reason for ENTJs to fold on their own core tenet of ultimate victory.";
							typeOverview += "\r\n\r\n";
							typeOverview += "The underlying thought running through the ENTJ mind might be something like \"I don't care if you call me an insensitive meany, as long as I remain an efficient meany\".";
							typeOverview += "\r\n\r\n";
							typeOverview += "If there's anyone ENTJs respect, it's someone who is able to stand up to them intellectually, who is able to act with a precision and quality equal to their own. ENTJ personalities have a particular skill in recognizing the talents of others, and this helps in both their team-building efforts (since no one, no matter how brilliant, can do everything alone), and to keep ENTJs from displaying too much arrogance and condescension. However, they also have a particular skill in calling out others' failures with a chilling degree of insensitivity, and this is where ENTJs really start to run into trouble.";
							typeOverview += "\r\n\r\n";
							typeOverview += "Emotional expression isn't the strong suit of any Analyst (NT) type, but because of their Extroverted (E) nature, ENTJs' distance from their emotions is especially public, and felt directly by a much broader swath of people. Especially in a professional environment, ENTJs will simply crush the sensitivities of those they view as inefficient, incompetent or lazy. To people with the ENTJ personality type, emotional displays are displays of weakness, and it's easy to make enemies with this approach – ENTJs will do well to remember that they absolutely depend on having a functioning team, not just to achieve their goals, but for their validation and feedback as well, something ENTJs are, curiously, very sensitive to.";
							typeOverview += "\r\n\r\n";
							typeOverview += "ENTJs are true powerhouses, and they cultivate an image of being larger than life – and often enough they are. They need to remember though, that their stature comes not just from their own actions, but from the actions of the team that props them up, and that it's important to recognize the contributions, talents and needs, especially from an emotional perspective, of their support network. Even if they have to adopt a \"fake it ‘til you make it\" mentality, if ENTJs are able to combine an emotionally healthy focus alongside their many strengths, they will be rewarded with deep, satisfying relationships and all the challenging victories they can handle.\n\n";
							typeOverview += "\r\n\r\n";
							typeOverview += "";
							#endregion
							PlayerPrefs.SetString ("MBTI", "ENTJ");

                        }
						//if intuition is less than sensing

                        else {
                            introverted = false;
                            intuitive = false;
                            thinking = true;
                            perceiving = false;
                            //RETURNING ESFJ
							#region ESFJ Personality Overview
							typeOverview += "ESTJ PERSONALITY (THE CONSUL)";
							typeOverview += "\r\n\r\n";
							typeOverview += "ESTJs are representatives of tradition and order, utilizing their understanding of what is right, wrong and socially acceptable to bring families and communities together. Embracing the values of honesty, dedication and dignity, people with the ESTJ personality type are valued for their clear advice and guidance, and they happily lead the way on difficult paths. Taking pride in bringing people together, ESTJs often take on roles as community organizers, working hard to bring everyone together in celebration of cherished local events, or in defense of the traditional values that hold families and communities together.";
							typeOverview += "\r\n\r\n";
							typeOverview += "Demand for such leadership is high in democratic societies, and forming no less than 11% of the population, it's no wonder that many of America's presidents have been ESTJs. Strong believers in the rule of law and authority that must be earned, ESTJ personalities lead by example, demonstrating dedication and purposeful honesty, and an utter rejection of laziness and cheating, especially in work. If anyone declares hard, manual work to be an excellent way to build character, it is ESTJs.";
							typeOverview += "\r\n\r\n";
							typeOverview += "ESTJs are aware of their surroundings and live in a world of clear, verifiable facts – the surety of their knowledge means that even against heavy resistance, they stick to their principles and push an unclouded vision of what is and is not acceptable. Their opinions aren't just empty talk either, as ESTJs are more than willing to dive into the most challenging projects, improving action plans and sorting details along the way, making even the most complicated tasks seem easy and approachable.";
							typeOverview += "\r\n\r\n";
							typeOverview += "However, ESTJs don't work alone, and they expect their reliability and work ethic to be reciprocated – people with this personality type meet their promises, and if partners or subordinates jeopardize them through incompetence or laziness, or worse still, dishonesty, they do not hesitate to show their wrath. This can earn them a reputation for inflexibility, a trait shared by all Sentinels (SJ), but it's not because ESTJs are arbitrarily stubborn, but because they truly believe that these values are what make society work.";
							typeOverview += "\r\n\r\n";
							typeOverview += "ESTJs are classic images of the model citizen: they help their neighbors, uphold the law, and try to make sure that everyone participates in the communities and organizations they hold so dear.";
							typeOverview += "\r\n\r\n";
							typeOverview += "The main challenge for ESTJs is to recognize that not everyone follows the same path or contributes in the same way. A true leader recognizes the strength of the individual, as well as that of the group, and helps bring those individuals' ideas to the table. That way, ESTJs really do have all the facts, and are able to lead the charge in directions that work for everyone.";
							typeOverview += "\r\n\r\n";
							typeOverview += "";
							#endregion
							PlayerPrefs.SetString ("MBTI", "ESTJ");

                        }
                    }
					//Not a dominant thinker
                    else {
						//The intuitive people of these types use NE and the sensing ones use NI, so add NE to intuition and NI to sensing to create better total

                        T.N += T.NE;
                        T.S += T.NI;
						//if intuition is greater than sensing

                        if (T.N > T.S)
                        {
                            introverted = false;
                            intuitive = true;
                            thinking = false;
                            perceiving = true;
                            //RETURNING ENFP
							#region ENFP Personality Overview
							typeOverview += "ENFP PERSONALITY (THE CAMPAIGNER)";
							typeOverview += "\r\n\r\n";
							typeOverview += "The ENFP personality is a true free spirit. They are often the life of the party, but unlike Explorers, they are less interested in the sheer excitement and pleasure of the moment than they are in enjoying the social and emotional connections they make with others. Charming, independent, energetic and compassionate, the 7% of the population that they comprise can certainly be felt in any crowd.";
							typeOverview += "\r\n\r\n";
							typeOverview += "More than just sociable people-pleasers though, ENFPs, like all their Diplomat cousins, are shaped by their Intuitive (N) quality, allowing them to read between the lines with curiosity and energy. They tend to see life as a big, complex puzzle where everything is connected – but unlike Analysts, who tend to see that puzzle as a series of systemic machinations, ENFPs see it through a prism of emotion, compassion and mysticism, and are always looking for a deeper meaning.";
							typeOverview += "\r\n\r\n";
							typeOverview += "ENFPs are fiercely independent, and much more than stability and security, they crave creativity and freedom.";
							typeOverview += "\r\n\r\n";
							typeOverview += "Many other types are likely to find these qualities irresistible, and if they've found a cause that sparks their imagination, ENFPs will bring an energy that oftentimes thrusts them into the spotlight, held up by their peers as a leader and a guru – but this isn't always where independence-loving ENFPs want to be. Worse still if they find themselves beset by the administrative tasks and routine maintenance that can accompany a leadership position. ENFPs' self-esteem is dependent on their ability to come up with original solutions, and they need to know that they have the freedom to be innovative – they can quickly lose patience or become dejected if they get trapped in a boring role.";
							typeOverview += "\r\n\r\n";
							typeOverview += "Luckily, ENFPs know how to relax, and they are perfectly capable of switching from a passionate, driven idealist in the workplace to that imaginative and enthusiastic free spirit on the dance floor, often with a suddenness that can surprise even their closest friends. Being in the mix also gives them a chance to connect emotionally with others, giving them cherished insight into what motivates their friends and colleagues. They believe that everyone should take the time to recognize and express their feelings, and their empathy and sociability make that a natural conversation topic.";
							typeOverview += "\r\n\r\n";
							typeOverview += "The ENFP personality type needs to be careful, however – if they rely too much on their intuition, assume or anticipate too much about a friend's motivations, they can misread the signals and frustrate plans that a more straightforward approach would have made simple. This kind of social stress is the bugbear that keeps harmony-focused Diplomats awake at night. ENFPs are very emotional and sensitive, and when they step on someone's toes, they both feel it.";
							typeOverview += "\r\n\r\n";
							typeOverview += "ENFPs will spend a lot of time exploring social relationships, feelings and ideas before they find something that really rings true. But when they finally do find their place in the world, their imagination, empathy and courage are likely to produce incredible results.";
							typeOverview += "\r\n\r\n";
							typeOverview += "";
							#endregion
							PlayerPrefs.SetString ("MBTI", "ENFP");

                        }
						//if intuition is less than sensing

                        else {
                            introverted = false;
                            intuitive = false;
                            thinking = false;
                            perceiving = true;
                            //RETURNING ESFP
							#region ESFP Personality Overview
							typeOverview += "ESFP PERSONALITY (THE ENTERTAINER)";
							typeOverview += "\r\n\r\n";
							typeOverview += "If anyone is to be found spontaneously breaking into song and dance, it is the ESFP personality type. ESFPs get caught up in the excitement of the moment, and want everyone else to feel that way, too. No other personality type is as generous with their time and energy as ESFPs when it comes to encouraging others, and no other personality type does it with such irresistible style.";
							typeOverview += "\r\n\r\n";
							typeOverview += "Born entertainers, ESFPs love the spotlight, but all the world's a stage. Many famous people with the ESFP personality type are indeed actors, but they love putting on a show for their friends too, chatting with a unique and earthy wit, soaking up attention and making every outing feel a bit like a party. Utterly social, ESFPs enjoy the simplest things, and there's no greater joy for them than just having fun with a good group of friends.";
							typeOverview += "\r\n\r\n";
							typeOverview += "It's not just talk either – ESFPs have the strongest aesthetic sense of any personality type. From grooming and outfits to a well-appointed home, ESFP personalities have an eye for fashion. Knowing what's attractive the moment they see it, ESFPs aren't afraid to change their surroundings to reflect their personal style. ESFPs are naturally curious, exploring new designs and styles with ease.";
							typeOverview += "\r\n\r\n";
							typeOverview += "Though it may not always seem like it, ESFPs know that it's not all about them – they are observant, and very sensitive to others' emotions. People with this personality type are often the first to help someone talk out a challenging problem, happily providing emotional support and practical advice. However, if the problem is about them, ESFPs are more likely to avoid a conflict altogether than to address it head-on. ESFPs usually love a little drama and passion, but not so much when they are the focus of the criticisms it can bring.";
							typeOverview += "\r\n\r\n";
							typeOverview += "The biggest challenge ESFPs face is that they are often so focused on immediate pleasures that they neglect the duties and responsibilities that make those luxuries possible. Complex analysis, repetitive tasks, and matching statistics to real consequences are not easy activities for ESFPs. They'd rather rely on luck or opportunity, or simply ask for help from their extensive circle of friends. It is important for ESFPs to challenge themselves to keep track of long-term things like their retirement plans or sugar intake – there won't always be someone else around who can help to keep an eye on these things.";
							typeOverview += "\r\n\r\n";
							typeOverview += "ESFPs recognize value and quality, which on its own is a fine trait. In combination with their tendency to be poor planners though, this can cause them to live beyond their means, and credit cards are especially dangerous. More focused on leaping at opportunities than in planning out long-term goals, ESFPs may find that their inattentiveness has made some activities unaffordable.";
							typeOverview += "\r\n\r\n";
							typeOverview += "There's nothing that makes ESFPs feel quite as unhappy as realizing that they are boxed in by circumstance, unable to join their friends.";
							typeOverview += "\r\n\r\n";
							typeOverview += "ESFPs are welcome wherever there's a need for laughter, playfulness, and a volunteer to try something new and fun – and there's no greater joy for ESFP personalities than to bring everyone else along for the ride. ESFPs can chat for hours, sometimes about anything but the topic they meant to talk about, and share their loved ones' emotions through good times and bad. If they can just remember to keep their ducks in a row, they'll always be ready to dive into all the new and exciting things the world has to offer, friends in tow.";
							typeOverview += "\r\n\r\n";
							typeOverview += "";
							#endregion
							PlayerPrefs.SetString ("MBTI", "ESF");

                        }
                    }
                }
            }
			typeOverview += "\r\n\r\n<Scroll All The Way Down And Press Button To Move Back To Main Menu>";
			text += "TYPE MEANING (PAGE 1)\r\n\r\n";
			text += "You are an\r\n";
            if (introverted)
            {
				Debug.Log("I");
				text += "I";
				type += "I";
				PlayerPrefs.SetInt ("Mind", 2);
            }
            else {
				Debug.Log("E");
				text += "E";
				type += "E";
				PlayerPrefs.SetInt ("Mind", 1);
            }
            if (intuitive)
            {
				Debug.Log("N");
				text += "N";
				type += "N";
				PlayerPrefs.SetInt ("Energy", 1);
            }
            else {
				Debug.Log("S");
				text += "S";
				type += "S";
				PlayerPrefs.SetInt ("Energy", 2);
            }
            if (thinking)
            {
				Debug.Log("T");
				text += "T";
				type += "T";
				PlayerPrefs.SetInt ("Nature", 1);
            }
            else {
				Debug.Log("F");
				text += "F";
				type += "F";
				PlayerPrefs.SetInt ("Nature", 2);
            }
            if (perceiving)
            {
				Debug.Log("P");
				text += "P\r\n\r\n";
				type += "P";
				PlayerPrefs.SetInt ("Tactics", 2);
            }
            else {
				Debug.Log("J");
				text += "J\r\n\r\n";
				type += "J";
				PlayerPrefs.SetInt ("Tactics", 1);
            }
			text += "Page One (Trait Overview)\r\n\r\n";
			text += "These letters stand for ";
			if (introverted) {
				text += "Introverted, ";
			} else {
				text += "Extroverted, ";
			}

			if (intuitive) {
				text += "iNtuition, ";
			}
			else {
				text += "Sensing, ";
			}
			if (thinking) {
				text += "Thinking, and ";
			} else {
				text += "Feeling, and ";
			}
			if (perceiving) {
				text += "Perceiving.\r\n";
			} else {
				text += "Judging.\r\n";
			}

			text += "Each of these letters say something about your personality, as well as your personality as a whole.";

			text += "Introversion and Extroversion represent how we interact with our environment.";
			text += "\r\n\r\nExtraverted individuals sincerely enjoy engaging with the external world and recharge by communicating with other people, while introverts prefer to rely on themselves and their own inner world instead of seeking stimulation from the outside.";
			text += "\r\n\r\n";
			if (introverted) {
				text += type + "s are Introverted, and are usually self-sufficient, have little desire to make plenty of friends, prefer working with systems rather than people, and have relatively poor social skills.";
				text += "\r\nSocializing depletes " + type + "'s internal energy reserves quite quickly and they always need to be able to return to their home base to recharge when that happens.";
			} else {
				text += type + "s are Extroverted, and usually have  good social skills and feel recharged after spending time in the company of other people.";
			}
			text += "\r\n\r\nThis is not black and white, and everyone has a bit of both extroversion and introversion in them.";
			text += "\r\n\r\n";
			text += "The iNtuitive and Sensing traits represent how we see the world and what kind of information you focus on.";
			text += "\r\n\r\nIntuitive personality types are visionary, idealistic, more interested in ideas, and focused on novelty while sensing types are more interested in facts and observable things, and focused on the tried and tested.";
			text += "\r\n\r\n";
			if (intuitive) {
				text += type + "s are iNtuitive, and prefer to rely on their imagination, ideas and possibilities. They dream, fantasize and question why things happen the way they do, always feeling slightly detached from the actual, concrete world. They may observe other people and events, but their mind remains directed both inwards and somewhere beyond – always questioning, wondering and making connections. Intuitive types believe in novelty, in the open mind, and in never-ending improvement.";
			} else {
				text += type + "s are Sensing, focus on the actual world and things happening around them. They enjoy seeing, touching, feeling and experiencing. They want to keep their feet on the ground and focus on the present, instead of wondering why or when something might happen. Consequently, people with this trait tend to be better at dealing with facts, tools and concrete objects as opposed to brainstorming about possibilities or future events, handling abstract theories, or exploring fantasy scenarios. Sensing types are also significantly better at focusing on just one thing at a time instead of bursting with energy and juggling multiple activities.";
			}
			text += "\r\n\r\n";
			text += "The Thinking and Feeling traits represent how we make decisions and cope with emotions.";
			text += "\r\n\r\nPeople with the Thinking preference seek logic and rational arguments, relying on their head rather than the heart. People with the Feeling preference follow their hearts and emotions.";
			text += "\r\n\r\n";
			if (thinking) {
				text += type + "s are Thinking, and they trust and prioritize logic, relying on rational arguments and doing everything they can to keep their true feelings and emotions deep below the surface.";
			}
			else {
				text += type + "s are Feeling, and they trust and prioritize feelings, relying on moral and ethical arguments,and doing everything they can to stay true to their deeply held principles.";
			}
			text += "\r\n\r\n";
			text += "Lastly, the Judging and Perceiving traits represent how we approach planning and available options.";
			text += "\r\n\r\nPeople with the Judging preference do not like to keep their options open. Perceiving individuals are always scanning for opportunities and options, willing to jump at them at a moment’s notice.";
			text += "\r\n\r\n";

			if (perceiving) {
				text += type + "s are Perceiving, so they want to be able to look for alternative options, knowing that there is always a better way. This may lead to unfinished projects or missed deadlines, but " + type + "s would rather take that risk than lock themselves into a position where the existing commitments would limit their freedom.";
			}
			else {
				text += type + "s are Judging, so they are decisive, choose security over freedom to improvise, and usually find it difficult to cope with uncertainty.";
			}
			text += "\r\n\r\n<Scroll All The Way Down And Press Button To Move Forward>";

			//After calculating and writing, move clicks forward
			clicks++;
        }
        else if (clicks == 58)
        {
			canClick = false;
        }
    }
    void OnGUI()
    {
        GUI.skin = MainSkin;

		//Scales text based off of resolution selected.
        float rX, rY;
        float scale_width, scale_height;
        scale_width = 1316;
        scale_height = 740;
        rX = Screen.width / scale_width;
        rY = Screen.height / scale_height;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rX, rY, 1));

		//Text displayed based on amount of clicks
		if (clicks == 0) {
			//ADD Blackscreen/blockscreen
			GUI.Label (new Rect (60, 60, 1316 - 120, 740 - 120), "Welcome to College Life\r\n\r\n<Left Click To Move Text>");
		} else if (clicks == 1) {
			GUI.Label (new Rect (60, 60, 1316 - 120, 740 - 120), "We will walk you through the beginnings of this game and set you on your way.");
		} else if (clicks == 2) {
			GUI.Label (new Rect (60, 60, 1316 - 120, 740 - 120), "Please note that your every message, every choice, every decision is being monitored and tested."); 
		} else if (clicks == 3) {
			GUI.Label (new Rect (60, 60, 1316 - 120, 740 - 120), "Therefore, for accurate results you must make your decisions as accurately as you would in real life.");
		} else if (clicks == 5) {
			GUI.Box (new Rect (60, 740 - 120, 1316 - 120, 120), "Here we are. Let's go over the controls.");
		} else if (clicks == 6) {
			GUI.Box (new Rect (60, 740 - 120, 1316 - 120, 120), "Use the WASD keys to move, and move the mouse to look. Try it now.");
		} else if (clicks == 7) {
			GUI.Box (new Rect (60, 740 - 120, 1316 - 120, 120), "Very Good. Let's move on.");
		} else if (clicks == 8) {
			GUI.Box (new Rect (60, 740 - 120, 1316 - 120, 120), "You see that notification in the top left corner? That means you have a text message. Press tab to view it now.");
		} else if (clicks == 9) {
			GUI.Box (new Rect (60, 740 - 120, 1316 - 120, 120), "You are late to dinner with your girlfriend at your apartment. Press the button at the bottom to begin sending her a return message.");
		} else if (clicks == 10) {
			GUI.Box (new Rect (60, 740 - 120, 1316 - 120, 120), "Very good. The phone has more uses than just sending text messages, however. Click the home button at the bottom of the phone, or the back button twice.");
		} else if (clicks == 11) {
			GUI.Box (new Rect (60, 740 - 120, 1316 - 120, 120), "This is the home screen. It is fairly blank, but each app took hours to code, so please appreciate it. Click the music button on the bottom left.");
		} else if (clicks == 12) {
			GUI.Box (new Rect (60, 740 - 120, 1316 - 120, 120), "This is the music screen. Here you can select in-game music. The opening screen is sorted by genre. Select your genre, then pick the song you want to play.");
		} else if (clicks == 13) {
			GUI.Box (new Rect (60, 740 - 120, 1316 - 120, 120), "Very good. Now go back to the home screen.");
		} else if (clicks == 14) {
			GUI.Box (new Rect (60, 740 - 120, 1316 - 120, 120), "Now click the options button on the bottom right.");
		} else if (clicks == 15) {
			GUI.Box (new Rect (60, 740 - 120, 1316 - 120, 120), "Here you can edit in-game options, and also quit the game to the desktop and main menu. Also, pressing escape at any time will exit the game. Change what you want and press finalize, or go back to the home screen.");
		} else if (clicks == 16) {
			GUI.Box (new Rect (60, 740 - 120, 1316 - 120, 120), "That is it for the controls. Press tab to exit the phone menu.");
		} else if (clicks == 17) {
			GUI.Box (new Rect (60, 740 - 120, 1316 - 120, 120), "Time to meet with your girlfriend. The red box in front of you marks your destination to your apartment.");
		} else if (clicks == 18) {
			GUI.Box (new Rect (60, 740 - 120, 1316 - 120, 120), "Walk up to your chair at the table and press E to sit down.");
		} else if (clicks == 20) {
			GUI.Box (new Rect (60, 740 - 120, 1316 - 120, 120), "Time for bed. Find the red box in your room and go to sleep.");
		} else if (clicks == 21) {
			GUI.Label (new Rect (60, 60, 1316 - 120, 740 - 120), "Thursday, April 27 10:00 p.m\r\nYou go to sleep and prepare for a busy day tomorrow.");
		} else if (clicks == 24) {
			GUI.Box (new Rect (60, 740 - 120, 1316 - 120, 120), "Check your messages.");
		} else if (clicks == 25) {
			GUI.Box (new Rect (60, 740 - 120, 1316 - 120, 120), "Go to your roommates room, and talk to him.");
		} else if (clicks == 27) {
			GUI.Box (new Rect (60, 740 - 120, 1316 - 120, 120), "It's time to go to work. Leave out the front door.");
		} else if (clicks == 29) {
			GUI.Box (new Rect (60, 740 - 120, 1316 - 120, 120), "Check your messages.");
		} else if (clicks == 30) {
			GUI.Box (new Rect (60, 740 - 120, 1316 - 120, 120), "Go to your boss's office and sit down. (Door in front of you)");
		} else if (clicks == 32) {
			if (!T.interacting) {
				GUI.Box (new Rect (60, 740 - 120, 1316 - 120, 120), "Sit in the chair again when you have made a decision.");
			}
		} else if (clicks == 34) {
			GUI.Box (new Rect (60, 740 - 120, 1316 - 120, 120), "Sit down in the Human Resources Representative's office.");
		} else if (clicks == 37) {
			GUI.Box (new Rect (60, 740 - 120, 1316 - 120, 120), "Check your messages.");
		} else if (clicks == 38) {
			GUI.Box (new Rect (60, 740 - 120, 1316 - 120, 120), "Leave out the door of the office to head back to your apartment.");
		} else if (clicks == 39) {
			GUI.Box (new Rect (60, 740 - 120, 1316 - 120, 120), "Talk to your girlfriend.");
		} else if (clicks == 41) {
			GUI.Box (new Rect (60, 740 - 120, 1316 - 120, 120), "Time to start this group meeting. Move to the couch and press e to interact in the discussion.");
		} else if (clicks == 43) {

		} else if (clicks == 44) {
			GUI.Box (new Rect (60, 740 - 120, 1316 - 120, 120), "Time for bed. Find the red box in your room and go to sleep.");
		} else if (clicks == 45) {
			GUI.Label (new Rect (60, 60, 1316 - 120, 740 - 120), "Friday, April 28 10:00 p.m\r\nYou go to sleep and are excited for an uneventful weekend.");
		} else if (clicks == 47) {
			GUI.Box (new Rect (60, 740 - 120, 1316 - 120, 120), "It's the weekend! You have been so busy lately you forgot what it means to have free time.");
		} else if (clicks == 48) {
			GUI.Box (new Rect (60, 740 - 120, 1316 - 120, 120), "But today (FINALLY) you have a day with nothing significant that needs to be done. You can spend the day doing whatever you want and recharge your batteries.");
		} else if (clicks == 51) {
			GUI.Box (new Rect (60, 740 - 120, 1316 - 120, 120), "Read the note.");
		} else if (clicks == 54) {
			GUI.Box (new Rect (60, 740 - 120, 1316 - 120, 120), "Check your messages.");
		} else if (clicks == 55) {
			if (!T.interacting) {
				GUI.Box (new Rect (60, 740 - 120, 1316 - 120, 120), "....You need to think this out. Write in your journal (on the desk).");
			}
		}  else if (clicks == 56) {
			GUI.Label (new Rect (60, 60, 1316 - 120, 740 - 120), "Thank you for playing our game. We will now analyze your results and calculate your personality type from them.");
		} else if (clicks == 58) {
			scrollViewVector = GUI.BeginScrollView (new Rect (30, 30, 1296 - 60, 729 - 60), scrollViewVector, new Rect (0, 0, 1296 - 80, 7 * 729 + 20));
			//vScrollbarValue = GUI.BeginScrollView(new Rect(30, 30, 1296 - 60, 729 - 60), vScrollbarValue, new Rect(30,30, 1296 - 60, 729 - 60));
			GUI.TextArea (new Rect (10, 10, 1296 - 90, 729 * 7 - 170), text);
			if (GUI.Button (new Rect (10, 729 * 7 - 159, 1296 - 90, 180), "Press to Return to move to the next page.")) {
				//PlayerPrefs.SetInt("Games Played", PlayerPrefs.GetInt("Games Played") + 5);
				//PlayerPrefs.Save();
				//Application.LoadLevel(1);
				//UnityEngine.SceneManagement.SceneManager.LoadScene(1);
				clicks++;
				scrollViewVector = Vector2.zero;
			}
			GUI.EndScrollView ();
		} else if (clicks == 59) {
			scrollViewVector = GUI.BeginScrollView (new Rect (30, 30, 1296 - 60, 729 - 60), scrollViewVector, new Rect (0, 0, 1296 - 80, 8 * 729 + 20));
			//vScrollbarValue = GUI.BeginScrollView(new Rect(30, 30, 1296 - 60, 729 - 60), vScrollbarValue, new Rect(30,30, 1296 - 60, 729 - 60));
			GUI.TextArea (new Rect (10, 10, 1296 - 90, 729 * 8 - 170), typeOverview);
			if (GUI.Button (new Rect (10, 729 * 8 - 159, 1296 - 90, 180), "Press to Return to move to the next page.")) {
				if (PlayerPrefs.GetInt ("Games Played") <= 4) {
					PlayerPrefs.SetInt ("Games Played", PlayerPrefs.GetInt ("Games Played") + 5);
				}
				PlayerPrefs.Save();
				UnityEngine.SceneManagement.SceneManager.LoadScene (1);
			}
			GUI.EndScrollView ();
		}
    }
}
