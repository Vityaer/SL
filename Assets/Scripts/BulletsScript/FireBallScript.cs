using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallScript : MonoBehaviour{
    private bool isGame = true;
    private Rigidbody2D rb;
    private Transform tr;
    public float speed = 7.5f; 
    private float x, y, xa, ya,xv,yv;
    public Vector2 miss = new Vector2(0,0);
    public int damage;
    private Vector2 RPosition;
    public string Message;
    private Vector2 direction;

    void Awake(){
    	rb = GetComponent<Rigidbody2D>();
    	tr = GetComponent<Transform>();
    }
    // Use this for initialization
    void Start () {
        RPosition = Information.Instance.position;
        Destroy(gameObject, 20);
        x = RPosition.x + UnityEngine.Random.Range(-miss.x, miss.x);
        y = RPosition.y + UnityEngine.Random.Range(-miss.y, miss.y);
        xa = tr.position.x;
        ya = tr.position.y;
        xv = x - xa;
        yv = y - ya;
        tr.rotation = Quaternion.Euler(0, 0, (xv < 0) ? Mathf.Atan(yv/xv)*(180/Mathf.PI) : 180+Mathf.Atan(yv/xv)*(180/Mathf.PI));    
        direction = new Vector2(xv / Mathf.Sqrt(xv * xv + yv * yv) * speed, yv / Mathf.Sqrt(xv * xv + yv * yv) * speed);
        rb.velocity = direction;
    }

    void OnTriggerEnter2D(Collider2D coll){
    	PlayerHP playerHP = coll.gameObject.GetComponent<PlayerHP>(); 
        if ((playerHP != null) && (isGame)){
			GetComponent<Animator>().Play("Death");
			rb.velocity = new Vector2(0, 0);
            isGame = false;
            if(playerHP.HP > 0) Information.Instance.CauseOfDeath = Message;
			bool leftDamage = (xv < 0) ? true : false;
			playerHP.GetDamage(damage, leftDamage);
            CameraShake.Shake(1, 1, CameraShake.ShakeMode.XY);
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
    void DestroyMagic(){
        Destroy(gameObject);
    }
    private void Flip(){
        //получаем размеры персонажа
        Vector3 theScale = transform.localScale;
        //зеркально отражаем персонажа по оси Х
        theScale.x *= -1;
        //задаем новый размер персонажа, равный старому, но зеркально отраженный
        transform.localScale = theScale;
    }
}
