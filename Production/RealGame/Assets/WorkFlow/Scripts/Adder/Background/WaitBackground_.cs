using UnityEngine;
using System.Collections;

public class WaitBackground_ : MonoBehaviour {
	public float m_velocity = 0.5f;
	public tk2dCamera cam;
	Material m_material;
	
	void Start () {
		enabled = false;
	}
	
	public void Initialize(){
		if(cam == null){
			Debug.Log("cam is null");
			return;
		}
		
		float nativeResolutionX = cam.nativeResolutionWidth; 
		float nativeResolutionY = cam.nativeResolutionHeight; 
		float ratio = cam.CameraSettings.orthographicPixelsPerMeter;
		
		enabled = true;
		float lenthUnit = 1 / ratio;
		transform.localScale = new Vector3(lenthUnit * nativeResolutionX, 
			lenthUnit * nativeResolutionY, 1);
		m_material = gameObject.renderer.material;
	}
	
	void Update () {
		Vector2 texOffset = m_material.GetTextureOffset("_MainTex");
		texOffset.x += Time.deltaTime * m_velocity;
		texOffset.y += Time.deltaTime * m_velocity;
		if(texOffset.x >= 1.0f)
			texOffset.x = 0.0f;
		if(texOffset.y >= 1.0f)
			texOffset.y = 0.0f;
		m_material.SetTextureOffset("_MainTex", texOffset);
	}
}
