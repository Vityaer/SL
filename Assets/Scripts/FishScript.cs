using UnityEngine;
using System.Collections;

public class FishScript : MonoBehaviour {

	public Rigidbody2D rb;
    public Transform tr;
    public Vector2 startPosition;
    public bool stateFly;
    private Animator anim;
    public float myTimer;
    float myTimerAttack;
    bool AttackRooster;

	// Use this for initialization
	void Start () {
    	rb = GetComponent<Rigidbody2D>();
    	tr = GetComponent<Transform>();
    	anim = GetComponent<Animator>();
    	startPosition = tr.position;
    	stateFly = true;
    	myTimer = UnityEngine.Random.Range(0, 5);
	}
	
	// Update is called once per frame
	void Update () {
		if(stateFly){
			if(tr.position.y - startPosition.y < 2.5f){
				rb.velocity = new Vector2(0,3);
			}else{
				stateFly = false;
				anim.Play("Landing");
			}
		}else{
			rb.velocity = new Vector2(0,-4);
			if(tr.position.y - startPosition.y < -2){
				stateFly = true;
				anim.Play("Fly");
			}
		}
	}
	void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.GetComponent<PlayerHP>() && !AttackRooster){
            Information.Instance.CauseOfDeath = "Fish";
            col.gameObject.GetComponent<PlayerHP>().GetClearDamage(1);
        	AttackRooster = true;
        }
    }
    void OnTriggerExit2D(Collider2D coll){
        if (coll.gameObject.GetComponent<PlayerHP>()){
        	AttackRooster = false;
        }
    }
	void AnimStop(){
		anim.speed = 0;
	}
}
