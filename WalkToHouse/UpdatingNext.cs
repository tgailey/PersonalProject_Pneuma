using UnityEngine;
using System.Collections;

public class UpdatingNext : MonoBehaviour {
    GameObject maid;
    Maid2 m;

    public Transform nextLocation;
	// Use this for initialization
	void Start () {
        maid = GameObject.Find("Maid");
        m = maid.GetComponent<Maid2>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (m.hit == true && m.Destination == this.transform)
        {
            m.hit = false;
            m.rotate(nextLocation);
        }
	}
}
