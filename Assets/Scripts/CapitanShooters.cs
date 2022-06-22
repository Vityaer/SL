using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapitanShooters : MonoBehaviour, IDeath {
    public Rigidbody2D rb;
    public Transform tr;
    public Transform punchMellee;
    public bool isFacingLeft = true;
    public bool isGame = true;
    private Animator anim;
    public bool grounded = true;
    public float punchRadius = 0.5f;
    public float RadiusShot;
    public float RadiusAttack;
    public float myTimerAttack = 0;
    public float groundRadius = 0.05f;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public GameObject Bullet;
    public float RestTime;
    float x,x1,y,y1;

    private Information information;
    public Vector2 RPosition;
    public bool RinDialogs;
    public float RHP;
    // Use this for initialization
    void Awake(){
        GetComponent<EnemyHP>().interfaceDeath = this as IDeath;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
    }
    void Start () {
        information = Information.Instance;
    }
    
    // Update is called once per frame
    void Update () {
        InformationAboutRooster();
        GroundCheckFunction();
        if (myTimerAttack > 0){
            myTimerAttack -= Time.deltaTime;
        }
        FindRooster();
        if (isGame && (RHP > 0)&&(!RinDialogs)&&grounded&&(myTimerAttack<= 0)){
            GetPosition();
            if((Mathf.Abs(x1-x) <= RadiusAttack)&&(Mathf.Abs(y1-y) <= 1)&&(myTimerAttack <= 0)) {
                if(myTimerAttack <= 0){
                    anim.Play("Attack2");
                    anim.SetBool("Attack", true);
                }
            }
            if((Mathf.Abs(y1-y) <= RadiusShot)&&(Mathf.Abs(x1-x) <= RadiusShot)&&(Mathf.Abs(x1-x) > RadiusAttack)&&(myTimerAttack <= 0)) {
                anim.Play("Attack1");
                anim.SetBool("Attack", true);
            }
        }
    }
    void AttackGo(){
        EnemyFight2D.Action(punchMellee.position, punchRadius, 10, 3, isFacingLeft,"CapitanShooters");
    }
    void GroundCheckFunction(){
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("grounded", grounded);
    }
    void GetPosition(){
        x = RPosition.x;
        y = RPosition.y;
        x1 = tr.position.x;
        y1 = tr.position.y;
    }
    void InformationAboutRooster(){
        RPosition = information.position;
        RinDialogs = information.isDialog;
        RHP= ObserverHP.level;
    }
    void AttackStop(){
        anim.SetBool("Attack", false);
        myTimerAttack = RestTime;
    }
    void FindRooster (){
            if ((RPosition.x - tr.position.x < 0)&&(!isFacingLeft)){
                isFacingLeft = true;
                Flip();
            }
            if ((RPosition.x - tr.position.x >= 0)&&(isFacingLeft)){
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
    void CreateBullet(){
        var position = tr.position;
        if(isFacingLeft){
            position += new Vector3(-1, 0,0);
        }else{
            position += new Vector3(1, 0,0);
        }
        Instantiate(Bullet,position,tr.rotation);
    }
    public void Death(){
        this.enabled = false;
    }
}
