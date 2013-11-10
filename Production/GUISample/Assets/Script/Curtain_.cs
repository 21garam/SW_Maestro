using UnityEngine;
using System.Collections;

public class Curtain_ : MonoBehaviour {
	//Vector2 screenSize;
	tk2dSprite white;
	
	void Start () {
		
	}
	
	public void Initialize(float screenWidth, float screenHeight, float ratio){
		//screenSize = new Vector2(screenWidth, screenHeight);
		white = transform.GetComponent<tk2dSprite>();
		white.scale = new Vector3((screenWidth / 2) / ratio, (screenHeight / 2) / ratio, 1);
	}
	
	public void FadeOut(){
		white.color = new Color(0, 0, 0, 0.5f);
		StartCoroutine(Animation_.LerpColorAToB(white, 1, new Color(0, 0, 0, 0)));
	}
	
	public void FadeIn(){
		white.color = new Color(0, 0, 0, 0);
		StartCoroutine(Animation_.LerpColorAToB(white, 1, new Color(0, 0, 0, 0.5f)));
	}
}
