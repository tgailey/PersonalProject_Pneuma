using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ForthTask : MonoBehaviour {
	/*
	 * Script for handling the flower editing in game
	 * */
	TaskScript TS;
	public Texture2D rightArrow, leftArrow; //Texture for right and left arrow
	public bool normal = true, potted = false, bush = false, dead = false;//Whether or not the flowers are normal, dead or potted.
	public bool doneForth = false;  //Whether not we finished creating the flowers the first time
	public GameObject pot; //Gameobject for the pot the flowers CAN come in
	public int typeOfFlower = 1; //What type of flower is selected
	float size = 1; //Amount of flowers in both string and float in order to get both best results and user friendliness
	int size2 = 1;
	public Texture2D colorWheel; //Color wheel texture
	public Color col2; //Color selected by color wheel
    public Material color1, color2, color3; //Colors of petals on flowers
	Texture2D targetTexture; //Texture of the color of flowers
	Queue flowers = new Queue(); //Queue of the flowers created in the game

	public GameObject GS; //GlowSpot gameobject
	private Vector3 screenPoint; 
	private Vector3 offset;

	public Material c;

	string[] typeNames = {"Rose", "Daisy", "Lily", "Sunflower"}; //Types of flowers
	public GameObject[] types = new GameObject[4]; //Gameobjects for flower types
	float y = 1.234f, potY = 0.616f, deadY; //The different y coordinates for each flower state
	float rot = 270; //Default rotation for flowers
	int typeNameNum = 0; //Type of flower selected
	int flowerCount = 0; //Amount of flowers to be created

	bool create = true, destroy = false; //Whether or not we are creating or getting rid of flowers
	Renderer glowRend; //Renderer for the glow spot
	
	// Use this for initialization
	void Start () {
		TS = GameObject.Find ("TheTasks").GetComponent<TaskScript> ();
		TS.editingFlowers = true;
		col2 = Color.red;
		GS = GameObject.Find ("TheTasks/ForthTask/GlowSpot");
		glowRend = GS.GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		//If we are editing the flowers, turn on glowspot
		if (TS.editingFlowers) {
			GS.SetActive(true);
		} else {
			GS.SetActive(false);
		}
		Vector3 mousePosition = Input.mousePosition;
		//mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
		Vector3 temp = mousePosition;
		//temp.x = mousePosition.x;

		//Set x and z of glowspot to where in the world the mouse is
		temp.z = mousePosition.y/(110/GameObject.Find("GameObject").transform.position.y);
		Vector3 temp2;
		temp2 = Camera.main.ScreenToWorldPoint(temp);
		temp2.y = .5f -0.481f;

		GS.transform.position = temp2;
		Vector3 tempSize = GS.transform.localScale;

		//Depending on the type of flower selected as well as the amount created, set size of glow spot.
		if (normal) {
			typeOfFlower = 1;
			tempSize.x = size / 10;
			tempSize.z = size / 10;
		} else if (potted) {
			typeOfFlower = 2;
            if (create)
            {
                tempSize.x = pot.transform.lossyScale.x / 4;
                tempSize.z = pot.transform.lossyScale.y / 4;
            }
            else
            {
                tempSize.x = pot.transform.lossyScale.x;
                tempSize.z = pot.transform.lossyScale.y;
            }
		} else if (dead) {
			typeOfFlower = 3;
			tempSize.x = 1.2f;
			tempSize.z = 1.2f;
		} else if (bush) {
			typeOfFlower = 4;
		} else {
			typeOfFlower = 1;
		}
	GS.transform.localScale = tempSize;

		//Depending on typeofflower, set the y values and rotation needed for them to display nicely
		if (typeNameNum == 0) {
			y = 1.23f;
			rot = 270;
			deadY = .2f;
		} else if (typeNameNum == 1) {
			y = 0.591f;
			rot = 0;
			deadY = 0.05f;
		} else if (typeNameNum == 2) {
			y = -0.044f;
			rot = 345;
			deadY = 0.05f;
		} else if (typeNameNum == 3) {
			y = 0.7f;
			rot = 0;
			deadY = 0.013f;
		}
		//If we right click with create, we call create flowers with information we selected. if destroy, we call destroy flowers
		if (Input.GetMouseButtonDown (1) && create && TS.editingFlowers) {
			createFlowers (typeOfFlower, size2, col2, types [typeNameNum]);
		} else if (Input.GetMouseButtonDown (1) && destroy && TS.editingFlowers) {
			destroyFlowers();
		}
		//Change color of glow spot based off of whether we are creating or destroying
		if (create) {
			glowRend.material.SetColor("_MKGlowColor", Color.cyan);
		} else if (destroy) {
			glowRend.material.SetColor("_MKGlowColor", Color.red);
		}
	}
	void OnGUI() {
		//Scales based off size of text
        float rX, rY;
        float scale_width, scale_height;
        scale_width = 1296;
        scale_height = 729;
        rX = Screen.width / scale_width;
        rY = Screen.height / scale_height;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rX, rY, 1));
		//If editing flowers, show the editor
        if (TS.editingFlowers && TS.clicks < 35) {
			GUI.Box (new Rect (0, 90, 370.285f, 470), "Flower Editor");
			if (GUI.RepeatButton (new Rect ((370.285f) / 2 - 256 / 2, 110, 256, 256), colorWheel)) {
				Vector2 pickpos = Event.current.mousePosition;
				int aaa = Convert.ToInt32 (pickpos.x) - (1296 * 2 / 14) - 256 / 2;
				int bbb = Convert.ToInt32 (pickpos.y) - 110;
				col2 = colorWheel.GetPixel (aaa, 256 - bbb);

			}
			if (GUI.Toggle (new Rect (60, 380, 123.429f - 40, 36.45f), create, "Create")) {
				create = true;
				destroy = false;
			}
			if (GUI.Toggle (new Rect (60 + 123.429f, 380, 123.429f - 20, 36.45f), destroy, "Destroy")) {
				create = false;
				destroy = true;
			}
			GUI.Label (new Rect (160, 400, 45, 20), "Type of Flower");
			GUI.Box (new Rect (75, 420, 370.285f - 150, 30), typeNames [typeNameNum]);
			if (GUI.Button (new Rect (370.285f - 45, 415, 32, 32), rightArrow)) {
				if (typeNameNum == 3)
					typeNameNum = 0;
				else
					typeNameNum++;
			}
			if (GUI.Button (new Rect (20, 415, 32, 32), leftArrow)) {
				if (typeNameNum != 0)
					typeNameNum--;
				else
					typeNameNum = 3;
			}
			if (GUI.Toggle (new Rect (10, 460, 123.429f, 36.45f), potted, "In a Pot")) {
				potted = true;
				bush = false;
				dead = false;
				normal = false;
			}
			/*   if (GUI.Toggle(new Rect(10 + (123.429f), 480, 123.429f, 36.45f), bush, "In a Bush"))
			{
				potted = false;
				bush = true;
				dead = false;
				normal = false;
			}*/
			if (GUI.Toggle (new Rect (10 + 2 * (123.429f), 460, 123.429f, 36.45f), dead, "Wilting Flowers")) {
				potted = false;
				bush = false;
				dead = true;
				normal = false;
			}
			if (GUI.Toggle (new Rect (10 + 123.429f, 460, 123.429f, 36.45f), normal, "Normal")) {
				potted = false;
				bush = false;
				dead = false;
				normal = true;
			}

			if (normal) {

				size = GUI.HorizontalSlider (new Rect (155, 510, 190, 24.3f), size, 1, 20);
				size2 = (int)size;
				GUI.Label (new Rect (350, 505, 30, 28), size2.ToString ());
				GUI.Box (new Rect (2, 500, 150, 27), "Amount of Flowers");
			}
			if (potted) {
				/*amtInPot = GUI.HorizontalSlider(new Rect(90, 510, Screen.width / 3.5f - 120, 24.3f), amtInPot, 0, 5);
				size2 = (int)amtInPot;
				GUI.Label(new Rect(Screen.width / 3.5f - 25, 510, 30, 28), size2.ToString());
				GUI.Box(new Rect(2, 500, 150, 27), "Amount of Flowers in Pot");*/
			}
			if (!doneForth) {
				if (GUI.Button (new Rect (75, 530, 370.285f - 150, 30), "I'm done making my flowers")) {
					TS.editingFlowers = false;
					doneForth = true;
				}
			}
			//If not editing flower, display button so that we can edit them at any time
		} else if (!TS.canClick && TS.editingCube && TS.clicks < 35) {
			if (GUI.Button (new Rect (0, 500 + 27, 370.285f - 150, 30), "Flowers")) {
				TS.editingWhat (4);
			}
		} else if (!TS.canClick && TS.editingLadder && TS.clicks < 35) {
			if (GUI.Button (new Rect (0, 260 + 27, 370.285f - 150, 30), "Flowers")) {
					TS.editingWhat (4);
				}		
		} else if (!TS.canClick && TS.editingHorse && TS.clicks < 35) {
			if (GUI.Button (new Rect (0, 380 + 27, 370.285f - 150, 30), "Flowers")) {
				TS.editingWhat (4);
			}	
		} else if (!TS.canClick && TS.clicks < 35) {
			if (GUI.Button (new Rect (0, 90, 370.285f - 150, 30), "Flowers")) {
					TS.editingWhat (4);
				}
		}
	}
	//Creates flowers based on custom information set by the gui
	void createFlowers (int type, int amt, Color color, GameObject flower) {
		Debug.Log(type);
		if (type == 1) {
			for (int i = 0; i < amt; i++) {
				flowerCount++;
				GameObject objects = 
			(GameObject)Instantiate
			(flower, new Vector3 
			(UnityEngine.Random.Range (GS.transform.position.x - GS.transform.lossyScale.x / 2, GS.transform.position.x + GS.transform.lossyScale.x / 2), 
			y, 
			UnityEngine.Random.Range (GS.transform.position.z - GS.transform.lossyScale.z / 2, GS.transform.position.z + GS.transform.lossyScale.z / 2)),
					 Quaternion.Euler (new Vector3 (0, UnityEngine.Random.Range (0, 360), rot)));
				objects.name = "Flower" + flowerCount.ToString ();
				objects.GetComponent<Renderer> ().material.color = col2;
                objects.layer = 12;
				//objects.renderer.material.color = col2;
			}
		}
		if (type == 2) {
				Instantiate (pot, new Vector3 (GS.transform.position.x + .08f, potY, GS.transform.position.z), Quaternion.Euler(270, 0, 0));
				Debug.Log("Testing");
				//for (int j = 0; j < amt; j++) {
					flowerCount++;
					GameObject objects = (GameObject) Instantiate (flower,
					                                               new Vector3 (GS.transform.position.x, y+.2f, GS.transform.position.z),
					                                               Quaternion.Euler(0, 
					                 								UnityEngine.Random.Range(0,360),
					                                                rot));

						objects.name = "PottedFlower"+flowerCount.ToString();
						objects.GetComponent<Renderer>().material.color = col2;
                         objects.layer = 12;
            //}
        }
        if (type == 3)
        {
            flowerCount++;
			if (typeNameNum != 3) {
            GameObject objects = (GameObject)Instantiate(flower,
            new Vector3(GS.transform.position.x, deadY, GS.transform.position.z),
                        Quaternion.Euler(0,
                        UnityEngine.Random.Range(0, 360),
                         rot - 90));
				objects.name = "WilitingFlower" + flowerCount.ToString();
                objects.layer = 13;
            }
			else {
				GameObject objects = (GameObject)Instantiate(flower,
				                                             new Vector3(GS.transform.position.x, deadY, GS.transform.position.z),
				                                             Quaternion.Euler(0,
				                 UnityEngine.Random.Range(0, 360),
				                 rot + 90));
				objects.name = "WilitingFlower" + flowerCount.ToString();
                objects.layer = 13;
            }
        }
		
	}
	//destoys flowers within range of glowspot
	void destroyFlowers () {
		GameObject[] AllTheFlowers;
		AllTheFlowers = GameObject.FindGameObjectsWithTag ("Flowers");
		foreach (GameObject f in AllTheFlowers) {
			if ((f.transform.position.x > GS.transform.position.x - GS.transform.lossyScale.x/2) && 
			    (f.transform.position.x < GS.transform.position.x + GS.transform.lossyScale.x/2) &&
			    (f.transform.position.z > GS.transform.position.z - GS.transform.lossyScale.z/2) &&
			    (f.transform.position.z > GS.transform.position.z - GS.transform.lossyScale.z/2)) {
				Destroy(f);
			}
		}


			    /* if (GameObject.Find("Player1").transform.position.x > this.transform.position.x - (this.transform.localScale.x/2) &&
			    GameObject.Find("Player1").transform.position.x < this.transform.position.x + (this.transform.localScale.x/2) ) {
				if (GameObject.Find("Player1").transform.position.z > this.transform.position.z - (this.transform.localScale.z/2) &&
				    GameObject.Find("Player1").transform.position.z < this.transform.position.z + (this.transform.localScale.z/2) )*/
	}
}
