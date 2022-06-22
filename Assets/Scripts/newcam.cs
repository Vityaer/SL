using UnityEngine; 
using System.Collections; 
using System;
using System.Collections.Generic;
public class newcam : MonoBehaviour {
    public float dampTime = 0.05f; 
    public float deltaY = 0.25f;
    private Vector3 velocity = new Vector3(0,10,0);
    private Transform tr;
    public Vector2 RPosition;
    public bool RinDialogs, RFacingRight;

    private Information information;
    Vector3 point = new Vector3(), destination = new Vector3(), delta = new Vector3(), See = new Vector3(3, 1, 0);
    Camera camera;
    void Awake(){
        dampTime = 0.05f;
        camera = GetComponent<Camera>();
        camera.orthographicSize = (Screen.width < 1000) ? 3.84f : 7.68f;
        tr = GetComponent<Transform>();
    }
    void Start(){
        Debug.Log("not delete");
    information = Information.Instance;
    RPosition = information.position;
    InformationAboutRooster();
    }
    public float dampTimeSee = 0.025f;
    public float dampTimeFollow = 0.0375f;
    public float dampTime3 = 0.05f;
    // Update is called once per frame 
    void LateUpdate () { 
        InformationAboutRooster();
        point = camera.WorldToViewportPoint(new Vector3 
            (RPosition.x, RPosition.y+0.75f, 0f)); 
            delta = new Vector3(RPosition.x, RPosition.y+0.75f, 0f)  
            - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
                See.x  = (RFacingRight) ? 3f : -3f;
                destination = tr.position + delta + See; 
                if(Mathf.Abs(tr.position.x - destination.x) > 1){
                    dampTime = dampTimeSee;
                    qRound = 4;
                }
                if(Mathf.Abs(tr.position.x - destination.x) <= 1){
                    dampTime = dampTimeFollow;
                    qRound = 3; 
                }
                if(Mathf.Abs(tr.position.x - destination.x) <= 0.5f){
                    dampTime = dampTime3;
                    qRound = 2;
                }
            if(Mathf.Abs(tr.position.y - RPosition.y) < deltaY) RPosition.y = tr.position.y;  
            tr.position = Vector3.Lerp(tr.position, destination, dampTime);
            positionCamera = tr.position;
            tr.position = new Vector3((float) Math.Round(positionCamera.x, qRound), (float)Math.Round(positionCamera.y, qRound), positionCamera.z) ;
    }

    Vector3 positionCamera;
    public int qRound = 2;
    void InformationAboutRooster(){
        if(!information.isLadder){
            if(Mathf.Abs(RPosition.y - information.position.y)< 1.5f){
                RPosition.x = information.position.x;
            }else{
                RPosition = information.position;
            }
        }else{
            RPosition = information.position;
        }
        RinDialogs = information.isDialog;
        RFacingRight = information.isFacingRight;
    } 
}