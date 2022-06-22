using UnityEngine;
using System.Collections;

public class ArrowFly : MonoBehaviour {

    private bool isGame = true;
    public Rigidbody2D rb;
    public Transform tr;
    public float speed = 7.5f; 
    private Vector3 posRooster, posArrow, dir, s0;
    public Vector2 miss = new Vector2(0,0);
    public int damage;
    private Information information;
    public string Message;
    public float gravity = 9.8f;
    [SerializeField] AudioSource audio;
    [SerializeField] AudioClip noiseGroundFinish;
    // Use this for initialization
    void Start () {
        Destroy(gameObject, 20);
        tr = GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        information = Information.Instance;
        InformationAboutRooster();
        posRooster = new Vector3(posRooster.x - UnityEngine.Random.Range(-miss.x, miss.x), posRooster.y - UnityEngine.Random.Range(-miss.y, miss.y), 0);
        posArrow = new Vector3(tr.position.x, tr.position.y, 0);
        dir = posRooster - posArrow;

        if (dir.x < 0){
            tr.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x)*(180/Mathf.PI));    
        }else{
            tr.rotation = Quaternion.Euler(0, 0, 180+Mathf.Atan2(dir.y, dir.x)*(180/Mathf.PI));    
        }
        if(dir.sqrMagnitude > 9f){
	        float speed2 = Mathf.Pow(speed, 2);
	        float speed4 = Mathf.Pow(speed, 4);
	        float x = dir.x, y = dir.y;
	        float gx = gravity*x;

	        float root = speed4 - gravity*(gravity*x*x + 2*y*speed2);
	        root = Mathf.Sqrt(Mathf.Abs(root));
	        float lowAng = Mathf.Atan2(speed2 - root, gx);
	        float highAng = Mathf.Atan2(speed2 + root, gx);
	        if(Mathf.Abs(dir.y) > 2f){
		        s0 = new Vector2(Mathf.Cos(highAng), Mathf.Sin(highAng))*speed * 2;
	        }else{
		        s0 = new Vector2(Mathf.Cos(lowAng), Mathf.Sin(lowAng))*speed * 2;
	        }
    	}else{
    		s0 = dir.normalized * speed * 2;
    	}
        rb.AddForce(s0);
    }
    Vector3 dirRotate;
    void Update(){
        if(isGame)
            tr.rotation = Quaternion.Euler(0, 0, 180+Mathf.Atan2(rb.velocity.y, rb.velocity.x)*(180/Mathf.PI));    
    }
    void OnTriggerEnter2D(Collider2D coll){
        if(coll.gameObject.CompareTag("Ground")){
            isGame = false;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            this.enabled = false;
            audio.PlayOneShot(noiseGroundFinish);
        }
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
        posRooster = information.position;
    }
    private void Flip(){
        //получаем размеры персонажа
        Vector3 theScale = transform.localScale;
        //зеркально отражаем персонажа по оси Х
        theScale.x *= -1;
        //задаем новый размер персонажа, равный старому, но зеркально отраженный
        transform.localScale = theScale;
    }
    private static float range = -1f;
    public float GetRange{get {
    		if(range < 0f) range = 2 * speed * speed *0.49f / gravity;
    		return range;
    	}
    }
}
