using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ScriptRustle : MonoBehaviour {

	public AudioClip[] Rustle;
	float Timer;
	void Update(){
		if(Timer > 0){
			Timer -= Time.deltaTime;
		}
	}
	void OnTriggerEnter2D(Collider2D coll){
		if((coll.gameObject.GetComponent<PlayerHP>() != null)||(coll.gameObject.GetComponent<PlayerHP>() != null)){
			if(Timer <= 0){
				GetComponent<AudioSource>().Pause();
				GetComponent<AudioSource>().PlayOneShot(Rustle[UnityEngine.Random.Range(0, Rustle.Length-1)]);
				Timer = 0.15f;
			}
		}
	}
}
