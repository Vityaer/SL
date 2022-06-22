using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnNextMusic : MonoBehaviour {
    public AudioClip Music;
    void OnTriggerEnter2D(Collider2D col){
		if(col.GetComponent<PlayerHP>()){
	        GameObject.Find("MusicLevel").GetComponent<AudioSource>().clip = Music;
			Destroy(gameObject);
		}
	}
}
