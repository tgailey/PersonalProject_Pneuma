using UnityEngine;
using System.Collections;
using System;

public class LadderResizer : MonoBehaviour
{

    GameObject STO;
	SecondTask ST;
	GameObject ML;
    private Vector3 screenPoint;
    private Vector3 offset;

	private Vector3 startPos, startRot;
    // Use this for initialization
    void Start()
    {
        STO = GameObject.Find("TheTasks/SecondTask");
		ST = STO.GetComponent<SecondTask> ();

		startPos = gameObject.transform.localPosition;
		startRot = gameObject.transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
		ML = ST.ladders [ST.texL];
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
		gameObject.transform.eulerAngles = startRot;
		gameObject.transform.localPosition = startPos;
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
		Vector3 temp = ML.transform.localScale;
		//GUICubeScript GCS = ML.GetComponentInChildren<GUICubeScript>();
		temp.x = Vector3.Distance(ML.transform.position, curPosition) - 1.5f;
		temp.y = Vector3.Distance(ML.transform.position, curPosition) - 1.5f;
		temp.z = Vector3.Distance(ML.transform.position, curPosition) - 1.5f;
		ML.transform.localScale = temp;
        //Debug.Log (temp.x);
        //GCS.sX = ((float)Math.Round((double)temp.x, 2)).ToString();
    }
    void OnMouseUp()
    {
        //GUICubeScript GCS = MC.GetComponentInChildren<GUICubeScript>();
        //GCS.cantMoveDown = false;
		//if (ML.transform.position.y < (.5f - ((1 - ML.transform.localScale.x) / 2)))
        //{
            /*  if (MC.transform.localScale.x > 1)
              GCS.pY = (Mathf.Abs((1 - MC.transform.localScale.x) / 2)).ToString();
              else
              GCS.pY = (-(1 - MC.transform.localScale.x) / 2).ToString();*/
            //GCS.pY = (-(1 - MC.transform.localScale.x) / 2).ToString();
		//	Vector3 tempy = ML.transform.position;
		//	tempy.y = (-(1 - ML.transform.localScale.x) / 2);
		//	ML.transform.position = tempy; 
            //  cantMoveDown = true;
        //}
    }
}
