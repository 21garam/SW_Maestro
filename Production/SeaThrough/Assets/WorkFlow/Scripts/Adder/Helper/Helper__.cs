using UnityEngine;
using System.Collections;

public class Helper__ {
	public static void SetLayer(GameObject target, string layerName){
		int layer = LayerMask.NameToLayer(layerName);
		if(layer < 0)
			layer = 0;
		RecursiveSetLayer(target, layer);
	}
	
	public static void RecursiveSetLayer(GameObject parent, int layer){
		parent.layer = layer;
		for(int i = 0; i < parent.transform.childCount; i++){
			Transform tranChd = parent.transform.GetChild(i);
			GameObject gameChd = tranChd.gameObject;
			RecursiveSetLayer(gameChd, layer);
		}	
	}
}
