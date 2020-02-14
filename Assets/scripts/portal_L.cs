using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Teleports PacMan if triggered. 
//Will not teleport Pacman if it's going out of the portal.

public class portal_L : MonoBehaviour {

	public Transform Portal1;

	void Start(){
		Portal1 = GameObject.Find("right_portal").transform;
	}

	void OnTriggerStay(Collider other){

	 if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<player_move>().direction != 2)
        {
			other.gameObject.transform.position = Portal1.position;
			
           
        }

	}
}
