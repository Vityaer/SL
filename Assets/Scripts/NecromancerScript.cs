using UnityEngine;
using System.Collections;

public class NecromancerScript : MonoBehaviour, IDeath {

    public Rigidbody2D rb;
    public Transform tr;
    private Vector3 position;  
    private bool isFacingLeft = true;
    public bool isGame = true;
    public bool ismove = true;
    private Animator anim;
    public bool grounded = true;
    public float RestTime;
    public bool CreateSceleton = true;
  
    public float myTimerAttack = 0;
    public float groundRadius = 0.3f;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public GameObject Sceleton;
    public GameObject Magic;
    private Quaternion rotation;

    public Vector2 RPosition;
    public bool RinDialogs;
    public float RHP;
    private EnemyHP enemyHP;
    private Information information;
    // Use this for initialization
    void Awake(){
        enemyHP = GetComponent<EnemyHP>();
        enemyHP.interfaceDeath = this as IDeath;
        information = Information.Instance;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        InformationAboutRooster();
        if ((isGame)&&(RHP > 0)&&(!RinDialogs)&&(!enemyHP.gameover)){
            grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
            if (myTimerAttack > 0){
                myTimerAttack -= Time.deltaTime;
            }else{
                ismove = true;
            }
            if ((ismove)&&(!RinDialogs)&&(grounded)&&(myTimerAttack <= 0)){
                if (RHP > 0){
                    var x = RPosition.x;
                    var y = RPosition.y;
                    var xa = tr.position.x;
                    var ya = tr.position.y;
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
                    if (Mathf.Abs(x - xa) > 30){
                        anim.Play("Idle");
                        anim.SetBool("Speed", false);
                    }
                    if ((xa - x < 4)&&(Mathf.Abs(ya-y)< 2)){
                        if(CreateSceleton){
                            anim.Play("CreateSceleton");
                            myTimerAttack = 10f;
                        }else{
                            anim.Play("Attack");
                            anim.SetBool("Attack", true);
                            myTimerAttack = 5f;
                        }
                    }

                    if ((Mathf.Abs(x - xa) <= 8)&& (Mathf.Abs(ya - y) < 5) && (Mathf.Abs(x - xa) >= 4) && (myTimerAttack <= 0)){
                        var Rand = UnityEngine.Random.Range(0, 100);

                        if (Rand < 80){
                            anim.Play("Attack");
                            anim.SetBool("Attack", true);
                        }
                        myTimerAttack = RestTime;

                        ismove = false;
                    }
                }
            }
        }else{
            anim.SetBool("Attack", false);
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
    void CreateMagic(){
        if (isFacingLeft){
           position = new Vector3(-1, 0, 0);
        }else{
            position = new Vector3(1, 0, 0);
        }
        position += transform.position;

        Instantiate(Magic,position,transform.rotation);
    }
    void MagicCreateSceleton(){
        CreateSceleton = false;
        var positionR = new Vector3(RPosition.x, RPosition.y, 0);
        if(tr.position.x > positionR.x){
            position = positionR+ new Vector3(-2, 0, 0);
        }else{
            position = positionR+ new Vector3(2, 0, 0);
        }
        rotation = Quaternion.Euler(0, 0, 0);
        if(UnityEngine.Random.Range(0, 100) < 50){
            Instantiate(Sceleton,position,rotation);
        }
    }
   void AttackStop(){
        anim.Play("Idle");
        anim.SetBool("Attack", false);
    }
    void InformationAboutRooster(){
        RPosition = information.position;
        RinDialogs = information.isDialog;
        RHP= ObserverHP.level;
    }
    public void Death(){
        this.enabled = false;
    }
}

