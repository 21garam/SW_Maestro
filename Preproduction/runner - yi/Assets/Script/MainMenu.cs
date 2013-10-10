using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	void Start () {
	
	}
	
	bool isLoad = false;
	void Update () {
		CharacterController controller = GetComponent<CharacterController>();
		#if UNITY_EDITOR
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Application.LoadLevel("GamePlayScene");
		}
		#elif UNITY_ANDROID
		if(isLoad)
			return;
		foreach(Touch touch in Input.touches){
			if(touch.phase == TouchPhase.Began){
				Application.LoadLevel("GamePlayScene");
				isLoad = true;
				break;
			}
		}
		
		#endif
	}
}
