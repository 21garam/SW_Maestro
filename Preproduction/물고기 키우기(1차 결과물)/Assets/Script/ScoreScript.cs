using UnityEngine;
using System.Collections;

public class ScoreScript : MonoBehaviour {
	static private tk2dTextMesh textMesh;
	static private int score;
	
	static public int Score
	{
		get
		{
			return score;
		}
		
		set
		{
			score = value;
		}
	}
	
	void Start () {
		score = 0;
		textMesh = GetComponent<tk2dTextMesh>();
	}
	
	void Update () {
		textMesh.text = string.Format("Score: {0}", score);
		textMesh.Commit();
	}
}
