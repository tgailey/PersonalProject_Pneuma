using UnityEngine;
using System.Collections;

public class roseTest : MonoBehaviour {
	Renderer rend;
	Renderer[] rends;

    ForthTask FT;
    // Use this for initialization
    void Start()
    {
        FT = GameObject.Find("TheTasks/ForthTask").GetComponent<ForthTask>();
        rend = this.GetComponent<Renderer>();
        if (rend.materials.Length == 1)
        {
            rends = gameObject.GetComponentsInChildren<Renderer>();
            if (FT.dead == false)
            {
                foreach (Renderer rend2 in rends)
                {
                    if (rend2.gameObject.tag.Equals("FlowerPetals"))
                    {
                        rend2.material.color = rend.material.color;
                    }
                }
            }
            else if (FT.dead)
            {
                foreach (Renderer rend2 in rends)
                {
                    if (rend2.gameObject.tag.Equals("FlowerPetals"))
                    {
                        rend2.material.color = FT.color2.color;
                    }
                    else
                    {
                        rend2.material.color = FT.color1.color;
                    }
                }
            }
        }
        else if (rend.materials.Length > 1)
        {
            if (FT.dead)
            {
                
                rend.materials[0].color = FT.color2.color;
                rend.materials[1].color = FT.color3.color;
                rend.materials[2].color = FT.color1.color;
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
