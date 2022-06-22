using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class GameInput : MonoBehaviour {
	[SerializeField] private string fileName = "input.settings"; // имя файла сохранения с разрешением
	[SerializeField] private InputComponent[] _input; // массив кнопок
	public float SoundLevel, MusicLevel;
	public GameObject SoundLevelObject, MusicLevelObject; 
	public GameObject DropdownLanguage; 

	private static GameInput gameInput;

	public static GameInput Key{
		get{ return gameInput; }
	}

	void Awake(){
		if(gameInput != null) {
			Destroy(gameObject);
		}else{
			gameInput = this;
			DontDestroyOnLoad(gameObject);
		}
	}	
	void Start(){	
		SoundLevelObject = GameObject.Find("SliderLevelSound");
		MusicLevelObject = GameObject.Find("SliderLevelMusic");
		DropdownLanguage = GameObject.Find("SelectLanguage");
		LoadSettings();

	}
	public KeyCode FindKey(string name){ // поиск кей-кода по имени
		for(int i = 0; i < _input.Length; i++){
			if(name == _input[i].defaultKeyName) return _input[i].keyCode;
		}
		return KeyCode.None;
	}

	int GetInt(string text){
		int value;
		if(int.TryParse(text, out value)) return value;
		return 0;
	}
	float GetFloat(string text){
		float _f;
		if(float.TryParse(text, out _f)) return _f;
		return 0;
	}

	string Path(){ // путь сохранения
		return Application.dataPath + "/" + fileName;
	}

	void SetKey(string value){ // настройка кнопок
		if(_input[0] != null){
			string[] result = value.Split(new char[]{'='});
			for(int i = 0; i < _input.Length; i++){
				if(result[0] == _input[i].defaultKeyName){
					_input[i].keyCode = (KeyCode)GetInt(result[1]);
					_input[i].buttonText.text = _input[i].keyCode.ToString();
				}
				if(result[0] == "SoundLevel"){
					SoundLevelObject.GetComponent<Slider>().value = GetFloat(result[1]);
				}
				if(result[0] == "MusicLevel"){
					MusicLevelObject.GetComponent<Slider>().value = GetFloat(result[1]);
				}
				if(result[0] == "NumLanguage"){
					DropdownLanguage.GetComponent<Dropdown>().value = GetInt(result[1]);
					Information.Instance.LanguageNumber = GetInt(result[1]);
				}
			}
		}
	}

	public void DefaultSettings(){ // возврат настроек по умолчанию
		Debug.Log("DefaultSettings");
		for(int i = 0; i < _input.Length; i++){
			_input[i].keyCode = _input[i].defaultKeyCode;
			_input[i].buttonText.text = _input[i].defaultKeyCode.ToString();
		}
		var culture = System.Globalization.CultureInfo.CurrentCulture;
		int numLanguage = 1;
	    if (culture.ToString() == "ru-RU"){
	    	numLanguage = 0;
	    }else{
	    	numLanguage = 1;
	    }
		SoundLevelObject.GetComponent<Slider>().value = 0.5f;
		MusicLevelObject.GetComponent<Slider>().value = 0.5f;
		DropdownLanguage.GetComponent<Dropdown>().value = numLanguage;
    	Information.Instance.LanguageNumber = numLanguage;
	}

	public void LoadSettings(){ // загрузка установок
		if(!File.Exists(Path())){
			DefaultSettings();
			return;
		}
		Debug.Log("load Settings");
		StreamReader reader = new StreamReader(Path());
		while(!reader.EndOfStream){
			SetKey(reader.ReadLine());
		}
		reader.Close();
	}

	public void SaveSettings(){
		StreamWriter writer = new StreamWriter(Path());

		for(int i = 0; i < _input.Length; i++){
			writer.WriteLine(_input[i].defaultKeyName + "=" + (int)_input[i].keyCode);
		}
		writer.WriteLine("SoundLevel=" + (float) SoundLevelObject.GetComponent<Slider>().value);
		writer.WriteLine("MusicLevel=" + (float) MusicLevelObject.GetComponent<Slider>().value);
		writer.WriteLine("NumLanguage=" + (int) DropdownLanguage.GetComponent<Dropdown>().value);
		writer.Close();
		Information.Instance.LanguageNumber = DropdownLanguage.GetComponent<Dropdown>().value;
		if(Dialogs.Instance != null)
			Dialogs.Instance.LanguageNumber = Information.Instance.LanguageNumber;
	}

	public bool GetKey(string name){
		return Input.GetKey(FindKey(name));
	}

	public bool GetKeyDown(string name){
		return Input.GetKeyDown(FindKey(name));
	}

	public bool GetKeyUp(string name){
		return Input.GetKeyUp(FindKey(name));
	}
}