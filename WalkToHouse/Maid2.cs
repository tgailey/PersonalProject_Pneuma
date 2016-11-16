using UnityEngine;
using System.Collections;

public class Maid2 : MonoBehaviour {
    Dialog da; 
    public float speed; //Walk speed
    Animator anim; //Maid animator
	 
    Vector3 previous; //Vector3 for previous location
    public float velocity; //Velocity read in animator
    public Transform Destination; //Destination we are going
    GameObject InvisibleWalls; //Invisible walls gameobject trapping player in at very beginnign
    public bool turn = false, go = false, sit = false, hit = false; //Whether or not we are turning, moving, sitting, or hitting our targer
	public  bool playerFinished = false;//Whether or not we are finished talking,
	public bool comeOut = false, firstTime = true; //Whether or not it is the first time talking to her our not
    float step;

    Transform couchFace;
	// Use this for initialization
	void Start () {
        couchFace = GameObject.Find("CouchFace").transform;
        anim = this.gameObject.GetComponent<Animator>();
        da = this.gameObject.GetComponent<Dialog>();
        InvisibleWalls = GameObject.FindGameObjectWithTag("InvisibleWalls");
	}
	
	// Update is called once per frame
	void Update () {
        step = speed * Time.deltaTime;

        velocity = ((transform.position - previous).magnitude) / Time.deltaTime;
        previous = transform.position;
        if (da.facing)
        {
            rotate(da.Destination);
        }

		//Set animations based on booleans that are true
        if (turn)
        {
            anim.SetBool("isTurning", true);
            rotate(Destination);
        }
        else
        {
            anim.SetBool("isTurning", false);
        }
        if (go)
        {
            anim.SetBool("isWalking", true);
            travel(Destination);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        if (sit)
        {
            go = false;
            turn = false;
            anim.SetBool("isWalking", false);
            anim.SetBool("isTurning", false);
            anim.SetBool("isSitting", true);

        }
        if (Destination == couchFace)
        {
            sit = true;
            go = false;
            turn = false;
        }
    }
    public void rotate(Transform d)
    {
		//When called rotate toward set destination
        hit = false;
        turn = true;
        Destination = d;
        Vector3 dir = d.position - this.gameObject.transform.position;
        dir.y = 0; // keep the direction strictly horizontal
        Quaternion rot = Quaternion.LookRotation(dir);
        // slerp to the desired rotation over time
        this.gameObject.transform.rotation = Quaternion.Slerp(this.gameObject.transform.rotation, rot, speed * Time.deltaTime);
        float angle = 10;
        if (Vector3.Angle(this.gameObject.transform.forward, d.position - this.gameObject.transform.position) < angle)
        {
            da.facing = false;
            turn = false;
            if (!d.name.Equals("CouchFace")) {
                travel(Destination);
            }
            else
            {
                sit = true;
            }
            
        }
        else if (Vector3.Angle(this.gameObject.transform.forward, d.position - this.gameObject.transform.position) < angle)
        {
            sit = true;
        }
    } 
    public void travel (Transform d)
    {
		//When called, move toward set destination
        go = true;
        
        transform.position = Vector3.MoveTowards(transform.position, (d.position - new Vector3(0, d.position.y, 0)), step);
        if (InvisibleWalls.activeInHierarchy)
        InvisibleWalls.SetActive(false);
        playerFinished = true;
        if (Vector3.Distance(this.gameObject.transform.position, d.position) < 1)
        {
            if (firstTime)
            {
                if (d.name.Equals("Cube2"))
                {
                    PlayerPrefs.SetInt("Get Sig Other", 1);
                    //turning = true;
                    comeOut = true;
                }
                else
                {
                    PlayerPrefs.SetInt("Get Sig Other", 2);
                    //rotate(couchFace);
                }
                firstTime = false;
            }
            go  = false;
            hit = true;
        }
    }
}
