using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Looks if the direction is available for Pacman.

public class canturn : MonoBehaviour {
public bool turn;




 void OnTriggerStay(Collider other){

	 if (other.gameObject.tag == "wall")
        {
           turn = false;
        }


}

void OnTriggerExit(Collider other){

	if (other.gameObject.tag == "wall")
        {
           turn = true;
        }

}
}
