using UnityEngine;
using System.Collections;

public class Maid : MonoBehaviour {
    public float speed;
    Animator anim;

    Vector3 previous;
    public float velocity;

    Dialog d;
    //public bool facing; 
        bool going, going2, turning, sitting, sitDown;
    public bool comeOut, playerFinished;
    Transform Destination2, couchFace;
    GameObject InvisibleWalls;
    //public Transform Destination;
    // Use this for initialization
    void Start () {
        anim = this.gameObject.GetComponent<Animator>();
        going = false;
        going2 = false;
        turning = false;
        comeOut = false;
        playerFinished = false;
        sitting = false;
        sitDown = false;
        Destination2 = GameObject.Find("Cube1").transform;
        couchFace = GameObject.Find("CouchFace").transform;
        d = this.gameObject.GetComponent<Dialog>();

        InvisibleWalls = GameObject.FindGameObjectWithTag("InvisibleWalls");
    }

    // Update is called once per frame
    void Update () {

        float step = speed * Time.deltaTime;

        velocity = ((transform.position - previous).magnitude) / Time.deltaTime;
        previous = transform.position;
        if (Input.GetKeyDown(KeyCode.L))
        {
            d.facing = true;
        }
        Debug.Log(d.facing);
        //print(velocity);
        anim.SetFloat("speed", velocity);
        if (d.facing)
        {
            Vector3 dir = d.Destination.position - this.gameObject.transform.position;
            dir.y = 0; // keep the direction strictly horizontal
            Quaternion rot = Quaternion.LookRotation(dir);
            // slerp to the desired rotation over time
            this.gameObject.transform.rotation = Quaternion.Slerp(this.gameObject.transform.rotation, rot, speed * Time.deltaTime);
            anim.SetBool("isTurning", true);
            float angle = 10;
            if (Vector3.Angle(this.gameObject.transform.forward, d.Destination.position - this.gameObject.transform.position) < angle)
            {
                d.facing = false;
                going = true;
            }
        }
        else
        {
            anim.SetBool("isTurning", false);
        }
        if (turning)
        {
            Vector3 dir = Destination2.position - this.gameObject.transform.position;
            dir.y = 0; // keep the direction strictly horizontal
            Quaternion rot = Quaternion.LookRotation(dir);
            // slerp to the desired rotation over time
            this.gameObject.transform.rotation = Quaternion.Slerp(this.gameObject.transform.rotation, rot, speed * Time.deltaTime);
            anim.SetBool("isTurning", true);
            float angle = 10;
            if (Vector3.Angle(this.gameObject.transform.forward, Destination2.position - this.gameObject.transform.position) < angle)
            {
                turning = false;
                going2 = true;
            }
        }
        else
        {
            anim.SetBool("isTurning", false);
        }
        if (sitting)
        {
            Vector3 dir = couchFace.position - this.gameObject.transform.position;
            dir.y = 0; // keep the direction strictly horizontal
            Quaternion rot = Quaternion.LookRotation(dir);
            // slerp to the desired rotation over time
            this.gameObject.transform.rotation = Quaternion.Slerp(this.gameObject.transform.rotation, rot, speed * Time.deltaTime);
            anim.SetBool("isTurning", true);
            float angle = 10;
            if (Vector3.Angle(this.gameObject.transform.forward, couchFace.position - this.gameObject.transform.position) < angle)
            {
                sitting = false;
                sitDown = true;
            }
        }
        else
        {
            anim.SetBool("isTurning", false);
        }
        if (sitDown == true)
        {
            anim.SetBool("isSitting", true);

        }
        if (going)
        {
            transform.position = Vector3.MoveTowards(transform.position, (d.Destination.position - new Vector3(0,d.Destination.position.y,0)), step);
            InvisibleWalls.SetActive(false);
            playerFinished = true;
            if (Vector3.Distance(this.gameObject.transform.position, d.Destination.position) < 1)
            {
                if (d.Destination.name.Equals("Cube2"))
                {
                    PlayerPrefs.SetInt("Get Sig Other", 1);
                    turning = true;
                    comeOut = true;
                }
                else
                {
                    PlayerPrefs.SetInt("Get Sig Other", 2);
                    sitting = true;
                }
                going = false;
            }
        }
        if (going2)
        {
            transform.position = Vector3.MoveTowards(transform.position, Destination2.position - new Vector3(0, Destination2.position.y, 0), step);
            if (Vector3.Distance(this.gameObject.transform.position, Destination2.position) < 1)
            {
                sitting = true;
                going2 = false;
            }
        }
      
       

    }
}
