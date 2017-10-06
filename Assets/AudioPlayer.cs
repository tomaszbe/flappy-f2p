using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour {

	public AudioSource collisionAudioSource;

	void Start() {
		if (collisionAudioSource == null) {
			collisionAudioSource = GetComponent<AudioSource> ();
		}
	}

	void OnCollisionEnter(Collision collision) {
		collisionAudioSource.Play ();
	}

}
