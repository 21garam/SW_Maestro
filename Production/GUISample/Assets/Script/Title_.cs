using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Title_ : MonoBehaviour {
	public tk2dCamera cam;
	public Loading_ loadingPrefabs;
	Loading_ loading;
	public bool isLoadingTest = false;
	float testTime = 0;
	WaitBackground_ waitBackgournd;
	public GameObject signUpWindow;
	public WWW_ www;
	List<string> parseStrList = new List<string>();
	void Start () {
		waitBackgournd = transform.GetChild(0).GetComponent<WaitBackground_>();
		waitBackgournd.Initialize(cam.nativeResolutionWidth, cam.nativeResolutionHeight, cam.CameraSettings.orthographicPixelsPerMeter);
		//title
		
		if(isLoadingTest)
			BeginLaoding();
		
		string retStr = FileIO_.ReadStringFromFile(Setting_.settingFileName);
		
		ParseString(retStr);
		
		if(parseStrList.Count <= 0){
			FileIO_.WriteStringToFile("null null", Setting_.settingFileName);
			ShowSignUpWindow();
		}
		else
			Login();
	}
	
	void ParseString(string str){
		parseStrList.Clear();
		if(str != null){
			string[] words = str.Split(' ');
			parseStrList.Add(words[0]);
			parseStrList.Add(words[1]);
		}
	}
	
	void ShowSignUpWindow(){
		StartCoroutine(Animation_.TransformAToB(signUpWindow.transform, 1, new Vector3(0, 0, 0)));
	}
	
	void Login(){
		string id = parseStrList[0];
		string ps = parseStrList[1];
		if(id != "null" && ps != "null")
			www.Login(id, ps, LoginCallbackPtr);
		else
			ShowSignUpWindow();
	}
			
	void LoginCallbackPtr(string msg){
		if(msg == WWWMessage_.FAIL_ID_NONE){
			Debug.Log("FAIL_ID_NONE");
			ShowSignUpWindow();
		}
		else if(msg == WWWMessage_.FAIL_PS_WRONG){
			Debug.Log("FAIL_PS_WRONG");
			ShowSignUpWindow();
		}
		else if(msg == WWWMessage_.OK){
			Application.LoadLevel("WaitRoom");
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
