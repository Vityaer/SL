using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfForDeath : MonoBehaviour {

	public GameObject Wolf;
	private int k = 0;
	private Vector3 Position;
	void Start () {
		k = 0;
		Position = GetComponent<Transform>().position + new Vector3(-4,0,0);
	}
	void Update(){
		if(k < 4){
			Position += new Vector3(-2,0,0);
			Instantiate(Wolf,Position,gameObject.transform.rotation);
			k++;
		}else{
			Destroy(this);
		}
	}
}
