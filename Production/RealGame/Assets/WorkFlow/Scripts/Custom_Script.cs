using UnityEngine;
using System.Collections;

public class Custom_Script : MonoBehaviour {
	
	public tk2dUIItem btn;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnEnable()
	{
		btn.OnClick += ClickButton;
	}
	
	void OnDisable()
	{
		btn.OnClick -= ClickButton;
	}
	
	void ClickButton()
	{
		Application.LoadLevel("Main_Scene");
	}
}
