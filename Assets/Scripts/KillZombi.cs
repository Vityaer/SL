using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZombi : MonoBehaviour {
	GameObject[] Zombi;
	// Use this for initialization
	void OnTriggerEnter2D(Collider2D col){
		if(col.GetComponent<PlayerHP>()){
	        Zombi = GameObject.FindGameObjectsWithTag("Zombi");
			foreach(GameObject zombi in Zombi){
	    		zombi.GetComponent<EnemyHP>().GetDamage(10f);
	    	}
	    	Destroy(gameObject);
    	}
	}
}
