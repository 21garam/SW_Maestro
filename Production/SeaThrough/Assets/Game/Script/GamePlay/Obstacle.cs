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
	string thisKind = "";
	
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
		thisKind = kind;
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
				sprite.Initialize("Obstacle_01");
			break;
			
			case "STALACTITE":
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
				transform.localPosition += new Vector3(sprite.Width() / 2, sprite.Height() / 2 - Random.Range(3, 9) * sprite.Height()/50, -1.0f);
			break;
			
			case "STALACTITE":
				transform.localPosition += new Vector3(sprite.Width() / 2, -sprite.Height() / 2 + Random.Range(3, 9) * sprite.Height()/50, -1.0f);
			break;
		}
	}
	
	public void SubInitializeAboutCollider(string kind){
		factory = GetComponent<ObstacleColliderFactory>();
		tk2dSprite col;
		switch(kind){
			case "KIND1":
				col = factory.MakeCollider(1);
				col.transform.parent = transform;
			break;
			
			case "STALACTITE":
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
	
	bool isTriggered = false;
	void OnTriggerEnter(Collider col)
	{
		if(isTriggered)
			return;
		if(col.gameObject.tag == "Player"){
			if(PlayerFish.Instance.Stop)
				return;
			PlayerFish.Instance.Bumped();
			isTriggered = true;
			if(PlayerFish.Instance.FeverTime < 1){
				Sound_.PlaySound("down");
				switch(thisKind){	
				case "KIND1":
					BreakDown();
				break;
				
				case "STALACTITE":
					BreakUp();
				break;
				
				case "STALAGMITE":
					BreakUp();
				break;
				}
			}
			else{
				Sound_.PlaySound("fall");
				switch(thisKind){	
				case "KIND1":
					FlyDown();
				break;
				
				case "STALACTITE":
					FlyUp();
				break;
				
				case "STALAGMITE":
					FlyUp();
				break;
				}
			}
		}
	}
	
	void BreakDown(){
		StartCoroutine(Effect_.Obstacle_BreakDown(transform, 1, new Vector3(0, 0, -45)));
	}
	
	void BreakUp(){
		StartCoroutine(Effect_.Obstacle_BreakUp(transform, 1, new Vector3(0, 0, 45)));
	}
	
	void FlyUp(){
		Rolling("UP");
		Out("UP");
	}
	
	void FlyDown(){
		Rolling("DOWN");
		Out("DOWN");
	}
	
	void Rolling(string str){
		switch(str){
			case "UP":
				StartCoroutine(Effect_.Roatate(transform, 1, new Vector3(0, 0, 45)));
			break;
			
			case "DOWN":
				StartCoroutine(Effect_.Roatate(transform, 1, new Vector3(0, 0, -45)));
			break;
		}
	}
	
	void Out(string str){
		switch(str){
			case "UP":
			StartCoroutine(Animation_.TransformAToB(transform, 1.3f, new Vector3(960 + sprite.Width(), 840, transform.position.z), DestroyItself));
			break;
			
			case "DOWN":
			StartCoroutine(Animation_.TransformAToB(transform, 1.3f, new Vector3(960 + sprite.Width(), -200,transform.position.z), DestroyItself));
			break;
		}
	}
}
