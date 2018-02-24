using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnscaledTimeParticle : MonoBehaviour {

	private ParticleSystem particle;

	private	void Awake ()
	{
		particle = GetComponent<ParticleSystem> ();
	}

	void Update ()
	{
		if (particle.gameObject.activeInHierarchy && !particle.isPlaying) {
//			Time.timeScale = 0;
			Destroy (gameObject);
		}
	}
}
