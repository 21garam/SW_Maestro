using UnityEngine;
using System.Collections;


public class Camera_ : MonoBehaviour {
	public tk2dCamera m_cam;
	public Backgound_ m_backGround;
	public InfoText_ m_infoText;
	
	Vector2 m_centerPos = new Vector2();
	MyAABB_ m_bound = new MyAABB_();
	Vector2 m_boundSize = new Vector2();
	
	void Start () {
		float boundBorderRate = 0.3f; 
		// (0.0 ~ 0.5)
		boundBorderRate = Mathf.Max(0.0f, boundBorderRate);
		boundBorderRate = Mathf.Min(boundBorderRate, 0.5f);
		float boundW = 0.0f;
		float boundH = 0.0f;
		if(m_cam.nativeResolutionWidth > m_cam.nativeResolutionHeight){
			boundW = m_cam.nativeResolutionWidth * 0.5f - m_cam.nativeResolutionHeight * boundBorderRate;
			boundH = m_cam.nativeResolutionHeight * (0.5f - boundBorderRate);
		}else{
			boundH = m_cam.nativeResolutionHeight * 0.5f - m_cam.nativeResolutionWidth * boundBorderRate;
			boundW = m_cam.nativeResolutionWidth * (0.5f - boundBorderRate);
		}
		m_boundSize.Set(boundW, boundH);
		//m_cam.transform.position = new Vector3(m_cam.nativeResolutionWidth * 0.5f, m_cam.nativeResolutionHeight * 0.5f, m_cam.transform.position.z);
	}
	
	void Update () {
		m_centerPos.Set(m_cam.transform.position.x + m_cam.NativeResolution.x * 0.5f, m_cam.transform.position.y + m_cam.NativeResolution.y * 0.5f);
	}
	
	public MyAABB_ GetBound(){
		m_bound.Set(m_centerPos.x - m_boundSize.x, m_centerPos.x + m_boundSize.x, m_centerPos.y + m_boundSize.y, m_centerPos.y - m_boundSize.y);
		return m_bound;
	}
	
	public override string ToString() {
		return m_bound.ToString();
	}
	
	public float GetNativeResolutionWidth(){
		return m_cam.nativeResolutionWidth;
	}
	
	public float GetNativeResolutionHeight(){
		return m_cam.nativeResolutionHeight;
	}
	
	public void Move(float x, float y){
		m_cam.transform.position = new Vector3(m_cam.transform.position.x + x, m_cam.transform.position.y + y, m_cam.transform.position.z);
		Update_WorldBound();
		m_infoText.SetPlayerInfoPos(m_cam.transform.position);
	}
	
	private void Update_WorldBound(){
		float interDisX = 0;
		float interDisY = 0;
		
		MyAABB_ bound = m_backGround.GetBound();
		//Debug.Log(bound.ToString());
		
		float maxRightBound = bound.right - m_cam.nativeResolutionWidth; 
		float maxUpBound = bound.up - m_cam.nativeResolutionHeight; 
		
		float posX = m_cam.transform.position.x;
		float posY = m_cam.transform.position.y;
		
		if(posX < bound.left)
			interDisX = +bound.left - posX;
		else if(posX > maxRightBound)
			interDisX = +maxRightBound - posX;
		
		if(posY < bound.down)
			interDisY = bound.down - posY; 
		else if(posY > maxUpBound)
			interDisY = maxUpBound - posY;
		
		posX += interDisX;
		posY += interDisY;
		m_cam.transform.position = new Vector3(posX, posY, m_cam.transform.position.z);
	}
}
