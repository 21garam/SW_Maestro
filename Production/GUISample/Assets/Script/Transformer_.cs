using UnityEngine;
using System.Collections;

public class Transformer_: MonoBehaviour {
	public enum Dir{
		LEFT,
		RIGHT,
	};
	
	public GameObject m_parent;
	public GameObject m_target;
	public float m_parentAniDuration = 1;
	public float m_targetAniDuration = 1;
	public tk2dUIItem m_uiItem;
	public Dir m_dir = Dir.LEFT;
	
	void OnEnable(){
	   	m_uiItem.OnClick += StartTransform;
	}

    void OnDisable(){
    } 
	
	public void StartTransform(){
		SetLayer(m_parent, "DisalbedUI");
		SetLayer(m_target, "DisalbedUI");
		m_target.transform.localPosition = new Vector3(0, 0, 0);
		m_target.transform.localScale = new Vector3(0, 0, 0);
		StartCoroutine(ScaleTo(m_target.transform, m_targetAniDuration, new Vector3(1, 1, 1)));
		
		Vector3 targetPos = new Vector3(0, 0, 0);
		switch(m_dir){
			case Dir.LEFT:
			targetPos = new Vector3(-3, 0, 0);
			break;
			
			case Dir.RIGHT:
			targetPos = new Vector3(3, 0, 0);
			break;
		}
		StartCoroutine(TransformTo(m_parent.transform, m_parentAniDuration, targetPos));
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
	
	IEnumerator ScaleTo(Transform trans, float time, Vector3 targetScale){
		Vector3 orinScale = trans.localScale;
		for(float t = 0; t < time; t += tk2dUITime.deltaTime){
            float nt = Mathf.Clamp01( t / time );
			nt = Mathf.Sin(nt * Mathf.PI * 0.5f);
			trans.localScale = Vector3.Lerp(orinScale, targetScale, nt);
            yield return 0;
        }
		trans.localScale = targetScale;
		SetLayer(m_parent, "EnabledUI");
		SetLayer(m_target, "EnabledUI");
	}
	
	IEnumerator TransformTo(Transform trans, float time, Vector3 targetPos){
		Vector3 orinPos = trans.localPosition;
        Debug.Log(orinPos);
		for(float t = 0; t < time; t += tk2dUITime.deltaTime){
            float nt = Mathf.Clamp01( t / time );
			nt = Mathf.Sin(nt * Mathf.PI * 0.5f);
			trans.localPosition = Vector3.Lerp(orinPos, targetPos, nt);
          	yield return 0;
        }
		trans.localPosition = targetPos;
	}
}
