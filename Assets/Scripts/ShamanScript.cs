using UnityEngine;
using System.Collections;

public class ShamanScript : MonoBehaviour, IDeath {
 public Rigidbody2D rb;
    public Transform tr;
    private Vector3 position;  
    private bool isFacingLeft = true;
    public bool isGame = true;
    public bool ismove = true;
    public float RestTime;
    private Animator anim;
    public float myTimerAttack = 0;
    public float RadiusAttack = 15;
    public GameObject thePrefab;
    private Quaternion rotation;
    public GameObject theWolf;
    public bool CreateWolfStart = true;

    public bool grounded = true;
    public float groundRadius = 0.3f;
    public LayerMask whatIsGround;
    public Transform groundCheck;

    public Information information;
    public Vector2 RPosition;
    public bool RinDialogs;
    public float RHP;
    private EnemyHP enemyHP;
    // Use this for initialization
    void Awake(){
        GetComponent<EnemyHP>().interfaceDeath = this as IDeath;
    }
    void Start () {
        information = Information.Instance;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        enemyHP = GetComponent<EnemyHP>();
    }
	
	// Update is called once per frame
	void Update () {
        InformationAboutRooster();
        if ((isGame)&&(!enemyHP.gameover)&&(!RinDialogs)&&(RHP > 0)){
            grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
            if (!grounded){ anim.SetBool("Grounded", false);} else { anim.SetBool("Grounded", true);}
            if (myTimerAttack > 0){
                myTimerAttack -= Time.deltaTime;
            }else{
                ismove = true;
            }
            if (ismove){
                if (RHP > 0){
                    
                    var x = RPosition.x;
                    var y = RPosition.y;
                    var xa = tr.position.x;
                    var ya = tr.position.y;
                    if (xa - x > 0)
                    {
                        if (!isFacingLeft)
                            Flip();
                        isFacingLeft = true;

                    }
                    if (xa - x < 0)
                    {

                        if (isFacingLeft)
                            Flip();
                        isFacingLeft = false;
                    }
                    if (Mathf.Abs(x - xa) > 30)
                    {
                        anim.Play("Idle");
                        anim.SetBool("Speed", false);
                    }

                    if ((Mathf.Abs(x - xa) <= RadiusAttack)&& (Mathf.Abs(ya - y) < 20) && (myTimerAttack <= 0)){
                        var Rand = UnityEngine.Random.Range(0, 100);
                        if(!CreateWolfStart){
                            if (Rand < 80){
                                anim.Play("Attack");
                                anim.SetBool("Attack", true);
                            }else{
                                anim.Play("Idle");
                            }
                        }else{
                            anim.Play("CreateWolf");
                            CreateWolfStart = false;
                        }
                        if(Mathf.Abs(x-xa) < 7.5){
                            myTimerAttack = 3f;
                        }else{
                            myTimerAttack = RestTime;
                        }

                        ismove = false;
                    }
                }
            }
        }
        else
        {
            anim.SetBool("Attack", false);
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
    void CreateBolt()
    {
        if (isFacingLeft)
        {
           position = new Vector3(-1, 0, 0);
        }
        else
        {
            position = new Vector3(1, 0, 0);
        }
        position += transform.position;
        var x = RPosition.x;
        var y = RPosition.y;

        tr = GetComponent<Transform>();
        position = tr.position;
        var xa = position.x;
        var ya = position.y;
        var xv = x - xa;
        var yv = y - ya;
        rotation = Quaternion.Euler(0, 0, 90-Mathf.Atan(xv / yv)*180/Mathf.PI);
        Instantiate(thePrefab,position,rotation);
    }
   void AttackStop(){
        anim.Play("Idle");
        anim.SetBool("Attack", false);
    }

    void CreateWolf(){
        var positionR = new Vector3(RPosition.x, RPosition.y, 0);
        position = positionR+ new Vector3(5, 0, 0);
        rotation = Quaternion.Euler(0, 0, 0);
        Instantiate(theWolf,position,rotation);
        position = positionR-new Vector3(5, 0, 0);
        Instantiate(theWolf,position,rotation);

    }
    void InformationAboutRooster(){
        RPosition = information.position;
        RinDialogs = information.isDialog;
        RHP= ObserverHP.level;
    }
    public void Death(){
        if(gameObject.name == "Shaman_for_speak")
            enemyHP.Speak();
        this.enabled = false;
    }
}
