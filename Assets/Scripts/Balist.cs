using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Balist : MonoBehaviour, IDeath {
    public Transform tr;
    private Vector3 position;  
    public bool isFacingLeft = true;
    private Animator anim;
    public bool Finish = false;
    public GameObject thePrefab;
    private Quaternion rotation;
    private bool adviceVisible = false;
    // Use this for initialization
    void Awake(){
        GetComponent<EnemyHP>().interfaceDeath = this as IDeath;
        anim = GetComponent<Animator>();
        tr = GetComponent<Transform>();
    }
    void OnEnable () {
        anim.Play("Attack");
        CreateBolt();
    }

    void CreateBolt(){
        if (isFacingLeft){
           position = new Vector3(-1, 0, 0);
        }else{
            position = new Vector3(1, 0, 0);
        }
        Finish = true;
        position += transform.position;
        rotation = Quaternion.Euler(0, 0, 0);
        Instantiate(thePrefab,position,rotation);
        if(adviceVisible){
            InteractiveCanvasControllerScript.Instance.OffInteractive();
            adviceVisible = false;
        }
    }
    void OnTriggerEnter2D(Collider2D coll){
        if(coll.gameObject.GetComponent<PlayerHP>() && !GetComponent<EnemyHP>().gameover){
            InteractiveCanvasControllerScript.Instance.NewPosition(transform.position + new Vector3(0f, 1f, 0f), GameObject.Find("TextButtonStrike").GetComponent<Text>().text);
            adviceVisible = true;
        }
        if(Finish){
            if(coll.gameObject.GetComponent<PlayerHP>()){
                if(GameObject.Find("BoltController")){
                    if(GameObject.Find("BoltController").GetComponent<BoltController>().Quantity > 0){
                        GameObject.Find("BoltController").GetComponent<BoltController>().RemoveBolt();
                        Finish = false;
                        gameObject.layer = 9;
                        GetComponent<EnemyHP>().gameover = false;
                        GetComponent<EnemyHP>().HP = 1f;
                        GetComponent<Animator>().Play("Idle");
                        this.enabled = false;
                    }
                }
            }
        }
    }
    void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.GetComponent<PlayerHP>() != null  && !GetComponent<EnemyHP>().gameover){
            InteractiveCanvasControllerScript.Instance.OffInteractive();
        }
    }
    public void Death(){
        this.enabled = true;
    }
}
