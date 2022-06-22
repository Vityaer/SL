using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {
	public string NextNameLevel;
	public bool ChangeRooster = true;
	private GameObject StartPosition;
	public string CurrentNameLevel;
	string nameCheckPoint = "StartPosition";
	private Transform tr;
	// Use this for initialization
	void Awake(){
		tr = GetComponent<Transform>();
	}
	void Start () {
		Information.Instance.NameLevel = CurrentNameLevel; 
		Information.Instance.SaveInfo();
		Information.Instance.temperature = 0;
		if(Information.Instance.game.nameCheckPoint.Length > 0){
			nameCheckPoint = Information.Instance.game.nameCheckPoint;
			PrepareLevel();
		}	 
		StartPosition = GameObject.Find(nameCheckPoint);
		if(StartPosition != null){
			Information.Instance.position = new Vector2(StartPosition.transform.position.x,StartPosition.transform.position.y); 
			Information.Instance.ChangeSkin();
			OptimizationScript.Instance.OnCreatedPlayer(StartPosition.transform.position);
		}
	}
	void PrepareLevel(){
		CreateItems();
	}
	void OnTriggerEnter2D(Collider2D col){
		if(col.GetComponent<PlayerHP>()){
			if(col.GetComponent<RoosterScript>()){
				col.GetComponent<RoosterScript>().isDialog = true;
			}
			if(col.GetComponent<RoosterScriptWithShield>()){
				col.GetComponent<RoosterScriptWithShield>().isDialog = true;
			}
			if(col.GetComponent<RoosterScriptWizard>()){
				col.GetComponent<RoosterScriptWizard>().isDialog = true;
			}
			col.GetComponent<Animator>().SetBool("Speed", false);
			col.GetComponent<Animator>().SetBool("Grounded", true);
			NextLevel();
		}
	}
	// Update is called once per frame
	void NextLevel(){
		Information.Instance.game.ClearLevelData();
        FadeInOut.sceneStarting = false;
		FadeInOut.nextLevel = NextNameLevel; 
        FadeInOut.sceneEnd = true;
	} 
	void CreateItems(){
		GameObject createItem = null;
		foreach(Item item in Information.Instance.game.getUPItems){
			if(item.typeAction == TypeAction.Create){
				switch(item.typeItem){
					case TypeItem.Monet:
						createItem = Monet;
						break;
					case TypeItem.Heart:
						createItem = Heart;
						break;
					case TypeItem.LigthArmor:
						createItem = LigthArmor;
						break;
					case TypeItem.MiddleArmor:
						createItem = MiddleArmor;
						break;
					case TypeItem.HardArmor:
						createItem = HardArmor;
						break;
					case TypeItem.Dagger:
						createItem = Dagger;
						break;
					case TypeItem.LongSword:
						createItem = LongSword;
						break;
					case TypeItem.SwordWithShield:
						createItem = SwordWithShield;
						break;
					case TypeItem.WizardArmor:
						createItem = WizardArmor;
						break;	
					default:
						Debug.Log("problem");
						break;								
				}
				GameObject bonus = Instantiate(createItem, item.pos, tr.rotation);
				bonus.name = item.name;
			}
		}
	}
	[Header("Items")]
	[SerializeField] private GameObject Monet, Heart, LigthArmor, MiddleArmor, HardArmor, Dagger, LongSword, SwordWithShield, WizardArmor;
}
