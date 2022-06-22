using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelpFuction;

public class DragonBoss : MonoBehaviour, IDeath {
	public Transform tr;
    private Animator anim;
    public bool isGame = true;
    public float TimerAttack;
    public float RestTime;
    public Transform Mouse;
    public Transform Hang;
    public float RadiusMouse = 0.5f;
    public float RadiusHang = 3f;
    public GameObject FireBall, FireBallUp;
    private Vector3 position;

    private float xd,yd,x,y;
    public GameObject Epilog;
	private Information information;
    public Vector2 RPosition;
    public bool RinDialogs;
    public float RHP;
    void Awake(){
        GetComponent<EnemyHP>().interfaceDeath = this as IDeath;
    }
	void Start () {
        GetComponent<EnemyHP>().enabled = true;
		anim = GetComponent<Animator>();
        tr = GetComponent<Transform>();
        information = Information.Instance;
        GetComponent<EnemyHP>().enabled = true;
        if(GameObject.Find("MusicObject")){ GameObject.Find("MusicObject").GetComponent<AudioSource>().enabled = true;}
        BossHPUIScript.Instance.OpenSlider(GetComponent<EnemyHP>());
	}
	
	// Update is called once per frame
	void Update () {
		if (TimerAttack > 0){
            TimerAttack -= Time.deltaTime;
        }else{
			InformationAboutRooster();
			if(isGame && (RHP >0) && !RinDialogs){
				GetPosition();
				if((Mathf.Abs(x-xd)<=0.7)&&(Mathf.Abs(yd-y)<2)){
					anim.Play("Attack2");
					TimerAttack = RestTime*1.5f;
				}
				if((Mathf.Abs(x-xd)<=4)&&(Mathf.Abs(xd-x)>0.7)&&(Mathf.Abs(yd-y)<2)){
					anim.Play("Attack1");
					TimerAttack = RestTime*1.25f;
				}
				if((Mathf.Abs(x-xd)<25)&&(Mathf.Abs(xd-x)>4)){
					anim.Play("Attack3");
					TimerAttack = RestTime;
				}
			}
        } 
	}
	void AttackMouseGo(){
        EnemyFight2D.Action(Mouse.position, RadiusMouse, 10, 5, true,"DragonMouse");
        GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().velocity = new Vector2(-10f, 6f);
    }
    void AttackHangGo(){
        EnemyFight2D.Action(Hang.position, RadiusMouse, 10, 5, true,"DragonMouse");
        GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().velocity = new Vector2(-10f, 6f);
        CameraShake.Shake(2, 2, CameraShake.ShakeMode.XY);
        CreateFireRain();
    }
	void AttackStop(){
        anim.Play("Idle");
    }
    void CreateBolt(){
        var x = RPosition.x;
        var y = RPosition.y;
        position = tr.position;
        var xa = position.x;
        var ya = position.y;
        var xv = x - xa;
        var yv = y - ya;
        position = new Vector3(0, 0.2f, 0);
        position += transform.position;
        var rotation  = Quaternion.Euler(0, 0, Mathf.Atan(yv/xv)*(180/Mathf.PI));
        if (xv < 0){
            rotation = Quaternion.Euler(0, 0, Mathf.Atan(yv/xv)*(180/Mathf.PI));    
        }else{
            rotation = Quaternion.Euler(0, 0, 180+Mathf.Atan(yv/xv)*(180/Mathf.PI));    
        }
        Instantiate(FireBall,position,rotation);
    }
	void GetPosition(){
        x = RPosition.x;
        y = RPosition.y;
        xd = tr.position.x;
        yd = tr.position.y;
    }
	 void InformationAboutRooster(){
        RPosition = information.position;
        RinDialogs = information.isDialog;
        RHP = ObserverHP.level;   
    }
    public  int FireRainStoneCount = 20;
    private GameTimer[] timersAttack = new GameTimer[20];
    public void CreateFireRain(){
        for(int i = 0; i<FireRainStoneCount; i++){
            timersAttack[i] = TimerScript.Timer.StartTimer(0.25f * i, CreateFireStone);
        }
    }

    public void CreateFireStone(){
        Vector2 startPos = new Vector2(RPosition.x + Random.Range(-10f, 10f), RPosition.y + 20f);
        GameObject stone = Instantiate(FireBallUp,startPos, tr.rotation);
    }
    void OnDestroy(){
        for(int i = 0; i<FireRainStoneCount; i++){
            if(timersAttack[i] != null){
                TimerScript.Timer.StopTimer(timersAttack[i]);
            }
        }
    }
    public void Death(){
        GameObject.Find("MusicObject").GetComponent<MusicController>().OffMusic();
        FadeInOut.nextLevel = "Epilog";
        FadeInOut.sceneEnd = true;
        if(GameObject.FindWithTag("Player").GetComponent<RoosterScriptWithShield>()){
            GameObject.FindWithTag("Player").GetComponent<RoosterScriptWithShield> ().isMove = false;
            GameObject.FindWithTag("Player").GetComponent<RoosterScriptWithShield> ().isDialog = true;
        }
        if(GameObject.FindWithTag("Player").GetComponent<RoosterScript>()){
            GameObject.FindWithTag("Player").GetComponent<RoosterScript> ().isMove = false;
            GameObject.FindWithTag("Player").GetComponent<RoosterScript> ().isDialog = true;
        }
        if(GameObject.FindWithTag("Player").GetComponent<RoosterScriptWizard>()){
            GameObject.FindWithTag("Player").GetComponent<RoosterScriptWizard> ().isMove = false;
            GameObject.FindWithTag("Player").GetComponent<RoosterScriptWizard> ().isDialog = true;
        }
        gameObject.GetComponent<DragonBoss>().enabled = false;
        anim.Play("Death");
        SteamAchievementsScript.Instance.UnlockAchievment("ACH_DRAGON_WIN");
    }
}
