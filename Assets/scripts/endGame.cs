using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//If no pellets are left, ends the game and go to win screen.


public class endGame : MonoBehaviour {

	
	
	// Update is called once per frame
	void FixedUpdate () {

		if(transform.childCount == 0){
			SceneManager.LoadScene("win", LoadSceneMode.Single);
		}
		
	}
}
