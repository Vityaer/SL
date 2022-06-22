using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogPoint : MonoBehaviour{
	bool trigger = false;
	void OnTriggerEnter2D(Collider2D coll){
		if(trigger == false){
			if(coll.CompareTag("Player")){
				trigger = true;
				Dialogs.Instance.OpenDialog(gameObject.name);
				Destroy(gameObject);
			}

		}
	}
}
