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
	//private int feverCount = 0;
	public int feverCount = 0;
	private FeverGauge feverInstance;
	
	public bool isInvincible;
	public float invincibleTime;
	private float remainInvincibleTime;
	
	public tk2dSprite sprBody;
	public tk2dSprite sprEyes;
	public tk2dSprite sprMouth;
	public tk2dSprite sprFin;
	
	private SharedData SharedDataInstance;
	
	private static PlayerFish instance;	
	
	public bool Stop = true;
	public tk2dCamera cam;
	public Curtain__ curtainPrefabs;
	public GameOverBox_ gameOverBoxPrefabs;
	public Particle_ particle;
	
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
	//--
	void ScaleDown(){
		gameObject.transform.localScale = new Vector3(2, 2, 1);
		StartCoroutine(Effect_.ScaledDown(gameObject.transform, 3, new Vector3(1, 1, 1)));
	}
	
	void ScaleUp(){
		gameObject.transform.localScale = new Vector3(1, 1, 1);
		StartCoroutine(Effect_.ScaledUp(gameObject.transform, 3, new Vector3(2, 2, 2)));
	}
	
	void BeTransparent(){
		StartCoroutine(Effect_.DoTransparency(new tk2dSprite[4]{sprBody, sprEyes, sprMouth, sprFin}, invincibleTime));
	}
	
	void Die(){
		Stop = true;
		isInvincible = true;
		StateHelper_.PlayerDie();
		StateHelper_.DisabledOptionBtr();
		StartCoroutine(DieCoroutine());
	}
	
	IEnumerator DieCoroutine(){
		SphereCollider col = collider as SphereCollider;
		float yPos = transform.position.y;
		float zRot = 0;
		while(true){
			if(transform.position.y > cam.nativeResolutionHeight + col.radius * 4)
				break;
			transform.localEulerAngles = new Vector3(0, 0, zRot); 
			transform.localPosition = new Vector3(transform.position.x, yPos, transform.position.z);
			yPos += Time.deltaTime * 175;
			zRot += Time.deltaTime * 750;
			zRot = zRot % 360;
			yield return 0; 
		}
		ViewGameOver();
	}
	
	void ViewGameOver(){
		Curtain__ curtain = GameObject.Instantiate(curtainPrefabs) as Curtain__;
		curtain.Initialize(cam.nativeResolutionWidth, cam.nativeResolutionHeight, cam.CameraSettings.orthographicPixelsPerMeter);
		curtain.FadeIn();
		GameOverBox_ gameOverBox = GameObject.Instantiate(gameOverBoxPrefabs) as GameOverBox_;
		gameOverBox.Begin();
	}
	//--
	
	void AccelerateSwimming()
	{
		if(bFeverTime)
			dx = Mathf.Min(maxXSpeedAtFeverTime, dx + (accelerateAtFeverTime * Time.deltaTime));
		else
			dx = Mathf.Min(maxXSpeedAtNormal, dx + (accelerateAtNormal * Time.deltaTime));
	}
	
	void Update () {		
		if(Stop)
			return;
		//Update_Input();
		
		if(Input.GetMouseButton(0) || Input.touchCount > 0){
			dy+=accelerateY * Time.deltaTime;
		}
		else{
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
			//End Fever
			if(currentFeverTime > feverLimit)
			{
				particle.SetEmitterMinMax(80, 160);
				particle.SetXVelocity(-200);
				ScaleDown();
				BeTransparent();
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
			Die();
		}
		
		remainInvincibleTime -= Time.deltaTime;
		if(remainInvincibleTime < 0)
			isInvincible = false;
	}

	public bool EatFeedFish(float feedFishSize)
	{
		if(feverInstance.getPoint(feedFishSize) == false)
		{
			//Start Fever
			if(bFeverTime==false)
			{
				//particle
				particle.SetEmitterMinMax(160, 320);
				particle.SetXVelocity(-400);
				ScaleUp();
				feverCount++;
				bFeverTime = true;
				currentFeverTime = -feverLimit;
			}
		}
		else
		{
			//ScaleDown();
			sizeOfFish = sizeOfFish*magnificationPower+magnificationAddition;
			transform.localScale = new Vector3(sizeOfFish, sizeOfFish, transform.localScale.z);
		}
		ScoreScript.Score += (int)Mathf.Pow(feedFishSize*10, 2);
		curHitPoint += feedFishSize/10;
		
		return true;
	}
	
	public bool Bumped()
	{
		if(bFeverTime == true || isInvincible == true)
		{
			return true;
		}
		BeTransparent();
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
}
