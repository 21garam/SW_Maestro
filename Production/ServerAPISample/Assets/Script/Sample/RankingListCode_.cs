using UnityEngine;
using System.Collections;

public class RankingListCode_ : MonoBehaviour {
	public tk2dUITextInput ID_TextInput;
	
	public WWW_ www;
	public MessageBox_ prefabsMsgBox;
	MessageBox_ msgBox;
	
	public tk2dUIItem acceptBtr; 
	
	void OnEnable() {
        acceptBtr.OnClick += GetRankingList;
    }
	
	void OnDisable() {
        acceptBtr.OnClick -= GetRankingList;
    }
	
	public void GetRankingList(){
		www.GetRankingList(10, XMLParseToList);
		TextClear();
	}
	
	public void XMLParseToList(string xml){
		//XMLParser_.Parse(xml);
	}
	
	public void TextClear(){
	}
	
	public void LoginMessageBox(string msg){
		msgBox = GameObject.Instantiate(prefabsMsgBox) as MessageBox_; 
		if(msg == WWWMessage_.LOGIN_FAIL)
			msgBox.Initalize(this, "Login Fail");
		else if(msg == WWWMessage_.LOGIN_OK)
			msgBox.Initalize(this, "Login OK");
	}
}
