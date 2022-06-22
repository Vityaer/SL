using UnityEngine;
using System.Collections;

public class RogueGoInToilet : MonoBehaviour {

	public Rigidbody2D rb;
	private Animator anim;
	public Transform tr;
	public bool grounded = true;
	public float groundRadius = 0.05f;
    public LayerMask whatIsGround;
    public Transform groundCheck;
	public GameObject RogueForFall;
	[SerializeField] private Transform posCreate;
    bool create = false;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		anim.SetBool("Speed", true);
		anim.Play("Run");
		Dialogs.Instance.SpeakWord();
	}
	
	// Update is called once per frame
	void Update () {
		GroundCheckFinction();
		rb.velocity = new Vector2(2, 0);
	}
	void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.name == "toilet"){
			if(!create){
				create = true;
	        	Dialogs.Instance.skip();
	        	CreateRogue();
	            Destroy(gameObject);
	        }
		}
    }
     void CreateRogue(){
        Debug.Log("create rogue");
        tr = GetComponent<Transform>();
        Instantiate(RogueForFall,posCreate.position, Quaternion.identity);
    }
    void GroundCheckFinction(){
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("grounded", grounded);
    }
}
