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
			me.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
			
			time = 0;
			strPos = new Vector2(me.gameObject.transform.localPosition.x,
								me.gameObject.transform.localPosition.y);
			signSize = new Vector2(100, 25);
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
			me.transform.localScale = new Vector3(0.45f, 0.45f, 1.0f);
			
			time = 0;
			strPos = new Vector2(me.gameObject.transform.localPosition.x,
								me.gameObject.transform.localPosition.y);
			signSize = new Vector2(100, 25);
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
		Vector2 signSize;
		float time;
		
		public FeedSmall(FeedFish _me)
		{
			me = _me;
			me.transform.localScale = new Vector3(0.4f, 0.4f, 1.0f);
			
			time = 0;
			strPos = new Vector2(me.gameObject.transform.localPosition.x,
								me.gameObject.transform.localPosition.y);
			signSize = new Vector2(100, 25);
		}
		
		public void Update()
		{
			time += Time.deltaTime;
			Strategy_.GoCosinePatternEx(me.gameObject, signSize, strPos, me.velocity.x, time, 1.5f);
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
	
	public SingleSprite_ spritePrefabs;
	SingleSprite_ sprite;
	Vector3 velocity;
	Action_ action;
	CapsuleCollider col;

	private float sizeOfFish;
	
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
		switch(kind)
		{	
			case "KIND1":
				velocity = new Vector3(-200.0f, 0, 0);
				sprite = GameObject.Instantiate(spritePrefabs) as SingleSprite_;
				sprite.transform.parent = transform;
				sprite.Initialize("FeedFish");
				col = transform.collider as CapsuleCollider;
				col.radius = sprite.Width() / 5;
			break;
				
			case "KIND2":
				velocity = new Vector3(-200.0f, 0, 0);
				sprite = GameObject.Instantiate(spritePrefabs) as SingleSprite_;
				sprite.transform.parent = transform;
				sprite.Initialize("FeedFish");
				col = transform.collider as CapsuleCollider;
				col.radius = sprite.Width() / 5;
			break;
			
			case "FEED_SMALL":
				velocity = new Vector3(-200.0f, 0, 0);
				sprite = GameObject.Instantiate(spritePrefabs) as SingleSprite_;
				sprite.transform.parent = transform;
				sprite.Initialize("FeedFish");
				col = transform.collider as CapsuleCollider;
				col.radius = sprite.Width() / 5;
			break;
			
			case "FEED_RANDOM":
				velocity = new Vector3(-200.0f, 0, 0);
				sprite = GameObject.Instantiate(spritePrefabs) as SingleSprite_;
				sprite.transform.parent = transform;
				sprite.Initialize("FeedFish");
				col = transform.collider as CapsuleCollider;
				col.radius = sprite.Width() / 5;
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
}
