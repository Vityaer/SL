using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BoltFly : MonoBehaviour {

    public int lives = 3;
    public Rigidbody2D rb;
    public bool isFacingLeft = true;
    public int damage;
    private List<GameObject> targets = new List<GameObject>(); 
    // Use this for initialization
    void Start () {
        Destroy(gameObject, 20);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
    	if(isFacingLeft){
	        rb.velocity = new Vector2(-5, 0);
    	}else{
    		rb.velocity = new Vector2(5, 0);
    	}
    	if(lives == 0) Destroy(gameObject);
    }
  

    void OnTriggerEnter2D(Collider2D coll){
        if(lives > 0){
            if(!targets.Contains(coll.gameObject)){
                if (coll.gameObject.GetComponent<EnemyHP>()){
                    if((coll.gameObject.GetComponent<FireHelpBolt>() == null)&&(coll.gameObject.GetComponent<BoxScript>() == null)){
                        coll.gameObject.GetComponent<EnemyHP>().GetDamage(damage);
                        targets.Add(coll.gameObject);
                        lives--;
                    }
                }
                if ((coll.gameObject.GetComponent<PlayerHP>() != null)){
                    if (coll.gameObject.GetComponent<PlayerHP>().HP > 0) Information.Instance.CauseOfDeath = "Bolt";
                    coll.gameObject.GetComponent<PlayerHP>().GetClearDamage(damage);
                    targets.Add(coll.gameObject);
                	lives--;
                }
            }
        }
    }
}
