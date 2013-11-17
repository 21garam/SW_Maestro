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
	
	public tk2dUITextInput itemTextInput;
	public tk2dUIToggleButton itemCheck;
	
	public tk2dUITextInput passwordTextInput;
	public tk2dUIToggleButton passwordCheck;
	
	public tk2dUITextInput hp_lvTextInput;
	public tk2dUIToggleButton hp_lvCheck;
	
	public tk2dUITextInput speed_lvTextInput;
	public tk2dUIToggleButton speed_lvCheck;
	
	public tk2dUITextInput fever_lvTextInput;
	public tk2dUIToggleButton fever_lvCheck;
	
	public tk2dUITextInput nick_nameTextInput;
	public tk2dUIToggleButton nick_nameCheck;
	
	public tk2dUITextInput exp_lvTextInput;
	public tk2dUIToggleButton exp_lvCheck;
	
	public tk2dUITextInput exp_subTextInput;
	public tk2dUIToggleButton exp_subCheck;
	
	public tk2dUIItem acceptBtr; 
	public tk2dUIItem searchBtr;
	
	public WWW_ www;
	//public MessageBox_ prefabsMsgBox;
	//MessageBox_ msgBox;
	
    void OnEnable() {
        acceptBtr.OnClick += UpdateAccount;
		searchBtr.OnClick += Search;
    }
	
	void OnDisable() {
        acceptBtr.OnClick -= UpdateAccount;
    	searchBtr.OnClick -= Search;
    }
	
	public void Search(){
		TextClear();
		if(ID_TextInput.Text != "")
			www.GetPlayerInfo(ID_TextInput.Text, SetPlayerInfo);
	}
	
	void SetPlayerInfo(string xml){
		if(XMLParser_.PlayerInfoXMLParse(xml)){
			//Debug.Log(xml);
			scoreTextInput.Text = XMLParser_.PlayerAccountInfo.score.ToString();
			softCashTextInput.Text = XMLParser_.PlayerAccountInfo.soft_cash.ToString();
			hardCashTextInput.Text = XMLParser_.PlayerAccountInfo.hard_cash.ToString();
			itemTextInput.Text = XMLParser_.PlayerAccountInfo.item.ToString();
			passwordTextInput.Text = XMLParser_.PlayerAccountInfo.password;
			hp_lvTextInput.Text = XMLParser_.PlayerAccountInfo.hp_lv.ToString();
			speed_lvTextInput.Text = XMLParser_.PlayerAccountInfo.speed_lv.ToString();
			fever_lvTextInput.Text = XMLParser_.PlayerAccountInfo.fever_lv.ToString();
			string nick_name = XMLParser_.PlayerAccountInfo.nick_name;
			if(nick_name == "")
				nick_nameTextInput.Text = "NULL";
			else
				nick_nameTextInput.Text = nick_name;
			exp_lvTextInput.Text = XMLParser_.PlayerAccountInfo.exp_lv.ToString();
			exp_subTextInput.Text = XMLParser_.PlayerAccountInfo.exp_sub.ToString();
		}
		else
			MessageBox("ID is not existed");
	}
	
	public void UpdateAccount(){
		int score = WWW_.INTEGER_NULL;
		if(scoreCheck.IsOn)
			score = Util_.ConvertStringtoInt(scoreTextInput.Text);
		
		int soft_cash = WWW_.INTEGER_NULL;
		if(softCachCheck.IsOn)
			soft_cash = Util_.ConvertStringtoInt(softCashTextInput.Text);
		
		int hard_cash = WWW_.INTEGER_NULL;
		if(hardCachCheck.IsOn)
			hard_cash = Util_.ConvertStringtoInt(hardCashTextInput.Text);
		
		int item = WWW_.INTEGER_NULL;
		if(itemCheck.IsOn)
			item = Util_.ConvertStringtoInt(itemTextInput.Text);
		
		string password = WWW_.STRING_NULL;
		if(passwordCheck.IsOn)
			password = passwordTextInput.Text;
		
		int hp_lv = WWW_.INTEGER_NULL;
		if(hp_lvCheck.IsOn)
			hp_lv = Util_.ConvertStringtoInt(hp_lvTextInput.Text);
		
		int speed_lv = WWW_.INTEGER_NULL;
		if(speed_lvCheck.IsOn)
			speed_lv = Util_.ConvertStringtoInt(speed_lvTextInput.Text);
		
		int fever_lv = WWW_.INTEGER_NULL;
		if(fever_lvCheck.IsOn)
			fever_lv = Util_.ConvertStringtoInt(fever_lvTextInput.Text);
		
		string nick_name = WWW_.STRING_NULL;
		if(nick_nameCheck.IsOn)
			nick_name = nick_nameTextInput.Text;
		
		int exp_lv = WWW_.INTEGER_NULL;
		if(exp_lvCheck.IsOn)
			exp_lv = Util_.ConvertStringtoInt(exp_lvTextInput.Text);
		
		int exp_sub = WWW_.INTEGER_NULL;
		if(exp_subCheck.IsOn)
			exp_sub = Util_.ConvertStringtoInt(exp_subTextInput.Text);
		
		www.UpdateAccount(ID_TextInput.Text, UpdateAccountMessageBox, score, soft_cash, hard_cash, WWW_.INTEGER_NULL, item, 
			WWW_.INTEGER_NULL, WWW_.INTEGER_NULL, WWW_.INTEGER_NULL, WWW_.INTEGER_NULL, hp_lv, speed_lv, fever_lv, nick_name, exp_lv, exp_sub, password);
		
		//www.UpdateAccount_Equipment( ... )
		//www.UpdateAccount_Item( ... )
		//www.UpdateAccount_Ability( ... )
		//www.UpdateAccount_EXP( ... )
		//www.UpdateAccount_NickName( ... )
		TextClear();
	}
	
	public void TextClear(){
		scoreTextInput.Text = "";
		softCashTextInput.Text = "";
		hardCashTextInput.Text = "";
		itemTextInput.Text = "";
		passwordTextInput.Text ="";
		hp_lvTextInput.Text ="";
		speed_lvTextInput.Text = "";
		fever_lvTextInput.Text = "";
		nick_nameTextInput.Text = "";
		exp_lvTextInput.Text = "";
		exp_subTextInput.Text = "";
	}
	
	public void UpdateAccountMessageBox(string msg){
		if(msg == WWWMessage_.OK)
			MessageBox("Account updated");
		else
			MessageBox(WWWMessage_.FAIL_ID_DUP);
	}
	
	private void MessageBox(string msg){
		//msgBox = GameObject.Instantiate(prefabsMsgBox) as MessageBox_; 
		//msgBox.Initalize(this, msg);
	}
}
