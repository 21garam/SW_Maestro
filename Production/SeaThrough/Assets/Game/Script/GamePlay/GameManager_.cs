using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ChunkFlow{

	public class Node
	{
		public class Edge
		{
			public int dest;
			public int weight;
			
			public void set(int _dest, int _weight)
			{
				dest = _dest;
				weight = _weight;
			}
		}
		
		public Edge[] edges;
		
		public Node()
		{
			edges = new Edge[3];
			for(int i=0;i<3;i++)
				edges[i] = new Edge();
		}
	}
	
	int point;
	int curChunk;
	Node[] matrix;
	
	public ChunkFlow()
	{
		point = 0;
		curChunk = 0;
		matrix = new Node[13];
		for(int i=0;i<13;i++)
		{
			matrix[i] = new Node();
			
			for(int j=0;j<3;j++)
			{
				matrix[i].edges[j].set((((i+2)/3*3+j)%12)+1, j+1);
			}
		}
	}
	
	public int getNextChunk()
	{
		point += PlayerFish.Instance.feverCount+1;
		
		if(Random.Range(0,3) == 0 && point > (curChunk+2)%3+matrix[curChunk].edges[2].weight)
		{
			point -= (curChunk+2)%3+matrix[curChunk].edges[2].weight;
			curChunk = matrix[curChunk].edges[2].dest;
		}
		else if(Random.Range(0,2) == 0 && point > (curChunk+2)%3+matrix[curChunk].edges[1].weight)
		{
			point -= (curChunk+2)%3+matrix[curChunk].edges[1].weight;
			curChunk = matrix[curChunk].edges[1].dest;
		}
		else
		{
			point -= (curChunk+2)%3+matrix[curChunk].edges[0].weight;
			curChunk = matrix[curChunk].edges[0].dest;
		}
		
		//Debug.Log(string.Format("nextChunk : " + curChunk + ", remain point : " + point));
		return curChunk;
	}
}

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
		
		//m_eventSet.Add(new EventSet_());
		//m_eventSet[0].AddEvent(0.0f, "SpawnObstacle", "0.0 2 2.0 KIND1");
		//m_eventSet[0].AddEvent(1.0f, "SpawnObstacle", "1.0 2 2.0 STALACTITE");
		//m_eventSet[0].AddEvent(2.5f, "SpawnFeedFish", "0.80 2 0.3 KIND1");
		//m_eventSet[0].AddEvent(3.55f, "SpawnFeedFish", "0.20 2 0.3 KIND1");
		//m_eventSet[0].AddEvent(4.6f, "SpawnFeedFish", "0.80 2 0.3 KIND1");
		//m_eventSet[0].AddEvent(5.65f, "SpawnFeedFish", "0.20 2 0.3 KIND1");
		//m_eventSet[0].AddEvent(6.0f, "EndOfChunk", "EndOfChunk");
		
		m_eventSet.Add(new EventSet_());
		m_eventSet[0].AddEvent(2.0f, "SpawnFeedFish", "0.40 3 0.3 FEED_SMALL");
		m_eventSet[0].AddEvent(3.5f, "SpawnFeedFish", "0.70 3 0.3 FEED_SMALL");
		m_eventSet[0].AddEvent(5.0f, "SpawnFeedFish", "0.30 3 0.3 FEED_SMALL");
		m_eventSet[0].AddEvent(6.0f, "EndOfChunk", "EndOfChunk");
		
		m_eventSet.Add(new EventSet_());
		m_eventSet[1].AddEvent(0.0f, "SpawnObstacle", "0.0 2 2.6 KIND1");
		m_eventSet[1].AddEvent(1.3f, "SpawnObstacle", "1.0 1 2.6 STALACTITE");
		m_eventSet[1].AddEvent(1.0f, "SpawnFeedFish", "0.50 8 0.4 FEED_SMALL");
		m_eventSet[1].AddEvent(2.0f, "SpawnFeedFish", "0.80 2 0.3 KIND1");
		m_eventSet[1].AddEvent(3.5f, "SpawnFeedFish", "0.20 2 0.3 KIND1");
		m_eventSet[1].AddEvent(4.6f, "SpawnFeedFish", "0.80 2 0.3 KIND1");
		m_eventSet[1].AddEvent(6.0f, "EndOfChunk", "EndOfChunk");
		
		m_eventSet.Add(new EventSet_());
		m_eventSet[2].AddEvent(0.0f, "SpawnObstacle", "0.0 2 2.0 KIND1");
		m_eventSet[2].AddEvent(1.0f, "SpawnObstacle", "1.0 2 2.0 STALACTITE");
		m_eventSet[2].AddEvent(2.0f, "SpawnFeedFish", "0.80 2 0.3 KIND1");
		m_eventSet[2].AddEvent(3.1f, "SpawnFeedFish", "0.20 2 0.3 KIND1");
		m_eventSet[2].AddEvent(4.2f, "SpawnFeedFish", "0.80 2 0.3 KIND1");
		m_eventSet[2].AddEvent(5.3f, "SpawnFeedFish", "0.20 2 0.3 KIND1");
		m_eventSet[2].AddEvent(6.0f, "EndOfChunk", "EndOfChunk");
		
		m_eventSet.Add(new EventSet_());
		m_eventSet[3].AddEvent(0.8f, "SpawnFeedFish", "0.86 15 0.35 FEED_SMALL");
		m_eventSet[3].AddEvent(0.7f, "SpawnFeedFish", "0.74 15 0.35 FEED_SMALL");
		m_eventSet[3].AddEvent(0.6f, "SpawnFeedFish", "0.62 15 0.35 FEED_SMALL");
		m_eventSet[3].AddEvent(0.5f, "SpawnFeedFish", "0.50 15 0.35 FEED_SMALL");
		m_eventSet[3].AddEvent(0.6f, "SpawnFeedFish", "0.38 15 0.35 FEED_SMALL");
		m_eventSet[3].AddEvent(0.7f, "SpawnFeedFish", "0.26 15 0.35 FEED_SMALL");
		m_eventSet[3].AddEvent(0.8f, "SpawnFeedFish", "0.14 15 0.35 FEED_SMALL");
		m_eventSet[3].AddEvent(6.0f, "EndOfChunk", "EndOfChunk");
		
		m_eventSet.Add(new EventSet_()); //float h, int size, float coolTime, string kind			
		m_eventSet[4].AddEvent(0.2f, "SpawnFeedFish", "0.50 6 0.3 FEED_SMALL");
		m_eventSet[4].AddEvent(0.4f, "SpawnFeedFish", "0.60 5 0.3 FEED_SMALL");
		m_eventSet[4].AddEvent(0.4f, "SpawnFeedFish", "0.40 5 0.3 FEED_SMALL");
		m_eventSet[4].AddEvent(2.0f, "SpawnEnemyFish", "0.5 1 0 KIND1");
		m_eventSet[4].AddEvent(2.8f, "SpawnFeedFish", "0.65 3 0.5 FEED_RANDOM");
		m_eventSet[4].AddEvent(3.0f, "SpawnFeedFish", "0.50 3 0.5 FEED_RANDOM");
		m_eventSet[4].AddEvent(2.8f, "SpawnFeedFish", "0.35 3 0.5 FEED_RANDOM");
		m_eventSet[4].AddEvent(6.0f, "EndOfChunk", "EndOfChunk");
		
		m_eventSet.Add(new EventSet_());
		m_eventSet[5].AddEvent(2.0f, "SpawnEnemyFish", "0.8 1 0 KIND1");
		m_eventSet[5].AddEvent(3.0f, "SpawnEnemyFish", "0.2 1 0 KIND1");
		m_eventSet[5].AddEvent(0.4f, "SpawnFeedFish", "0.15 7 0.6 KIND1");
		m_eventSet[5].AddEvent(0.6f, "SpawnFeedFish", "0.25 7 0.6 KIND1");
		m_eventSet[5].AddEvent(0.4f, "SpawnFeedFish", "0.85 7 0.6 KIND1");
		m_eventSet[5].AddEvent(0.6f, "SpawnFeedFish", "0.75 7 0.6 KIND1");
		m_eventSet[5].AddEvent(0.0f, "SpawnFeedFish", "0.50 10 0.5 FEED_SMALL");
		m_eventSet[5].AddEvent(6.0f, "EndOfChunk", "EndOfChunk");
				
		m_eventSet.Add(new EventSet_());
		m_eventSet[6].AddEvent(0.0f, "SpawnObstacle",   "1.0 5 1 STALACTITE");
		m_eventSet[6].AddEvent(2.2f, "SpawnFeedFish", "0.70 10 0.3 FEED_RANDOM");
		m_eventSet[6].AddEvent(2.0f, "SpawnFeedFish", "0.60 10 0.3 FEED_RANDOM");
		m_eventSet[6].AddEvent(1.8f, "SpawnFeedFish", "0.50 10 0.3 FEED_RANDOM");
		m_eventSet[6].AddEvent(3f, "SpawnEnemyFish", "0.15 1 0 KIND2");
		m_eventSet[6].AddEvent(6.0f, "EndOfChunk", "EndOfChunk");
		
		
		m_eventSet.Add(new EventSet_());
		m_eventSet[7].AddEvent(0.0f, "SpawnObstacle",   "0.0 3 1.3 KIND1");
		m_eventSet[7].AddEvent(0.0f, "SpawnObstacle",   "1.0 3 1.3 STALACTITE");
		m_eventSet[7].AddEvent(2.2f, "SpawnFeedFish", "0.60 10 0.4 FEED_SMALL");
		m_eventSet[7].AddEvent(2.0f, "SpawnFeedFish", "0.50 10 0.4 FEED_SMALL");
		m_eventSet[7].AddEvent(1.8f, "SpawnFeedFish", "0.40 10 0.4 FEED_SMALL");
		m_eventSet[7].AddEvent(6.0f, "EndOfChunk", "EndOfChunk");
		
		m_eventSet.Add(new EventSet_());
		m_eventSet[8].AddEvent(0.0f, "SpawnObstacle", "1.0 1 0 STALACTITE");
		m_eventSet[8].AddEvent(0.0f, "SpawnObstacle", "0.0 1 0 KIND1");
		m_eventSet[8].AddEvent(0.4f, "SpawnFeedFish", "0.5 10 0.4 FEED_BIGSINEEX");
		m_eventSet[8].AddEvent(0.4f, "SpawnFeedFish", "0.5 10 0.4 FEED_BIGCOSINEEX");
		m_eventSet[8].AddEvent(6.0f, "EndOfChunk", "EndOfChunk");
		
		m_eventSet.Add(new EventSet_());
		m_eventSet[9].AddEvent(0.0f, "SpawnObstacle",   "0.0 4 1 KIND1");
		m_eventSet[9].AddEvent(0.0f, "SpawnObstacle",   "1.0 4 1 STALACTITE");
		m_eventSet[9].AddEvent(2.2f, "SpawnFeedFish", "0.60 8 0.4 FEED_SMALL");
		m_eventSet[9].AddEvent(2.0f, "SpawnFeedFish", "0.50 8 0.4 FEED_SMALL");
		m_eventSet[9].AddEvent(1.8f, "SpawnFeedFish", "0.40 8 0.4 FEED_SMALL");
		m_eventSet[9].AddEvent(6.0f, "EndOfChunk", "EndOfChunk");
		
		m_eventSet.Add(new EventSet_());
		m_eventSet[10].AddEvent(0.0f, "SpawnObstacle",   "0.0 4 1.3 KIND1");
		m_eventSet[10].AddEvent(1.0f, "SpawnFeedFish", "0.55 10 0.4 FEED_SMALL");
		m_eventSet[10].AddEvent(1.0f, "SpawnFeedFish", "0.40 10 0.4 KIND2");
		m_eventSet[10].AddEvent(6.0f, "EndOfChunk", "EndOfChunk");
		
		m_eventSet.Add(new EventSet_());
		m_eventSet[11].AddEvent(0.4f, "SpawnFeedFish", "0.5 10 0.4 FEED_BIGSINE");
		m_eventSet[11].AddEvent(0.4f, "SpawnFeedFish", "0.5 10 0.4 FEED_BIGCOSINE");
		m_eventSet[11].AddEvent(2.0f, "SpawnEnemyFish", "0.5 1 0 KIND2");
		m_eventSet[11].AddEvent(6.0f, "EndOfChunk", "EndOfChunk");
		
		m_eventSet.Add(new EventSet_());
		m_eventSet[12].AddEvent(0.5f, "SpawnFeedFish", "0.50 10 0.5 FEED_SMALL");
		m_eventSet[12].AddEvent(0.0f, "SpawnObstacle", "1.0 2 3 STALACTITE");
		m_eventSet[12].AddEvent(1.5f, "SpawnObstacle", "0.0 1 0 KIND1");
		m_eventSet[12].AddEvent(2.2f, "SpawnFeedFish", "0.20 2 0.3 KIND1");
		m_eventSet[12].AddEvent(4.0f, "SpawnFeedFish", "0.80 2 0.3 KIND1");
		m_eventSet[12].AddEvent(4.9f, "SpawnFeedFish", "0.20 2 0.3 KIND1");
		m_eventSet[12].AddEvent(6.0f, "EndOfChunk", "EndOfChunk");
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
		Ready_Ing,
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
	
	ChunkFlow chunkFlow;
	
	public GUI_Ready_ gui_ready;
	
	void Start () {
		StartCoroutine(InitializeProperties());
		m_time = 0;
		state = State_.Ready;
		m_pattern = new Pattern_();
		
		chunkFlow = new ChunkFlow();
	}
	
	bool timeCheck = false;
	void Update () {
		switch(state){
			case State_.Ready:
				gui_ready.Begin();
				state = State_.Ready_Ing;
			break;
			
			case State_.Ready_Ing:
				if(gui_ready.IsEnd)
					state = State_.InitPattern;
			break;
			
			case State_.InitPattern:
			m_eventSetCursor = m_pattern.GetEventSet(patternIDX);
			m_time = 0;
			patternIDX = chunkFlow.getNextChunk();
			state = State_.PlayPattern;
			break;
			
			case State_.PlayPattern:
			float incTime = 0;
			float incRate = 1.0f;
			if(PlayerFish.Instance.FeverTime > 0){
				incRate = PlayerFish.Instance.feverLimit / 2;
				timeCheck = false;
			}
			else if(PlayerFish.Instance.isInvincible){
				if(!timeCheck){
					incTime = -PlayerFish.Instance.invincibleTime;;//-Time.deltaTime; //* PlayerFish.Instance.dx;
					timeCheck = true;
				}
			}
			else{
				timeCheck = false;
			}
			
			m_time += Time.deltaTime * incRate + incTime;//(Time.deltaTime + incTime);
			List<Event_> eventList = m_eventSetCursor.GetEvents(m_time);
			
			if(m_eventSetCursor.IsEmptyEventList()){
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
						
				m_spawner.Initialize(screenWidth, screenHeight);
				break;
			}else
				yield return null;
		}
	}
}
