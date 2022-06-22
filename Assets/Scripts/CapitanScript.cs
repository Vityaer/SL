using UnityEngine;
using System.Collections;

public class CapitanScript : MonoBehaviour, IDeath {
 public Rigidbody2D rb;
    public Transform tr;
    public bool isFacingLeft = true;
    public bool isGame = true;
    public bool ismove;
    private Animator anim;
    public bool grounded = true;
    public Transform punch1;
    public float punchRadius = 0.2f;
    public float myTimerAttack = 0;
    public float TimerBlock = 5f;
    public float groundRadius = 0.3f;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public bool isSpeed;
    public bool isBlock;
    public float RadiusSee = 8;
    public float RadiusRun = 5;
    public int damage = 3;
    public float RadiusAttack = 2f;
    public float RadiusBlock = 0.7f;
    public float RestTime = 1.2f; 
   
    public float SpeedRun = 4;
    public Vector2 RememberRooster;

    float x,x1,y,y1;

    public Information information;
    public Vector2 RPosition;
    public bool RinDialogs;
    public bool RFacingRight;
    public bool RAttack;
    public float RHP;
    void Awake(){
        GetComponent<EnemyHP>().interfaceDeath = this as IDeath;
    }
    void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        information = Information.Instance;
}

    // Update is called once per frame
    void Update() {
        InformationAboutRooster();
        GroundCheck();
        if (myTimerAttack > 0){
            myTimerAttack -= Time.deltaTime;
        }else{
            if (grounded){
                ismove = true;   
            }
        }
		if ((isGame) && (ismove)&&(grounded)&& (RHP > 0)){
            if (!RinDialogs){
                GetPosition();
		        FindRooster();
               	
            	if(!isBlock){ 
	                if (Mathf.Abs(x - x1) <= RadiusAttack){
	                    SelfSave();
	                    AttackRooster();
	                }else{
	                	isBlock = false;
	                	GetComponent<EnemyHP>().isBlock = false;
	                }    
                }else{
                    if (TimerBlock > 0)
                        TimerBlock -= Time.deltaTime;
                	if((TimerBlock <= 0) || (Mathf.Abs(x - x1) > RadiusAttack)){
                		isBlock = false;
                        anim.SetBool("Block", false);
	                	GetComponent<EnemyHP>().isBlock = false;
                	}
                }
            }else{
                FindRooster();
            }
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
    void AttackGo(){
        EnemyFight2D.Action(punch1.position, punchRadius, 10, damage, isFacingLeft, true, "Capitan");
    }
  
    void AttackStop(){
        anim.SetBool("Attack", false);
        anim.Play("Idle");
    }

   
    void GroundCheck(){
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        if(grounded){
            anim.SetBool("Grounded", true);
        }else{
            anim.SetBool("Grounded", false);
        }
    }
    void GetPosition(){
        InformationAboutRooster();
        if(TimerBlock > 0){
            x = RPosition.x;
            y = RPosition.y;
        }
        x1 = tr.position.x;
        y1 = tr.position.y;
    }
   
    void FindRooster (){
        if(grounded && !isBlock && (myTimerAttack <= 0)){
            if(Mathf.Abs(x1-x) < RadiusSee){
                if (((Mathf.Abs(x1-x) > RadiusSee)|| (Mathf.Abs(x1-x) < RadiusAttack)) && (Mathf.Abs(y1-y) < 2) && isSpeed){
                    anim.Play("Idle");
                    anim.SetBool("Speed", false);
                    isSpeed = false;
                }
                if ((x1-x < RadiusSee) && ( x1-x > RadiusAttack)&&(Mathf.Abs(y1-y) < 2)){
                    rb.velocity = new Vector2(-SpeedRun, 0);
                    anim.Play("Run");
                    anim.SetBool("Speed", true);
                    isSpeed = true;
                }
                if ((x1-x > (-1)*RadiusSee) && (x1-x < -RadiusAttack) && (Mathf.Abs(y1-y) < 2)){
                    rb.velocity = new Vector2(SpeedRun, 0);
                    anim.Play("Run");
                    anim.SetBool("Speed", true);
                    isSpeed = true;
                }
                if(((x1-x) < RadiusSee)&&( (x1-x) > 0)){
                    if (!isFacingLeft) Flip();
                    isFacingLeft = true;
                }
                if(((x1-x) > -RadiusSee)&&((x1-x)< 0)){
                    if (isFacingLeft) Flip();
                    isFacingLeft = false;
                }
            }
        } 
    }
    
    void SelfSave(){
        if(RAttack){
            var Rand = UnityEngine.Random.Range(0, 100);
            if(Rand < 80){
                anim.Play("BlockStart");
                isBlock = true;
                anim.SetBool("Block", true);
	            GetComponent<EnemyHP>().isBlock = true;
            }
        }else{
            var Rand = UnityEngine.Random.Range(0, 100);
            if(Rand < 25){
                anim.Play("BlockStart");
                anim.SetBool("Block", true);
                isBlock = true;
                GetComponent<EnemyHP>().isBlock = true;
            }
        }   
    }

    public void AttackRooster(){
        GetPosition();
        if((Mathf.Abs(x1-x) <= RadiusAttack)&&(Mathf.Abs(x1-x) >= RadiusBlock)&&(Mathf.Abs(y1-y) <= 2)){
            isSpeed = false;
            anim.SetBool("Speed", false);
            if ((myTimerAttack <= 0)&&(!isSpeed)){
                var Rand = UnityEngine.Random.Range(0, 100);
                if (Rand < 80){
                    anim.Play("Attack1");
                    anim.SetBool("Attack", true);
                }else{
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
        if((Mathf.Abs(x1-x) <= RadiusBlock)&&(Mathf.Abs(y1-y) <= 2)){
            isSpeed = false;
            anim.SetBool("Speed", false);
            if ((myTimerAttack <= 0)&&(!isSpeed)){
                var Rand = UnityEngine.Random.Range(0, 100);
                
                if ((Rand >= 0) && (Rand < 20)){
                    anim.Play("Attack1");
                    anim.SetBool("Attack", true);
                }

                if ((Rand >= 20) && (Rand < 70)){
                    anim.Play("Attack2");
                    anim.SetBool("Attack", true);
                }

               
                if ((Rand < 100) && (Rand >= 70)){
                    if (isFacingLeft){
                        rb.velocity = new Vector2(6, 8);
                    }else{
                        rb.velocity = new Vector2(-6, 8);
                    }
                }
               
                myTimerAttack = RestTime;
                ismove = false;
            }    
        }    
    }
    void Block(){
        TimerBlock = 5f;
    	anim.Play("Block");
    }
    void InformationAboutRooster(){
        RPosition = information.position;
        RinDialogs = information.isDialog;
        RFacingRight = information.isFacingRight;
        RAttack = information.isAttack;
        RHP= ObserverHP.level;
    }
    public void Death(){
        SteamAchievementsScript.Instance.UnlockAchievment("ACH_CAPTAIN_WIN");
        this.enabled = false;
    }
}
