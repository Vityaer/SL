using UnityEngine;
using System.Collections;

public class MovePlatform : MonoBehaviour {

    [SerializeField] private Rigidbody2D rbPlayer;
    void OnCollisionEnter2D(Collision2D coll){
    	if(coll.gameObject.GetComponent<PlayerHP>()){
	        // coll.transform.parent = transform;
            rbPlayer = coll.gameObject.GetComponent<Rigidbody2D>();
    	}
    }
    public void ChangeVelocityObject(Vector2 dir){
        if(rbPlayer != null){
            rbPlayer.velocity = rbPlayer.velocity + dir;
        }
    }
    void OnCollisionExit2D(Collision2D coll){
    	if(coll.gameObject.GetComponent<PlayerHP>()){
	        coll.transform.parent = null;
            rbPlayer = null;
    	}
    }
}
