using UnityEngine;
using System.Collections;

public class RoosterScriptWizard : MonoBehaviour {

	public bool isFacingRight = true;
	private float move;
    float v;

	public bool grounded = true;
	public float groundRadius = 0.05f;
	public LayerMask whatIsGround;
	public float SpeedRun = 4;
	public float PowerJump = 9f;

	public bool isGame = true;
    public bool isMove = true;
    public bool Attack = false;
    public bool Block = false;
    public bool isDialog = false;

    public AudioClip AStrike;

    public Transform punch1;
    public Transform punchBlock;
    public float punchRadius;
    public float punchBlockRadius;
    public float myTimerAttack;

    public bool Stun = false;
    public float StunTime;

	public Animator anim;
	public Rigidbody2D rb;
	public Transform tr;
	public Transform groundCheck;

    public GameObject ColdPrefab;
    public GameObject FirePrefab;
    private Vector3 position;
    private Quaternion rotation;

	private Vector3 upLadder, downLadder, ladderPos, direction;
    private bool isLadder;
    public string ladderTag = "GameController";
    public int armor;
    public int weapon;
    public int MP;
    public bool HorizontalStop = true;
    [Header("Cost magic")]
    public int costCold = 4;
    public int costFire = 6;
    public int costBlock = 8;
    public int costNecr = 15;
    public Transform aimCenter;
    private SpriteRenderer aimSprite;
    private Vector3 mousePos;
    private Information information;
    private ManaBar manaBar;
    private Camera cam;
    private SpriteRenderer stunObject;
    private PlayerHP playerHP;
    private StaminaControllerScript staminaController;

	// Use this for initialization
	void Start () {
        playerHP = GetComponent<PlayerHP>();
        aimSprite = aimCenter.Find("Aim").GetComponent<SpriteRenderer>();
        cam = Camera.main;
        manaBar = GetComponent<ManaBar>();
        information = Information.Instance;
        MP = information.MP;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>(); 
        Dialogs.Instance.RegisterOnDialog(OnDialog);
        stunObject = transform.Find("StunEffect").GetComponent<SpriteRenderer>();
        staminaController = GetComponent<StaminaControllerScript>();

	}
	
	// Update is called once per frame
	void Update () {
        if(Stun){
            if(Attack) Attack = false; 
            if(StunTime > 0){
                StunTime -= Time.deltaTime; 
               stunObject.enabled = true;
            }else{
                Stun = false;
                stunObject.enabled = false;
            }
        }
        GroundCheck();
        ChangeInformation();
        if(!isDialog && !Stun && isGame){
            if((Block)&&(GameInput.Key.GetKeyUp("Q"))){
                anim.Play("Idle");
                anim.SetBool("Block", false);
                playerHP.isblock = false;
                Block = false;
            }
			if(!Attack && !Block){
                mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                if( ((mousePos.x > tr.position.x) && isFacingRight) || ((mousePos.x <= tr.position.x) && !isFacingRight) ){
                    aimSprite.enabled = true;
                    aimCenter.rotation = Quaternion.Euler(0, 0, -Mathf.Atan2(mousePos.x - tr.position.x, mousePos.y - tr.position.y) * Mathf.Rad2Deg);
                }else{
                    aimSprite.enabled = false;
                }
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
                if(GameInput.Key.GetKeyUp("A")||GameInput.Key.GetKeyUp("D")){
                    move = 0f;
                }
                wMove();
                if(move == 0 && grounded){
                    anim.SetBool("Speed", false);
                }
                if(GameInput.Key.GetKey("E")&&(!Attack)&& grounded && staminaController.ResolutionForAttack()){
                    move = 0f;
                    anim.SetBool("Attack", true);
                    Attack = true;
                    RoosterAttack();
                }
                if(GameInput.Key.GetKeyDown("Z")&&(!Attack)&&(MP >= costCold)&& !isLadder && aimSprite.enabled  && staminaController.ResolutionForAttack()){
                    move = 0f;
                    anim.SetBool("Attack", true);
                    Attack = true;
                    RoosterAttackCold();
                }
                if(GameInput.Key.GetKeyDown("X")&&(!Attack)&&(MP >= costFire)&& !isLadder && aimSprite.enabled && staminaController.ResolutionForAttack()){
                    move = 0f;
                    anim.SetBool("Attack", true);
                    Attack = true;
                    RoosterAttackFire();
                }
                if(GameInput.Key.GetKeyDown("C")&&(!Attack)&&(MP >= costNecr)&& !isLadder){
                    move = 0f;
                    anim.SetBool("Attack", true);
                    Attack = true;
                    RoosterNecromantsAttack();
                }
                if (((GameInput.Key.GetKeyDown("Q"))||(GameInput.Key.GetKey("Q")))&&(MP >= costBlock)&& grounded) {
                    move = 0f;
                    Block = true;
                    anim.SetBool("Speed", false);
                    anim.SetBool("Block", true);
                    playerHP.isblock = true;
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
            move = 0f;
            if(Stun){
                    anim.SetBool("Speed", false);
                    anim.SetBool("Attack", false);
                }
        }
	}
	void GroundCheck(){
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
	   anim.SetBool("Grounded",grounded);
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
    public void Flip(){
        Information.Instance.isFacingRight = isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
     void ChangeInformation(){
        information.position = new Vector2(tr.position.x, tr.position.y);
        information.isFacingRight = isFacingRight;
        information.Attack = Attack;
        information.isDialog = isDialog;
        information.isGame = isGame;
        information.isLadder = isLadder;
        information.Grounded = grounded;
        if(MP >= 0) manaBar.MP = MP; 
    }
   
    void RoosterAttack(){
        anim.Play("Attack3");
    }
    void AttackEnd(){
        staminaController.DecreaseAttackStamina();
        anim.SetBool("Attack", false);
        Attack = false;
    }
    void AttackGo(){
        Fight2D.Action(punch1.position, punchRadius, 9, 1, isFacingRight);
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
            float xPos;
            if(!HorizontalStop){
                xPos = Mathf.Lerp(transform.position.x, ladderPos.x, 10 * Time.deltaTime);
            }else{
                xPos = Mathf.Lerp(transform.position.x, ladderPos.x, Time.deltaTime);
            }
            if(Mathf.Abs(transform.position.x - ladderPos.x) < 0.5f){
                if(rb.velocity.y != 0){
                    transform.position = new Vector2(ladderPos.x, transform.position.y);
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }
            }
            transform.position = new Vector2(xPos, transform.position.y); // плавное выравнивание по центру лестницы
        }
    }
    void RoosterAttackCold(){
        MP -= costCold;
        information.MP = MP;
        anim.Play("Attack1");

    }
    void RoosterMagicBlock(){
        if(MP >= costBlock){
            MP -= costBlock;
            Fight2D.Action(punchBlock.position, punchBlockRadius, 9, 1, isFacingRight);
        }else{
            Block = false;
            anim.SetBool("Block", false);
            gameObject.GetComponent<PlayerHP>().isblock = false;
        }
    }
    void RoosterAttackFire(){
        MP -= costFire;
        information.MP = MP;
        anim.Play("Attack2");
    }
    void RoosterNecromantsAttack(){
        GetComponent<PlayerHP>().AddHP(1);
        MP -= costNecr;
        information.MP = MP;
        anim.Play("Attack4");
    }

    void CreateCold(){
         if (isFacingRight){
            position = new Vector3(0.6f, 0.6f, 0);
        }else{
            position = new Vector3(-0.6f, 0.6f, 0);
        }

        position += tr.position;
        GameObject bullet = Instantiate(ColdPrefab,position,aimCenter.rotation);
        bullet.GetComponent<RoostersMagicFly>().DoImpulse(tr.position, mousePos);
    }
    void CreateFire(){
         if (isFacingRight){
           position = new Vector3(0.8f, -0.1f, 0);
        }else{
            position = new Vector3(-0.8f, -0.1f, 0);
        }

        position += tr.position;
        rotation = Quaternion.Euler(0, 0, 0);
        GameObject bullet = Instantiate(FirePrefab,position,aimCenter.rotation);
        bullet.GetComponent<RoostersMagicFly>().DoImpulse(tr.position, mousePos);
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
    
    [ContextMenu("add 1000 MP")]
    void CheatAdd1000MP(){
        Information.Instance.MP += 1000;
        MP += 1000;
    }
}
