using UnityEngine;
using System.Collections;

public class Loading_Script : MonoBehaviour {
	
	public tk2dUIProgressBar bar;

	// Use this for initialization
	void Start () {
		bar.Value = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		bar.Value += 0.01f;
		
		if(bar.Value==1.0f)
			Application.LoadLevel("Main_Scene");
	}
}
