using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Xml;
using UnityEditor;
public class SaveLoadProgressAchievement : MonoBehaviour{
	private static SaveLoadProgressAchievement instance;
	public static SaveLoadProgressAchievement Instance{get => instance;}
    private static string NameFileAchievement                = "FileAchievement";
    public List<ProgressAchievement> listAchievements               = new List<ProgressAchievement>();
    public List<ProgressAchievement> defaultListAchievements = new List<ProgressAchievement>();
    void Awake(){
    	instance = this;
    	listAchievements = LoadProgressAchievements();
    	
    }
    public void AddPointInProgressAchievement(string name){
    	string nameAchievement = "";
    	switch (name){
    		case "DeathRooster":
    			nameAchievement = "ACH_ALWAYS_DEATH";
    			break;
    		case "DeathEnemy":
    			nameAchievement = "ACH_ENEMIES_DEATH";
    			break;	
    	}
    	for(int i=0; i < listAchievements.Count; i++){
    		if(listAchievements[i].Name.Equals(nameAchievement)){
    			listAchievements[i].AddPoint();
    			break;
    		}
    	}
    	SaveProgressAchievements(listAchievements);
    }
//API
	//Game status
		public void SaveProgressAchievements(List<ProgressAchievement> listAchievements){
	    	StreamWriter sw = CreateStream(NameFileAchievement, false);
	    	foreach(ProgressAchievement ach in listAchievements){
			    sw.WriteLine( JsonUtility.ToJson(ach) );
	    	}
			sw.Close();
		}


		public List<ProgressAchievement> LoadProgressAchievements(){
			List<ProgressAchievement> result = new List<ProgressAchievement>();
		    CheckFile(NameFileAchievement);
			List<string> rows = ReadFile(NameFileAchievement);
			if(rows.Count > 0){
				foreach(string row in rows){
					result.Add(JsonUtility.FromJson<ProgressAchievement>(row)); 
				}
			}else{
				foreach(ProgressAchievement ach in defaultListAchievements){
					result.Add(new ProgressAchievement(ach.Name, ach.targetCount));
				}
			}

			return result;
		}
//Core
	private static string GetPrefix(){
			string prefixNameFile;
			prefixNameFile = Application.dataPath;	
			return prefixNameFile;
	}
	private static List<string> ReadFile(string NameFile){
		List<string> ListResult = new List<string>(); 
		try{
			ListResult = new List<string>(File.ReadAllLines(GetPrefix() + "/" + NameFile + ".data"));
		}catch{}
		return ListResult;
	}
	private static StreamWriter CreateStream(string NameFile, bool AppendFlag){
    	return new StreamWriter(GetPrefix() + "/" + NameFile + ".data", append: AppendFlag);
    	
    }
    public static void CheckFile(string NameFile){
    	if(!File.Exists(GetPrefix() + "/" + NameFile + ".data")){
    		CreateFile(NameFile);
    	}
    }
    public static void CreateFile(string NameFile){
        StreamWriter sw = CreateStream(NameFile, false);
        sw.Close();
    }
}
[System.Serializable]
public class ProgressAchievement{
	public string Name;
	[SerializeField]
	private int currentCount;
	public int targetCount;
	public ProgressAchievement(string Name, int targetCount){
		this.Name        = Name;
		this.targetCount = targetCount;
	}
	public void AddPoint(){
		currentCount += 1;
		CheckAchievement();
	}
	public void CheckAchievement(){
		if(currentCount == targetCount){
            SteamAchievementsScript.Instance.UnlockAchievment(Name);
		}
	}
}