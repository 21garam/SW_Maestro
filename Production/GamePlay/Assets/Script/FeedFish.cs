using UnityEngine;
using System.Collections;

public class FeedFish : MonoBehaviour {
	
	private float sizeOfFish;
	private float velocity;
	
	private float default_y;
	
	public tk2dSprite mSprite;

	void Awake()
	{
		mSprite.transform.position = new Vector3(Random.Range(900, 1000), mSprite.transform.position.y, mSprite.transform.position.z);
		velocity = Random.Range(120, 150);
		sizeOfFish = 1+(Random.Range(-50, 20)/100.0f);
		transform.localScale = new Vector3(sizeOfFish, sizeOfFish, transform.localScale.z);
	}
	
	void Start () {
		default_y = mSprite.transform.position.y;
		mSprite.scale = new Vector3(Mathf.Abs(mSprite.scale.x), mSprite.scale.y, mSprite.scale.z);
	}
	
	void Update () {
		Vector3 v;
		if(PlayerFish.Instance == false)
		{
			return;
		}
		v.x = mSprite.transform.position.x - (velocity * Time.deltaTime);
		v.y = mSprite.transform.position.y;
		v.z = mSprite.transform.position.z;
		
		mSprite.transform.position = v;
		
		if(mSprite.transform.position.x < -100)
		{
			Relocation();
		}
	}
	
	void OnTriggerEnter(Collider coll)
	{
		if(coll.gameObject.tag == "Player")
		{
			if(PlayerFish.Instance.EatFeedFish(sizeOfFish))
				Relocation();
		}
	}
	
	void Relocation()
	{
		velocity = Random.Range(120, 150);
		Vector2 v = PlayerFish.Instance.playerPosition;
		
		mSprite.scale = new Vector3(Mathf.Abs(mSprite.scale.x), mSprite.scale.y, mSprite.scale.z);
		mSprite.transform.position = new Vector3(900, default_y, mSprite.transform.position.z);
		
		sizeOfFish = PlayerFish.Instance.SizeOfFish*(1+(Random.Range(-50, 20)/100.0f));
		transform.localScale = new Vector3(sizeOfFish, sizeOfFish, transform.localScale.z);
	}
}
