using UnityEngine;
using System.Collections;


public class SpiritScript : MonoBehaviour {
    private Rigidbody2D rb;
    private Transform tr;
    public bool isFacingRight = true;
    public float xmin,xmax,ymin,ymax;
    public bool isGame = true;
    public bool ismove = true;
    private bool isTouch = true;
    private Animator anim;
    public Transform punch1;
    public float time;
    public float punchRadius = 0.2f;
    public float myTimerAttack = 0;
    public float myTimerPlace = 0;
    private float myTimerTouch = 2.0f; 
    private bool isSpeed;
    public float RadiusSee = 8;
    public float RadiusAttack = 1;
    private Vector2 posStart, posTarget, posCurrent;
    private Information information;
    public Vector2 RPosition;
   

    void Start () {
        information = Information.Instance;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        posStart = new Vector2(tr.position.x, tr.position.y);
        CreateTarget();
}

    // Update is called once per frame
    void Update() {
        GetPosition();
        if (myTimerAttack > 0){
            myTimerAttack -= Time.deltaTime;
        }else{
            AttackRooster();
        }
        if (myTimerTouch > 0){
            myTimerTouch -= Time.deltaTime;
        }else{
            isTouch = true;
        }

        if ((isGame) && (ismove)&&(myTimerAttack > 0)){
            if(Vector2.Distance(posCurrent, posTarget) < 0.1f){
                StartCoroutine(WaitInPLace(UnityEngine.Random.Range(1.0f, 3.0f)));
                rb.velocity = new Vector2(0,0);
                CreateTarget();
            }else{
                rb.velocity = new Vector2(posTarget.x - posCurrent.x, posTarget.y - posCurrent.y);
            }
        }
    } 
    private void Flip(){
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void GetPosition(){
        posCurrent.x = tr.position.x;
        posCurrent.y = tr.position.y;
    }
    void CreateTarget(){
        posTarget.x = posStart.x + UnityEngine.Random.Range(-xmin, xmax);
        posTarget.y = posStart.y + UnityEngine.Random.Range(-ymin, ymax);
        if(((posCurrent.x > posTarget.x)&&(isFacingRight)) ||((posCurrent.x < posTarget.x)&&(!isFacingRight))){
            isFacingRight = !isFacingRight;
            Flip();
        }
    }
    void AttackRooster(){
        rb.velocity = new Vector2(0,0);
        anim.SetBool("Attack", true);  
    }
    void AttackStop(){
        myTimerAttack = UnityEngine.Random.Range(7.0f, 12.0f);
        anim.SetBool("Attack", false);
    }
    void AttackGo(){
        EnemyFight2D.Action(punch1.position, punchRadius, 10, 1, !isFacingRight,"Spirit");
    }
    void OnTriggerEnter2D(Collider2D coll){
        if ((coll.gameObject.GetComponent<PlayerHP>()) && isTouch){
            coll.gameObject.GetComponent<PlayerHP>().GetClearDamage(2);
            isTouch = false;
            myTimerTouch = 2.0f;
        }
    }
    IEnumerator WaitInPLace(float timerWait){
        yield return new WaitForSeconds (timerWait);
    }
}
