using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RankingListCode_ : MonoBehaviour {
	public WWW_ www;
	//public MessageBox_ prefabsMsgBox;
	//MessageBox_ msgBox;
	//public TitleBackground_ titleBackgournd;
	public WaitBackground_ waitBackground;
	
	public tk2dUIScrollableArea list;
	
	public tk2dTextMesh textPrefabs;
	
	public GameObject listPrefabs;
	
	const int listCount = 10;
	
	GameObject playerMyC = null;
	public GameObject thisListMyc;
	
	List<GameObject> gamelist = new List<GameObject>(listCount);
	List<tk2dTextMesh> textList = new List<tk2dTextMesh>(listCount);
	
	void Start(){
		GetRankingList();
		waitBackground.Initialize();
	}
	
	public void Initialize(){
		//Debug.Log("RankingListCode_ : Initialize is called");
		GetRankingList();
		//SharedData.bodyId = ItemTestCode_.bodyID;
		//SharedData.eyesId = ItemTestCode_.eyeID;
		//SharedData.finId= ItemTestCode_.finID;
		//SharedData.mouthId = ItemTestCode_.mouthID;
	}
	
	private void GetRankingList(){
		GameListClear();
		www.GetRankingList(listCount, GUI_Setting_.PLAYER_ID, XMLParseToList);
	}
	
	private void Test(string msg){
		Debug.Log(msg);
	}
	
	public void SetPlayerCostume(){
		if(playerMyC != null){
			SetCostume(GUI_Setting_.PLAYER_ID , playerMyC);
		}
	}
	
	//GameObject MyC;
	void SetCostume(string id, GameObject myc){
		//MyC = myc;
		www.GetPlayerInfo(id, myc, SetEquipmentInfo);
	}
	
	void SetEquipmentInfo(GameObject myc, string xml){
		if(XMLParser_.PlayerInfoXMLParse(xml)){
			int equipment = XMLParser_.PlayerAccountInfo.equipment;
			//int body = Equipment_.GetBodyValue_FromEquipment(equipment);
			//int eye = Equipment_.GetEyeValue_FromEquipment(equipment);
			//int mouth = Equipment_.GetMouthValue_FromEquipment(equipment);
			//int fin = Equipment_.GetFinValue_FromEquipment(equipment);//GetFinVale_FromEquipment(equipment);
			//SetMyEquipmentC(myc, body, eye, mouth, fin);
			SetMyEquipmentC(myc, equipment);
		}
		else{
			Debug.Log("ID is not existed");
		}
	}
	
	void SetMyEquipmentC(GameObject MyC, int equipment){
		int body = Equipment_.GetBodyValue_FromEquipment(equipment);
		int eye = Equipment_.GetEyeValue_FromEquipment(equipment);
		int mouth = Equipment_.GetMouthValue_FromEquipment(equipment);
		int fin = Equipment_.GetFinValue_FromEquipment(equipment);//GetFinVale_FromEquipment(equipment);
		SetMyEquipmentC(MyC, body, eye, mouth, fin);
	}
	
	void SetMyEquipmentC(GameObject MyC, int body, int eye, int mouth, int fin){
		SetMyBody(MyC, body);
		SetMyeye(MyC, eye);
		SetMyfin(MyC, fin);
		SetMymouth(MyC, mouth);
	}
	
	void SetMyBody(GameObject MyC, int body){
		if(body == Equipment_.GetBodyValue_FromTag("Defualt")){
			MyC.transform.FindChild("body").GetComponent<tk2dSprite>().SetSprite("Body_1");
		}
		else if(body == Equipment_.GetBodyValue_FromTag("cat")){
			MyC.transform.FindChild("body").GetComponent<tk2dSprite>().SetSprite("Body_2");	
		}
		else if(body == Equipment_.GetBodyValue_FromTag("round")){
			MyC.transform.FindChild("body").GetComponent<tk2dSprite>().SetSprite("Body_3");	
		}
		else if(body == Equipment_.GetBodyValue_FromTag("sharp")){
			MyC.transform.FindChild("body").GetComponent<tk2dSprite>().SetSprite("Body_4");
		}
		else{
			MyC.transform.FindChild("body").GetComponent<tk2dSprite>().SetSprite("Body_1");	
			Debug.Log("Body Value : " + body.ToString());
		}
	}
	
	void SetMyeye(GameObject MyC, int eye){
		if(eye == Equipment_.GetEyeValue_FromTag("Defualt")){
			MyC.transform.FindChild("eye").GetComponent<tk2dSprite>().SetSprite("Eye_1");	
		}
		else if(eye == Equipment_.GetEyeValue_FromTag("cat")){
			MyC.transform.FindChild("eye").GetComponent<tk2dSprite>().SetSprite("Eye_2");	
		}
		else if(eye == Equipment_.GetEyeValue_FromTag("round")){
			MyC.transform.FindChild("eye").GetComponent<tk2dSprite>().SetSprite("Eye_3");	
		}
		else if(eye == Equipment_.GetEyeValue_FromTag("sharp")){
			MyC.transform.FindChild("eye").GetComponent<tk2dSprite>().SetSprite("Eye_4");
		}
		else{
			MyC.transform.FindChild("eye").GetComponent<tk2dSprite>().SetSprite("Eye_1");
			Debug.Log("eye Value : " + eye.ToString());
		}
	}
	
	void SetMymouth(GameObject MyC, int mouth){
		if(mouth == Equipment_.GetMouthValue_FromTag("Defualt")){
			MyC.transform.FindChild("mouth").GetComponent<tk2dSprite>().SetSprite("Mouth_1");	
		}
		else if(mouth == Equipment_.GetMouthValue_FromTag("cat")){
			MyC.transform.FindChild("mouth").GetComponent<tk2dSprite>().SetSprite("Mouth_2");
		}
		else if(mouth == Equipment_.GetMouthValue_FromTag("round")){
			MyC.transform.FindChild("mouth").GetComponent<tk2dSprite>().SetSprite("Mouth_3");
		}
		else if(mouth == Equipment_.GetMouthValue_FromTag("sharp")){
			MyC.transform.FindChild("mouth").GetComponent<tk2dSprite>().SetSprite("Mouth_4");
		}
		else{
			MyC.transform.FindChild("mouth").GetComponent<tk2dSprite>().SetSprite("Mouth_1");
			Debug.Log("mouth Value : " + mouth.ToString());
		}
	}
	
	void SetMyfin(GameObject MyC, int fin){
		if(fin == Equipment_.GetFinValue_FromTag("Defualt")){
			MyC.transform.FindChild("fin").GetComponent<tk2dSprite>().SetSprite("Fin_1");
		}
		else if(fin == Equipment_.GetFinValue_FromTag("cat")){
			MyC.transform.FindChild("fin").GetComponent<tk2dSprite>().SetSprite("Fin_2");	
		}
		else if(fin == Equipment_.GetFinValue_FromTag("round")){
			MyC.transform.FindChild("fin").GetComponent<tk2dSprite>().SetSprite("Fin_3");
		}
		else if(fin == Equipment_.GetFinValue_FromTag("sharp")){
			MyC.transform.FindChild("fin").GetComponent<tk2dSprite>().SetSprite("Fin_4");
		}
		else{
			MyC.transform.FindChild("fin").GetComponent<tk2dSprite>().SetSprite("Fin_1");
			Debug.Log("fin Value : " + fin.ToString());
		}
	}
	
	private void XMLParseToList(string xml){
		if(XMLParser_.RankingInfoXMLParse(xml)){
			int listCount = XMLParser_.OtherUserRankingInfoList.Count;
			
			/*
			tk2dTextMesh fistTextElement = GameObject.Instantiate(textPrefabs) as tk2dTextMesh;
			fistTextElement.transform.parent = list.contentContainer.transform;
			fistTextElement.transform.localPosition = new Vector3(0.05f, - 0.02f, 0);
			fistTextElement.text = XMLParser_.PlayerUserRankingInfo.ToString();
			fistTextElement.Commit();	
			textList.Add(fistTextElement);
			*/
			GameObject listElement = GameObject.Instantiate(listPrefabs) as GameObject;
			listElement.transform.parent = list.contentContainer.transform;
			listElement.transform.localPosition = new Vector3(-0.14f, -0.1f, 0);
			listElement.transform.localScale = new Vector3(1f, 1f, 1f);
			
			listElement.transform.FindChild("Text_rank").localPosition = new Vector3(0.6f, -0.03f, 0);
			listElement.transform.FindChild("Text_rank").GetComponent<tk2dTextMesh>().text = XMLParser_.PlayerUserRankingInfo.ranking.ToString();
			listElement.transform.FindChild("Text_rank").GetComponent<tk2dTextMesh>().Commit();
			
			listElement.transform.FindChild("Text_id").GetComponent<tk2dTextMesh>().text = XMLParser_.PlayerUserRankingInfo.id.ToString();
			listElement.transform.FindChild("Text_id").GetComponent<tk2dTextMesh>().Commit();
			
			listElement.transform.FindChild("Text_Score").GetComponent<tk2dTextMesh>().text = XMLParser_.PlayerUserRankingInfo.score.ToString();
			listElement.transform.FindChild("Text_Score").GetComponent<tk2dTextMesh>().Commit();
			
			playerMyC = listElement.transform.FindChild("MyC").gameObject;
			SetCostume(XMLParser_.PlayerUserRankingInfo.id, playerMyC);
			if(thisListMyc != null)
				SetCostume(XMLParser_.PlayerUserRankingInfo.id, thisListMyc);
			//listElement.transform.FindChild("MyC").GetComponent<GameObject>().transform.FindChild("");
			
			gamelist.Add(listElement);
			
			for(int i = 0;i< listCount; i++)
			{
				listElement = GameObject.Instantiate(listPrefabs) as GameObject;
				listElement.transform.parent = list.contentContainer.transform;
				listElement.transform.localPosition = new Vector3(-0.14f, -(i * 0.35f)-0.45f, 0);
				listElement.transform.localScale = new Vector3(1f, 1f, 1f);
				
				listElement.transform.FindChild("Text_rank").localPosition = new Vector3(0.6f, -0.03f, 0);
				listElement.transform.FindChild("Text_rank").GetComponent<tk2dTextMesh>().text = XMLParser_.OtherUserRankingInfoList[i].ranking.ToString();
				listElement.transform.FindChild("Text_rank").GetComponent<tk2dTextMesh>().Commit();
				
				listElement.transform.FindChild("Text_id").GetComponent<tk2dTextMesh>().text = XMLParser_.OtherUserRankingInfoList[i].id.ToString();
				listElement.transform.FindChild("Text_id").GetComponent<tk2dTextMesh>().Commit();
				
				listElement.transform.FindChild("Text_Score").GetComponent<tk2dTextMesh>().text = XMLParser_.OtherUserRankingInfoList[i].score.ToString();
				listElement.transform.FindChild("Text_Score").GetComponent<tk2dTextMesh>().Commit();
				
				SetCostume(XMLParser_.OtherUserRankingInfoList[i].id, listElement.transform.FindChild("MyC").gameObject);
			
				gamelist.Add(listElement);
			}
			Debug.Log("hi");
		}
		else{
			UpdateAccountMessageBox(WWWMessage_.FAIL_ID_NONE);
		}
	}
	
	private void GameListClear(){
		if(gamelist.Count > 0){
			for(int i = 0; i<textList.Count; i++)
				GameObject.Destroy(gamelist[i].gameObject);
			gamelist.Clear();
		}
	}
	
	private void UpdateAccountMessageBox(string msg){
		//msgBox = GameObject.Instantiate(prefabsMsgBox) as MessageBox_; 
		//msgBox.Initalize(this, msg);
	}
}
