using UnityEngine;
using System.Collections;

public class MessageBox_ : MonoBehaviour {
	public tk2dTextMesh message;
	public bool active = false;
	// Use this for initialization
	//void Start () {
		
	//}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void SetMessage(string text){
		message.text = text;
		message.Commit();
		active = true;
	}
}
