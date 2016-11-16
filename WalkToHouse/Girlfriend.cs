using UnityEngine;
using System.Collections;

public class Girlfriend : MonoBehaviour {
    public float speed;
    Animator anim;

    Vector3 previous;
    public float velocity;
    public Transform Destination;
    float step;

    GameObject maid;
    Maid2 m;
    // Use this for initialization
    void Start () {
        maid = GameObject.Find("Maid");
        m = maid.GetComponent<Maid2>();

        Destination = GameObject.FindGameObjectWithTag("Player").transform;
        anim = this.gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        step = speed * Time.deltaTime;

        velocity = ((transform.position - previous).magnitude) / Time.deltaTime;
        previous = transform.position;
	}

}
