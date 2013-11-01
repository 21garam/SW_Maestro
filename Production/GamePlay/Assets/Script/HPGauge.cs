using UnityEngine;
using System.Collections;

public class HPGauge : MonoBehaviour {
				
	public tk2dSprite HPFiend = null;
	private static HPGauge instance = null;
	
	public static HPGauge Instance
	{
		get
		{
			if(instance == null)
			{
				Debug.LogError("HPGauge instance does not exist");
			}
			return instance;
		}
	}
	
	void Awake()
	{
		instance = this;
		HPFiend.transform.localScale = new Vector3(1, 1, 1);
	}
	
	void UpdateHPGauge()
	{
		HPFiend.transform.localScale = new Vector3(1, 1, 1);
	}
}
