using UnityEngine;
using System.Collections;

public class Sound_ : MonoBehaviour {
	public AudioClip boubble;
	public AudioClip down;
	public AudioClip fall;
	public AudioClip click;
	public AudioSource audioSrc;
	public static bool sound_enabled = true;
	
	private void Play(string str){
		if(!sound_enabled)
			return;
		switch(str){
			case "boubble":
				audioSrc.PlayOneShot(boubble);
			break;
			
			case "down":
				audioSrc.PlayOneShot(down);
			break;
			
			case "fall":
				audioSrc.PlayOneShot(fall);
			break;
			
			case "click":
				audioSrc.PlayOneShot(click);
			break;
			
			default:
				audioSrc.PlayOneShot(boubble);
			break;
		}
	}
	
	public static void PlaySound(string str){
		GameObject gm = GameObject.FindGameObjectWithTag("Sound");
		Sound_ sound = gm.GetComponent<Sound_>();
		sound.Play(str);
	}
}
