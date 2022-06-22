using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PointForCameraScript : MonoBehaviour{
	public bool isFollow = true;
	[SerializeField] private Transform tr;
	private Information information;
	[SerializeField] private CameraPosition selectPosition = CameraPosition.RightMain;
	[SerializeField] private Vector3 delta;
	[SerializeField] private GameObject rightMainCamera, leftMainCamera, rightDialogCamera, leftDialogCamera;
	private GameObject currentCamera;
	void Awake(){
		instance = this;
	}
	void Start(){ 
    	currentCamera = rightMainCamera;
		information = Information.Instance;
		SetStartPosition();
		Information.Instance.RegisterOnChangeIsFacingSide(OnChangeFacingSide);
		ChangePositionCamera(CameraPosition.RightMain); }
    public void SetStartPosition(){
    	tr.position = information.position;
    }
    void Update(){
    	if(isFollow){
			tr.position = information.PlayerPosition;
    	}
    }
    private void ChangePositionCamera(CameraPosition newPosition){
    	currentCamera.SetActive(false);
        float pos = currentCamera.transform.position.y;
    	this.selectPosition = newPosition;
    	switch(newPosition){
    		case CameraPosition.RightMain:
    			currentCamera = rightMainCamera;
    			break;
    		case CameraPosition.LeftMain:
    			currentCamera = leftMainCamera;
    			break;	
    		case CameraPosition.RightDialog:
	    		currentCamera = rightDialogCamera;
    			break;
    		case CameraPosition.LeftDialog:
	    		currentCamera = leftDialogCamera;
    			break;
    		default:
    			break;			
    	}
    	currentCamera.SetActive(true);
        currentCamera.transform.position = new Vector3(currentCamera.transform.position.x, pos, currentCamera.transform.position.z);
    }
    public void SetCameraToDialog(){
		ChangePositionCamera( information.isFacingRight ? CameraPosition.RightDialog : CameraPosition.LeftDialog);
    }
    public void SetSimpleCamera(){
    	ChangePositionCamera( information.isFacingRight ? CameraPosition.RightMain : CameraPosition.LeftMain );
    }
    private void OnChangeFacingSide(bool isFacingRight){
    	if(information.isDialog){
    		SetCameraToDialog();
    	}else{
    		SetSimpleCamera();
    	}
    }
    private static PointForCameraScript instance;
    public static PointForCameraScript Instance{get => instance;}
    void OnDestroy(){
    	Information.Instance.UnregisterOnChangeIsFacingSide(OnChangeFacingSide);
    }
}
public enum CameraPosition{
	RightMain = 0,
	LeftMain = 1,
	RightDialog = 2,
	LeftDialog = 3,
	UpMain = 4,
	DownMain = 5,
	Other = 6
}
