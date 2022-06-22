using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLevel8 : MonoBehaviour {
    public float dampTime = 0.05f; 
    private Vector3 velocity = new Vector3(0,10,0);
    private Vector3 See; 

    public Vector2 RPosition;
    public bool RinDialogs, RFacingRight;

    private CameraShake cameraShake;
    private Information information;
    Camera camera;
    Vector3 point = new Vector3(), destination = new Vector3(), delta = new Vector3();
    void Awake(){
        cameraShake = GetComponent<CameraShake>();
        camera = GetComponent<Camera>();
        dampTime = 0.05f;
        if(Screen.width < 700){
            camera.orthographicSize = 3.46f;
        }
        if((Screen.width >= 700)&&(Screen.width < 1000)){
            camera.orthographicSize = 3.46f;
        }
        if(Screen.width >= 1000){
            camera.orthographicSize = 5f;
        }
    }
    void Start(){
    information = Information.Instance;
    RPosition = Information.Instance.position;
    InformationAboutRooster();
    }
    // Update is called once per frame 
    void Update () { 
        InformationAboutRooster();
        point = camera.WorldToViewportPoint(new Vector3(RPosition.x, RPosition.y+0.75f, 0f)); 
        delta = new Vector3(RPosition.x, RPosition.y+0.75f, 0f) - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
                if(RFacingRight){
                    See = new Vector3(3,1,0);
                }else{
                    See = new Vector3(-3,1,0);
                }
                destination = transform.position + delta + See; 
 
        transform.position = Vector3.Lerp(transform.position, destination, dampTime);
        cameraShake.OriginalPos = transform.position;  
    }
    void InformationAboutRooster(){
        if(!information.isLadder){
            if(Mathf.Abs(RPosition.y - information.position.y)< 1.5f){
                RPosition.y = information.position.y;
            }
        	if(information.position.x < 11.31f){
                RPosition.x = information.position.x;
        	}
        }else{
            if(information.position.x < 11.31f){
                RPosition.x = information.position.x;
        	}
            RPosition.y = information.position.y;
        }
        RinDialogs = information.isDialog;
        RFacingRight = information.isFacingRight;
    } 
}
