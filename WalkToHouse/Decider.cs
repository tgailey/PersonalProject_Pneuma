using UnityEngine;
using System.Collections;

public class Decider : MonoBehaviour {
    /*
     * This script decides where the player should go and do based on the amount of story that has been covered
     * */

	StayOnForever SOT;
    PlayerControl PC; // Part one controller
    PlayerControlC PCC; //Part two controller
    GameObject player;
    GameObject steps;
	// Use this for initialization
	void Start () {
        if (!GameObject.ReferenceEquals(GameObject.Find("GameHandler"), null))
        {
            SOT = GameObject.Find("GameHandler").GetComponent<StayOnForever>();
        }
        PC = this.gameObject.GetComponent<PlayerControl>();
        PCC = this.gameObject.GetComponent<PlayerControlC>();

        player = GameObject.FindGameObjectWithTag("Player");
        steps = GameObject.Find("Steps");
    }
	
	// Update is called once per frame
	void Update () {
        if (!GameObject.ReferenceEquals(GameObject.Find("GameHandler"), null))
        {
			//If already went inside, play second half
            if (SOT.inside == 1)
            {
                PC.enabled = false;
                steps.SetActive(false);
                Vector3 temp, temp2;
                temp.x = 137.1f;
                temp.y = 3.2f;
                temp.z = 102.6f;
                temp2.x = 0;
                temp2.y = 197.7247f;
                temp2.z = 0;
                player.transform.position = temp;
                //player.transform.eulerAngles = temp2;
            }
			//if not play first half
            else
            {
                PCC.enabled = false;
            }
        }
        else
        {
            PCC.enabled = false;
        }
        
	}
}
