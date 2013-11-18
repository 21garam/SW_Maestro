using UnityEngine;
using System.Collections;

public class Curtain__ : MonoBehaviour {
	public tk2dSprite white;
	void Start () {
		
	}
	
	public void Initialize(float screenWidth, float screenHeight, float ratio){
		white.transform.localPosition = new Vector3(screenWidth/2,  screenHeight/2, white.transform.localPosition.z);
		
		float lenthUnit = 1 / ratio;
		white.scale = new Vector3(lenthUnit * screenWidth / white.GetBounds().size.x, 
			lenthUnit * screenHeight / white.GetBounds().size.y, 1);
	}
	
	public void FadeOut(){
		white.color = new Color(0, 0, 0, 0.5f);
		StartCoroutine(Animation_.LerpColorAToB(white, 1, new Color(0, 0, 0, 0)));
	}
	
	public void FadeIn(){
		white.color = new Color(0, 0, 0, 0.0f);
		StartCoroutine(Animation_.LerpColorAToB(white, 1, new Color(0, 0, 0, 0.5f)));
	}
}
