using UnityEngine;
using System.Collections;

public class Particle_ : MonoBehaviour {
	public ParticleRenderer particleRenderer_;
	public ParticleEmitter particleEmitter_;
	
	public void SetEnable(bool enable_ = true){
		particleRenderer_.enabled = enable_;
	}
	
	public void SetEmitterMinMax(float maxSize, float minSize){
		particleEmitter_.minSize = minSize;
		particleEmitter_.maxSize = maxSize;
	}
	
	public void SetXVelocity(float velocity){
		particleEmitter_.worldVelocity = new Vector3(velocity, particleEmitter_.worldVelocity.y, particleEmitter_.worldVelocity.z);
	}
}
