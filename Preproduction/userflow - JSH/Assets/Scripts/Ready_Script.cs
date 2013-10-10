using UnityEngine;
using System.Collections;

public class Ready_Script : MonoBehaviour {

	public tk2dUIItem btn;
	public tk2dUIItem btn2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnEnable()
	{
		btn.OnClick += ClickButton;
		btn2.OnClick += ClickButton2;
	}
	
	void OnDisable()
	{
		btn.OnClick -= ClickButton;
		btn2.OnClick -= ClickButton2;
	}
	
	void ClickButton()
	{
		Application.LoadLevel("Game_Scene");
	}
	void ClickButton2()
	{
		Application.LoadLevel("Main_Scene");
	}
}
