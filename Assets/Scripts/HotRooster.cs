using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotRooster : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.GetComponent<PlayerHP>() != null){
			col.gameObject.GetComponent<ColdRooster>().enabled = false;
		}
	}
	void OnTriggerExit2D(Collider2D col){
		if(col.gameObject.GetComponent<PlayerHP>() != null){
			col.gameObject.GetComponent<ColdRooster>().enabled = true;
			col.gameObject.GetComponent<ColdRooster>().TimerAttack = 10f;

		}
	}
}
