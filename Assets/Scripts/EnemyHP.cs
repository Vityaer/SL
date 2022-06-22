using UnityEngine;
using System.Collections;


public class EnemyHP : MonoBehaviour{
    private Rigidbody2D rb;
    public float HP;
    private bool leftDamage = false;
    private Animator anim;
    public bool gameover = false;
    public bool grounded;
    public LayerMask whatIsGround;
    public bool isBlock; 
    public int BonusMP;
    public float RadiusForFindGround = 0.5f;
    public AudioClip SoundStrikeDistance;
    public AudioClip SoundStrikeMelle1;
    public AudioClip SoundStrikeMelle2;
    public AudioClip SoundLandscape;
    public AudioClip SoundWalk;   
    public AudioClip SoundBlock;
    public AudioClip SoundTeleportation;  
    public AudioClip SoundDamage;
    public AudioClip SoundDeath;
    private AudioSource audioSource;
    public GameObject theMonet;
    public GameObject theBlood;
    public GameObject theDialog;
    bool isPlayDeath = false;
    bool DeathEnd = false;
    public IDeath interfaceDeath;
    void Awake(){
        GetComponents();
    }
    void GetComponents(){
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }
    public bool killFromSave = false;
    void Start(){
        KilledEnemy infoKilled = Information.Instance.CheckEnemy(gameObject.name);
        if(infoKilled == null){
            ObserverSoundLevel.Register( ChangeLevelSound );
        }else{
            transform.position = infoKilled.pos;
            killFromSave = true;
            StartDeath();
        }
    }

    Coroutine deathCoroitine;
    void OffEnemy(){
        if(deathCoroitine == null){
            deathCoroitine = StartCoroutine(IDeathGroundCheck());
        }
    }
    public void GetDamage(float damage){
        if(this.enabled){
            if(HP > 0f){
                HP = (HP > damage) ? HP - damage : 0f;
                Jump();
                PlayDamage();
            }
            ObserverChangeHP(HP);
            if(HP <= 0) StartDeath(); 
        }
    }
    public void GetDamage(float damage, bool leftDamage){  
        if(this.enabled){
            if (HP > 0f){
                if(!isBlock || (GetComponent<CapitanScript>().isFacingLeft != leftDamage)){
                        HP = (HP > damage) ? HP - damage : 0f;
                        Jump();
                        PlayDamage();
                }else{
                    PlayBlock();
                }
            }
            ObserverChangeHP(HP);
            if(HP <= 0) StartDeath(); 
        }
    }        
    void StartDeath(){
        if (!gameover){
            if(killFromSave == false){
                SaveLoadProgressAchievement.Instance?.AddPointInProgressAchievement("DeathEnemy");
                Information.Instance.game.AddEnemy(gameObject.name, transform.position);
            }
            gameover = true;
            GetComponent<SpriteRenderer>().sortingOrder = 1;
            if(interfaceDeath != null){
                interfaceDeath.Death();
            }else{
                Debug.Log(string.Concat("Problem ", gameObject.name));
            }
            
            if(gameObject.name =="PlatformForDestroy"){
                GetComponent<SpriteRenderer>().enabled = false;
            }
            OffEnemy();
            AddMP();
        }
    }
    public void ChangeLevelSound(float num){
        audioSource.volume = num;
    }
    void OnDestroy(){
        ObserverSoundLevel.UnRegister( ChangeLevelSound );
    }
    public void Speak(){
        if(killFromSave == false){
            if(GetComponent<WolfForDeath>())
                GetComponent<WolfForDeath>().enabled = true;
            anim.Play("Death");
            if(gameObject.name != "HangMan"){
                Instantiate(theDialog,GameObject.FindWithTag("Player").GetComponent<Transform>().position,Quaternion.Euler(0,0,0));
            }else{
                Instantiate(theDialog,GameObject.Find("PlaceForDialogEndBoss").GetComponent<Transform>().position,Quaternion.Euler(0,0,0));
            }
        }
    }
    RaycastHit2D hit;
    void CheckGrounded(){
        hit = Physics2D.Raycast(transform.position, Vector3.down, RadiusForFindGround, whatIsGround);
        grounded = (hit.collider != null);
    }
    void Jump(){
        if ((HP > 0f) && (rb != null)){
            if (leftDamage && (HP > 1)){
                if(!GetComponent<GuyScript>() && !GetComponent<DragonBoss>()){
                    rb.velocity = new Vector2(-3, 5);
                }else{
                    if(HP <= 3)
                        rb.velocity = new Vector2(-3, 5);
                }
            }else{
                if(!GetComponent<GuyScript>() && !GetComponent<DragonBoss>()){
                    rb.velocity = new Vector2(3, 5);
                    if(HP <= 3)
                        rb.velocity = new Vector2(3, 5);
                }
            }
        }
    }
    void Pause(){
        DeathEnd = true;
        anim.speed = 0;
        if(killFromSave == false)
            CreateMonet();
    }
    static int numMonet = 0;
    void CreateMonet(){
        GameObject gameObjectBonus = null;
        var Rand = UnityEngine.Random.Range(0, 100);
        if  ((Rand < 30)&&(GetComponent<RogueScript>() != null))
            gameObjectBonus = Instantiate(theMonet,gameObject.transform.position,gameObject.transform.rotation);
        if  ((Rand < 100) && (GetComponent<GuyScript>() != null))
            gameObjectBonus = Instantiate(theMonet,gameObject.transform.position,gameObject.transform.rotation);
        if  ((Rand < 100) && (GetComponent<BerserkScript>() != null))
            gameObjectBonus = Instantiate(theMonet,gameObject.transform.position,gameObject.transform.rotation);
        if  ((Rand < 50) && (GetComponent<Archer>() != null))
            gameObjectBonus = Instantiate(theMonet,gameObject.transform.position,gameObject.transform.rotation);
        if  ((Rand < 80) && (GetComponent<NecromancerScript>() != null))
            gameObjectBonus = Instantiate(theMonet,gameObject.transform.position,gameObject.transform.rotation);
        if  ((Rand < 20) && (GetComponent<WolfScript>() != null))
            gameObjectBonus = Instantiate(theMonet,gameObject.transform.position,gameObject.transform.rotation);
        if(gameObjectBonus != null){
            gameObjectBonus.name = string.Concat("bonusCreated", (numMonet++).ToString()); 
            TypeItem typeItem = (gameObjectBonus.GetComponent<MonetScript>() != null) ? TypeItem.Monet : TypeItem.Heart;
            Information.Instance.game.AddCteatedItem(gameObjectBonus.name, typeItem, transform.position);
        }
    }
    void PlayStrikeDistance(){
        audioSource.PlayOneShot(SoundStrikeDistance);
    }
    void PlayStrikeMelle1(){
        if(SoundStrikeMelle1 != null)
        audioSource.PlayOneShot(SoundStrikeMelle1);
    }
    void PlayWalk(){
        if(SoundWalk != null)
            audioSource.PlayOneShot(SoundWalk);
    }
    void PlayLandscape(){
        audioSource.PlayOneShot(SoundLandscape);
    }
    void PlayStrikeMelle2(){
        audioSource.PlayOneShot(SoundStrikeMelle2);
    }
    void PlayDamage(){
        if(HP > 0){
            if(theBlood != null)
                Instantiate(theBlood, new Vector3(GetComponent<Transform>().position.x,GetComponent<Transform>().position.y,-1), GetComponent<Transform>().rotation);
            audioSource.PlayOneShot(SoundDamage);
        } 
    }
    void PlayBlock(){
        audioSource.PlayOneShot(SoundBlock);
    }
    public void PlaySoundTeleportation(){
        if(SoundTeleportation != null)
            audioSource.PlayOneShot(SoundTeleportation);
    }
    void PlayDeath(){
        if(!isPlayDeath){
            isPlayDeath = true;
            if(killFromSave == false)
                audioSource.PlayOneShot(SoundDeath);
        }
    }
    void AddMP(){
        if(killFromSave == false){
            Information.Instance.MP += BonusMP;
            RoosterScriptWizard player = GameObject.FindWithTag("Player")?.GetComponent<RoosterScriptWizard>();
            if(player != null)
                    player.MP += BonusMP;
        }
    }
    IEnumerator IDeathGroundCheck(){
        while(!grounded){
            CheckGrounded();
            yield return null;
        }
        if (GetComponent<CircleCollider2D>())
            GetComponent<CircleCollider2D>().enabled = false;
        if (GetComponent<BoxCollider2D>())
            if(!GetComponent<Balist>())
            GetComponent<BoxCollider2D>().enabled = false;
        if (GetComponent<PolygonCollider2D>())
            GetComponent<PolygonCollider2D>().enabled = false;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(0,0);
        PlayDeath();
        anim.Play("Death");
        gameObject.layer = 0;
    }

    public delegate void Del(float num);
    public Del dels;
    public void RegisterOnChangeHP(Del d){ dels += d; }
    public void UnRegisterOnChangeHP(Del d){ dels -= d; }
    private void ObserverChangeHP(float HP){
        if(dels != null) {dels(HP);}
    }
}
