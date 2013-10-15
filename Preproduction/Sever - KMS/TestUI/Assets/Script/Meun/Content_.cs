using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Content_ : MonoBehaviour {
	public tk2dTextMesh textMesh;
	public tk2dUIScrollableArea scoll;
	//public List<tk2dTextMesh> textMeshList;
	// Use this for initialization
	void Start () {
	//	textMeshList = new List<tk2dTextMesh>(20);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void Parsing(string text){
		string buf = "";
		int count = 1;
		for(int i = 0; i < text.Length; i++){
			char ch = text[i];
			if(ch != '\n')
				buf += ch;
			else{
				AddMeshText(buf, count++);
				buf = "";
			}
		}
		scoll.ContentLength = count > 5 ? (1.0f + (0.2f) * (count-5)) : 1.0f;
	}
	
	private void AddMeshText(string buf, int count){
		tk2dTextMesh textMeshPrefab = GameObject.Instantiate(textMesh) as tk2dTextMesh;	
		textMeshPrefab.transform.parent = transform;
		textMeshPrefab.transform.localPosition = new Vector3(0.05f, -0.2f * count, 0);
		textMeshPrefab.text = buf;
		textMeshPrefab.Commit();
	}
}
