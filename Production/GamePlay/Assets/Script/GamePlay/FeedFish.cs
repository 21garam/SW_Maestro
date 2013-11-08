using UnityEngine;
using System.Collections;

public class FeedFish : MonoBehaviour 
{
	class Kind1_Action_ : Action_
	{
		FeedFish me;
		public Kind1_Action_(FeedFish _me)
		{
			this.me = _me;
		}
		
		public void Update()
		{
			Strategy_.GoStraight(me.gameObject, me.velocity);		
		}
	}
	
	class Kind2_Action_ : Action_
	{
		FeedFish me;
		Vector2 strPos;
		Vector2 signSize;
		float time;
		
		public Kind2_Action_(FeedFish _me)
		{
			this.me = _me;
			time = 0;
			strPos = new Vector2(me.gameObject.transform.localPosition.x,
								me.gameObject.transform.localPosition.y);
			signSize = new Vector2(100, 100);
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
				velocity = new Vector3(-100.0f, 0, 0);
				sprite = GameObject.Instantiate(spritePrefabs) as SingleSprite_;
				sprite.transform.parent = transform;
				sprite.Initialize(100, 100, "FeedFish");
				col = transform.collider as CapsuleCollider;
				col.radius = sprite.Width() / 2;
			break;
				
			case "KIND2":
				velocity = new Vector3(-100.0f, 0, 0);
				sprite = GameObject.Instantiate(spritePrefabs) as SingleSprite_;
				sprite.transform.parent = transform;
				sprite.Initialize(50, 50, "FeedFish");
				col = transform.collider as CapsuleCollider;
				col.radius = sprite.Width() / 2;
			break;
		}
	}
	
	private void SubInitializeAboutAction(string kind)
	{
		switch(kind)
		{	
			case "KIND1":
				action = new Kind1_Action_(this);
			break;
				
			case "KIND2":
				action = new Kind2_Action_(this);
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
