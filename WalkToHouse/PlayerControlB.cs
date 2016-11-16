using UnityEngine;
using System.Collections;

public class PlayerControlB : MonoBehaviour {
    /*
     * Player Controller and Story progressor for the inside scene of her house game
     * */
	public int step; 
    public int clicks; //Click counter that determines where in the story we are.
    bool canClick; //Whether or not we can click text forward
    public GUISkin textSkin; //skin for appropriate look
    GameObject player; //the player
    
    GameObject maid; //The maid
    Maid2 m; //Script that runs maid
    CharacterMotor CM;
    FPSInputController FPS;
    MouseLook[] ML; //Mouse look arrays. 
    bool turnedOn;

    bool onBed, onWindow; //Whether roses are placed on window or bed
    string[] places = { "Window sill", "Bed" }; //String array for each placement
    int placeNum = 0; //What place is selected
    public Texture2D rightArrow, leftArrow; //Left and right arrow textures

    GameObject roses1, roses2; //Both roses, depending on which place is selected, one will be set to true and the other false
    GameObject[] cps; //Checkpoint GameObject array
    public GameObject cp4, cp5, cp6, cp7;
    float speed = 2.5f;

    GameObject BS;
    GameObject girlfriend, replacement; //Girlfriend GameObjects 

    string[] animss = { "Sleeping", "Awake" };//String array for each state
    int animssNum = 0; //What animation is selected.
    float y1 = 182.85f, y2 = .31f, x1 = 1.2f, x2 = 183.52f; //amounts for sleeping/awake movement. Based off of location for animations


    //CharacterMotor CM;
    // Use this for initialization
    void Start () {
        turnedOn = true;
        step = 3;
        clicks = 0;
        canClick = false;
        onBed = false;
        onWindow = false;
        player = GameObject.FindGameObjectWithTag("Player");
        maid = GameObject.Find("Maid");
        m = maid.GetComponent<Maid2>();
        cps = GameObject.FindGameObjectsWithTag("CheckPoint");
        cp4 = new GameObject("PlaceHolder");
        cp5 = new GameObject("PlaceHolder1");
        cp6 = new GameObject("PlaceHolder2");
        cp7 = new GameObject("PlaceHolder3");
        foreach (GameObject cp in cps)
        {
            if (cp.layer == 18)
                cp4 = cp;
            else if (cp.layer == 19)
            {
                cp5 = cp;
            }
            else if (cp.layer == 20)
            {
                cp6 = cp;
            }
            else if (cp.layer == 21)
            {
                cp7 = cp;
            }
        }
        cp4.SetActive(false);
        cp5.SetActive(false);
        cp6.SetActive(false);
        cp7.SetActive(false);
        roses1 = GameObject.Find("Holder/TotalBundle");
        roses1.SetActive(false);
        roses2 = GameObject.Find("Holder/TotalBundle (1)");
        roses2.SetActive(false);

        BS = GameObject.Find("PlayerCapsule/Main Camera/BlockScreen");
        BS.SetActive(false);
        girlfriend = GameObject.FindGameObjectWithTag("Girlfriend");
        replacement = GameObject.Find("Replacement");
        replacement.SetActive(false);


        CM = player.GetComponent<CharacterMotor>();
    }

    // Update is called once per frame
    void Update()
    {
		//If can click, move clicks forward
        if (Input.GetMouseButtonDown(0) && canClick)
        {
            clicks++;
        }
        if (m.playerFinished == true && step == 3)
        {
            step++;
        }
        if (step == 4)
        {            
            if (clicks == 1)
            {
                cp4.SetActive(true);
                if (player.gameObject.transform.position.x > cp4.transform.position.x - cp4.transform.lossyScale.x / 2 && player.gameObject.transform.position.x < cp4.transform.position.x + cp4.transform.lossyScale.x / 2
            && player.gameObject.transform.position.z > cp4.transform.position.z - cp4.transform.lossyScale.z / 2 && player.gameObject.transform.position.z < cp4.transform.position.z + cp4.transform.lossyScale.z / 2)
                { 
                    switchMovement();
                    clicks++;
                    
                }
            }
            if (clicks == 2)
            {
                cp4.SetActive(false);
                canClick = true;
            }
            if (clicks == 3)
            {
                canClick = false;
                if (placeNum == 0)
                {
                    onWindow = true;
                    onBed = false;
                }
                else
                {
                    onWindow = false;
                    onBed = true;
                }
            }

            if (onBed)
            {
				//If on bed, face the bed and set roses on window to false while roses on bed to true
                roses2.SetActive(true);
                roses1.SetActive(false);
                Vector3 dir = roses2.transform.position - player.transform.position;
                dir.y = 0; // keep the direction strictly horizontal
                Quaternion rot = Quaternion.LookRotation(dir);
                // slerp to the desired rotation over time
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, rot, speed * Time.deltaTime);
            }
            else if (onWindow)
            {
				//If on window, face the window and set roses on window to true while roses on bed to false
                roses1.SetActive(true);
                roses2.SetActive(false);
                Vector3 dir = roses1.transform.position - player.transform.position;
                dir.y = 0; // keep the direction strictly horizontal
                Quaternion rot = Quaternion.LookRotation(dir);
                // slerp to the desired rotation over time
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, rot, speed * Time.deltaTime);
            }
            
        }
        if (step == 5)
        {
			//turns on blackscreen, turns off movement, and moves step forward.
            cp5.SetActive(true);
            if (player.gameObject.transform.position.x > cp5.transform.position.x - cp5.transform.lossyScale.x / 2 && player.gameObject.transform.position.x < cp5.transform.position.x + cp5.transform.lossyScale.x / 2
            && player.gameObject.transform.position.z > cp5.transform.position.z - cp5.transform.lossyScale.z / 2 && player.gameObject.transform.position.z < cp5.transform.position.z + cp5.transform.lossyScale.z / 2)
            {
                switchMovement();
                clicks = 0;
                BS.SetActive(true);
                step++;
            }

            girlfriend.SetActive(false);
            
        }
        if (step == 6)
        {
            replacement.SetActive(true);
            cp5.SetActive(false);
            Renderer rend = BS.GetComponent<Renderer>();
            Color color = rend.material.color;
            if (clicks == 0)
            {
                canClick = true;
            }
            if (clicks == 1)
            {
				//Fade screen at one clicks
                canClick = false;
                color.a -= .0025f;
                if (color.a < .3)
                {
                    color.a = 0;
                    canClick = true;
                    clicks++;
                    BS.SetActive(false);
                    switchMovement();
                }
                rend.material.color = color;
            }
            if (clicks == 2)
            {
				//Turn off movement and move clicks forward
                canClick = false;
                cp6.SetActive(true);
                if (player.gameObject.transform.position.x > cp6.transform.position.x - cp6.transform.lossyScale.x / 2 && player.gameObject.transform.position.x < cp6.transform.position.x + cp6.transform.lossyScale.x / 2
                && player.gameObject.transform.position.z > cp6.transform.position.z - cp6.transform.lossyScale.z / 2 && player.gameObject.transform.position.z < cp6.transform.position.z + cp6.transform.lossyScale.z / 2)
                {
                    switchMovement();
                    //clicks = 0;
                    //BS.SetActive(true);
                    //step++;
                    clicks++;
                }
            }
            if (clicks == 3)
            {
				//Set animation and location based on what is selected in the text edior
                cp6.SetActive(false);
                Animator anim2 = replacement.GetComponent<Animator>();
                if (animssNum == 0)
                {
                    anim2.SetBool("isSleeping", true);
                    anim2.SetBool("isAwake", false);

                    Vector3 temp = replacement.transform.position;
                    temp.y = x1;
                    temp.x = y1;
                    replacement.transform.position = temp;
                }
                else if (animssNum == 1)
                {
                    anim2.SetBool("isSleeping", false);
                    anim2.SetBool("isAwake", true);
                    Vector3 temp = replacement.transform.position;
                    temp.y = y2;
                    temp.x = x2;
                    replacement.transform.position = temp;
                }
            }
        }
        if (step == 7)
        {
			//If within box, load next scene
            cp7.SetActive(true);
            if (player.gameObject.transform.position.x > cp7.transform.position.x - cp7.transform.lossyScale.x / 2 && player.gameObject.transform.position.x < cp7.transform.position.x + cp7.transform.lossyScale.x / 2
            && player.gameObject.transform.position.z > cp7.transform.position.z - cp7.transform.lossyScale.z / 2 && player.gameObject.transform.position.z < cp7.transform.position.z + cp7.transform.lossyScale.z / 2)
            {
                PlayerPrefs.Save();
                if (!GameObject.ReferenceEquals(GameObject.Find("GameHandler"), null))
                {
                    //Application.LoadLevel(3);
                    UnityEngine.SceneManagement.SceneManager.LoadScene(3);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            clicks++;
        }
    }
    void OnGUI()
    {
		//Scale based off of resolution
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
		//Based on step and clicks, display text
        if (step == 3)
        {
            if (clicks == 0)
            {
                GUI.Box(new Rect(60, 729 - 200, 1296 - 120, 180), "Talk to the maid. You can talk by left-clicking your mouse when near her.");
            }
        }
        if (step == 4)
        {
            if (clicks == 0)
            {
                GUI.Box(new Rect(60, 729 - 100, 1296 - 120, 90), "Talk to your girlfiend.");
            }
            else if (clicks == 1)
            {
                GUI.Box(new Rect(60, 729 - 100, 1296 - 120, 90), "Go to her Room");
            }
            else if (clicks == 2)
            {
                GUI.Box(new Rect(30, 729 - 280, 1296 - 60, 270), "Drop off the roses. You can leave the roses by the window sill, or on the bed.");
            }
            else if (clicks == 3)
            {
                GUI.Box(new Rect(60, 729 - 200, 1296 - 120, 90), "Where do you put the roses?");
                GUI.Box(new Rect(120, 729 - 100, 1296 - 470, 90), places[placeNum]);
                if (GUI.Button(new Rect(1296 - 345, 729 - 75, 50, 50), rightArrow))
                {
                    if (placeNum == 1)
                        placeNum = 0;
                    else
                        placeNum++;
                }
                if (GUI.Button(new Rect(65, 729 - 75, 50, 50), leftArrow))
                {
                    if (placeNum != 0)
                        placeNum--;
                    else
                        placeNum = 1;
                }
                if (GUI.Button(new Rect(1296 - 280, 729 - 100, 260, 90), "This Place!"))
                {
                    switchMovement();
                    step++;
                    canClick = true;
                    if (placeNum == 0)
                    {
                        PlayerPrefs.SetInt("Place Chosen", 2);

                       
                    }
                    else
                    {
                        PlayerPrefs.SetInt("Place Chosen", 1);
                        
                    }
                    clicks = 0;
                }

            }
        }
        if (step == 5)
        {
            if (clicks == 0)
            {
                GUI.Box(new Rect(30, 729 - 280, 1296 - 60, 270), "It's time for bed, go to the guest room located behind you.");
            }
        }
        if (step == 6)
        {
            if (clicks == 0)
            {
                GUI.Label(new Rect(30, 30, 1296 - 60, 729 - 60), "It's morning time, you should check on your girlfriend in her room before you go.");
            }
            if (clicks == 2)
            {
                GUI.Box(new Rect(30, 729 - 100, 1296 - 60, 90), "Check on girlfriend");
            }
            if (clicks == 3)
            {
                GUI.Box(new Rect(60, 729 - 200, 1296 - 120, 90), "Is she awake or asleep?");
                GUI.Box(new Rect(120, 729 - 100, 1296 - 470, 90), animss[animssNum]);
                if (GUI.Button(new Rect(1296 - 345, 729 - 75, 50, 50), rightArrow))
                {
                    if (animssNum == 1)
                        animssNum = 0;
                    else
                        animssNum++;
                }
                if (GUI.Button(new Rect(65, 729 - 75, 50, 50), leftArrow))
                {
                    if (animssNum != 0)
                        animssNum--;
                    else
                        animssNum = 1;
                }
                if (GUI.Button(new Rect(1296 - 280, 729 - 100, 260, 90), "Just Like This!"))
                {
                    switchMovement();
                    step++;
                    canClick = true;
                    if (animssNum == 0)
                    {
                        PlayerPrefs.SetInt("Girlfriend State", 1);


                    }
                    else
                    {
                        PlayerPrefs.SetInt("Girlfriend State", 2);

                    }
                    clicks = 0;
                }
            }
        }
        if (step == 7)
        {
            GUI.Box(new Rect(30, 729 - 100, 1296 - 60, 90), "Leave Out the Front Door.");

        }
    }
	//when called, turns movement off/on based on what it is now.
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
