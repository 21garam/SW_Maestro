using UnityEngine;
using System.Collections;

public class Title_ : MonoBehaviour {
	public tk2dCamera cam;
	public Loading_ loadingPrefabs;
	Loading_ loading;
	public bool isLoadingTest = false;
	float testTime = 0;
	WaitBackground_ waitBackgournd;
	
	void Start () {
		waitBackgournd = transform.GetChild(0).GetComponent<WaitBackground_>();
		waitBackgournd.Initialize(cam.nativeResolutionWidth, cam.nativeResolutionHeight, cam.CameraSettings.orthographicPixelsPerMeter);
		//title
		
		if(isLoadingTest)
			BeginLaoding();
		
		string retStr = FileIO_.ReadStringFromFile(Setting_.settingFileName);
		if(retStr == null){
			FileIO_.WriteStringToFile("null", Setting_.settingFileName);
			retStr = "null";
		}
		
		if(retStr == "null"){
			//Start Make ID
		}
		else{
			//Animation_.TransformAToB
			//Wait Room
		}
	}
	
	void Update(){
		if(isLoadingTest){
			testTime += Time.deltaTime;
			if(testTime > 5){
				isLoadingTest = false;
				EndLoading();
			}
		}
	}
	
	void BeginLaoding(){
		loading = GameObject.Instantiate(loadingPrefabs) as Loading_;
		loading.Initalize(cam);
		loading.BeginLoading();
	}
	
	void EndLoading(){
		loading.EndLoading();
	}
}
