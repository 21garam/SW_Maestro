using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy_ : MonoBehaviour {
	public tk2dSprite m_sprite;
	public Camera_ m_cam;
	public Backgound_ m_backGround;
	public Player_ m_player;
	public InfoText_ m_infoText;
	public FeedFishManager_ m_fishManager;
	
	public Vector2 m_size = new Vector2();
	Vector2 m_orinHalfSize = new Vector2();
	Vector2 m_halfSize = new Vector2();
	
	float m_moveSpeed = 250.0f;
	//openList
	//closedList
	//eight dir search at openlist
	List<Vector2> pathList;
	
	float incRadiusSize;// = sphereCollider.radius * 0.1f;
	float incScaleSize;// = 
		
	void Start () {
		Start_ThisSize();
		Start_ThisPosition();
		
		SphereCollider sphereCollider = collider as SphereCollider;
		incRadiusSize = sphereCollider.radius * 0.1f;
		incScaleSize = m_sprite.scale.x * 0.1f;
	}
	
	void Start_ThisSize(){
		m_size.x = m_sprite.GetUntrimmedBounds().size.x;
		m_size.y = m_sprite.GetUntrimmedBounds().size.y;
		m_halfSize.x = m_size.x * 0.5f;
		m_halfSize.y = m_size.y * 0.5f;
		
		m_orinHalfSize.x = m_halfSize.x;
		m_orinHalfSize.y = m_halfSize.y;
		
		m_infoText.SetPlayerInfo(m_moveSpeed, m_sprite.scale.x);
	}
	
	void Start_ThisPosition(){
		transform.position = new Vector3(m_backGround.GetBound().left + m_halfSize.x, 
										 m_backGround.GetBound().down + m_halfSize.y, 
										 transform.position.z);
	}
	
	int idx = 0;
	Vector2 dir = new Vector2();
	Vector2 nextPos = new Vector2();
	//float velocity = 100f;
	//bool first = false;
	/*
	void Update_TracePathList(){
		if(!first){
			pathList = PathFinder_.FindPath(new Vector2(0,0), new Vector2(Backgound_.GetTileWidth()-1, Backgound_.GetTileHeight()-1), this);
			pathList.RemoveAt(0);
			//for(int i = 0; i < pathList.Count; i++){
			//	Debug.Log(pathList[i]);
			//}
			dir.Set(m_backGround.GetTilePosX((int)pathList[idx].x) - transform.position.x, m_backGround.GetTilePosY((int)pathList[idx].y) - transform.position.y); 
			dir.Normalize();
			//Debug.Log(dir);
			first = true;
		}
		if(idx < pathList.Count){
			nextPos.Set(transform.position.x + dir.x * Time.deltaTime * m_moveSpeed, transform.position.y + dir.y * Time.deltaTime * m_moveSpeed);
			bool endX = false;
			bool endY = false;
			if(dir.x >= 0){
				if(nextPos.x >= m_backGround.GetTilePosX((int)pathList[idx].x)){
					nextPos.x = m_backGround.GetTilePosX((int)pathList[idx].x);
					endX = true;
				}
			}
			else{
				if(nextPos.x <= m_backGround.GetTilePosX((int)pathList[idx].x)){
					nextPos.x = m_backGround.GetTilePosX((int)pathList[idx].x);
					endX = true;
				}
			}
			
			if(dir.y >= 0){
				if(nextPos.y >= m_backGround.GetTilePosY((int)pathList[idx].y)){
					nextPos.y = m_backGround.GetTilePosY((int)pathList[idx].y);
					endY = true;
				}
			}
			else{
				if(nextPos.y <= m_backGround.GetTilePosY((int)pathList[idx].y)){
					nextPos.y = m_backGround.GetTilePosY((int)pathList[idx].y);
					endY = true;
				}
			}
			
			if(endX && endY){
				int preIDX = idx;
				idx++;
				if(idx < pathList.Count){
					dir.Set(m_backGround.GetTilePosX((int)pathList[idx].x) - m_backGround.GetTilePosX((int)pathList[preIDX].x), 
							m_backGround.GetTilePosY((int)pathList[idx].y) - m_backGround.GetTilePosY((int)pathList[preIDX].y)); 
					dir.Normalize();
				}
			}
			
			transform.position = new Vector3(nextPos.x, nextPos.y, transform.position.z);
			//Debug.Log(idx.ToString() + ", " + dir.ToString());
		}
		else{
			state = State_.WAIT;
		}
	}
	*/
	
	public enum State_{
		TRACE,
		WAIT,
		SEARCH,
		GO,
		TRACEFEED
	}
	
	State_ state = State_.SEARCH;
	
	FeedFish_ targetFish = null;
	int goGridX = 0;
	int goGridY = 0;
	void Update () {
		switch(state){
			case State_.TRACE:
				pathList = PathFinder_.FindPath(new Vector2(m_backGround.GetTileGridX(transform.position.x), m_backGround.GetTileGridY(transform.position.y)), 
												new Vector2(m_backGround.GetTileGridX(m_player.transform.position.x), m_backGround.GetTileGridY(m_player.transform.position.y)), this);
				if(pathList != null && pathList.Count > 1){
					dir.Set(m_backGround.GetTilePosX((int)pathList[1].x) - transform.position.x, 
							m_backGround.GetTilePosY((int)pathList[1].y) - transform.position.y); 
					dir.Normalize();
					nextPos.Set(transform.position.x + dir.x * Time.deltaTime * m_moveSpeed, transform.position.y + dir.y * Time.deltaTime * m_moveSpeed);
					transform.position = new Vector3(nextPos.x, nextPos.y, transform.position.z);
				}
				else {
					state = State_.SEARCH;
				}
			
				if(!(m_size.x > m_player.m_size.x)){ //&& m_moveSpeed > m_player.m_moveSpeed)){
					state = State_.WAIT;
				}
			break;
			
			case State_.WAIT:
				if(m_size.x > m_player.m_size.x){ //&& m_moveSpeed > m_player.m_moveSpeed){
					state = State_.TRACE;
				}
				else{
					state = State_.SEARCH;
				}
			break;
			
			case State_.SEARCH:
				float MIN = 100000000000.0f;
				Vector2 thisGrid = new Vector2();
				Vector2 feedGrid = new Vector2();
				for(int i = 0; i < m_fishManager.feedFishList.Count; i++){
					FeedFish_ feed = m_fishManager.feedFishList[i];
					//Debug.Log(feed.m_rowPos.ToString() + ", " + feed.m_colPos.ToString());
					//Debug.Log(feed.state);
					if(feed.state != FeedFish_.State_.NOT && feed.state != FeedFish_.State_.DIE){
						thisGrid.x = m_backGround.GetTileGridX(transform.position.x);
						thisGrid.y = m_backGround.GetTileGridX(transform.position.y);	
						feedGrid.x = feed.m_rowPos;
						feedGrid.y = feed.m_colPos;
						float dist = Vector2.Distance(thisGrid, feedGrid);
						//Debug.Log(dist);
						if(dist < MIN){
						  	MIN = dist;
							targetFish = feed;
						}
					}
				}
				pathList = PathFinder_.FindPath(new Vector2(m_backGround.GetTileGridX(transform.position.x), m_backGround.GetTileGridY(transform.position.y)), 
											new Vector2(targetFish.m_rowPos, targetFish.m_colPos), this);
				
				idx = 0;
				if(pathList != null && pathList.Count > 1){
					pathList.RemoveAt(0);
					dir.Set(m_backGround.GetTilePosX((int)pathList[idx].x) - transform.position.x, m_backGround.GetTilePosY((int)pathList[idx].y) - transform.position.y); 
					dir.Normalize();
					//Debug.Log(dir);
			
					state = State_.GO;
				}
				//Debug.Log();
			break;
			
			case State_.GO:
				if(idx < pathList.Count){
					nextPos.Set(transform.position.x + dir.x * Time.deltaTime * m_moveSpeed, transform.position.y + dir.y * Time.deltaTime * m_moveSpeed);
					bool endX = false;
					bool endY = false;
					if(dir.x >= 0){
						if(nextPos.x >= m_backGround.GetTilePosX((int)pathList[idx].x)){
							nextPos.x = m_backGround.GetTilePosX((int)pathList[idx].x);
							endX = true;
						}
					}
					else{
						if(nextPos.x <= m_backGround.GetTilePosX((int)pathList[idx].x)){
							nextPos.x = m_backGround.GetTilePosX((int)pathList[idx].x);
							endX = true;
						}
					}
					
					if(dir.y >= 0){
						if(nextPos.y >= m_backGround.GetTilePosY((int)pathList[idx].y)){
							nextPos.y = m_backGround.GetTilePosY((int)pathList[idx].y);
							endY = true;
						}
					}
					else{
						if(nextPos.y <= m_backGround.GetTilePosY((int)pathList[idx].y)){
							nextPos.y = m_backGround.GetTilePosY((int)pathList[idx].y);
							endY = true;
						}
					}
					
					if(endX && endY){
						int preIDX = idx;
						idx++;
						if(idx < pathList.Count){
							dir.Set(m_backGround.GetTilePosX((int)pathList[idx].x) - m_backGround.GetTilePosX((int)pathList[preIDX].x), 
									m_backGround.GetTilePosY((int)pathList[idx].y) - m_backGround.GetTilePosY((int)pathList[preIDX].y)); 
							dir.Normalize();
						}
					}
					
					transform.position = new Vector3(nextPos.x, nextPos.y, transform.position.z);
					//Debug.Log(idx.ToString() + ", " + dir.ToString());
					if(targetFish.state == FeedFish_.State_.DIE){
						state = State_.WAIT;
					}
				}
				else{
					if(targetFish.state == FeedFish_.State_.DIE){
						state = State_.WAIT;
					}
					else{
						state = State_.TRACEFEED;
					}
				}
			break;
			
			case State_.TRACEFEED:
				dir.Set(targetFish.transform.position.x - transform.position.x, targetFish.transform.position.y - transform.position.y);
				dir.Normalize();	
				nextPos.Set(transform.position.x + dir.x * Time.deltaTime * m_moveSpeed, transform.position.y + dir.y * Time.deltaTime * m_moveSpeed);
				transform.position = new Vector3(nextPos.x, nextPos.y, transform.position.z);
				if(targetFish.state == FeedFish_.State_.DIE){
					state = State_.WAIT;
				}	
			break;
		}
		Update_View(dir.x);
	}
	
	private void Update_View(float x){
		if(x > 0){
			m_sprite.FlipX = true;
		}
		else if(x < 0){
			m_sprite.FlipX = false;
		}
	}
	
	public bool IsWallCollision(int gridX, int gridY){
		float posX = m_backGround.GetTilePosX(gridX);
		float posY = m_backGround.GetTilePosY(gridY);
		//Debug.Log(posX.ToString() + ", " + posY.ToString());
		RaycastHit hit;
		bool retBool = false;
		
		int count = 0;
		float ySize = Mathf.Max(m_orinHalfSize.y * 1.1f, m_halfSize.y);//Mathf.Clamp(m_halfSize.y, m_orinHalfSize.y,  
		float xSize = Mathf.Max(m_orinHalfSize.x * 1.1f, m_halfSize.x);//Mathf.Clamp(m_halfSize.y, m_orinHalfSize.y,  
		if(Physics.Raycast(new Vector3(posX, posY, 0), -(transform.up), out hit, ySize)){
			if(hit.collider.gameObject.tag == "TERRAIN"){
				//retBool = true;
				count++;
				//return true;
			}
		}
		
		if(Physics.Raycast(new Vector3(posX, posY, 0), (transform.up), out hit, ySize)){
			if(hit.collider.gameObject.tag == "TERRAIN"){
				//retBool = true;
				//return true;
				count++;
			}
		}
		
		if(Physics.Raycast(new Vector3(posX, posY, 0), (transform.right), out hit, xSize)){
			if(hit.collider.gameObject.tag == "TERRAIN"){
				retBool = true;
				count++;
				//return true;
			}
		}
		
		if(Physics.Raycast(new Vector3(posX, posY, 0), -(transform.right), out hit, xSize)){
			if(hit.collider.gameObject.tag == "TERRAIN"){
				retBool = true;
				count++;
				//return true;
			}
		}
		
		if(count > 1)
			retBool = true;
		return retBool;
	}
	
	void OnTriggerEnter(Collider col) {
		if(enabled)
			Trigger_Collision(col);
    }
	
	private void Trigger_Collision(Collider col){
		if(col.tag == "FEED"){
			FeedFish_ feed = col.gameObject.GetComponent<FeedFish_>();
			FeedFish_.FeedType_ type = feed.FeedType;
			ChangeProperties(col, type);
			feed.Destroy();
		}
	}
	
	private void ChangeProperties(Collider col, FeedFish_.FeedType_ type){
		SphereCollider sphereCollider = collider as SphereCollider;
		float sign;
		if(m_sprite.scale.x >= 0)
			sign = 1;
		else
			sign = -1;
		
		float len = Mathf.Abs(m_sprite.scale.x);
		
		//Debug.Log(len);
		
		switch(type){
			case FeedFish_.FeedType_.SIZE_UP:
				if(len < 2.6f){
					sphereCollider.radius += incRadiusSize;
					m_sprite.scale = new Vector3(m_sprite.scale.x + sign * incScaleSize, m_sprite.scale.y + incScaleSize, m_sprite.scale.z);
					Update_Size();	
				}
			break;
			
			case FeedFish_.FeedType_.SIZE_DOWN:
				if(len > 0.4f){
					sphereCollider.radius -= incRadiusSize;
					m_sprite.scale = new Vector3(m_sprite.scale.x - sign * incScaleSize, m_sprite.scale.y - incScaleSize, m_sprite.scale.z);
					Update_Size();
				}
			break;
			
			case FeedFish_.FeedType_.SPEED_UP:
				m_moveSpeed += 50;
			break;
			
			case FeedFish_.FeedType_.SPEED_DOWN:
				m_moveSpeed -= 50;
			break;
		}
		
		len = Mathf.Abs(m_sprite.scale.x);
		
		m_moveSpeed = Mathf.Clamp(m_moveSpeed, 50, 500);
		m_infoText.SetEnemyInfo(m_moveSpeed, len);
		//Debug.Log(m_moveSpeed.ToString() + ", " + len.ToString());
	}
	
	private void Update_Size(){
		m_size.x = Mathf.Abs(m_sprite.GetUntrimmedBounds().size.x);
		m_size.y = Mathf.Abs(m_sprite.GetUntrimmedBounds().size.y);
		m_halfSize.x = m_size.x * 0.5f;
		m_halfSize.y = m_size.y * 0.5f;
	}
	
	public void Destroy(){
		enabled = false;
		transform.position = new Vector3(-100, -100, transform.position.z);
	}
}
