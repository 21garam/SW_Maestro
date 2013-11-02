using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {
	public float velocity;
	
	private float default_y;
	
	public tk2dSprite mSprite;

	void Awake()
	{
		velocity = 100;
	}
	
	void Start () {
		default_y = mSprite.transform.position.y;
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
			PlayerFish.Instance.Bumped();
		}
	}
	
	void Relocation()
	{
		Vector2 v = PlayerFish.Instance.playerPosition;
		
		mSprite.transform.position= new Vector3(mSprite.transform.position.x+3240.0f, mSprite.transform.position.y, mSprite.transform.position.z);
	}
}
