using UnityEngine;
using System.Collections;

public class dialogscam : MonoBehaviour {
 public float dampTime = 0.15f; 
    private Vector3 velocity = Vector3.zero;
    private Vector3 See; 
    public Transform target; 

    void Start(){
        Debug.Log("not delete");
        if(GameObject.FindWithTag("Player") != null){
            target = GameObject.FindWithTag("Player").transform;
        }
    }
    // Update is called once per frame 
    void Update () { 
        if(GameObject.FindWithTag("Player") != null){
            target = GameObject.FindWithTag("Player").transform;
        }
        Camera camera = GetComponent<Camera>();
        if (target){ 
            Vector3 point = camera.WorldToViewportPoint(new Vector3 
            (target.position.x, target.position.y+0.75f,target.position.z)); 
            Vector3 delta = new Vector3(target.position.x, target.position.y+0.75f,target.position.z)  
            - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
                if(Information.Instance.isFacingRight){
                    See = new Vector3(5,1,0);
                }else{
                    See = new Vector3(-5,1,0);
                }
                Vector3 destination = transform.position + delta + See; 
 
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime); 
        } 
         
    } 
}
