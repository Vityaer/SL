using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.Audio;
using System.IO;
using System.Xml;

public class MenuOnScript : MonoBehaviour {
	public Image Light_on;
	public Image Light_Logo;
	public Image Light_off; 
	public Image on;
	public Image off;
	public Sprite on_eng;
	public Sprite off_eng;
	public Sprite Light_off_eng;
	public Sprite Light_on_eng;
	public Sprite on_rus;
	public Sprite off_rus;
	public Sprite Light_off_rus;
	public Sprite Light_on_rus;
	public Image Logo;
	public String NameLevel;
	public int NumLanguage;
	
	public Canvas SettingsMenu;
	public Image Settings;
    public Button exitText;
    public Text NewGame;
    public Text ContinueGame;
	public Canvas StartMenu;
	public Canvas StartOrContinue;
    public Dropdown Languages;
    private String outText;
    public AudioClip SoundMouseEnter;
    public AudioClip SoundMouseClick;  
	void Start () {
		var culture = System.Globalization.CultureInfo.CurrentCulture;
	    if (culture.ToString() == "ru-RU"){
	    	ChangeLanguage(0);
	    }else{
	    	ChangeLanguage(1);
	    }
		SaveLoad.Instance.loadInformation();
		NameLevel = Information.Instance.NameLevel;
		StartMenu = StartMenu.GetComponent<Canvas>();
		SettingsMenu = GameObject.Find("SettingsMenu").GetComponent<Canvas>();
		Settings = Settings.GetComponent<Image>();
		NewGame = NewGame.GetComponent<Text>();
		ContinueGame = ContinueGame.GetComponent<Text>();
		Light_off = Light_off.GetComponent<Image>();
	    Light_Logo = Light_Logo.GetComponent<Image>();
	    Light_on = Light_on.GetComponent<Image>();
	    Logo = Logo.GetComponent<Image>();
	    on = on.GetComponent<Image>();
	    off = off.GetComponent<Image>();
		Cursor.visible = true;

		if(Screen.width < 700){
            Logo.GetComponent<RectTransform>().localScale = new Vector3(1.25f, 1f, 1f);
        }
        if(Screen.width >= 700){
            Logo.GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.2f, 1.2f);
        }
        if(Screen.width >= 1000){
            Logo.GetComponent<RectTransform>().localScale = new Vector3(3f, 2.4f, 2.4f);
        }
	}
	public void OnMouseEnter () {
		Light_Logo.enabled = true;
		Light_on.enabled = true;
		off.enabled = false;
	}
	public void OnMouseExit () {
		Light_Logo.enabled = false;
		Light_on.enabled = false;
		off.enabled = true;
	}
	
	public void OffMouseEnter () {
		Logo.enabled = false;
		on.enabled = false;
		Light_off.enabled = true;
	}
	
	public void OffMouseExit () {
		Logo.enabled = true;
		on.enabled = true;
		Light_off.enabled = false;
	}
	
	public void onMousePress(){
        FadeInOut.sceneStarting = false;
		FadeInOut.nextLevel = NameLevel;
		if(NameLevel == "Prolog"){
			FadeInOut.sceneEnd = true;
		}else{
			StartMenu.enabled = false;
			StartOrContinue.enabled = true;
		}
	}
	public void NewGamePress(){
		if(File.Exists(Application.dataPath + "/Game.sl")){
			File.Delete(Application.dataPath + "/Game.sl");
			var obj = Information.Instance;
			obj.game = new Game();
		}	
		FadeInOut.nextLevel = "Prolog";
		FadeInOut.sceneEnd = true;
	}
	public void ContinueGamePress(){
		FadeInOut.sceneEnd = true;
	}
	public void offMousePress(){
		Application.Quit();
	}
	
	public void SettingsPress(){
		SettingsMenu.enabled = true;
        StartMenu.enabled = false;
	}

	 public void BackPress() {
       SettingsMenu.enabled = false;
       StartMenu.enabled = true;
    }
    public void PlayMouseEnter(){
    	if(GameObject.Find("Sounds").GetComponent<AudioSource>()){
	        GameObject.Find("Sounds").GetComponent<AudioSource>().PlayOneShot(SoundMouseEnter);
    	}
    }
    public void PlayMouseClick(){
    	if(GameObject.Find("Sounds").GetComponent<AudioSource>()){
	        GameObject.Find("Sounds").GetComponent<AudioSource>().PlayOneShot(SoundMouseClick);
	    }    
    }
    public void ChangeLanguage(int _NumLanguage){
    	NumLanguage = _NumLanguage;
    	if(NumLanguage == 0){
	    	on.sprite = on_rus;
	    	off.sprite = off_rus;
	    	Light_off.sprite = Light_off_rus;
	    	Light_on.sprite = Light_on_rus;
    	}
    	if(NumLanguage == 1){
	    	on.sprite = on_eng;
	    	off.sprite = off_eng;
	    	Light_off.sprite = Light_off_eng;
	    	Light_on.sprite = Light_on_eng;
    	}
    }
}
