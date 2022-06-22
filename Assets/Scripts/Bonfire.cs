using UnityEngine;
using System.Collections;

public class Bonfire : MonoBehaviour {
	public bool trigger = false;
    Coroutine restAttack;
	void OnTriggerStay2D(Collider2D coll){
        if (coll.gameObject.GetComponent<PlayerHP>() && !trigger){
                Rest();
        		coll.gameObject.GetComponent<TemperatureScript>().temperature += 0.2f;
                if(coll.gameObject.GetComponent<PlayerHP>().HP > 0) Information.Instance.CauseOfDeath = "Bonfire";
                coll.gameObject.GetComponent<PlayerHP>().GetClearDamage(1);
        }
        if (coll.gameObject.GetComponent<EnemyHP>() && !coll.gameObject.GetComponent<GuyScript>() && !trigger){
            Rest();
            if(coll.gameObject.GetComponent<GuyScript>() == null){
                coll.gameObject.GetComponent<EnemyHP>().GetDamage(1);
            }
        }
        if(coll.gameObject.GetComponent<GuyScript>() && !trigger){
            Rest();	
            coll.gameObject.GetComponent<EnemyHP>().GetDamage(10f);
        }
    }
    void Rest(){
        if(!trigger){
            if(restAttack != null){
                StopCoroutine(restAttack);
                restAttack = null;
            }
            restAttack = StartCoroutine(IRestTime());
        }
    }
    IEnumerator IRestTime(){
        trigger = true;
        float time = 2f;
        while(time > 0f){
            time -= Time.deltaTime;
            yield return null; 
        }
        trigger = false;
    }
}
