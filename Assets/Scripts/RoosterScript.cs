using UnityEngine;
using System.Collections;

public class RoosterScript : MonoBehaviour {
	
	public bool isFacingRight = true;
    public bool isGame = true;
    public bool isMove = true;
    public bool Attack = false;

    public bool isDialog = false;

    public float SpeedRun = 3;
    private float move;
    float v;
    public float PowerJump = 9f;


    public Animator anim;
    public Rigidbody2D rb;
    public Transform tr;
    public Transform groundCheck;
    public bool grounded = true;
    
    public float groundRadius = 0.05f;  
    public LayerMask whatIsGround;

    public Vector2 VAttack1, VAttack2, VAttack3;

  
    public AudioClip AStrike;

    public Transform punch1;
    public float punchRadius;
    public float myTimerAttack;

    public bool Stun = false;
    public float StunTime;

    private Vector3 upLadder, downLadder, ladderPos, direction;
    public bool isLadder;
    public string ladderTag = "GameController";
    public bool HorizontalStop = true;
    public int armor;
    public int weapon;
    private Information information;
    private StaminaControllerScript staminaController;
	void Start () {
        information = Information.Instance;
		anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();	
        Dialogs.Instance.RegisterOnDialog(OnDialog);
        staminaController = GetComponent<StaminaControllerScript>();
	}
	
	void FixedUpdate () {
        if(Stun){
            if(Attack) Attack = false; 
            if(StunTime > 0){
                StunTime -= Time.deltaTime; 
                GameObject.Find("StunEffect").GetComponent<SpriteRenderer>().enabled = true;
            }else{
                Stun = false;
                GameObject.Find("StunEffect").GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        ChangeInformation();
		if(!isDialog && !Stun && isGame){
			GroundCheck();
			if(!Attack){
                move = 0f;
    			if(GameInput.Key.GetKey("A")){
                    move = -1f;
    				if(isFacingRight){
    					Flip();
    					isFacingRight = false;
    				}
    			}
                if(GameInput.Key.GetKey("D")){
                    move = 1f;
                    if(!isFacingRight){
                        Flip();
                        isFacingRight = true;
                    }
                }
                wMove();
                if(GameInput.Key.GetKeyUp("A")||GameInput.Key.GetKeyUp("D")){
                    move = 0f;
                }
                if(move == 0){
                    anim.SetBool("Speed", false);
                    Attack = false;
                }
                if(GameInput.Key.GetKey("E") && (!Attack) && grounded && staminaController.ResolutionForAttack() ){
                    RoosterAttack();
                }
                if (grounded && !isLadder && GameInput.Key.GetKey("Space")){
                    rb.velocity = (new Vector2(0f, PowerJump));
                } 
                if(GameInput.Key.GetKey("W")){
                    v = 1f;
                }
                if(GameInput.Key.GetKey("S")){
                    v = -1f;
                }
                if(GameInput.Key.GetKeyUp("W") || GameInput.Key.GetKeyUp("S")){
                    v = 0f;
                }
                if (isLadder) {
                    LadderMode(v, move);
                    if(!grounded){
                        anim.SetBool("IsLadder", true);
                        if(v != 0){
                            anim.speed = 1;
                        }else{
                            anim.speed = 0;
                        }
                    }else{
                        anim.speed = 1;
                        anim.SetBool("IsLadder", false);
                    }
                }else{
                    anim.speed = 1;
                    anim.SetBool("IsLadder", false);
                }   
            }     
        }else{
            if(Stun){
                    anim.SetBool("Speed", false);
                    anim.SetBool("Attack", false);
                }
        }
	}
	void GroundCheck(){
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Grounded", grounded);
	}
	public void Flip(){
        Information.Instance.isFacingRight = isFacingRight;
        Vector3 theScale = transform.localScale;
        //зеркально отражаем персонажа по оси Х
        theScale.x *= -1;
        //задаем новый размер персонажа, равный старому, но зеркально отраженный
        transform.localScale = theScale;
    }
    void ChangeInformation(){
        if(information != null){
            information.position = new Vector2(tr.position.x, tr.position.y);
            information.isFacingRight = isFacingRight;
            information.isAttack = Attack;
            information.isDialog = isDialog;
            information.isGame = isGame;
            information.Grounded = grounded;
            information.isLadder = isLadder;
        }
    }
    void wMove(){
        anim.SetBool("Speed", true);
        if(!isLadder){
            rb.velocity = new Vector2(move * SpeedRun, rb.velocity.y);
        }else{
            if(!HorizontalStop)
                rb.velocity = new Vector2(move * SpeedRun, rb.velocity.y);
        }
    }
    void RoosterAttack(){
        anim.SetBool("Speed",false);
        anim.SetBool("Attack", true);
        Attack = true;
        var Rand = UnityEngine.Random.Range(0, 90);
        if(Rand < 30){
            anim.Play("Attack1");
            if(isFacingRight){
                rb.velocity = new Vector2(VAttack1.x, rb.velocity.y + VAttack1.y);
            }else{
                rb.velocity = new Vector2(-VAttack1.y, rb.velocity.y + VAttack1.y);
            }
        }
        if((Rand >= 30)&&(Rand < 60)){
            anim.Play("Attack2");
            if(isFacingRight){
                rb.velocity = new Vector2(VAttack2.x, rb.velocity.y + VAttack2.y);
            }else{
                rb.velocity = new Vector2(-VAttack2.x, rb.velocity.y + VAttack2.y);
            }
        }
        if(Rand >= 60){
            anim.Play("Attack3");
            if(isFacingRight){
                rb.velocity = new Vector2(VAttack3.x, rb.velocity.y + VAttack3.y);
            }else{
                rb.velocity = new Vector2(-VAttack3.x, rb.velocity.y + VAttack3.y);
            }
        }
    }
    void AttackEnd(){
        staminaController.DecreaseAttackStamina();
        anim.SetBool("Attack", false);
        Attack = false;
    }
     void AttackGo(){
        int damage = 0;
        switch(weapon){
            case 0:
                damage = 1;
                break;
            case 1:
                damage = (UnityEngine.Random.Range(0, 100) < 25) ? 5 : 1;
                break;
            case 2:
                damage = 1;
                break;
            case 3:
                damage = 3;
                break;
            default:
                break;
        }
        Fight2D.Action(punch1.position, punchRadius, 9, damage, isFacingRight);
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
        else if(transform.position.y > downLadder.y && vertical < 0 && transform.position.y > upLadder.y){
            rb.isKinematic = true;
        }
        else if(vertical < 0 && grounded && transform.position.y <= upLadder.y){
            rb.isKinematic = false;
        }
        if(vertical == 0 && horizontal == 0){
            rb.velocity = new Vector2(0f,0f);
        }
        else if(vertical != 0 && horizontal == 0){
            rb.velocity = new Vector2(0f,rb.velocity.y);
        }
        else if(vertical == 0 && horizontal != 0){
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
        if(grounded){
            HorizontalStop = false;
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
