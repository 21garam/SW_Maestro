﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public float maxSpeed;
	public float accelerate;
	public float friction;
	
	public float magnificationPower;
	public float magnificationAddition;
	
	public tk2dSprite mSprite = null;
	
	public Vector2 playerPosition;
	
	public float sizeOfFish;
	private float dx = 0.0f, dy = 0.0f;
	private FeverGauge feverInstance = null;
	
	public float feverLimit;
	public float feverPower;
	public float feverAcc;
	private bool bFeverTime = false;
	private float currentFeverTime = 0.0f;
	
	private static Player instance;
	
	public static Player Instance
	{
		get
		{
			if(instance == null)
			{
				Debug.LogError("Player fish instance does not exist");
			}
			return instance;
		}
	}
	
	public float SizeOfFish
	{
		get
		{
			return sizeOfFish;
		}
	}
	
	public float FeverAcc
	{
		get
		{
			return feverAcc;
		}
	}
	
	public int FeverTime
	{
		get
		{
			return bFeverTime?1:0;
		}
	}
	
	public void SetFlip()
	{
		tk2dSprite spr = mSprite;
		spr.scale = new Vector3(-Mathf.Abs(spr.scale.x), spr.scale.y, spr.scale.z);
		
		if(dy > 0.0f)
		{
			spr.transform.transform.rotation = Quaternion.Euler(0f, 0f, 10f);
		}
		else if(dy < 0.0f)
		{
			spr.transform.transform.rotation = Quaternion.Euler(0f, 0f, -10f);
		}
		
		if(dy == 0.0f)
		{
			spr.transform.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
		}
	}
	
	void riseFish(float feedSize)
	{}
	
	void Awake()
	{
		instance = this;
		transform.localScale = new Vector3(sizeOfFish, sizeOfFish, transform.localScale.z);
	}
	
	void Start () {
		feverInstance = FeverGauge.Instance;
	}
	
	void Update () {
		CharacterController controller = GetComponent<CharacterController>();
		Vector3 v;
		
		if(Input.GetMouseButton(0))
		{
			dy+=accelerate * Time.deltaTime;
		}
		else
		{
			dy-=accelerate * Time.deltaTime;
		}
		
		float _friction = friction * Time.deltaTime;
		float _maxSpeed = maxSpeed * Time.deltaTime;
		
		v.x = dx = (dx>_maxSpeed)?_maxSpeed:(dx<-_maxSpeed)?-_maxSpeed:dx;
		v.y = dy = (dy>_maxSpeed)?_maxSpeed:(dy<-_maxSpeed)?-_maxSpeed:dy;
		v.x = dx += (dx>=_friction)?-_friction:(dx<=-_friction)?_friction:-dx;
		v.y = dy += (dy>=_friction)?-_friction:(dy<=-_friction)?_friction:-dy;
		v.z = 0;
				
		SetFlip();
		
		if(bFeverTime == true)
		{
			currentFeverTime += Time.deltaTime;
			
			if(currentFeverTime > feverLimit)
			{
				bFeverTime =false;
				feverInstance.turnEnable();
			}
		}
		
		controller.Move(v);
		playerPosition = controller.transform.position;
	}

	public bool EatFeedFish(float feedFishSize)
	{
		if(feedFishSize>sizeOfFish && bFeverTime == false)
		{
			return false;
		}
		
		if(feverInstance.getPoint(feedFishSize) == false)
		{
			if(bFeverTime==false)
			{
				bFeverTime = true;
				currentFeverTime = 0.0f;
			}
			transform.localScale = new Vector3(3, 3, transform.localScale.z);
		}
		else
		{
			sizeOfFish = sizeOfFish*magnificationPower+magnificationAddition;
			transform.localScale = new Vector3(sizeOfFish, sizeOfFish, transform.localScale.z);
		}
		
		ScoreScript.Score += (int)Mathf.Pow(feedFishSize*10, 2);
		return true;
	}
}
