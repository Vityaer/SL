using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiPriestScript : MonoBehaviour{
	EnemyHP enemyHP;
	BoxCollider2D boxCollider;
	CircleCollider2D circleCollider;
	Rigidbody2D rb;
    RogueScript rogueScript;
    Archer archerScript;
    Animator anim;
    SpriteRenderer spriteRenderer;
    int startOrder = 0;
	void Start(){
		anim = GetComponent<Animator>();
		enemyHP  = GetComponent<EnemyHP>();
		boxCollider = GetComponent<BoxCollider2D>();
		circleCollider = GetComponent<CircleCollider2D>();
		rb = GetComponent<Rigidbody2D>();
		rogueScript = GetComponent<RogueScript>();
		archerScript = GetComponent<Archer>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		startOrder  = spriteRenderer.sortingOrder; 
		DefaultState();
	}
	void DefaultState(){
        spriteRenderer.sortingOrder = 1;
		boxCollider.enabled = false;
        circleCollider.enabled = false;
        rb.isKinematic = true;
        rb.gravityScale = 0;
        enemyHP.HP = 0;
        enemyHP.gameover = true;
        if(rogueScript  != null) rogueScript.enabled  = false;
        if(archerScript != null) archerScript.enabled = false;
        gameObject.layer = 12;
        anim.Play("Death");
	}   
	public void Kill(){
		enemyHP.GetDamage(9999);
    }
    public void Rresurrection(){
    	if(enemyHP.HP == 0){
	        spriteRenderer.sortingOrder = startOrder;
            boxCollider.enabled = true;
            circleCollider.enabled = true;
            rb.isKinematic = false;
            rb.gravityScale = 1;
            enemyHP.HP = 2;
            enemyHP.gameover = false;
            if(rogueScript  != null) {rogueScript.enabled  = true; rogueScript.isGame  = true;}
            if(archerScript != null) {archerScript.enabled = true; archerScript.isGame = true;}
            gameObject.layer = 9;
            anim.speed = 1;
		}
    }
}
