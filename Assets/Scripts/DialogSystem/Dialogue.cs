using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue{
	[SerializeField] private List<Speech> russianSpeechs = new List<Speech>();
	[SerializeField] private List<Speech> englishSpeechs = new List<Speech>();
	[SerializeField] private int currentSpeech = 0;
	[SerializeField] private bool isFinish = false;
	public List<string> GetArrayLine(int LanguageNumber){
		List<string> result = new List<string>();
		List<Speech> workList = new List<Speech>();
		GetDialogueOnLanguage(ref workList, LanguageNumber);
		Debug.Log(workList.Count.ToString());
		for(int i = 0; i < workList.Count; i++){
			result.AddRange(workList[i].Lines);
		}
		return result;
	}
	public List<string> GetArraySpeakers(int LanguageNumber){
		List<string> result = new List<string>();
		List<Speech> workList = new List<Speech>();
		GetDialogueOnLanguage(ref workList, LanguageNumber);
		
		for(int i = 0; i < workList.Count; i++){
			for(int j = 0; j < workList[i].Lines.Count; j++){
				result.Add(workList[i].GetSpeecher);
			}
		}
		return result;
	}
	private void GetDialogueOnLanguage(ref List<Speech> workList, int LanguageNumber){
		Debug.Log(LanguageNumber);
		Debug.Log(russianSpeechs.Count.ToString());
		Debug.Log(englishSpeechs.Count.ToString());
		switch(LanguageNumber){
			case 0: 
				workList = russianSpeechs;
				break;
			case 1:
				workList = englishSpeechs;
				break;
			default:
				workList = englishSpeechs;
				break;
		}
	}
	public bool IsFinish{get => isFinish;}
}