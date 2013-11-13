using UnityEngine;
using System.Collections;

public class PlayerFish : MonoBehaviour 
{
	public float maxYSpeed;
	public float accelerateY;
	public float friction;
	
	public float magnificationPower;
	public float magnificationAddition;
	
	public Vector2 playerPosition;
	
	public float sizeOfFish;
	private float distance;
	public float dx = 0.0f, dy = 0.0f;
	
	public float maxHitPoint;
	public float nibbleHitPointPerSec;
	private float curHitPoint;
	
	public float maxXSpeedAtNormal;
	public float maxXSpeedAtFeverTime;
	public float accelerateAtNormal;
	public float accelerateAtFeverTime;
	public float feverLimit;
	public float feverPower;
	public float feverAcc;
	private bool bFeverTime = false;
	private float currentFeverTime = 0.0f;
	private int feverCount = 0;
	private FeverGauge feverInstance;
	
	private bool isInvincible;
	public float invincibleTime;
	private float remainInvincibleTime;
	
	public tk2dSprite sprBody;
	public tk2dSprite sprEyes;
	public tk2dSprite sprMouth;
	public tk2dSprite sprFin;
	
	private SharedData SharedDataInstance;
	
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
	
	public float CurHitPoint
	{
		get
		{
			return curHitPoint;
		}
	}
	
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
	
	void Awake()
	{
		instance = this;
		
	}
	
	void Start () {
		feverInstance = FeverGauge.Instance;
		SharedDataInstance = SharedData.Instance;
		sprBody.SetSprite(string.Format("body_{0}", SharedDataInstance.bodyId));
		sprEyes.SetSprite(string.Format("eyes_{0}", SharedDataInstance.eyesId));
		sprMouth.SetSprite(string.Format("mouth_{0}", SharedDataInstance.mouthId));
		sprFin.SetSprite(string.Format("fin_{0}", SharedDataInstance.finId));
		
		curHitPoint = maxHitPoint;
	}
	
	void AccelerateSwimming()
	{
		if(bFeverTime)
			dx = Mathf.Min(maxXSpeedAtFeverTime, dx + (accelerateAtFeverTime * Time.deltaTime));
		else
			dx = Mathf.Min(maxXSpeedAtNormal, dx + (accelerateAtNormal * Time.deltaTime));
	}
	
	void Update () {		
		if(Input.GetMouseButton(0) || Input.touchCount > 0)
		{
			dy+=accelerateY * Time.deltaTime;
		}
		else
		{
			dy-=accelerateY * Time.deltaTime;
		}
		
		float _friction = friction * Time.deltaTime;
		float _maxSpeed = maxYSpeed * Time.deltaTime;
				
		AccelerateSwimming();
		dy = (dy>_maxSpeed)?_maxSpeed:(dy<-_maxSpeed)?-_maxSpeed:dy;
		dy += (dy>=_friction)?-_friction:(dy<=-_friction)?_friction:-dy;
				
		SetFlip();
		
		if(bFeverTime == true)
		{
			currentFeverTime += Time.deltaTime;
			
			if(currentFeverTime > feverLimit)
			{
				bFeverTime =false;
				feverInstance.turnEnable();
				transform.localScale = new Vector3(sizeOfFish, sizeOfFish, transform.localScale.z);
			}
		}
		
		this.transform.position+=new Vector3(0, dy, 0);
		playerPosition = this.transform.position;
		
		if(playerPosition.y>=600.0f)
		{
			Vector2 tra = new Vector2(playerPosition.x, 600.0f);
			PlayerFish.instance.transform.position = tra;
		}
		if(playerPosition.y<=40.0f)
		{
			Vector2 tra = new Vector2(playerPosition.x, 40.0f);
			PlayerFish.instance.transform.position = tra;
		}
		
		curHitPoint -= nibbleHitPointPerSec * Time.deltaTime;
		
		if(curHitPoint <= 0 && bFeverTime == false)
		{
			SharedDataInstance.score = ScoreScript.Score;
			Application.LoadLevel("GamePlay");
		}
		
		remainInvincibleTime -= Time.deltaTime;
		isInvincible = false;
	}

	public bool EatFeedFish(float feedFishSize)
	{
		if(feverInstance.getPoint(feedFishSize) == false)
		{
			if(bFeverTime==false)
			{
				feverCount++;
				bFeverTime = true;
				currentFeverTime = 0.0f;
			}
			transform.localScale = new Vector3(3.0f, 3.0f, transform.localScale.z);
		}
		else
		{
			sizeOfFish = sizeOfFish*magnificationPower+magnificationAddition;
			transform.localScale = new Vector3(sizeOfFish, sizeOfFish, transform.localScale.z);
		}
		
		ScoreScript.Score += (int)Mathf.Pow(feedFishSize*10, 2);
		curHitPoint += feedFishSize/5;
		
		return true;
	}
	
	public bool Bumped()
	{
		if(bFeverTime == true)
		{
			return true;
		}
		
		curHitPoint -= 15;
		dx = -50;
		remainInvincibleTime = invincibleTime;
		isInvincible = true;
		
		if(curHitPoint <= 0)
		{
			return false;
		}

		return true;
	}
	
	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "FeedFish")
		{
			EatFeedFish(1);
			GameObject.Destroy(col.gameObject);
		}
		
		if(col.gameObject.tag == "EnemyFish" && isInvincible == false)
		{
			Bumped();
		}
		
		if(col.gameObject.tag == "Obstacle" && isInvincible == false)
		{
			Bumped();
		}
	}
}
