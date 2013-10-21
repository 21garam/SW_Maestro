using UnityEngine;
using System.Collections;

public class BackGround : MonoBehaviour {
	
	float deltaTime = 0;
	
	void Start () {
	
	}
	
	void Update () {
		tk2dSprite sprite = GetComponent<tk2dSprite>();
		deltaTime -= Time.deltaTime;
		sprite.transform.position = new Vector3(deltaTime*10, PlayerFish.Instance.playerPosition.y/40, 20);
	}
}
