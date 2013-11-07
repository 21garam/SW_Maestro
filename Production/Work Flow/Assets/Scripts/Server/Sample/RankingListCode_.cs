using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RankingListCode_ : MonoBehaviour {
	public tk2dUITextInput ID_TextInput;
	
	public WWW_ www;
	public MessageBox_ prefabsMsgBox;
	MessageBox_ msgBox;
	
	public tk2dUIItem acceptBtr; 
	
	public tk2dUIScrollableArea list;
	
	public tk2dTextMesh textPrefabs;
	
	const int listCount = 10;
	
	List<tk2dTextMesh> textList = new List<tk2dTextMesh>(listCount);
	
	void Start(){
	}
	
	void OnEnable() {
        acceptBtr.OnClick += GetRankingList;
    }
	
	void OnDisable() {
        acceptBtr.OnClick -= GetRankingList;
    }
	
	public void GetRankingList(){
		TextListClear();
		if(ID_TextInput.Text != "")
			www.GetRankingList(listCount, ID_TextInput.Text, XMLParseToList);
		TextClear();
	}
	
	private void Test(string msg){
		Debug.Log(msg);
	}
	
	private void XMLParseToList(string xml){
		if(XMLParser_.RankingInfoXMLParse(xml)){
			tk2dTextMesh fistTextElement = GameObject.Instantiate(textPrefabs) as tk2dTextMesh;
			fistTextElement.transform.parent = list.contentContainer.transform;
			fistTextElement.transform.localPosition = new Vector3(0.05f, - 0.02f, 0);
			fistTextElement.text = XMLParser_.PlayerUserRankingInfo.ToString();
			fistTextElement.Commit();	
			textList.Add(fistTextElement);
			
			int listCount = XMLParser_.OtherUserRankingInfoList.Count;
			for(int i = 0; i < listCount; i++){
				tk2dTextMesh textElement = GameObject.Instantiate(textPrefabs) as tk2dTextMesh;
				textElement.transform.parent = list.contentContainer.transform;
				textElement.transform.localPosition = new Vector3(0.05f, -((i+1) * 0.1f) - 0.02f, 0);
				textElement.text = XMLParser_.OtherUserRankingInfoList[i].ToString();
				textElement.Commit();	
				textList.Add(textElement);
			}
		}
		else{
			UpdateAccountMessageBox(WWWMessage_.FAIL_ID_NONE);
		}
	}
	
	private void TextListClear(){
		if(textList.Count > 0){
			for(int i = 0; i < textList.Count; i++)
				GameObject.Destroy(textList[i].gameObject);
			textList.Clear();
		}
	}
	
	private void UpdateAccountMessageBox(string msg){
		msgBox = GameObject.Instantiate(prefabsMsgBox) as MessageBox_; 
		msgBox.Initalize(this, msg);
	}
	
	private void TextClear(){
	}
}
