using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueScript : MonoBehaviour{
    public Dialogue dialogue = null;

	public bool trigger = false;
    void OnTriggerEnter2D(Collider2D coll){
		if(trigger == false){
			if(coll.gameObject.GetComponent<PlayerHP>()){
				trigger = true;
		 		OpenDialog();
			}
			
		}
	}
	public bool setedDialogue = false;
	public void SetDialogue(Dialogue dialogue){
 		this.dialogue = dialogue;
 		setedDialogue = true;
 		OpenDialog();
	}
	private void OpenDialog(){
		if(setedDialogue && trigger){
			Dialogs.Instance.OpenDialog(dialogue);
			Destroy(gameObject);
		}
	}
    
}
