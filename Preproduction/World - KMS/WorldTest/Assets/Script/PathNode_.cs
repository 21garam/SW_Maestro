using UnityEngine;
using System.Collections;

public class PathNode_ {//: MonoBehaviour {
	public PathNode_ m_parentNode;
	public PathNode_ m_endNode;
	
	Vector2 m_gridLocation;
	public float m_totalCost;
	public float m_directCost;
	
	public Vector2 GridLocation{
		get {return m_gridLocation;}
		set {
			m_gridLocation = new Vector2(
				(float)Mathf.Clamp(value.x, 0f, (float)Backgound_.GetTileWidth()),
				(float)Mathf.Clamp(value.y, 0f, (float)Backgound_.GetTileHeight()));
		}
	}
	
	public int GridX{
		get {return (int)m_gridLocation.x;}
	}
	
	public int GridY{
		get {return (int)m_gridLocation.y;}
	}
	
	public PathNode_(PathNode_ parentNode, PathNode_ endNode, Vector2 gridLocation, float cost){
		m_parentNode = parentNode;
		m_endNode = endNode;
		m_gridLocation = gridLocation;
		m_directCost = cost;
		if(!(endNode == null))
			m_totalCost = m_directCost + LinearCost();
	}
	
	public float LinearCost(){
		return(Vector2.Distance(m_endNode.m_gridLocation, this.m_gridLocation));
	}
	
	public bool IsEqualToNode(PathNode_ node){
		return (m_gridLocation == node.m_gridLocation);
	}
	
	public override string ToString (){
		return string.Format ("[PathNode_: GridX={0}, GridY={1}, DirectCost={2}]", GridX, GridY, m_directCost);
	}
}
