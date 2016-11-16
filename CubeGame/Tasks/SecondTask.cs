using UnityEngine;
using System.Collections;
using System;

public class SecondTask : MonoBehaviour
{
	/*
	 * Script handling the editing of the ladder.
	 * */
    GameObject cube, tasks;
    GUICubeScript GCS;
    TaskScript TS;
    public GameObject[] ladders = new GameObject[3]; //The three ladders of three different types
    public GameObject RP;
    public bool doneSecond; //If we are done editing ladder for first time
    String[] lTypes = { "Extension Ladder", "Step Ladder", "Platform Ladder" }; //Ladder types available
    public int texL; //Which ladder type is selected
    public Texture2D rightArrow, leftArrow; //Texture for right and left arrows
	public String lpX, lpY, lpZ; //Position strings for ladder
    public Vector3 startPos; //The start position of the ladder
    // Use this for initialization

	//Instantiating the components needed for controlling the material of the ladders
	public Material[] lMat = new Material[5]; //Materials for ladders
	String[] mSets = {"None", "New Metal", "Rusted Metal", "New Wood", "Rotting Wood"}; //Names of materials
	public int mString = 0; //What one is selected

	//Instantiating materials needed for controlling how/in what way to edit the ladder in game
	GameObject[] editors = new GameObject[3]; //The editors that are available
	bool isMove = false, isRotate = false, isResize = false, isNone = true; //Whether we are moving, rotating, resizing, or none

    void Start()
    {
		
        tasks = GameObject.Find("TheTasks");
        cube = GameObject.Find("TheTasks/FirstTask");
        TS = tasks.GetComponent<TaskScript>();
        GCS = cube.GetComponent<GUICubeScript>();
        gameObject.transform.position = new Vector3(cube.transform.position.x + 3, cube.transform.position.y, cube.transform.position.z);
        ladders[0] = GameObject.Find("TheTasks/SecondTask/LadderOne");
        ladders[1] = GameObject.Find("TheTasks/SecondTask/LadderTwo");
        ladders[2] = GameObject.Find("TheTasks/SecondTask/LadderThree");
        //	lTwo.SetActive (false);
        //	lThree.SetActive (false);

        TS.editingLadder = true;
        doneSecond = false;
       
        texL = 0;
        lpX = (0f).ToString();
        lpY = (0f).ToString();
        lpZ = (0f).ToString();
		startPos = ladders[texL].transform.position;
		lpX = (ladders[texL].transform.position.x - startPos.x).ToString();
		lpY = (ladders[texL].transform.position.y - startPos.y).ToString();
		lpZ = (ladders[texL].transform.position.z - startPos.z).ToString();
	}

    // Update is called once per frame
    void Update()
    {
		//Based on the ladder version selected, turn on that ladder and turn off the other ones.
        if (texL == 0)
        {
            ladders[0].SetActive(true);
            ladders[1].SetActive(false);
            ladders[2].SetActive(false);
            RP = GameObject.Find("TheTasks/SecondTask/LadderOne/RotatePoint");
			//RP = GameObject.Find("TheTasks/SecondTask/RotatePoint");
            Vector3 tempRot = this.transform.localEulerAngles;
        }
        else if (texL == 1)
        {
            ladders[0].SetActive(false);
            ladders[1].SetActive(true);
            ladders[2].SetActive(false);
            RP = GameObject.Find("TheTasks/SecondTask/LadderTwo/RotatePoint");
        }
        else if (texL == 2)
        {
            ladders[0].SetActive(false);
            ladders[1].SetActive(false);
            ladders[2].SetActive(true);
            RP = GameObject.Find("TheTasks/SecondTask/LadderThree/RotatePoint");
        }
			

		//Controlling the material of the ladders
		Renderer rend = ladders [texL].GetComponent<Renderer> ();
		rend.material = lMat [mString];

		//Controlling the movers/rotators/reisizors
		editors [0] = ladders [texL].transform.Find ("Movers").gameObject;
		editors [1] = ladders [texL].transform.Find ("Rotators").gameObject;
		editors [2] = ladders [texL].transform.Find ("Resizors").gameObject;

		if (isMove) {
			editors[0].SetActive(true);
			editors[1].SetActive(false);
			editors[2].SetActive(false);
		}
		if (isRotate) {
			editors[0].SetActive(false);
			editors[1].SetActive(true);
			editors[2].SetActive(false);
		}
		if (isResize) {
			editors[0].SetActive(false);
			editors[1].SetActive(false);
			editors[2].SetActive(true);
		}
		if (isNone) {
			editors[0].SetActive(false);
			editors[1].SetActive(false);
			editors[2].SetActive(false);
		}
	}
    void OnGUI()
    {
		//Scale GUI Materials based off of resolution
        float rX, rY;
        float scale_width, scale_height;
        scale_width = 1296;
        scale_height = 729;
        rX = Screen.width / scale_width;
        rY = Screen.height / scale_height;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rX, rY, 1));
		//If we are editing the ladder, display editor
        if (TS.editingLadder && TS.clicks < 35)
        {
            GUI.Box(new Rect(0, 30, 370.285f, 227), "Ladder Editor");
            GUI.Label(new Rect(140, 50, 80, 20), "Ladder Type");
            GUI.Box(new Rect(75, 70, 220.285f, 30), lTypes[texL]);
            if (GUI.Button(new Rect(325.285f, 70, 32, 32), rightArrow))
            {
                if (texL == 2)
                    texL = 0;
                else
                    texL++;
            }
            if (GUI.Button(new Rect(20, 70, 32, 32), leftArrow))
            {
                if (texL != 0)
                    texL--;
                else
                    texL = 2;
            }

            if (GUI.Toggle(new Rect(10, 107, 83.249f, 18), isMove, "Move"))
            {
                isMove = true;
                isRotate = false;
                isResize = false;
                isNone = false;
            }
            if (GUI.Toggle(new Rect(10 + 123.249f, 107, 83.249f, 18), isRotate, "Rotate"))
            {
                isMove = false;
                isRotate = true;
                isResize = false;
                isNone = false;
            }
            if (GUI.Toggle(new Rect(10 + 2 * (123.249f), 107, 83.249f, 18), isResize, "Resize"))
            {
                isMove = false;
                isRotate = false;
                isResize = true;
                isNone = false;
            }
            if (GUI.Toggle(new Rect(10 + (123.249f), 137, 83.249f, 18), isNone, "None"))
            {
                isMove = false;
                isRotate = false;
                isResize = false;
                isNone = true;
            }
            //GUI For changing the material in game
            GUI.Label(new Rect(160, 167, 45, 20), "Textures");
			GUI.Box(new Rect(75, 187, 220.285f, 30), mSets[mString]);
			if (GUI.Button(new Rect(325.285f, 182, 32, 32), rightArrow))
			{
				if (mString == 4)
					mString = 0;
				else
					mString++;
			}
			if (GUI.Button(new Rect(20, 182, 32, 32), leftArrow))
			{
				if (mString != 0)
					mString--;
				else
					mString = 4;
			}

			//GUI for changing how to edit the ladder in game

			if (!doneSecond)
			{
				if (GUI.Button(new Rect(75, 227, 220.285f, 30), "I'm done making my ladder"))
				{
					TS.editingLadder = false;
					doneSecond = true;
				}
			}
        }
		//If we are not editing, set button so we are able to edit it at any time
		if (!TS.editingLadder && !TS.canClick && !TS.editingCube && TS.clicks < 35) {
			if (GUI.Button (new Rect (0, 30, 220.285f, 30), "Ladder")) {
				TS.editingWhat (2);
			}
		} else if (!TS.editingLadder && !TS.canClick && TS.editingCube && TS.clicks < 35) {
			if (GUI.Button(new Rect(0, 467, 220.285f, 30), "Ladder"))
			{
				TS.editingWhat(2);
			}
		}
    }
    public bool testIfClose()
    {
		//This function is called at the end game to see how close the ladder is close to the cube
        GameObject go;
        bool close = false;
        go = ladders[texL];
        Transform[] dist = go.GetComponentsInChildren<Transform>();
        Debug.Log(dist.Length);
        foreach (Transform d in dist)
        {
            if (Vector3.Distance(d.position, cube.transform.position) < 1.2f)
            {
                close = true;
            }
        }
        return close;
    }
}
