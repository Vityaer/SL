using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour{
    private CheckPointScript checkPoint = null;
    void OnTriggerEnter2D(Collider2D col){
		if(col.CompareTag("Player")){
			if(checkPoint != this){
				checkPoint = this;
				Information.Instance.nameCheckPoint = gameObject.name;
				Information.Instance.SaveInfo();
			}
		}
	}
}
