using UnityEngine;
using System.Collections;

public class FireHelpBolt : MonoBehaviour, IDeath {

	public GameObject BoltLeft;
	public GameObject BoltRight;
	public int quantity = 2;
	private float myTimerCreate = 0.5f;
	// Use this for initialization
	void Awake(){
        GetComponent<EnemyHP>().interfaceDeath = this as IDeath;
	}
	void Start(){
		myTimerCreate = 0.5f;
	}
	void Update() {
		if (myTimerCreate > 0){
            myTimerCreate -= Time.deltaTime;
        }else{
			if(quantity > 0){
				GameObject bullet = Instantiate(BoltLeft, GetComponent<Transform>().position + new Vector3(-3f,0,0), GetComponent<Transform>().rotation);
					       bullet.GetComponent<RoostersMagicFly>().DoImpulse(transform.position, GetComponent<Transform>().position + new Vector3(-6f, 0, 0));
						   bullet = Instantiate(BoltRight, GetComponent<Transform>().position + new Vector3(3f,0,0), GetComponent<Transform>().rotation);
					       bullet.GetComponent<RoostersMagicFly>().DoImpulse(transform.position, GetComponent<Transform>().position + new Vector3(6f, 0, 0));
			}
			quantity--;
			myTimerCreate = 0.5f;
		}
		if(quantity == 0){
			Destroy(gameObject);
		}
	}
	public void Death(){
		this.enabled = true;
	}
}
