using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdRooster : MonoBehaviour {
	public float TemperatureOutside;
	TemperatureScript temperatureScript;
	public float TimerAttack = 0.1f;
	void Awake(){
		temperatureScript = GetComponent<TemperatureScript>();
	}
	void Start (){
		TimerAttack = 0.1f;
	}
	void Update () {
    	if(!Information.Instance.isDialog){
			if (TimerAttack > 0){
	            TimerAttack -= Time.deltaTime;
	        }else{
	        		
	        	temperatureScript.temperature -= TemperatureOutside;
	        	TimerAttack = 0.1f;
	        }
    	}
	}
}
