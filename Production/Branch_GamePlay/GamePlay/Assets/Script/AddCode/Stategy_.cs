using UnityEngine;
using System.Collections;

public class Strategy_ {
	public static void GoStraight(GameObject me, Vector3 vel){
		Vector3 delta = vel * Time.deltaTime;
		me.transform.localPosition += delta;
	}
	
	public static void GoSignPattern(GameObject me, Vector2 signSize, Vector2 strPos, float xVel, float time){
		float x = strPos.x + time * xVel;
		float y = Mathf.Sin(x / signSize.x) * signSize.y + strPos.y;
		float z = me.transform.localPosition.z;
		me.transform.localPosition = new Vector3(x, y, z);
	}
}
