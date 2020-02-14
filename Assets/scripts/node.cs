using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Node Class and attributes.
//contains each node informations.

public class node {

	public bool canWalk;
	public Vector3 world;
	public int positionX;
	public int positionY;

	public int gCost;
	public int hCost;
	public node parent;

	public node(bool _canWalk, Vector3 _world, int _positionX, int _positionY){

		canWalk = _canWalk;
		world = _world;
		positionX= _positionX;
		positionY = _positionY;

	}

	public int fCost{
	
	 get {
		return gCost+ hCost;
	 }

	}
}
