using UnityEngine;
using System.Collections;

public class TerrainPartFactory_ : MonoBehaviour {
	public tk2dSprite m_t_OuterPartPrefab;
	public tk2dSprite m_t_InnerPartPrefab;
	public Backgound_ m_backGround;

	// Use this for initialization
	//void Start () {
	
	//}
	
	// Update is called once per frame
	//void Update () {
	
	//}
	
	public void MakeTerrains(int leftRowPos, int downColPos, int width, int height){
		if(!(width > 0 && height > 0)){
			Debug.Log("Warring : (Width || Hegith) < 1");
			return;
		}
		
		float partW = m_t_OuterPartPrefab.GetUntrimmedBounds().size.x;	
		float partH = m_t_OuterPartPrefab.GetUntrimmedBounds().size.y;	
		
		//for(int i = 0; i < width; i++){
		//	for(int j = 0; j < height; j++){
		//		GameObject.Instantiate(m_t_OuterPartPrefab, 
	//				new Vector3(m_backGround.GetTilePosX(leftRowPos + i), m_backGround.GetTilePosY(downColPos), transform.position.z),
	//				transform.rotation);
	//		}
	//	}
		for(int i = 0; i < width; i++)
			GameObject.Instantiate(m_t_OuterPartPrefab, 
						new Vector3(m_backGround.GetTilePosX(leftRowPos + i), m_backGround.GetTilePosY(downColPos), transform.position.z),
						transform.rotation);
		
		if(height > 1)
		for(int i = 0; i < width; i++)
			GameObject.Instantiate(m_t_OuterPartPrefab, 
						new Vector3(m_backGround.GetTilePosX(leftRowPos + i), m_backGround.GetTilePosY(downColPos + height-1), transform.position.z),
					transform.rotation);
		
		
		if(height > 2){
			for(int j = 0; j < height-2; j++){
				GameObject.Instantiate(m_t_OuterPartPrefab, 
									new Vector3(m_backGround.GetTilePosX(leftRowPos), m_backGround.GetTilePosY(downColPos + j + 1), transform.position.z),
									transform.rotation);
				for(int i = 0; i < width-2; i++){
					GameObject.Instantiate(m_t_InnerPartPrefab, 
									new Vector3(m_backGround.GetTilePosX(leftRowPos + i + 1), m_backGround.GetTilePosY(downColPos + j + 1), transform.position.z),
									transform.rotation);
				}
				GameObject.Instantiate(m_t_OuterPartPrefab, 
									new Vector3(m_backGround.GetTilePosX(leftRowPos + width - 1), m_backGround.GetTilePosY(downColPos + j + 1), transform.position.z),
									transform.rotation);
			}
		}
	}
}
