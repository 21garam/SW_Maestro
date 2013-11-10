using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Loading_ : MonoBehaviour {
	//public tk2dCamera cam;
	Curtain_ curtain;
	LoadingAni_ ani;
	//public bool isTest = false;
	float testTime = 0;
	
	//public tk2dCamera SetCam{
	//	set{
	//		cam = value;
	//	}
	//}
	
	void Start () {
		enabled = false;
		//Initalize();
		//BeginLoading();
	}
	
	//tk2dCamera cam
	public void Initalize(tk2dCamera cam){
		curtain = transform.GetChild(0).GetComponent<Curtain_>();
		ani = transform.GetChild(1).GetComponent<LoadingAni_>();
		curtain.Initialize(cam.nativeResolutionWidth, cam.nativeResolutionHeight, cam.CameraSettings.orthographicPixelsPerMeter / 20);
		ani.Initialize(cam.nativeResolutionWidth, cam.nativeResolutionHeight, cam.CameraSettings.orthographicPixelsPerMeter);
		enabled = true;
	}
	
	public void Update(){
		//if(isTest){
		//	testTime += Time.deltaTime;
		//	if(testTime > 10){
		//		EndLoading();
		//		isTest = false;
		//	}
		//}
		if(ani.IsEndLoading)
			DestroyItself();
	}
	
	public void BeginLoading(){
		curtain.FadeIn();
		ani.BeginLoadingAni();
	}
	
	public void EndLoading(){
		curtain.FadeOut();
		ani.EndLoadingAni();
	}
	
	public void DestroyItself(){
		Destroy(this.gameObject);
	}
}
