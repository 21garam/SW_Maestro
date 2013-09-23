using UnityEngine;
using System.Collections;

public class FeedFish_ : MonoBehaviour {
	public tk2dSprite m_sprite;
	public tk2dTextMesh m_textPrefabs;
	
	public static float MOVE_LENTH = 32 * 2;
	public Backgound_ m_backGround;
	
	Vector2 m_nextPos = new Vector2();
	float m_leftBound;
	float m_rightBound;
	float m_dir;
	float m_velocity;
	FeedType_ m_feedyType;
	public int m_rowPos;
	public int m_colPos;
	tk2dTextMesh m_text;
	
	void Start () {
		//Debug.Log(m_sprite.color.a);
		//Initialize();
	}
	
	public void ReInitialize(){
		float dir = Random.value - 0.5f;
		float xPadding;
		if(dir >= 0)
			xPadding = -FeedFish_.MOVE_LENTH * 0.5f;
		else
			xPadding = +FeedFish_.MOVE_LENTH * 0.5f;
		transform.position = new Vector3(32.0f + m_backGround.GetTilePosX(m_rowPos) + xPadding, m_backGround.GetTilePosY(m_colPos), transform.position.z);
		
		state = State_.ALIVEING;
		
		SubInitialize(dir);
	}
	
	public void Initialize(float dir, int row, int col){
		m_rowPos = row;
		m_colPos = col;
		m_text = GameObject.Instantiate(m_textPrefabs, transform.position, transform.rotation) as tk2dTextMesh;
		SubInitialize(dir);
		state = State_.INIT;
	}
	
	Vector3 rayDir;
	Vector3 rayPos;
	bool ExistInitialize(float dir){
		RaycastHit hit;
		if(dir > 0){
			rayDir = transform.right;
			rayPos = new Vector3(m_leftBound, transform.position.y, transform.position.z);
		}
		else{
			rayDir = -transform.right;
			rayPos = new Vector3(m_rightBound, transform.position.y, transform.position.z);
		}
		
		bool retBool = true;
		if(Physics.Raycast(rayPos, rayDir, out hit, MOVE_LENTH)){
			if(hit.collider.gameObject.tag == "TERRAIN"){
				retBool = false;
				//Debug.Log("AA");
			}
		}
		
		rayDir = -rayDir;
		if(rayDir.x < 0)
			rayPos = new Vector3(rayPos.x + MOVE_LENTH, rayPos.y, rayPos.z);
		else
			rayPos = new Vector3(rayPos.x - MOVE_LENTH, rayPos.y, rayPos.z);
		if(Physics.Raycast(rayPos, rayDir, out hit, MOVE_LENTH)){
			if(hit.collider.gameObject.tag == "TERRAIN"){
				retBool = false;
				//Debug.Log("AA");
			}
		}
		
		return retBool;
	}
	
	void OnDrawGizmos(){
     	//Gizmos.color = Color.red;
		//Gizmos.DrawRay(rayPos, rayDir * MOVE_LENTH);
	}
	
	void SubInitialize(float dir){
		m_dir = dir;
		m_velocity = Random.Range(50.0f, 100.0f);
		m_feedyType = (FeedType_)Random.Range(0, 4);
		if(m_dir >= 0) {
			m_dir = 1;
			m_leftBound = transform.position.x;
			m_rightBound = transform.position.x + MOVE_LENTH;
		}
		else {
			m_dir = -1;
			m_leftBound = transform.position.x - MOVE_LENTH;
			m_rightBound = transform.position.x;
		}
		
		switch(m_feedyType){
			case FeedType_.SPEED_UP:
				m_text.text = "S+";
			break;
			
			case FeedType_.SPEED_DOWN:
				m_text.text = "S-";
			break;
			
			case FeedType_.SIZE_UP:
				m_text.text = "P+";
			break;
			
			case FeedType_.SIZE_DOWN:
				m_text.text = "P-";
			break;
		}
		m_text.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		m_text.Commit();
	}
	
	public enum FeedType_{
		SPEED_UP,
		SPEED_DOWN,
		SIZE_UP,
		SIZE_DOWN,
	}
	
	public FeedType_ FeedType{
		get{return m_feedyType;}
	}
	
	public enum State_{
		NOT,
		INIT,
		ALIVEING,
		ALIVE,
		DIE,
	}
	
	public State_ state;
	
	// Update is called once per frame
	void Update () {
		switch(state){
			case State_.NOT:
			break;
			
			case State_.INIT:
				if(ExistInitialize(m_dir))
					state = State_.ALIVE;
				else{
					state = State_.NOT;
					transform.position = new Vector3(-100, -100, transform.position.z);
					m_text.transform.position = new Vector3(-100, -100, transform.position.z);
				}
			break;
			
			case State_.ALIVEING:
				m_sprite.color = new Color(m_sprite.color.a, m_sprite.color.g, m_sprite.color.b, m_sprite.color.a + Time.deltaTime);
				if(m_sprite.color.a >= 1){
					m_sprite.color = new Color(m_sprite.color.a, m_sprite.color.g, m_sprite.color.b, 1);
					state = State_.ALIVE;
				}
			break;
			
			case State_.ALIVE:
				//ExistInitialize(m_dir);
				m_nextPos.x = transform.position.x;
				Update_MerryGoRound();
				Update_View(m_dir);
				transform.position = new Vector3(m_nextPos.x, this.transform.position.y, this.transform.position.z);
				if(m_text != null)
					m_text.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
			break;
			
			case State_.DIE:
				Update_ReSpwanTimer();
			break;
		}
	}
	
	
	
	float m_timeLine;
	private void Update_ReSpwanTimer(){
		m_timeLine += Time.deltaTime;
		if(m_timeLine > 3.0f){
			ReInitialize();
			m_timeLine = 0;
		}
	}
	
	private void Update_View(float x){
		if(x > 0){
			m_sprite.FlipX = true;
		}
		else if(x < 0){
			m_sprite.FlipX = false;
		}
	}
	
	void Update_MerryGoRound(){
		m_nextPos.x += m_dir * m_velocity * Time.deltaTime;
		if(m_dir >= 0){
			if(m_nextPos.x > m_rightBound){
				m_dir = -1;
				m_nextPos.x = m_rightBound;
			}
		}
		else{
			if(m_nextPos.x < m_leftBound){
				m_dir = 1;
				m_nextPos.x = m_leftBound;
			}
		}
	}
	
	public void Destroy(){
		m_sprite.color = new Color(m_sprite.color.a, m_sprite.color.g, m_sprite.color.b, 0);
		transform.position = new Vector3(-100, -100, transform.position.z);
		m_text.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		state = State_.DIE;
		//enabled = false;
	}
}
