using UnityEngine;
using System.Collections;

public class FeverGauge : MonoBehaviour {
	
	public float requirePoint;
	public float magnificationPower;
	public float magnificationAddition;
	
	public tk2dSprite feverField = null;
	
	private bool enable = true;
	private float feverPoint = 0;
	
	private static FeverGauge instance = null;
	
	public static FeverGauge Instance
	{
		get
		{
			if(instance == null)
			{
				Debug.LogError("FeverGauge instance does not exist");
			}
			return instance;
		}
	}
	
	void Awake()
	{
		instance = this;
		feverField.transform.localScale = new Vector3(0, 1, 1);
	}
	
	public bool Enable
	{
		get
		{
			return enable;
		}
	}
	
	public void turnEnable()
	{
		feverPoint = 0;
		enable = true;
	}
	
	public void turnDisable()
	{
		feverPoint = requirePoint;
		enable = false;
	}
	
	public bool getPoint(float feedSize)
	{
		if(enable == false)
			return false;
		
		feverPoint += feedSize;
		
		if(feverPoint >= requirePoint)
		{
			turnDisable();
		}
		
		feverField.transform.localScale = new Vector3(feverPoint/requirePoint, 1, 1);
		
		return enable;
	}
}
