using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {



    GameObject player, Sidecheck, master;
    FPSInputController FPS;
    MouseLook[] ML;
     GameObject maincam, Sidecam;
     bool insidex = false;
     public int step;
    public int clicks;
    bool canClick;
    Camera[] cams;
    GameObject[] checkPoints;
    public GUISkin textSkin;

    CharacterMotor CM;
    #region Step0Variables
    #region BlackScreenInitiations
    GameObject BS;
    Renderer rend;
    Color color;
    #endregion
    #region pathInitiations
    float speed = 2.5f;
    bool turnedOn = true;
    public GameObject LookShort, LookLong;
    bool lookShort = false, lookLong = false;
    string[] paths = { "Long", "Short" };
    int pathNum = 0;
    public Texture2D rightArrow, leftArrow;
    GameObject[] shortObjects;
    GameObject[] longObjects;
    #endregion
    #endregion
    int redFlowers = 0, whiteFlowers = 0;

	GameObject checks;

    // Use this for initialization
    void Start()
    {
        if (!GameObject.ReferenceEquals(GameObject.Find("GameHandler"), null))
        {
            if (GameObject.Find("GameHandler").GetComponent<StayOnForever>().inside != 1)
            {
                step = 0;
                clicks = 0;
                canClick = true;
                player = GameObject.FindGameObjectWithTag("Player");
                switchMovement();
                maincam = GameObject.FindGameObjectWithTag("MainCamera");
                master = GameObject.Find("Steps");
                CM = player.GetComponent<CharacterMotor>();

                cams = master.GetComponentsInChildren<Camera>();
                checkPoints = GameObject.FindGameObjectsWithTag("CheckPoint");
                Sidecheck = new GameObject("Placeholder");
				checkPoints [checkPoints.Length - 3].SetActive (false);

                #region Step0Start
                BS = GameObject.Find("PlayerCapsule/Main Camera/BlockScreen");
                rend = BS.GetComponent<Renderer>();
                color = rend.material.color;
                player.transform.LookAt(LookShort.transform);
                shortObjects = GameObject.FindGameObjectsWithTag("Short");
                foreach (GameObject go in shortObjects)
                {
                    go.SetActive(false);
                }
                longObjects = GameObject.FindGameObjectsWithTag("Long");
                foreach (GameObject go2 in longObjects)
                {
                    go2.SetActive(false);
                }

				//checks = GameObject.Find("Steps/Step2");
				//checks.SetActive(false);
                #endregion
            }
        }
    }

    // Update is called once per frame
    void Update() {
        if (!GameObject.ReferenceEquals(GameObject.Find("GameHandler"), null))
        {
            if (GameObject.Find("GameHandler").GetComponent<StayOnForever>().inside != 1)
            {
                #region Step0
                if (step == 0)
                {
                    if (clicks == 2)
                    #region BlackScreenFade
                    {
                        canClick = false;
                        color.a -= .0035f;
                        if (color.a < .3)
                        {
                            color.a = 0;
                            canClick = true;
                            clicks++;
                            BS.SetActive(false);
                        }
                        rend.material.color = color;
                    }
                    #endregion
                    else if (clicks == 3 || lookShort)
                    #region LookAtShort
                    {
                        //player.transform.LookAt(LookShort.transform);
                        Vector3 dir = LookShort.transform.position - player.transform.position;
                        dir.y = 0; // keep the direction strictly horizontal
                        Quaternion rot = Quaternion.LookRotation(dir);
                        // slerp to the desired rotation over time
                        player.transform.rotation = Quaternion.Slerp(player.transform.rotation, rot, speed * Time.deltaTime);
                    }
                    #endregion
                    else if (clicks == 4 || lookLong)
                    #region LookAtLong
                    {
                        //player.transform.LookAt(LookLong.transform);
                        Vector3 dir = LookLong.transform.position - player.transform.position;
                        dir.y = 0; // keep the direction strictly horizontal
                        Quaternion rot = Quaternion.LookRotation(dir);
                        // slerp to the desired rotation over time
                        player.transform.rotation = Quaternion.Slerp(player.transform.rotation, rot, speed * Time.deltaTime);
                    }
                    #endregion
                    if (clicks == 5)
                    {
                        #region switchPathView
                        if (pathNum == 0)
                        {
                            lookLong = true;
                            lookShort = false;
                        }
                        else
                        {
                            lookShort = true;
                            lookLong = false;
                        }
                        canClick = false;
                        #endregion
                    }
                }
                #endregion
                #region Step1
                if (step == 1)
                {
                    if (clicks == 1)
                    {
                        canClick = false;
                        switchMovement();
                        clicks++;
                    }
                    if (player.gameObject.transform.position.x > Sidecheck.transform.position.x - Sidecheck.transform.lossyScale.x / 2 && player.gameObject.transform.position.x < Sidecheck.transform.position.x + Sidecheck.transform.lossyScale.x / 2
                    && player.gameObject.transform.position.z > Sidecheck.transform.position.z - Sidecheck.transform.lossyScale.z / 2 && player.gameObject.transform.position.z < Sidecheck.transform.position.z + Sidecheck.transform.lossyScale.z / 2
                    && insidex == false)
                    {
                        //maincam.SetActive(false);
                        Sidecam.SetActive(true);
                        insidex = true;
                        switchMovement();
                        //player.SetActive(false);
                        maincam.SetActive(false);
                        CM.enabled = false;
                        Sidecheck.SetActive(false);
                        clicks++;
                    }
                    if (clicks == 3)
                    {
                        canClick = true;
                    }
                    else if (clicks == 4)
                    {
                        canClick = false;
                    }
                }
                #endregion
                if (step == 2)
                {
					//checks.SetActive (true);

                    if (player.gameObject.transform.position.x > Sidecheck.transform.position.x - Sidecheck.transform.lossyScale.x / 2 && player.gameObject.transform.position.x < Sidecheck.transform.position.x + Sidecheck.transform.lossyScale.x / 2
                    && player.gameObject.transform.position.z > Sidecheck.transform.position.z - Sidecheck.transform.lossyScale.z / 2 && player.gameObject.transform.position.z < Sidecheck.transform.position.z + Sidecheck.transform.lossyScale.z / 2
                    && insidex == false)
                    {
                        //clicks++;
                        PlayerPrefs.Save();
                        //Application.LoadLevel(4);
                        UnityEngine.SceneManagement.SceneManager.LoadScene(4);
                    }

                }
                if (Input.GetMouseButtonDown(0) && canClick)
                {
                    clicks++;
                }
            }
        }
    }
    void OnGUI()
    {
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

        #region Step0GUI
        if (step == 0)
        {
            if (clicks == 0)
            {
                GUI.Label(new Rect(30, 30, 1296 - 60, 729 - 60), "Imagine you're walking to your girlfriend's house.");
                GUI.Label(new Rect(30, (729 / 2) + 100, 1296 - 60, 120), "(Left-Click mouse to continue)");
            }
            else  if (clicks == 1)
            {
                GUI.Label(new Rect(30, 30, 1296 - 60, 729 - 60), "There are two roads to get there.");
            }
            else if (clicks == 3)
            {
                GUI.Box(new Rect(30, 729 - 190, 1296 - 60, 180), "One is a straight path which takes you there quickly, but is very plain and boring.");
            }
            else if (clicks == 4)
            {
                GUI.Box(new Rect(30, 729 - 280, 1296 - 60, 270), "The other is curvy and full of wonderful sights on the way, but takes quite a while to reach your loved one's house.");
            }
            else if (clicks == 5)
            {
                GUI.Box(new Rect(60, 729 - 200, 1296 - 120, 90), "Which path do you choose?");
                GUI.Box(new Rect(120, 729 - 100, 1296 - 470, 90), paths[pathNum]);
                if (GUI.Button(new Rect(1296 - 345, 729 - 75, 50, 50), rightArrow))
                {
                    if (pathNum == 1)
                        pathNum = 0;
                    else
                        pathNum++;
                }
                if (GUI.Button(new Rect(65, 729 - 75, 50, 50), leftArrow))
                {
                    if (pathNum != 0)
                        pathNum--;
                    else
                        pathNum = 1;
                }
                if (GUI.Button(new Rect(1296 - 280, 729 - 100, 260, 90), "This Path!"))
                {
                    //switchMovement();
                    step++;
                    canClick = true;
                    clicks = 0;
                    if (pathNum == 0)
                    {
                        PlayerPrefs.SetInt("First Path Chosen", 2);
                        
                        foreach (GameObject go2 in longObjects)
                        {
                            go2.SetActive(true);
                        }
                    }
                    else
                    {
                        PlayerPrefs.SetInt("First Path Chosen", 1);
                        foreach (GameObject go in shortObjects)
                        {
                            go.SetActive(true);
                        }
                    }
                    renew();
                }
            }
        }
        #endregion
        #region Step1GUI
        if (step == 1)
        {
            if (clicks == 0)
            {
                GUI.Box(new Rect(30, 729 - 190, 1296 - 60, 180), "Next, follow the path until you reach the glowing area.");
            }
            else if (clicks == 3)
            {
                GUI.Box(new Rect(30, 729 - 280, 1296 - 60, 270), "You notice these two rose bushes on your path. One is full of white roses. One is full of red roses. You decide to pick 20 roses for your boyfriend/girlfriend.");
            }
            else if (clicks == 4)
            {
                GUI.Box(new Rect(60, 729 - 100, 1296 - 120, 90), "What color combination do you choose?");
                GUI.Label(new Rect(1300 / 2 + 120, 729 - 250, 376, 150), redFlowers.ToString());
                redFlowers = (int)GUI.HorizontalSlider(new Rect(1300 / 2 + 200, 729 - 120, 250, 40), redFlowers, 0, 20 - whiteFlowers);
                GUI.Label(new Rect(120, 729 - 250, 376, 150), whiteFlowers.ToString());
                whiteFlowers = (int)GUI.HorizontalSlider(new Rect(200, 729 - 120, 250, 40), whiteFlowers, 0, 20 - redFlowers);
                GUI.Label(new Rect(0, 0, 1296, 100), "You need a total of 20 flowers!");
                if (GUI.Button(new Rect(120 + 376 + 25, 729 - 190, 260, 90), "This combination!"))
                {
                    if (redFlowers + whiteFlowers == 20)
                    {
                        float redFlowerFloat = redFlowers;
                        float whiteFlowerFloat = whiteFlowers;
                        float redFlowerPercentage = ((redFlowerFloat * 100)/(redFlowerFloat + whiteFlowerFloat)) + 1;
                        float whiteFlowerPercentage = ((whiteFlowerFloat * 100) / (redFlowerFloat + whiteFlowerFloat)) + 1;
                        PlayerPrefs.SetInt("Red Flowers", redFlowers);
                        PlayerPrefs.SetInt("White Flowers", whiteFlowers);
                        PlayerPrefs.SetFloat("Red Flower Percentage", redFlowerPercentage);
                        PlayerPrefs.SetFloat("White Flower Percentage", whiteFlowerPercentage);
                        //Debug.Log(PlayerPrefs.GetFloat("Red Flower Percentage") - 1);
                        Sidecam.SetActive(false);
                        //player.SetActive(true);
                        maincam.SetActive(true);
                        CM.enabled = true;
                        switchMovement();
                        //maincam.SetActive(true);
                        step++;
                        clicks = 0;
                        insidex = false;
						checkPoints[checkPoints.Length-3].SetActive(true);
                        renew();
                    }

                }
            }
            //GUI.Label(new Rect(0, 0, 1296, 729), "I hate you");
        }
        #endregion
        if (step == 2)
        {
            if (clicks == 0)
            {
                GUI.Box(new Rect(30, 729 - 190, 1296 - 60, 180), "Next, follow the path until you reach the glowing area.");
            }
        }
    }
    void renew()
    {
        foreach (Camera c in cams)
        {
            if (c.gameObject.layer == 14 + step && c.gameObject.activeInHierarchy)
            {
                Sidecam = c.gameObject;
            }
            c.gameObject.SetActive(false);
        }
            foreach (GameObject g in checkPoints)
            {
                if (g.layer == 14 + step && g.activeInHierarchy)
                {
                    Sidecheck = g;
                }
            }
    }
    void switchMovement()
    {
        FPS = player.GetComponent<FPSInputController>();
        ML = player.GetComponentsInChildren<MouseLook>();
        if (turnedOn)
        {
            FPS.enabled = false;
            foreach (MouseLook ml in ML)
                ml.enabled = false;
            turnedOn = false;
        }
        else
        {
            FPS.enabled = true;
            foreach (MouseLook ml in ML)
                ml.enabled = true;
            turnedOn = true;
        }
    }
}
