using UnityEngine;
using System.Collections;

public class BGM_ : MonoBehaviour {
public AudioSource audioSrc;
	public static bool bgm_enabled = true;
	
	public void Stop(){
		audioSrc.Stop();
	}
	
	public void Play(){
		if(bgm_enabled)
			audioSrc.Play();
	}
	
	public static void StopBGM(){
		GameObject gm = GameObject.FindGameObjectWithTag("MainCamera");
		BGM_ bgm = gm.GetComponent<BGM_>();
		bgm.Stop();
	}
	
	public static void PlayBGM(){
		GameObject gm = GameObject.FindGameObjectWithTag("MainCamera");
		BGM_ bgm = gm.GetComponent<BGM_>();
		bgm.Play();
	}
}
