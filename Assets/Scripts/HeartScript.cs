using UnityEngine;
using System.Collections;

public class HeartScript : MonoBehaviour {

	private bool isGame = true;
    public AudioClip AHeart;
	 void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.GetComponent<PlayerHP>()&& isGame){
            col.gameObject.GetComponent<PlayerHP>().AddHP(1);
            GetComponent<AudioSource>().PlayOneShot(AHeart);
            Information.Instance.game.AddDestroyItem(gameObject.name);
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject.transform.Find("Light").gameObject);   
            Destroy(gameObject,10);
            isGame = false;
           
        }
    }    
}
