using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundsController : MonoBehaviour {
    public AudioClip Landscape;
    public AudioClip SoundStrike;
    public AudioClip SoundStrikeShield;
    public AudioClip NoiseRunOnGround;
    public AudioClip SoundColdArrow;
    public AudioClip SoundFireBall;
    public AudioClip SoundUpHP;
	private AudioSource audio;
	void Start () {
		audio = GetComponent<AudioSource>();
	}
		
	void RunOnGround(){
		audio.PlayOneShot(NoiseRunOnGround);
	}
	void Strike(){
		audio.PlayOneShot(SoundStrike);
	}
	void StrikeShield(){
		audio.PlayOneShot(SoundStrikeShield);
	}
	void FireBall(){
		audio.PlayOneShot(SoundFireBall);
	}
	void ColdArrow(){
		audio.PlayOneShot(SoundColdArrow);
	}
	void UpHP(){
		audio.PlayOneShot(SoundUpHP);
	}
}
