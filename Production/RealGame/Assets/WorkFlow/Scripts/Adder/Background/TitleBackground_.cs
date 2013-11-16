using UnityEngine;
using System.Collections;

public class TitleBackground_ : MonoBehaviour {
	public tk2dCamera cam;
	public float m_velocity = 0.5f;
	tk2dSprite sprite;
	
	void Start () {
	}
	
	public void Initialize(){
		if(cam == null){	
			Debug.Log("cam is null");
			return;
		}
		
		float nativeResolutionX = cam.nativeResolutionWidth; 
		float nativeResolutionY = cam.nativeResolutionHeight; 
		float ratio = cam.CameraSettings.orthographicPixelsPerMeter;
		
		sprite = GetComponent<tk2dSprite>();
		float lenthUnit = 1 / ratio;
		transform.localScale = new Vector3(lenthUnit * nativeResolutionX / sprite.GetBounds().size.x, 
			lenthUnit * nativeResolutionY / sprite.GetBounds().size.y, 1);
		//m_material = gameObject.renderer.material;
		//Color color = m_material.GetColor("_Color");
		//Debug.Log(color);
		//color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
		//m_material.SetColor("_Color", color);
	}
	
	//public void BeginAni(){
	//	Color color;
	//	sprite.color = new Color(1, 1, 1, 0);
	//	StartCoroutine(Animation_.LerpColorAToB(sprite, 1, new Color(1, 1, 1, 0)));
	//}
	
	public void EndAniAndDestroy(){
		StartCoroutine(Animation_.LerpColorAToB(sprite, 1, new Color(1, 1, 1, 0), SelfDestroy));
	}
	
	void SelfDestroy(){
		GameObject.Destroy(this.gameObject);
	}
}
