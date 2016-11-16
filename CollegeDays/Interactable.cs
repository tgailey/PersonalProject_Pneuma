using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {
	/*
	 * This script holds info for each individual interactable object
	 * */
    public string interactingText; //Text shown when we can interact with this object
    public Vector3 nametagOffset; //Offset of nametag in game
    public float maximumDistance = 2.0f; //Maximum distance away from object to still interact with it
    public int storySet; //What interacting with this object will set the story

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
