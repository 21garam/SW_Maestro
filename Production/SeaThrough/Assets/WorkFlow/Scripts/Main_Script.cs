using UnityEngine;
using System.Collections;

public class Main_Script : MonoBehaviour {

	public tk2dUIItem Ready_btn;
	public tk2dUIItem Custom_btn;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnEnable()
	{
		Ready_btn.OnClick += ClickButton;
		Custom_btn.OnClick += ClickButton2;
	}
	
	void OnDisable()
	{
		Ready_btn.OnClick -= ClickButton;
		Custom_btn.OnClick -= ClickButton2;
	}
	
	void ClickButton()
	{
	}
	void ClickButton2()
	{
	}
}
