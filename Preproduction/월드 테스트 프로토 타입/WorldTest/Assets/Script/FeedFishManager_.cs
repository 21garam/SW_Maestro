using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FeedFishManager_ : MonoBehaviour {
	public FeedFish_ m_feedFish;
	public Backgound_ m_backGround;
	//public Dictionary<Vector2, FeedFish_> feedFishDic = new Dictionary<Vector2, FeedFish_>();
	public List<FeedFish_> feedFishList = new List<FeedFish_>();
	// Use this for initialization
	void Start () {
		Start_CreateFeedFishes();
	}
	
	void Start_CreateFeedFishes(){
		int wShfit = 3;
		int hShfit = 3;
		for(int j = 0; j < m_backGround.TileHeight; j+=hShfit){
			for(int i = 0; i < m_backGround.TileWidth; i+=wShfit){
				if(i == 0 && j == 0){
					continue;
				}
				float dir = Random.value - 0.5f;
				float xPadding;
				if(dir >= 0)
					xPadding = -FeedFish_.MOVE_LENTH * 0.5f;
				else
					xPadding = +FeedFish_.MOVE_LENTH * 0.5f;
				
				FeedFish_ fish = GameObject.Instantiate(m_feedFish, 
					new Vector3(32.0f + m_backGround.GetTilePosX(i) + xPadding, m_backGround.GetTilePosY(j), transform.position.z), transform.rotation) as FeedFish_;
				fish.Initialize(dir, i, j);
				feedFishList.Add(fish);
				//feedFishDic.Add(new Vector2(i, j), fish);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
