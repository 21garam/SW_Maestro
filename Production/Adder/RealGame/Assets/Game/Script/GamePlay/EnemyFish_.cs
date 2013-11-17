using UnityEngine;
using System.Collections;

public class EnemyFish_ : MonoBehaviour {
	class Kind1_Action_ : Action_{
		EnemyFish_ me;
		public Kind1_Action_(EnemyFish_ me_){
			this.me = me_;
		}
		
		public void Update(){
			Strategy_.GoStraight(me.gameObject, me.velocity);		
		}
	}
	
	class Kind2_Action_ : Action_{
		EnemyFish_ me;
		Vector2 strPos;
		Vector2 signSize;
		float time;
		public Kind2_Action_(EnemyFish_ me_){
			this.me = me_;
			time = 0;
			strPos = new Vector2(me_.gameObject.transform.localPosition.x,
								me_.gameObject.transform.localPosition.y);
			signSize = new Vector2(100, 100);
		}
		
		public void Update(){
			time += Time.deltaTime;
			Strategy_.GoSinePattern(me.gameObject, signSize, strPos, me.velocity.x, time);
		}
	}
	
	//public SingleSprite_ spritePrefabs;
	public tk2dSprite sprite;
	Vector3 velocity;
	Action_ action;
	SphereCollider col;
	
	void Start () {
	}
	
	void Update () {
		action.Update();
		DestroyCheck();
	}
	
	public void Initialize(Transform parent, Vector3 pos, string kind){//EnemyFish_.Kind kind){
		SubInitializeAboutProperties(kind);
		transform.parent = parent;
		transform.localPosition = pos;
		transform.localPosition += new Vector3(sprite.GetUntrimmedBounds().size.x / 2, 0, -2f);
		SubInitializeAboutAction(kind);
	}
	
	private void SubInitializeAboutProperties(string kind){
		switch(kind){	
			case "KIND1":
				velocity = new Vector3(-200.0f, 0, 0);
				//sprite = GameObject.Instantiate(spritePrefabs) as SingleSprite_;
				sprite.transform.parent = transform;
				//sprite.Initialize("EnemyFish");
				col = transform.collider as SphereCollider;
				col.radius = sprite.GetUntrimmedBounds().size.x / 2;
			break;
				
			case "KIND2":
				velocity = new Vector3(-150.0f, 0, 0);
				//sprite = GameObject.Instantiate(spritePrefabs) as SingleSprite_;
				sprite.transform.parent = transform;
				//sprite.Initialize("EnemyFish");
				col = transform.collider as SphereCollider;
				col.radius = sprite.GetUntrimmedBounds().size.x / 2;
			break;
		}
	}
	
	private void SubInitializeAboutAction(string kind){
		switch(kind){	
			case "KIND1":
				action = new Kind1_Action_(this);
			break;
				
			case "KIND2":
				action = new Kind2_Action_(this);
			break;
		}
	}
	
	public void DestroyCheck(){
		Vector3 worldPos = transform.TransformPoint(transform.position);
		if(worldPos.x < -sprite.GetUntrimmedBounds().size.x){
			DestroyItself();
		}
	}
	
	public void DestroyItself(){
		Destroy(this.gameObject);
	}
	
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Player")
		{
			PlayerFish.Instance.Bumped();
			if(PlayerFish.Instance.FeverTime > 0){
				Sound_.PlaySound("fall");
				int rand = Random.Range(0, 1000) % 2;
				if((rand & 1) == 0){
					FlyUp();
				}
				else{
					FlyDown();	
				}
			}
		}
	}
	
	void FlyUp(){
		Rolling();
		Out("UP");
	}
	
	void FlyDown(){
		Rolling();
		Out("DOWN");
	}
	
	void Rolling(){
		StartCoroutine(Effect_.Roatate(transform, 1, new Vector3(0, 0, 720)));
	}
	
	void Out(string str){
		switch(str){
			case "UP":
			StartCoroutine(Animation_.TransformAToB(transform, 1, new Vector3(1000, 640,transform.position.z), DestroyItself));
			break;
			
			case "DOWN":
			StartCoroutine(Animation_.TransformAToB(transform, 1, new Vector3(1000, 0,transform.position.z), DestroyItself));
			break;
		}
	}
}
