using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class SteamAchievementsScript : MonoBehaviour{
    private static SteamAchievementsScript instance;
	public static SteamAchievementsScript Instance{get => instance;}
    void Awake(){
        instance = this;
    }

    public void UnlockAchievment(string ID){
        StartCoroutine( ISetAchievment(ID) );
    }
    
    IEnumerator ISetAchievment(string ID){
        bool success = false, unlockAchievment = false;
        float timeAwait = 20f;
    	SteamUserStats.GetAchievement(ID, out unlockAchievment);
        if(!unlockAchievment){
            while((timeAwait >= 0f) && (!success)){
        		success = SteamUserStats.SetAchievement(ID);
                SteamAPI.RunCallbacks();
                timeAwait -= Time.deltaTime; 
                yield return null;
            }
            if(success) SteamUserStats.StoreStats();
        }
    }

}
