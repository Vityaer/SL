using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverMenu : MonoBehaviour {
    public AudioClip SoundMouseEnter;
    public AudioClip SoundMouseClick; 
    
    void Start(){
        audio = GameObject.Find("Sounds").GetComponent<AudioSource>();
    }
    public void InMainMenu(){
        ReturnInformation();
        FadeInOut.nextLevel = "Scene0";
        FadeInOut.sceneEnd = true;
    }
    public void Restart(){
        ReturnInformation();
        FadeInOut.nextLevel = Information.Instance.NameLevel;
        FadeInOut.sceneEnd = true;
    }
    void ReturnInformation(){
        Information.Instance.Reestablish();
        Information.Instance.temperature = 0f;
        PrepareFade();
    }
    AudioSource audio;
    public void PlayMouseEnter(){
        audio.PlayOneShot(SoundMouseEnter);
    }
    public void PlayMouseClick(){
        audio.PlayOneShot(SoundMouseClick);
    }
    void PrepareFade(){
        FadeInOut.sceneStarting = false;
    }
}

