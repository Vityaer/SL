using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public delegate void Del(float num);
public static class ObserverSoundLevel{
	public static Del dels;
	public static void Register(Del d){
		dels += d;
	}
	public static void UnRegister(Del d){
		dels -= d;
	}
	public static float level;
	public static void ChangeLevel(float num){
		level = num;
		if(dels != null) dels(num);
	}
}

public static class ObserverMusicLevel{
	public static Del dels;
	public static void Register(Del d){
		dels += d;
	}
	public static void UnRegister(Del d){
		dels -= d;
	}
	public static float level;
	public static void ChangeLevel(float num){
		level = num;
		if(dels != null) dels(num);
	}
}

public class LevelSoundControllerScript : MonoBehaviour{
	public Slider musicLevel;
	public Slider soundLevel;
	void Start(){
        soundLevel = GameObject.Find("SliderLevelSound").GetComponent<Slider>();
		musicLevel = GameObject.Find("SliderLevelMusic").GetComponent<Slider>();
	}
	void OnLevelWasLoaded(){
		ChangeLevelMusic();
		ChangeLevelSound();
	}
	public void ChangeLevelMusic(){
		if(musicLevel != null)
			ObserverMusicLevel.ChangeLevel(musicLevel.value);
	}
	public void ChangeLevelSound(){
		if(soundLevel != null)
			ObserverSoundLevel.ChangeLevel(soundLevel.value);
	}
}
