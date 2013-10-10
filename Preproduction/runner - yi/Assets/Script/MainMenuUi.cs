using UnityEngine;
using System.Collections;

public class MainMenuUi : MonoBehaviour {
	public tk2dUIItem playGameButton;
	
	void OnEnable()
	{
		playGameButton.OnClick += ClickButton;
	}
	
	void OnDisable()
	{
		playGameButton.OnClick -= ClickButton;
	}
	
	void ClickButton()
	{
		Application.LoadLevel("Ready_Scene");
	}
}
