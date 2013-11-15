using UnityEngine;
using System.Collections;

public class Strategy_ {
	public static void GoStraight(GameObject me, Vector3 vel){
		vel.x -= PlayerFish.Instance.dx;
		Vector3 delta = vel * Time.deltaTime;
		me.transform.localPosition += delta;
	}
	
	public static void GoSinePattern(GameObject me, Vector2 signSize, Vector2 strPos, float xVel, float time){
		float x = strPos.x + time * xVel;
		float y = Mathf.Sin(x / signSize.x) * signSize.y + strPos.y;
		float z = me.transform.localPosition.z;
		me.transform.localPosition = new Vector3(me.transform.localPosition.x + Time.deltaTime * (xVel - PlayerFish.Instance.dx), y, z);
	}
	
	public static void GoSinePatternEx(GameObject me, Vector2 signSize, Vector2 strPos, float xVel, float time, float frequency){
		float x = strPos.x + time * xVel * frequency;
		float y = Mathf.Sin(x / signSize.x) * signSize.y + strPos.y;
		float z = me.transform.localPosition.z;
		me.transform.localPosition = new Vector3(me.transform.localPosition.x + Time.deltaTime * (xVel - PlayerFish.Instance.dx), y, z);
	}
	
	public static void GoCosinePattern(GameObject me, Vector2 signSize, Vector2 strPos, float xVel, float time){
		float x = strPos.x + time * xVel;
		float y = Mathf.Cos(x / signSize.x) * signSize.y + strPos.y;
		float z = me.transform.localPosition.z;
		me.transform.localPosition = new Vector3(me.transform.localPosition.x + Time.deltaTime * (xVel - PlayerFish.Instance.dx), y, z);
	}
	
	public static void GoCosinePatternEx(GameObject me, Vector2 signSize, Vector2 strPos, float xVel, float time, float frequency){
		float x = strPos.x + time * xVel * frequency;
		float y = Mathf.Cos(x / signSize.x) * signSize.y + strPos.y;
		float z = me.transform.localPosition.z;
		me.transform.localPosition = new Vector3(me.transform.localPosition.x + Time.deltaTime * (xVel - PlayerFish.Instance.dx), y, z);
	}
}
