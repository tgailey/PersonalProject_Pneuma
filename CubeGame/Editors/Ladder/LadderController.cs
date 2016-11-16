using UnityEngine;
using System.Collections;

public class LadderController : MonoBehaviour {
	SecondTask Main;
	GameObject ST;

	LadderMover LM;
	int lTexNum = 0;
	// Use this for initialization
	void Start () {
		ST = GameObject.Find ("TheTasks/SecondTask");
		Main = ST.GetComponent<SecondTask> ();
		//Rotators = GameObject.FindGameObjectWithTag ("Rotators");
		//Movers = GameObject.FindGameObjectWithTag ("Movers");
		//Resizers = GameObject.FindGameObjectWithTag ("Resizers");

		//Rotators.SetActive (false);
		//Movers.SetActive (false);
		//Resizers.SetActive (false);
		LM = ST.GetComponentInChildren<LadderMover> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "Cube") {
			LM.rotWork = false;
		}
	}
}
