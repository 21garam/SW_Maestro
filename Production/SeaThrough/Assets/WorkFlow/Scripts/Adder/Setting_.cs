using UnityEngine;
using System.Collections;

public static class Setting_ {
	public static string settingFileName = "setting.txt";
	public static string playerID;
}
/*
	//string fileName = "setting.txt";
	//tk2dTextMesh textMesh;
	
	void Start () {
		//string str = "abc\nabc\n"; 
		//ParseString(str);
		//textMesh = transform.GetChild(0).GetComponent<tk2dTextMesh>();
		//FileIO_.WriteStringToFile("test io", fileName);
		//textMesh.text = FileIO_.ReadStringFromFile(fileName);	
		//textMesh.Commit();
	}
	
	private void ParseString(string str){
		string[] words = str.Split('\n');
		if(words.Length > 0)
			Setting_.playerID = words[0];
	}
	
	//private void EnemyFishSpawn(Event_ eve){
	//	string[] words = eve.m_param.Split(' ');
	//	float h = (float)Helper_.ConvertStringtoFloat(words[0]);
	//	int size = Helper_.ConvertStringtoInt(words[1]);
	//	float coolTime = (float)Helper_.ConvertStringtoFloat(words[2]); 
	//	string kind = words[3];	
	//	StartCoroutine(EnemyFishSpawn(new Vector3(screenSize.x, screenSize.y * h, 0), size, coolTime, kind));
	//}
	//void Update () { 
	
	//}
 */