using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Teleports PacMan if triggered. 
//Will not teleport Pacman if it's going out of the portal.

public class portal_R : MonoBehaviour {

	public Transform Portal2;

	void Start(){

		Portal2 = GameObject.Find("left_portal").transform;

	}

	void OnTriggerStay(Collider other){

	 if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<player_move>().direction != 1)
        {
			other.gameObject.transform.position = Portal2.position;
			
           
        }

	}
}
