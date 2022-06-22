using UnityEngine;
using System.Collections;

public class CloadMove : MonoBehaviour {

	private float Speed; 
    public Transform tr;
	public Rigidbody2D rb;

	public GameObject Wall;
	// Use this for initialization
	void Start () {
        Wall = GameObject.Find("CloudWallStart");
		Speed = 0.1f;
        rb = GetComponent<Rigidbody2D>();
		tr = GetComponent<Transform>();
	    rb.velocity = new Vector2(-Speed, 0);
	}

	void OnTriggerExit2D(Collider2D col){
        if (col.gameObject.name == "CloudWall"){
			tr.position = new Vector3(Wall.GetComponent<Transform>().position.x, tr.position.y, 0);
        }
    }
}
