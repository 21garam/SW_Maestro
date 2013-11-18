using UnityEngine;
using System.Collections;

public class SignUpWindow_ : MonoBehaviour {
	public tk2dUITextInput ID_TextInput;
	public tk2dUITextInput PS_TextInput;
	
	public tk2dUIItem acceptBtr; 
	
	public WWW_ www;
	
	public GameObject bar;
	//public TitleBackground_ titleBackground;
	//public WaitBackground_ waitBackground;
	
	public MessageBox_ msgBoxPrefabs;
    Transformer_ transformer;
	//private MessageBox_ msgBox;
	
	void OnEnable() {
        transformer = GetComponent<Transformer_>();
		acceptBtr.OnClick += CreateAccount;
	}
	
	void OnDisable() {
        acceptBtr.OnClick -= CreateAccount;
	}
	
	public void CreateAccount(){
		www.CreateAccount(ID_TextInput.Text, PS_TextInput.Text, CreateAccountMessageBox);
		//TextClear();
	}
	
	public void TextClear(){
		ID_TextInput.Text = "";
		PS_TextInput.Text = "";
	}
	
	public void CreateAccountMessageBox(string msg){
		if(msg == WWWMessage_.OK){
			string writeStr = "";
			writeStr = ID_TextInput.Text + " " + PS_TextInput.Text;
			//Debug.Log(writeStr);
			FileIO_.WriteStringToFile(writeStr, Setting_.settingFileName);
			//string id = parseStrList[0];
			GUI_Setting_.PLAYER_ID = ID_TextInput.Text;
			
			MessageBox_ msgBox = GameObject.Instantiate(msgBoxPrefabs) as MessageBox_;// msgBoxPrefabs) as MessageBox_;
			msgBox.Initalize(this, "Account is created", CreatedAccountEvents);	
			//Application.LoadLevel("WaitRoom");
		}
		else{
			string msgBoxStr = "Unknown Error";
			if(msg == WWWMessage_.FAIL_ID_DUP){
				msgBoxStr = WWWMessage_.FAIL_ID_DUP;	
			}
			
			MessageBox_ msgBox = GameObject.Instantiate(msgBoxPrefabs) as MessageBox_;// msgBoxPrefabs) as MessageBox_;
			msgBox.Initalize(this, msgBoxStr);	
		}
	}
	
	void CreatedAccountEvents(){
		//titleBackground.EndAniAndDestroy();
		//waitBackground.Initialize();
		StartCoroutine(Animation_.TransformAToB(bar.transform, 0.5f, new Vector3(0, 1.15f, 0)));
		transformer.BeginTransform();
	}
}
