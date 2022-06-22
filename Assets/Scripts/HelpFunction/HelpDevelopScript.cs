using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpDevelopScript : MonoBehaviour{
	public GameObject information;
	void Awake(){
		if(Information.Instance == null){
			Instantiate(information, transform.position, transform.rotation);
		}
	}

}