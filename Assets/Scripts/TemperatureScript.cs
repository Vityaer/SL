using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureScript : MonoBehaviour {
	private SpriteRenderer Sprite;
    private Color color;
    public float timeTemperature;
    public float temperature;
    public float heatRecovery;
    public float heatAcceptance;
    private FrostEffect frostEffect;
	// Use this for initialization
	void Awake () {
		Sprite = GetComponent<SpriteRenderer>();
		if((Information.Instance?.NameLevel == "Level7")||(Information.Instance?.NameLevel == "Level8"))
			GetComponent<ColdRooster>().enabled = true;
		frostEffect = GameObject.Find("Main Camera").GetComponent<FrostEffect>();
		temperature = 0f;
	}
	
	// Update is called once per frame
	void Update(){
		ChangeInformation();
		if(!Information.Instance.isDialog){
			if(timeTemperature > 0){
				timeTemperature -= Time.deltaTime;
			}else{
				if(temperature > 0){
					if(temperature >= heatRecovery){
						temperature -= heatRecovery;
						}else{
							temperature = 0;
						}
				}
				if(temperature < 0){
					if(Mathf.Abs(temperature) >= heatAcceptance){
						temperature += heatAcceptance;
					}else{
						temperature = 0;
					}
				}
				if(temperature != 0)
					timeTemperature = 0.1f;
				changeTemperature();
			}
			if(Mathf.Abs(temperature) >= 1f){
				if(temperature < 0){
	                Information.Instance.CauseOfDeath = "TempCold";
				}else{
	                Information.Instance.CauseOfDeath = "TempHot";
				}
				GetComponent<PlayerHP>().GetClearDamage(9999);
			}
		}
	}
	void changeTemperature () {
		if(frostEffect){
			float Temp;
			if(temperature <= 0){
				Temp = Mathf.Abs(temperature);
			}else{
				Temp = 0;
			}
			if(frostEffect.FrostAmount > Temp)
				frostEffect.FrostAmount -= 0.001f; 
			if(frostEffect.FrostAmount < Temp)
				frostEffect.FrostAmount += 0.001f; 
			if(frostEffect.FrostAmount - Temp <= 0.01f)
				frostEffect.FrostAmount = Temp; 
		}		
		if(temperature > 0)
			color = new Color(1f,1f - temperature,1f,1f);
		if(temperature == 0)
			color = new Color(1f,1f,1f,1f);
		if(temperature < 0)
			color = new Color(1f + temperature,1f,1f,1f);
        Sprite.color = color;
	}
	void ChangeInformation(){
		if(Information.Instance != null)
	        Information.Instance.temperature = temperature;
	}
	void OnDestroy(){
		temperature = 0f;
	}
}
