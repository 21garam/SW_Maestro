using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
		#if UNITY_EDITOR
		if(Input.GetKeyDown(KeyCode.Space)){
			Application.LoadLevel("GamePlayScene");
		}
		#elif UNITY_ANDROID
		if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended){
			Application.LoadLevel("GamePlayScene");
		}
		#endif
	}
}
