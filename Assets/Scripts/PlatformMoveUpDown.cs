using UnityEngine;
using System.Collections;

public class PlatformMoveUpDown : MonoBehaviour{
    public Rigidbody2D rb;
    public Transform tr;
    Coroutine TimeStopCoroutine; 
    private MovePlatform movePlatformScript;   
    void Awake(){
        movePlatformScript = GetComponent<MovePlatform>();
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }
    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.name == "Down_side"  || col.gameObject.GetComponent<PlayerHP>()){
            if(TimeStopCoroutine != null){
                StopCoroutine(TimeStopCoroutine);
                TimeStopCoroutine = null;
            }
            TimeStopCoroutine = StartCoroutine(ITimerStop(true));
        }
        if (col.gameObject.name == "Up_side"){
            if(TimeStopCoroutine != null){
                StopCoroutine(TimeStopCoroutine);
                TimeStopCoroutine = null;
            }
            TimeStopCoroutine = StartCoroutine(ITimerStop(false));
        }
    }
    IEnumerator ITimerStop(bool move){
        rb.velocity = new Vector2(0, 0);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        float timerStop = 3f;
        while(timerStop >= 0f){
            timerStop -= Time.deltaTime;
            yield return null;
        }
        rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        while(true){
            movePlatformScript.ChangeVelocityObject(move ? new Vector2(0, 3) : new Vector2(0, -3)); 
            rb.velocity = move ? new Vector2(0, 3) : new Vector2(0, -3);
            yield return null;
        }
    }
}
