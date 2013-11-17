using UnityEngine;
using System.Collections;

public class MessageBox_ : MonoBehaviour {
	public tk2dTextMesh message;
	public tk2dUIItem confirmBtr;
	MonoBehaviour parent;
	Animation_.CallBackPtr callback = null;
	
	public void Initalize(MonoBehaviour parent, string text, Animation_.CallBackPtr callback = null){
		transform.position = parent.transform.position;
		transform.position -= Vector3.forward * 6;
		this.parent = parent;
		Helper__.SetLayer(parent.gameObject, "DisalbedUI");
		message.text = text;
		message.Commit();
		
		if(callback != null)
			this.callback = callback;
	}
	
	void OnEnable() {
        confirmBtr.OnClick += Close;
    } 
	
	void Close(){
		Helper__.SetLayer(parent.gameObject, "EnabledUI");
		GameObject.Destroy(gameObject);
		if(callback != null)
			callback();
	}
}