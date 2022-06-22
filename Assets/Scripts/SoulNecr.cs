using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulNecr : MonoBehaviour {
	 public Rigidbody2D rb;
	 public GameObject Dialog;
	 bool speak;
	// Use this for initialization
	void Start () {
        GameObject Player= GameObject.FindWithTag("Player");
        if(Player.GetComponent<RoosterScript>()){
        	Player.GetComponent<RoosterScript>().isDialog = true;
        }
        if(Player.GetComponent<RoosterScriptWizard>()){
        	Player.GetComponent<RoosterScriptWizard>().isDialog = true;
        }
        if(Player.GetComponent<RoosterScriptWithShield>()){
        	Player.GetComponent<RoosterScriptWithShield>().isDialog = true;
        }
        GameObject.Find("Necromancer").GetComponent<EnemyHP>().GetDamage(999f);

		rb = GetComponent<Rigidbody2D>();
		GetComponent<SpriteRenderer>().enabled = true;
		gameObject.transform.GetChild(0).gameObject.GetComponent<Light>().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		rb.velocity = new Vector2(0, 3);
		if(!speak &&(GetComponent<Transform>().position.y > 8)){
			Instantiate(Dialog,GameObject.FindWithTag("Player").GetComponent<Transform>().position,gameObject.transform.rotation);
			speak = true;
		}
		if(speak){
			Destroy(gameObject);
		}
	}
}
