using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Simple script telling the ghost to re-calculate its path.
//this makes it so the ghosts are not constantly calculating paths
//it makes them just a bit less efficient so the game is still playable.

public class calculators : MonoBehaviour {

	void OnTriggerEnter(Collider other){

	if (other.gameObject.tag == "ghost")
        {
            other.gameObject.GetComponent<Blink_move>().calculate = true;
        }

}
}
