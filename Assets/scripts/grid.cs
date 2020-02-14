using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This codes creates a grid of nodes
//it alsos checks if there is a wall at the
//nodes position and marks it as unwalkable if it's the case.

//It can also calculate which node an object is on by its world position.

public class grid : MonoBehaviour {

	public LayerMask walls;
	public Vector2 gridSize;
	public float radius;
	public node[,] gridlist;

	float diameter;
	int gridx, gridy;

	void Start(){

		diameter = radius*2;
		gridx = Mathf.RoundToInt(gridSize.x/diameter);
		gridy = Mathf.RoundToInt(gridSize.y/diameter);


		CreateGrid();

	}


	
	void CreateGrid(){

		//creates a 2 dimensional array of nodes.

		gridlist = new node[gridx, gridy];

		//places a point to the bottom left of the map to mark the first node.

		Vector3 BottomLeft = transform.position - Vector3.right*gridSize.x/2 - Vector3.up *gridSize.y/2;


		//Creates the node grid and checks if there is a wall for each node.

		for (int x = 0; x < gridx; x++){
			for (int y = 0; y < gridy; y++){

				Vector3 Worldpoint = BottomLeft +Vector3.right *(x*diameter +radius) + Vector3.up * (y*diameter + radius);
				bool walking = !(Physics.CheckSphere(Worldpoint, radius, walls));
				gridlist[x,y] = new node (walking, Worldpoint, x, y);
			}
		}
	}

	//Find nodes neighbors. Ignores own nodes.
	//returns a list of nodes for the pathfinding script.

	public List<node> GetNeighbors(node node){

		List<node> GetNeighbors = new List<node>();

		for(int x = -1; x<=1; x++){
				for(int y = -1; y<=1; y++){
					if(x == 0 && y == 0){
						continue;
					}

					int CheckX = node.positionX + x;
					int CheckY = node.positionY + y;

					//Makes sure to only add nodes that are part of the node array.

					if(CheckX >= 0 && CheckX < gridx && CheckY >= 0 && CheckY < gridy ){
						GetNeighbors.Add(gridlist[CheckX, CheckY]);
					}
			
			}
		}

		return GetNeighbors;

		 

	}

	//Find the node associated with an object by its world position.

	public node PacmanPosition (Vector3 worldPosition){

		//transform the x an y position in a float between 0 and 1.

		float percentX = (worldPosition.x + gridSize.x/2)/gridSize.x;
		float percentY = (worldPosition.y + gridSize.y/2)/gridSize.y;

		//Clamp variable. Used because PacMan goes out of the maze for the portals.

		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		//returns the corresponding node from the world position.

		int x = Mathf.RoundToInt((gridx-1)*percentX);
		int y = Mathf.RoundToInt((gridy-1)*percentY);

		return gridlist[x,y];
	}




}
