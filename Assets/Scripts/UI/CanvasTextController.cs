using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasTextController : MonoBehaviour{
   
	private AudioSource audio;
	public Text HistoryText;
   	public AudioClip SoundMouseEnter;
    public AudioClip SoundMouseClick;
    private Text textComponent;
	string[] arrayString;
    Coroutine textPrint;
    void Start(){
		audio = GameObject.Find("Sounds").GetComponent<AudioSource>();
    }
    void StartWrite(string[] arrayString){
    	this.arrayString = arrayString;

    }
}
