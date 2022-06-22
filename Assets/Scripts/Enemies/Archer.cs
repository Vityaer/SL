using UnityEngine;
using System.Collections;

public class Archer : MonoBehaviour, IDeath {
    public Rigidbody2D rb;
    public Transform tr;
    private Vector3 position;  
    private bool isFacingLeft = true;
    public bool isGame = true;
    public bool ismove = true;
    private Animator anim;
    public bool grounded = true;
    public float myTimerAttack = 0;
    public float Timer;
    public float groundRadius = 0.1f;
    public float RadiusAttack = 15;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public GameObject thePrefab;
    private Quaternion rotation;
    private float xa,ya,x,y;
    public Vector2 RPosition;
    public bool RinDialogs;
    public float RHP;
    private EnemyHP enemyHP;
    void Start () {
        enemyHP = GetComponent<EnemyHP>();    
        enemyHP.interfaceDeath = this as IDeath;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        RadiusAttack = thePrefab.GetComponent<ArrowFly>().GetRange;
    }
	void Update () {
        InformationAboutRooster();
        GetPosition();
        if ((isGame)&&(!enemyHP.gameover)&&(RHP > 0)&&(!RinDialogs)){
            if (myTimerAttack > 0){
                myTimerAttack -= Time.deltaTime;
            }else{
                ismove = true;
            }
            if((Mathf.Abs(x - xa) <= RadiusAttack)&& (Mathf.Abs(ya - y) < RadiusAttack)){
                grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
                if (!grounded){
                    anim.SetBool("Grounded", false);
                }else{
                    anim.SetBool("Grounded", true);
                }
                if (ismove) {
                    if (xa - x > 0){
                        if (!isFacingLeft)
                            Flip();
                        isFacingLeft = true;
                    }
                    if (xa - x < 0){
                        if (isFacingLeft)
                            Flip();
                        isFacingLeft = false;
                    }
                    if (Mathf.Abs(x - xa) > RadiusAttack){
                        anim.Play("Idle");
                        anim.SetBool("Speed", false);
                    }
                    if ((Mathf.Abs(x - xa) <= RadiusAttack)&& (Mathf.Abs(ya - y) < RadiusAttack) && (myTimerAttack <= 0)){
                        var Rand = UnityEngine.Random.Range(0, 100);
                        if ((Rand >= 0) && (Rand < 95)){
                            anim.Play("Attack");
                            anim.SetBool("Attack", true);
                        }
                        if ((Rand < 100) && (Rand >= 95)){
                            anim.Play("Idle");
                        }
                       
                        myTimerAttack = Timer;

                        ismove = false;
                    }
                    
                }
            }
            else
            {
                anim.SetBool("Attack", false);
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
    void CreateBolt(){
        if (isFacingLeft){
           position = new Vector3(-1, 0, 0);
        }else{
            position = new Vector3(1, 0, 0);
        }
        tr = GetComponent<Transform>();

        position += tr.position;
        Instantiate(thePrefab,position,tr.rotation);
    }
   void AttackStop(){
        anim.Play("Idle");
        anim.SetBool("Attack", false);
    }
     void GetPosition(){
        x = RPosition.x;
        y = RPosition.y;
        xa = tr.position.x;
        ya = tr.position.y;
    }
    void InformationAboutRooster(){
        RPosition = Information.Instance.position;
        RinDialogs = Information.Instance.isDialog;
        RHP = ObserverHP.level;
    }
    public void Death(){
        gameObject.layer = 12;
        if(gameObject.name == "Archer_for_speak")
            enemyHP.Speak();
        this.isGame = false;
    }
}
