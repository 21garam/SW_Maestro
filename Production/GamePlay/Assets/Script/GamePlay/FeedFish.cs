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
			Strategy_.GoSignPattern(me.gameObject, signSize, strPos, me.velocity.x, time);		
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
			Strategy_.GoSignPattern(me.gameObject, signSize, strPos, me.velocity.x, time);
		}
	}
	
	class Type3 : Action_
	{
		FeedFish me;
		Vector2 strPos;
		Vector2 signSize;
		float time;
		
		public Type3(FeedFish _me)
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
			Strategy_.GoSignPattern(me.gameObject, signSize, strPos, me.velocity.x, time);
		}
	}
	
	public SingleSprite_ spritePrefabs;
	SingleSprite_ sprite;
	Vector3 velocity;
	Action_ action;
	CapsuleCollider col;
	
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
				action = new Type3(this);
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
