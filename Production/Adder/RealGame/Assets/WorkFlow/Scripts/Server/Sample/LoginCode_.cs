using UnityEngine;
using System.Collections;

public class LoginCode_ : MonoBehaviour {
	public tk2dUITextInput ID_TextInput;
	public tk2dUITextInput PS_TextInput;
	
	public tk2dUIItem acceptBtr; 
	
	public WWW_ www;
	//public MessageBox_ prefabsMsgBox;
	//MessageBox_ msgBox;
	
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
	
	public void Test(string msg){
	}
	
	public void LoginMessageBox(string msg){
		//msgBox = GameObject.Instantiate(prefabsMsgBox) as MessageBox_; 
		//if(msg == WWWMessage_.OK)
		//	msgBox.Initalize(this, "Login Succeed");
		//else{
		//	if(msg == WWWMessage_.FAIL_ID_NONE)
		//		msgBox.Initalize(this, WWWMessage_.FAIL_ID_NONE);
		//	else if(msg == WWWMessage_.FAIL_PS_WRONG)
		//		msgBox.Initalize(this, WWWMessage_.FAIL_PS_WRONG);
		//}
	}
}
