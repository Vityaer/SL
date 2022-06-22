using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptimizationScript : MonoBehaviour{
	public delegate void Del(Vector3 pos);
	private Del observerPositionPlayer;
	[SerializeField] private float radiusVisible;
	private float calculatedRadius;
	public float CalculatedRadius{get => calculatedRadius;}
	private Information information;
	private Vector3 oldPlayerPosition,playerPosition;
	public bool calculate = true;
	void Awake(){ 
		if(instance != null){
			Destroy(gameObject);
		}else{
			instance = this;
		}
	}
	void Start(){
		calculatedRadius = radiusVisible * radiusVisible;
		information = Information.Instance;
		SceneManager.sceneLoaded += OnSceneLoaded;
	}
	void Update(){ InformationAboutRooster(); }
	public void RegisterOnChangePosition(Del d){ observerPositionPlayer += d; }
	public void UnregisterOnChangePosition(Del d){ observerPositionPlayer -= d; }
	private void ChangePosition(){
		if(observerPositionPlayer != null){
			if(calculate == true){
				observerPositionPlayer(playerPosition);
			}
		}
	  }
	void InformationAboutRooster(){
        playerPosition = information.position;
        if((playerPosition - oldPlayerPosition).sqrMagnitude > 9f){
        	oldPlayerPosition = playerPosition;
        	ChangePosition();
        }
    }
    public void OnCreatedPlayer(Vector3 position){
    	Debug.Log(position);
    	playerPosition = position;
    	ChangePosition();
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
    	ChangePosition();
    }
    public Vector3 GetPlayerPosition{get => Information.Instance.position; }
	private static OptimizationScript instance;
	public static OptimizationScript Instance{get => instance;}
}