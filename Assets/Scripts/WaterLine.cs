using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLine : MonoBehaviour {
	float myTimer = 0;
	bool trigger = false;

	void Update(){
		if(myTimer > 0){
			myTimer -= Time.deltaTime;
		}else{
			trigger = false;
		}
	}
	void OnTriggerEnter2D(Collider2D coll){
        if ((coll.gameObject.GetComponent<PlayerHP>())&&(!trigger)){
        		trigger = true;
        		myTimer = 1f;
        		coll.gameObject.GetComponent<PlayerHP>().HP -= 3;
        		coll.gameObject.transform.position = new Vector3(63f, 5f, 0f);
                Information.Instance.CauseOfDeath = "Water";
        }
    }
}
