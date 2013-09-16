using UnityEngine;
using System.Collections;

public class MainGame_ : MonoBehaviour {
	public Player_ m_player;
	
	// Use this for initialization
	void Start () {
		Enemy0_.m_player = m_player;
		//m_player.transform.position = new Vector3(m_cam.NativeResolution.x * 0.5f, m_cam.NativeResolution.y * 0.5f, m_player.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
