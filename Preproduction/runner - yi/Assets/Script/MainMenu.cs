using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0)
		{
			Application.LoadLevel("GamePlayScene");
		}
	}
}
