using UnityEngine;
using System.Collections;

public class SingleSprite_ : MonoBehaviour {
	tk2dTextMesh m_text;
	tk2dSprite m_sprite;
	Vector2 size;
	
	void Start () {
	}
	
	public void Initialize(string spriteName){
		m_sprite = transform.GetChild(0).GetComponent<tk2dSprite>();
		m_sprite.SetSprite(spriteName);
		
		size = new Vector2(m_sprite.GetUntrimmedBounds().size.x, m_sprite.GetUntrimmedBounds().size.y); 
	}
	
	public float Width(){
		return size.x;
	}
	
	public float Height(){
		return size.y;
	}
}
