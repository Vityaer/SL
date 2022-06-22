using UnityEngine;
using System.Collections;

public class DangerScript : MonoBehaviour {
	private bool AttackRooster = false;
    public float myTimer;
    public int damage;
	// Update is called once per frame
	void Update () {
    	if (myTimer > 0){
            myTimer -= Time.deltaTime;
        }else{
            AttackRooster = false;
        }
	}
	void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.GetComponent<PlayerHP>() && !AttackRooster){
            Information.Instance.CauseOfDeath = "Danger";
            col.gameObject.GetComponent<PlayerHP>().GetClearDamage(damage);
        	AttackRooster = true;
        	myTimer = 0.3f;   
        }
        if (col.gameObject.GetComponent<EnemyHP>() != null){
                col.gameObject.GetComponent<EnemyHP>().GetDamage(damage);
        }
    }
    void OnTriggerExit2D(Collider2D col){
        if (col.gameObject.GetComponent<PlayerHP>() && (myTimer < 0)){
        	AttackRooster = false;   
        }
    }
}
