using UnityEngine;
using System.Collections;

public class LoadingAni_ : MonoBehaviour {
	tk2dSprite sprite;
	tk2dTextMesh textMesh;
	float zRot = 0;
	float zPos;
	float xLenth;
	bool isEndLoading = false;
	Animation_.CallBackPtr endAniCallBack = null;
	bool isInit = false;
	bool isEndBeginAni = false;
	
	public float aniTime = 2;
	
	void Start() {
		if(!isInit)
			enabled = false;
	}
	
	public bool IsEndLoading{
		get{
			return isEndLoading;
		}
	}
	
	public void Initialize(float screenWidth, float screenHeight, float ratio){
		if(!isInit){
			isInit = true;
		}
		isEndBeginAni = false;
		enabled = true;
		sprite = transform.GetChild(0).GetComponent<tk2dSprite>();//GetComponent<tk2dSprite>();
		textMesh = transform.GetChild(1).GetComponent<tk2dTextMesh>();
		xLenth = (1 / ratio) * screenWidth / 2 + sprite.GetBounds().size.x;
		//if(callbackPtr != null)
		//	endAniCallBack = callbackPtr;
	}
	
	public void BeginLoadingAni(){
		sprite.transform.localPosition = new Vector3(-xLenth, 0, 0);
		StartCoroutine(Animation_.TransformAToB(sprite.transform, aniTime, new Vector3(0, 0, 0), SetEndBeginAni));
		
		textMesh.transform.localScale = new Vector3(0, 0, 1);
		StartCoroutine(Animation_.ScaleAToB(textMesh.transform, aniTime / 2, new Vector3(1, 1, 1)));
	}
	
	void SetEndBeginAni(){
		isEndBeginAni = true;
	}
	
	public bool IsEndBenginAni(){
		return isEndBeginAni;
	}
	
	public void EndLoadingAni(Animation_.CallBackPtr callbackPtr = null){
		if(callbackPtr != null)
			endAniCallBack = callbackPtr;
		StartCoroutine(Animation_.ScaleAToB(textMesh.transform, aniTime / 2, new Vector3(0, 0, 1)));
		StartCoroutine(Animation_.TransformAToB(sprite.transform, aniTime, new Vector3(xLenth, 0, 0), SetEndFlag));
	}
	
	public void SetEndFlag(){
		isEndLoading = true;
		if(endAniCallBack != null)
			endAniCallBack();
		GameObject.Destroy(this.gameObject);
	}
	
	void Update(){
		sprite.transform.localEulerAngles = new Vector3(0, 0, zRot);	
		zRot += Time.deltaTime * 100;
		zRot = zRot % 360;
	}
}
