using UnityEngine;
using System.Collections;

public class OptionBox_ : MonoBehaviour {
	public tk2dUIItem resumeBtr;
	public tk2dUIToggleButton soundTog;
	public tk2dUIToggleButton bgmTog;
	
	Animation_.CallBackPtr callbakcPtr;
	
	void OnEnable() {
	 	soundTog.IsOn = Sound_.sound_enabled;
	 	bgmTog.IsOn = BGM_.bgm_enabled;
		resumeBtr.OnClick += Close;
		soundTog.OnToggle += ToggleSound;
		bgmTog.OnToggle += ToggleBGM;
	}
	
	public void Initialize(Animation_.CallBackPtr callbakcPtr_ = null){
		if(callbakcPtr_ != null)
			callbakcPtr = callbakcPtr_;
	}
	
	void ToggleSound(tk2dUIToggleButton obj){
		Sound_.PlaySound("click");
		if(obj.IsOn)
			Sound_.sound_enabled = true;
		else
			Sound_.sound_enabled = false;
	}
	
	void ToggleBGM(tk2dUIToggleButton obj){
		Sound_.PlaySound("click");
		if(obj.IsOn){
			BGM_.bgm_enabled = true;
			BGM_.PlayBGM();
		}
		else{
			BGM_.bgm_enabled = false;
			BGM_.StopBGM();
		}
	}
	
	void Close(){
		Sound_.PlaySound("click");
		if(callbakcPtr != null)
			callbakcPtr();// = callbakcPtr_;
		GameObject.Destroy(this.gameObject);
	}
}
