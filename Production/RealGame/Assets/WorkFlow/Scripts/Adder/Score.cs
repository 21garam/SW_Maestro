using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	public tk2dTextMesh score;
	
	public int score_int;
	// Use this for initialization
	void Start () {
		score_int = SharedData.Instance.score;
		score.text = score_int.ToString();
	}
}
