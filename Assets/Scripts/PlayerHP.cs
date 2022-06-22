using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PlayerHP : MonoBehaviour
{
    public Rigidbody2D rb;
    public int HP;
    public int armor;
    public bool isblock = false; 
    private bool gameover = false;
    public int score;
    public float Evasion;
    public AudioClip LowHP;
    public AudioClip ANoise;
    public AudioClip ADeath;
    public AudioClip AStrikeInBlock;
	public bool grounded;

    public Text Helth;
    public Text Gold;

    private AudioSource audioSource;
    private AudioSource audioSourceHeart;
    public GameObject theBlood;
    
    private Coroutine LowHPCoroutine;
    private StaminaControllerScript staminaController;

    void Awake(){
        audioSourceHeart = gameObject.transform.Find("audioHeart").GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        score = (Information.Instance != null) ? Information.Instance.Gold : 0;
        staminaController = GetComponent<StaminaControllerScript>();
    }
    void Start(){
        HP = ObserverHP.level;
        Helth = GameObject.Find("xHelth").GetComponent<Text>();
        Gold = GameObject.Find("xGold").GetComponent<Text>();
        DrawChange();
    }
    public void AddHP(int bonus){
        HP += bonus;
        ObserverHP.ChangeHP(HP);
        DrawChange();
        if(HP > 50){ SteamAchievementsScript.Instance?.UnlockAchievment("ACH_VERY_MANY_HP"); }
    }
    public void AddMonet(int bonus){
        score += bonus;
        if(score >= 10){
            score -= 10;
            HP += 1;
            ObserverHP.ChangeHP(HP);
        } 
        DrawChange();
    }
    public void GetClearDamage(int damage){
        HP = (damage < HP) ? HP - damage : 0;
        CreateBlood();
        DrawChange();
        ObserverHP.ChangeHP(HP);
        if (HP == 0) StartDeath();
    }
    public void GetDamage(int damage, bool isFacingRight){
        if((isblock)&&(GetComponent<RoosterScriptWizard>())){
            damage = 0;
            staminaController.DecreaseBlockStamina();
        }
        if(Evasion > 0){
            if(UnityEngine.Random.Range(0, 100) < Evasion){
                damage = 0;
            }
        }
        if(GetComponent<RoosterScriptWithShield>()){
            if ((isblock) && (isFacingRight == GetComponent<RoosterScriptWithShield>().isFacingRight)){
                damage = (damage > 3) ? damage - 3 : 0;
                staminaController.DecreaseBlockStamina();
                audioSource.PlayOneShot(AStrikeInBlock);
            }    
        }
        damage = (damage > armor) ? damage - armor : 0;
        if(damage > 0){
            staminaController.DecreaseStamina(damage/2);
            if(Information.Instance.CauseOfDeath.Equals("Rogue") && score > 0) score--; 
            HP = (damage < HP) ? HP - damage : 0;
            Jump(damage, isFacingRight);
            DrawChange();
        }
        ObserverHP.ChangeHP(HP);
        if (HP == 0) StartDeath();
    }
    void Jump(float damage, bool leftDamage){
        if(damage > 0 && (HP > 0)){
            if(GetComponent<RoosterScriptWizard>() != null)
                grounded  = GetComponent<RoosterScriptWizard>().grounded;
            if(GetComponent<RoosterScript>() != null)
                grounded  = GetComponent<RoosterScript>().grounded;
            if(GetComponent<RoosterScriptWithShield>() != null)
                grounded  = GetComponent<RoosterScriptWithShield>().grounded;
            if (grounded){
                if (leftDamage){
                    rb.AddForce(new Vector2(3*damage/2, 3*damage/2));
                }else{
                    rb.AddForce(new Vector2(-3*damage/2, 3*damage/2));
                }
            }
            CreateBlood();
        }
    }
    void CreateBlood(){
        if(!gameover){
            audioSource.PlayOneShot(ANoise);
            Instantiate(theBlood, GetComponent<Transform>().position, GetComponent<Transform>().rotation);  
        }
    }
    void StartDeath(){
        if(!gameover){
            gameover = true;
            audioSourceHeart.PlayOneShot(ADeath);
            if(GetComponent<RoosterScript>()){
                gameObject.GetComponent<RoosterScript>().enabled = false;
            }
            if(GetComponent<RoosterScriptWithShield>()){
                gameObject.GetComponent<RoosterScriptWithShield>().enabled = false;
            }
            if(GetComponent<RoosterScriptWizard>()){
                gameObject.GetComponent<RoosterScriptWizard>().enabled = false;
            }
            gameObject.GetComponent<Animator>().Play("Death");
            Death();
        }
    }
    public void DrawChange(){
        Gold.text  = String.Concat("x", score.ToString());
        Helth.text = String.Concat("x", HP.ToString());
        if(!gameover) ChangeInformation();
        if(HP <= 3){
            if(LowHPCoroutine == null) 
                LowHPCoroutine = StartCoroutine(ITimerLowHP());
        }else{
            if(LowHPCoroutine != null){
                StopCoroutine(LowHPCoroutine);
                LowHPCoroutine = null;
            }
        }
    }
    void Pause(){
        gameObject.GetComponent<Animator>().speed = 0;
    }
    Coroutine deathAnimCoroutine;
    void Death(){
        SaveLoadProgressAchievement.Instance?.AddPointInProgressAchievement("DeathRooster");
        if(deathAnimCoroutine == null){
            deathAnimCoroutine = StartCoroutine( ITimerAnimDeath() );
        }
    }
    public void StopAnimDeath(){
        if(deathAnimCoroutine != null){
            StopCoroutine(deathAnimCoroutine);
        }
    }
    IEnumerator ITimerAnimDeath(){
        if (Information.Instance != null) Information.Instance.Reestablish();
        float timerDeath = 5f;
        while(timerDeath >= 0f){
            timerDeath -= Time.deltaTime;
            yield return null;
        }
        FadeInOut.sceneStarting = false;
        FadeInOut.nextLevel = "GameOver"; 
        FadeInOut.sceneEnd = true;
    }
    void SoundLowHP(){
        audioSourceHeart.PlayOneShot(LowHP);
    }
    void ChangeInformation(){
        if(Information.Instance != null) Information.Instance.Gold = score;
    }
    IEnumerator ITimerLowHP(){
        float timerSound = HP;
        while( (HP <= 3) && (HP > 0) ){
            timerSound = (timerSound <= HP) ? timerSound : (float) HP;
            timerSound -= Time.deltaTime;
            if(timerSound <= 0){
                SoundLowHP();
                timerSound = (float) HP;
            }
            yield return null;
        }
    }

    [ContextMenu("add 1000 hp")]
    void CheatAdd1000HP(){
        AddHP(1000);
    }
}


