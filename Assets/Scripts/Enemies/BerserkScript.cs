using UnityEngine;
using System.Collections;


public class BerserkScript : MonoBehaviour, IDeath {
    public Rigidbody2D rb;
    public Transform tr;
    public bool isFacingLeft = true;
    public bool isGame = true;
    public bool ismove;
    public bool combo = false;
    private Animator anim;
    public bool grounded = true;
    public Transform punch1;
    public Transform punch2;
    public float punchRadius = 0.2f;
    public float myTimerAttack = 0;
    public float groundRadius = 0.3f;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public bool isSpeed;
    public float RadiusSee = 8;
    public float RadiusRun = 5;
    public float RadiusJump = 3;
    private bool JumpAttack;
    public int damage = 2;
    public float RadiusAttack = 1.5f;
    public float RestTime = 1.2f; 
   
    public float SpeedRun = 4;

    float x,x1,y,y1;

    private Information information;
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
        var Rand = UnityEngine.Random.Range(0,100);
        if(Rand < 60){
            JumpAttack = true; 
        }else{ JumpAttack = false;}
        
}

    // Update is called once per frame
    void Update() {
        InformationAboutRooster();
        GetPosition();
        GroundCheckFinction();
        if (myTimerAttack > 0){
            myTimerAttack -= Time.deltaTime;
        }else{
            if (grounded){
                ismove = true;   
            }
        }
		if (isGame && ismove){
            if(grounded){
                if ((RHP > 0)&&(!RinDialogs)){
                    GetPosition();
                    FindRooster();
                    
                    if (Mathf.Abs(x - x1) <= RadiusAttack){
                       SelfSave();
                       AttackRooster();
                    }
                }else{
                    FindRooster();
                }
            }else{
               if(JumpAttack) 
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
        EnemyFight2D.Action(punch1.position, punchRadius, 10, damage, isFacingLeft,"Berserk");
    }
    void AttackComboEnd(){
        combo = false;
        EnemyFight2D.Action(punch2.position, punchRadius, 10, damage, isFacingLeft,"Berserk");
    }
    void AttackStop(){
        anim.Play("Idle");
    }
    void AttackComboStop(){
        Flip();
        AttackStop();
    }
    void AttackComboJump(){
        anim.Play("Jump");
        if (isFacingLeft){
            rb.velocity = new Vector2(-5, 8);
        }else{
            rb.velocity = new Vector2(5, 8);
        }
    }

    void LandAttack(){
     if(grounded){
            anim.Play("LandAttack");
        }
    }
    void GroundCheckFinction(){
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        if(grounded){
            anim.SetBool("Grounded", true);
        }else{
            anim.SetBool("Grounded", false);
        }
    }
    void GetPosition(){
        x = RPosition.x;
        y = RPosition.y;

        x1 = tr.position.x;
        y1 = tr.position.y;
    }
    void FindRooster (){
        if(grounded){
            if (((Mathf.Abs(x1-x) > RadiusSee)|| (Mathf.Abs(x1-x) < RadiusAttack)) && (Mathf.Abs(y1-y) < 2) && isSpeed){
                anim.Play("Idle");
                anim.SetBool("Speed", false);
                isSpeed = false;
            }
            if ((x1-x < RadiusSee) && ( x1-x > RadiusRun)&&(Mathf.Abs(y1-y) < 2)){
                rb.velocity = new Vector2(-SpeedRun/2, 0);
                if (!isFacingLeft) Flip();
                isFacingLeft = true;
                anim.Play("Run");
                anim.SetBool("Speed", true);
                isSpeed = true;
            }
            if ((x1-x > (-1)*RadiusSee) && (x1-x < -RadiusRun) && (Mathf.Abs(y1-y) < 2)){
                rb.velocity = new Vector2(SpeedRun/2, 0);
                if (isFacingLeft) Flip();
                isFacingLeft = false;
                anim.Play("Run");
                anim.SetBool("Speed", true);
                isSpeed = true;
            }
            if(JumpAttack == false){        
                if ((x1-x < RadiusRun) && (x1-x > RadiusAttack)&&(Mathf.Abs(y1-y) < 2)){
                    rb.velocity = new Vector2(-SpeedRun, 0);
                    if (!isFacingLeft) Flip();
                    isFacingLeft = true;
                    anim.Play("Attack1");
                    anim.SetBool("Speed", true);
                    isSpeed = true;
                }
                if ((x1-x > (-1)*RadiusRun) && (x1-x < -RadiusAttack) && (Mathf.Abs(y1-y) < 2)){
                    rb.velocity = new Vector2(SpeedRun, 0);
                    if (isFacingLeft) Flip();
                    isFacingLeft = false;
                    anim.Play("Attack1");
                    anim.SetBool("Speed", true);
                    isSpeed = true;
                }
            }else{
                if(Mathf.Abs(x1-x-RadiusJump)< 1){
                    AttackComboJump();
                }
            }    
        }else{
            if(JumpAttack){
                if(Mathf.Abs(x1-x-1) < RadiusAttack){
                    AttackRooster();
                }   
            }
        } 
    }
    
    void SelfSave(){
        if(RAttack){
            var Rand = UnityEngine.Random.Range(0, 100);
            if(Rand < 15){
                if(isFacingLeft){
                    rb.velocity = new Vector2(3, 4);
                }else{
                    rb.velocity = new Vector2(-3, 4);
                }
            }
        }    
    }

    public void AttackRooster(){
        GetPosition();
        if(grounded){
           
        if((Mathf.Abs(x1-x) <= RadiusAttack)&&(Mathf.Abs(y1-y) <= 2)){
            isSpeed = false;
            anim.SetBool("Speed", false);
            if ((myTimerAttack <= 0)&&(!isSpeed)){
                var Rand = UnityEngine.Random.Range(0, 100);
                
                if ((Rand >= 0) && (Rand < 60)){
                    anim.Play("Attack2");
                    anim.SetBool("Attack", true);
                }

                if ((Rand >= 60) && (Rand < 80)){
                    anim.Play("Attack3");
                    anim.SetBool("Attack", true);
                    CameraShake.Shake(1, 1, CameraShake.ShakeMode.XY);
                }

               
                if ((Rand < 100) && (Rand >= 90)){
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
        }else{
           if((x1 - x < RadiusAttack) &&(Mathf.Abs(y-y1)<2)){
            anim.Play("Attack3");
            myTimerAttack = RestTime;
            JumpAttack = false;
           } 
        }
    }
    void InformationAboutRooster(){
        RPosition = information.position;
        RinDialogs = information.isDialog;
        RAttack = information.Attack;
        RHP= ObserverHP.level;
    }
    public void Death(){
        gameObject.layer = 12;
        if(gameObject.name == "Berserk_for_speak")
            GetComponent<EnemyHP>().Speak();
        this.isGame = false;
    }
}
