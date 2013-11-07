using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner_ : MonoBehaviour {
	public EnemyFish_ enemyFishPrefab;
	public Obstacle_ obstaclePrefab;
	
	Vector2 screenSize;
	
	void Start () {
	}
	
	public void Initialize(float screenWidth, float screenHeight){
		screenSize = new Vector2(screenWidth, screenHeight);
	}
	
	public void Spwan(Event_ eve){
		switch(eve.m_event){
			case "EnemyFishSpawn":
				EnemyFishSpawn(eve);
			break;
			
			case "ObstacelSpawn":
				ObstacleSpawn(eve);
			break;
			
			case "null":
				Debug.Log("null");
			break;
		}
	}
	
	private void EnemyFishSpawn(Event_ eve){
		string[] words = eve.m_param.Split(' ');
		float h = (float)Helper_.ConvertStringtoFloat(words[0]);
		int size = Helper_.ConvertStringtoInt(words[1]);
		float coolTime = (float)Helper_.ConvertStringtoFloat(words[2]); 
		string kind = words[3];	
		StartCoroutine(EnemyFishSpawn(new Vector3(screenSize.x, screenSize.y * h, 0), size, coolTime, kind));
	}
	
	private void ObstacleSpawn(Event_ eve){
		string[] words = eve.m_param.Split(' ');
		float h = (float)Helper_.ConvertStringtoFloat(words[0]);
		int size = Helper_.ConvertStringtoInt(words[1]);
		float coolTime = (float)Helper_.ConvertStringtoFloat(words[2]); 
		string kind = words[3];	
		StartCoroutine(ObstacleSpawn(new Vector3(screenSize.x, screenSize.y * h, 0), size, coolTime, kind));
	}
	
	private IEnumerator EnemyFishSpawn(Vector3 pos, int size, float coolTime, string kind){
		for(int i = 0; i < size; i++){
			EnemyFish_ chd = GameObject.Instantiate(enemyFishPrefab) as EnemyFish_;
			chd.Initialize(transform, pos, kind);
			yield return new WaitForSeconds(coolTime);
		}
	}
	
	private IEnumerator ObstacleSpawn(Vector3 pos, int size, float coolTime, string kind){
		for(int i = 0; i < size; i++){
			Obstacle_ chd = GameObject.Instantiate(obstaclePrefab) as Obstacle_;
			chd.Initialize(transform, pos, kind);
			yield return new WaitForSeconds(coolTime);
		}
	}
}
