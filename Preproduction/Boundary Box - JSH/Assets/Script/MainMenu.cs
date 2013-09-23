using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
		CharacterController controller = GetComponent<CharacterController>();
		
		if(Input.GetKeyDown(KeyCode.Space))
		{
			//Debug.Log("Space key down");
			Application.LoadLevel("GamePlayScene");
		}
	}
}
