using UnityEngine;
using System.Collections;

public class LiftUp : MonoBehaviour {

    public Rigidbody2D rb;
    public bool move = false;
    // Use this for initialization
    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update(){
        if (move){
            rb.velocity = new Vector2(0, 1);
        }else{
            rb.velocity = new Vector2(0, -1);
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.name == "Rooster"){
            move = true;
        }
        if (col.gameObject.name == "Right_side"){
            move = false;
        }
    }
}
