using UnityEngine;
using System.Collections;

public class ObstacleCollierFactory_ : MonoBehaviour {
	public tk2dSprite obstacle00Prefabs;
	
	public tk2dSprite MakeCollider(int number){
		tk2dSprite retSprite = null;
		switch(number){
			case 0:
				retSprite = GameObject.Instantiate(obstacle00Prefabs) as tk2dSprite;
			break;
		}
		return retSprite;
	}
}
