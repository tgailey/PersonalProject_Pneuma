using UnityEngine;
using System.Collections;
using System;

public class HorseMover : MonoBehaviour
{
    GameObject TTO;
    private Vector3 screenPoint;
    private Vector3 offset;
    public int LAxis;
    private Vector3 startPos, startRot;
    bool freeze1;
    bool horseRotWork;
    bool movingX = false, movingY = false, movingZ = false;
    private Rigidbody rb;
    ThirdTask TT;
    // Use this for initialization
    void Start()
    {

        freeze1 = true;
        TTO = GameObject.Find("TheTasks/ThirdTask");
        TT = TTO.GetComponent<ThirdTask>();
        startPos = gameObject.transform.localPosition;
        startRot = gameObject.transform.eulerAngles;
        horseRotWork = true;


    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.eulerAngles = startRot;
        gameObject.transform.localPosition = startPos;

     /*   if (Input.GetMouseButton(0) && (movingX || movingY || movingZ))
        {
            freeze1 = false;
        }
        else
        {
            freeze1 = true;
        }
        if (freeze1)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else if (!freeze1 && horseRotWork)
        {
            //rb.constraints = RigidbodyConstraints.FreezeRotation;
            if (movingX)
            {
                rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                //rb.constraints = RigidbodyConstraints.FreezePositionZ;
            }
            else if (movingY)
            {
                rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                //rb.constraints = RigidbodyConstraints.FreezePositionZ;
            }
            else if (movingZ)
            {
                rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
                //rb.constraints = RigidbodyConstraints.FreezePositionY;	
            }
        }
        else if (!freeze1 && !horseRotWork)
        {
            rb.constraints = RigidbodyConstraints.None;
        }
        */



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

        Vector3 temp = TT.horse.transform.position;
        if (LAxis == 1)
        {
            Debug.Log("DraggingShit");
            //GCS.pX = (MC.transform.position.x-500+curPosition.x).ToString();
            temp.x = curPosition.x;
            //Below is working code. Anything else might break. Go back to here if fail 
           TT.horse.transform.position = temp;

            movingX = true;
            //ST.lpZ = ((float)Math.Round ((double)temp.x, 2)-ST.startPos.x).ToString ();
        }
        else if (LAxis == 2)
        {
            //if (!GCS.cantMoveDown || curPosition.y - .05f > 0) {
            temp.y = curPosition.y;
            TT.horse.transform.position = temp;
            //GCS.pY = ((float)Math.Round ((double)temp.y, 2) - .5f).ToString ();
            //}
            movingY = true;
        }
        else if (LAxis == 3)
        {
            temp.z = curPosition.z;
            TT.horse.transform.position = temp;
            movingZ = true;
            //GCS.pZ = ((float)Math.Round ((double)temp.z, 2) - 250).ToString ();

        }

    }
    void OnMouseUp()
    {
        horseRotWork = true;
        movingX = false;
        movingY = false;
        movingZ = false;
        /*	GUICubeScript GCS = MC.GetComponentInChildren<GUICubeScript>();
            //GCS.cantMoveDown = false;
            if (MC.transform.position.y < (.5f - ((1 - MC.transform.localScale.x) / 2)))
            {
                /*  if (MC.transform.localScale.x > 1)
                  GCS.pY = (Mathf.Abs((1 - MC.transform.localScale.x) / 2)).ToString();
                  else
                  GCS.pY = (-(1 - MC.transform.localScale.x) / 2).ToString();*/
        /* GCS.pY = (-(1 - MC.transform.localScale.x) / 2).ToString();
         //  cantMoveDown = true;
     } */
    }
}
