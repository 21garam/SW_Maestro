using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
	public FeedFish FeedFishPrefab;
	public EnemyFish_ enemyFishPrefab;
	public Obstacle obstaclePrefab;	
	
	Vector2 screenSize;
	
	void Start () {
	}
	
	public void Initialize(float screenWidth, float screenHeight){
		screenSize = new Vector2(screenWidth, screenHeight);
	}
	
	public void Spwan(Event_ eve){
		switch(eve.m_event){
			case "SpawnFeedFish":
				SpawnFeedFish(eve);
			break;	
		
			case "SpawnEnemyFish":
				SpawnEnemyFish(eve);
			break;
						
			case "SpawnObstacle":
				SpawnObstacle(eve);
			break;
			
			case "EndOfChunk":
			break;
		}
	}
	
	private void SpawnFeedFish(Event_ eve){
		string[] words = eve.m_param.Split(' ');
		float h = (float)Helper_.ConvertStringtoFloat(words[0]);
		int size = Helper_.ConvertStringtoInt(words[1]);
		float coolTime = (float)Helper_.ConvertStringtoFloat(words[2]); 
		string kind = words[3];	
		StartCoroutine(SpawnFeedFish(new Vector3(screenSize.x, screenSize.y * h, 0), size, coolTime, kind));
	}
	
	private void SpawnEnemyFish(Event_ eve){
		string[] words = eve.m_param.Split(' ');
		float h = (float)Helper_.ConvertStringtoFloat(words[0]);
		int size = Helper_.ConvertStringtoInt(words[1]);
		float coolTime = (float)Helper_.ConvertStringtoFloat(words[2]); 
		string kind = words[3];	
		StartCoroutine(SpawnEnemyFish(new Vector3(screenSize.x, screenSize.y * h, 0), size, coolTime, kind));
	}
	
	private void SpawnObstacle(Event_ eve){
		string[] words = eve.m_param.Split(' ');
		float h = (float)Helper_.ConvertStringtoFloat(words[0]);
		int size = Helper_.ConvertStringtoInt(words[1]);
		float coolTime = (float)Helper_.ConvertStringtoFloat(words[2]); 
		string kind = words[3];	
		StartCoroutine(SpawnObstacle(new Vector3(screenSize.x, screenSize.y * h, 0), size, coolTime, kind));
	}
	
	private IEnumerator SpawnFeedFish(Vector3 pos, int size, float coolTime, string kind){
		for(int i = 0; i < size; i++){
			if(!enabled){
				i--;
				yield return 0;
			}else{
				FeedFish chd = GameObject.Instantiate(FeedFishPrefab) as FeedFish;
				chd.Initialize(transform, pos, kind);
				yield return new WaitForSeconds(coolTime);
			}
		}
	}
	
	private IEnumerator SpawnEnemyFish(Vector3 pos, int size, float coolTime, string kind){
		for(int i = 0; i < size; i++){
			if(!enabled){
				i--;
				yield return 0;
			}else{
				EnemyFish_ chd = GameObject.Instantiate(enemyFishPrefab) as EnemyFish_;
				chd.Initialize(transform, pos, kind);
				yield return new WaitForSeconds(coolTime);
			}
		}
	}
	
	private IEnumerator SpawnObstacle(Vector3 pos, int size, float coolTime, string kind){
		for(int i = 0; i < size; i++){
			if(!enabled){
				i--;
				yield return 0;
			}else{
				Obstacle chd = GameObject.Instantiate(obstaclePrefab) as Obstacle;
				chd.Initialize(transform, pos, kind);
				yield return new WaitForSeconds(coolTime);
			}
		}
	}
}
