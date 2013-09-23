using UnityEngine;
using System.Collections;

public class GameOver_ : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	bool keyDown = false;
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space) && !keyDown){
			keyDown = true;
			Application.LoadLevel("MainGame");
		}
	}
}
