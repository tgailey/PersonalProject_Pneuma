using UnityEngine;
using System.Collections;

public class WalkToSOsHouse : MonoBehaviour {
    int clicks = 0;
    bool fallInLoveFast;
    public GUIStyle customGUIStyle;
    //public GUISkin customGUISkin;
    // Use this for initialization
    void Start () {
        //customGUIStyle = new GUIStyle();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        float rX, rY;
        float scale_width, scale_height;
        scale_width = 1296;
        scale_height = 729;
        rX = Screen.width / scale_width;
        rY = Screen.height / scale_height;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rX, rY, 1));
        //   if (customGUISkin != null)
        // {
        //     GUI.skin = customGUISkin;
        //}
        if (clicks == 1)
        {
            if (GUI.Button(new Rect(20, 20, 1296 / 2 - 40, 729 - 40), "The long and beautiful path"))
            {
                fallInLoveFast = false;
                clicks++;
            }
            if (GUI.Button(new Rect(1296 / 2 + 20, 20, 1296 / 2 - 40, 729 - 40), "Quick and Easy Path"))
            {
                fallInLoveFast = true;
                clicks++;
            }
        }
    }
}
