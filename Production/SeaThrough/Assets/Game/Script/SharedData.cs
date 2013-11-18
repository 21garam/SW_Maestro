using UnityEngine;
using System.Collections;

public class SharedData {//: MonoBehaviour {
	
	public static int bodyId = 3;
	public static int eyesId = 3;
	public static int mouthId = 3;
	public static int finId = 3;
	public static int score = 0;
	
	static SharedData(){
	}
	
	//private static SharedData instance;
	
	//public static SharedData Instance
	//{
	//	get
	//	{
	//		if(instance == null)
	//		{
	//			Debug.LogError("Shared Data instance does not exist");
	//		}
	//		return instance;
	//	}
	//}
	
	//void Awake()
	//{
	//	instance = this;
		/*
		bodyId = 0;
		eyesId = 1;
		mouthId = 0;
		finId = 1;*/
	//}
	
	//void Start () {
	
	//}
	
	//void Update () {
	
	//}
};
