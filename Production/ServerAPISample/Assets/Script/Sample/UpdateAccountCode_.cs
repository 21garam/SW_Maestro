using UnityEngine;
using System.Collections;

public class UpdateAccountCode_ : MonoBehaviour {
	public tk2dUITextInput ID_TextInput;
	
	public tk2dUITextInput scoreTextInput;
	public tk2dUIToggleButton scoreCheck;
	
	public tk2dUITextInput softCashTextInput;
	public tk2dUIToggleButton softCachCheck;
	
	public tk2dUITextInput hardCashTextInput;
	public tk2dUIToggleButton hardCachCheck;
	
	public tk2dUITextInput equipmentTextInput;
	public tk2dUIToggleButton equipmentCheck;
	
	public tk2dUITextInput itemTextInput;
	public tk2dUIToggleButton itemCheck;
	
	public tk2dUITextInput passwordTextInput;
	public tk2dUIToggleButton passwordCheck;
	
	public tk2dUIItem acceptBtr; 
	
	public WWW_ www;
	public MessageBox_ prefabsMsgBox;
	MessageBox_ msgBox;
	
    void OnEnable() {
        acceptBtr.OnClick += UpdateAccount;
    }
	
	void OnDisable() {
        acceptBtr.OnClick -= UpdateAccount;
    }
	
	public void UpdateAccount(){
		int scoreParam = WWW_.INTEGER_NULL;
		if(scoreCheck.IsOn)
			scoreParam = Util_.ConvertStringtoInt(scoreTextInput.Text);
		
		int softCashParam = WWW_.INTEGER_NULL;
		if(softCachCheck.IsOn)
			softCashParam = Util_.ConvertStringtoInt(softCashTextInput.Text);
		
		int hardCashParam = WWW_.INTEGER_NULL;
		if(hardCachCheck.IsOn)
			hardCashParam = Util_.ConvertStringtoInt(hardCashTextInput.Text);
		
		int equipmentParam = WWW_.INTEGER_NULL;
		if(equipmentCheck.IsOn)
			equipmentParam = Util_.ConvertStringtoInt(equipmentTextInput.Text);
		
		int itemParam = WWW_.INTEGER_NULL;
		if(itemCheck.IsOn)
			itemParam = Util_.ConvertStringtoInt(itemTextInput.Text);
		
		string passwordParam = WWW_.STRING_NULL;
		if(passwordCheck.IsOn)
			passwordParam = passwordTextInput.Text;
		
		www.UpdateAccount(ID_TextInput.Text, UpdateAccountMessageBox, 
			scoreParam, softCashParam, hardCashParam, equipmentParam, itemParam, passwordParam);
		TextClear();
	}
	
	public void TextClear(){
		ID_TextInput.Text = "";
		scoreTextInput.Text = "";
		softCashTextInput.Text = "";
		hardCashTextInput.Text = "";
		equipmentTextInput.Text = "";
		itemTextInput.Text = "";
		passwordTextInput.Text ="";
	}
	
	public void UpdateAccountMessageBox(string msg){
		msgBox = GameObject.Instantiate(prefabsMsgBox) as MessageBox_; 
		if(msg == WWWMessage_.OK)
			msgBox.Initalize(this, "Account updated");
		else{
			msgBox.Initalize(this, WWWMessage_.FAIL_ID_DUP);
		}
	}
}
