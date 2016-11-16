using UnityEngine;
using System.Collections;

public class Song : MonoBehaviour {
    string title;
    string genre;
    AudioClip audio;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public Song(string g, string t, AudioClip AC)
    {
        genre = g;
        title = t;
        audio = AC;
    }
    public string getGenre()
    {
        return genre;
    }
    public string getTitle()
    {
        return title;
    }
    public AudioClip getAudio()
    {
        return audio;
    }
}
