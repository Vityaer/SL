using UnityEngine;

[AddComponentMenu("Destructible 2D/D2D Replace On Collision")]
public class D2D_ReplaceOnCollision : MonoBehaviour
{
	public Transform tr;
	
	public GameObject Spawn;
	private float xa,ya,xv,yv;

	private Information information;
    public Vector2 RPosition;
    private bool isGame = true;
	void Start(){
		tr = GetComponent<Transform>();
		information = Information.Instance;
	}
	protected virtual void OnCollisionEnter2D(Collision2D collision){
		if(isGame){
			if(collision.gameObject.layer == 8){
				if (Spawn != null){
					InformationAboutRooster();
			        var x = RPosition.x;
			        var y = RPosition.y;

                	xa = tr.position.x;
                	ya = tr.position.y;
                	xv = x - xa;
                	yv = y - ya;
			        xv = Mathf.Abs(Mathf.Sqrt(xv * xv + yv * yv));
			        if(xv < 10)
						CameraShake.Shake(Mathf.Min(1, 2/xv), Mathf.Min(1f, 2/xv), CameraShake.ShakeMode.XY);
				}	
				StartDeath();
			}
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		if(isGame){
			if(other.gameObject.GetComponent<PlayerHP>() != null){
					other.gameObject.GetComponent<PlayerHP>().GetClearDamage(1);
					isGame = false;
					CameraShake.Shake(1, 1, CameraShake.ShakeMode.XY);
					StartDeath();
			}
		}
	}
	private void StartDeath(){
		GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
		isGame = false;
		GetComponent<Animator>().Play("Death");
	}
	public void DestroyObject(){
		Destroy(gameObject);
	}
	 void InformationAboutRooster(){
        RPosition = information.position;
    }
}