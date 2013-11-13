﻿using UnityEngine;
using System.Collections;

public class Animation_ {
	public delegate void CallBackPtr();
	public static IEnumerator LerpColorAToB(tk2dSprite sprite, float time, Color targetColor){
		Color orinColor = sprite.color;
		for(float t = 0; t < time; t += tk2dUITime.deltaTime){
			float nt = Mathf.Clamp01( t / time );
			nt = Mathf.Sin(nt * Mathf.PI * 0.5f);
			sprite.color = Color.Lerp(orinColor, targetColor, nt);
			yield return 0;
		}
		sprite.color = targetColor;
	}
	
	public static IEnumerator ScaleAToB(Transform trans, float time, Vector3 targetScale){
		Vector3 orinScale = trans.localScale;
		for(float t = 0; t < time; t += tk2dUITime.deltaTime){
            float nt = Mathf.Clamp01( t / time );
			nt = Mathf.Sin(nt * Mathf.PI * 0.5f);
			trans.localScale = Vector3.Lerp(orinScale, targetScale, nt);
            yield return 0;
        }
		trans.localScale = targetScale;
	}
	
	public static IEnumerator ScaleAToB(Transform trans, float time, Vector3 targetScale, CallBackPtr callbackPtr){
		Vector3 orinScale = trans.localScale;
		for(float t = 0; t < time; t += tk2dUITime.deltaTime){
            float nt = Mathf.Clamp01( t / time );
			nt = Mathf.Sin(nt * Mathf.PI * 0.5f);
			trans.localScale = Vector3.Lerp(orinScale, targetScale, nt);
            yield return 0;
        }
		trans.localScale = targetScale;
		callbackPtr();
	}
	
	public static IEnumerator TransformAToB(Transform trans, float time, Vector3 targetPos){
		Vector3 orinPos = trans.localPosition;
        for(float t = 0; t < time; t += tk2dUITime.deltaTime){
            float nt = Mathf.Clamp01( t / time );
			nt = Mathf.Sin(nt * Mathf.PI * 0.5f);
			trans.localPosition = Vector3.Lerp(orinPos, targetPos, nt);
          	yield return 0;
        }
		trans.localPosition = targetPos;
	}
	
	public static IEnumerator TransformAToB(Transform trans, float time, Vector3 targetPos, CallBackPtr callbackPtr){
		Vector3 orinPos = trans.localPosition;
        for(float t = 0; t < time; t += tk2dUITime.deltaTime){
            float nt = Mathf.Clamp01( t / time );
			nt = Mathf.Sin(nt * Mathf.PI * 0.5f);
			trans.localPosition = Vector3.Lerp(orinPos, targetPos, nt);
          	yield return 0;
        }
		trans.localPosition = targetPos;
		callbackPtr();
	}
}
