using UnityEngine;
using System.Collections;

public class Player_ : MonoBehaviour {
	public tk2dSprite m_sprite;
	public Backgound_ m_backGround;
	public Camera_ m_cam;
	Vector2 m_nextPos;
	float m_moveSpeed = 250.0f;
	float m_halfSize;
	float m_aspectRate;
	
	void Start () {
		//transform.position = new Vector3(m_cam.GetNativeResolutionWidth() * 0.5f, m_cam.GetNativeResolutionHeight() * 0.5f, transform.position.z);
		transform.position = new Vector3(m_backGround.GetBound().right * 0.5f, m_backGround.GetBound().up * 0.5f, transform.position.z);
		m_halfSize = m_sprite.GetUntrimmedBounds().size.x * 0.5f;
		SphereCollider sphereCollider = collider as SphereCollider;
		Debug.Log(sphereCollider.radius);
		Debug.Log(m_halfSize);
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(m_cam.GetPlayerBound());
		Update_Input();
		Update_WorldBound();
		Update_CameraTrace();
		Update_Collision();
		Update_BindPos();
	}
	
	void Update_Collision(){
		RaycastHit hit;
		float xInterLen = 0;
		float yInterLen = 0;
		
		if(Physics.Raycast(new Vector3(m_nextPos.x, m_nextPos.y, 0), -(transform.up), out hit, m_halfSize)){
			if(hit.collider.gameObject.tag == "TERRAIN"){
				yInterLen = +(m_halfSize - hit.distance);
				m_nextPos.Set(m_nextPos.x, m_nextPos.y + yInterLen);
			}
		}
		
		if(Physics.Raycast(new Vector3(m_nextPos.x, m_nextPos.y, 0), (transform.up), out hit, m_halfSize)){
			if(hit.collider.gameObject.tag == "TERRAIN"){
				yInterLen = -(m_halfSize - hit.distance);
				m_nextPos.Set(m_nextPos.x, m_nextPos.y + yInterLen);
			}
		}
		
		if(Physics.Raycast(new Vector3(m_nextPos.x, m_nextPos.y, 0), -(transform.right), out hit, m_halfSize)){
			if(hit.collider.gameObject.tag == "TERRAIN"){
				xInterLen = +(m_halfSize - hit.distance);
				m_nextPos.Set(m_nextPos.x + xInterLen, m_nextPos.y);
			}
		}
		
		if(Physics.Raycast(new Vector3(m_nextPos.x, m_nextPos.y, 0), (transform.right), out hit, m_halfSize)){
			if(hit.collider.gameObject.tag == "TERRAIN"){
				xInterLen = -(m_halfSize - hit.distance);
				m_nextPos.Set(m_nextPos.x + xInterLen, m_nextPos.y);
			}
		}
		
		if(Physics.Raycast(new Vector3(m_nextPos.x, m_nextPos.y, 0), new Vector3(1,1,0), out hit, m_halfSize)){
			if(hit.collider.gameObject.tag == "TERRAIN"){
				yInterLen = -((m_halfSize - hit.distance) * 1.4142f);
				xInterLen = -((m_halfSize - hit.distance) * 1.4142f);
				m_nextPos.Set(m_nextPos.x + xInterLen, m_nextPos.y + yInterLen);
			}
		}
		
		if(Physics.Raycast(new Vector3(m_nextPos.x, m_nextPos.y, 0), new Vector3(-1,1,0), out hit, m_halfSize)){
			if(hit.collider.gameObject.tag == "TERRAIN"){
				yInterLen = -((m_halfSize - hit.distance) * 1.4142f);
				xInterLen = +((m_halfSize - hit.distance) * 1.4142f);
				m_nextPos.Set(m_nextPos.x + xInterLen, m_nextPos.y + yInterLen);
			}
		}
		
		if(Physics.Raycast(new Vector3(m_nextPos.x, m_nextPos.y, 0), new Vector3(-1,-1,0), out hit, m_halfSize)){
			if(hit.collider.gameObject.tag == "TERRAIN"){
				yInterLen = +((m_halfSize - hit.distance) * 1.4142f);
				xInterLen = +((m_halfSize - hit.distance) * 1.4142f);
				m_nextPos.Set(m_nextPos.x + xInterLen, m_nextPos.y + yInterLen);
			}
		}
		
		if(Physics.Raycast(new Vector3(m_nextPos.x, m_nextPos.y, 0), new Vector3(1,-1,0), out hit, m_halfSize)){
			if(hit.collider.gameObject.tag == "TERRAIN"){
				yInterLen = +((m_halfSize - hit.distance) * 1.4142f);
				xInterLen = -((m_halfSize - hit.distance) * 1.4142f);
				m_nextPos.Set(m_nextPos.x + xInterLen, m_nextPos.y + yInterLen);
			}
		}
	}
	
	private void Update_WorldBound(){
		float interDisX = 0;
		float interDisY = 0;
		
		MyDir4_ bound = m_backGround.GetBound();
		//float spriteHalfWidth = m_sprite.GetBounds().size.x * 0.5f;
		//float spriteHalfHeight = m_sprite.GetBounds().size.y * 0.5f;
		if(m_nextPos.x < bound.left + m_halfSize)
			interDisX = +(bound.left + m_halfSize) - m_nextPos.x;
		else if(m_nextPos.x > bound.right + m_halfSize)
			interDisX = +(bound.right + m_halfSize) - m_nextPos.x;
		
		if(m_nextPos.y < bound.down + m_halfSize)
			interDisY = (bound.down + m_halfSize) - m_nextPos.y; 
		else if(m_nextPos.y > bound.up - m_halfSize)
			interDisY = (bound.up - m_halfSize) - m_nextPos.y;
		
		m_nextPos.x += interDisX;
		m_nextPos.y += interDisY;
	}
	
	private void Update_Input(){
		float x = Input.GetAxis("Horizontal") * Time.deltaTime * m_moveSpeed;
		float y = Input.GetAxis("Vertical") * Time.deltaTime * m_moveSpeed;
		//Debug.Log(x +", " + y);
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
	
	private void Update_CameraTrace(){
		float interDisX = 0;
		float interDisY = 0;
		
		MyDir4_ bound = m_cam.GetBound();
		
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
	
	private void Update_BindPos(){
		//Debug.Log(m_nextPos);
		transform.position = new Vector3(m_nextPos.x, m_nextPos.y, transform.position.z);
	}
	
	float m_incRate = 0.1F;
	void OnTriggerEnter(Collider col) {
    	if(col.tag == "ENEMY"){
			if(m_halfSize > 75){
				m_halfSize = 75f;
				return;
			}
			SphereCollider sphereCollider = collider as SphereCollider;
			sphereCollider.radius = sphereCollider.radius * (1 + (m_incRate / 30));
			transform.localScale = new Vector3(transform.localScale.x * (1 + (m_incRate)), 
				                               transform.localScale.y * (1 + (m_incRate)), 
											   transform.localScale.z);
			m_halfSize = m_halfSize *  (1 + m_incRate);
			Destroy(col.gameObject);
		}
	}
}
