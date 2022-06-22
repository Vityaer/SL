using UnityEngine;
using System.Collections;

public class FireMagic : MonoBehaviour{

    private bool isGame = true;
    public Rigidbody2D rb;
    public Transform tr;
    private float x, y, xa, ya,xv,yv;
    public Animator anim;
    public float TimerDeath = 0.05f;
    public int damage = 1;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        Destroy(gameObject, 20);
        var position = GameObject.FindWithTag("Player").transform.position;
         x = position.x;
         y = position.y;
       
        tr = GetComponent<Transform>();
        position = tr.position;
        xa = position.x;
        ya = position.y;
        xv = x - xa;
        yv = y - ya;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }


    void OnTriggerEnter2D(Collider2D coll){
        if ((coll.gameObject.GetComponent<PlayerHP>()) && (isGame)){
            Information.Instance.CauseOfDeath = "FireBall";
            coll.gameObject.GetComponent<TemperatureScript>().temperature += 0.2f;
            bool leftDamage = (xv < 0) ? true : false;
            coll.gameObject.GetComponent<PlayerHP>().GetDamage(damage, leftDamage);
            Death();
            isGame = false;

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

    void Death(){
        rb.velocity = new Vector2(xv / Mathf.Sqrt(xv * xv + yv * yv) * 4f, yv / Mathf.Sqrt(xv * xv + yv * yv) * 4f);
        GetComponent<CircleCollider2D>().enabled = true;
        anim.Play("Death");

        var position = GameObject.FindWithTag("Player").transform.position;
         x = position.x;
         y = position.y;
       
        tr = GetComponent<Transform>();
        position = tr.position;
        xa = position.x;
        ya = position.y;
        xv = x - xa;
        yv = y - ya;
        GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().velocity = new Vector2(xv / Mathf.Sqrt(xv * xv + yv * yv) * 6f, yv / Mathf.Sqrt(xv * xv + yv * yv) * 6f);
    }
     void DestroyMagic(){
        Destroy(gameObject);
    }
}
