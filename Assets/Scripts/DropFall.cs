using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropFall : MonoBehaviour {

	public int lives = 3;
	bool flag = false;
	bool ExitFlag = false;
	public Sprite DownSprite;
	public Sprite UpSprite;
	private Transform tr;
	private Rigidbody2D rb;
	private SpriteRenderer sr;
	private Vector3 startPosition; 
	private Vector3 startScale; 

	void Awake(){
		tr = GetComponent<Transform>();
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
		startPosition = tr.position;
		startScale = tr.localScale;
	}
	void Update(){
		if(lives > 0)
			sr.sprite = (rb.velocity.y <= 0) ? DownSprite : UpSprite;   
	}
	void OnTriggerEnter2D(Collider2D coll){
		if(coll.gameObject.name == "FinishDrop"){
			if(!ExitFlag){
				lives--;
				Vector3 theScale = tr.localScale;
		        theScale.x *= 0.5f;
		        theScale.y *= 0.5f;
		        tr.localScale = theScale;
				ExitFlag = true;
			}
			if(lives == 0){
				RestDrop();
			}
		}
		if(coll.gameObject.GetComponent<PlayerHP>()){
			if(!flag){
				flag = true;
	            Information.Instance.CauseOfDeath = "Drop";
	            coll.gameObject.GetComponent<PlayerHP>().GetClearDamage(1);
	            RestDrop();
			}
		}
		if(coll.gameObject.GetComponent<EnemyHP>()){
			RestDrop();
		}
	}
	void OnTriggerExit2D(Collider2D coll){
		if(coll.gameObject.name == "FinishDrop"){
			ExitFlag = false;
		}
	}
	void RestDrop(){
		tr.position = new Vector3(1000f, 1000f, 0f);
		rb.gravityScale = 0f;
		rb.velocity = new Vector2(0f, 0f); 
		StartCoroutine( IRestTime() );
	}
	void ReturnInStart(){
		lives = 3;
		tr.position = startPosition;
		tr.localScale = startScale;
		rb.gravityScale = 1f; 
	}
	IEnumerator IRestTime(){
		float restTime = 4f;
		while(restTime >= 0f){
			restTime -= Time.deltaTime;
			yield return null;
		}
		ReturnInStart();
	}
}
