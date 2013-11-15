using UnityEngine;
using System.Collections;

public class ObstacleColliderFactory : MonoBehaviour {
	public tk2dSprite obstacle00Prefabs;
	public tk2dSprite obstacle01Prefabs;
	public tk2dSprite obstacle02Prefabs;
	
	public tk2dSprite MakeCollider(int number){
		tk2dSprite retSprite = null;
		switch(number){
			case 0:
				retSprite = GameObject.Instantiate(obstacle00Prefabs) as tk2dSprite;
			break;
			case 1:
				retSprite = GameObject.Instantiate(obstacle01Prefabs) as tk2dSprite;
			break;
			case 2:
				retSprite = GameObject.Instantiate(obstacle02Prefabs) as tk2dSprite;
			break;
		}
		return retSprite;
	}
}
