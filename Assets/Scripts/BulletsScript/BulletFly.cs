using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFly : MonoBehaviour{
    private bool isGame = true;
    private Rigidbody2D rb;
    public Transform tr;
    public float speed = 7.5f; 
    private Vector3 posRooster;
    public Vector2 miss = new Vector2(0,0);
    public int damage;
    public string Message;
    Vector3 dir = new Vector3();
    // Use this for initialization
    void Start () {
        Destroy(gameObject, 20);
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        InformationAboutRooster();
        posRooster = new Vector3(posRooster.x - UnityEngine.Random.Range(-miss.x, miss.x), posRooster.y - UnityEngine.Random.Range(-miss.y, miss.y), 0);
        dir = posRooster - tr.position;
        tr.rotation = Quaternion.Euler(0, 0, ((dir.x > 0) ? 180 : 0) + Mathf.Atan2(dir.y, dir.x)*(180/Mathf.PI));    
        rb.velocity = dir.normalized * speed;
    }

    void OnTriggerEnter2D(Collider2D coll){
        if ((coll.gameObject.GetComponent<PlayerHP>()) && (isGame)){
            isGame = false;
            if(coll.gameObject.GetComponent<PlayerHP>().HP > 0){
	            Information.Instance.CauseOfDeath = Message;
				bool leftDamage = (dir.x < 0) ? true : false;
				coll.gameObject.GetComponent<PlayerHP>().GetDamage(damage, leftDamage);
			}
            Destroy(gameObject);
        }
    }
    void InformationAboutRooster(){
        posRooster = Information.Instance.position;
    }
    private void Flip(){
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
