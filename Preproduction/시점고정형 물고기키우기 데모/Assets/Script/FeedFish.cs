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
		mSprite.transform.position = new Vector3(Random.Range(-40, 40), mSprite.transform.position.y, mSprite.transform.position.z);
		mSprite.transform.rotation = Quaternion.Euler(0f, 0f, -10f);
		velocity = Random.Range(5, 15);
		dir = (Random.Range(-10,10)>0)?1:-1;
		
		sizeOfFish = 1+(Random.Range(-6, 4)/10.0f);
		transform.localScale = new Vector3(sizeOfFish, sizeOfFish, transform.localScale.z);
	}
	
	void Start () {
		Vector2 v = Player.Instance.playerPosition;
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
		float Ran = Random.Range(1, 10);
		Vector2 pv = Player.Instance.playerPosition;
		v.x = mSprite.transform.position.x + (velocity * dir * Time.deltaTime);
		v.y = mSprite.transform.position.y + (velocity * dir * Time.deltaTime)/Ran;
		v.z = mSprite.transform.position.z;
		
		mSprite.transform.position = v;
		
		if(mSprite.transform.position.x > 50 || mSprite.transform.position.x < -50 || mSprite.transform.position.y > 20 || mSprite.transform.position.y < -20)
		{
			Relocation();
		}
		
		if(Distance2to3(Player.Instance.playerPosition, mSprite.transform.position) < 10
			&& Player.Instance.sizeOfFish > sizeOfFish)
		{
			Debug.Log("sadfasf");
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
		velocity = Random.Range(5, 15);
		dir = (Random.Range(-10,10)>0)?1:-1;
		Vector2 v = Player.Instance.playerPosition;
		if(dir == -1)
		{
			mSprite.scale = new Vector3(Mathf.Abs(mSprite.scale.x), mSprite.scale.y, mSprite.scale.z);
			mSprite.transform.position = new Vector3(30+v.x, default_y+v.y, mSprite.transform.position.z);
		}
		else
		{
			mSprite.scale = new Vector3(-Mathf.Abs(mSprite.scale.x), mSprite.scale.y, mSprite.scale.z);
			mSprite.transform.position = new Vector3(-30+v.x, default_y+v.y, mSprite.transform.position.z);
		}
		
		sizeOfFish = Player.Instance.SizeOfFish*(1+(Random.Range(-6, 4)/10.0f));
		//Debug.Log("size of feed fish : " + sizeOfFish);
		transform.localScale = new Vector3(sizeOfFish, sizeOfFish, transform.localScale.z);
	}
}
