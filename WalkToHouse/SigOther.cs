using UnityEngine;
using System.Collections;

public class SigOther : MonoBehaviour {
    public float speed;
    Animator anim;

    Vector3 previous;
    public float velocity;

    GameObject maid;
    Maid m;

    GameObject player;
    public bool isFacing, isMoving;
    Transform Destination;
    // Use this for initialization
    void Start () {
        isFacing = false;
        isMoving = false;
        anim = this.gameObject.GetComponent<Animator>();
        maid = GameObject.Find("Maid");
        m = maid.GetComponent<Maid>();
        player = GameObject.FindGameObjectWithTag("Player");
        
    }
	
	// Update is called once per frame
	void Update () {
        float step = speed * Time.deltaTime;
        velocity = ((transform.position - previous).magnitude) / Time.deltaTime;
        previous = transform.position;
        anim.SetFloat("speed", velocity);
        float angle = 12f;
        if (m.comeOut)
        {
            if (!isFacing)
            {
                Vector3 dir = player.transform.position - this.gameObject.transform.position;
                dir.y = 0; // keep the direction strictly horizontal
                Quaternion rot = Quaternion.LookRotation(dir);
                // slerp to the desired rotation over time
                this.gameObject.transform.rotation = Quaternion.Slerp(this.gameObject.transform.rotation, rot, speed * Time.deltaTime);
                anim.SetBool("isTurning", true);
                isMoving = false;
            }
            else if (!(Vector3.Distance(this.gameObject.transform.position , player.transform.position) < 1f))
            {
                isMoving = true;
                anim.SetBool("isTurning", false);
            }
            else
            {
                isMoving = false;
                anim.SetBool("isTurning", false);
            }
            if (isMoving)
            {
                //isFacing = false;
                anim.SetBool("isMoving", true);
                this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, player.transform.position - new Vector3(0, player.transform.position.y, 0), step);
            }
            else
            {
                anim.SetBool("isMoving", false);

            }
            if (Vector3.Angle(this.gameObject.transform.forward, player.transform.position - this.gameObject.transform.position) < angle)
            {
                isFacing = true;
            }
            else
            {
                isFacing = false;
            }
        }
    }
}
