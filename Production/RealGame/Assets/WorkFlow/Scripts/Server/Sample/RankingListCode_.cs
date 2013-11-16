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
	
	List<GameObject> gamelist = new List<GameObject>(listCount);
	List<tk2dTextMesh> textList = new List<tk2dTextMesh>(listCount);
	
	void Start(){
		GetRankingList();
		waitBackground.Initialize();
	}
	
	public void GetRankingList(){
		GameListClear();
		www.GetRankingList(listCount, GUI_Setting_.PLAYER_ID, XMLParseToList);
	}
	
	private void Test(string msg){
		Debug.Log(msg);
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
			
			for(int i = 0;i< listCount; i++)
			{
				GameObject listElement = GameObject.Instantiate(listPrefabs) as GameObject;
				listElement.transform.parent = list.contentContainer.transform;
				listElement.transform.localPosition = new Vector3(-0.14f, -(i * 0.35f)-0.1f, 0);
				listElement.transform.localScale = new Vector3(1f, 1f, 1f);
				
				listElement.transform.FindChild("Text_rank").localPosition = new Vector3(0.6f, -0.03f, 0);
				listElement.transform.FindChild("Text_rank").GetComponent<tk2dTextMesh>().text = XMLParser_.OtherUserRankingInfoList[i].ranking.ToString();
				listElement.transform.FindChild("Text_rank").GetComponent<tk2dTextMesh>().Commit();
				
				listElement.transform.FindChild("Text_Score").localPosition = new Vector3(1.94f, -0.03f, 0);
				listElement.transform.FindChild("Text_Score").GetComponent<tk2dTextMesh>().text = XMLParser_.OtherUserRankingInfoList[i].score.ToString();
				listElement.transform.FindChild("Text_Score").GetComponent<tk2dTextMesh>().Commit();
				
				gamelist.Add(listElement);
			}
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
