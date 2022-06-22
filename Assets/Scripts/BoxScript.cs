using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BoxScript : MonoBehaviour, IDeath {

	public GameObject Bonus1;
    public GameObject Bonus2;
    public GameObject Enemy1;
    public GameObject Enemy2;
    [SerializeField] Rigidbody2D rb;
	// Use this for initialization
	void Awake(){
        GetComponent<EnemyHP>().interfaceDeath = this as IDeath;
	}
	void Start () {
		if(GetComponent<EnemyHP>().killFromSave == false){
			var Rand = UnityEngine.Random.Range(0, 100);
			if(Rand <= 50){
	            Instantiate(Bonus1, GetComponent<Transform>().position+new Vector3(0, 1,0), GetComponent<Transform>().rotation);
			}
			if((Rand > 50)&&(Rand <= 80)){
	            Instantiate(Bonus2, GetComponent<Transform>().position+new Vector3(0, 1,0), GetComponent<Transform>().rotation);
			}
			if((Rand > 80)&&(Rand <= 95)){
	            Instantiate(Enemy1, GetComponent<Transform>().position+new Vector3(-1, 0,0), GetComponent<Transform>().rotation);
			}
			if(Rand > 95){
	            Instantiate(Enemy2, GetComponent<Transform>().position+new Vector3(-1, 0,0), GetComponent<Transform>().rotation);
			}
		}
		GetComponent<BoxCollider2D>().enabled = false;
		InteractiveCanvasControllerScript.Instance.OffInteractive();
	}
	bool isShow = false;
	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.GetComponent<PlayerHP>() != null  && !GetComponent<EnemyHP>().gameover && !isShow){
			isShow = true;
			InteractiveCanvasControllerScript.Instance.NewPosition(transform.position + new Vector3(0f, 0.5f, 0f), GameObject.Find("TextButtonStrike").GetComponent<Text>().text);
		}
	}
	void OnTriggerExit2D(Collider2D col){
		if(col.gameObject.GetComponent<PlayerHP>() != null && !GetComponent<EnemyHP>().gameover && isShow){
			InteractiveCanvasControllerScript.Instance.OffInteractive();
			isShow  = false;
		}
	}
	public void Death(){
		this.enabled = true;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(0,0);
	}
}
