using UnityEngine;
using System.Collections;

public class GuyScript : MonoBehaviour, IDeath{
    public Rigidbody2D rb;
    public Transform tr;
    public RigidbodyConstraints2D constraints; 
    public bool isFacingLeft = true;
    public bool isGame = true;
    public bool ismove = true;
    private Animator anim;
    public bool grounded = true;
    public Transform punch1;
    public float punchRadius = 0.2f;
    public float myTimerAttack = 0;
    public float RestTime;
    public float groundRadius = 0.3f;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public bool isSpeed;
    public bool isVsRogue;
    private bool speakRooster = false;

    public float RadiusAttack = 0.8f;
    public float heigthYSee = 3f; 
    public float RadiusSee = 8;
    public float SpeedRun = 1;

    public GameObject thePrefab;
    public GameObject theDialog7;

    float x,x1,y,y1;

    private Information information;
    public Vector2 RPosition;
    public bool RinDialogs;
    public bool RFacingRight;
    public bool RAttack;
    public float RHP;
    public bool RGrounded;
    private EnemyHP componentEnemyHP;
    // Use this for initialization
    void Awake(){
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        componentEnemyHP = GetComponent<EnemyHP>();
        componentEnemyHP.interfaceDeath = this as IDeath;
        information = Information.Instance;
    }

    // Update is called once per frame
    void Update(){
        InformationAboutRooster();
        CheckGrounded();
        GetPosition();
        if ((isGame) && (!componentEnemyHP.gameover)){
            if ((componentEnemyHP.HP <= 3)&&(!RinDialogs)){
                RefreezeMove();
                if (myTimerAttack > 0){myTimerAttack -= Time.deltaTime;}else{ismove = true;}
                if (ismove && grounded){
                    if (RHP > 0){
                        if ((x1 - x < RadiusSee) && (x1 - x > RadiusAttack) && (Mathf.Abs(y - y1) < heigthYSee)){
                            rb.velocity = new Vector2(-SpeedRun, 0);
                            if (!isFacingLeft) Flip();
                            isFacingLeft = true;
                            anim.SetBool("Speed", true);
                            isSpeed = true;
                        }
                        if ((x1 - x > -RadiusSee) && (x1 - x < -RadiusAttack) && (Mathf.Abs(y - y1) < heigthYSee)){
                            rb.velocity = new Vector2(SpeedRun, 0);
                            if (isFacingLeft) Flip();
                            isFacingLeft = false;
                            anim.SetBool("Speed", true);
                            isSpeed = true;
                        }
                        if (((Mathf.Abs(x - x1) > RadiusSee) || (Mathf.Abs(x - x1) < RadiusAttack)) && (Mathf.Abs(y - y1) < heigthYSee) && isSpeed){
                            anim.SetBool("Speed", false);
                            isSpeed = false;
                        }
                        if ((Mathf.Abs(x - x1) <= RadiusAttack) && (myTimerAttack <= 0) && (!isSpeed)){
                            var Rand = UnityEngine.Random.Range(0, 100);
                            if ((Rand >= 0) && (Rand < 75)){
                                anim.Play("Attack");
                                anim.SetBool("Attack", true);
                            }
                            if ((Rand < 100) && (Rand >= 75)){
                                if (isFacingLeft){
                                    rb.velocity = new Vector2(4, 5);
                                }else{
                                    rb.velocity = new Vector2(-4, 5);
                                }
                            }
                            myTimerAttack = RestTime;
                            ismove = false;
                        }
                    }
                }
            }else{
             if ((x1 - x < RadiusSee) && (x1 - x > RadiusAttack) && (Mathf.Abs(y - y1) < heigthYSee)){
                    if (!isFacingLeft) Flip();
                    isFacingLeft = true;
                }
                if ((x1 - x > -RadiusSee) && (x1 - x < -RadiusAttack) && (Mathf.Abs(y - y1) < heigthYSee)){
                    if (isFacingLeft) Flip();
                    isFacingLeft = false;
                }
                if(Mathf.Abs(x - x1)<5 && (y - y1 < heigthYSee)&& (y - y1 > 0)){
                    if(y - y1 > 0.1f){
                        rb.velocity = new Vector2(0, 4);
                    }
                    if(y - y1 < -0.1f){
                        rb.velocity = new Vector2(0, -7);
                    }
                    if(Mathf.Abs(y - y1) <= 0.1f){
                        rb.velocity = new Vector2(0, 0);
                    }
                }
            }
        }else{isSpeed = false; anim.SetBool("Speed", true);}
    }

    private void Flip(){
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void AttackGo(){
        EnemyFight2D.Action(punch1.position, punchRadius, 10, 1, isFacingLeft,"Guy");
    }
    void AttackStop(){
        anim.SetBool("Attack", false);
    }
     void GetPosition(){
        x = RPosition.x;
        y = RPosition.y;
        x1 = tr.position.x;
        y1 = tr.position.y; 
    }
     void InformationAboutRooster(){
        if(information != null){
            RPosition    = information.position;
            RinDialogs   = information.isDialog;
            RFacingRight = information.isFacingRight;
            RAttack      = information.isAttack;
            RHP          = ObserverHP.level;
        }
    }
    void EducationFight(){
        GameObject.Find("SwordWithShieldScript").GetComponent<BonusScript>().NewPosition(new Vector3(GameObject.Find("SwordWithShieldScript").GetComponent<Transform>().position.x, 5, 0));
        anim.Play("Idle");
        rb.AddForce(new Vector2(4, 5));
        speakRooster = true;
        var positionR = RPosition;
        myTimerAttack = 0f;
        ismove = true;
        InformationAboutRooster();
        Instantiate(theDialog7,positionR,Quaternion.Euler(0,0,0));
    }
    void CheckGrounded(){
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Grounded", grounded);
    }
    private bool refreeze = false;
    private void RefreezeMove(){
        if(refreeze == false){
            rb.mass = 1;
            rb.gravityScale = 1;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            refreeze = true;
            if(gameObject.name == "Guy_for_speak" && !speakRooster) EducationFight();
        }
    }
    public void Death(){
        gameObject.layer = 12;
        if(gameObject.name == "Guy_for_speak")
            componentEnemyHP.Speak();
        this.enabled = false;
    }
}
