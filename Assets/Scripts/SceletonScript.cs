using UnityEngine;
using System.Collections;

public class SceletonScript : MonoBehaviour, IDeath {
    public Rigidbody2D rb;
    public Transform tr;
    private bool isFacingLeft = true;
    public bool isGame = true;
    public bool ismove = true;
    private Animator anim;
    public Transform punch1;
    public float punchRadius = 0.2f;
    public float myTimerAttack = 0;

    public int AttackDamage = 3;
    public float RadiusSee = 5;
    public float RadiusAttack = 1.7f;
    public float SpeedRun = 1;
    public float groundRadius = 0.3f;
    public LayerMask whatIsGround;
     public Transform groundCheck;
     public bool grounded = true;

    public Information information;
    public Vector2 RPosition;
    public bool RinDialogs;
    public bool RFacingRight;
    public bool RAttack;
    public float RHP;
    public bool RGrounded;
    private float x,x1,y1,y;
    public GameObject PointLightForTeleport;
    public EnemyHP enemyHP;
    void Awake(){
        GetComponent<EnemyHP>().interfaceDeath = this as IDeath;
    }
    void Start(){
        information = Information.Instance;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        enemyHP = GetComponent<EnemyHP>();
    }

    void Update(){
        InformationAboutRooster();
        GetPosition();
        if(RHP > 0){
            if(enemyHP.HP <= 0){
                this.enabled = false;
            }
            grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
            if (myTimerAttack > 0){
                myTimerAttack -= Time.deltaTime;
            }else{
                ismove = true;
                anim.SetBool("Attack", false);
            }

            if ((isGame)&&(!RinDialogs) && (ismove)&&(grounded)){
                if ((x1 - x < RadiusSee) && (x1 - x > RadiusAttack) &&(Mathf.Abs(y - y1)< 0.3)){
                    rb.velocity = new Vector2(-SpeedRun, 0);
                    if (!isFacingLeft)
                        Flip();
                    isFacingLeft = true;
                    anim.SetBool("Speed", true);
                }
                if ((x1 - x > -RadiusSee) && (x1 - x < -RadiusAttack) &&(Mathf.Abs(y - y1)< 0.3)){
                    rb.velocity = new Vector2(SpeedRun, 0);
                    if (isFacingLeft)
                        Flip();
                    isFacingLeft = false;
                    anim.SetBool("Speed", true);
                }

                if ((Mathf.Abs(x - x1) > RadiusSee) || (Mathf.Abs(x - x1) < RadiusAttack)){
                    anim.SetBool("Speed", false);
                }

                if ((Mathf.Abs(x - x1) < RadiusAttack)&&(Mathf.Abs(y - y1)< 0.3)&& (myTimerAttack <= 0)){
                    if(UnityEngine.Random.Range(1, 10) < 4){
                        Teleportion();
                    }else{
                        anim.SetBool("Attack", true);
                        myTimerAttack = UnityEngine.Random.Range(1, 3);
                        ismove = false;
                    }
                }
            }else{
                rb.velocity = new Vector2(0,0);
            }
        }
    }
    void GetPosition(){
        x = RPosition.x;
        y = RPosition.y;
        x1 = tr.position.x;
        y1 = tr.position.y;
        if((x1 > x) && !isFacingLeft){
            isFacingLeft = true;
            Flip();
        }
        if((x1 < x) && isFacingLeft){
            isFacingLeft = false;
            Flip();
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

    private void Walk(){
        if (isFacingLeft){
            rb.velocity = new Vector2(-1, 0);
        }else{
            rb.velocity = new Vector2(1, 0);
        }
    }

     void AttackGo(){
        EnemyFight2D.Action(punch1.position, punchRadius, 10, AttackDamage, isFacingLeft,"Sceleton");
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
        RHP= ObserverHP.level;
    }
    void Teleportion(){
        CreatePointLight();
        StartCoroutine("Teleport");
        if(RFacingRight){
            tr.position = new Vector3(RPosition.x-1.5f,tr.position.y, 0);
        }else{
            tr.position = new Vector3(RPosition.x+1.5f,tr.position.y, 0);
        }
        Flip();
        isFacingLeft = !isFacingLeft;
    }
    IEnumerator Teleport(){
        enemyHP.PlaySoundTeleportation();
        yield return new WaitForSeconds (0.8f);
    }
    void CreatePointLight(){
        for(int i=0; i<3; i++){
            var xt = UnityEngine.Random.Range(-1.2f, 1.2f);
            var yt = UnityEngine.Random.Range(-1.2f, 1.2f);
            Instantiate(PointLightForTeleport,new Vector3(tr.position.x + xt, tr.position.y+yt, -1),tr.rotation);
        }
    }
    public void Death(){
        isGame = false;
        this.enabled = false;
    }
}
