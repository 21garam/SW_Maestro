using UnityEngine;
using System.Collections;

public class BackGround : MonoBehaviour {
	
	void Start () {
	
	}
	
	void Update () {
		tk2dSprite sprite = GetComponent<tk2dSprite>();
		sprite.transform.position = new Vector3(Player.Instance.playerPosition.x/5, Player.Instance.playerPosition.y/20, 20);
	}
}
