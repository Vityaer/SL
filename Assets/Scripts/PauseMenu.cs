using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public static class ObserverTimeScale{
    public static Del dels;
    public static void Register(Del d){
        dels += d;
    }
    public static void UnRegister(Del d){
        dels -= d;
    }
    public static float level = 1f;
    public static void ChangeLevel(float num){
        level = num;
        if(dels != null) dels(num);
    }
}

public class PauseMenu : MonoBehaviour {

    public Canvas quitMenu;
    public bool flagOpen = false;
    public bool FlagSettings = false;
    [SerializeField] Text ButtonContinue, ButtonSettings, ButtonRestart , ButtonExitInMainMenu;
    void Avake(){
        Cursor.visible = false;
    }
	void Start () {
        Cursor.visible = false;
        CheckScale();
	    quitMenu = GetComponent<Canvas>();
        quitMenu.enabled = false;

	}
	 void Update(){
    	if (Input.GetKeyUp(KeyCode.Escape)){
            Cursor.visible = flagOpen ? true : false;
            OnOffMenu();
        }else{
            if(!flagOpen && Cursor.visible)
                Cursor.visible = false;
        }   
    }
    public void OnOffMenu(){
        flagOpen = !flagOpen;
        if(flagOpen){
            Cursor.visible = true;
            Time.timeScale = 0;
            ObserverTimeScale.ChangeLevel(0f);
            quitMenu.enabled = true;
            GameObject Player = GameObject.FindWithTag("Player");
            if(Player.GetComponent<RoosterScript>()) 
                Player.GetComponent<RoosterScript>().isGame = false;
            if(Player.GetComponent<RoosterScriptWithShield>()) 
                Player.GetComponent<RoosterScriptWithShield>().isGame = false;
            if(Player.GetComponent<RoosterScriptWizard>()) 
                Player.GetComponent<RoosterScriptWizard>().isGame = false;
        }else{
            GoPress();
        }
    }
    public void GoPress(){
        Cursor.visible = false;
        flagOpen = false;
        quitMenu.enabled = false;
	    Time.timeScale = 1;
        ObserverTimeScale.ChangeLevel(1f);
        GameObject Player = GameObject.FindWithTag("Player");
        if(Player.GetComponent<RoosterScript>()) 
            Player.GetComponent<RoosterScript>().isGame = true;
        if(Player.GetComponent<RoosterScriptWithShield>()) 
            Player.GetComponent<RoosterScriptWithShield>().isGame = true;
        if(Player.GetComponent<RoosterScriptWizard>()) 
            Player.GetComponent<RoosterScriptWizard>().isGame = true;
    }
    public void OpenSettings(){
        FlagSettings = true;
        SettingsMenuScript.Instance.Open();
        quitMenu.enabled = false; 
    } 
    public void RestartLevel(){
        Cursor.visible = false;
        GameObject Player = GameObject.FindWithTag("Player");
        Player.GetComponent<PlayerHP>().StopAnimDeath();
        Player.GetComponent<PlayerHP>().enabled = false;
        ReturnInformation();
        FadeInOut.sceneStarting = false;
        FadeInOut.nextLevel = Information.Instance.NameLevel; 
        FadeInOut.sceneEnd = true;
    }
    public void InMainMenu(){
        GameObject Player = GameObject.FindWithTag("Player");
        Player.GetComponent<PlayerHP>().StopAnimDeath();
        ReturnInformation();
        FadeInOut.sceneStarting = false;
        FadeInOut.nextLevel = "Scene0"; 
        FadeInOut.sceneEnd = true;
    }
    void ReturnInformation(){
        GameObject.Find("Dialogs").GetComponent<Canvas>().enabled = false; 
        Information.Instance.Reestablish();
        Information.Instance.temperature = 0f;
        quitMenu.enabled = false;
        Time.timeScale = 1;
        
    }
    void CheckScale(){
        if(Screen.width <= 600){
            ButtonContinue.fontSize = 60;
            ButtonSettings.fontSize = 60;
            ButtonRestart.fontSize = 60;
            ButtonExitInMainMenu.fontSize = 60;
        }
        if(Screen.width > 600){
            ButtonContinue.fontSize = 90;
            ButtonSettings.fontSize = 90;
            ButtonRestart.fontSize = 90;
            ButtonExitInMainMenu.fontSize = 90;
        }
        if(Screen.width > 1200){
            ButtonContinue.fontSize = 120;
            ButtonSettings.fontSize = 120;
            ButtonRestart.fontSize = 120;
            ButtonExitInMainMenu.fontSize = 120;
        }
        if(Screen.width > 1600){
            ButtonContinue.fontSize = 150;
            ButtonSettings.fontSize = 150;
            ButtonRestart.fontSize = 150;
            ButtonExitInMainMenu.fontSize = 150;
        }
    }
}