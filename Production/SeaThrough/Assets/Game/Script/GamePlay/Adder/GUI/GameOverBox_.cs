using UnityEngine;
using System.Collections;

public class GameOverBox_ : MonoBehaviour {
public tk2dTextMesh gameOverText;
	public tk2dTextMesh touchText;
	bool isBegin = false;
	
	void Start(){
		if(!isBegin)
			enabled = false;
	}
	
	void Update(){
		if(Input.GetMouseButton(0)){
			Application.LoadLevel("Loading_Scene");
		}
	}
	
	public void Begin(){
		if(!isBegin)
			isBegin = true;
		enabled = true;
		transform.position = new Vector3(0, 0, -5);
		gameOverText.gameObject.transform.localScale = new Vector3(0, 0, 1);
		StartCoroutine(Animation_.ScaleAToB(gameOverText.gameObject.transform, 1, new Vector3(1, 1, 1), BlingBlingTouchText_Seq0));
	}
	
	void BlingBlingTouchText_Seq0(){
		touchText.color = new Color(1,1,1,1);
		StartCoroutine(Animation_.LerpColorAToB(touchText, 1.0f, new Color(1,1,1,0), BlingBlingTouchText_Seq1));
	}
	
	void BlingBlingTouchText_Seq1(){
		touchText.color = new Color(1,1,1,0);
		StartCoroutine(Animation_.LerpColorAToB(touchText, 1.0f, new Color(1,1,1,1), BlingBlingTouchText_Seq0));
	}
}
