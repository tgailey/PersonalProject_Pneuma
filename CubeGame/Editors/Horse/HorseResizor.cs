using UnityEngine;
using System.Collections;
using System;

public class HorseResizor : MonoBehaviour
{

    GameObject TTO;
    ThirdTask TT;
    //GameObject ML;
    private Vector3 screenPoint;
    private Vector3 offset;

    //private Vector3 startPos, startRot;
    // Use this for initialization
    void Start()
    {
        TTO = GameObject.Find("TheTasks/ThirdTask");
        TT = TTO.GetComponent<ThirdTask>();

      //  startPos = gameObject.transform.localPosition;
       // startRot = gameObject.transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
       // ML = ST.ladders[ST.texL];
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
     //   gameObject.transform.eulerAngles = startRot;
     //   gameObject.transform.localPosition = startPos;
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
        Vector3 temp = TT.horse.transform.localScale;
        //GUICubeScript GCS = ML.GetComponentInChildren<GUICubeScript>();
        temp.x = Vector3.Distance(TT.horse.transform.position, curPosition) - 3;
        temp.y = Vector3.Distance(TT.horse.transform.position, curPosition) - 3;
        temp.z = Vector3.Distance(TT.horse.transform.position, curPosition) - 3;
        TT.horse.transform.localScale = temp;
        //Debug.Log (temp.x);
        //GCS.sX = ((float)Math.Round((double)temp.x, 2)).ToString();
    }
    void OnMouseUp()
    {
        //Will use below later, have to play with numbers

      /*  if (TT.horse.transform.position.y < (.5f - ((1 - TT.horse.transform.localScale.x) / 2)))
        {
            Vector3 tempy = TT.horse.transform.position;
            tempy.y = (-(1 - TT.horse.transform.localScale.x) / 2);
            TT.horse.transform.position = tempy;
        }*/
    }
}
