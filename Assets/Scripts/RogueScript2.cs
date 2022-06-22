using UnityEngine;
using System.Collections;


public class RogueScript2 : MonoBehaviour {
    public Transform tr;
    public Rigidbody2D rb;
    public float myTimerAttack = 0;
    public float RadiusVisible = 5;
    private bool flag = false;
    public float time = 1.0f;
    public bool Jump = false;
    public bool leftSee; 
    public float groundRadius = 0.05f;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public bool grounded = true;

    public Vector2 RPosition;

    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider2D;
    private BoxCollider2D boxCollider2D;
    private EnemyHP enemyHP;
    private RogueScript rogueScript;
    private RogueScript2 rogueScript2;
    private Light light;
    private Information information;
    void Awake(){
        GetComponent<EnemyHP>().interfaceDeath = this as IDeath;
    }
    void Start () {
      information = Information.Instance;
      if(gameObject.transform.GetChild(0).gameObject.GetComponent<Light>())
        light          = gameObject.transform.GetChild(0).gameObject.GetComponent<Light>();
      rogueScript      = GetComponent<RogueScript>();
      enemyHP          = GetComponent<EnemyHP>();
      circleCollider2D = GetComponent<CircleCollider2D>();
      boxCollider2D    = GetComponent<BoxCollider2D>();
      rogueScript2     = GetComponent<RogueScript2>();
      spriteRenderer   = GetComponent<SpriteRenderer>();
      tr = GetComponent<Transform>();
      rb = GetComponent<Rigidbody2D>();
      anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        InformationAboutRooster();
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        if (myTimerAttack > 0){myTimerAttack -= Time.deltaTime;}
        var x = RPosition.x;
        var y = RPosition.y;
        var x1 = tr.position.x;
        var y1 = tr.position.y;
        if(leftSee){
          if ((x1 - x < RadiusVisible) && (Mathf.Abs(y - y1) < 2) && (!flag) ){
              myTimerAttack = time;
              flag = true;
          }
          }else{
            if ((x1 < x)&&(x1 - x > (-1)*RadiusVisible) && (Mathf.Abs(y - y1) < 2) && (!flag) ){
              myTimerAttack = time;
              flag = true;
            }
          }
        
        if ((flag==true)&&(myTimerAttack<=0)&&(!Jump)){
          Jump = true;
          anim.enabled = true;
          spriteRenderer.enabled = true; 
          rb.isKinematic = false;
          rb.velocity = new Vector2(0,1);
          anim.Play("Jump");
          OnScriptRogue();
        }       
               
  }
  void OnScriptRogue() {
    circleCollider2D.enabled = true;
    boxCollider2D.enabled = true;  
    rogueScript.enabled = true;
    enemyHP.enabled = true;
    rb.gravityScale = 1;
    if(light){
      light.enabled = true;
    }
    rogueScript2.enabled = false;
  }

  void InformationAboutRooster(){
        RPosition = information.position;
    }
}
