using UnityEngine;
using System.Collections;
using System;

public class FifthTask : MonoBehaviour
{
	/*
	 * Script for editing ladder in game
	 * */
    TaskScript TS;
    public Texture2D rightArrow, leftArrow; //Texture for right and left arrow
    public GameObject tornado, rain, snow, clouds, dustCloud; //GameObjects for each type of storm

    string[] stormTypes = { "Storm in the distance", "Rain Storm", "Blizzard", "Tornado", "Dust Storm" }; //String names of all the types of storms

    public int stormTypeNum = 0; //Which type of storm is selected

    public Material[] skyboxes = new Material[3]; //Skyboxes to be changed with storm
    ParticleSystem[] rainPS, snowPS; //Rain and snow partile systems


   public bool cubeAffected = false, ladderAffected = false, horseAffected = false, flowersAffected = false; //What is affected by the storm
    public bool isApproaching = true, isFading = false; //Whether or not storm is closing in or going away
    public bool doneFifth = false; //Whether editing the storm has been done the first time.
    // Use this for initialization
    void Start()
    {
        TS = GameObject.Find("TheTasks").GetComponent<TaskScript>();
        TS.editingStorm = true;

        tornado = GameObject.Find("TheTasks/FifthTask/Twister");
        tornado.SetActive(false);
        rain = GameObject.Find("TheTasks/FifthTask/RainPrefab");
        rainPS = rain.GetComponentsInChildren<ParticleSystem>();
        rain.SetActive(false);
        snow = GameObject.Find("TheTasks/FifthTask/SnowTest");
        snowPS = snow.GetComponentsInChildren<ParticleSystem>();
        snow.SetActive(false);
        clouds = GameObject.Find("TheTasks/FifthTask/CloudSystem");
        clouds.SetActive(false);
        dustCloud = GameObject.Find("TheTasks/FifthTask/DustCloud");
        dustCloud.SetActive(false);

        skyboxes[0] = RenderSettings.skybox;
    }

    // Update is called once per frame
    void Update()
    {	
		//Depending on the storm type selected, turn on that gameobject, and stop/play respective particle systems
        if (stormTypeNum == 0)
        {
            foreach (ParticleSystem ps in rainPS)
            {
                ps.Stop();
            }
            RenderSettings.skybox = skyboxes[0];
            clouds.SetActive(true);
            rain.SetActive(false);
            snow.SetActive(false);
            tornado.SetActive(false);
            dustCloud.SetActive(false);
        }
        else if (stormTypeNum == 1)
        {
            foreach (ParticleSystem sps in snowPS)
            {
                sps.Stop();
            }
            RenderSettings.skybox = skyboxes[1];
            clouds.SetActive(false);
            snow.SetActive(false);

                rain.SetActive(true);
            foreach (ParticleSystem ps in rainPS)
            {
                ps.Play();
            }
            // rainS.RainIntensity = .99f;
            //rainS.RainIntensity = 1;
            tornado.SetActive(false);
            dustCloud.SetActive(false);
        }
        else if (stormTypeNum == 2)
        {
            foreach (ParticleSystem ps in rainPS)
            {
                ps.Stop();
            }

            RenderSettings.skybox = skyboxes[1];
            clouds.SetActive(false);
            rain.SetActive(false);
            snow.SetActive(true);
            foreach (ParticleSystem sps in snowPS)
            {
                sps.Play();
            }
            tornado.SetActive(false);
            dustCloud.SetActive(false);
        }
        else if (stormTypeNum == 3)
        {


            foreach (ParticleSystem sps in snowPS)
            {
                sps.Stop();
            }
            RenderSettings.skybox = skyboxes[0];
            clouds.SetActive(false);
            rain.SetActive(false);
            snow.SetActive(false);
            tornado.SetActive(true);
            dustCloud.SetActive(false);
        }
        else if (stormTypeNum == 4)
        {
            RenderSettings.skybox = skyboxes[0];
            clouds.SetActive(false);
            rain.SetActive(false);
            snow.SetActive(false);
            tornado.SetActive(false);
            dustCloud.SetActive(true);
        }
    }
    void OnGUI()
    {
		//Scale GUI Based off resolution
        float rX, rY;
        float scale_width, scale_height;
        scale_width = 1296;
        scale_height = 729;
        rX = Screen.width / scale_width;
        rY = Screen.height / scale_height;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rX, rY, 1));
        //If editing, display editor
		if (TS.editingStorm && TS.clicks < 35)
        {
            GUI.Box(new Rect(0, 120, 370.285f, 235), "Storm Editor");
            GUI.Label(new Rect(110, 140, 90, 20), "Type of Storm");
            GUI.Box(new Rect(75, 160, 370.285f - 150, 30), stormTypes[stormTypeNum]);
            if (GUI.Button(new Rect(370.285f - 45, 155, 32, 32), rightArrow))
            {
                if (stormTypeNum == 4)
                    stormTypeNum = 0;
                else
                    stormTypeNum++;
            }
            if (GUI.Button(new Rect(20, 155, 32, 32), leftArrow))
            {
                if (stormTypeNum != 0)
                    stormTypeNum--;
                else
                    stormTypeNum = 4;
            }
            GUI.Label(new Rect(20, 200, 250, 30), "What items are affected by this storm?");
            cubeAffected = (GUI.Toggle(new Rect(60, 225, 123.429f - 40, 20), cubeAffected, "Cube")) ;
            ladderAffected = (GUI.Toggle(new Rect(60 + 123.429f, 225, 123.429f - 20, 20), ladderAffected, "Ladder"));
            horseAffected = (GUI.Toggle(new Rect(60, 250, 123.429f - 40, 20), horseAffected, "Horse"));
            flowersAffected = (GUI.Toggle(new Rect(60 + 123.429f, 250, 123.429f - 20, 20), flowersAffected, "Flowers")) ;
            if (stormTypeNum == 0)
            {
                if (GUI.Toggle(new Rect(20, 280, 200, 18), isApproaching, "The Storm is Approaching"))
                {
                    isApproaching = true;
                    isFading = false;
                }
                if (GUI.Toggle(new Rect(20, 300, 250, 18), isFading, "The Storm is Getting Further Away"))
                {
                    isApproaching = false;
                    isFading = true;
                }
            }
            if (!doneFifth)
            {
                if (GUI.Button(new Rect(75, 325, 370.285f - 150, 30), "I'm done making the storm"))
                {
                    TS.editingStorm = false;
                    doneFifth = true;
                }
            }
        }
		//If not editing storm, display button so that it can be edited at any time
        else if (!TS.canClick && TS.editingCube && TS.clicks < 35)
        {
            if (GUI.Button(new Rect(0, 530 + 27, 370.285f - 150, 30), "Storm"))
            {
                TS.editingWhat(5);
            }
        }
        else if (!TS.canClick && TS.editingLadder && TS.clicks < 35)
        {
            if (GUI.Button(new Rect(0, 290 + 27, 370.285f - 150, 30), "Storm"))
            {
                TS.editingWhat(5);
            }
        }
        else if (!TS.canClick && TS.editingHorse && TS.clicks < 35)
        {
            if (GUI.Button(new Rect(0, 410 + 27, 370.285f - 150, 30), "Storm"))
            {
                TS.editingWhat(5);
            }
        }
        else if (!TS.canClick && TS.editingFlowers && TS.clicks < 35)
        {
            if (GUI.Button(new Rect(0, 560, 370.285f - 150, 30), "Storm"))
            {
                TS.editingWhat(5);
            }
        }
        else if (!TS.canClick && TS.clicks < 35)
        {
            if (GUI.Button(new Rect(0, 120, 370.285f - 150, 30), "Storm"))
            {
                TS.editingWhat(5);
            }
        }
    }
}

