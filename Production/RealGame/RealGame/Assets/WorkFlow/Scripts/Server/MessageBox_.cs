using UnityEngine;
using System.Collections;

public class MessageBox_ : MonoBehaviour {
	public tk2dTextMesh message;
	public tk2dUIItem closeBtr;
	public Camera cam;
	MonoBehaviour parent;
	
	public void Initalize(MonoBehaviour parent, string text){
		transform.position = parent.transform.position;
		transform.position -= Vector3.forward * 2;
		this.parent = parent;
		parent.enabled = false;
		message.text = text;
		message.Commit();
	}
	
	void OnEnable() {
        closeBtr.OnClick += Close;
    } 
	
	void Close(){
		parent.enabled = true;
		GameObject.Destroy(gameObject);
	}
}
