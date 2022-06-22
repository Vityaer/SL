using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Game{
	public string nameLevel;
	public string nameCheckPoint;
	public int health;
	public int gold;
	public int MP;
	public int armor, weapon;
	public bool optionGirl = false;
	public bool fatherGirlFound = false;
	public bool startOptionGirl, startFatherGirlFound;
	public int startHealth, startMP, startWeapon, startArmor, startGold;
	public List<KilledEnemy> killedEnemies = new List<KilledEnemy>();
	public List<Item> getUPItems = new List<Item>();

	public Game(){
		this.nameLevel = "Prolog";
		this.nameCheckPoint = "";
		this.health = 10;
		this.gold = 0;
		this.MP = 0;
		this.armor = 0;
		this.weapon = 0;
		this.optionGirl = false;
		this.fatherGirlFound = false;
		this.killedEnemies = new List<KilledEnemy>();
		this.getUPItems = new List<Item>();
		this.startHealth = 10;
		this.startGold = 0;
		this.startMP = 0;
		this.startWeapon = 0;
		this.startArmor = 0;
		this.startOptionGirl = false;
		this.startFatherGirlFound = false; 
	}
	public Game(SaveGame save){
		this.nameLevel = save.nameLevel;
		this.nameCheckPoint = save.nameCheckPoint;
		this.health = save.health;
		this.gold = save.gold;
		this.MP = save.MP;
		this.armor = save.armor;
		this.weapon = save.weapon;
		this.optionGirl = save.optionGirl;
		this.fatherGirlFound = save.fatherGirlFound;
		this.killedEnemies = save.killedEnemies;
		this.getUPItems = save.getUPItems;
		this.startHealth = save.health;
		this.startGold = save.gold;
		this.startMP = save.MP;
		this.startWeapon = save.weapon;
		this.startArmor = save.armor;
		this.startOptionGirl = save.optionGirl;
		this.startFatherGirlFound = save.fatherGirlFound; 
	}
	public void AddEnemy(string name, Vector3 pos){
		killedEnemies.Add(new KilledEnemy(name, pos));
	}
	public void AddCteatedItem(string name, TypeItem typeItem, Vector3 pos){
		getUPItems.Add(new Item(name, typeItem, pos));
	}
	public void AddDestroyItem(string name){
		Item removeItem = null;
		foreach(Item item in getUPItems){
			if(item.name == name){
				removeItem = item;
				break;
			}
		}
		if(removeItem != null){
			getUPItems.Remove(removeItem);
		}else{
			getUPItems.Add(new Item(name));
		}
	}
	public void ClearLevelData(){
		nameCheckPoint = "";
		killedEnemies.Clear();
		getUPItems.Clear();
	}
}

[System.Serializable]
public class SaveGame{
	public string nameLevel;
	public string nameCheckPoint;
	public int health;
	public int gold;
	public int MP;
	public int armor, weapon;
	public bool optionGirl = false;
	public bool fatherGirlFound = false;
	public List<KilledEnemy> killedEnemies = new List<KilledEnemy>();
	public List<Item> getUPItems = new List<Item>();
	public SaveGame(Game game){
		this.nameLevel = game.nameLevel;
		this.nameCheckPoint = game.nameCheckPoint;
		this.health = game.health;
		this.gold = game.gold;
		this.MP = game.MP;
		this.armor = game.armor;
		this.weapon = game.weapon;
		this.optionGirl = game.optionGirl;
		this.fatherGirlFound = game.fatherGirlFound;
		this.killedEnemies = game.killedEnemies;
		this.getUPItems = game.getUPItems;
	}
}
[System.Serializable]
public class KilledEnemy{
	public string name;
	public Vector3 pos;
	public KilledEnemy(string name, Vector3 pos){
		this.name = name;
		this.pos = pos;
	} 
}
[System.Serializable]
public class Item{
	public string name;
	public TypeAction typeAction;
	public TypeItem typeItem;
	public Vector3 pos;
	public Item(string name){
		this.name = name;
		this.typeAction = TypeAction.Destroy;
	}
	public Item(string name, TypeItem typeItem, Vector3 pos){
		this.name = name;
		this.typeItem = typeItem;
		this.pos = pos;
		this.typeAction = TypeAction.Create;
	}
}
public enum TypeAction{
	Create,
	Destroy
}
public enum TypeItem{
	Monet,
	Heart,
	LigthArmor,
	MiddleArmor,
	HardArmor,
	Dagger,
	LongSword,
	SwordWithShield,
	WizardArmor
}