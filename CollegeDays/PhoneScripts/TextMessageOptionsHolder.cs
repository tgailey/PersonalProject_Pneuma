using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextMessageOptionsHolder : MonoBehaviour {
    private List<string[]> options = new List<string[]>();
    //private int story = 0;
	// Use this for initialization


	void Start () {

	}
	public TextMessageOptionsHolder()
    {

    }
    public void newOptions(string a, string b, string c, string d)
    {
        string[] mymymy = { a, b, c, d };
        options.Add(mymymy);
    }
   // public void moveStoryForward()
    //{
     //   story++;
    //}
    public string[] getOptions(int story)
    {
        return options[story];
    }
	// Update is called once per frame
	void Update () {
	
	}
    
}
