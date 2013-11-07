using UnityEngine;
using System.Collections;

public class SharedData : MonoBehaviour {
	
	public int bodyId;
	public int eyesId;
	public int mouthId;
	public int finId;
	
	public int score;
	
	private static SharedData instance;
	
	public static SharedData Instance
	{
		get
		{
			if(instance == null)
			{
				Debug.LogError("Shared Data instance does not exist");
			}
			return instance;
		}
	}
	
	void Awake()
	{
		instance = this;
		
		bodyId = 0;
		eyesId = 1;
		mouthId = 0;
		finId = 1;
	}
	
	void Start () {
	
	}
	
	void Update () {
	
	}
}
