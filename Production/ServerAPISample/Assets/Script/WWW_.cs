using UnityEngine;
using System.Collections;
using System.Collections.Generic;

	
public class WWWMessage_{
	public static string ACCOUNT_CREATE_FAIL = "Account Create Failed : already ID exist";
	public static string ACCOUNT_CREATE_OK = "Account Create OK";
	
	public static string ACCOUNT_UPDATE_FAIL = "Account Update Failed : ID not exist";
	public static string ACCOUNT_UPDATE_OK = "Account Update OK";
	
	public static string LOGIN_FAIL = "fail";
	public static string LOGIN_OK = "ok";
};

public class WWW_ : MonoBehaviour {
	public delegate void CallBackPtr(string str);
	private string URL = "http://localhost:8080/"; 
	//private string URL = "http://americanoninja86.appspot.com";
	
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
	
	/*
	public void CreateAccount(string id, string password, CallBackPtr func = null,
							  string score = null, string soft_cash = null, string hard_cash = null, 
							  string equipment = null, string item = null){
		WWWForm form = new WWWForm();
		form.AddField("action", "create");
		form.AddField("id",id);
		form.AddField("password",password);
		
		FormAddHelperToInt(form, "score", score);
		FormAddHelperToInt(form, "soft_cash", soft_cash);
		FormAddHelperToInt(form, "hard_cash", hard_cash);
		FormAddHelperToInt(form, "equipment", equipment);
		FormAddHelperToInt(form, "item", item);
		
		StartCoroutine(WaitingForResponse(new WWW(URL, form), func));
		form = null;
	}
	*/
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
	
	public void UpdateAccount(string id, CallBackPtr func = null, 
							  int score = 0, int soft_cash = 0, int hard_cash = 0, 
							  int equipment = 0, int item = 0, string password = null){
		WWWForm form = new WWWForm();
		form.AddField("action", "update");
		form.AddField("id", id);
		
		//FormAddHelperToInt(form, "score", score);
		//FormAddHelperToInt(form, "soft_cash", soft_cash);
		//FormAddHelperToInt(form, "hard_cash", hard_cash);
		//FormAddHelperToInt(form, "equipment", equipment);
		//FormAddHelperToInt(form, "item", item);
		
		form.AddField("score",score);
		form.AddField("soft_cash",soft_cash);
		form.AddField("hard_cash",hard_cash);
		form.AddField("equipment",equipment);
		form.AddField("item",item);
		FormAddHelperToString(form, "password", password);
		
		StartCoroutine(WaitingForResponse(new WWW(URL, form), func));
		form = null;
	}
	
	public void GetRankingList(int count, CallBackPtr func = null, string id = null){
		WWWForm form = new WWWForm();
		if(id != null){
			form.AddField("action", "getLocalRankingList");
			form.AddField("id", id);
			form.AddField("count", count);
		}
		else{
			form.AddField("action", "getGlobalRankingList");
			form.AddField("count", count);
		}
		StartCoroutine(WaitingForResponse(new WWW(URL, form), func));
	}
	
	public IEnumerator WaitingForResponse(WWW www, CallBackPtr callBackPtr){
		yield return www;
		if(www.error == null){
			Debug.Log("WWW Link is Successful.");
			Debug.Log(www.text);
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
