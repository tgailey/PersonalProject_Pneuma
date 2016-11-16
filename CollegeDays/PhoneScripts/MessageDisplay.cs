using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MessageDisplay : MonoBehaviour {
	/*
	 * Displays messages created through MessageController
	 * */
	public bool show; //Whether or not to show messages
    public Queue<Message> messages2 = new Queue<Message>(); //Queue of all messages both sent and received
    Vector2 scrollView = Vector2.zero ; // Default scroll view
    public GUISkin sentSkin, gotSkin; //Sent and received skins
    // Use this for initialization
    void OnGUI()
    {
		//Scales text based on resolution
        float rX, rY;
        float scale_width, scale_height;
        scale_width = 1316;
        scale_height = 740;
        rX = Screen.width / scale_width;
        rY = Screen.height / scale_height;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rX, rY, 1));
		//If messages are to be displayed, display them
        if (show)
        {

            int y = 175;
            
            scrollView = GUI.BeginScrollView(new Rect(400, 175, 400, 430), scrollView, new Rect(400, 175, 380, 60 * messages2.Count));
            foreach (Message m in messages2)
            {
                if (m.getSent() == 0)
                {
					GUI.skin = gotSkin;
                    GUI.Box(new Rect(510, y, 200, 50), m.getMessage());
                    y += 60;
                }
                else
                {
					GUI.skin = sentSkin;
                    GUI.Box(new Rect(580, y, 200, 50), m.getMessage());
                    y += 60;
                }
            }
            GUI.EndScrollView();
            Debug.Log(scrollView.y);
        }
    }
}
