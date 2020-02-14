using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This dictates the movement of the ghosts.

public class Blink_move : MonoBehaviour {

	public List<node> path;
	public List<Vector3> waypoints;
	public int cur = 0;
	public float speed = 0.3f;

	public bool calculate;
	
	void FixedUpdate () {

		//Only updates the list of nodes at certain
		//points or time during the game instead of constantly.

		if(calculate){

			waypoints.Clear();
			cur = 0;

			foreach (node n in path)
		{
			waypoints.Add(n.world);
		}

			calculate = false;

		}

		//Only move if the list is not empty.

		if(waypoints.Count > 0){

			if(transform.position != waypoints[cur]){
				Vector3 p = Vector3.MoveTowards(transform.position,waypoints[cur],speed);
        		GetComponent<Rigidbody>().MovePosition(p);
			} 

			//Go to the next node if object if very close to
			//the node it is supposed to go to.
		
			if(Vector3.Distance(transform.position, waypoints[cur])< 0.3f){

				if(cur < waypoints.Count-1){

					cur = cur+1;
				}else if (cur == waypoints.Count-1){
					calculate = true;
				}	
			
			}

			//If the list is empty, calculate in case the target changes.
		}else{
			calculate = true;
		}


	
		
	}
}
