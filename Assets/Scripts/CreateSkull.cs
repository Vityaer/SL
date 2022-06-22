using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSkull : MonoBehaviour {
	public GameObject Skull;
    public Transform tr;
	public float Timer = 3f;
	void Start(){
		tr = GetComponent<Transform>();
	}
	void Update () {
		if(Timer >= 0){
			Timer -= Time.deltaTime;
		}else{
			Instantiate(Skull,tr.position, tr.rotation);
			Timer = 3f;
		}		
	}
}
