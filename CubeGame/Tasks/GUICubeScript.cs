using UnityEngine;
using System;
using System.Collections;

public class GUICubeScript : MonoBehaviour
{   
	/*
	 * Script handling the editing of the cube
	 * */
    public bool doneFirst;    //tells the computer whether are not we are editing the cube
    public GUISkin _theSkin = null; //Declaring skin for text, we can edit this to make text look how we prefer.
    public GUIStyle customStyle;
    Texture2D targetTexture; //Declares base texture
    public Texture2D rightArrow; //Texture for right arrow
    public Texture2D leftArrow; //Texture for left arrow
    public Texture2D colorPicker; //Texture for the color picture
    public Color col; //Color selected by color picker
    public Material mirror, Default; //Materials needed.
    Renderer rend; //Cube renderer
    float transValue = 100f;//calculated value for velocity
	public int tValue = 100;//displayed value for velocity (done to make interface more clean and user friendly)
    int aaa, bbb; //x and y on coordinates when clicking the color picker
    bool isMove, isRotate, isResize; //Whether we are currently moving, rotating, or resizing the cube
   // public bool cantMoveDown;
    public String pX, pY, pZ, rX, rY, rZ, sX, sY, sZ; //Variables. These will be changed through the editors and then set to the new location of the cube.
													  //pX_Y_Z change position, rX_Y_Z change rotation, and sX_Y_Z change scale
    GameObject movers, rotators, resizers, GC, MC, cube; //Gameobjects needed. 

    //Glowing additions
    GameObject glowCube; //Seperate cube for glowing due to different properties
    Renderer glowRend; //Seperate renderer for seperate cube
    public bool glowOn; //Whether or not cube is glowing
    Color glowColor; //Color of the glow
    public Texture2D colorPicker2; //Seperate color picker for the color of the glow
    float glowFloat; //Strength of glow
    // Material glow;

    //String[] pSets = {"None", "Dice", "Completed Rubix Cube", "Uncompleted Rubix Cube"};
    String[] tSets = { "None", "Bolted Box", "Wooden Box", "Mirror", "Dice", "Completed Rubix Cube", "Uncompleted Rubix Cube" }; //Texture names for individual textures
    public Texture2D[] texs = new Texture2D[4]; //Actual texture files
    public int texT = 0; //What texture is selected through in game menu

    TaskScript TS; 


    SecondTask ST;

    GameObject main;
    // Use this for initialization
    void Start()
    {
		//Declaring variables above.

		TS = GameObject.Find("TheTasks").GetComponent<TaskScript>();
        //Glow cube declarations
        glowCube = GameObject.Find("TheTasks/FirstTask/GlowCube");
        glowRend = glowCube.GetComponent<Renderer>();
        glowRend.enabled = true;
        glowOn = false;
        glowFloat = 1;
        glowColor = Color.white;

        TS.editingCube = true; //says we are editing the cube
        cube = GameObject.Find("TheTasks/FirstTask/MainCube/Cube"); //Finds The cube that has the different materials within it, allowing to program different sides to cube
                                                                    //Done by turning off current cube and turning on this one.
        cube.SetActive(false); //turn multi-sided cube off
        rend = GetComponent<Renderer>(); //get the renderer
        rend.enabled = true; //enable to the renderer
        targetTexture = new Texture2D(64, 64); //create a new blank texture to edit/change color on.
        texs[0] = targetTexture;
        texs[3] = targetTexture;
        //set the first and forth textures in the array to empty texture
        isMove = false;
        isRotate = false;
        isResize = false;
        //Not currently editing the cube in any of these ways at beginning.
        movers = GameObject.Find("TheTasks/FirstTask/Movers");
        movers.gameObject.SetActive(false);
        resizers = GameObject.Find("TheTasks/FirstTask/Resizers");
        resizers.gameObject.SetActive(false);
        rotators = GameObject.Find("TheTasks/FirstTask/Rotators");
        rotators.gameObject.SetActive(false);
        GC = GameObject.Find("TheTasks/FirstTask");
        MC = GameObject.Find("Cube");
        pX = (GC.transform.position.x - 250).ToString();
        pY = (GC.transform.position.y - .5f).ToString();
        pZ = (GC.transform.position.z - 250).ToString();
        rX = "0";
        rY = "0";
        rZ = "0";
        sX = "1";
        col = Color.white;
        ChangeColorTex();

        main = GameObject.Find("GameObject");
        //main.transform.position = new Vector3(GC.transform.position.x, GC.transform.position.y + 1.5f, GC.transform.position.z - (5 * GC.transform.localScale.x));
        //main.transform.LookAt(GC.transform);
        main.transform.position = new Vector3(GC.transform.position.x, GC.transform.position.y + 1.5f, GC.transform.position.z - (5 * GC.transform.localScale.x));
        main.transform.eulerAngles = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

        //Glow cube updates
		//If glowing, set the color and power to the values selected in the editor.
        if (glowOn)
        {
            glowRend.material.SetFloat("_MKGlowPower", glowFloat);
            glowRend.material.SetColor("_MKGlowColor", glowColor);
        }
		//If not glowing, set the strength to zero, therefore making it unseen.
        else
        {
            glowRend.material.SetFloat("_MKGlowPower", 0);
        }


		#region editingCubeWorldProperties
		//Set position, rotation, and scale of cube based on values.
        Vector3 posTemp = GC.transform.position;
        posTemp.x = float.Parse(pX) + 250;
        posTemp.y = float.Parse(pY) + .5f;
        posTemp.z = float.Parse(pZ) + 250;
        GC.transform.position = posTemp;
        Vector3 rotTemp = GC.transform.localEulerAngles;
        rotTemp.x = float.Parse(rX);
        rotTemp.y = float.Parse(rY);
        rotTemp.z = float.Parse(rZ);
        GC.transform.eulerAngles = rotTemp;

        Vector3 scaTemp = GC.transform.localScale;
        scaTemp.x = float.Parse(sX);
        scaTemp.y = float.Parse(sX);
        scaTemp.z = float.Parse(sX);
        GC.transform.localScale = scaTemp;
		#endregion
		#region textureChanging
		//Set textures based on value of texT
		//If third texture (mirror) is active, set renderer material to mirror
        if (texT == 3)
        {
            rend.material = mirror;
            cube.SetActive(false);
        }
        else
        {
            rend.material = Default;
            setTextures sT = cube.GetComponent<setTextures>();
            Color color = rend.material.color;
            if (texT < 3)
			{
                color.a = transValue / 100;
                cube.SetActive(false);
            }
            else
            {
                color.a = 0f;
                cube.SetActive(true);
            }
            rend.material.color = color;
            rend.material.shader = Shader.Find("Transparent/Diffuse");

        }
		#endregion
        if (texT < 4)
            rend.material.mainTexture = texs[texT];
        else
            rend.material.mainTexture = targetTexture;

		#region editorsActive
		//If moving, rotating, or resizing, set those editors active. Otherwise, disable them.
        if (isMove)
        {
            movers.gameObject.SetActive(true);
            //this.GetComponent<Movers>().enabled();
        }
        else
            movers.gameObject.SetActive(false);
        if (isRotate)
        {
            rotators.gameObject.SetActive(true);
            //this.GetComponent<Movers>().enabled();
        }
        else
            rotators.gameObject.SetActive(false);
        if (isResize)
        {
            resizers.gameObject.SetActive(true);
            //this.GetComponent<Movers>().enabled();
        }
        else
            resizers.gameObject.SetActive(false);
		#endregion
		//when not editing the cube, set movers to false
        if (!TS.editingCube)
        {
            isMove = false;
            isRotate = false;
            isResize = false;
        }
        if (TS.clicks == 11)
        {
            ST = GameObject.Find("TheTasks/SecondTask").GetComponent<SecondTask>();
        }
    }
    void OnGUI()
    {
		//Scales GUI elements based off resolution
        float rX, rY;
        float scale_width, scale_height;
        scale_width = 1296;
        scale_height = 729;
        rX = Screen.width / scale_width;
        rY = Screen.height / scale_height;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rX , rY , 1));
        // Debug.Log("rx: " + rX);
        // Debug.Log("ry: " + rY);
        if (_theSkin != null)
        {
            GUI.skin = _theSkin;
        }
		//If we are editing the cube, display the editor for it
        if (TS.editingCube && TS.clicks < 35)
        {
            GUI.Box(new Rect(0, 0, 370.28f, 468.11f), "Cube Editor");

            if (GUI.RepeatButton(new Rect((370.28f) / 2 - 256 / 2, 20, /*Screen.width / 3.5f - 40, (Screen.height / 3)*/256, 256), colorPicker))
            {

                Vector2 pickpos = Event.current.mousePosition;

                int aaa = Convert.ToInt32(pickpos.x) - (1296 * 2 / 14) - 256 / 2;
                int bbb = Convert.ToInt32(pickpos.y) - 20;
                col = colorPicker.GetPixel(aaa, 256 - bbb);

                // "col" is the color value that Unity is returning.
                // Here you would do something with this color value, like
                // set a model's material tint value to this color to have it change
                // colors, etc, etc.
                //
                // Right now we are just printing the RGBA color values to the Console
                ChangeColorTex();
            }
            transValue = GUI.HorizontalSlider(new Rect(90, 300, 250.28f, 24.3f), transValue, 0, 100);
            tValue = (int)transValue;
            GUI.Label(new Rect(345.28f, 300, 30, 28), tValue.ToString());
            GUI.Box(new Rect(2, 290, 87, 27), "Transparancy");
            if (GUI.Toggle(new Rect(10, 337, 83.43f, 18), isMove, "Move"))
            {
                isMove = true;
                isRotate = false;
                isResize = false;
            }
            if (GUI.Toggle(new Rect(10 + 123.43f, 337, 83.43f, 18), isRotate, "Rotate"))
            {
                isMove = false;
                isRotate = true;
                isResize = false;
            }
            if (GUI.Toggle(new Rect(10 + 2 * (123.43f), 337, 83.43f, 18), isResize, "Resize"))
            {
                isMove = false;
                isRotate = false;
                isResize = true;
            }
            GUI.Label(new Rect(160, 357, 45, 20), "Textures");
            GUI.Box(new Rect(75, 377, 220.285f, 30), tSets[texT]);
            if (GUI.Button(new Rect(320, 372, 32, 32), rightArrow))
            {
                if (texT == 6)
                    texT = 0;
                else
                    texT++;
            }
            if (GUI.Button(new Rect(20, 372, 32, 32), leftArrow))
            {
                if (texT != 0)
                    texT--;
                else
                    texT = 6;
            }

            glowOn = (GUI.Toggle(new Rect(120, 417, 150, 18), glowOn, "Cube Glow On"));
            if (glowOn)
            {
                GUI.Box(new Rect(925.715f, 0, 370.285f, 400), "Glow Editor");
                if (GUI.RepeatButton(new Rect((1296 - 185.143f - 256 / 2), 30, /*Screen.width / 3.5f - 40, (Screen.height / 3)*/256, 256), colorPicker))
                {

                    Vector2 pickpos2 = Event.current.mousePosition;

                    int aaa2 = Convert.ToInt32(pickpos2.x) - (1296 - (1296 * 2 / 14) - 256 / 2);
                    int bbb2 = Convert.ToInt32(pickpos2.y) - 30;
                    glowColor = colorPicker.GetPixel(aaa2, 256 - bbb2);
                }
                float temp;
                temp = GUI.HorizontalSlider(new Rect((965.715f), 330, 250.285f, 24.3f), glowFloat * 20, 0, 100);
                GUI.Label(new Rect(1246, 330, 30, 28), ((int)(glowFloat * 20)).ToString());
                GUI.Box(new Rect(1035.8575f, 290, 150, 27), "Glow Strength");
                glowFloat = temp / 20;
            }
            if (!doneFirst)
            {
                if (GUI.Button(new Rect(75, 437, 220.285f, 30), "I'm done making my cube"))
                {
                    TS.editingCube = false;
                    doneFirst = true;
                }
            }
        }
		//if not editing cube, place button in proper place so that it can be re-edited at anytime
        if (!TS.editingCube && !TS.canClick && TS.clicks < 35)
        {
            if (GUI.Button(new Rect(0, 0, 220.285f, 30), "Cube"))
            {
				TS.editingWhat(1);
            }
        }
    }
	//Called to change color of a texture one pixel at a time
    void ChangeColorTex()
    {
        for (int y = 0; y < 64; y++)
        {
            for (int x = 0; x < 64; x++)
            {

                targetTexture.SetPixel(x, y, col);


            }
        }
        targetTexture.Apply();
        if (texT != 3)
            rend.material.color = col;


    }

}
