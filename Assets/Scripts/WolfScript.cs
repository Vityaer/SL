using UnityEngine;
using System.Collections;

public class WolfScript : MonoBehaviour, IDeath {
    public Rigidbody2D rb;
    public Transform tr;
    public bool isFacingLeft = true;
    public bool isGame = true;
    public bool ismove;
    private Animator anim;
    public bool grounded = true;
    public bool groundedFaceDown = false;
    public bool groundedFace = true;

    public Transform punch1;
    public float punchRadius = 0.2f;
    public float myTimerAttack = 0;
    public float groundRadius = 0.05f;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public Transform groundCheckFace;
    public Transform groundCheckFaceDown;
    public bool isSpeed;
    public float RadiusSee = 8f;
    public float RadiusLeave = 2f;
    public float heightYSee = 3f;
    public float RadiusAttack = 1.2f;
    public bool RepeatAttack = false;
    public bool _MPRepeat;
    private int Rand;

    public Vector2 RPosition;
    public bool RinDialogs;
    public bool RFacingRight;
    public bool RAttack;
    public float RHP;
    public bool RGrounded;
    private Information information;
    private PolygonCollider2D pc2d;
    private CircleCollider2D cc2d;
    void Awake(){
        GetComponent<EnemyHP>().interfaceDeath = this as IDeath;
    }
    void Start (){
        information = Information.Instance;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        pc2d = GetComponent<PolygonCollider2D>();
        cc2d = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update(){
        InformationAboutRooster();
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        groundedFace = Physics2D.OverlapCircle(groundCheckFace.position, groundRadius, whatIsGround);
        groundedFaceDown = Physics2D.OverlapCircle(groundCheckFaceDown.position, groundRadius, whatIsGround);
        anim.SetBool("Grounded", grounded);
        
        if ((isGame) && (ismove)&&(grounded)){
        if (myTimerAttack > 0){
            myTimerAttack -= Time.deltaTime;
        }else{
            if (grounded){
                ismove = true;
            }else{
            pc2d.enabled = false;  
            }
        }
            if ((RHP > 0)&&(!RinDialogs)){
                var x = RPosition.x;
                var y = RPosition.y;
                var position = tr.position;
                var x1 = position.x;
                var y1 = position.y;
                if((Mathf.Abs(x1-x)<RadiusSee)&&(Mathf.Abs(x1 - x) > RadiusLeave)&&(Mathf.Abs(y-y1)<heightYSee)&&(grounded)&&(!RinDialogs)&&(!RepeatAttack)){
                    pc2d.isTrigger = true;
                    cc2d.isTrigger = true; 
                    rb.gravityScale = 0;
                }else{
                    pc2d.isTrigger = false; 
                    cc2d.isTrigger = false; 
                    rb.gravityScale = 1;
                    anim.SetBool("Speed", false);
                }
                if ((x1 - x < RadiusSee) && (x1 - x > RadiusAttack)&&(Mathf.Abs(y-y1)<heightYSee)&&grounded&&(!RinDialogs) && !RepeatAttack){
                    rb.velocity = new Vector2(-4, 0);
                    if (!isFacingLeft)
                        Flip();
                    isFacingLeft = true;
                    anim.SetBool("Speed", true);
                    isSpeed = true;
                }
                if ((x1 - x > -RadiusSee) && (x1 - x < -RadiusAttack) && (Mathf.Abs(y - y1) < heightYSee)&&grounded&&(!RinDialogs) &&!RepeatAttack){
                    rb.velocity = new Vector2(4, 0);
                    if (isFacingLeft)
                        Flip();
                    isFacingLeft = false;
                    anim.SetBool("Speed", true);
                    isSpeed = true;
                }
                if (((Mathf.Abs(x - x1) > RadiusSee) || (Mathf.Abs(x - x1) < RadiusAttack)) && (Mathf.Abs(y - y1) < heightYSee) && (isSpeed)&&grounded&&(!RinDialogs) &&!RepeatAttack){
                    anim.SetBool("Speed", false);
                    isSpeed = false;
                }
                if ((Mathf.Abs(x - x1) <= RadiusAttack) && (myTimerAttack <= 0)&&(!isSpeed)&&grounded&&(!RinDialogs) &&!RepeatAttack){
                    anim.Play("Attack");
                    anim.SetBool("Attack", true);
                    myTimerAttack = 2f;
                }
                if((RepeatAttack)&&(_MPRepeat)){
                    pc2d.enabled = false; 
                    rb.gravityScale = 0;
                	if((Mathf.Abs(x1 - x) < Rand) && !groundedFace && grounded &&(!RinDialogs)&& groundedFaceDown){
                		if(isFacingLeft){
                			rb.velocity = new Vector2(-4, 0);
                		}else{
                			rb.velocity = new Vector2(4, 0);
                		}
                        anim.SetBool("Speed", true);
                	}else{
                        rb.velocity = new Vector2(0, 0);
                        anim.SetBool("Speed", false);
                		RepeatAttack = false;
                        pc2d.enabled = true; 
                        rb.gravityScale = 1;
                	}
                }else{
                    if(RepeatAttack){
                        RepeatAttack = false;
                    }
                }
            }
            else{
                anim.Play("Idle");
                rb.velocity= new Vector2(0, 0);
                anim.SetBool("Speed", false);    
            }
        }else{
            rb.velocity= new Vector2(0, 0);
            anim.SetBool("Speed", false);    
        }
    }
    private void Repeat(){
        if(RHP > 0){
            isSpeed = true;
            anim.Play("Run");
            anim.SetBool("Speed", true);
            Flip();
            isFacingLeft = !isFacingLeft;
            RepeatAttack = true;
            Rand = UnityEngine.Random.Range(2, 5);
        }
   	}
    private void Flip(){
        //получаем размеры персонажа
        Vector3 theScale = transform.localScale;
        //зеркально отражаем персонажа по оси Х
        theScale.x *= -1;
        //задаем новый размер персонажа, равный старому, но зеркально отраженный
        transform.localScale = theScale;
    }
    void AttackGo(){
        EnemyFight2D.Action(punch1.position, punchRadius, 10, 1, isFacingLeft,"Wolf");
    }
    void AttackStop(){
        anim.Play("Idle");
        anim.SetBool("Attack", false);
    }
    void InformationAboutRooster(){
        RPosition = information.position;
        RinDialogs = information.isDialog;
        RFacingRight = information.isFacingRight;
        RAttack = information.isAttack;
        RHP = ObserverHP.level;
    }
    public void Death(){
        gameObject.layer = 12;
        this.enabled = false;
    }
}
