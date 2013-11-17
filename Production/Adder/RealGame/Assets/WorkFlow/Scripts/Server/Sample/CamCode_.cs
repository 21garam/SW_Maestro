using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CamCode_ : MonoBehaviour {
    public tk2dUIItem rightBtr;
	public tk2dUIItem leftBtr;
	
	public Camera cam;
	
	List<Vector3> windowList;
	int currentWindowListID;
	int spaceCount;
	
	void Start () {
		currentWindowListID = (int)(cam.transform.position.x / 3);
		spaceCount = currentWindowListID + 1;
		windowList = new List<Vector3>(10);
		for(int i = 0; i < spaceCount; i++)
			windowList.Add(new Vector3(3*i,1,-10));
	}
	
    void OnEnable() {
        rightBtr.OnClick += GoToRight;
        leftBtr.OnClick += GoToLeft;
    }
	
    private void GoToRight() {
		if(currentWindowListID >= windowList.Count-1)
			return;
		currentWindowListID++;
    	Vector3 position = new Vector3(
			windowList[currentWindowListID].x, 
			windowList[currentWindowListID].y,
			windowList[currentWindowListID].z);
		GoToLocation(cam, position);
	}
	
    private void GoToLeft() {
		if(currentWindowListID <= 0)
			return;
		currentWindowListID--;
    	Vector3 position = new Vector3(
			windowList[currentWindowListID].x, 
			windowList[currentWindowListID].y,
			windowList[currentWindowListID].z);
		GoToLocation(cam, position);
	}
	
	private void GoToLocation(Camera obj, Vector3 loc) {
		Transform t = obj.transform;
    	StartCoroutine(TransformTo(t, 0.3f, loc));
    }
	
	IEnumerator TransformTo( Transform transform, float time, Vector3 toPos) {
        Vector3 fromPos = transform.localPosition;
        for (float t = 0; t < time; t += tk2dUITime.deltaTime) {
            float nt = Mathf.Clamp01( t / time );
            nt = Mathf.Sin(nt * Mathf.PI * 0.5f);

            transform.localPosition = Vector3.Lerp( fromPos, toPos, nt );
            yield return 0;
        }
		transform.localPosition = toPos;
    }
}
