using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {
	class Kind1_Action_ : Action_{
		Obstacle me;
		public Kind1_Action_(Obstacle me_){
			this.me = me_;
		}
		
		public void Update(){
			Strategy_.GoStraight(me.gameObject, me.velocity);		
		}
	}
	
	public SingleSprite_ spritePrefabs;
	SingleSprite_ sprite;
	Vector3 velocity;
	Action_ action;
	ObstacleColliderFactory factory;
	
	void Start () {
	}
	
	void Update () {
		ActionUpdate();
		DestroyCheck();
	}
	
	private void ActionUpdate(){
		action.Update();
	}
	
	public void Initialize(Transform parent, Vector3 pos, string kind){
		SubInitializeAboutCollider(kind);
		SubInitializeAboutProperties(kind);
		SubInitializeAboutTransforms(parent, pos, kind);
		SubInitializeAboutAction(kind);
	}
	
	private void SubInitializeAboutProperties(string kind){
		switch(kind){	
			case "KIND1":
				velocity = new Vector3(0, 0, 0);
				sprite = GameObject.Instantiate(spritePrefabs) as SingleSprite_;
				sprite.transform.parent = transform;
				sprite.Initialize("Obstacle_00");
			break;
			
			case "STALACTITE":
				velocity = new Vector3(0, 0, 0);
				sprite = GameObject.Instantiate(spritePrefabs) as SingleSprite_;
				sprite.transform.parent = transform;
				sprite.Initialize("Obstacle_01");
			break;
				
			case "STALAGMITE":
				velocity = new Vector3(0, 0, 0);
				sprite = GameObject.Instantiate(spritePrefabs) as SingleSprite_;
				sprite.transform.parent = transform;
				sprite.Initialize("Obstacle_02");
			break;
		}
	}
	
	private void SubInitializeAboutTransforms(Transform parent, Vector3 pos, string kind){
		transform.parent = parent;
		transform.localPosition = pos;
		
		switch(kind){
			case "KIND1":
				transform.localPosition += new Vector3(sprite.Width() / 2, sprite.Height() / 2, -1.0f);
			break;
			
			case "STALACTITE":
				transform.localPosition += new Vector3(sprite.Width() / 2, -sprite.Height(), -1.0f);
			break;
			
			case "STALAGMITE":
				transform.localPosition += new Vector3(sprite.Width() / 2, sprite.Height() / 2, -1.0f);
			break;
		}
		
	}
	
	public void SubInitializeAboutCollider(string kind){
		factory = GetComponent<ObstacleColliderFactory>();
		tk2dSprite col;
		switch(kind){
			case "KIND1":
				col = factory.MakeCollider(0);
				col.transform.parent = transform;
			break;
			
			case "STALACTITE":
				col = factory.MakeCollider(1);
				col.transform.parent = transform;
			break;
			
			case "STALAGMITE":
				col = factory.MakeCollider(2);
				col.transform.parent = transform;
			break;
		}
	}
	
	private void SubInitializeAboutAction(string kind){
		switch(kind){	
			case "KIND1":
				action = new Kind1_Action_(this);
			break;
			
			case "STALACTITE":
				action = new Kind1_Action_(this);
			break;
			
			case "STALAGMITE":
				action = new Kind1_Action_(this);
			break;
		}
	}
	
	private void DestroyCheck(){
		Vector3 worldPos = transform.TransformPoint(transform.position);
		if(worldPos.x < -sprite.Width()){
			DestroyItself();
		}
	}
	
	private void DestroyItself(){
		Destroy(this.gameObject);
	}
	
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Player")
		{
			PlayerFish.Instance.Bumped();
		}
	}
}
