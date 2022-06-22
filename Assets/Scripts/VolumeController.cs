using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using DG.Tweening;

public class VolumeController : MonoBehaviour {
    public AudioSource audio;
	public float Volume;
	public float FinishEarly = 0.5f;
	public float levelFromHP = 1f;
	private float RoosterHP;
	public float scale = 1f;
	public bool music = false;
	void Awake(){
        if(GetComponent<AudioSource>()){
    		audio = GetComponent<AudioSource>();
    		if(gameObject.name == "MusicLevel") {
    			music = true;
    		}else{
        		audio.maxDistance = 15f;
            }	
        }else{
            this.enabled = false;
        }

	}
	void Start () {
		ObserverHP.Register( ChangeHPRooster );
		if(!music){
			ObserverSoundLevel.Register( ChangeLevelSound );
    		Volume = ObserverSoundLevel.level;
		}else{
			ObserverMusicLevel.Register( ChangeLevelSound );
            Volume = ObserverMusicLevel.level;
		}
		ObserverTimeScale.Register( ChangeTimeScale );
		scale = ObserverTimeScale.level;
		ChangeHPRooster(ObserverHP.level);
	}
	public void ChangeLevelSound(float num){
        Volume = num;
        CalculateVolume();
    }
    public void ChangeHPRooster(float num){
    	if(num > 3){
			levelFromHP = 1f;
		}else if(num == 3){
			levelFromHP = 0.4f;
		}else if(num == 2){
			levelFromHP = 0.2f;
		}else if(num <= 1){
			levelFromHP = 0f;
		}
        CalculateVolume();

    }
    public void ChangeTimeScale(float _scale){
    	scale = _scale;
    	if(scale == 0f) {
    		audio.Pause();
    	}else{
    		audio.Play();
    	}
    }
    void OnDestroy(){
    	if(!music){
	        ObserverSoundLevel.UnRegister( ChangeLevelSound );
	    }else{
	        ObserverMusicLevel.UnRegister( ChangeLevelSound );
	    }
        ObserverTimeScale.UnRegister( ChangeTimeScale );
        ObserverHP.UnRegister( ChangeHPRooster );
    }

    void CalculateVolume(){
        audio.DOFade(Volume * FinishEarly * levelFromHP * scale, 0.1f); 
    }
    public void ChangeFinishEarly(float num){
    	FinishEarly = num;
    	CalculateVolume();
    }
    public void PlayMusic(AudioClip clip){
        audio.clip = clip;
        audio.Play();
    }
}
