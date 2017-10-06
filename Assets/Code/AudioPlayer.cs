using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour {

	public AudioSource collisionAudioSource;

	void Start() {
		// If no AudioSource assigned, let's get it ourselves.
		if (collisionAudioSource == null) {
			collisionAudioSource = GetComponent<AudioSource> ();
		}
	}

	void OnCollisionEnter(Collision collision) {
		// Play the sound on collision.
		collisionAudioSource.Play ();
	}

}
