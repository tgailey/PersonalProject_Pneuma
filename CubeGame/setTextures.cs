using UnityEngine;
using System.Collections;

public class setTextures : MonoBehaviour {
	//public int tex;
	GameObject back, bottom, front, left, right, up;
	GameObject MC;
	public Renderer[] rends; /*ba,bo,fr,le,ri,u;*/
	public Texture[] dice = new Texture[6];
	public Texture[] rCube = new Texture[6];
	public Texture[] uRCube = new Texture[6];
	GUICubeScript GCS;
	// Use this for initialization
	void Start () {
		MC = GameObject.Find ("TheTasks/FirstTask/MainCube");
		GCS = MC.GetComponent<GUICubeScript> ();
		GCS.col = Color.white;
		rends = new Renderer[6];
		back = GameObject.Find ("TheTasks/FirstTask/MainCube/Cube/Back");
		//ba = back.GetComponent<Renderer> ();
		//ba.enabled = true;
		bottom = GameObject.Find ("TheTasks/FirstTask/MainCube/Cube/Bottom");
		//bo = bottom.GetComponent<Renderer> ();
		//bo.enabled = true;
		front = GameObject.Find ("TheTasks/FirstTask/MainCube/Cube/Front");
		//fr = front.GetComponent<Renderer> ();
		//fr.enabled = true;
		left = GameObject.Find ("TheTasks/FirstTask/MainCube/Cube/Left");
		//le = left.GetComponent<Renderer> ();
		//le.enabled = true;
		right = GameObject.Find ("TheTasks/FirstTask/MainCube/Cube/Right");
		//ri = right.GetComponent<Renderer> ();
		//ri.enabled = true;
		up = GameObject.Find ("TheTasks/FirstTask/MainCube/Cube/Top");
		//u = up.GetComponent<Renderer> ();
		//u.enabled = true;
		rends[0] = back.GetComponent<Renderer> ();
		rends[1] = bottom.GetComponent<Renderer> ();
		rends[2] = front.GetComponent<Renderer> ();
		rends[3] = left.GetComponent<Renderer> ();
		rends[4] = right.GetComponent<Renderer> ();
		rends[5] = up.GetComponent<Renderer> ();

		for (int i = 0; i< rends.Length; i++) {
			rends[i].enabled = true;
		}

	}
	
	// Update is called once per frame
	void Update () {
		//gameObject.transform.position = GCS.transform.position;	
		if (GCS.texT == 4) {
			//GCS.sX = .15f.ToString();
			for (int i = 0; i < rends.Length; i++) {
				rends [i].material.mainTexture = dice [i];
				rends[i].material.color = GCS.col;
			}
		}
		if (GCS.texT == 5) {
			//GCS.sX = .3f.ToString();
			for (int i = 0; i < rends.Length; i++) {
				rends [i].material.mainTexture = rCube [i];
				rends[i].material.color = Color.white;
			}
		}
		if (GCS.texT == 6) {
			//GCS.sX = .3f.ToString();
			for (int i = 0; i < rends.Length; i++) {
				rends [i].material.mainTexture = uRCube [i];
				rends[i].material.color = Color.white;
			}
		}
		/*if (tex > 3) {
			tex = 0;
		}*/
	}
}
