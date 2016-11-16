using UnityEngine;
using System.Collections;
using System;

public class Resizer : MonoBehaviour
{

    GameObject MC;
    private Vector3 screenPoint;
    private Vector3 offset;
    // Use this for initialization
    void Start()
    {
        MC = GameObject.Find("TheTasks/FirstTask");
    }

    // Update is called once per frame
    void Update()
    {
        /* Vector3 temp = MC.transform.localScale;
		temp.x = 0;
		temp.y = 0;
		temp.z = 0;
		if (MC.transform.localScale.y < 0) {
			MC.transform.localScale = temp;
		}
		Vector3 temp2 = MC.transform.position;
		temp2.y = MC.transform.localScale.y/2;
		if (MC.transform.position.y < 	MC.transform.localScale.y/2) {
			MC.transform.position = temp2;
		}*/
    }
    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }
    void OnMouseDrag()
    {

        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        Vector3 temp = MC.transform.localScale;
        GUICubeScript GCS = MC.GetComponentInChildren<GUICubeScript>();
        temp.x = Vector3.Distance(MC.transform.position, curPosition) - 2;
        temp.y = Vector3.Distance(MC.transform.position, curPosition) - 2;
        temp.z = Vector3.Distance(MC.transform.position, curPosition) - 2;
        //MC.transform.localScale = temp;
        //Debug.Log (temp.x);
        GCS.sX = ((float)Math.Round((double)temp.x, 2)).ToString();
    }
    void OnMouseUp()
    {
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
