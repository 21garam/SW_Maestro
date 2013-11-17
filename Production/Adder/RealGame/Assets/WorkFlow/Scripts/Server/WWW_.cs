using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WWWMessage_{
	public static string OK = "ok";
	public static string FAIL_ID_DUP = "fail : ID Duplicated";
	public static string FAIL_ID_NONE = "fail : ID not Existed";
	public static string FAIL_PS_WRONG = "fail : PS is wrong";
}

public class WWW_ : MonoBehaviour {
	public delegate void CallBackPtr(string str);
	public const int INTEGER_NULL = -1;
	public const string STRING_NULL = null;
	private string URL = "http://americanoninja86.appspot.com";
	//private string URL = "http://localhost:8080/"; 
	
	public void Login(string id, string password, CallBackPtr func){
		WWWForm form = new WWWForm();
		form.AddField("action", "login");
		form.AddField("id",id);
		form.AddField("password",password);
		StartCoroutine(WaitingForResponse(new WWW(URL, form), func));
		form = null;
	}
	
	private void FormAddHelperToInt(WWWForm form, string key, string val){
		if(val != null)
			form.AddField(key, Util_.ConvertStringtoInt(val));
		else
			form.AddField(key, 0);
	}
	
	private void FormAddHelperToString(WWWForm form, string key, string val = null){
		if(val != null)
			form.AddField(key, val);
		else
			form.AddField(key, "null");
	}
	
	public void CreateAccount(string id, string password, CallBackPtr func = null,
							  int score = 0, int soft_cash = 0, int hard_cash = 0, 
							  int equipment = 0, int item = 0, 
							  int buying_equipment_body = 0, int buying_equipment_eye = 0,
							  int buying_equipment_mouth = 0, int buying_equipment_fin = 0,
							  int hp_lv = 0, //int scale_lv = 0,
							  int speed_lv = 0, int fever_lv = 0,
							  string nick_name = "", int exp_lv = 0, int exp_sub = 0){
		WWWForm form = new WWWForm();
		form.AddField("action", "create");
		form.AddField("id",id);
		form.AddField("password",password);
		
		form.AddField("score",score);
		form.AddField("soft_cash",soft_cash);
		form.AddField("hard_cash",hard_cash);
		form.AddField("equipment",equipment);
		form.AddField("item",item);
		
		form.AddField("buying_equipment_eye", buying_equipment_eye);
		form.AddField("buying_equipment_body", buying_equipment_body);
		form.AddField("buying_equipment_mouth", buying_equipment_mouth);
		form.AddField("buying_equipment_fin", buying_equipment_fin);
		
		form.AddField("hp_lv", hp_lv);
		//form.AddField("scale_lv", scale_lv);
		form.AddField("speed_lv", speed_lv);
		form.AddField("fever_lv", fever_lv);
		
		form.AddField("nick_name", nick_name);
		form.AddField("exp_lv", exp_lv);
		form.AddField("exp_sub", exp_sub);
		StartCoroutine(WaitingForResponse(new WWW(URL, form), func));
		form = null;
	}
	
	public void GetPlayerInfo(string id, CallBackPtr func){
		WWWForm form = new WWWForm();
		form.AddField("action", "getPlayerInfo");
		form.AddField("id", id);
		
		StartCoroutine(WaitingForResponse(new WWW(URL, form), func));
		form = null;
	}
	
	public void UpdateAccount(string id, CallBackPtr func = null, 
							  int score = INTEGER_NULL, int soft_cash = INTEGER_NULL, int hard_cash = INTEGER_NULL, 
							  int equipment = INTEGER_NULL, int item = INTEGER_NULL,  
							  int buying_equipment_body = INTEGER_NULL, int buying_equipment_eye = INTEGER_NULL,
							  int buying_equipment_mouth = INTEGER_NULL, int buying_equipment_fin = INTEGER_NULL,
							  int hp_lv = INTEGER_NULL, //int scale_lv = INTEGER_NULL,
							  int speed_lv = INTEGER_NULL, int fever_lv = INTEGER_NULL,
							  string nick_name = STRING_NULL, int exp_lv = INTEGER_NULL, int exp_sub = INTEGER_NULL,
							  string password = STRING_NULL){
		WWWForm form = new WWWForm();
		form.AddField("action", "update");
		form.AddField("id", id);
		
		form.AddField("score",score);
		form.AddField("soft_cash",soft_cash);
		form.AddField("hard_cash",hard_cash);
		form.AddField("equipment",equipment);
		form.AddField("item",item);
		FormAddHelperToString(form, "password", password);
		
		form.AddField("buying_equipment_eye", buying_equipment_eye);
		form.AddField("buying_equipment_body", buying_equipment_body);
		form.AddField("buying_equipment_mouth", buying_equipment_mouth);
		form.AddField("buying_equipment_fin", buying_equipment_fin);
		
		form.AddField("hp_lv", hp_lv);
		//form.AddField("scale_lv", scale_lv);
		form.AddField("speed_lv", speed_lv);
		form.AddField("fever_lv", fever_lv);
		
		FormAddHelperToString(form, "nick_name", nick_name);
		form.AddField("exp_lv", exp_lv);
		form.AddField("exp_sub", exp_sub);
		
		StartCoroutine(WaitingForResponse(new WWW(URL, form), func));
		form = null;
	}
	
	public void UpdateAccount_BuyingEquipment(string id, CallBackPtr func = null, 
								int buying_equipment_body = INTEGER_NULL, int buying_equipment_eye = INTEGER_NULL,
							  	int buying_equipment_mouth = INTEGER_NULL, int buying_equipment_fin = INTEGER_NULL){
		UpdateAccount(id, func, INTEGER_NULL, INTEGER_NULL, INTEGER_NULL, INTEGER_NULL, INTEGER_NULL, 
					  buying_equipment_body, buying_equipment_eye, buying_equipment_mouth, buying_equipment_fin);
	}
	
	public void UpdateAccount_Equipment(string id, CallBackPtr func = null, int equipment = INTEGER_NULL){
		UpdateAccount(id, func, INTEGER_NULL, INTEGER_NULL, INTEGER_NULL, equipment);
	}
	
	public void UpdateAccount_Item(string id, CallBackPtr func = null, int item = INTEGER_NULL){
		UpdateAccount(id, func, INTEGER_NULL, INTEGER_NULL, INTEGER_NULL, INTEGER_NULL, item);
	}
	
	public void UpdateAccount_Ability(string id, CallBackPtr func = null, 
		int hp_lv = INTEGER_NULL, int speed_lv = INTEGER_NULL, int fever_lv = INTEGER_NULL){
		UpdateAccount(id, func, INTEGER_NULL, INTEGER_NULL, INTEGER_NULL, INTEGER_NULL, INTEGER_NULL, INTEGER_NULL, INTEGER_NULL, INTEGER_NULL, INTEGER_NULL, 
			hp_lv, speed_lv, fever_lv);
	}
	
	public void UpdateAccount_EXP(string id, CallBackPtr func = null, int exp_lv = INTEGER_NULL, int exp_sub = INTEGER_NULL){
		UpdateAccount(id, func, INTEGER_NULL, INTEGER_NULL, INTEGER_NULL, INTEGER_NULL, INTEGER_NULL, INTEGER_NULL, INTEGER_NULL,
					  INTEGER_NULL, INTEGER_NULL, INTEGER_NULL, INTEGER_NULL, INTEGER_NULL, STRING_NULL, exp_lv, exp_sub);
	}
	
	public void UpdateAccount_NickName(string id, CallBackPtr func = null, string nick_name = STRING_NULL){
		UpdateAccount(id, func, INTEGER_NULL, INTEGER_NULL, INTEGER_NULL, INTEGER_NULL, INTEGER_NULL, INTEGER_NULL, INTEGER_NULL, INTEGER_NULL,
			INTEGER_NULL, INTEGER_NULL, INTEGER_NULL, INTEGER_NULL, nick_name);
	}
	
	public void GetRankingList(int count, string id, CallBackPtr func = null){
		WWWForm form = new WWWForm();
		form.AddField("action", "getRankingList");
		form.AddField("id", id);
		form.AddField("count", count);
		StartCoroutine(WaitingForResponse(new WWW(URL, form), func));
		form = null;
	}
	
	public IEnumerator WaitingForResponse(WWW www, CallBackPtr callBackPtr){
		yield return www;
		if(www.error == null){
			//Debug.Log("WWW Link is Successful.");
			//Debug.Log(www.text);
		}
		else
			Debug.Log("WWW Link is Failed.");
		
		if(callBackPtr != null){
			callBackPtr(www.text);
			callBackPtr = null;
		}
		
		www.Dispose();
	}
}
