using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HolderOfAudio : MonoBehaviour {
	/*
	 *This script handles all audio ingame, controlls what is playing, how to change it, and when it is displayed 
	 * */
    List<Song> TechnoSongs = new List<Song>(); //List of all Techno songs
    List<Song> RockSongs = new List<Song>(); //List of all rock songs
    List<Song> PeacefulSongs = new List<Song>(); //List of all peaceful songs
    List<Song> HipHopSongs = new List<Song>(); //List of all Hip Hop songs
    List<Song> CountrySongs = new List<Song>(); //List of all Country songs
    List<List<Song>> listedGenres = new List<List<Song>>(); //List of all the lists of specific genres we have
    int selectedClip; //What clip in the list is playing
    public AudioClip[] audios; //Holder of all the audios
    int selectedGenre = 0; //Selected list
    PhoneController PC; //Reference phone controller so we know what must be displayed based on screen of phone

	//All the songs
	Song R1, R2, R3, R4, R5, P1, P2, P3, P4, P5, T1, T2, T3, T4, T5, H1, H2,H3,H4,H5,C1,C2,C3,C4,C5; 
    public string songPlaying = ""; //string for the name of the song playing
    public GUISkin MSkin; //Skin for music
	public Texture2D ffb, rb, pb; //Textures for fast forward, rewind, and play buttons.


    bool stopped = false; //Whether or not the music is stopped

	void Start () {
		//Add individual genres lists to listedgenres
        listedGenres.Add(RockSongs); 
        listedGenres.Add(PeacefulSongs);
		listedGenres.Add(TechnoSongs);
		listedGenres.Add(HipHopSongs);
        listedGenres.Add(CountrySongs);

        PC = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PhoneController>();
        //Creates songs with audio and adds them to respective genres.
		R1 = new Song("Rock", audios[0].name, audios[0]);
		RockSongs.Add(R1);
		R2 = new Song("Rock", audios[1].name, audios[1]);
		RockSongs.Add(R2);
		R3 = new Song("Rock", audios[2].name, audios[2]);
		RockSongs.Add(R3);
		R4 = new Song("Rock", audios[3].name, audios[3]);
		RockSongs.Add(R4);
		R5 = new Song("Rock", audios[4].name, audios[4]);
		RockSongs.Add(R5);
		P1 = new Song("Peaceful", audios[5].name, audios[5]);
		PeacefulSongs.Add(P1);
		P2 = new Song("Peaceful", audios[6].name, audios[6]);
		PeacefulSongs.Add(P2);
		P3 = new Song("Peaceful", audios[7].name, audios[7]);
		PeacefulSongs.Add(P3);
		P4 = new Song("Peaceful", audios[8].name, audios[8]);
		PeacefulSongs.Add(P4);
		P5 = new Song("Peaceful", audios[9].name, audios[9]);
		PeacefulSongs.Add(P5);
		T1 = new Song("Techno", audios[10].name, audios[10]);
		TechnoSongs.Add(T1);
		T2 = new Song("Techno", audios[11].name, audios[11]);
		TechnoSongs.Add(T2);
		T3 = new Song("Techno", audios[12].name, audios[12]);
		TechnoSongs.Add(T3);
		T4 = new Song("Techno", audios[13].name, audios[13]);
		TechnoSongs.Add(T4);
		T5 = new Song("Techno", audios[14].name, audios[14]);
		TechnoSongs.Add(T5);
		H1 = new Song("HipHop/Pop", audios[15].name, audios[15]);
		HipHopSongs.Add(H1);
		H2 = new Song("HipHop/Pop", audios[16].name, audios[16]);
		HipHopSongs.Add(H2);
		H3 = new Song("HipHop/Pop", audios[17].name, audios[17]);
		HipHopSongs.Add(H3);
		H4 = new Song("HipHop/Pop", audios[18].name, audios[18]);
		HipHopSongs.Add(H4);
		H5 = new Song("HipHop/Pop", audios[19].name, audios[19]);
		HipHopSongs.Add(H5);
		C1 = new Song("Country", audios[20].name, audios[20]);
		CountrySongs.Add(C1);
		C2 = new Song("Country", audios[21].name, audios[21]);
		CountrySongs.Add(C2);
		C3 = new Song("Country", audios[22].name, audios[22]);
		CountrySongs.Add(C3);
		C4 = new Song("Country", audios[23].name, audios[23]);
		CountrySongs.Add(C4);
	}
	

	void Update () {
		//If a song is selected, and music is not being played (and we did not disable it ourselves) play next song in genre
        if (!string.Equals(songPlaying, ""))
        {
            if (!PC.AS.isPlaying && !stopped)
            {

                moveMusicForward();
            }
        }

    }
    void OnGUI()
    {
		//Scales GUI based on resolution
        float rX, rY;
        float scale_width, scale_height;
        scale_width = 1316;
        scale_height = 740;
        rX = Screen.width / scale_width;
        rY = Screen.height / scale_height;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rX, rY, 1));
        GUI.skin = MSkin;
		//If on base music screen, display genres. Move screen forward if one is selected
            if (PC.screen == 5)
            {
                if (GUI.Button(new Rect(510, 175, 300, 60), "Rock"))
                {
                selectedGenre = 0;
                    PC.screen++;
                }
                if (GUI.Button(new Rect(510, 235, 300, 60), "Peaceful"))
                {
                selectedGenre = 1;
                PC.screen++;
                }
                if (GUI.Button(new Rect(510, 295, 300, 60), "Techno"))
                {
                selectedGenre = 2;
                PC.screen++;
                }
                if (GUI.Button(new Rect(510, 355, 300, 60), "Hip Hop"))
                {
                selectedGenre = 3;
                PC.screen++;
                }
                if (GUI.Button(new Rect(510, 415, 300, 60), "Country"))
                {
                selectedGenre = 4;
                PC.screen++;
                }
            }
            else if (PC.screen == 6)
            {
                int y = 175;
			//If genre is selected, show all songs available in that genre
            foreach (Song s in listedGenres[selectedGenre])
            {
                if (GUI.Button(new Rect(510, y, 300, 50), s.getTitle()))
                {
                    selectedClip = listedGenres[selectedGenre].IndexOf(s);
                    PC.AS.clip = s.getAudio();
                    songPlaying = s.getTitle();
                    PC.AS.Play();
                }
                y += 50;
                
            }
            MSkin.label.fontSize = 20;
            GUI.Label(new Rect(550, 120, 200, 30), listedGenres[selectedGenre][0].getGenre());
            GUI.skin = null;
            if (GUI.Button(new Rect(510, 115, 30, 30), ""))
            {
                PC.screen = 5;
            }     
        }
            if (PC.screen > 4 && PC.screen < 7)
            {
            if (!string.Equals(songPlaying, ""))
            {
                GUI.skin = null;
                GUI.Box(new Rect(520, 500, 280, 60), "Song Currently Playing: \r\n" + PC.AS.clip.name);
				if (GUI.Button(new Rect(545, 540,35,20), rb)) {
                    moveMusicBackward();
                }
				if (GUI.Button(new Rect(645, 540, 35, 20), pb))
                {
                    stopMusic();
                }
				if (GUI.Button(new Rect(745, 540, 35, 20), ffb))
                {
                    moveMusicForward();
                }
            }
            else {
                GUI.Box(new Rect(520, 520, 280, 40), "Song Currently Playing: \r\nNone");
            }
            }
        }
	//Void that plays next song in the genre
    public void moveMusicForward()
    {
        if (selectedClip + 1 == listedGenres[selectedGenre].Count)
        {
            selectedClip = 0;
        }
        else {
            selectedClip++;
        }
        PC.AS.clip = listedGenres[selectedGenre][selectedClip].getAudio();
        songPlaying = listedGenres[selectedGenre][selectedClip].getTitle();
        PC.AS.Play();
    }
	//void that plays last song in the genre
    public void moveMusicBackward()
    {
        if (selectedClip == 0)
        {
            selectedClip = listedGenres[selectedGenre].Count - 1;
        }
        else
        {
            selectedClip--;
        }
        PC.AS.clip = listedGenres[selectedGenre][selectedClip].getAudio();
        songPlaying = listedGenres[selectedGenre][selectedClip].getTitle();
        PC.AS.Play();
    }
	//void that plays music if it is stopped, and stops music if it is playing
    public void stopMusic()
    {
        if (stopped)
        {
            stopped = false;
            PC.AS.Play();
        }
        else
        {
            stopped = true;
            PC.AS.Stop();
        }
    }
    }
