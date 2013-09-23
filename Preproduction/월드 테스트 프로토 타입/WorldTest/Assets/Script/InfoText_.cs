using UnityEngine;
using System.Collections;

public class InfoText_ : MonoBehaviour {
	public tk2dTextMesh m_textPrefab;
	//public Backgound_ m_backGround;
	public Camera_ m_cam;
	//public Player_ m_player;
	
	tk2dTextMesh m_textPlayerInfo;
	tk2dTextMesh m_textEnemyInfo;
	
	string playerInfoHeadStr = "<Player>\n";
	string enemyInfoHeadStr = "<Enemy>\n";
	// Use this for initialization
	void Awake(){
		m_textPlayerInfo = GameObject.Instantiate(m_textPrefab, transform.position, new Quaternion()) as tk2dTextMesh;
		m_textEnemyInfo = GameObject.Instantiate(m_textPrefab, transform.position, new Quaternion()) as tk2dTextMesh;
	}
	
	void Start () {
		//m_textEnemyInfo.text = "<Enemy>\n";
		//m_textEnemyInfo.Commit();
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void SetPlayerInfoPos(Vector3 pos){
		m_textPlayerInfo.transform.position = new Vector3(pos.x + m_cam.GetNativeResolutionWidth() * 0.9f, 
														  pos.y + m_cam.GetNativeResolutionHeight() * 0.9f, 
														  m_textPlayerInfo.transform.position.z);
		m_textEnemyInfo.transform.position = new Vector3(pos.x + m_cam.GetNativeResolutionWidth() * 0.8f, 
														  pos.y + m_cam.GetNativeResolutionHeight() * 0.9f, 
														  m_textPlayerInfo.transform.position.z);
	}
	
	public void SetPlayerInfo(float pMoveSpeed, float pSize){
		string infoStr = playerInfoHeadStr;
		string moveSpeedStr;
		if(pMoveSpeed >= 500)
			moveSpeedStr = "MAX";
		else if(pMoveSpeed <= 50)
			moveSpeedStr = "MIN";
		else
			moveSpeedStr = pMoveSpeed.ToString();
		
		string sizeStr;
		if(pSize >= 2.6)
			sizeStr = "MAX";
		else if(pSize <= 0.4f)
			sizeStr = "MIN";
		else
			sizeStr = pSize.ToString();
		
		infoStr += "Speed : " + moveSpeedStr + "\n";
		infoStr += "Size : " + sizeStr + "\n";
		m_textPlayerInfo.text = infoStr;
		m_textPlayerInfo.Commit();
	}
	
	public void SetEnemyInfo(float pMoveSpeed, float pSize){
		string infoStr = enemyInfoHeadStr;
		string moveSpeedStr;
		if(pMoveSpeed > 500)
			moveSpeedStr = "MAX";
		else if(pMoveSpeed < 50)
			moveSpeedStr = "MIN";
		else
			moveSpeedStr = pMoveSpeed.ToString();
		
		string sizeStr;
		if(pSize > 2.6)
			sizeStr = "MAX";
		else if(pSize < 0.4f)
			sizeStr = "MIN";
		else
			sizeStr = pSize.ToString();
		
		infoStr += "Speed : " + moveSpeedStr + "\n";
		infoStr += "Size : " + sizeStr + "\n";
		m_textEnemyInfo.text = infoStr;
		m_textEnemyInfo.Commit();
	}
}
