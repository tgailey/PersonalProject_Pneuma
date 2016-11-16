using UnityEngine;
using System.Collections;

public class StayOnForever : MonoBehaviour {
    public int inside = 0;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        if (!GameObject.ReferenceEquals(GameObject.FindGameObjectWithTag("Player"), null))
        {
            if (!Component.ReferenceEquals(GameObject.FindGameObjectWithTag("Player").GetComponent<MouseLook>(), null))
            {

                if (GameObject.FindGameObjectWithTag("Player").GetComponent<MouseLook>().enabled)
                {
                    Cursor.visible = false;
                }
                else
                {
                    Cursor.visible = true;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 4)
        {
            inside = 1;
        }
        else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 1)
        {
            inside = 0;
        }
    }
}
