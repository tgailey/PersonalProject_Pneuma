using UnityEngine;
using System.Collections;

public class ThirdTask : MonoBehaviour {
	/*
	 * script handling the editing of the horse.
	 * */
	TaskScript TS;
	public Texture2D rightArrow, leftArrow; //Texture for right and left arrows

    public GameObject horse; //Horse game object itself
    GameObject saddle; //Saddle GameObject
	public Material[] horseColors= new Material[5]; //Materials the horse is able to be 
	int HMat = 0; //What material is selected
	bool isIdle, isWalking, isGalloping; //Whether the horse is standing, walking, or galloping
	public bool yesSaddle, noSaddle; //Whether or not the horse is saddled or not
	public bool doneThird = false; //Whether or not we have finished editing the horse for the first time
	Renderer rend; //Renderer of the horse

	string[] mythSets = {"N0?!?!", "Unicorn", "Pegasus"}; //Mythicality strings.
	public int setNum = 0; //What mythicality is selected
	GameObject uniHorn, pegWings; //Gameobjects for mythicality. 

    Animator anim; //Animator for horse

    public string[] descriptions; //String array for descriptions
    public int desNum = 0; //What description is selected 

    bool isMove = false, isRotate = false, isResize = false, isNone = true; //Whether or not we are moving, rotating, resizing, or none of the above
    public GameObject[] horseEditors = new GameObject[3]; //The editors for the horse
	// Use this for initialization
	void Start () {
		TS = GameObject.Find ("TheTasks").GetComponent<TaskScript> ();
		TS.editingHorse = true;

		horse = GameObject.Find ("TheTasks/ThirdTask/Horse");
		rend = GameObject.Find("TheTasks/ThirdTask/Horse/Horse").GetComponent<Renderer>(); //get the renderer
		rend.enabled = true;

		yesSaddle = false;
		noSaddle = true;

		saddle = GameObject.Find ("TheTasks/ThirdTask/Horse/Root/Hips/Spine/Volume03/Saddle");
		saddle.SetActive (false);

		isIdle = true;
		isWalking = false;
		isGalloping = false;

		uniHorn = GameObject.Find ("TheTasks/ThirdTask/Horse/Root/Hips/Spine/Spine1/Neck/Neck1/Neck2/Head/R_Ear/Horn");
		pegWings = GameObject.Find ("TheTasks/ThirdTask/Horse/Root/Hips/Spine/Volume03/Wings");

        anim = horse.GetComponent<Animator>();

        descriptions = new string[]{"Strong, steady, workhorse", "Battle Horse", "Pretty Show Horse", "Stallion"};
    }
	
	// Update is called once per frame
	void Update () {
		//Set material of horse to selected material in editor
		rend.material = horseColors [HMat];
		//If saddled is selected, turn on the saddle. If not, turn it off
		if (yesSaddle) {
			saddle.SetActive (true);
		} else if (noSaddle) {
			saddle.SetActive (false);
		}

		//If mythicality is 0, than both the wings and horn is disabled. 1 sets horn active, 2 sets wings active.
		if (setNum == 0) {
			uniHorn.SetActive (false);
			pegWings.SetActive (false);
		} else if (setNum == 1) {
			uniHorn.SetActive (true);
			pegWings.SetActive (false);
		} else if (setNum == 2) {
			uniHorn.SetActive (false);	
			pegWings.SetActive (true);
		} else {
			uniHorn.SetActive (false);
			pegWings.SetActive (false);
		}

		//Depending on what bools are true, set the animations of the horse to that
		#region HorseAnimations
        if (isIdle)
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isWalking", false);
            anim.SetBool("isGalloping", false);
        }
        else if (isWalking)
        {
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking", true);
            anim.SetBool("isGalloping", false);
        }
        else if (isGalloping)
        {
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isGalloping", true);
        }
		#endregion

		//Set movers active based on what bool is true
        if (isMove)
        {
            horseEditors[0].SetActive(true);
            horseEditors[1].SetActive(false);
            horseEditors[2].SetActive(false);
        }
        if (isRotate)
        {
            horseEditors[0].SetActive(false);
            horseEditors[1].SetActive(true);
            horseEditors[2].SetActive(false);
        }
        if (isResize)
        {
            horseEditors[0].SetActive(false);
            horseEditors[1].SetActive(false);
            horseEditors[2].SetActive(true);
        }
        if (isNone)
        {
            horseEditors[0].SetActive(false);
            horseEditors[1].SetActive(false);
            horseEditors[2].SetActive(false);
        }
    }
	void OnGUI() {
		//Scales GUI elements with resolution
        float rX, rY;
        float scale_width, scale_height;
        scale_width = 1296;
        scale_height = 729;
        rX = Screen.width / scale_width;
        rY = Screen.height / scale_height;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rX, rY, 1));
        //If editing horse, display the editor
		if (TS.editingHorse && TS.clicks < 35) {
			GUI.Box (new Rect (0, 60, 370.285f, 347), "Horse");
			GUI.Label (new Rect (140, 80, 80, 20), "Horse Color");
			GUI.Box (new Rect (75, 100, 220.285f, 30), horseColors [HMat].name);
			if (GUI.Button (new Rect (325.285f, 100, 32, 32), rightArrow)) {
				if (HMat == 4)
					HMat = 0;
				else
					HMat++;
			}
			if (GUI.Button (new Rect (20, 100, 32, 32), leftArrow)) {
				if (HMat != 0)
					HMat--;
				else
					HMat = 4;
			}

			if (GUI.Toggle (new Rect (60, 142, 83.429f, 18), yesSaddle, "Saddle")) {
				yesSaddle = true;
				noSaddle = false;
			}
			if (GUI.Toggle (new Rect (60 + 123.429f, 142, 103.429f, 18), noSaddle, "No Saddle")) {
				yesSaddle = false;
				noSaddle = true;
			}


			if (GUI.Toggle (new Rect (20, 172, 83.429f, 18), isIdle, "Standing")) {
				isIdle = true;
				isWalking = false;
				isGalloping = false;
			}
			if (GUI.Toggle (new Rect (20 + 123.429f, 172, 83.429f, 18), isWalking, "Walking")) {
				isIdle = false;
				isWalking = true;
				isGalloping = false;
			}
			if (GUI.Toggle (new Rect (20 + 2 * (123.429f), 172, 83.429f, 18), isGalloping, "Galloping")) {
				isIdle = false;
				isWalking = false;
				isGalloping = true;
			}

			GUI.Label (new Rect (140, 202, 80, 20), "Mythicality");
			GUI.Box (new Rect (75, 222, 220.285f, 30), mythSets [setNum]);
			if (GUI.Button (new Rect (325.285f, 222, 32, 32), rightArrow)) {
				if (setNum == 2)
					setNum = 0;
				else
					setNum++;
			}
			if (GUI.Button (new Rect (20, 222, 32, 32), leftArrow)) {
				if (setNum != 0)
					setNum--;
				else
					setNum = 2;
			}

			if (GUI.Toggle (new Rect (20, 257, 83.429f, 18), isMove, "Move")) {
				isMove = true;
				isRotate = false;
				isResize = false;
				isNone = false;
			}
			if (GUI.Toggle (new Rect (20 + 123.429f, 257, 83.429f, 18), isRotate, "Rotate")) {
				isMove = false;
				isRotate = true;
				isResize = false;
				isNone = false;
			}
			if (GUI.Toggle (new Rect (20 + 2 * (123.429f), 257, 83.429f,18), isResize, "Resize")) {
				isMove = false;
				isRotate = false;
				isResize = true;
				isNone = false;
			}
			if (GUI.Toggle (new Rect (20 + (123.429f), 287, 83.429f, 18), isNone, "None")) {
				isMove = false;
				isRotate = false;
				isResize = false;
				isNone = true;
			}


			GUI.Label (new Rect (115.285f, 312, 140, 20), "Describe your horse");
			GUI.Box (new Rect (75, 332, 220.285f, 30), descriptions [desNum]);
			if (GUI.Button (new Rect (325.285f, 332, 32, 32), rightArrow)) {
				if (desNum == 3)
					desNum = 0;
				else
					desNum++;
			}
			if (GUI.Button (new Rect (20, 332, 32, 32), leftArrow)) {
				if (desNum != 0)
					desNum--;
				else
					desNum = 3;
			}

			if (!doneThird) {
				if (GUI.Button (new Rect (75, 377, 220.285f, 30), "I'm done making my horse")) {
					TS.editingHorse = false;
					doneThird = true;
				}
			}
			//If not editing horse, display button to be clicked so that we can edit any time in game
		} else if (!TS.canClick && TS.editingCube && TS.clicks < 35) {
			if (GUI.Button (new Rect (0, 497, 220.285f, 30), "Horse")) {
				TS.editingWhat (3);
			}
		} else if (!TS.canClick && TS.editingLadder && TS.clicks < 35) {
			if (GUI.Button (new Rect (0, 230 + Screen.height / 27, 220.285f, 30), "Horse")) {
				TS.editingWhat (3);
			}
		} else if (!TS.canClick && TS.clicks < 35) {
			if (GUI.Button (new Rect (0, 60, 220.285f, 30), "Horse")) {
				TS.editingWhat (3);
			}
		}
		     
	}
}
