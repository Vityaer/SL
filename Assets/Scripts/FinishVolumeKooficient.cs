using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishVolumeKooficient : MonoBehaviour {
	public float Kooficient;

	void OnTriggerEnter2D(Collider2D coll){
        if (coll.gameObject.GetComponent<PlayerHP>()){
        	GameObject.Find("MusicLevel").GetComponent<VolumeController>().ChangeFinishEarly(Kooficient);
        }
    }
}
