using UnityEngine;
using System.Collections;

// Enemy A*
// Game Scene State

public class Player_ : MonoBehaviour {
	public tk2dSprite m_sprite;
	public Backgound_ m_backGround;
	public Camera_ m_cam;
	public InfoText_ m_infoText;
	
	Vector2 m_nextPos;
	public float m_moveSpeed = 250.0f;
	public Vector2 m_size;
	Vector2 m_halfSize;
	float m_aspectRate;
	float incRadiusSize;// = sphereCollider.radius * 0.1f;
	float incScaleSize;// = 
		
	void Start () {
		Start_ThisPosition();
		Start_CamPosition();
		m_size.x = m_sprite.GetUntrimmedBounds().size.x;
		m_size.y = m_sprite.GetUntrimmedBounds().size.y;
		m_halfSize.x = m_size.x * 0.5f;
		m_halfSize.y = m_size.y * 0.5f;
		m_infoText.SetPlayerInfo(m_moveSpeed, m_sprite.scale.x);
		
		SphereCollider sphereCollider = collider as SphereCollider;
		incRadiusSize = sphereCollider.radius * 0.1f;
		incScaleSize = m_sprite.scale.x * 0.1f;
	}
	
	void Start_ThisPosition(){
		transform.position = new Vector3(m_backGround.GetBound().right - m_halfSize.x,//m_cam.GetNativeResolutionWidth() * 0.5f, 
										 m_backGround.GetBound().up    - m_halfSize.y,//m_cam.GetNativeResolutionHeight() * 0.5f, 
										 transform.position.z);
	}
	
	void Start_CamPosition(){
		m_cam.transform.position = new Vector3(transform.position.x - m_cam.GetNativeResolutionWidth() * 0.5f, 
											   transform.position.y - m_cam.GetNativeResolutionHeight() * 0.5f, 
											   m_cam.transform.position.z);
	}
	
	void Update () {
		Update_Input();
		Update_BoundaryWorld();
		Update_CameraPos();
		Update_CollisionTerrain();
		Update_ThisPos();
	}
	
	void Update_CollisionTerrain(){
		RaycastHit hit;
		float xInterLen = 0;
		float yInterLen = 0;
		
		if(Physics.Raycast(new Vector3(m_nextPos.x, m_nextPos.y + m_halfSize.y, 0), -(transform.up), out hit, m_size.y)){
			if(hit.collider.gameObject.tag == "TERRAIN"){
				yInterLen = +(m_size.y - hit.distance);
				//Debug.Log("aa");
				m_nextPos.Set(m_nextPos.x, m_nextPos.y + yInterLen);
			}
		}
		
		if(Physics.Raycast(new Vector3(m_nextPos.x, m_nextPos.y - m_halfSize.y, 0), (transform.up), out hit, m_size.y)){
			if(hit.collider.gameObject.tag == "TERRAIN"){
				yInterLen = -(m_size.y - hit.distance);
				//Debug.Log("aa");
				m_nextPos.Set(m_nextPos.x, m_nextPos.y + yInterLen);
			}
		}
		
		if(Physics.Raycast(new Vector3(m_nextPos.x + m_halfSize.x, m_nextPos.y, 0), -(transform.right), out hit, m_size.x)){
			if(hit.collider.gameObject.tag == "TERRAIN"){
				xInterLen = +(m_size.x - hit.distance);
				//Debug.Log("aa");
				m_nextPos.Set(m_nextPos.x + xInterLen, m_nextPos.y);
			}
		}
		
		if(Physics.Raycast(new Vector3(m_nextPos.x - m_halfSize.x, m_nextPos.y, 0), (transform.right), out hit, m_size.x)){
			if(hit.collider.gameObject.tag == "TERRAIN"){
				xInterLen = -(m_size.x - hit.distance);
				//Debug.Log("aa");
				m_nextPos.Set(m_nextPos.x + xInterLen, m_nextPos.y);
			}
		}
	}
	
	private void Update_BoundaryWorld(){
		float interDisX = 0;
		float interDisY = 0;
		
		MyAABB_ bound = m_backGround.GetBound();
		if(m_nextPos.x < bound.left + m_halfSize.x)
			interDisX = +(bound.left + m_halfSize.x) - m_nextPos.x;
		else if(m_nextPos.x > bound.right - m_halfSize.x)
			interDisX = +(bound.right - m_halfSize.x) - m_nextPos.x;
		
		if(m_nextPos.y < bound.down + m_halfSize.y)
			interDisY = (bound.down + m_halfSize.y) - m_nextPos.y; 
		else if(m_nextPos.y > bound.up - m_halfSize.y)
			interDisY = (bound.up - m_halfSize.y) - m_nextPos.y;
		
		m_nextPos.x += interDisX;
		m_nextPos.y += interDisY;
	}
	
	private void Update_Input(){
		float x = Input.GetAxis("Horizontal") * Time.deltaTime * m_moveSpeed;
		float y = Input.GetAxis("Vertical") * Time.deltaTime * m_moveSpeed;
		Update_View(x);
		m_nextPos.Set(transform.position.x + x, transform.position.y + y);	
	}
	
	private void Update_View(float x){
		if(x > 0){
			m_sprite.FlipX = true;
		}
		else if(x < 0){
			m_sprite.FlipX = false;
		}
	}
	
	private void Update_CameraPos(){
		float interDisX = 0;
		float interDisY = 0;
		
		MyAABB_ bound = m_cam.GetBound();
		
		if(m_nextPos.x < bound.left)
			interDisX = -bound.left + m_nextPos.x;
		else if(m_nextPos.x > bound.right)
			interDisX = -bound.right + m_nextPos.x;
		
		if(m_nextPos.y < bound.down)
			interDisY = -bound.down + m_nextPos.y; 
		else if(m_nextPos.y > bound.up)
			interDisY = -bound.up + m_nextPos.y;
		
		m_cam.Move(interDisX, interDisY);
	}
	
	private void Update_ThisPos(){
		transform.position = new Vector3(m_nextPos.x, m_nextPos.y, transform.position.z);
	}
	
	void OnTriggerEnter(Collider col) {
		if(enabled)
			Trigger_CollisionEnemy(col);
    }
	
	private void Trigger_CollisionEnemy(Collider col){
		if(col.tag == "FEED"){
			FeedFish_ feed = col.gameObject.GetComponent<FeedFish_>();
			FeedFish_.FeedType_ type = feed.FeedType;
			ChangeProperties(col, type);
			feed.Destroy();
		}
		
		if(col.tag == "ENEMY"){
			Enemy_ enemy = col.gameObject.GetComponent<Enemy_>();
			float subSize = m_size.x - enemy.m_size.x;
			if(subSize > 0.01){
				enemy.Destroy();
				Application.LoadLevel("Victory");
			}
			else if(subSize < -0.01){
				Destroy();
				Application.LoadLevel("GameOver");
			}
		}
	}
	
	private void ChangeProperties(Collider col, FeedFish_.FeedType_ type){
		SphereCollider sphereCollider = collider as SphereCollider;
		int sign;
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
					float scaleX = m_sprite.scale.x - sign * incScaleSize;
					m_sprite.scale = new Vector3(scaleX, m_sprite.scale.y - incScaleSize, m_sprite.scale.z);
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
		m_infoText.SetPlayerInfo(m_moveSpeed, len);
	}
	
	private void Update_Size(){
		m_size.x = Mathf.Abs(m_sprite.GetUntrimmedBounds().size.x);
		m_size.y = Mathf.Abs(m_sprite.GetUntrimmedBounds().size.y);
		m_halfSize.x = m_size.x * 0.5f;
		m_halfSize.y = m_size.y * 0.5f;
	}
	
	void Destroy(){
		enabled = false;
		transform.position = new Vector3(-100, -100, transform.position.z);
	}
}
