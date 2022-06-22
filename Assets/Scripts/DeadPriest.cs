using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPriest : MonoBehaviour, IDeath {
	Vector3 position;
	GameObject[] PlaceForTeleport;
	List<ZombiPriestScript> Zombi = new List<ZombiPriestScript>();
	public GameObject WhitePoint;
	public GameObject RedPoint;
	public Rigidbody2D rb;
    public Transform tr;
    public bool isFacingLeft = true;
    public bool isGame = true;
    private Animator anim;
    public bool grounded = true;
    public Transform punch1;
    public Transform punch2;
    public float HP;
    public float punchRadius = 0.2f;
    public float RadiusShot;
    public float myTimerAttack = 0;
    public float myTimeRresurrection = 0;
    public float RestRresurrection;
    public float myTimerTeleport = 10f;
    public float groundRadius = 0.05f;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public bool isSpeed;
    public float GradeSelfSave;
    public float SpeedRun = 4;
    public bool ismove;
    bool StartTeleport = true;
    public GameObject MagicShot;

    public float RestTime;
    public float RadiusAttack;

    public float RadiusSee = 8;

    float x,x1,y,y1;
    public GameObject PointLightForTeleport;

    private Information information;
    public Vector2 RPosition;
    public bool RinDialogs;
    public bool RFacingRight;
    public bool RAttack;
    public bool RBlock;
    public float RHP;
    public bool RGrounded;
    public AudioClip musicBoss;
    public AudioClip musicLevel;
	// Use this for initialization
    void Awake(){
        GetComponent<EnemyHP>().interfaceDeath = this as IDeath;
    }
	void Start () {
		WhitePoint.GetComponent<Light>().enabled = false;
		RedPoint.GetComponent<Light>().enabled = true;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        information = Information.Instance;
        PlaceForTeleport = GameObject.FindGameObjectsWithTag("PlaceForTeleport");
        GameObject[] zombi = GameObject.FindGameObjectsWithTag("Zombi");
        foreach(GameObject enemy in zombi){ Zombi.Add(enemy.GetComponent<ZombiPriestScript>()); }
        GameObject.Find("MusicLevel").GetComponent<VolumeController>().PlayMusic(musicBoss);
        
        EnemyHP enemyHP = GetComponent<EnemyHP>();
        enemyHP.enabled = true;
        BossHPUIScript.Instance.OpenSlider(enemyHP);
        enemyHP.RegisterOnChangeHP(GetDamage);
        Teleportion();
	}
	
	// Update is called once per frame
	void Update () {
		InformationAboutRooster();
        GroundCheckFunction();
        if (myTimeRresurrection > 0){
            myTimeRresurrection -= Time.deltaTime;
        }
        if (myTimerTeleport > 0){
            myTimerTeleport -= Time.deltaTime;
        }
        if (myTimerAttack > 0){
            myTimerAttack -= Time.deltaTime;
            ismove = false;
        }else{
            if (grounded){
                ismove = true;
            }
        }
        FindRooster();
        if (isGame && ismove && (RHP > 0)&&(!RinDialogs)){
            GetPosition();
            if((Mathf.Abs(x1) <= RadiusAttack)&&(Mathf.Abs(y1) <= 1)) {
                SelfSave();
                if(myTimerAttack <= 0){
                    AttackRooster();
                }
            }
            if((Mathf.Abs(x1) <= RadiusShot)&&(Mathf.Abs(x1) > RadiusAttack)) {
                ShotInRooster();
            }
        }else{
            rb.velocity = new Vector2(0,0);
        }
        if (isGame && (myTimerTeleport<=0) && (RHP > 0)&&(!RinDialogs)){
            GetPosition();
            if((Mathf.Abs(x1) <= RadiusShot)) {
            	Teleportion();
            }
        }
        if((myTimerTeleport > 0)&&(myTimerAttack > 0)&&(myTimeRresurrection <= 0)){
        	RresurrectionZombiAnim();
            RresurrectionZombiMagic();
        }
	}
	void SelfSave(){
        if(RAttack){
        	myTimerTeleport = 0f;
            var Rand = UnityEngine.Random.Range(0, 100);
            if(Rand < GradeSelfSave){
                Teleportion();
            }
        }    
    }
    void RresurrectionZombiAnim(){
    	anim.Play("Attack3");
        RresurrectionZombiMagic();
    	myTimeRresurrection = RestRresurrection;
    }
    void RresurrectionZombiMagic(){
        for(int i = 0; i < Zombi.Count; i++) Zombi[i].Rresurrection();
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
        x1 = x1 - x;
        y1 = y1 - y;  
    }
	void InformationAboutRooster(){
        RPosition = information.position;
        RBlock = information.Block;
        RinDialogs = information.isDialog;
        RFacingRight = information.isFacingRight;
        RAttack = information.isAttack;
        RHP= ObserverHP.level;
    }
    void AttackStop(){
        anim.SetBool("Attack", false);
        anim.Play("Idle");
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
    void AttackGo(){
        EnemyFight2D.Action(punch1.position, punchRadius, 10, 2, isFacingLeft,"DeadPriest");
    }
    public void AttackRooster(){
        GetPosition();
        var RandTeleport = UnityEngine.Random.Range(0, 100);
        if(RandTeleport < 50){
            if((Mathf.Abs(x1) <= RadiusAttack)&&(Mathf.Abs(y1) <= 1)){
                isSpeed = false;
                anim.SetBool("Speed", false);
                if (myTimerAttack <= 0)
                    anim.SetBool("Attack", true);
                Teleportion();
                myTimerAttack = RestTime;
            }    
        }else{
            Teleportion();
        }
        ismove = false;
    }
    void ShotInRooster(){
    	anim.Play("Attack1");
    	myTimerAttack = RestTime;
    }
    void KillZombi(){
        for(int i = 0; i < Zombi.Count; i++) Zombi[i].Kill();
    	StartCoroutine("IDeath");
		RedPoint.GetComponent<Light>().enabled = false;
    }
    void CreateBolt(){
        if (isFacingLeft){
           position = new Vector3(-1, 0, 0);
        }else{
            position = new Vector3(1, 0, 0);
        }
        position += tr.position;
        Instantiate(MagicShot,position,tr.rotation);
    }
    void CreatePointLight(){
        for(int i=0; i<3; i++){
            var xt = UnityEngine.Random.Range(-1f, 1f);
            var yt = UnityEngine.Random.Range(-1f, 1f);
            Instantiate(PointLightForTeleport,new Vector3(tr.position.x + xt, tr.position.y+yt, -1),tr.rotation);
        }
    }
    void Teleportion(){
    	if(myTimerTeleport <= 0){
	        CreatePointLight();
	        var RandPlaceTeleport = UnityEngine.Random.Range(0, PlaceForTeleport.Length);
	        StartCoroutine("Teleport");
            if(!StartTeleport){
    	        tr.position = new Vector3(PlaceForTeleport[RandPlaceTeleport].GetComponent<Transform>().position.x,PlaceForTeleport[RandPlaceTeleport].GetComponent<Transform>().position.y, 0);
            }else{
                tr.position = new Vector3(GameObject.Find("PlaceForTeleport (4)").GetComponent<Transform>().position.x,GameObject.Find("PlaceForTeleport (4)").GetComponent<Transform>().position.y, 0);
                StartTeleport = false;
            }
	        myTimerTeleport = 10f;
    	}
    }
    IEnumerator Teleport(){
        GetComponent<EnemyHP>().PlaySoundTeleportation();
        yield return new WaitForSeconds (0.6f);
    }
    IEnumerator IDeath(){
        yield return new WaitForSeconds (2f);
    }
    public void PlayMusic(){
        GameObject.Find("MusicLevel").GetComponent<VolumeController>().PlayMusic(musicLevel);
    }
    public GameObject lift;
    public void PlayLift(){
        lift.SetActive(true);
    }
    public void GetDamage(float HP){
        Teleportion();
        myTimerTeleport = 0f;
    }
    public void Death(){
        SteamAchievementsScript.Instance.UnlockAchievment("ACH_PRIEST_WIN");
        PlayLift();
        PlayMusic();
        this.enabled = false;
    }
}
