using UnityEngine;
using System.Collections;

public class RotatePointTest : MonoBehaviour {
	Vector3 startPos, startRot;
	// Use this for initialization
	void Start () {
		startPos = this.transform.localPosition;
		startRot = this.transform.localEulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.localPosition = startPos;
		this.transform.localEulerAngles = startRot;
	}
}
