using UnityEngine;
using System.Collections;

public class SignUpWindow_ : MonoBehaviour {
	public tk2dUITextInput ID_TextInput;
	public tk2dUITextInput PS_TextInput;
	
	public tk2dUIItem acceptBtr; 
	
	public WWW_ www;
	
    void OnEnable() {
        acceptBtr.OnClick += CreateAccount;
	}
	
	void OnDisable() {
        acceptBtr.OnClick -= CreateAccount;
	}
	
	public void CreateAccount(){
		www.CreateAccount(ID_TextInput.Text, PS_TextInput.Text, CreateAccountMessageBox);
		TextClear();
	}
	
	public void TextClear(){
		ID_TextInput.Text = "";
		PS_TextInput.Text = "";
	}
	
	public void CreateAccountMessageBox(string msg){
		if(msg == WWWMessage_.OK){
			
		}
		else{
		}
	}
}
