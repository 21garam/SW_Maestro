﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public float maxSpeed;
	public float accelerate;
	public float friction;
	
	public tk2dSprite mSprite = null;
	
	public Vector2 playerPosition;
	
	public float sizeOfFish;
	private float dx = 0.0f, dy = 0.0f;
	
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
	
	public void SetFlip()
	{
		tk2dSprite spr = mSprite;
		transform.localScale = new Vector3(sizeOfFish, sizeOfFish, transform.localScale.z);
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
	}
	
	void Start () {
	
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
		
		
		controller.Move(v);
		playerPosition = controller.transform.position;
	}

	public bool EatFeedFish(float feedFishSize)
	{
		if(feedFishSize>sizeOfFish)
		{
			return false;
		}
		
		ScoreScript.Score += (int)Mathf.Pow(feedFishSize*10, 2);
		//sizeOfFish += 0.05f;
		transform.localScale = new Vector3(sizeOfFish, sizeOfFish, transform.localScale.z);
		return true;
	}
}
