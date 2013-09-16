using UnityEngine;
using System.Collections;

public class Enemy0_ : MonoBehaviour {
	public tk2dSprite m_sprite;
	public Backgound_ m_backGround;
	public static Player_ m_player;
	public ArrayList m_traceList;
	
	void Start () {
		transform.position = new Vector3(-42, 503,
										 transform.position.z);	
		//Debug.Log(transform.position);
		m_traceList = new ArrayList();
		m_traceList.Add(new Vector2(148, 863));
		m_traceList.Add(new Vector2(564, 985));
		m_traceList.Add(new Vector2(856, 976));
		m_traceList.Add(new Vector2(994, 785));
		m_traceList.Add(new Vector2(1050, 312));
		m_traceList.Add(new Vector2(938, -54));
	}
	
	enum State{
		OnTarget,
		OnTrace,
		Destroy,
	}
	
	State state = State.OnTarget;
	void Update () {
		switch(state){
			case State.OnTarget:
			OnTarget();
			break;
			
			case State.OnTrace:
			OnTrace();
			break;
			
			case State.Destroy:
			Destroy();
			break;
		}
	}
	
	int m_traceIDX = 0;
	Vector2 m_p0 = new Vector2();
	Vector2 m_p1 = new Vector2();
	Vector2 m_p2 = new Vector2();
	float m_timeLine = 0.0f;
	bool m_isDestroy = false;
	float m_rate = 1.0f;
	void OnTarget(){
		m_timeLine = 0.0f;
		m_p0.Set(transform.position.x, transform.position.y);
		Vector2 targetParam = (Vector2)m_traceList[m_traceIDX++];
		m_p1.Set(targetParam.x, targetParam.y);
		targetParam = (Vector2)m_traceList[m_traceIDX++];
		m_p2.Set(targetParam.x, targetParam.y);
		float curveLen = AnimationHelper_.BezierCurveVec2Lenth(m_p0, m_p1, m_p2);
		m_rate = 1 / curveLen;
		//Debug.Log(AnimationHelper_.BezierCurveVec2Lenth(m_p0, m_p1, m_p2));
		state = State.OnTrace;
		if(m_traceIDX >= m_traceList.Count)
			m_isDestroy = true;
	}
	
	void Destroy(){
		//Debug.Log("A");
		Destroy(this);
	}
	
	void OnTrace(){
		bool isTraceEnd = false;
		m_timeLine += Time.deltaTime * m_rate * 200f;
		if(m_timeLine > 1.0){
			m_timeLine = 1.0f;
			isTraceEnd = true;
		}
		Vector2 nextPos = AnimationHelper_.BezierCurveAtVec2(m_p0, m_p1, m_p2, m_timeLine);
		transform.position = new Vector3(nextPos.x, nextPos.y, transform.position.z);
		if(isTraceEnd && !m_isDestroy)
			state = State.OnTarget;
		else if(isTraceEnd && m_isDestroy)
			state = State.Destroy;
	}
	
	void OnTriggerEnter(Collider col) {
	}
}
