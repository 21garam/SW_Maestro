using UnityEngine;
using System.Collections;

public class ParticleFactory_ : MonoBehaviour {
	public Particle_ dieBubblePrefabs;
	
	private void MakeDieBubbleParticle(Vector3 pos){
		Particle_ particle = GameObject.Instantiate(dieBubblePrefabs) as Particle_;	
		particle.transform.position = pos;
	}
	
	public static void MakeParticle(Vector3 pos){
		GameObject gm = GameObject.FindGameObjectWithTag("ParticleFactory");
		ParticleFactory_ factory = gm.GetComponent<ParticleFactory_>();
		factory.MakeDieBubbleParticle(pos);
	}
}
