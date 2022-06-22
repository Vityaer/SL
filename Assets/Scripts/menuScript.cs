using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class menuScript : MonoBehaviour {
    public Canvas quitMenu;
    public Canvas SettingsMenu;
    public Canvas StartMenu;

    public Button startText;
    public Button SettingsText;
    public Button exitText;

    public Dropdown Languages;
    private String outText;

	// Use this for initialization
	void Start () {
	    quitMenu = quitMenu.GetComponent<Canvas>();
        SettingsMenu = SettingsMenu.GetComponent<Canvas>();
        StartMenu = StartMenu.GetComponent<Canvas>();
        startText = startText.GetComponent<Button>();
        
        ChangeLanguage();
        quitMenu.enabled = false;
        SettingsMenu.enabled = false;

	}
 public void ChangeLanguage(){
        if(Languages.value == 1){
            startText.GetComponent<Text>().text = "Play";
            SettingsText.GetComponent<Text>().text = "Settings";
            exitText.GetComponent<Text>().text = "Exit";
            GameObject.Find("Back").GetComponent<Text>().text = "Back";
            GameObject.Find("No").GetComponent<Text>().text = "No";
            GameObject.Find("Yes").GetComponent<Text>().text = "Yes";
            GameObject.Find("Language").GetComponent<Text>().text = "Language";
            GameObject.Find("Question").GetComponent<Text>().text = "Will you want to exit?";
        }
        if(Languages.value == 0){
            startText.GetComponent<Text>().text = "Играть";
            SettingsText.GetComponent<Text>().text = "Опции";
            exitText.GetComponent<Text>().text = "Выход";
            GameObject.Find("Back").GetComponent<Text>().text = "Назад";
            GameObject.Find("No").GetComponent<Text>().text = "Нет";
            GameObject.Find("Yes").GetComponent<Text>().text = "Да";
            GameObject.Find("Language").GetComponent<Text>().text = "Язык";
            GameObject.Find("Question").GetComponent<Text>().text = "Вы действительно хотите выйти?";
        }
    }
// Quit
    public void ExitPress() {
        quitMenu.enabled = true;
        startText.enabled = false;
        SettingsText.enabled = false;
        exitText.enabled = false;
    }
    public void NoPress() {
        quitMenu.enabled = false;
        startText.enabled = true;
        SettingsText.enabled = true;
        exitText.enabled = true;
    }
    public void StartLevel(){
        FadeInOut.sceneEnd = true;
    }
    public void ExitGame(){
        Application.Quit ();
    }
}
