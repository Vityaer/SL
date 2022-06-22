using UnityEngine;
using System.Collections;

public class RoostersMagicFly : MonoBehaviour {

	public Animator anim;
    public Rigidbody2D rb;
    public bool isGame = true;
    public float myTimerDeath = 10f;
    public float Speed = 4f;
    public bool is_speed = true;
    private Transform tr;
	
	void Start () {
        tr = GetComponent<Transform>();
        Destroy(gameObject,myTimerDeath);
		anim = GetComponent<Animator>();
	}
	
	public void DoImpulse (Vector3 playerPos, Vector3 mousePos) {
        Transform tr = GetComponent<Transform>();
        Vector2 dir = new Vector2(mousePos.x - playerPos.x, mousePos.y - playerPos.y).normalized;
		GetComponent<Rigidbody2D>().velocity = dir * Speed;
        tr.rotation = Quaternion.Euler(0, 0, 90-Mathf.Atan2(mousePos.x - playerPos.x, mousePos.y - playerPos.y) * Mathf.Rad2Deg);

	}

	void DestroyMagic(){
        if(gameObject.GetComponent<CircleCollider2D>()){
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
        if(gameObject.GetComponent<BoxCollider2D>()){
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject, 1);
    }

     private void Flip(){
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnTriggerEnter2D(Collider2D coll){
        if ((coll.gameObject.layer == 9)&&(isGame)) {
            coll.gameObject.GetComponent<EnemyHP>().GetDamage(2f);
            is_speed = false;
            isGame = false;
            anim.Play("Death");
        }
        if(coll.gameObject.GetComponent<BoxCollider2D>()){
            if(coll.gameObject.layer == 8){
                is_speed = false;
                isGame = false;
                anim.Play("Death");
            }
        }
         if(coll.gameObject.GetComponent<SpriteRenderer>() != null){
            if(coll.gameObject.CompareTag("BulletGround")){
                GetComponent<Animator>().Play("Death");
                rb.velocity = new Vector2(0, 0);
                if( Vector3.Distance(tr.position, Information.Instance.position) < 8f){
                    float power = (8f - Vector3.Distance(tr.position, Information.Instance.position))/8f;
                    Vector2 dir = new Vector2();
                    dir.x = Information.Instance.position.x - tr.position.x;
                    dir.y = Information.Instance.position.y - tr.position.y;
                    if(ObserverHP.level > 0)
                        GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().velocity = dir.normalized * power * 2f;
                    CameraShake.Shake(power, power, CameraShake.ShakeMode.XY);
                }
            }
        }
    }
}
