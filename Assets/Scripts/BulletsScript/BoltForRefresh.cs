using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltForRefresh : MonoBehaviour {
	bool Add = false;
	 void OnTriggerEnter2D(Collider2D coll){
        if(coll.gameObject.GetComponent<PlayerHP>() && !Add){
        	Add = true;
        	GameObject.Find("BoltController").GetComponent<BoltController>().AddBolt();
 			Destroy(gameObject);
        }
    }
}
