using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public static class ObserverHP{
	public static Del dels;
	public static void Register(Del d){
		dels += d;
	}
	public static void UnRegister(Del d){
		dels -= d;
	}
	private static int _level = 10;
	public static int level{get => _level; set{Debug.Log("Кто вызвал?"); _level = value; }}
	public static void ChangeHP(int num){
		level = num;
		if(dels != null) dels(num);
	}
}

public class Information : MonoBehaviour {
//Singleton
	private static Information instance;
	public static Information Instance { get{return instance;} }
	public GameObject Skin00;
	public GameObject Skin01;
	public GameObject Skin02;
	public GameObject Skin03;
	public GameObject Skin11;
	public GameObject Skin12;
	public GameObject Skin13;
	public GameObject Skin21;
	public GameObject Skin22;
	public GameObject Skin23;
	public GameObject Skin31;
	public GameObject Skin32;
	public GameObject Skin33;
	public GameObject Skin4;
	public GameObject armor1;
	public GameObject armor2;
	public GameObject armor3;
	public GameObject armor4;
	public GameObject weapon1;
	public GameObject weapon2;
	public GameObject weapon3;
private GameObject[] armors;
public int LanguageNumber = 1;
public int Gold{get => game.gold; set => game.gold = value; }
public bool Grounded;
public bool isDialog;
public bool isAttack;
public bool Attack;
public bool Block;
[SerializeField] 
private bool _isFacingRight;
public bool isFacingRight{
	get{
		return _isFacingRight;
	}
	set{
		_isFacingRight = value;
		ChangeFacingSide();
	}
}
public float temperature;
public string CauseOfDeath;
public string NameLevel{get => game.nameLevel; set => game.nameLevel = value;}
public string nameCheckPoint{get => game.nameCheckPoint; set => game.nameCheckPoint = value;}
public bool OptionGirl{get => game.optionGirl; set => game.optionGirl = value; }
public bool fatherGirlFound{get => game.fatherGirlFound; set => game.fatherGirlFound = value; }
public int MP{get => game.MP; set => game.MP = value; }
public bool isGame;
public bool isLadder;
public int armor{get => game.armor; set => game.armor = value; }
public int weapon{get => game.weapon; set => game.weapon = value; }
public int Startarmor{get => game.startArmor; set => game.startArmor = value;}
public int Startweapon{get => game.startWeapon; set => game.startWeapon = value;}
public int StartMP{get => game.startMP; set => game.startMP = value; }
public int StartHealth{get => game.startHealth; set => game.startHealth = value; }
public int StartGold{get => game.startGold; set => game.startGold = value;}
public bool StartOptionGirl{get => game.startOptionGirl; set => game.startOptionGirl = value;}
public bool StartFatherGirlFound{get => game.startFatherGirlFound; set => game.startFatherGirlFound = value;}
public Vector2 position;

private Vector3 playerPosition = new Vector3();
public Vector3 PlayerPosition{
	get{
		playerPosition.x = position.x;
		playerPosition.y = position.y;
		return playerPosition;
	}
}
public float myTimerChangeSkin;
public int[] DialogsIndex = new int[100];
GameObject Rooster;
public float stamina = 15f;
public Game game;
	void OnLevelWasLoaded(){
		stamina = 15f;
		temperature = 0f;
	}

	void Awake(){
		if(Instance == null){
			instance = this;
			DontDestroyOnLoad(gameObject);
		}else{
			Destroy(gameObject);
		}
	}
	void Start(){
		SaveLoad.Instance.loadInformation();
	}
	void Update(){
		if (myTimerChangeSkin > 0){
            myTimerChangeSkin -= Time.deltaTime;
        }   
	}
	public void SaveInfo(){
		Information.Instance.game.health = ObserverHP.level;
		SaveLoad.Instance.saveInformation(game);
	}

	public void Reestablish(){
		SaveLoad.Instance.loadInformation();
		ObserverHP.ChangeHP(StartHealth);
		Gold = StartGold;
		MP = StartMP;
		temperature = 0;
	}
	public void ChangeSkin(){
		if(ObserverHP.level == 0){
			ObserverHP.ChangeHP(StartHealth);
			Gold = StartGold;
			MP = StartMP;
			temperature = 0;
		}
		if(myTimerChangeSkin <= 0){
			if((weapon == 4) && (armor < 4)){
				weapon = 0;
			}
			if((weapon < 4) && (armor == 4)){
				armor = 0;
			}
			if((armor == 0)&&(weapon == 0)){
				Instantiate(Skin00, new Vector3(position.x, position.y, 0), GetComponent<Transform>().rotation);
			}
			if((armor == 0)&&(weapon == 1)){
				Instantiate(Skin01, new Vector3(position.x, position.y, 0), GetComponent<Transform>().rotation);
			}
			if((armor == 0)&&(weapon == 2)){
				Instantiate(Skin02, new Vector3(position.x, position.y, 0), GetComponent<Transform>().rotation);
			}
			if((armor == 0)&&(weapon == 3)){
				Instantiate(Skin03, new Vector3(position.x, position.y, 0), GetComponent<Transform>().rotation);
			}
			if((armor == 1)&&(weapon <= 1)){
				Instantiate(Skin11, new Vector3(position.x, position.y, 0), GetComponent<Transform>().rotation);
			}
			if((armor == 1)&&(weapon == 2)){
				Instantiate(Skin12, new Vector3(position.x, position.y, 0), GetComponent<Transform>().rotation);
			}
			if((armor == 1)&&(weapon == 3)){
				Instantiate(Skin13, new Vector3(position.x, position.y, 0), GetComponent<Transform>().rotation);
			}
			if((armor == 2)&&(weapon <= 1)){
				Instantiate(Skin21, new Vector3(position.x, position.y, 0), GetComponent<Transform>().rotation);
			}
			if((armor == 2)&&(weapon == 2)){
				Instantiate(Skin22, new Vector3(position.x, position.y, 0), GetComponent<Transform>().rotation);
			}
			if((armor == 2)&&(weapon == 3)){
				Instantiate(Skin23, new Vector3(position.x, position.y, 0), GetComponent<Transform>().rotation);
			}
			if((armor == 3)&&(weapon <= 1)){
				Instantiate(Skin31, new Vector3(position.x, position.y, 0), GetComponent<Transform>().rotation);
			}
			if((armor == 3)&&(weapon == 2)){
				Instantiate(Skin32, new Vector3(position.x, position.y, 0), GetComponent<Transform>().rotation);
			}
			if((armor == 3)&&(weapon == 3)){
				Instantiate(Skin33, new Vector3(position.x, position.y, 0), GetComponent<Transform>().rotation);
			}
			if((armor == 4)&&(weapon == 4)){
				Instantiate(Skin4, new Vector3(position.x, position.y, 0), GetComponent<Transform>().rotation);
			}
			if((armor != 4)&&(weapon != 4)){
				GameObject.Find("ImageMPBar").GetComponent<Image>().enabled = false;
				GameObject.Find("MPBar").GetComponent<Text>().enabled = false;
			}
			myTimerChangeSkin = 0.5f;
		}
	}
	int numArmor = 0;
	public void CreateArmor(Vector3 ArmorPosition){
		GameObject gameObjectArmor = null;
		TypeItem typeItem = TypeItem.Monet;
		if(armor == 1){
			gameObjectArmor = Instantiate(armor1, ArmorPosition, GetComponent<Transform>().rotation);
			typeItem = TypeItem.LigthArmor;
		}
		if(armor == 2){
			gameObjectArmor = Instantiate(armor2, ArmorPosition, GetComponent<Transform>().rotation);
			typeItem = TypeItem.MiddleArmor;
		}
		if(armor == 3){
			gameObjectArmor = Instantiate(armor3, ArmorPosition, GetComponent<Transform>().rotation);
			typeItem = TypeItem.HardArmor;
		}
		if(armor == 4){
			gameObjectArmor = Instantiate(armor4, ArmorPosition, GetComponent<Transform>().rotation);
			typeItem = TypeItem.WizardArmor;
		}
		if(gameObjectArmor != null){
			gameObjectArmor.name = string.Concat("armorCreated", (numArmor++).ToString());  
			Information.Instance.game.AddCteatedItem(gameObjectArmor.name, typeItem, ArmorPosition);
		}
	}
	int numWeapon = 0;
	public void CreateWeapon(Vector3 WeaponPosition){
		GameObject gameObjectWeapon = null;
		TypeItem typeItem = TypeItem.Monet;
		if(weapon == 1){
			Instantiate(weapon1, WeaponPosition, GetComponent<Transform>().rotation);
			typeItem = TypeItem.Dagger;
		}
		if(weapon == 2){
			Instantiate(weapon2, WeaponPosition, GetComponent<Transform>().rotation);
			typeItem = TypeItem.SwordWithShield;
		}
		if(weapon == 3){
			Instantiate(weapon3, WeaponPosition, GetComponent<Transform>().rotation);
			typeItem = TypeItem.LongSword;
		}
		if(weapon == 4){
			Instantiate(armor4, WeaponPosition, GetComponent<Transform>().rotation);
			typeItem = TypeItem.WizardArmor;
		}
		if(gameObjectWeapon != null){
			gameObjectWeapon.name = string.Concat("weaponCreated", (numWeapon++).ToString());  
			Information.Instance.game.AddCteatedItem(gameObjectWeapon.name, typeItem, WeaponPosition);
		}
	}

	private Action<bool> observerIsFacingSide;
	public void RegisterOnChangeIsFacingSide(Action<bool> d){
		observerIsFacingSide += d;
	}
	public void UnregisterOnChangeIsFacingSide(Action<bool> d){
		observerIsFacingSide -= d;
	}
	private void ChangeFacingSide(){
		if(observerIsFacingSide != null)
			observerIsFacingSide(_isFacingRight);
	}
	public KilledEnemy CheckEnemy(string name){
		return game.killedEnemies.Find(x => x.name == name);	
	}
	public bool CheckItem(string name){
		bool result = false;
		if(game.getUPItems.Find(x => x.name == name) != null)
			result = true;
		return result;	
	}
}
