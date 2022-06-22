using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightObject : MonoBehaviour {
	public Material LightMaterial;
	public Material DefaultLightMaterial;
	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.GetComponent<PlayerHP>()){
			col.gameObject.GetComponent<SpriteRenderer>().material = LightMaterial;
			col.gameObject.transform.GetChild(0).gameObject.GetComponent<Light>().enabled = true;
		}
	}
	void OnTriggerExit2D(Collider2D col){
		if(col.gameObject.GetComponent<PlayerHP>()){
			col.gameObject.GetComponent<SpriteRenderer>().material = DefaultLightMaterial;
			col.gameObject.transform.GetChild(0).gameObject.GetComponent<Light>().enabled = false;
		}
	}
}
