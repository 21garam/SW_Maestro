using UnityEngine;
using System.Collections;

public class Menu_ : MonoBehaviour {
	WWW_ www;
	public Content_ content;
	//string parsingText;
	
	void Start () {
		www = GetComponent<WWW_>();
		www.GetFrinedList(10, GetParsingString);
		//Debug.Log(parsingText);
		//content.Parsing(parsingText);
	}
	
	void GetParsingString(string text){
		//this.parsingText = text;
		content.Parsing(text);
	}
	
	void Update () {
	
	}
}
