using UnityEngine;
using System.Collections;
// The RotateBrain script does what it says and rotates the brain shown in the background of the main menu.
public class RotateBrain : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		//Rotate the brain on the y axis (so it spins slowly) 10 units a second.
		transform.Rotate(new Vector3(0, Time.deltaTime*10, 0 ));
	}
}
