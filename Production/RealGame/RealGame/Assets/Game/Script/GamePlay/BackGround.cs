using UnityEngine;
using System.Collections;

public class BackGround : MonoBehaviour {
	public float velocityPerNormalizedWidth;
	Material m_material;
	
	void Start () {
	}
	
	public void Initialize(float nativeResolutionX, float nativeResolutionY, float ratio_scrWidth_To_bgWidth){
		transform.localScale = new Vector3(nativeResolutionX, nativeResolutionY, 1);
		transform.localPosition = new Vector3(nativeResolutionX / 2, nativeResolutionY / 2, 0);
		
		m_material = gameObject.renderer.material;
		m_material.SetTextureScale("_MainTex", new Vector2(0.5f, 1));
		m_material.SetTextureScale("_MainTex", new Vector2(ratio_scrWidth_To_bgWidth, 1));
	}
	
	void Update () {
		Vector2 texOffset = m_material.GetTextureOffset("_MainTex");
		texOffset.x += Time.deltaTime * (PlayerFish.Instance.dx/velocityPerNormalizedWidth);
		if(texOffset.x >= 1.0f)
			texOffset.x = 0.0f;
		m_material.SetTextureOffset("_MainTex", texOffset);
	}
}
