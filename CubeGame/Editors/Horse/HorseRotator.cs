using UnityEngine;
using System.Collections;

public class HorseRotator : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    public int HRNum;
    //public int lNum;
    //TaskScript TS;
    ThirdTask TT;
 //   private Rigidbody rb;
 //   private bool freeze;
    GameObject Rotators, LadderOne;
  //  Vector3 tempPos;



   // Vector3 startPos, startRot;
    //GameObject[] Ladders = new GameObject[3];
    // Use this for initialization


    void Start()
    {
       // freeze = true;
        //TS = GameObject.Find("TheTasks").GetComponent<TaskScript>();

        /*Ladders[0] = ST.ladders[0];
        Ladders[1] = ST.ladders[1];
        Ladders[2] = ST.ladders[2];*/
        TT = GameObject.Find("TheTasks/ThirdTask").GetComponent<ThirdTask>();
        Rotators = GameObject.Find("TheTasks/ThirdTask/Horse/Rotators");
     //   LadderOne = GameObject.Find("TheTasks/SecondTask/LadderOne");


    //    startPos = this.transform.localPosition;
    //    startRot = this.transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
      //  rb = ST.ladders[ST.texL].GetComponent<Rigidbody>();
       // lNum = ST.texL + 1;
      //  Vector3 tempRot1 = this.transform.eulerAngles;
      //  tempRot1.x = 0;
       // this.transform.eulerAngles = tempRot1;
      //  if (lNum == 1)
     //   {
            /*Vector3 tempPos;
			tempPos.x = LadderOne.transform.position.x + .3f;
			//tempPos.x = Mathf.Sin(LadderOne.transform.localEulerAngles.x) * 5 + (LadderOne.transform.position.x + .3f);
			tempPos.y = LadderOne.transform.position.y + 5;
			tempPos.z = LadderOne.transform.position.z - 2.9f;
			GameObject.Find("TheTasks/SecondTask/Rotators/RLX1").transform.position = tempPos;
			Vector3 tempRot;
			tempRot.x = LadderOne.transform.eulerAngles.x;
			tempRot.y = LadderOne.transform.eulerAngles.y + 90;
			tempRot.z = LadderOne.transform.eulerAngles.z - 117.5f;
			GameObject.Find("TheTasks/SecondTask/Rotators/RLX1").transform.eulerAngles = tempRot;*/

            /*if (RNum == 1) {
				Vector3 tempPos;
				//tempPos.x -= 2.71f;
				tempPos.x = -0.114f;
				//tempPos.y += 4.312f;
				tempPos.y = 4.528f;
				//tempPos.z += .266f;
				tempPos.z = 0.352f;
				Vector3 tempRot;
				tempRot.x = 0;
				tempRot.y = 270;
				tempRot.z = 117.5f;
				this.transform.localPosition = tempPos;
				this.transform.localEulerAngles = tempRot;
			} else if (RNum == 2) {
				Vector3 tempPos;
				//tempPos.x -= 2.71f;
				tempPos.x = -0.114f;
				//tempPos.y += 4.312f;
				tempPos.y = 4.528f;
				//tempPos.z += .266f;
				tempPos.z = -0.298f;
				Vector3 tempRot;
				tempRot.x = 0;
				tempRot.y = 270;
				tempRot.z = 242.5f;
				this.transform.localPosition = tempPos;
				this.transform.localEulerAngles = tempRot;
			}
			else if (RNum == 3) {
				Vector3 tempPos;
				//tempPos.x -= 2.71f;
				tempPos.x = 252.43f;
				//tempPos.y += 4.312f;
				tempPos.y = 12.28f;
				//tempPos.z += .266f;
				tempPos.z = 250.139f;
				Vector3 tempScale;
				tempScale.x = 63;
				tempScale.y = tempScale.x;
				tempScale.z = tempScale.x;
				this.transform.localPosition = tempPos;
				this.transform.localScale = tempScale;
			}*/
       // }
      // if (freeze)
       // {
         //   rb.constraints = RigidbodyConstraints.FreezeAll;
        //}
        //else if (!freeze)
        //{
       //     rb.constraints = RigidbodyConstraints.None;
        //}
        //if (Input.GetMouseButton(0))
        //{
        //    freeze = false;
        //}
        //else
        //{
         //   freeze = true;
        //}



        //this.transform.localPosition = startPos;
        //this.transform.localEulerAngles = startRot;
    }
    void OnMouseDown()
    {
        //freeze = false;
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }
    void OnMouseDrag()
    {
        //freeze = false;
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        //Vector3 temp = MC.transform.position;
        if (HRNum == 1 /*&& screenPoint.z > 0*/)
        {
            Debug.Log("DraggingOne");
            //Rigidbody rb;
            //rb = ST.ladders[ST.texL].GetComponent<Rigidbody>();
            //GameObject.Find ("TheTasks/SecondTask/LadderOne").transform.Rotate(new Vector3(((curPosition.y - gameObject.transform.position.y) + (curPosition.x - gameObject.transform.position.x))/3, 0, 0));

            TT.horse.transform.Rotate(new Vector3(((curPosition.y - gameObject.transform.position.y) + (curPosition.x - gameObject.transform.position.x)) / 3, 0, 0));

            //rb.MoveRotation(Quaternion.Euler(new Vector3(0, 0, -((curPosition.y - gameObject.transform.position.y) + (curPosition.x - gameObject.transform.position.x)))));
            //GameObject.Find ("TheTasks/SecondTask/Rotators/LadderOneRotators/RL1X1").transform.Rotate(new Vector3(((curPosition.y - gameObject.transform.position.y) + (curPosition.x - gameObject.transform.position.x)), 0, 0));

            //GameObject.Find ("TheTasks/SecondTask").transform.Rotate(new Vector3((curPosition.y - gameObject.transform.position.y) - (curPosition.x - gameObject.transform.position.x), 0, 0));
            //GameObject.Find ("TheTasks/SecondTask/LadderOne").transform.RotateAround (ST.RP.transform.position, new Vector3 (0, 0, 1), 10 * -((curPosition.y - gameObject.transform.position.y) + (curPosition.x - gameObject.transform.position.x)) * Time.deltaTime);
          //  if (lNum == 1)
          //  {
                //GameObject.Find("TheTasks/SecondTask/Rotators/LadderOneRotators/RL1X1").transform.RotateAround (ST.RP.transform.position, new Vector3 (0, 0, 1), 10 * -((curPosition.y - gameObject.transform.position.y) + (curPosition.x - gameObject.transform.position.x)) * Time.deltaTime);
           // }
            //Rotators.transform.RotateAround (ST.RP.transform.position, new Vector3 (0, 0, 1), 10 * -((curPosition.y - gameObject.transform.position.y) + (curPosition.x - gameObject.transform.position.x)) * Time.deltaTime);
        }
        else if (HRNum == 3 /*&& screenPoint.z < 0*/)
        {
            Debug.Log("DraggingThree");
            //GameObject.Find ("TheTasks/SecondTask/LadderOne").transform.Rotate(new Vector3(((curPosition.y - gameObject.transform.position.y) + (curPosition.x - gameObject.transform.position.x))/3, 0, 0));
            //GameObject.Find ("TheTasks/SecondTask/LadderOne").transform.Rotate(new Vector3(0, 0, ((curPosition.y - gameObject.transform.position.y) - (curPosition.x - gameObject.transform.position.x))/3));

            TT.horse.transform.Rotate(new Vector3(0, 0, ((curPosition.y - gameObject.transform.position.y) - (curPosition.x - gameObject.transform.position.x)) / 3));


            //GameObject.Find ("TheTasks/SecondTask/LadderOne").transform.RotateAround (ST.RP.transform.position, new Vector3 (0, 0, 1), 10 * -((curPosition.y - gameObject.transform.position.y) + (curPosition.x - gameObject.transform.position.x)) * Time.deltaTime);
            //Rotators.transform.RotateAround (ST.RP.transform.position, new Vector3 (0, 0, 1), 10 * -((curPosition.y - gameObject.transform.position.y) + (curPosition.x - gameObject.transform.position.x)) * Time.deltaTime);
          //  if (lNum == 1)
           // {
                // 	GameObject.Find ("TheTasks/SecondTask/Rotators/LadderOneRotators/RL1X2").transform.RotateAround (ST.RP.transform.position, new Vector3 (0, 0, 1), 10 * -((curPosition.y - gameObject.transform.position.y) + (curPosition.x - gameObject.transform.position.x)) * Time.deltaTime);
           // }

        }
        else if (HRNum == 2)
        {
            //GameObject.Find ("TheTasks/SecondTask/LadderOne").transform.Rotate(new Vector3(0, (curPosition.y - gameObject.transform.position.y) - (curPosition.x - gameObject.transform.position.x), 0));


           TT.horse.transform.Rotate(new Vector3(0, (curPosition.y - gameObject.transform.position.y) - (curPosition.x - gameObject.transform.position.x), 0));



            Debug.Log("DraggingTwo");
            //GameObject.Find ("TheTasks/SecondTask/LadderOne").transform.RotateAround (ST.RP.transform.position, new Vector3 (0, 1, 0), 10 * -((curPosition.y - gameObject.transform.position.y) + (curPosition.x - gameObject.transform.position.x)) * Time.deltaTime);
          //  if (lNum == 1)
         //   {
                //	GameObject.Find ("TheTasks/SecondTask/Rotators/LadderOneRotators/RLY").transform.RotateAround (ST.RP.transform.position, new Vector3 (0, 1, 0), 10 * -((curPosition.y - gameObject.transform.position.y) + (curPosition.x - gameObject.transform.position.x)) * Time.deltaTime);
        //    }
            //Rotators.transform.RotateAround (ST.RP.transform.position, new Vector3 (0, 1, 0), 10 * -((curPosition.y - gameObject.transform.position.y) + (curPosition.x - gameObject.transform.position.x)) * Time.deltaTime);
            //GameObject.Find ("TheTasks/SecondTask/LadderOne").transform.Rotate(new Vector3(0, (curPosition.y - gameObject.transform.position.y) - (curPosition.x - gameObject.transform.position.x), 0));
            //GameObject.Find ("TheTasks/SecondTask").transform.RotateAround (ST.RP.transform.position, new Vector3 (0, 1, 0), 10 * -((curPosition.y - gameObject.transform.position.y) + (curPosition.x - gameObject.transform.position.x)) * Time.deltaTime);
        }

    }
    void OnMouseUp()
    {
        //freeze = true;
    }
}
