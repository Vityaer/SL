using UnityEngine;
using System.Collections;


public class RogueScript : MonoBehaviour, IDeath {
    private Rigidbody2D rb;
    private Transform tr;
    public bool isFacingLeft = true;
    public bool isGame = true;
    public bool ismove;
    private Animator anim;
    public bool grounded = true;
    public bool groundedFace = false;
    public Transform punch1;
    public Transform CheckFace;
    public float PowerJump;
    public float punchRadius = 0.2f;
    public float myTimerAttack = 0;
    public float groundRadius = 0.05f;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float GradeSelfSave;
    public bool isBalist;
    public bool isVsGuy;
    public int damage;
    public float SpeedRun = 4;

    public float RestTime = 1.5f;
    public float RadiusAttack = 1f;

    public float RadiusSee = 8;
    public float RadiusFire = 3;

    float x,x1,y,y1;
    public bool Combo = false;
    public Vector2 RPosition;
    public bool RinDialogs;
    public bool RAttack;
    public float RHP;
    private Information InformationScript;
    void Awake () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
    }
    void Start(){
        GetComponent<EnemyHP>().interfaceDeath = this as IDeath;
        InformationScript = Information.Instance;
    }

    void Update() {
        InformationAboutRooster();
        GetPosition();
        GroundCheckFunction();
        if (myTimerAttack > 0){
            myTimerAttack -= Time.deltaTime;
        }else{
            if (grounded)
                ismove = true;
        }
        anim.SetBool("grounded", grounded);
		if (grounded && isGame && ismove && !RinDialogs && myTimerAttack <= 0 && RHP > 0){
            if(!isBalist){
                if((Mathf.Abs(x1) <= RadiusAttack)&&(Mathf.Abs(y1) <= 2)) {
                    SelfSave();
                    AttackRooster();
                }else{
                    if(grounded) FindRooster();
                }
            }else{
                if((isFacingLeft) && (x1 > 0) && (x1 < RadiusSee) && (Mathf.Abs(y1) < 1.5f)){
                    anim.SetBool("Attack", true);
                    anim.Play("Attack2");

                }
                if((!isFacingLeft) && (x1 < 0) && (Mathf.Abs(x1) < RadiusSee) && (Mathf.Abs(y1) < 1.5f)){
                    anim.SetBool("Attack", true);
                    anim.Play("Attack2");
                }
            }
        }
    }
  

    void AttackGo(){
        EnemyFight2D.Action(punch1.position, punchRadius, 10, damage, isFacingLeft,"Rogue");
        if(isBalist){
            Fight2D.Action(punch1.position, punchRadius*2f, 9, 1, isFacingLeft);
            isBalist = false;
            StartCoroutine("AttackInBalist");

        }
    }
    public void AttackRooster(){
        if((Mathf.Abs(x1) <= RadiusAttack)&&(Mathf.Abs(y1) <= 1)){
            anim.SetBool("Speed", false);
            if(Combo){
                anim.Play("ComboAttack3");
                myTimerAttack = RestTime;
                ismove = false;
                Combo = false;
            }
            if (myTimerAttack <= 0){
                var Rand = UnityEngine.Random.Range(0, 100);
                
                if ((Rand >= 0) && (Rand < 40)){
                    anim.SetBool("Attack", true);
                    anim.Play("Attack1");
                }

                if ((Rand >= 40) && (Rand < 80)){
                    anim.SetBool("Attack", true);
                    anim.Play("Attack2");
                }

                if(grounded){
                    if ((Rand < 90) && (Rand >= 80)){
                        if (isFacingLeft){
                            rb.velocity = new Vector2(4, PowerJump);
                        }else{
                            rb.velocity = new Vector2(-4, PowerJump);
                        }
                    }
                    if ((Rand < 100) && (Rand >= 90)){
                        if (isFacingLeft){
                            rb.velocity = new Vector2(-5, PowerJump);
                        }else{
                            rb.velocity = new Vector2(5, PowerJump);
                        }
                        Combo = true;
                    }
                }
                myTimerAttack = RestTime;
                ismove = false;
            }    
        }
    }
    void AttackStop(){
        anim.SetBool("Attack", false);
    }
    void GroundCheckFunction(){
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        groundedFace = Physics2D.OverlapCircle(CheckFace.position, groundRadius, whatIsGround);
        anim.SetBool("grounded", grounded);
    }
    void GetPosition(){
        x = RPosition.x;
        y = RPosition.y;
        if(isVsGuy){
            if (Mathf.Abs(x - tr.position.x) > 3){
                if(Mathf.Abs(RPosition.x - tr.position.y) > RadiusSee){
                    x = GameObject.Find("GuyVS").transform.position.x;
                    y = GameObject.Find("GuyVS").transform.position.y;
                }
            }
        }
        x1 = tr.position.x;
        y1 = tr.position.y;
        x1 = x1 - x;
        y1 = y1 - y; 
        if((x1 < 0 && isFacingLeft) || (x1 > 0 && !isFacingLeft)){
            if(Mathf.Abs(y1) < 2)
                Flip();
        }else{
            if(Combo) Combo = false;
        } 
    }
    void FindRooster (){
        Combo = false;
        if ((Mathf.Abs(x1) > RadiusSee)|| (Mathf.Abs(x1) < RadiusAttack) || (Mathf.Abs(y1)>1)){
            anim.SetBool("Speed", false);
        }
        if ((x1 < RadiusSee) && (x1 > RadiusAttack)&&(Mathf.Abs(y1) < 2)){
            if(!groundedFace ){
                rb.velocity = new Vector2(-SpeedRun, rb.velocity.y);
            }else{
                if(grounded)
                    rb.velocity = new Vector2(-SpeedRun, PowerJump);
            }
            if (!isFacingLeft) Flip();
            anim.SetBool("Speed", true);
        }
        if ((x1 > -RadiusSee) && (x1 < -RadiusAttack) && (Mathf.Abs(y1) < 2)){
            if(!groundedFace){
                rb.velocity = new Vector2(SpeedRun, rb.velocity.y);
            }else{
                if(grounded)
                    rb.velocity = new Vector2(SpeedRun, PowerJump);
            }
            if (isFacingLeft) Flip();
            anim.SetBool("Speed", true);
        }
    }

    void SelfSave(){
        if(RAttack && grounded){
            var Rand = UnityEngine.Random.Range(0, 100);
            if(Rand < GradeSelfSave){
                Combo = false;
                if(grounded){
                    if(isFacingLeft){
                        rb.velocity = new Vector2(4, 5);
                    }else{
                        rb.velocity = new Vector2(-4, 5);
                    }
                }
            }
        }    
    }
    private void Flip(){
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        isFacingLeft = !isFacingLeft;
    }

    void InformationAboutRooster(){
        RPosition = InformationScript.position;
        RinDialogs = InformationScript.isDialog;
        RAttack = InformationScript.isAttack;
        RHP= ObserverHP.level;
    }
    IEnumerator AttackInBalist(){
        yield return new WaitForSeconds (3f);
    }
    public void Death(){
        gameObject.layer = 12;
        anim.SetBool("Death", true);
        this.enabled = false;
    }
}
