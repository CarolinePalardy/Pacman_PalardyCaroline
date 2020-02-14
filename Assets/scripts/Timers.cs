using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Activates PacMan and the ghosts at different times to mimic the game.
//Also so the game doesn't start as soon as the scene loads.

public class Timers : MonoBehaviour {
	public player_move pacman;
	public Blink_move ghost1, ghost2, ghost3, ghost4;
	

	
	public float time = 0;

	void start(){
		pacman = GameObject.Find("pacman").GetComponent<player_move>();
		ghost1 = GameObject.Find("Blinky").GetComponent<Blink_move>();
		ghost2 = GameObject.Find("Pinky").GetComponent<Blink_move>();
		ghost3 = GameObject.Find("Inky").GetComponent<Blink_move>();
		ghost4 = GameObject.Find("Clyde").GetComponent<Blink_move>();

	}
	
	
	void FixedUpdate () {

		time += Time.deltaTime;
		if(time > 3){
			pacman.enabled = true;
			ghost1.enabled = true;

		}

		if(time > 10){
			ghost2.enabled = true;

		}

		if(time > 15){
			ghost3.enabled = true;

		}

		if(time > 20){
			ghost4.enabled = true;
			Destroy(this);

		}



		
	}
}
