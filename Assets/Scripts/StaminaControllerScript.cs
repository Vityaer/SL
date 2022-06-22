using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelpFuction;

public class StaminaControllerScript : MonoBehaviour{
	public static float max = 15f;
	public float current;
	public float timeRecovery = 0.1f, speedRecovery = 0.2f;
	public float costStrike, costBlock;

	void Start(){
		current = Information.Instance.stamina;
		StaminaSliderScript.Instance.maxStamina = max;
		recoveryTimer = TimerScript.Timer.StartTimer(timeRecovery, RecoveryStamina);
	}

	float timerWait;
	bool isFlagRecovery = true;
	void Update(){
		if(timerWait > 0){ timerWait -= Time.deltaTime; }else{ isFlagRecovery = true; }
	}

//API
	public bool ResolutionForAttack(){ return (current >= costStrike); }
	public void DecreaseAttackStamina(){DecreaseStamina(costStrike);}
	public bool ResolutionForBlock() { return (current >= costBlock ); }
	public void DecreaseBlockStamina(){DecreaseStamina(costBlock);}

//Core
	private GameTimer recoveryTimer;
	private void RecoveryStamina(){
		if(isFlagRecovery){
			if(current < max) current += speedRecovery;
			if(current > max) current = max;
			ChangeUI();
		}
		recoveryTimer = TimerScript.Timer.StartTimer(timeRecovery, RecoveryStamina);
	}

	public void DecreaseStamina(float count){
		timerWait = 2f;
		isFlagRecovery  = false;
		current -= count;
		if(current <= 0f) current = 0f;
		ChangeUI(); 
	}
	private void ChangeUI(){
		StaminaSliderScript.Instance.ChangeStamina(current);
	}

	void OnDestroy(){
		TimerScript.Timer.StopTimer(recoveryTimer);
	}
}
