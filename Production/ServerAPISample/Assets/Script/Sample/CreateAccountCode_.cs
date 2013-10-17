using UnityEngine;
using System.Collections;

public class CreateAccountCode_ : MonoBehaviour{
	public tk2dUITextInput ID_TextInput;
	public tk2dUITextInput PS_TextInput;
	
	public tk2dUIItem acceptBtr; 
	public tk2dUIItem autoBtr;
	
	public WWW_ www;
	public MessageBox_ prefabsMsgBox;
	MessageBox_ msgBox;
	
    void OnEnable() {
        acceptBtr.OnClick += CreateAccount;
		autoBtr.OnClick += AutoCreateAccount;
    }
	
	void OnDisable() {
        acceptBtr.OnClick -= CreateAccount;
		autoBtr.OnClick -= AutoCreateAccount;
    }
	
	public void CreateAccount(){
		www.CreateAccount(ID_TextInput.Text, PS_TextInput.Text, CreateAccountMessageBox);
		TextClear();
	}
	
	public void AutoCreateAccount(){
		int score;
		string id;
		for(int i = 0; i < 19; i++){
			score = Random.Range(100, 1000);
			id = Random.Range(0, int.MaxValue).ToString();
			www.CreateAccount(id, "1111", null, score);
		}
		score = Random.Range(100, 1000);
		id = Random.Range(0, int.MaxValue).ToString();
		www.CreateAccount(id, "1111", CreateAccountMessageBox, score);
	}
	
	public void TextClear(){
		ID_TextInput.Text = "";
		PS_TextInput.Text = "";
	}
	
	public void CreateAccountMessageBox(string msg){
		msgBox = GameObject.Instantiate(prefabsMsgBox) as MessageBox_; 
		if(msg == WWWMessage_.ACCOUNT_CREATE_FAIL)
			msgBox.Initalize(this, WWWMessage_.ACCOUNT_CREATE_FAIL);
		else if(msg == WWWMessage_.ACCOUNT_CREATE_OK)
			msgBox.Initalize(this, WWWMessage_.ACCOUNT_CREATE_OK);
	}
}
