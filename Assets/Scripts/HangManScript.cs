using UnityEngine;
using System.Collections;

public class HangManScript : MonoBehaviour, IDeath {
 public Rigidbody2D rb;
    public Transform tr;
    public bool isFacingLeft = true;
    public bool isGame = true;
    public bool ismove;
  
    private Animator anim;
    public bool grounded = true;
    public Transform punch1;

    public float punchRadius = 0.83f;
    public float myTimerAttack = 0;

    public float groundRadius = 0.05f;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public bool isSpeed;

    public float RadiusSee = 8;

    public float RadiusAttack = 1.5f;
    public int Damage = 3;

    public float RestTime = 4f; 
   
    public float SpeedRun = 2;

    float x,x1,y,y1;

    private Information information;
    public Vector2 RPosition;
    public bool RinDialogs;
    public bool RAttack;
    public float RHP;

    void Awake () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        information = Information.Instance;
        GetComponent<EnemyHP>().interfaceDeath = this as IDeath;
    }
    void Start(){
        leftWall.SetActive(true);
        BossHPUIScript.Instance.OpenSlider(GetComponent<EnemyHP>());
    }

    // Update is called once per frame
    void Update() {
        InformationAboutRooster();
        
        GroundCheckFinction();
        if (myTimerAttack > 0){
            myTimerAttack -= Time.deltaTime;
        }else{
            if (grounded){
                ismove = true;   
            }
        }
        
        if ((isGame) && (ismove)&&(grounded)){
            if (!RinDialogs && (RHP > 0)){
                GetPosition();
		        FindRooster();
                if (Mathf.Abs(x - x1) <= RadiusAttack){
                    SelfSave();
                    AttackRooster();
                }    
            }else{
                FindRooster();
            }
        }
    }
    void StartVeer(){
    	anim.SetBool("Veer", true);
    }
    private void Flip(){
        //получаем размеры персонажа
        Vector3 theScale = transform.localScale;
        //зеркально отражаем персонажа по оси Х
        theScale.x *= -1;
        //задаем новый размер персонажа, равный старому, но зеркально отраженный
        transform.localScale = theScale;
        anim.SetBool("Veer",false);
    }
   
    void AttackGo(){
        EnemyFight2D.Action(punch1.position, punchRadius, 10, Damage, isFacingLeft, true,"HangMan");
    }
    void AttackGoRun(){
        EnemyFight2D.Action(punch1.position, punchRadius, 10, 1, isFacingLeft,"HangManRun");
    }
   
    void AttackStop(){
    	anim.SetBool("Attack", false);
        anim.Play("Idle");
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
        if(grounded && (myTimerAttack <= 0)){
            if (((Mathf.Abs(x1-x) > RadiusSee)|| (Mathf.Abs(x1-x) < RadiusAttack)) && (Mathf.Abs(y1-y) < 2) && isSpeed){
                anim.SetBool("Speed", false);
                isSpeed = false;
            }
            if ((x1-x < RadiusSee) && ( x1-x > RadiusAttack) && (isFacingLeft) && (Mathf.Abs(y1-y) < 2)){
                rb.velocity = new Vector2(-SpeedRun, 0);
                anim.SetBool("Speed", true);
                isSpeed = true;
            }
            if ((x1-x > (-1)*RadiusSee) && (x1-x < -RadiusAttack)&& (!isFacingLeft) && (Mathf.Abs(y1-y) < 2)){
                rb.velocity = new Vector2(SpeedRun, 0);
                anim.SetBool("Speed", true);
                isSpeed = true;
            }
            
        } 
    }
    
    void SelfSave(){
        if(RAttack) {
            var Rand = UnityEngine.Random.Range(0, 100);
            if(Rand < 95){
                if(isFacingLeft){
                    rb.velocity = new Vector2(4, 5);
                }else{
                    rb.velocity = new Vector2(-4, 5);
                }
            }
        }    
    }

    public void AttackRooster(){
        GetPosition();
        if((Mathf.Abs(x1-x) <= RadiusAttack)&&(Mathf.Abs(y1-y) <= 2)){
            isSpeed = false;
            anim.SetBool("Speed", false);
            if ((myTimerAttack <= 0)&&(!isSpeed)){
                var Rand = UnityEngine.Random.Range(0, 100);
                
                if ((Rand >= 0) && (Rand < 50)){
                    anim.Play("Attack1");
                    anim.SetBool("Attack", true);
                }
                  if ((Rand >= 50) && (Rand < 100)){
                    anim.Play("Attack2");
                    anim.SetBool("Attack", true);
                }
                myTimerAttack = RestTime;
                ismove = false;
            }    
        }    
    }
    void InformationAboutRooster(){
        RPosition = information.position;
        RinDialogs = information.isDialog;
        RAttack = information.isAttack;
        RHP= ObserverHP.level;
    }
    [SerializeField] GameObject leftWall, rightwall;
    public void Death(){
        Destroy(leftWall);
        Destroy(rightwall);
        this.enabled = false;
        GetComponent<EnemyHP>().Speak();
    }
}