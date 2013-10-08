using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WWW_ : MonoBehaviour {
	public delegate void CallBackPtr(string str);
	private string URL = "http://localhost:11080/";
	
	public void Login(string id, string password, CallBackPtr func){
		WWWForm form = new WWWForm();
		form.AddField("action", "Login");
		form.AddField("id",id);
		form.AddField("password",password);
		StartCoroutine(WaitingForResponse(new WWW(URL, form), func));
	}
	
	public void CreateAccount(string id, string password){
		WWWForm form = new WWWForm();
		form.AddField("action", "Create");
		form.AddField("id",id);
		form.AddField("password",password);
		StartCoroutine(WaitingForResponse(new WWW(URL, form), null));
	}
	
	/*
	public void SendScore(int score, string name){
		WWWForm form = new WWWForm();
		form.AddField("","");
		form.AddField("action", "Login");
		form.AddField("score", score);
		form.AddField("name", name);
		StartCoroutine(WaitingForResponse(new WWW(URL, form), null));
	}
	*/
	
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
	
	/*
	public WWW GET(string url) 
	{ 
		WWW www = new WWW(url); 
		StartCoroutine(WaitForRequest(www)); 
		return www; 
	}
	
	public WWW POST(string url, Dictionary<string, string> post) 
	{ 
		WWWForm form = new WWWForm(); 
 		foreach (KeyValuePair<string, string> post_arg in post){ 
			form.AddField(post_arg.Key, post_arg.Value); 
		} 
		WWW www = new WWW(url, form); 
		StartCoroutine(WaitForRequest(www)); 
		return www;
	} 
	
	private IEnumerator WaitForRequest(WWW www) {
		yield return www;
		if (www.error == null){ 
			Debug.Log("WWW Ok!: " + www.text);
		} 
		else{ 
			Debug.Log("WWW Error: " + www.error);
		}
	} 
	*/
}
