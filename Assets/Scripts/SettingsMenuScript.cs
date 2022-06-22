using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuScript : MonoBehaviour {
	public GameObject InformationSave;
	public Text Save;
	public Text Default;
	public Text Close;
	public Text Right;
	public Text Left;
	public Text Up;
	public Text Down;
	public Text GetUp;
	public Text Magic1;
	public Text Magic2;
	public Text Magic3;
	public Text Block;
	public Text Jump;
	public Text Strike;
	public Text LevelSounds;
	public Text LevelMusic;
	public Text InfoMessage;
    [Header("Language")]
	public Dropdown Language;
    private int numLanguage = 1;

    [Header("Audio")]
    private AudioSource aSource;
    public AudioClip SoundMouseEnter;
    public AudioClip SoundMouseClick; 
    private Canvas settingsCanvas;
	void Awake () {
        aSource = GetComponent<AudioSource>();
		if(GameObject.Find("SettingsMenu")){
			Destroy(gameObject);
		}else{
            settingsCanvas = GetComponent<Canvas>();
            instance = this;
            gameObject.name = "SettingsMenu";
    		InformationSave.SetActive(false);
    		// DontDestroyOnLoad(gameObject);
            ObserverSoundLevel.Register( ChangeLevelSound );
            ChangeLanguage();
        }	
	}
    [SerializeField] bool isOpen = false;
	void Update(){
        if(isOpen == true){
    		if (Input.GetKeyUp(KeyCode.Escape)){
    			CloseSettings();
    		}
        }
	}
	void OnLevelWasLoaded(){
        numLanguage = Information.Instance.LanguageNumber;
        Language.value = numLanguage;
		ChangeLanguage();
	}
    public void Open(){
        isOpen = true;
        settingsCanvas.enabled = true;
    }
	public void CloseInformationSave(){
		InformationSave.SetActive(false);
		CloseUI();
	}
	public void EnterInformationSave(){
        GameInput.Key.SaveSettings();
		InformationSave.SetActive(true);
	}
    private void CloseUI(){
        settingsCanvas.enabled = false;
        if(GameObject.Find("StrartMenu")){
            GameObject.Find("StrartMenu").GetComponent<Canvas>().enabled = true;
        }
        if(GameObject.Find("MenuPause")){
            GameObject.Find("MenuPause").GetComponent<Canvas>().enabled = true;
            GameObject.Find("MenuPause").GetComponent<PauseMenu>().FlagSettings = false;
        }
    }
	public void CloseSettings(){
        isOpen = false;
		InformationSave.SetActive(false);
        CloseUI();
        GameInput.Key.LoadSettings();
    }
    public void ChangeLanguage(){
        numLanguage = Language.value;
        if(GameObject.Find("StrartMenu")){
            GameObject.Find("StrartMenu").GetComponent<MenuOnScript>().ChangeLanguage(numLanguage);
        }
    	if(numLanguage == 1){
            Save.text = "Save";
            Default.text = "Default";
            Close.text = "Close";
            Right.text = "Right";
            Left.text = "Left";
            Up.text = "Up";
            Down.text = "Down";
            GetUp.text = "Get up";
            Magic1.text = "Magic 1";
            Magic2.text = "Magic 2";
            Magic3.text = "Magic 3";
            Block.text = "Block";
            Jump.text = "Jump";
            Strike.text = "Strike";
            LevelSounds.text = "Sounds";
            LevelMusic.text = "Music";
            InfoMessage.text = "Saved!";
            if(GameObject.Find("StartOrContinue")){
            	GameObject obj = GameObject.Find("StartOrContinue").transform.Find("Quit").gameObject;
            	obj.transform.Find("BtnNewGame").gameObject.GetComponent<Text>().text = "New game";
            	obj.transform.Find("BtnContinue").gameObject.GetComponent<Text>().text = "Continue";
            	obj.transform.Find("Text").gameObject.GetComponent<Text>().text = "Continue or new game?";
            }
            if(GameObject.Find("QuitMenu")){
            	GameObject obj = GameObject.Find("QuitMenu").transform.Find("Quit").gameObject;
            	obj.transform.Find("Question").gameObject.GetComponent<Text>().text = "Go out?";
            	obj.transform.Find("No").gameObject.GetComponent<Text>().text = "No";
            	obj.transform.Find("Yes").gameObject.GetComponent<Text>().text = "Yes";
            }
            if(GameObject.Find("MenuPause/Image/Panel/Panel/ButtonContinue")){
            	GameObject.Find("MenuPause/Image/Panel/Panel/ButtonContinue").GetComponent<Text>().text = "Continue";
            	GameObject.Find("MenuPause/Image/Panel/Panel/ButtonSettings").GetComponent<Text>().text = "Settings";
            	GameObject.Find("MenuPause/Image/Panel/Panel/ButtonRestart").GetComponent<Text>().text = "Restart";
            	GameObject.Find("MenuPause/Image/Panel/Panel/ButtonExitInMainMenu").GetComponent<Text>().text = "In main menu";
            }

        }else if(numLanguage == 0){
            Save.text = "Сохранить";
            Default.text = "По умолчанию";
            Close.text = "Закрыть";
            Right.text = "Вправо";
            Left.text = "Влево";
            Up.text = "Вверх";
            Down.text = "Вниз";
            GetUp.text = "Поднять";
            Magic1.text = "Магия 1";
            Magic2.text = "Магия 2";
            Magic3.text = "Магия 3";
            Block.text = "Блок";
            Jump.text = "Прыжок";
            Strike.text = "Удар";
            LevelSounds.text = "Звуки";
            LevelMusic.text = "Музыка";
            InfoMessage.text = "Сохранено!";
            if(GameObject.Find("StartOrContinue")){
            	GameObject obj = GameObject.Find("StartOrContinue").transform.Find("Quit").gameObject;
            	obj.transform.Find("BtnNewGame").gameObject.GetComponent<Text>().text = "Новая игра";
            	obj.transform.Find("BtnContinue").gameObject.GetComponent<Text>().text = "Продолжить";
            	obj.transform.Find("Text").gameObject.GetComponent<Text>().text = "Продолжить или начать новую игру?";
            }
            if(GameObject.Find("QuitMenu")){
            	GameObject obj = GameObject.Find("QuitMenu").transform.Find("Quit").gameObject;
            	obj.transform.Find("Question").gameObject.GetComponent<Text>().text = "Вы действительно хотите выйти?";
            	obj.transform.Find("No").gameObject.GetComponent<Text>().text = "Нет";
            	obj.transform.Find("Yes").gameObject.GetComponent<Text>().text = "Да";
            }
            if(GameObject.Find("MenuPause/Image/Panel/Panel/ButtonContinue")){
            	GameObject.Find("MenuPause/Image/Panel/Panel/ButtonContinue").GetComponent<Text>().text = "Продолжить";
            	GameObject.Find("MenuPause/Image/Panel/Panel/ButtonSettings").GetComponent<Text>().text = "Настройки";
            	GameObject.Find("MenuPause/Image/Panel/Panel/ButtonRestart").GetComponent<Text>().text = "Рестарт";
            	GameObject.Find("MenuPause/Image/Panel/Panel/ButtonExitInMainMenu").GetComponent<Text>().text = "Выход в меню";
            }
        }
    }  
    public void ChangeLevelSound(float num){
        aSource.volume = num/2f;
    }
    public void PlayMouseEnter(){
        aSource.PlayOneShot(SoundMouseEnter);
    }
    public void PlayMouseClick(){
        aSource.PlayOneShot(SoundMouseClick);
    }
    void OnDestroy(){
        ObserverSoundLevel.UnRegister( ChangeLevelSound );
    }
    private static SettingsMenuScript instance;
    public static SettingsMenuScript Instance{get => instance;} 
}