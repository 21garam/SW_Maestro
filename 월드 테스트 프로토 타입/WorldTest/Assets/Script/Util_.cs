using UnityEngine;
using System.Collections;

public class MyDir4_{
	public float left;
	public float right;
	public float up;
	public float down;
	
	public MyDir4_(){
		left = right = up = down = 0;
	}
	
	MyDir4_(float left_, float right_, float up_, float down_){
		SetDir(left_, right_, up_, down_);
	}
	
	public void SetDir(float left_, float right_, float up_, float down_){
		left = left_; right = right_; up = up_; down = down_;
	}
	
	public override string ToString(){
		return "Left : " + left.ToString() + ", Right : " + right.ToString() + ", Up : " + up.ToString() + ", Down : " + down.ToString();
	}
}

public class AnimationHelper_ {
	public static Vector2 BezierCurveAtVec2(Vector2 p0, Vector2 p1, Vector2 p2, float t){
		float x = BezierCurveAtVec1(p0.x, p1.x, p2.x, t);
		float y = BezierCurveAtVec1(p0.y, p1.y, p2.y, t);
		return new Vector2(x, y);
	}
	
	public static float BezierCurveAtVec1(float p0, float p1, float p2, float t){
		return p0 * (1-t) * (1-t) 
			 + p1 * 2 * t * (1-t)
		     + p2 * t * t;
	}
	
	private static float SubBezierCurveVec2Lenth(Vector2 p0, Vector2 p1, Vector2 p2){
		Vector2 subVec = new Vector2(p0.x - p2.x, p0.y - p2.y);
		float len = subVec.magnitude;
		if(len < 0.1f)
			return len;
		else{
			Vector2 midVec0 = BezierCurveAtVec2(p0, p1, p2, 0.25f);// = new Vector2((p0.x + p1.x) / 2, (p0.y + p0.y) / 2);
			float subLen0 = SubBezierCurveVec2Lenth(p0, midVec0, p1);
			Vector2 midVec1 = BezierCurveAtVec2(p0, p1, p2, 0.75f);// = new Vector2((p0.x + p1.x) / 2, (p0.y + p0.y) / 2);
			float subLen1 = SubBezierCurveVec2Lenth(p1, midVec1, p2);
			return subLen0 + subLen1;
		}
	}	
	
	public static float BezierCurveVec2Lenth(Vector2 p0, Vector2 p1, Vector2 p2){
		return SubBezierCurveVec2Lenth(p0, p1, p2);
	}
}
