using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Event_{
	public float m_time;
	public string m_event;
	public string m_param;
	
	public Event_(float time_, string event_, string param_){
		Set(time_, event_, param_);
	}
	
	public void Set(float time_, string event_, string param_){
		m_time = time_;
		m_event = event_;
		m_param = param_;
	}
	
	public override string ToString (){
		return string.Format ("time: " + m_time.ToString() + ", event: " + m_event + ", param: " + m_param);
	}
}

public class EventSet_{
	List<Event_> m_eventList;
	List<Event_> m_returnEventList;
	List<Event_> m_srcEventList;
	
	public EventSet_(){
		m_eventList = new List<Event_>();
		m_returnEventList = new List<Event_>();
		m_srcEventList = new List<Event_>();
	}
	
	public void AddEvent(float time, string eventName, string parameter){
		m_eventList.Add(new Event_(time, eventName, parameter));
		m_srcEventList.Add(new Event_(time, eventName, parameter));
	}
	
	public List<Event_> GetEvents(float time){
		m_returnEventList.Clear();
		for(int i = 0; i < m_eventList.Count; i++){
			Event_ chd = m_eventList[i];
			if(chd.m_time <= time){
				m_returnEventList.Add(chd);
			}
		}
		
		for(int i = 0; i < m_returnEventList.Count; i++){
			Event_ removingChd = m_returnEventList[i];
			m_eventList.Remove(removingChd);
		}
		
		return m_returnEventList;
	}
	
	public bool IsEmptyEventList(){
		return m_eventList.Count > 0 ? false : true;
	}
	
	public void RetoreEventList(){
		m_eventList.Clear();
		for(int i = 0; i < m_srcEventList.Count; i++){
			Event_ eventEle = m_srcEventList[i];
			m_eventList.Add(eventEle);
		}
	}
}

public class Pattern_{
	List<EventSet_> m_eventSet;
	
	public Pattern_(){		
		m_eventSet = new List<EventSet_>();
		
		m_eventSet.Add(new EventSet_()); //float h, int size, float coolTime, string kind			
		m_eventSet[0].AddEvent(0.0f, "SpawnFeedFish", "0.60 6 0.3 FEED_SMALL");
		m_eventSet[0].AddEvent(0.2f, "SpawnFeedFish", "0.50 6 0.3 FEED_SMALL");
		m_eventSet[0].AddEvent(0.4f, "SpawnFeedFish", "0.40 6 0.3 FEED_SMALL");
		m_eventSet[0].AddEvent(2.2f, "SpawnEnemyFish", "0.5 1 0 KIND1");
		m_eventSet[0].AddEvent(2.8f, "SpawnFeedFish", "0.60 3 0.5 FEED_RANDOM");
		m_eventSet[0].AddEvent(3.0f, "SpawnFeedFish", "0.50 3 0.5 FEED_RANDOM");
		m_eventSet[0].AddEvent(2.8f, "SpawnFeedFish", "0.40 3 0.5 FEED_RANDOM");
		m_eventSet[0].AddEvent(6.0f, "EndOfChunk", "EndOfChunk");
		
		m_eventSet.Add(new EventSet_());
		m_eventSet[1].AddEvent(0.0f, "SpawnObstacle",   "0.0 3 1 KIND1");
		m_eventSet[1].AddEvent(0.0f, "SpawnObstacle",   "1.0 3 1 STALACTITE");
		m_eventSet[1].AddEvent(2.2f, "SpawnFeedFish", "0.60 10 0.4 FEED_SMALL");
		m_eventSet[1].AddEvent(2.0f, "SpawnFeedFish", "0.50 10 0.4 FEED_SMALL");
		m_eventSet[1].AddEvent(1.8f, "SpawnFeedFish", "0.40 10 0.4 FEED_SMALL");
		m_eventSet[1].AddEvent(6.0f, "EndOfChunk", "EndOfChunk");
		
		m_eventSet.Add(new EventSet_());
		m_eventSet[2].AddEvent(0.0f, "SpawnObstacle",   "0.0 5 1 KIND1");
		m_eventSet[2].AddEvent(1.0f, "SpawnFeedFish", "0.50 10 0.4 FEED_SMALL");
		m_eventSet[2].AddEvent(1.0f, "SpawnFeedFish", "0.40 10 0.4 KIND2");
		m_eventSet[2].AddEvent(6.0f, "EndOfChunk", "EndOfChunk");
		
		m_eventSet.Add(new EventSet_());
		m_eventSet[3].AddEvent(0.0f, "SpawnObstacle",   "1.0 5 1 STALACTITE");
		m_eventSet[3].AddEvent(2.2f, "SpawnFeedFish", "0.70 10 0.3 FEED_RANDOM");
		m_eventSet[3].AddEvent(2.0f, "SpawnFeedFish", "0.60 10 0.3 FEED_RANDOM");
		m_eventSet[3].AddEvent(1.8f, "SpawnFeedFish", "0.50 10 0.3 FEED_RANDOM");
		m_eventSet[3].AddEvent(3f, "SpawnEnemyFish", "0.20 1 0 KIND1");
		m_eventSet[3].AddEvent(6.0f, "EndOfChunk", "EndOfChunk");
		
		m_eventSet.Add(new EventSet_());
		m_eventSet[4].AddEvent(0.0f, "SpawnObstacle",   "0.0 4 1 KIND1");
		m_eventSet[4].AddEvent(0.0f, "SpawnObstacle",   "1.0 4 1 STALACTITE");
		m_eventSet[4].AddEvent(2.2f, "SpawnFeedFish", "0.60 10 0.4 FEED_SMALL");
		m_eventSet[4].AddEvent(2.0f, "SpawnFeedFish", "0.50 10 0.4 FEED_SMALL");
		m_eventSet[4].AddEvent(1.8f, "SpawnFeedFish", "0.40 10 0.4 FEED_SMALL");
		m_eventSet[4].AddEvent(6.0f, "EndOfChunk", "EndOfChunk");
		
		/*
		m_eventSet.Add(new EventSet_());
		m_eventSet[2].AddEvent(0, "SpawnFeedFish", "0.65 10 0.5 KIND1");
		m_eventSet[2].AddEvent(0, "SpawnFeedFish", "0.5 10 0.5 KIND2");
		m_eventSet[2].AddEvent(0, "SpawnFeedFish", "0.35 10 0.5 FEED_SMALL");
		m_eventSet[2].AddEvent(0, "SpawnFeedFish", "0.20 10 0.5 FEED_RANDOM");
		m_eventSet[2].AddEvent(0, "SpawnObstacle",  "0.0 1 0 KIND1");
		m_eventSet[2].AddEvent(2, "SpawnObstacle",  "1.0 1 0 STALACTITE");
		m_eventSet[2].AddEvent(4, "SpawnObstacle",  "0.0 1 0 STALAGMITE");
		m_eventSet[2].AddEvent(6, "EndOfChunk", "EndOfChunk");
		
		m_eventSet.Add(new EventSet_());
		m_eventSet[3].AddEvent(0, "SpawnObstacle",  "0.0 1 0 KIND1");
		m_eventSet[3].AddEvent(1, "SpawnFeedFish", "0.3 7 0.8 KIND1");
		m_eventSet[3].AddEvent(2, "SpawnEnemyFish", "0.3 1 0 KIND2");
		m_eventSet[3].AddEvent(2, "SpawnFeedFish", "0.7 7 0.8 KIND1");
		m_eventSet[3].AddEvent(3, "SpawnObstacle",  "0.0 1 0 KIND1");
		m_eventSet[3].AddEvent(9, "EndOfChunk", "EndOfChunk");
		*/
	}
	
	public EventSet_ GetEventSet(int idx){
		return m_eventSet[idx];
	}
	
	public int GetEventSize(){
		return m_eventSet.Count;
	}
}

public class GameManager_ : MonoBehaviour {
	enum State_{
		Ready,
		InitPattern,
		PlayPattern,
		End,
	};
	State_ state;
	
	public BackGround m_bg;
	public tk2dCamera m_cam;
	public Spawner m_spawner;
	Pattern_ m_pattern;
	float m_time;
	EventSet_ m_eventSetCursor = null;
	int patternIDX = 0;
	
	void Start () {
		StartCoroutine(InitializeProperties());
		m_time = 0;
		state = State_.InitPattern;
		m_pattern = new Pattern_();
	}
		
	void Update () {
		switch(state){
			case State_.InitPattern:
			m_eventSetCursor = m_pattern.GetEventSet(patternIDX);
			m_time = 0;
			patternIDX = (patternIDX + 1) % m_pattern.GetEventSize();
			state = State_.PlayPattern;
			break;
			
			case State_.PlayPattern:
			m_time += Time.deltaTime;
			List<Event_> eventList = m_eventSetCursor.GetEvents(m_time);
			
			if(m_eventSetCursor.IsEmptyEventList()){
				Debug.Log(string.Format("Next Chunk : {0}", patternIDX));
				m_eventSetCursor.RetoreEventList();
				state = State_.InitPattern;
			}
			
			for(int i = 0; i < eventList.Count; i++){
				MakeEvent(eventList[i]);	
			}
			break;
			
			case State_.End:
			break;
		}
	}
	
	public void MakeEvent(Event_ eve){
		m_spawner.Spwan(eve);
	}
	
	IEnumerator InitializeProperties(){
		while(true){
			if(m_cam != null){
				float screenWidth = m_cam.NativeResolution.x;
				float screenHeight = m_cam.NativeResolution.y;
				
				m_bg.Initialize(screenWidth, screenHeight,  0.5f);
				
				//m_spawner = transform.GetChild(0).GetComponent<Spawner_>();				
				m_spawner.Initialize(screenWidth, screenHeight);
				break;
			}else
				yield return null;
		}
	}
}
