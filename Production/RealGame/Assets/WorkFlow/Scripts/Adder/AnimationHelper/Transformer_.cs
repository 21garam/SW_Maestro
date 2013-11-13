using UnityEngine;
using System.Collections;

public class Transformer_: MonoBehaviour {
	public enum Dir{
		LEFT,
		RIGHT,
	};
	
	public GameObject m_parent;
	public GameObject m_target;
	public float m_parentAniDuration = 0.5f;
	public float m_targetAniDuration = 0.5f;
	public tk2dUIItem m_uiItem;
	public Dir m_dir = Dir.LEFT;
	
	void OnEnable(){
	   	m_uiItem.OnClick += BeginTransform;
	}

    void OnDisable(){
    } 
	
	public void BeginTransform(){
		SetLayer(m_parent, "DisalbedUI");
		SetLayer(m_target, "DisalbedUI");
		m_target.transform.localPosition = new Vector3(0, 0, 0);
		m_target.transform.localScale = new Vector3(0, 0, 0);
		StartCoroutine(Animation_.ScaleAToB(m_target.transform, m_targetAniDuration, new Vector3(1.1f, 1.1f, 1), ScaleDown));
		
		Vector3 targetPos = new Vector3(0, 0, 0);
		switch(m_dir){
			case Dir.LEFT:
			targetPos = new Vector3(-5, 0, 0);
			break;
			
			case Dir.RIGHT:
			targetPos = new Vector3(5, 0, 0);
			break;
		}
	  	StartCoroutine(Animation_.TransformAToB(m_parent.transform, m_parentAniDuration, targetPos, EndParentTransform));
	}
	
	void ScaleDown(){
		StartCoroutine(Animation_.ScaleAToB(m_target.transform, m_targetAniDuration / 5, new Vector3(1,1,1), EndTargetTransform));
	}
	
	void EndParentTransform(){
		SetLayer(m_parent, "EnabledUI");
	}
	
	void EndTargetTransform(){
		SetLayer(m_target, "EnabledUI");
	}
	
	void SetLayer(GameObject target, string layerName){
		int layer = LayerMask.NameToLayer(layerName);
		if(layer < 0)
			layer = 0;
		RecursiveSetLayer(target, layer);
	}
	
	void RecursiveSetLayer(GameObject parent, int layer){
		parent.layer = layer;
		for(int i = 0; i < parent.transform.childCount; i++){
			Transform tranChd = parent.transform.GetChild(i);
			GameObject gameChd = tranChd.gameObject;
			RecursiveSetLayer(gameChd, layer);
		}	
	}
}
