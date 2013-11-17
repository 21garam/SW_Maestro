using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnStart_ : MonoBehaviour {
	public tk2dCamera cam;
	public LogoBackground_ logoBackground;
	//public TitleBackground_ titleBackgournd;
	public WaitBackground_ waitBackground;
	public GameObject bar;
	public Loading_ loadingPrefabs;
	Loading_ loading;
	//public bool isLoadingTest = false;
	//float testTime = 0;
	
	//public static string loginKey = "";
	
	public GameObject signUpWindow;
	public WWW_ www;
	List<string> parseStrList = new List<string>();
	Transformer_ firstWindow;
	void Start () {
		PreInitialze();
		logoBackground.Initialize();
		logoBackground.BeginAni(Initialize);
		//Initialize();
	}
	
	void PreInitialze(){
		waitBackground.Initialize();
	}
	
	void Initialize(){
		firstWindow = GetComponent<Transformer_>();
		
		BeginLaoding();
		
		string retStr = FileIO_.ReadStringFromFile(Setting_.settingFileName);
		
		ParseString(retStr);
		
		if(parseStrList.Count <= 0){
			FileIO_.WriteStringToFile("null null", Setting_.settingFileName);
			EndLoading(EndLoadingType.SHOW_SIGN_UP_WINDOW);
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
	
	void Login(){
		string id = parseStrList[0];
		string ps = parseStrList[1];
		if(id != "null" && ps != "null")
			www.Login(id, ps, LoginCallbackPtr);
		else{
			EndLoading(EndLoadingType.SHOW_SIGN_UP_WINDOW);
			//ShowSignUpWindow();
		}
	}
			
	void LoginCallbackPtr(string msg){
		if(msg == WWWMessage_.FAIL_ID_NONE){
			Debug.Log("FAIL_ID_NONE");
			EndLoading(EndLoadingType.SHOW_SIGN_UP_WINDOW);
		}
		else if(msg == WWWMessage_.FAIL_PS_WRONG){
			Debug.Log("FAIL_PS_WRONG");
			EndLoading(EndLoadingType.SHOW_SIGN_UP_WINDOW);
		}
		else if(msg == WWWMessage_.OK){
			string id = parseStrList[0];
			GUI_Setting_.PLAYER_ID = id;
			EndLoading(EndLoadingType.SHOW_FIRST_WINDOW);
		}
	}
	
	void Update(){
	}
	
	void BeginLaoding(){
		loading = GameObject.Instantiate(loadingPrefabs) as Loading_;
		loading.Initalize(cam);
		loading.BeginLoading();
	}
	
	enum EndLoadingType{
		SHOW_SIGN_UP_WINDOW,
		SHOW_FIRST_WINDOW,
	};
	
	void EndLoading(EndLoadingType endLoadingtype){
		switch(endLoadingtype){
			case EndLoadingType.SHOW_SIGN_UP_WINDOW:
				//StartCoroutine(CheckEndBeginLoading_ShowFirstWindow());
				StartCoroutine(CheckEndBeginLoading_ShowSignUpWindow());
			break;
			
			case EndLoadingType.SHOW_FIRST_WINDOW:
				StartCoroutine(CheckEndBeginLoading_ShowFirstWindow());
			break;
		}
	}
	
	IEnumerator CheckEndBeginLoading_ShowSignUpWindow(){
		while(!loading.IsEndBeginAni()){
			yield return 0;
		}
		loading.EndLoading(ShowSignUpWindow);
	}
	
	IEnumerator CheckEndBeginLoading_ShowFirstWindow(){
		while(!loading.IsEndBeginAni()){
			yield return 0;
		}
		loading.EndLoading(CallFirstWindow);
	}
	
	void CallFirstWindow(){
		//Debug.Log("CallFirstWindow");
		StartCoroutine(Animation_.TransformAToB(bar.transform, 0.5f, new Vector3(0, 1.15f, 0)));
		firstWindow.BeginTransform();
	}
	
	void ShowSignUpWindow(){
		//Debug.Log("aa");
		StartCoroutine(Animation_.TransformAToB(signUpWindow.transform, 1, new Vector3(0, 0, 0)));
	}
}
