using UnityEngine;
using System.Collections;

public class LoadingAni_ : MonoBehaviour {
	tk2dSprite sprite;
	tk2dTextMesh textMesh;
	float zRot = 0;
	float zPos;
	float xLenth;
	bool isEndLoading = false;
	
	void Start() {
		//enabled = false;
	}
	
	public bool IsEndLoading{
		get{
			return isEndLoading;
		}
	}
	
	public void Initialize(float screenWidth, float screenHeight, float ratio){
		sprite = transform.GetChild(0).GetComponent<tk2dSprite>();//GetComponent<tk2dSprite>();
		textMesh = transform.GetChild(1).GetComponent<tk2dTextMesh>();
		xLenth = (1 / ratio) * screenWidth / 2 + sprite.GetBounds().size.x;
		enabled = true;
	}
	
	public void BeginLoadingAni(){
		sprite.transform.localPosition = new Vector3(-xLenth, 0, 0);
		StartCoroutine(Animation_.TransformAToB(sprite.transform, 1, new Vector3(0, 0, 0)));
		
		textMesh.transform.localScale = new Vector3(0, 0, 1);
		StartCoroutine(Animation_.ScaleAToB(textMesh.transform, 1, new Vector3(1, 1, 1)));
	}
	
	public void EndLoadingAni(){
		StartCoroutine(Animation_.ScaleAToB(textMesh.transform, 1, new Vector3(0, 0, 1)));
		StartCoroutine(Animation_.TransformAToB(sprite.transform, 1, new Vector3(xLenth, 0, 0), SetEndFlag));
	}
	
	public void SetEndFlag(){
		isEndLoading = true;
	}
	
	void Update(){
		sprite.transform.localEulerAngles = new Vector3(0, 0, zRot++);		
		zRot = zRot % 360;
	}
}
