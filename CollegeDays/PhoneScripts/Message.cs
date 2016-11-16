using UnityEngine;
using System.Collections;

public class Message : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    int sent;
    string message;
    public Message(int s, string m)
    {
        sent = s;
        message = m;
    }
    public int getSent()
    {
        return sent;
    }
    public string getMessage()
    {
        return message;
    }
}
