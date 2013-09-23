using UnityEngine;
using System.Collections;

public class TerrainManager_ : MonoBehaviour {
	public TerrainPartFactory_ m_t_factory;
	public Backgound_ m_backGround;
	
	//Random rand = new Random();
	
	// Use this for initialization
	void Start () {
		Start_MakeRandomTerrain();
		//m_t_factory.MakeTerrains(0, 0, 3, 5);
	}
	
	void Start_MakeRandomTerrain(){
		int rowSize = 4;
		int colSize = 2;
		int tileMaxW = m_backGround.TileWidth / rowSize;
		int tileMaxH = m_backGround.TileHeight / colSize;
		//Debug.Log(rowSize);
		//Debug.Log(colSize);
		for(int i = 0; i < rowSize; i++){
			for(int j = 0; j < colSize; j++){
				int randW = Random.Range(tileMaxW / 4, tileMaxW / 2);
				int randH = Random.Range(tileMaxH / 4, tileMaxH / 2);
				m_t_factory.MakeTerrains(i * 8 + randW, 
										 1 + j * 8 + randH, 
					(tileMaxW / 2 + 1) - randW , (tileMaxH / 2 + 1) - randH);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
