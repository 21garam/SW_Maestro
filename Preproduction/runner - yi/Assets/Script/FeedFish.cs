using UnityEngine;
using System.Collections;

public class FeedFish : MonoBehaviour {
	
	public float sizeOfFish;
	public float velocity;
	public int dir;
	
	private float default_y;
	
	private tk2dSprite mSprite;
	public tk2dSprite mWarnning;

	void Awake()
	{
		mSprite = GetComponent<tk2dSprite>();
		mSprite.transform.position = new Vector3(Random.Range(40, 60), mSprite.transform.position.y, mSprite.transform.position.z);
		//mSprite.transform.position = new Vector3(Random.Range(-40, 40), mSprite.transform.position.y, mSprite.transform.position.z);
		velocity = Random.Range(8, 15);
		dir = -1;
		//dir = (Random.Range(-10,10)>0)?1:-1;
		
		sizeOfFish = 1+(Random.Range(-50, 20)/100.0f);
		transform.localScale = new Vector3(sizeOfFish, sizeOfFish, transform.localScale.z);
	}
	
	void Start () {
		default_y = mSprite.transform.position.y;
		if(dir == -1)
		{
			mSprite.scale = new Vector3(Mathf.Abs(mSprite.scale.x), mSprite.scale.y, mSprite.scale.z);
		}
		else
		{
			mSprite.scale = new Vector3(-Mathf.Abs(mSprite.scale.x), mSprite.scale.y, mSprite.scale.z);
		}
		
		
	}
	
	float Distance2to3(Vector2 p1, Vector3 p2)
	{
		return Mathf.Sqrt(Mathf.Pow(p1.x-p2.x, 2) + Mathf.Pow(p1.y-p2.y, 2));
	}
	
	void Update () {
		Vector3 v;
		if(Player.Instance == false)
		{
			return;
		}
		v.x = mSprite.transform.position.x + (velocity * dir * Time.deltaTime);
		v.y = mSprite.transform.position.y;
		v.z = mSprite.transform.position.z;
		
		mSprite.transform.position = v;
		
		if(mSprite.transform.position.x < -30)
		//if(mSprite.transform.position.x-pv.x > 50 || mSprite.transform.position.x-pv.x < -50)
		{
			Relocation();
		}
		
		if(Distance2to3(Player.Instance.playerPosition, mSprite.transform.position) < 15
			&& Player.Instance.sizeOfFish >= sizeOfFish)
		{
			mWarnning.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
		}
		else
		{
			mWarnning.transform.localScale = new Vector3(0.0f, 0.0f, 1.0f);
		}
	}
	
	void OnTriggerEnter(Collider coll)
	{
		if(coll.gameObject.tag == "Player")
		{
			if(Player.Instance.EatFeedFish(sizeOfFish))
				Relocation();
			else
			{
				Application.LoadLevel("MainMenuScene");
			}				
		}
	}
	
	void Relocation()
	{
		velocity = Random.Range(8, 15);
		dir = -1;
		//dir = (Random.Range(-10,10)>0)?1:-1;
		Vector2 v = Player.Instance.playerPosition;
		if(dir == -1)
		{
			mSprite.scale = new Vector3(Mathf.Abs(mSprite.scale.x), mSprite.scale.y, mSprite.scale.z);
			mSprite.transform.position = new Vector3(30, default_y, mSprite.transform.position.z);
		}
		else
		{
			mSprite.scale = new Vector3(-Mathf.Abs(mSprite.scale.x), mSprite.scale.y, mSprite.scale.z);
			mSprite.transform.position = new Vector3(-30+v.x, default_y+v.y, mSprite.transform.position.z);
		}
		
		sizeOfFish = Player.Instance.SizeOfFish*(1+(Random.Range(-50, 20)/100.0f));
		//Debug.Log("size of feed fish : " + sizeOfFish);
		transform.localScale = new Vector3(sizeOfFish, sizeOfFish, transform.localScale.z);
	}
}
