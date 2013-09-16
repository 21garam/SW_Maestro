using UnityEngine;
using System.Collections;

public class Backgound_ : MonoBehaviour {
	public tk2dSprite m_sprite;
	MyDir4_ m_bound = new MyDir4_();
	
	void Start () {
		m_bound.left = m_sprite.GetBounds().min.x;
		m_bound.right = m_sprite.GetBounds().max.x;
		m_bound.up = m_sprite.GetBounds().max.y;
		m_bound.down = m_sprite.GetBounds().min.y;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public MyDir4_ GetBound(){
		return m_bound;
	}
}
