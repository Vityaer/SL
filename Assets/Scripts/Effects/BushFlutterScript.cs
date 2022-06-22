using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelpFuction;
public class BushFlutterScript : MonoBehaviour{
    public static int layerPlayer = 11;
    public SpriteRenderer sprite;
    public CircleCollider2D collider;
    private Material material;
    public float minDeltaX = 0.85f, maxDeltaX = 1.3f, middleDeltaX = 1f;
    void Start(){
    	if(sprite.sortingOrder < layerPlayer){
    		collider.enabled = false;
    	}else{
	    	material = GetComponent<SpriteRenderer>().material;
    	}
    }
    bool fluttering = false, canCollision = true;
    Coroutine coroutineFlutter;
	float deltaX;
	Transform target;
	[Header("Sounds")]
	[SerializeField] private List<AudioClip> noises = new List<AudioClip>();
	[SerializeField] private AudioSource audio;
    void OnTriggerEnter2D(Collider2D coll){
    	if((canCollision) && (material != null)){
	    	if(coll.GetComponent<EnemyHP>() || coll.GetComponent<PlayerHP>() || coll.GetComponent<ArrowFly>() || coll.GetComponent<BoltFly>() || coll.GetComponent<FireBallScript>()){
		    	canCollision = false;
		    	target = coll.transform;
		    	if(fluttering){
		    		StopCoroutine(coroutineFlutter);
		    		coroutineFlutter = null;
		    	}
		    	audio.PlayOneShot(noises[Random.Range(0, noises.Count)]);
	    		deltaX = transform.position.x - coll.transform.position.x;
	    		coroutineFlutter = StartCoroutine(IFlutter(deltaX));
	    	}
    	}
    }
    private void OnTriggerExit2D(Collider2D coll){
    	if(coll.transform == target){
	    	canCollision = true;
	    	target = null;
    	}
    }
    public float speed = 2f;
    IEnumerator IFlutter(float dir){
		deltaX = material.GetFloat("deltaX");
		float velocity = (dir < 0) ? speed : -speed;
		int step = 3; 
		while(step > 0){
			deltaX += Time.deltaTime * velocity;
			if((deltaX > maxDeltaX) || (deltaX < minDeltaX)){
				step -= 1;
				// velocity *= -0.5f;
			}
			material.SetFloat("deltaX", deltaX);
			yield return null;
		}
		velocity = Mathf.Abs(velocity);
		while(deltaX < middleDeltaX){
			deltaX += Time.deltaTime * velocity;
			material.SetFloat("deltaX", deltaX);
			yield return null;
		}
		while(deltaX > middleDeltaX){
			deltaX -= Time.deltaTime * velocity;
			material.SetFloat("deltaX", deltaX);
			yield return null;
		}
		material.SetFloat("deltaX", middleDeltaX);
    }
}
