using UnityEngine;
using System.Collections;

public class FeedFish : MonoBehaviour 
{
	class Type1 : Action_
	{
		FeedFish me;
		
		Vector2 strPos;
		Vector2 signSize;
		float time;
		
		public Type1(FeedFish _me)
		{
			me = _me;
			me.sizeOfFish = 0.5f;
			me.transform.localScale = new Vector3(me.sizeOfFish, me.sizeOfFish, 1.0f);
			
			time = 0;
			strPos = new Vector2(me.gameObject.transform.localPosition.x,
								me.gameObject.transform.localPosition.y);
			signSize = new Vector2(100, 50);
		}
		
		public void Update()
		{
			time += Time.deltaTime;
			Strategy_.GoSinePatternEx(me.gameObject, signSize, strPos, me.velocity.x, time, 1.5f);		
		}
	}
	
	class Type2 : Action_
	{
		FeedFish me;
		Vector2 strPos;
		Vector2 signSize;
		float time;
		
		public Type2(FeedFish _me)
		{
			me = _me;
			me.sizeOfFish = 0.45f;
			me.transform.localScale = new Vector3(me.sizeOfFish, me.sizeOfFish, 1.0f);
			
			time = 0;
			strPos = new Vector2(me.gameObject.transform.localPosition.x,
								me.gameObject.transform.localPosition.y);
			signSize = new Vector2(100, 50);
		}
		
		public void Update()
		{
			time += Time.deltaTime;
			Strategy_.GoSinePatternEx(me.gameObject, signSize, strPos, me.velocity.x, time, 2.0f);
		}
	}
	
	class FeedSmall : Action_
	{
		FeedFish me;
		Vector2 strPos;
		Vector2 cosSize;
		float time;
		
		public FeedSmall(FeedFish _me)
		{
			me = _me;
			me.sizeOfFish = 0.4f;
			me.transform.localScale = new Vector3(me.sizeOfFish, me.sizeOfFish, 1.0f);
			
			time = 0;
			strPos = new Vector2(me.gameObject.transform.localPosition.x,
								me.gameObject.transform.localPosition.y);
			cosSize = new Vector2(100, 50);
		}
		
		public void Update()
		{
			time += Time.deltaTime;
			Strategy_.GoCosinePatternEx(me.gameObject, cosSize, strPos, me.velocity.x, time, 2.0f);
		}
	}
	
	class FeedRandom : Action_
	{
		FeedFish me;
		Vector2 strPos;
		Vector2 signSize;
		float time;
		
		int type;
		
		public FeedRandom(FeedFish _me)
		{
			me = _me;
			me.sizeOfFish = 0.4f + Random.Range(0, 2) * 0.05f;
			me.transform.localScale = new Vector3(me.sizeOfFish, me.sizeOfFish, 1.0f);			
			
			type = Random.Range(0, 2);
			time = 0;
			strPos = new Vector2(me.gameObject.transform.localPosition.x,
								me.gameObject.transform.localPosition.y);
			signSize = new Vector2(80+Random.Range(0, 40), 25+Random.Range(0, 50));
		}
		
		public void Update()
		{
			time += Time.deltaTime;
			switch(type)
			{
			case 0:
				Strategy_.GoCosinePatternEx(me.gameObject, signSize, strPos, me.velocity.x, time, 1.5f);
				break;
			
			case 1:
				Strategy_.GoSinePatternEx(me.gameObject, signSize, strPos, me.velocity.x, time, 1.5f);
				break;
				
			case 2:
				Strategy_.GoCosinePatternEx(me.gameObject, signSize, strPos, me.velocity.x, time, 3.0f);
				break;
			}
			
		}
	}
	
	class FeedBigSine : Action_
	{
		FeedFish me;
		Vector2 strPos;
		Vector2 sineSize;
		float time;
		
		public FeedBigSine(FeedFish _me)
		{
			me = _me;
			me.sizeOfFish = 0.5f;
			me.transform.localScale = new Vector3(me.sizeOfFish, me.sizeOfFish, 1.0f);
			
			time = 0;
			strPos = new Vector2(me.gameObject.transform.localPosition.x,
								me.gameObject.transform.localPosition.y);
			sineSize = new Vector2(100, 200);
		}
		
		public void Update()
		{
			time += Time.deltaTime;
			Strategy_.GoSinePatternEx(me.gameObject, sineSize, strPos, me.velocity.x, time, 0.8f);
		}
	}
	
	class FeedBigCosine : Action_
	{
		FeedFish me;
		Vector2 strPos;
		Vector2 cosSize;
		float time;
		
		public FeedBigCosine(FeedFish _me)
		{
			me = _me;
			me.sizeOfFish = 0.5f;
			me.transform.localScale = new Vector3(me.sizeOfFish, me.sizeOfFish, 1.0f);
			
			time = 2.8f;
			strPos = new Vector2(me.gameObject.transform.localPosition.x,
								me.gameObject.transform.localPosition.y);
			cosSize = new Vector2(100, 200);
		}
		
		public void Update()
		{
			time += Time.deltaTime;
			Strategy_.GoCosinePatternEx(me.gameObject, cosSize, strPos, me.velocity.x, time, 0.8f);
		}
	}
	
	class FeedBigSineEx : Action_
	{
		FeedFish me;
		Vector2 strPos;
		Vector2 sineSize;
		float time;
		static float correction;
		
		public FeedBigSineEx(FeedFish _me)
		{
			me = _me;
			me.sizeOfFish = 0.5f;
			me.transform.localScale = new Vector3(me.sizeOfFish, me.sizeOfFish, 1.0f);
			
			correction += 0.2f;
			time = correction;
			
			strPos = new Vector2(me.gameObject.transform.localPosition.x,
								me.gameObject.transform.localPosition.y);
			sineSize = new Vector2(100, 150);
		}
		
		public void Update()
		{
			time += Time.deltaTime;
			Strategy_.GoSinePatternEx(me.gameObject, sineSize, strPos, me.velocity.x, time, 0.9f);
		}
	}
	
	class FeedBigCosineEx : Action_
	{
		FeedFish me;
		Vector2 strPos;
		Vector2 cosSize;
		float time;
		static float correction;
		
		public FeedBigCosineEx(FeedFish _me)
		{
			me = _me;
			me.sizeOfFish = 0.5f;
			me.transform.localScale = new Vector3(me.sizeOfFish, me.sizeOfFish, 1.0f);
			
			correction += 0.2f;
			time = 2.8f + correction;
			strPos = new Vector2(me.gameObject.transform.localPosition.x,
								me.gameObject.transform.localPosition.y);
			cosSize = new Vector2(100, 150);
		}
		
		public void Update()
		{
			time += Time.deltaTime;
			Strategy_.GoCosinePatternEx(me.gameObject, cosSize, strPos, me.velocity.x, time, 0.9f);
		}
	}
	
	public SingleSprite_ spritePrefabs;
	SingleSprite_ sprite;
	Vector3 velocity;
	Action_ action;
	CapsuleCollider col;

	private float sizeOfFish;
	private bool isGoldFish;
	public float goldFishRatio;
	
	void Update () 
	{
		action.Update();
		DestroyCheck();
	}
	
	public void Initialize(Transform parent, Vector3 pos, string kind)
	{
		SubInitializeAboutProperties(kind);
		transform.parent = parent;
		transform.localPosition = pos;
		transform.localPosition += new Vector3(sprite.Width() / 2, 0, -0.1f);
		
		SubInitializeAboutAction(kind);
	}
	
	private void SubInitializeAboutProperties(string kind)
	{
		velocity = new Vector3(-200.0f, 0, 0);
		sprite = GameObject.Instantiate(spritePrefabs) as SingleSprite_;
		sprite.transform.parent = transform;
		
		if(Random.Range(0, 100) > goldFishRatio)
		{
			isGoldFish = false;
			sprite.Initialize("FeedFish");
		}
		else
		{
			isGoldFish = true;
			sprite.Initialize("FeedFish_Gold");
		}
		
		col = transform.collider as CapsuleCollider;
		col.radius = sprite.Width() / 5;
		
		switch(kind)
		{	
			case "KIND1":
			break;
				
			case "KIND2":
			break;
			
			case "FEED_SMALL":
			break;
			
			case "FEED_RANDOM":				
			break;
		}
	}
	
	private void SubInitializeAboutAction(string type)
	{
		switch(type)
		{	
			case "KIND1":
				action = new Type1(this);
			break;
				
			case "KIND2":
				action = new Type2(this);
			break;
			
			case "FEED_SMALL":
				action = new FeedSmall(this);
			break;
			
			case "FEED_RANDOM":
				action = new FeedRandom(this);
			break;
				
			case "FEED_BIGSINE":
				action = new FeedBigSine(this);
			break;
			
			case "FEED_BIGCOSINE":
				action = new FeedBigCosine(this);
			break;
			
			case "FEED_BIGSINEEX":
				action = new FeedBigSineEx(this);
			break;
			
			case "FEED_BIGCOSINEEX":
				action = new FeedBigCosineEx(this);
			break;
		}
	}
	
	public void DestroyCheck()
	{
		Vector3 worldPos = transform.TransformPoint(transform.position);
		if(worldPos.x < -sprite.Width())
		{
			DestroyItself();
		}
	}
	
	public void DestroyItself()
	{
		Destroy(this.gameObject);
	}
	
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Player")
		{
			PlayerFish.Instance.EatFeedFish(sizeOfFish);
			DestroyItself();
		}
	}
}
