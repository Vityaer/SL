using UnityEngine;
using System.Collections;

public class MagicFly : MonoBehaviour{
     public Animator anim;
    public Rigidbody2D rb;
    public Transform tr;
    public bool isGame = true;
    private float mytimer = 1f;
    private float x, y, xa, ya, xv, yv;
    public float myTimerDeath = 10;
    public bool speed = false;
    public int damage = 2;

    // Use this for initialization
    void Start(){
        anim = GetComponent<Animator>();
        anim.Play("Create");
        FindPlayer();
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        if (xv > 0){
            Flip();
        }
    }
    void FindPlayer(){
        if(speed){
            if(isGame){
                if(Information.Instance.isDialog){
                    Death();
                } 
                var position = GameObject.FindWithTag("Player").transform.position;
                x = position.x;
                y = position.y;

                position = tr.position;
                xa = position.x;
                ya = position.y;
                xv = x - xa;
                yv = y - ya;
                if((Mathf.Abs(xv)<0.2)&&(Mathf.Abs(yv)<0.2)){
                    Death();
                    isGame = false;
                }
                rb.velocity = new Vector2(xv / Mathf.Sqrt(xv * xv + yv * yv) * 3f, yv/Mathf.Sqrt(xv * xv + yv * yv) * 3f);
            }else{
                rb.velocity = new Vector2(0,0);
            }
        }
    }
    void Fly(){
         anim.SetBool("Speed", true);
         speed = true;
    }
    void Death(){
        anim.Play("Death");
       
    }
    void DestroyMagic(){
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update(){
        if (myTimerDeath > 0){
            myTimerDeath -= Time.deltaTime;
        }else{ Death(); }
        if (mytimer > 0){
            mytimer -= Time.deltaTime;
        }else{
            FindPlayer();
            mytimer = 1f;
        }
        FindPlayer();
    }


    void OnTriggerStay2D(Collider2D coll){
        if (coll.gameObject.GetComponent<PlayerHP>()&&(isGame)){
            isGame = false;
            Information.Instance.CauseOfDeath = "Magic";
            bool leftDamage = (xv < 0) ? true : false;
            coll.gameObject.GetComponent<PlayerHP>().GetDamage(damage, leftDamage);
            Death();
        }
    }
    private void Flip()
    {
        //получаем размеры персонажа
        Vector3 theScale = transform.localScale;
        //зеркально отражаем персонажа по оси Х
        theScale.x *= -1;
        //задаем новый размер персонажа, равный старому, но зеркально отраженный
        transform.localScale = theScale;
    }
}

