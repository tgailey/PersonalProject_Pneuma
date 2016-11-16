using UnityEngine;
using System.Collections;

public class PlayMenu : MonoBehaviour {

    float currentVelocity = 1.0f;
    float currentVelocity2 = -.4f;


    float currentVelocity3 = -1.0f;
    float currentVelocity4 = .4f;

    float decelerationRate = 1;

    public MeshRenderer cube;
    public MeshRenderer house;
    public MeshRenderer comingsoon;
    public BoxCollider cubeB, houseB;
    public LineRenderer l1;
    public LineRenderer l2;

    public LineRenderer lp1, lp2, lp3;

    float timer = 5;
    float ltimer = 2.5f;
    int i = 0;
    int k = 0;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (i==1 && transform.position.x < -28 && transform.position.y > 7)
        {   
            currentVelocity = currentVelocity - (decelerationRate * decelerationRate / 180);
            currentVelocity2 = currentVelocity2 + (decelerationRate * decelerationRate / 2000);
            transform.Translate(currentVelocity, currentVelocity2, 0);

            currentVelocity3 = -1.0f;
            currentVelocity4 = .4f;
        }

        if (k == 2 && transform.position.x > -114 && transform.position.y < 60)
        {
            currentVelocity3 = currentVelocity3 + (decelerationRate * decelerationRate / 180);
            currentVelocity4 = currentVelocity4 - (decelerationRate * decelerationRate / 2000);
            transform.Translate(currentVelocity3, currentVelocity4, 0);
            currentVelocity = 1.0f;
            currentVelocity2 = -.4f;

            if (ltimer > 0)
            {
                ltimer -= Time.deltaTime;
            }
            if(ltimer<= .15)
            {
                l1.enabled = true;
                l2.enabled = true;
            }
        }

        if (i == 1)
        {
            if (timer > 0)
            {

                timer -= Time.deltaTime;
            }

            if (timer <= 2.5)
            {
                house.enabled = true;
                houseB.enabled = true;
                lp1.enabled = true;
            }


            if (timer <= 1.5)
            {
                lp2.enabled = true;
                cube.enabled = true;
                cubeB.enabled = true;
            }

            if (timer <= .5)
            {
                lp3.enabled = true;
                comingsoon.enabled = true;
            }            
        }
    }

    void OnMouseUp() {
        if (i == 0)
        {
            ltimer = 2.5f;
            timer = 5;
            l1.enabled = false;
            l2.enabled = false;
            k = 1;

            //-115, 60
            //-28, 7

            //87 diff on x
            //53 diff on y
        }
        if (i == 1) // Called once you click the Play button a second time
        {
            lp1.enabled = false;
            lp2.enabled = false;
            lp3.enabled = false;
            houseB.enabled = false;
            cubeB.enabled = false;

            house.enabled = false;
            cube.enabled = false;
            comingsoon.enabled = false;
            
            i = -1; // Set to -1 because the increment increases it to zero so the next time they press it the previous if statement will run through
            k = 2;
        }

        i++;

    }
}
