using UnityEngine;
using System.Collections;

public class GUI_Ready_ : MonoBehaviour {
public tk2dTextMesh text;
	bool isEnd = false;
	
	void Start () {
		//Begin();	
	}
	
	public void Begin(){
		Sequence_0();
	}
	
	public bool IsEnd{
		get{return isEnd;}
	}
	
	void Sequence_0(){
		//Debug.Log("aaa");
		text.text = "Ready";
		text.Commit();
		text.gameObject.transform.localScale = new Vector3(0, 0, 1);
		StartCoroutine(Animation_.ScaleAToB(text.gameObject.transform, 1f, new Vector3(1,1,1), Sequence_1));
	}
	
	void Sequence_1(){
		StartCoroutine(Animation_.LerpColorAToB(text, 0.5f, new Color(1,1,1,0), Sequence_2));	
	}
	
	void Sequence_2(){
		text.color = new Color(1,1,1,1);
		text.text = "GO !!!";
		text.Commit();
		text.gameObject.transform.localScale = new Vector3(0, 0, 1);
		StartCoroutine(Animation_.ScaleAToB(text.gameObject.transform, 1f, new Vector3(1,1,1), Sequence_3));
	}
	
	void Sequence_3(){
		StartCoroutine(Animation_.LerpColorAToB(text, 0.5f, new Color(1,1,1,0), Sequence_4));	
	}
	
	void Sequence_4(){
		isEnd = true;
	}
}
