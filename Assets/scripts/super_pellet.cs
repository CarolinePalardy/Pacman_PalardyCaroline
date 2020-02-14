using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class super_pellet : MonoBehaviour {

	//puts the ghosts in scared mode.

	 public GameObject blinky;
	 public GameObject inky;
	 public GameObject pinky;
	 public GameObject clyde;

	 public Transform startingPoint;

	 public float chrono = 0;


	void Start () {

		blinky = GameObject.Find("Blinky");
		inky = GameObject.Find("Inky");
		pinky = GameObject.Find("Pinky");
		clyde = GameObject.Find("Clyde");

		
	}
	
	void Update () {

		if(Time.time < chrono){
			blinky.GetComponent<Chasing>().scared = true;
			inky.GetComponent<Chasing>().scared = true;
			pinky.GetComponent<Chasing>().scared = true;
			clyde.GetComponent<Chasing>().scared = true;
			

		}else {

			blinky.GetComponent<Chasing>().scared = false;
			inky.GetComponent<Chasing>().scared = false;
			pinky.GetComponent<Chasing>().scared = false;
			clyde.GetComponent<Chasing>().scared = false;

		}
		
	}

	//On collision, the pellet will put ghosts in scared mode
	//and send them to the opposite corner of the screen.
	//this lasts for 7 seconds.

	 void OnCollisionEnter(Collision col) {
         if (col.gameObject.tag == "super")
        {
           chrono = Time.time + 7;
		   Destroy(col.gameObject);
        } 

        if (col.gameObject.tag == "ghost" && col.gameObject.GetComponent<Chasing>().scared == true)
        {
           
            col.gameObject.transform.position = startingPoint.position;
        }
}
}
