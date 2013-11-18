using UnityEngine;
using System.Collections;

public class GUI_Option_ : MonoBehaviour {
	public tk2dCamera cam;
	public tk2dUIItem optionBtr;
	public OptionBox_ optionBoxPrefabs;
	public Curtain__ curtainPrefabs;
	Curtain__ curtain;
	
	void OnEnable(){
	   	optionBtr.OnClick += CreatePause;
	}
	
	void CreatePause(){
		Sound_.PlaySound("click");
		StateHelper_.Pause();
		curtain = GameObject.Instantiate(curtainPrefabs) as Curtain__;
		curtain.Initialize(cam.nativeResolutionWidth, cam.nativeResolutionHeight, cam.CameraSettings.orthographicPixelsPerMeter);
		curtain.FadeIn();
		
		OptionBox_ optionBox = GameObject.Instantiate(optionBoxPrefabs) as OptionBox_;
		optionBox.Initialize(CreateResume);
		optionBox.transform.position = new Vector3(0, 0, -5);//= transform.position;
		//optionBox.transform.position //+= new Vector3(0, 0, -5);
		
		Helper__.SetLayer(optionBtr.gameObject, "DisalbedUI");
	}
	
	void CreateResume(){
		curtain.FadeOut();
		StateHelper_.Resume();
		Helper__.SetLayer(optionBtr.gameObject, "EnabledUI");
	}
}
