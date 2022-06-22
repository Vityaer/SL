using UnityEngine;
using System.Collections;

public class MonetScript : MonoBehaviour {
    
    private bool isGame = true;
    public AudioClip AMonet;

    void OnTriggerEnter2D(Collider2D coll){
        if (coll.gameObject.GetComponent<PlayerHP>() && isGame){
            coll.gameObject.GetComponent<PlayerHP>().AddMonet(1);
            GetComponent<AudioSource>().PlayOneShot(AMonet);
            GetComponent<SpriteRenderer>().enabled = false;
            Information.Instance.game.AddDestroyItem(gameObject.name);
            Destroy(gameObject.transform.Find("Light").gameObject);        
            Destroy(gameObject,10);
            isGame = false;
        }
    }
}
