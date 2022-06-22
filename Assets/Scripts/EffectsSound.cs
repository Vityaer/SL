using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsSound : MonoBehaviour {
	
	public AudioClip SoundCreateMagic;
    public AudioClip SoundFly;
    public AudioClip SoundDeathEffect;
	private AudioSource audioSource;
	void Awake(){
		audioSource = GetComponent<AudioSource>();
	}
	void CreateMagic(){
		audioSource.PlayOneShot(SoundCreateMagic);
	}
	void Fly(){
		audioSource.PlayOneShot(SoundFly);
	}
	void DeathEffect(){
		audioSource.PlayOneShot(SoundDeathEffect);
	}
}
