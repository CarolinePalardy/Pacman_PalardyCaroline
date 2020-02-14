using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PathFinder. Uses the grid and the nodes to find the shortest path to a goal.
//FindPath_nopac avoids PacMan, for scattering.

public class Chasing : MonoBehaviour {

	public grid grid;
	public Transform seeker, target, target2, corner, scary;
	Blink_move Blink;
	public canturn possible;
	public bool scatter;
	public bool scared;

	void Awake(){
		Blink = GetComponent<Blink_move>();
		scatter = true;
	}


	void FixedUpdate(){

		//Scatters sends the ghosts to their own corner
		//so they are not alway chasing PacMan.
		//The ghosts spend 5 seconds scattering and 15 chasing.
		if(!scared){
			GetComponent<SpriteRenderer>().color = Color.white;

		}
		if(scared){

			GetComponent<SpriteRenderer>().color = Color.gray;

			FindPath_nopac(seeker.position, scary.position, target2.position);

		} else if (scatter){

			FindPath_nopac(seeker.position, corner.position, target2.position);

			if(Time.time %5 == 1){
				scatter = false;

			}
		}else{

			//If the node is walkable. The ghost will go to their own side
			//of PacMan to circle him. If the node is not avalaible they will simply chase PacMan.
			
			if(possible.turn){
			FindPath_nopac(seeker.position, target.position, target2.position);
			}else{
			FindPath(seeker.position, target.position);
			}
			if(Time.time %20 == 1){
				scatter = true;
			}

		}

		
	}

	//Finds the closest path to PacMan from the ghost position.

	void FindPath(Vector3 StartPos, Vector3 targetPos){

		node StartNode = grid.PacmanPosition(StartPos);
		node targetNode = grid.PacmanPosition(targetPos);

		List<node> openSet = new List<node>();
		HashSet<node> closetSet = new HashSet<node>();

		openSet.Add(StartNode);

		while(openSet.Count > 0){

			node currentNode = openSet[0];
			for(int i = 1; i< openSet.Count; i++){
				if(openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost){

					currentNode = openSet[i];
				}
			}

			openSet.Remove(currentNode);
			closetSet.Add(currentNode);

			if(currentNode ==  targetNode){
				RetracePath(StartNode, targetNode);
				return;
			}

			foreach(node neighbor in grid.GetNeighbors(currentNode)){
				if(!neighbor.canWalk || closetSet.Contains(neighbor)){
					continue;
				}

				int newMovementCost = currentNode.gCost + GetDistance (currentNode, neighbor);

				if(newMovementCost < neighbor.gCost || !openSet.Contains(neighbor)){
					neighbor.gCost = newMovementCost;
					neighbor.hCost = GetDistance(neighbor, targetNode);

					neighbor.parent = currentNode;

					if(!openSet.Contains(neighbor)){
						openSet.Add(neighbor);
					}
				}


			}


		}
	}

	//Same PathFinding but the ghosts also try to not go through PacMan.

	void FindPath_nopac(Vector3 StartPos, Vector3 targetPos, Vector3 Pacman){

		node StartNode = grid.PacmanPosition(StartPos);
		node targetNode = grid.PacmanPosition(targetPos);
		node RoundPacman = grid.PacmanPosition(target2.position);

		List<node> openSet = new List<node>();
		HashSet<node> closetSet = new HashSet<node>();

		openSet.Add(StartNode);

		while(openSet.Count > 0){

			node currentNode = openSet[0];
			for(int i = 1; i< openSet.Count; i++){
				if(openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost){

					currentNode = openSet[i];
				}
			}

			openSet.Remove(currentNode);
			closetSet.Add(currentNode);

			//When the node reaches the target, calls RetracePath to make the list of nodes.

			if(currentNode ==  targetNode){
				RetracePath(StartNode, targetNode);
				return;
			}

			//Makes sure the grid is walkable, is not in the closed Set and is not PacMan.
			//This way. The ghost don't clump as much since they try to go around
			//PacMan to their own side. And they don't run into PacMan while scattering.

			foreach(node neighbor in grid.GetNeighbors(currentNode)){
				if(!neighbor.canWalk || closetSet.Contains(neighbor) || neighbor == RoundPacman){
					continue;
				}

				//Calculates the movement cost between the nodes.

				int newMovementCost = currentNode.gCost + GetDistance (currentNode, neighbor);

				//if the movement is efficient, adds the node to the list.

				if(newMovementCost < neighbor.gCost || !openSet.Contains(neighbor)){
					neighbor.gCost = newMovementCost;
					neighbor.hCost = GetDistance(neighbor, targetNode);

					neighbor.parent = currentNode;

					if(!openSet.Contains(neighbor)){
						openSet.Add(neighbor);
					}
				}


			}


		}
	}

	//Makes a list of all the nodes that will be used to guide the ghost.
	void RetracePath (node StartNode, node EndNode){
		List<node> path = new List<node>();
		node currentNode = EndNode;

		while(currentNode != StartNode){
			path.Add(currentNode);
			currentNode = currentNode.parent;
			
		}
		path.Reverse();
		Blink.path = path;
	}

	//Calculate the distance between the seeker and the target.
	//No diagonal needed so cost is set to 10.

	int GetDistance( node nodeA, node nodeB){
		int DistX = Mathf.Abs(nodeA.positionX - nodeB.positionX);
		int DistY = Mathf.Abs(nodeA.positionY - nodeB.positionY);
		return 10*DistX + 10*DistY;
	}
}
