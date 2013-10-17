using UnityEngine;
using System.Collections;

public class LoginCode_ : MonoBehaviour {
	public tk2dUITextInput ID_TextInput;
	public tk2dUITextInput PS_TextInput;
	
	public tk2dUIItem acceptBtr; 
	
	public WWW_ www;
	public MessageBox_ prefabsMsgBox;
	MessageBox_ msgBox;
	
	void OnEnable() {
        acceptBtr.OnClick += Login;
    }
	
	void OnDisable() {
        acceptBtr.OnClick -= Login;
    }
	
	public void Login(){
		www.Login(ID_TextInput.Text, PS_TextInput.Text, LoginMessageBox);
		TextClear();
	}
	
	public void TextClear(){
		ID_TextInput.Text = "";
		PS_TextInput.Text = "";
	}
	
	public void LoginMessageBox(string msg){
		msgBox = GameObject.Instantiate(prefabsMsgBox) as MessageBox_; 
		if(msg == WWWMessage_.LOGIN_FAIL)
			msgBox.Initalize(this, "Login Fail");
		else if(msg == WWWMessage_.LOGIN_OK)
			msgBox.Initalize(this, "Login OK");
	}
}
