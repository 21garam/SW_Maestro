using UnityEngine;
using System.Collections;

public class StateHelper_ {
	public static void DisabledOptionBtr(){
		GameObject optionBtr = GameObject.FindGameObjectWithTag("OptionBtr");
		Helper__.SetLayer(optionBtr, "DisalbedUI");	
	}
	
	public static void PlayerDie(){
		GameObject[] feedFishArr = GameObject.FindGameObjectsWithTag("FeedFish");
		if(feedFishArr != null){
			for(int i = 0; i < feedFishArr.Length; i++)
				feedFishArr[i].GetComponent<MonoBehaviour>().enabled = false;
		}
		else{
			Debug.Log("StateHelper_ : particle tag null");
		}
		
		GameObject[] enemyFishArr = GameObject.FindGameObjectsWithTag("EnemyFish");
		for(int i = 0; i < enemyFishArr.Length; i++)
			enemyFishArr[i].GetComponent<MonoBehaviour>().enabled = false;
		
		GameObject[] obstacleArr = GameObject.FindGameObjectsWithTag("Obstacle");
		for(int i = 0; i < obstacleArr.Length; i++)
			obstacleArr[i].GetComponent<MonoBehaviour>().enabled = false;
		
		GameObject background = GameObject.FindGameObjectWithTag("Background");
		background.GetComponent<MonoBehaviour>().enabled = false;
		
		GameObject gm = GameObject.FindGameObjectWithTag("GM");
		gm.GetComponent<MonoBehaviour>().enabled = false;
		
		GameObject sp = GameObject.FindGameObjectWithTag("SP");
		sp.GetComponent<Spawner>().enabled = false;
		
		GameObject[] guiArr = GameObject.FindGameObjectsWithTag("GUI");
		for(int i = 0; i < guiArr.Length; i++)
			guiArr[i].GetComponent<MonoBehaviour>().enabled = false;
		
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		player.GetComponent<PlayerFish>().Stop = true;
	}
	
	public static void Pause(){
		GameObject particle = GameObject.FindGameObjectWithTag("Particle");
		if(particle != null){
			particle.GetComponent<Particle_>().SetEnable(false);
		}
		else{
			Debug.Log("StateHelper_ : particle tag null");
		}
		GameObject[] feedFishArr = GameObject.FindGameObjectsWithTag("FeedFish");
		if(feedFishArr != null){
			for(int i = 0; i < feedFishArr.Length; i++)
				feedFishArr[i].GetComponent<MonoBehaviour>().enabled = false;
		}
		else{
			Debug.Log("StateHelper_ : particle tag null");
		}
		
		GameObject[] enemyFishArr = GameObject.FindGameObjectsWithTag("EnemyFish");
		for(int i = 0; i < enemyFishArr.Length; i++)
			enemyFishArr[i].GetComponent<MonoBehaviour>().enabled = false;
		
		GameObject[] obstacleArr = GameObject.FindGameObjectsWithTag("Obstacle");
		for(int i = 0; i < obstacleArr.Length; i++)
			obstacleArr[i].GetComponent<MonoBehaviour>().enabled = false;
		
		GameObject background = GameObject.FindGameObjectWithTag("Background");
		background.GetComponent<MonoBehaviour>().enabled = false;
		
		GameObject gm = GameObject.FindGameObjectWithTag("GM");
		gm.GetComponent<MonoBehaviour>().enabled = false;
		
		GameObject sp = GameObject.FindGameObjectWithTag("SP");
		sp.GetComponent<Spawner>().enabled = false;
		
		GameObject[] guiArr = GameObject.FindGameObjectsWithTag("GUI");
		for(int i = 0; i < guiArr.Length; i++)
			guiArr[i].GetComponent<MonoBehaviour>().enabled = false;
		
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		player.GetComponent<PlayerFish>().Stop = true;
	}
	
	public static void Resume(){
		GameObject particle = GameObject.FindGameObjectWithTag("Particle");
		particle.GetComponent<Particle_>().SetEnable(true);
		
		GameObject[] feedFishArr = GameObject.FindGameObjectsWithTag("FeedFish");
		for(int i = 0; i < feedFishArr.Length; i++)
			feedFishArr[i].GetComponent<MonoBehaviour>().enabled = true;
		
		GameObject[] enemyFishArr = GameObject.FindGameObjectsWithTag("EnemyFish");
		for(int i = 0; i < enemyFishArr.Length; i++)
			enemyFishArr[i].GetComponent<MonoBehaviour>().enabled = true;
		
		GameObject[] obstacleArr = GameObject.FindGameObjectsWithTag("Obstacle");
		for(int i = 0; i < obstacleArr.Length; i++)
			obstacleArr[i].GetComponent<MonoBehaviour>().enabled = true;
		
		GameObject background = GameObject.FindGameObjectWithTag("Background");
		background.GetComponent<MonoBehaviour>().enabled = true;
		
		GameObject gm = GameObject.FindGameObjectWithTag("GM");
		gm.GetComponent<MonoBehaviour>().enabled = true;
		
		GameObject sp = GameObject.FindGameObjectWithTag("SP");
		sp.GetComponent<Spawner>().enabled = true;
		
		GameObject[] guiArr = GameObject.FindGameObjectsWithTag("GUI");
		for(int i = 0; i < guiArr.Length; i++)
			guiArr[i].GetComponent<MonoBehaviour>().enabled = true;
		
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		player.GetComponent<PlayerFish>().Stop = false;
	}
}
