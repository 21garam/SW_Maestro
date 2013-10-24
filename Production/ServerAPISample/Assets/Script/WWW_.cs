using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WWWMessage_{
	public static string OK = "ok";
	public static string FAIL_ID_DUP = "fail : ID Duplicated";
	public static string FAIL_ID_NONE = "fail : ID not Existed";
}

public class WWW_ : MonoBehaviour {
	public delegate void CallBackPtr(string str);
	public const int INTEGER_NULL = -1;
	public const string STRING_NULL = null;
	private string URL = "http://americanoninja86.appspot.com";
	//private string URL = "http://localhost:8080/"; 
	
	public void Login(string id, string password, CallBackPtr func = null){
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
	
	private void FormAddHelperToString(WWWForm form, string key, string val){
		if(val != null)
			form.AddField(key, val);
		else
			form.AddField(key, "null");
	}
	
	public void CreateAccount(string id, string password, CallBackPtr func = null,
							  int score = 0, int soft_cash = 0, int hard_cash = 0, 
							  int equipment = 0, int item = 0){
		WWWForm form = new WWWForm();
		form.AddField("action", "create");
		form.AddField("id",id);
		form.AddField("password",password);
		
		form.AddField("score",score);
		form.AddField("soft_cash",soft_cash);
		form.AddField("hard_cash",hard_cash);
		form.AddField("equipment",equipment);
		form.AddField("item",item);
		
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
							  int equipment = INTEGER_NULL, int item = INTEGER_NULL, string password = STRING_NULL){
		WWWForm form = new WWWForm();
		form.AddField("action", "update");
		form.AddField("id", id);
		
		form.AddField("score",score);
		form.AddField("soft_cash",soft_cash);
		form.AddField("hard_cash",hard_cash);
		form.AddField("equipment",equipment);
		form.AddField("item",item);
		FormAddHelperToString(form, "password", password);
		
		StartCoroutine(WaitingForResponse(new WWW(URL, form), func));
		form = null;
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
