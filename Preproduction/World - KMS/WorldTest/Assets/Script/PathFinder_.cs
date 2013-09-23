using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class PathFinder_ {
	enum NodeStatus {Open, Cldsed};
	static Dictionary<Vector2, NodeStatus> nodeSatus = new Dictionary<Vector2, NodeStatus>();

	const int CostStraight = 10;
	const int CostDiagonal = 15;
	
	static List<PathNode_> openList = new List<PathNode_>();
	
	static Dictionary<Vector2, float> nodeCosts = new Dictionary<Vector2, float>();
	
	static void AddNodeToOpenList(PathNode_ node){
		int index = 0;
		float cost = node.m_totalCost;
		
		while((openList.Count > index) && (cost < openList[index].m_totalCost) ){
			index++;
		}
		
		openList.Insert(index, node);
		nodeCosts[node.GridLocation] = node.m_totalCost;
		nodeSatus[node.GridLocation] = NodeStatus.Open;
	}
	
	static List<PathNode_> FindAdjacentNodes(PathNode_ currentNode, PathNode_ endNode, Enemy_ enemy){
		List<PathNode_> adjacentNodes = new List<PathNode_>();
		
		int x = currentNode.GridX;
		int y = currentNode.GridY;
		
		bool upLeft = true;
		bool upRight = true;
		bool downLeft = true;
		bool downRight = true;
		
		if ((x > 0) && !enemy.IsWallCollision(x-1, y)) { // (X-1,Y) IS WALL?
			adjacentNodes.Add(new PathNode_(currentNode, endNode, new Vector2(x-1, y), CostStraight + currentNode.m_directCost));
		}
		else{
			upLeft = false;
			downLeft = false;
		}
		
		if((x < Backgound_.GetTileWidth() - 1) && !enemy.IsWallCollision(x+1, y)){ // (X+1, Y) IS Wall?
			adjacentNodes.Add(new PathNode_(currentNode, endNode, new Vector2(x+1, y), CostStraight + currentNode.m_directCost));
		}
		else{
			upRight = false;
			downRight = false;
		}
		
		if((y > 0) && !enemy.IsWallCollision(x, y-1)){
			adjacentNodes.Add(new PathNode_(currentNode, endNode, new Vector2(x, y-1), CostStraight + currentNode.m_directCost));
		}
		else{
			downLeft = false;
			downRight = false;
		}
		
		if((y < Backgound_.GetTileHeight() - 1) && !enemy.IsWallCollision(x, y+1)){ 
			adjacentNodes.Add(new PathNode_(currentNode, endNode, new Vector2(x, y+1), CostStraight + currentNode.m_directCost));
		}
		else{
			upLeft = false;
			upRight = false;
		}
		
		if((upLeft) && !enemy.IsWallCollision(x-1, y+1)){
			adjacentNodes.Add(new PathNode_(currentNode, endNode, new Vector2(x-1, y+1), CostStraight + currentNode.m_directCost));
		}
		
		if((upRight) && !enemy.IsWallCollision(x+1, y+1)){
			adjacentNodes.Add(new PathNode_(currentNode, endNode, new Vector2(x+1, y+1), CostStraight + currentNode.m_directCost));
		}
		
		if((downLeft) && !enemy.IsWallCollision(x-1, y-1)){
			adjacentNodes.Add(new PathNode_(currentNode, endNode, new Vector2(x-1, y-1), CostStraight + currentNode.m_directCost));
		}
		
		if((downRight) && !enemy.IsWallCollision(x+1, y-1)){
			adjacentNodes.Add(new PathNode_(currentNode, endNode, new Vector2(x+1, y-1), CostStraight + currentNode.m_directCost));
		}
		
		//Debug.Log(currentNode.ToString());
		
		return adjacentNodes;
	}
	
	static public List<Vector2> FindPath(Vector2 startTile, Vector2 endTile, Enemy_ enemy){
		//test null
		
		openList.Clear();
		nodeCosts.Clear();
		nodeSatus.Clear();
		
		PathNode_ startNode;
		PathNode_ endNode;
		
		endNode = new PathNode_(null, null, endTile, 0);
		startNode = new PathNode_(null, endNode, startTile, 0);
		
		AddNodeToOpenList(startNode);
		//Debug.Log(openList.Count);
		while(openList.Count > 0){
			PathNode_ currentNode = openList[openList.Count - 1];
			//Debug.Log(currentNode.ToString());
			
			if(currentNode.IsEqualToNode(endNode)){
				List<Vector2> bestPath = new List<Vector2>();
				while(currentNode != null){
					bestPath.Insert(0, currentNode.GridLocation);
					currentNode = currentNode.m_parentNode;
				}
				return bestPath;
			}
			
			openList.Remove(currentNode);
			nodeCosts.Remove(currentNode.GridLocation);
			
			foreach(PathNode_ possibleNode in FindAdjacentNodes(currentNode, endNode, enemy)){
				if(nodeSatus.ContainsKey(possibleNode.GridLocation)){
					if(nodeSatus[possibleNode.GridLocation] == NodeStatus.Cldsed){
						continue;
					}
					
					if(nodeSatus[possibleNode.GridLocation] == NodeStatus.Open){
						if(possibleNode.m_totalCost >= nodeCosts[possibleNode.GridLocation]){
							continue;
						}
					}
				}
				
				AddNodeToOpenList(possibleNode);
			}
			
			nodeSatus[currentNode.GridLocation] = NodeStatus.Cldsed;
		}
		return null;
	}	
}
