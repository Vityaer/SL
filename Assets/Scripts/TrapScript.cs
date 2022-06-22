using UnityEngine;
using System.Collections;

public class TrapScript : MonoBehaviour {

	public Rigidbody2D rb;
	public float myTimerTrap;
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	 void OnTriggerStay2D(Collider2D coll){
        if (coll.gameObject.GetComponent<PlayerHP>() && myTimerTrap > 0f){
        	myTimerTrap -= Time.deltaTime;
        	if(myTimerTrap <= 0){
				rb.velocity = new Vector2(0,-2);
			}   
        }
    }
}
