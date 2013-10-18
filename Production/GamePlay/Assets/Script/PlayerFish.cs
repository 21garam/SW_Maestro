using UnityEngine;
using System.Collections;

public class PlayerFish : MonoBehaviour 
{
	public float maxSpeed;
	public float accelerate;
	public float friction;
	
	public float magnificationPower;
	public float magnificationAddition;
	
	public Vector2 playerPosition;
	
	public float sizeOfFish;
	private float baseSizeOfFish;
	private float dx = 0.0f, dy = 0.0f;
	
	public float maxHitPoint;
	public float nibbleHitPointPerSec;
	private float curHitPoint;
	
	/*
	public float feverLimit;
	public float feverPower;
	public float feverAcc;
	private bool bFeverTime = false;
	private float currentFeverTime = 0.0f;
	private int feverCount = 0;
	*/
	
	private static PlayerFish instance;
	
	public static PlayerFish Instance
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
	
	/*public float FeverAcc
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
	}*/
	
	public void SetFlip()
	{
		tk2dSprite spr = GetComponent<tk2dSprite>();
		
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
		baseSizeOfFish = sizeOfFish;
	}
	
	void Start () {
		//feverInstance = FeverGauge.Instance;
	}
	
	void Update () {
		CharacterController controller = GetComponent<CharacterController>();
		Vector3 v;
		
		if(Input.GetMouseButton(0) || Input.touchCount > 0)
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
		
		/*if(bFeverTime == true)
		{
			currentFeverTime += Time.deltaTime;
			
			if(currentFeverTime > feverLimit)
			{
				bFeverTime =false;
				feverInstance.turnEnable();
				transform.localScale = new Vector3(sizeOfFish, sizeOfFish, transform.localScale.z);
			}
		}*/
		
		controller.Move(v);
		playerPosition = controller.transform.position;
		
		if(playerPosition.y>=300.0f)
		{
			Vector2 tra = new Vector2(playerPosition.x, 300.0f);
			PlayerFish.instance.transform.position = tra;
		}
		if(playerPosition.y<=-300.0f)
		{
			Vector2 tra = new Vector2(playerPosition.x, -300.0f);
			PlayerFish.instance.transform.position = tra;
		}
	}

	public bool EatFeedFish(float feedFishSize)
	{
		if(feedFishSize>sizeOfFish/* && bFeverTime == false*/)
		{
			return false;
		}
		
		/*if(feverInstance.getPoint(feedFishSize) == false)
		{
			if(bFeverTime==false)
			{
				feverCount++;
				bFeverTime = true;
				currentFeverTime = 0.0f;
			}
			transform.localScale = new Vector3(3.5f, 3.5f, transform.localScale.z);
		}
		else*/
		{
			sizeOfFish = sizeOfFish*magnificationPower+magnificationAddition;
			transform.localScale = new Vector3(sizeOfFish, sizeOfFish, transform.localScale.z);
		}
		
		/*ScoreScript.Score += (int)Mathf.Pow(feedFishSize*10, 2);*/
		return true;
	}
}
