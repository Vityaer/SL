using UnityEngine;
using System.Collections;

public class ScriptArmor : MonoBehaviour {

	public SpriteRenderer outColor;
	private bool ready = false;
	public int armor;
	public int weapon;
	public Transform tr;
	private Vector3 delta = new Vector3(0, 1f, 0f);
	private Information information;
    Vector3 ObjectPosition;
	void Start(){
		ObjectPosition = GetComponent<Transform>().position;
        information = Information.Instance;
    }

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.GetComponent<PlayerHP>() != null){
			outColor.enabled = true;
			InteractiveCanvasControllerScript.Instance.NewPosition(tr.position + delta, GameInput.Key.FindKey("F").ToString());
			if((GameInput.Key.GetKeyUp("F") || GameInput.Key.GetKey("F") || GameInput.Key.GetKeyDown("F"))&& !ready){
				ready = true;
				ChangeRooster(col.gameObject);
			}
		}
	}
	void OnTriggerStay2D(Collider2D col){
		if(col.gameObject.GetComponent<PlayerHP>() != null){
			if((GameInput.Key.GetKeyUp("F") || GameInput.Key.GetKey("F") || GameInput.Key.GetKeyDown("F"))&& !ready){
				ready = true;
				ChangeRooster(col.gameObject);
			}
		}
	}
	void OnTriggerExit2D(Collider2D col){
		if(col.gameObject.GetComponent<PlayerHP>() != null){
			if((GameInput.Key.GetKeyUp("F") || GameInput.Key.GetKey("F") || GameInput.Key.GetKeyDown("F"))&& !ready){
				ready = true;
				ChangeRooster(col.gameObject);
			}
			InteractiveCanvasControllerScript.Instance.OffInteractive();
			outColor.enabled = false;
		}
	}
	
	void ChangeRooster(GameObject Rooster){
        if(information.myTimerChangeSkin <= 0){
        	information.stamina = Rooster.GetComponent<StaminaControllerScript>().current;
			Destroy(Rooster);
			if(armor > 0){
				if(armor == 4){
					ObjectPosition = GetComponent<Transform>().position + new Vector3(-1, 0, 0);
				}
				if(information.armor > 0){
					information.CreateArmor(ObjectPosition);
				}	
				information.armor = armor;
			}
			if(weapon > 0){
				if(weapon == 4){
					ObjectPosition = GetComponent<Transform>().position + new Vector3(1, 0, 0);
				}
				if(information.weapon > 0){
					information.CreateWeapon(ObjectPosition);
				}
				information.weapon = weapon;	
			}
			information.ChangeSkin();
            Information.Instance.game.AddDestroyItem(gameObject.name);
			Destroy(gameObject);
			}else{
				ready = false;
			}
	}
}
