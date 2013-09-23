using UnityEngine;
using System.Collections;

public class Backgound_ : MonoBehaviour {
	public tk2dSprite m_sprite;
	public tk2dTextMesh m_tileCoordTextPrefab;
	MyAABB_ m_bound = new MyAABB_();
	float tilePixelsPerMeter = 64.0f;
	
	static int tileWidth;
	static int tileHeight;
	
	//Vector2[,] tilePos;
	
	void Awake(){
		m_bound.left = m_sprite.GetBounds().min.x;
		m_bound.right = m_sprite.GetBounds().max.x;
		m_bound.up = m_sprite.GetBounds().max.y;
		m_bound.down = m_sprite.GetBounds().min.y;
		
		tileWidth = (int)(m_bound.right / tilePixelsPerMeter);
		tileHeight = (int)(m_bound.up / tilePixelsPerMeter);
	}
	
	void Start () {
		//Start_CoordText();
	}
	
	void Start_CoordText(){
		for(int i = 0; i < tileWidth; i++){
			for(int j = 0; j < tileHeight; j++){
				tk2dTextMesh coordText = GameObject.Instantiate(m_tileCoordTextPrefab,
					new Vector3(i * tilePixelsPerMeter + tilePixelsPerMeter * 0.5f, j * tilePixelsPerMeter + tilePixelsPerMeter * 0.5f, transform.position.z), 
					transform.rotation) as tk2dTextMesh;
				coordText.text = "(" + i.ToString() + ", " + j.ToString() + ")";
				coordText.Commit();
				//Debug.Log(coordText.text);
			}
		}
	}
	
	public int TileWidth{
		get{ return tileWidth; }
	}
	
	public int TileHeight{
		get{ return tileHeight; }
	}
	
	public static int GetTileWidth(){
		return tileWidth;
	}
	
	public static int GetTileHeight(){
		return tileHeight;
	}
	
	void Update () {
		
	}
	
	public MyAABB_ GetBound(){
		return m_bound;
	}
	
	void OnDrawGizmos(){
     	Gizmos.color = Color.green;
		Vector2 rayPos = new Vector2();
		for(int i = 0; i < tileHeight; i++){
			Gizmos.DrawRay(new Vector3(rayPos.x, rayPos.y + tilePixelsPerMeter * i, 0), transform.right * m_bound.right);
		}
		
		rayPos.Set(0, 0);
		for(int i = 0; i < tileWidth; i++){
			Gizmos.DrawRay(new Vector3(rayPos.x + tilePixelsPerMeter * i, rayPos.y, 0), transform.up * m_bound.up);
		}
	}
	
	public float GetTilePosX(int row){
		return tilePixelsPerMeter * row + tilePixelsPerMeter * 0.5f;
	}
	
	public float GetTilePosY(int col){
		return tilePixelsPerMeter * col + tilePixelsPerMeter * 0.5f;
	}
	
	public int GetTileGridX(float posX){
		return (int)((posX) / tilePixelsPerMeter);
	}
	
	public int GetTileGridY(float posY){
		return (int)((posY) / tilePixelsPerMeter);
	}
	
	//public float GetTileRowID(float x){
	//}
	
	//public float GetTileColID(float y){
	//}
}
