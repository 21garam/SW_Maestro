using UnityEngine;
using System.Collections;

public class LogoBackground_ : MonoBehaviour {
	public tk2dCamera cam;
	public float logoTime = 2;
	public float m_velocity = 0.5f;
	tk2dSprite sprite;
	//float timeLine = 0;
	Animation_.CallBackPtr callBackPtr = null;
	
	void Start () {
		//Initialize();
		//BeginAni();
	}
	
	public void Initialize(){
		if(cam == null){	
			Debug.Log("cam is null");
			return;
		}
		
		float nativeResolutionX = cam.nativeResolutionWidth; 
		float nativeResolutionY = cam.nativeResolutionHeight; 
		float ratio = cam.CameraSettings.orthographicPixelsPerMeter;
		//Debug.Log(nativeResolutionX);
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
	
	//void Update () {
	//	if(timeLine < 5){
	//		timeLine += Time.deltaTime;
	//	}
	//	else{
	//		EndAniAndDestroy();	
	//	}
	//}
	
	public void BeginAni(Animation_.CallBackPtr callBackPtr_ = null){
		//Color color;
		sprite.color = new Color(0, 0, 0, 1);
		StartCoroutine(Animation_.LerpColorAToB(sprite, 1, new Color(1, 1, 1, 1), DoAfterBeginAni));
		if(callBackPtr_ != null)
			callBackPtr = callBackPtr_;
	}
	
	public void DoAfterBeginAni(){
		StartCoroutine(EndAniCoroutin());
		//StartCoroutine(
		//StartCoroutine(Animation_.LerpColorAToB(sprite, 1, new Color(1, 1, 1, 0), SelfDestroy));
	}
	
	IEnumerator EndAniCoroutin(){
		yield return new WaitForSeconds(logoTime);	
		StartCoroutine(Animation_.LerpColorAToB(sprite, 1, new Color(1, 1, 1, 0), EndAni));
	}
	
	void EndAni(){
		Debug.Log("aaa");
		if(callBackPtr != null)
			callBackPtr();
		SelfDestroy();
	}
	
	void SelfDestroy(){
		Debug.Log("Logo is Destroyed");
		GameObject.Destroy(this.gameObject);
	}
}
