using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Goes from menu scene to the Main scene if Space is pressed.
//Attached to every scene that is not the game.


public class scene_change : MonoBehaviour {

	void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }else if (Input.GetKeyDown("space"))
        {
             SceneManager.LoadScene("Main", LoadSceneMode.Single);
        }
    }

	

}
