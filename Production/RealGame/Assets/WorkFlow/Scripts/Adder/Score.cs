using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	public tk2dTextMesh score;
	
	public WWW_ www;
	
	int score_int;
	// Use this for initialization
	void Start () {
		score_int = SharedData.Instance.score;
		score.text = score_int.ToString();
		if(GUI_Setting_.PLAYER_ID != ""){
			//Debug.Log("ID : " + GUI_Setting_.PLAYER_ID);
			www.UpdateAccount(GUI_Setting_.PLAYER_ID, null, score_int);
		}
	}
}
