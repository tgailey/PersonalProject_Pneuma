using UnityEngine;
using System.Collections;
using System;

public class XMover : MonoBehaviour {
	GameObject MC;
	private Vector3 screenPoint;
	private Vector3 offset;
	public int MAxis;
	// Use this for initialization
	void Start () {
		MC = GameObject.Find ("TheTasks/FirstTask");
	}
	
	// Update is called once per frame
	void Update () {
		if (MAxis == 1)
		gameObject.transform.eulerAngles = new Vector3 (0,0,90);
		if (MAxis == 2)
			gameObject.transform.eulerAngles = new Vector3 (0,0,0);
		if (MAxis == 3)
			gameObject.transform.eulerAngles = new Vector3 (0,270,90);
		/*Vector3 temp = MC.transform.position;
		temp.y = 0.5f;
		if (MC.transform.position.y < .5) {
			MC.transform.position = temp;
		}*/
	}
	void OnMouseDown() {
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}
	void OnMouseDrag() {
		Debug.Log ("DraggingShit");
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		Vector3 temp = MC.transform.position;
		GUICubeScript GCS = MC.GetComponentInChildren<GUICubeScript>();
				if (MAxis == 1) {
					//GCS.pX = (MC.transform.position.x-500+curPosition.x).ToString();
					temp.x = curPosition.x;
					//MC.transform.position = temp;
					GCS.pX = ((float)Math.Round ((double)temp.x, 2) - 250).ToString ();
				} else if (MAxis == 2) { 
			//if (!GCS.cantMoveDown || curPosition.y - .05f > 0) {
					temp.y = curPosition.y;
					//MC.transform.position = temp;
					GCS.pY = ((float)Math.Round ((double)temp.y, 2) - .5f).ToString ();
			//}
				} else if (MAxis == 3) {
					temp.z = curPosition.z;
					//MC.transform.position = temp;
					GCS.pZ = ((float)Math.Round ((double)temp.z, 2) - 250).ToString ();
				}
	}
	void OnMouseUp() {
		GUICubeScript GCS = MC.GetComponentInChildren<GUICubeScript>();
        //GCS.cantMoveDown = false;
        if (MC.transform.position.y < (.5f - ((1 - MC.transform.localScale.x) / 2)))
        {
            /*  if (MC.transform.localScale.x > 1)
              GCS.pY = (Mathf.Abs((1 - MC.transform.localScale.x) / 2)).ToString();
              else
              GCS.pY = (-(1 - MC.transform.localScale.x) / 2).ToString();*/
            GCS.pY = (-(1 - MC.transform.localScale.x) / 2).ToString();
            //  cantMoveDown = true;
        }
    }
}
