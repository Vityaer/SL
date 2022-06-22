using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicController : MonoBehaviour {
    public AudioClip StartMusic;
    public AudioClip CenterMusic;
    bool StartMusicFlag;
    bool PlayMusic = false;
    bool FlagOff = false;
    float Timer;
    float MaxValue = 1f;
    private Slider LevelSound;
    public AudioSource audio;
    private Information information;
	// Use this for initialization
	void Awake(){
		audio = GetComponent<AudioSource>();
	}
	void Start () {
		information = Information.Instance;
		if(!information.isDialog){
			audio.clip = StartMusic;
			audio.Play();
			PlayMusic = true;
			Timer = audio.clip.length; 
			StartMusicFlag = true;
		}
	}
	public void OffMusic(){
		FlagOff = true;
		audio.Stop();
	}
}
