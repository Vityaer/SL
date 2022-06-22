using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdRoosterOn : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D col){
		if(col.GetComponent<PlayerHP>()){
			col.GetComponent<ColdRooster>().enabled = true;
		}
    }
}
