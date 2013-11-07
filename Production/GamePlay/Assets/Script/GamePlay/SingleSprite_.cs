using UnityEngine;
using System.Collections;

public class SingleSprite_ : MonoBehaviour {
	tk2dTextMesh m_text;
	tk2dSprite m_sprite;
	Vector2 size;
	
	void Start () {
	}
	
	public void Initialize(float w, float h, string spriteName){
		m_sprite = transform.GetChild(0).GetComponent<tk2dSprite>();
		bool hasSpriteName = m_sprite.SetSprite(spriteName);
		Debug.Log(hasSpriteName);
		if(!hasSpriteName)
			m_sprite.SetSprite("White");
		
		m_text = transform.GetChild(1).GetComponent<tk2dTextMesh>();
		m_text.text = "";
		if(!hasSpriteName)
			m_text.text = spriteName;
		m_text.transform.localScale = new Vector3(w, h, 1);
		m_text.Commit();
		
		if(!hasSpriteName){
			m_sprite.transform.localScale = new Vector3(w / 2, h / 2, 1);
			size = new Vector2(w, h);
		}
		else{
			size = new Vector2(m_sprite.GetUntrimmedBounds().size.x, m_sprite.GetUntrimmedBounds().size.y); 
		}
	}
	
	public float Width(){
		return size.x;
	}
	
	public float Height(){
		return size.y;
	}
}
