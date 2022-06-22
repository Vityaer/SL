using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Xml;
public class SaveLoad : MonoBehaviour {
	private static SaveLoad instance;
	public static SaveLoad Instance{ get{return instance;} }
	void Awake(){
		if(Instance == null){
			instance = this;
		}else{
			Destroy(gameObject);
		}
	}
	[SerializeField] private string fileName = "Game";
	public bool isCrypt = false;
	public void saveInformation(Game game){
		if(game.health > 0){
			SaveGame save = new SaveGame(game);
			StreamWriter sw = new StreamWriter(Application.dataPath + "/" + fileName + ".sl", false);
			string sp = " ";
			sw.WriteLine(Crypt(JsonUtility.ToJson(save)));
			sw.Close();
		}
	}
	
	public void loadInformation(){
		Information obj = Information.Instance;
		if(File.Exists(Application.dataPath + "/" + fileName + ".sl")){
			string[] rows = File.ReadAllLines(Application.dataPath + "/" + fileName + ".sl");
			if(rows.Length > 1){
				obj.NameLevel = GetValue(rows, "key_LevelName");
				float _Health;
				if(float.TryParse(GetValue(rows, "key_Healt"), out _Health)) {obj.StartHealth = (int)_Health; ObserverHP.level = (int)_Health;}
				int _i;
				if(int.TryParse(GetValue(rows, "key_Gold"), out _i)) {obj.StartGold = _i; obj.Gold = _i;}
				if(int.TryParse(GetValue(rows, "key_MP"), out _i)) {obj.StartMP = _i; obj.MP = _i;}
				if(int.TryParse(GetValue(rows, "key_armor"), out _i)) obj.armor = _i;
				if(int.TryParse(GetValue(rows, "key_weapon"), out _i)) obj.weapon = _i;
				bool _OptionGirl;
				if(bool.TryParse(GetValue(rows, "key_OptionGirl"), out _OptionGirl)) obj.OptionGirl = _OptionGirl;
				obj.game.nameCheckPoint = "";
			}else{
				obj.game = new Game(JsonUtility.FromJson<SaveGame>(GetValue(rows[0])));
			}
		
		}else{
			obj.NameLevel   = "Prolog";
			obj.game.nameCheckPoint = "";
			obj.StartHealth = 10;
			obj.StartGold   = 0;
			obj.StartMP     = 0;
			ObserverHP.level = 10;
			obj.Gold        = 0;
			obj.MP          = 0;
			obj.armor       = 0;
			obj.weapon      = 0;
			obj.OptionGirl  = false;	
			obj.fatherGirlFound  = false;	
		}
	}
	string Crypt(string text){
		if(!isCrypt) return text;
		string result = string.Empty;
		foreach (char j in text){
			// ((int) j ^ 49) - применение XOR к номеру символа
			// (char)((int) j ^ 49) - получаем символ из измененного номера
			// Число, которым мы XORим можете поставить любое. Эксперементируйте.
			result += (char)((int)j ^ 49);
		}
		return result;
	}
	string GetValue(string[] line, string pattern){
		string result = "";
		foreach(string key in line){
			if(key.Trim() != string.Empty){
				string value = "";
				if(isCrypt) value = Crypt(key); else value = key;
				if(pattern == value.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries)[0]){
					result = value.Remove(0, value.IndexOf(' ')+1);
				}
			}
		}
		return result;
	}
	string GetValue(string text){
		string result = "";
		result = (isCrypt) ? Crypt(text) : text;
		return result; 
	}
}