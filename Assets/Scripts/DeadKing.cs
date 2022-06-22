using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadKing : MonoBehaviour, IDeath {
	public Rigidbody2D rb;
    public Transform tr;
    public bool isFacingLeft = true;
    public bool isGame = true;
    private Animator anim;
    public bool grounded = true;
    public Transform punch1;
    public Transform punch2;
    public float punchRadius = 0.2f;
    public float myTimerAttack = 0;
    public float groundRadius = 0.05f;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public bool isSpeed;
    public float GradeSelfSave;
    public float SpeedRun = 4;
    public bool ismove;
    public int damage;

    public float RestTime;
    public float RadiusAttack;

    public float RadiusSee = 8;
    float RandTeleport;

    float x,x1,y,y1;
    public GameObject PointLightForTeleport;

    public Vector2 RPosition;
    public bool RinDialogs;
    public bool RFacingRight;
    public bool RAttack;
    public bool RBlock;
    public float RHP;
    public bool RGrounded;
    public AudioClip musicBoss;
    public AudioClip musicLevel;
    private Information InformationScript;

	// Use this for initialization
    void Awake(){
        GetComponent<EnemyHP>().interfaceDeath = this as IDeath;
    }
	void Start () {
		anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        InformationScript = Information.Instance;
        GameObject.Find("WallBossEnd2").GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<EnemyHP>().enabled = true;
        GameObject.Find("MusicLevel").GetComponent<VolumeController>().FinishEarly = 0.5f;
        GameObject.Find("MusicLevel").GetComponent<AudioSource>().clip = musicBoss;
        GameObject.Find("MusicLevel").GetComponent<AudioSource>().Play();
        BossHPUIScript.Instance.OpenSlider(GetComponent<EnemyHP>());
    }
	
	// Update is called once per frame
	void Update () {
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
        if (isGame && ismove && (RHP > 0)&&(!RinDialogs)){
            GetPosition();
            if((Mathf.Abs(x1) <= RadiusAttack)&&(Mathf.Abs(y1) <= 1f)) {
                SelfSave();
                if(myTimerAttack <= 0){
                    AttackRooster();
                }
            }else{
                FindRooster();
            }
        }else{
            rb.velocity = new Vector2(0,0);
        }
		
	}
    void AttackGo(){
        EnemyFight2D.Action(punch1.position, punchRadius, 10, damage, isFacingLeft, "DeadKing");
        GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().velocity = x1 > 0 ? new Vector2(-6f, 3f) : new Vector2(6f, 3f);
    }
    public void AttackRooster(){
        GetPosition();
        if((x <= 51f)&&(x >= 38f)){
            RandTeleport = UnityEngine.Random.Range(0, 100);
        }else{
            RandTeleport = 10f;
        }
        if(RandTeleport < 75){
            if((Mathf.Abs(x1) <= RadiusAttack)&&(Mathf.Abs(y1) <= 1)){
                isSpeed = false;
                anim.SetBool("Speed", false);
                if ((myTimerAttack <= 0)&&(!isSpeed)){
                    var Rand = UnityEngine.Random.Range(0, 100);
                    
                    if ((Rand >= 0) && (Rand < 40)){
                        anim.Play("Attack1");
                        anim.SetBool("Attack", true);
                    }

                    if ((Rand >= 40) && (Rand < 80)){
                        anim.Play("Attack2");
                        anim.SetBool("Attack", true);
                    }
                    if ((Rand < 100) && (Rand >= 80)){
                       Teleportion();
                    }
                    myTimerAttack = RestTime;
                    ismove = false;
                }    
            }
        }else{
                Teleportion();
        }
    }
    void AttackStop(){
        anim.SetBool("Attack", false);
    }
     void FindRooster (){
            if ((Mathf.Abs(x1) > RadiusSee)|| (Mathf.Abs(x1) < RadiusAttack)||(Mathf.Abs(y1)>1)){
                anim.SetBool("Speed", false);
                isSpeed = false;
            }

            if ((x1 < RadiusSee) && (x1 > RadiusAttack)&&(Mathf.Abs(y1) < 1)){
                rb.velocity = new Vector2(-SpeedRun, 0);
                if (!isFacingLeft) Flip();
                isFacingLeft = true;
                anim.SetBool("Speed", true);
                isSpeed = true;
            }
            if ((x1 > (-1)*RadiusSee) && (x1 < -RadiusAttack) && (Mathf.Abs(y1) < 1)){
                rb.velocity = new Vector2(SpeedRun, 0);
                if (isFacingLeft) Flip();
                isFacingLeft = false;
                anim.SetBool("Speed", true);
                isSpeed = true;
            }
    }
    void GroundCheckFinction(){
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("grounded", grounded);
    }
    void SelfSave(){
        if(RAttack){
            var Rand = UnityEngine.Random.Range(0, 100);
            if(Rand < GradeSelfSave){
                Teleportion();
            }
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
        x1 = x1 - x;
        y1 = y1 - y;
    }
	void InformationAboutRooster(){
        RPosition = InformationScript.position;
        RBlock = InformationScript.Block;
        RinDialogs = InformationScript.isDialog;
        RFacingRight = InformationScript.isFacingRight;
        RAttack = InformationScript.isAttack;
        RHP= ObserverHP.level;
    }
    void CreatePointLight(){
        for(int i=0; i<3; i++){
            var xt = UnityEngine.Random.Range(-1.2f, 1.2f);
            var yt = UnityEngine.Random.Range(-1.2f, 1.2f);
            Instantiate(PointLightForTeleport,new Vector3(tr.position.x + xt, tr.position.y+yt, -1),tr.rotation);
        }
    }
    void Teleportion(){
        if((x <= 50f)&&(x >= 40f)){
            Vector3 point;
            if(RFacingRight){
                point = new Vector3(RPosition.x-1.5f,tr.position.y, 0);
            }else{
                point = new Vector3(RPosition.x+1.5f,tr.position.y, 0);
            }
            if(MyPhysics2D.RaycastFindLayer(point, new Vector3(0, -1f, 0), 2f, 8) && InformationScript.Grounded){
                CreatePointLight();
                StartCoroutine("Teleport");
                tr.position = point;
                Flip();
                isFacingLeft = !isFacingLeft;
                myTimerAttack = 0f;
            }
        }
    }
    IEnumerator Teleport(){
        GetComponent<EnemyHP>().PlaySoundTeleportation();
        yield return new WaitForSeconds (0.8f);
    }
    public void PlayMusic(){
        GameObject.Find("MusicLevel").GetComponent<AudioSource>().clip = musicLevel;
        GameObject.Find("MusicLevel").GetComponent<AudioSource>().Play();
    }
    public void Death(){
        SteamAchievementsScript.Instance.UnlockAchievment("ACH_DEATH_KING_WIN");
        Destroy(GameObject.Find("WallBossEnd"));
        Destroy(GameObject.Find("WallBossEnd2"));
        PlayMusic();
        GameObject.Find("LadderFall1").GetComponent<Animator>().enabled = true;
        GameObject.Find("LadderFall2").GetComponent<Animator>().enabled = true;
        this.enabled = false;
    }
}
