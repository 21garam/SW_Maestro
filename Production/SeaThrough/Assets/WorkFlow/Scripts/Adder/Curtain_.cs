using UnityEngine;
using System.Collections;

public class Curtain_ : MonoBehaviour {
	//Vector2 screenSize;
	tk2dSprite white;
	
	void Start () {
		
	}
	
	public void Initialize(float screenWidth, float screenHeight, float ratio){
		//float nativeResolutionX = cam.nativeResolutionWidth; 
		//float nativeResolutionY = cam.nativeResolutionHeight; 
		//float ratio = cam.CameraSettings.orthographicPixelsPerMeter;
		
		//enabled = true;
		float lenthUnit = 1 / ratio;
		//transform.localScale = new Vector3(lenthUnit * nativeResolutionX, 
		//	lenthUnit * nativeResolutionY, 1);
		//screenSize = new Vector2(screenWidth, screenHeight);
		white = transform.GetComponent<tk2dSprite>();
		white.scale = new Vector3(lenthUnit * screenWidth / white.GetBounds().size.x, 
			lenthUnit * screenHeight / white.GetBounds().size.y, 1);
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
