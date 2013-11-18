using UnityEngine;
using System.Collections;

public class ReadyCode_ : MonoBehaviour {
	public WWW_ www;
	
	public tk2dUIItem Hp_up_btn;
	public tk2dUIItem Hp_down_btn;
	
	public tk2dUIItem Score_up_btn;
	public tk2dUIItem Score_down_btn;
	
	public tk2dUIItem Fever_up_btn;
	public tk2dUIItem Fever_down_btn;
	
	int hp = 0;
	int score = 0;
	int fever = 0;
	
	public tk2dTextMesh hp_text;
	public tk2dTextMesh score_text;
	public tk2dTextMesh fever_text;
	
	// Use this for initialization
	void Start () {
		GetPlayerInfo();
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void Initialize(){
		GetPlayerInfo();
	}
	
	void GetPlayerInfo(){
		www.GetPlayerInfo(GUI_Setting_.PLAYER_ID, CallBackFunc_GetPlayerInfo);
	}
	
	void SetPlayerInfo(){
		www.UpdateAccount_Ability(GUI_Setting_.PLAYER_ID, null, hp, score, fever);
	}
			
	void CallBackFunc_GetPlayerInfo(string xml){
		if(XMLParser_.PlayerInfoXMLParse(xml)){
			hp = XMLParser_.PlayerAccountInfo.hp_lv;
			score = XMLParser_.PlayerAccountInfo.speed_lv;
			fever = XMLParser_.PlayerAccountInfo.fever_lv;
			
			hp_text.text = hp.ToString();
			score_text.text = score.ToString();
			fever_text.text = fever.ToString();
		}
	}
	
	void OnEnable()
	{
		Hp_up_btn.OnClick += Hp_up;
		Hp_down_btn.OnClick += Hp_down;
		
		Score_up_btn.OnClick += Score_up;
		Score_down_btn.OnClick += Score_down;
	
		Fever_up_btn.OnClick += Fever_up;
		Fever_down_btn.OnClick += Fever_down;
	}
	
	void OnDisable()
	{
		Hp_up_btn.OnClick -= Hp_up;
		Hp_down_btn.OnClick += Hp_down;
		
		Score_up_btn.OnClick -= Score_up;
		Score_down_btn.OnClick -= Score_down;
		
		Fever_up_btn.OnClick -= Fever_up;
		Fever_down_btn.OnClick -= Fever_down;
	}
	
	void Hp_up(){
		hp++;
		SetPlayerInfo();
		//Debug.Log("HP : "+ hp.ToString());
		//www.UpdateAccount_Ability(GUI_Setting_.PLAYER_ID, AcceptCallBackFunc, hp, score, fever);
		hp_text.text = hp.ToString();
	}
	
	void Hp_down(){
		hp--;
		SetPlayerInfo();
		//www.UpdateAccount_Ability(GUI_Setting_.PLAYER_ID, AcceptCallBackFunc, hp, score, fever);
		hp_text.text = hp.ToString();
	}
	
	void Score_up(){
		score++;
		SetPlayerInfo();
		score_text.text = score.ToString();
	}
	
	void Score_down(){
		score--;
		SetPlayerInfo();
		score_text.text = score.ToString();
	}
	
	void Fever_up(){
		fever++;
		SetPlayerInfo();
		fever_text.text = fever.ToString();
	}
	
	void Fever_down(){
		fever--;
		SetPlayerInfo();
		fever_text.text = fever.ToString();
	}
			
	void AcceptCallBackFunc(string msg){
		//Debug.Log(msg);
	}
}
