using UnityEngine;
using System.Collections;

public class Effect_ {
	public delegate void CallBackPtr();
	public static IEnumerator Roatate(Transform trans, float time, Vector3 targetRoatate, CallBackPtr callbackPtr = null){
		Vector3 orinRotate = trans.localEulerAngles;
		for(float t = 0; t < time; t += tk2dUITime.deltaTime){
			float nt = Mathf.Clamp01( t / time );
			nt = Mathf.Sin(nt * Mathf.PI * 0.5f);
			trans.localEulerAngles = Vector3.Lerp(orinRotate, targetRoatate, nt);
			//Debug.Log(trans.localEulerAngles);
			yield return 0;
		}
		trans.localEulerAngles = targetRoatate;
		if(callbackPtr != null)
			callbackPtr();
	}
	
	public static IEnumerator Obstacle_BreakDown(Transform trans, float time, Vector3 targetRoatate, CallBackPtr callbackPtr = null){
		Vector3 orinRotate = trans.localEulerAngles;
		for(float t = 0; t < time; t += tk2dUITime.deltaTime){
			float nt = Mathf.Clamp01( t / time );
			nt = Mathf.Sin(nt * Mathf.PI * 0.5f);
			trans.localPosition -= new Vector3(0, 2, 0);
			trans.localEulerAngles = Vector3.Lerp(orinRotate, targetRoatate, nt);
			trans.localPosition += new Vector3(0, -2, 0);
			yield return 0;
		}
		trans.localEulerAngles = targetRoatate;
		if(callbackPtr != null)
			callbackPtr();
	}
	
	public static IEnumerator Obstacle_BreakUp(Transform trans, float time, Vector3 targetRoatate, CallBackPtr callbackPtr = null){
		Vector3 orinRotate = trans.localEulerAngles;
		for(float t = 0; t < time; t += tk2dUITime.deltaTime){
			float nt = Mathf.Clamp01( t / time );
			nt = Mathf.Sin(nt * Mathf.PI * 0.5f);
			trans.localPosition -= new Vector3(-1, -2, 0);
			trans.localEulerAngles = Vector3.Lerp(orinRotate, targetRoatate, nt);
			trans.localPosition += new Vector3(1, 2, 0);
			yield return 0;
		}
		trans.localEulerAngles = targetRoatate;
		if(callbackPtr != null)
			callbackPtr();
	}
	
	
	
	public static IEnumerator DoTransparency(tk2dSprite[] sprite, float time, CallBackPtr callbackPtr = null){
		Color orinColor = new Color(1, 1, 1, 0);//sprite.color;
		Color targetColor = new Color(1, 1, 1, 1);
		Color curSrcColor;
		Color curTargetColor;
		int reverseCount = 16 * (int)time;
		for(int i = 0; i < reverseCount; i++){
			if((i & 1) == 0){
				curSrcColor = new Color(orinColor.r, orinColor.g, orinColor.b, orinColor.a);
				curTargetColor = new Color(targetColor.r, targetColor.g, targetColor.b, targetColor.a);
			}
			else{
				curSrcColor = new Color(targetColor.r, targetColor.g, targetColor.b, targetColor.a);
				curTargetColor = new Color(orinColor.r, orinColor.g, orinColor.b, orinColor.a);
			}
			for(float t = 0; t < time / reverseCount; t += tk2dUITime.deltaTime){
				float nt = Mathf.Clamp01( t / (time / reverseCount) );
				nt = Mathf.Sin(nt * Mathf.PI * 0.5f);
				for(int j = 0; j < sprite.Length; j++)
					sprite[j].color = Color.Lerp(curSrcColor, curTargetColor, nt);
				yield return 0;
			}
		}
		for(int j = 0; j < sprite.Length; j++)
			sprite[j].color = targetColor;//Color.Lerp(curSrcColor, curTargetColor, nt);
		if(callbackPtr != null)
			callbackPtr();
	}
	
	public static IEnumerator ScaledUp(Transform trans, float time, Vector3 targetScale, CallBackPtr callbackPtr = null){
		Vector3 orinScale = trans.localScale;
		Vector3 curSrcScale = new Vector3(orinScale.x, orinScale.y, orinScale.z);
		Vector3 curTargetScale = new Vector3(orinScale.x, orinScale.y, orinScale.z);
		int reverseCount = 16; //* (int)time;
		float xRate = (orinScale.x / targetScale.x) / (reverseCount * 2);
		float yRate = (orinScale.y / targetScale.y) / (reverseCount * 2);
		for(int i = 0; i < reverseCount; i++){
			if((i & 1) == 0){
				curSrcScale = new Vector3(orinScale.x + targetScale.x * i * xRate, orinScale.y + targetScale.y * i * yRate, orinScale.z);
				curTargetScale = new Vector3(curSrcScale.x + targetScale.x * i * xRate, curSrcScale.y + targetScale.y * i * yRate, targetScale.z);
			}
			else{
				curSrcScale = new Vector3(curTargetScale.x, curTargetScale.y, curTargetScale.z);
				curTargetScale = new Vector3(curSrcScale.x - targetScale.x * xRate, curSrcScale.x, curSrcScale.x - targetScale.y * yRate);
			}
			for(float t = 0; t < time / (reverseCount * 2); t += tk2dUITime.deltaTime){
				float nt = Mathf.Clamp01( t / (time / (reverseCount * 2)) );
				nt = Mathf.Sin(nt * Mathf.PI * 0.5f);
				trans.localScale = Vector3.Lerp(curSrcScale, curTargetScale, nt);
				yield return 0;
			}
		}
		trans.localScale = targetScale;
		if(callbackPtr != null)
			callbackPtr();	
	}
	
	public static IEnumerator ScaledDown(Transform trans, float time, Vector3 targetScale, CallBackPtr callbackPtr = null){
		Vector3 orinScale = trans.localScale;
		Vector3 curSrcScale = new Vector3(orinScale.x, orinScale.y, orinScale.z);
		Vector3 curTargetScale = new Vector3(orinScale.x, orinScale.y, orinScale.z);
		int reverseCount = 16; //* (int)time;
		float xRate = (targetScale.x / orinScale.x) / (reverseCount * 2);
		float yRate = (targetScale.y / orinScale.y) / (reverseCount * 2);
		for(int i = 0; i < reverseCount; i++){
			if((i & 1) == 0){
				curSrcScale = new Vector3(orinScale.x - orinScale.x * i * xRate, orinScale.y - orinScale.y * i * yRate, orinScale.z);
				curTargetScale = new Vector3(curSrcScale.x - orinScale.x * i * xRate, curSrcScale.y - orinScale.y * i * yRate, targetScale.z);
			}
			else{
				curSrcScale = new Vector3(curTargetScale.x, curTargetScale.y, curTargetScale.z);
				curTargetScale = new Vector3(curSrcScale.x + orinScale.x * xRate, curSrcScale.x, curSrcScale.x + orinScale.y * yRate);
			}
			for(float t = 0; t < time / (reverseCount * 2); t += tk2dUITime.deltaTime){
				float nt = Mathf.Clamp01( t / (time / (reverseCount * 2)) );
				nt = Mathf.Sin(nt * Mathf.PI * 0.5f);
				trans.localScale = Vector3.Lerp(curSrcScale, curTargetScale, nt);
				yield return 0;
			}
		}
		trans.localScale = targetScale;
		if(callbackPtr != null)
			callbackPtr();	
	}
}
