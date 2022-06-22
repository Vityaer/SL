using UnityEngine;
using System.Collections;

public class RoosterScriptWithShield : MonoBehaviour {

	public bool isFacingRight = true;
    public bool isGame = true;
    public bool isMove = true;
    public bool Attack = false;
    public bool Block = false;

    public bool isDialog = false;

    public float SpeedRun = 3;
    private float move;
    float v;
    public float PowerJump = 9f;


    private Animator anim;
    private Rigidbody2D rb;
    private Transform tr;
    public Transform groundCheck;
    public bool grounded = true;
    
    public float groundRadius = 0.05f;  
    public LayerMask whatIsGround;

    public Vector2 VAttack1, VAttack2;
  
    public AudioClip AStrike;

    public Transform punch1;
    public Transform punch2;

    public float punchRadius;
    public float myTimerAttack;

    public bool Stun = false;
    public float StunTime;

    private Vector3 upLadder, downLadder, ladderPos, direction;
    private bool isLadder;
    public string ladderTag = "GameController";
    public bool HorizontalStop = true;
    public int armor;
    public int weapon;
    private Information information;
    private PlayerHP componentPlayerHP; 
    private StaminaControllerScript staminaController;

	// Use this for initialization
	void Start () {
        information = Information.Instance;
		anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();	
        componentPlayerHP = GetComponent<PlayerHP>();
        Dialogs.Instance.RegisterOnDialog(OnDialog);
        staminaController = GetComponent<StaminaControllerScript>();
	}
	
	// Update is called once per frame
	void Update () {
        if(Stun){
            if(!Block){
                if(Attack) Attack = false; 
                if(StunTime > 0){
                    StunTime -= Time.deltaTime;
                    GameObject.Find("StunEffect").GetComponent<SpriteRenderer>().enabled = true;
                }else{
                    Stun = false;
                    GameObject.Find("StunEffect").GetComponent<SpriteRenderer>().enabled = false;
                }
            }else{
                Stun = false;
                GameObject.Find("StunEffect").GetComponent<SpriteRenderer>().enabled = false;
            }}
		GroundCheck();
        ChangeInformation();
        move = 0f;
        if(!isDialog && !Stun && isGame){
            if((Block)&&(GameInput.Key.GetKeyUp("Q"))){
                anim.SetBool("Block", false);
                componentPlayerHP.isblock = false;
                Block = false;
            }
            if(!Attack){
                if(!GameInput.Key.GetKeyDown("E")){
                    if(GameInput.Key.GetKey("A")){
                        move = -1f;
                        isFacingRight = false;
                        Information.Instance.isFacingRight = isFacingRight;
                    }
                    if(GameInput.Key.GetKey("D")){
                        move = 1f;
                        isFacingRight = true;
                        Information.Instance.isFacingRight = isFacingRight;
                    }
                    if(GameInput.Key.GetKeyUp("A")||GameInput.Key.GetKeyUp("D")){
                        move = 0f;
                    }
                }               
                anim.SetBool("IsFacingRight", isFacingRight); 
    			if(!Block){
                    wMove();
                    if(GameInput.Key.GetKey("E") && grounded && staminaController.ResolutionForAttack()){
                        move = 0f;
                        Attack = true;
                        RoosterAttack();
                    }
                     if ((GameInput.Key.GetKeyDown("Q"))||(GameInput.Key.GetKey("Q")) && grounded) {
                        Block = true;
                        anim.SetBool("Block", true);
                        anim.SetBool("Speed",false);
                        componentPlayerHP.isblock = true;
                    }
                    if (grounded && (GameInput.Key.GetKeyDown("Space"))){
                        rb.velocity = (new Vector2(0f, PowerJump));
                    } 
                    if(GameInput.Key.GetKey("W")){
                        v = 1f;
                    }
                    if(GameInput.Key.GetKey("S")){
                        v = -1f;
                    }
                    if(GameInput.Key.GetKeyUp("W")||GameInput.Key.GetKeyUp("S")){
                        v = 0f;
                    }
                    if (isLadder) {
                        LadderMode(v, move);
                        if(!grounded){
                            anim.SetBool("IsLadder", true);
                            anim.speed = (v != 0) ? 1 : 0;
                        }else{
                            anim.speed = 1;
                            anim.SetBool("IsLadder", false);
                        }
                    }else{
                        anim.SetBool("IsLadder", false);
                        anim.speed = 1;
                    }
                }     
            }else{
                if(Stun){
                    anim.SetBool("Speed", false);
                    anim.SetBool("Attack", false);
                }
            }
        }
	}
	void GroundCheck(){
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
	    anim.SetBool("Grounded", grounded);
    }
	public void Flip(){
        anim.SetBool("IsFacingRight", isFacingRight);
    }
    void ChangeInformation(){
        information.position = new Vector2(tr.position.x, tr.position.y);
        information.isFacingRight = isFacingRight;
        information.isAttack = Attack;
        information.isDialog = isDialog;
        information.isGame = isGame;
        information.Grounded = grounded;
        information.isLadder = isLadder;
    }
    void wMove(){
        if(grounded) anim.SetBool("Speed", (Mathf.Abs(move) > 0) ? true : false);
        if(!isLadder){
            rb.velocity = new Vector2(move * SpeedRun, rb.velocity.y);
        }else{
            if(!HorizontalStop)
                rb.velocity = new Vector2(move * SpeedRun, rb.velocity.y);
        }
    }
    void RoosterAttack(){
        var Rand = UnityEngine.Random.Range(0, 60);
        anim.SetBool("Attack", true);
        if(Rand < 30){
            if(isFacingRight){
                rb.velocity = new Vector2(VAttack1.x, rb.velocity.y + VAttack1.y);
                anim.Play("Attack1_right");
            }else{
                rb.velocity = new Vector2(-VAttack1.y, rb.velocity.y + VAttack1.y);
                anim.Play("Attack1_left");
            }
        }
        if((Rand >= 30)&&(Rand < 60)){
            if(isFacingRight){
                anim.Play("Attack2_right");
                rb.velocity = new Vector2(VAttack2.x, rb.velocity.y + VAttack2.y);
            }else{
                anim.Play("Attack2_left");
                rb.velocity = new Vector2(-VAttack2.x, rb.velocity.y + VAttack2.y);
            }

        }
    }
    void AttackEnd(){
        staminaController.DecreaseAttackStamina();
        anim.SetBool("Attack", false);
        Attack = false;
    }
     void AttackGo(){
        if(isFacingRight){
            Fight2D.Action(punch1.position, punchRadius, 9, 1, isFacingRight);
        }else{
            Fight2D.Action(punch2.position, punchRadius, 9, 1, isFacingRight);
        }
    }
    void OnTriggerStay2D(Collider2D other){
        if (other.tag == ladderTag && !isLadder){
            if(v != 0){
                rb.velocity = new Vector2(rb.velocity.x,0f);
                Ladder ladder = other.GetComponent<Ladder>();
                upLadder = ladder.up.position;
                downLadder = ladder.down.position;
                ladderPos = other.transform.position;
                HorizontalStop = ladder.HorizontalStop;
                if(grounded){
                    HorizontalStop = false;
                }
                isLadder = true;
            }
        }
        if(other.tag == ladderTag && isLadder){
            if(other.GetComponent<Ladder>().HorizontalStop){
                if(!grounded){
                    HorizontalStop = true;
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if (other.tag == ladderTag){
            rb.gravityScale = 1;
            isLadder = false;
            rb.isKinematic = false;
            anim.SetBool("IsLadder", false);
            anim.speed = 1;
        }
    }
    void LadderMode(float vertical, float horizontal){
        if(transform.position.y < upLadder.y && vertical > 0){
            rb.isKinematic = true;
        }
        else if(transform.position.y > downLadder.y &&  vertical < 0 && transform.position.y > upLadder.y){
            rb.isKinematic = true;
        }
        else if(vertical < 0 && grounded && transform.position.y <= upLadder.y){
            rb.isKinematic = false;
        }
        if(grounded){
            HorizontalStop = false;
        }
        if(vertical == 0 && horizontal == 0){
            rb.velocity = new Vector2(0f,0f);
        }
        else if(vertical != 0 && horizontal == 0){
            rb.velocity = new Vector2(0f,rb.velocity.y);
        }
        else if(vertical == 0 && horizontal != 0){
            if(!HorizontalStop)
                rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
        if(rb.isKinematic && Mathf.Abs(vertical) > 0){
            rb.gravityScale = 0;
            transform.Translate(new Vector2(0, 2f * vertical * Time.deltaTime)); // движение по лестнице
            float xPos = Mathf.Lerp(transform.position.x, ladderPos.x, 10 * Time.deltaTime);
            if(Mathf.Abs(transform.position.x - ladderPos.x) < 0.5f){
                if(rb.velocity.y != 0){
                    transform.position = new Vector2(ladderPos.x, transform.position.y);
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }
            }
            transform.position = new Vector2(xPos, transform.position.y); // плавное выравнивание по центру лестницы
        }
    }
    void OnDestroy(){
        if(Dialogs.Instance != null)
        Dialogs.Instance.UnRegisterOnDialog(OnDialog);
    }
    public void OnDialog(bool isDialog){
        isMove = !isDialog;
        this.isDialog = isDialog;
        anim.SetBool("Speed", false);
        anim.SetBool("Grounded", true);
    }
}
