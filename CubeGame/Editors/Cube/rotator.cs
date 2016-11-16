using UnityEngine;
using System.Collections;
using System;

public class rotator : MonoBehaviour {

	GameObject MC, GC;
	private Vector3 screenPoint;
	private Vector3 offset;
	public int RAxis;
	private float addingToX, addingToY, addingToZ;
	// Use this for initialization
	void Start () {
		MC = GameObject.Find ("Cube");
		GC = GameObject.Find ("TheTasks/FirstTask");
		//MC.transform.localEulerAngles = new Vector3 (45, 45, 45);
	}
	
	// Update is called once per frame
	void Update () {


		/*	
	 * Vector3 temp = MC.transform.position;
		temp.y = 0.5f;
		if (MC.transform.position.y < .5) {
			MC.transform.position = temp;
		}
*/
	}
	void OnMouseDown() {
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		/*MC.transform.localEulerAngles.x = GC.transform.localEulerAngles.x;
		MC.transform.localEulerAngles.y = GC.transform.localEulerAngles.y;
		MC.transform.localEulerAngles.z = GC.transform.localEulerAngles.z;*/
		MC.transform.localEulerAngles = GC.transform.localEulerAngles;
		Debug.Log (screenPoint.z);
	}
	void OnMouseDrag() {

		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		//Vector3 temp = MC.transform.localEulerAngles;
		if (RAxis == 1) {
			//temp.x = curPosition.x;
			//MC.transform.localEulerAngles = new Vector3(addingToX+ (curPosition.y) * 20- .7f, addingToY, addingToZ);
			MC.transform.Rotate(new Vector3(((curPosition.y - gameObject.transform.position.y) + (curPosition.x - gameObject.transform.position.x)), 0, 0));
		

			Debug.Log ("RotatingX");
			//MC.transform.LookAt(Vector3.up);
			//MC.transform.RotateAround(Vector3.zero, Vector3.up, curPosition.x*5.0f);
		} else if (RAxis == 2) {
			//temp.y = curPosition.y;
			//MC.transform.localEulerAngles = new Vector3(addingToX, addingToY+ curPosition.x * 20 - .7f, addingToZ);
			MC.transform.Rotate(new Vector3(0, (curPosition.y - gameObject.transform.position.y) - (curPosition.x - gameObject.transform.position.x), 0));
			Debug.Log ("RotatingY");
		} else if (RAxis == 3) {
			//temp.z = curPosition.z;
			//MC.transform.position = temp;
			MC.transform.Rotate(new Vector3(0, 0, (curPosition.y - gameObject.transform.position.y) - (curPosition.x - gameObject.transform.position.x)));
			Debug.Log ("RotatingZ");
		}
		Vector3 temp1 = MC.transform.localEulerAngles;
		GUICubeScript GCS = GameObject.Find("TheTasks/FirstTask").GetComponentInChildren<GUICubeScript>();
		GCS.rX = ((float) Math.Round( (double)temp1.x, 2)).ToString();
		GCS.rY = ((float) Math.Round( (double)temp1.y, 2)).ToString();
		GCS.rZ = ((float) Math.Round( (double)temp1.z, 2)).ToString();
		
	}
}
