using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
// This script plays the video at the beginning of the game.
public class PlayVideo : MonoBehaviour
{

	public MovieTexture movie;
	private AudioSource audio;

	// Use this for initialization
	void Start()
	{
		GetComponent<RawImage>().texture = movie as MovieTexture;
		audio = GetComponent<AudioSource>();
		audio.clip = movie.audioClip;
		movie.Play();
		audio.Play();

	}
}
