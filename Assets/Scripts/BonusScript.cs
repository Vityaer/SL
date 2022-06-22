using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelpFuction;
public class BonusScript : MonoBehaviour {

	[SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform tr;
    private bool MoveUp;
    GameTimer timerMove;
    private bool work = false;
	// Use this for initialization
	void Awake(){
		if(tr == null) tr = GetComponent<Transform>();
		if(rb == null) rb = GetComponent<Rigidbody2D>();
	}
	void Start () {
		if(Information.Instance.CheckItem(gameObject.name) == false){
			OptimizationScript.Instance.RegisterOnChangePosition(OnChangePosition);
			OnChangePosition(OptimizationScript.Instance.GetPlayerPosition);
	        MoveUp = (Random.Range(0, 2) == 1) ? true : false;	
		}else{
			Destroy(gameObject);
		}

	}
	public void ChangeDirection(){
		if(work == true){
			if(TimerScript.Timer != null){
		        timerMove = TimerScript.Timer.StartTimer(0.75f, ChangeDirection);
				MoveUp      = !MoveUp;
				rb.velocity = MoveUp ? new Vector2(0f, 0.1f) : new Vector2(0f, -0.1f);  
			}
		}
	}
	public void NewPosition(Vector3 newPosition){
		transform.position = newPosition;
	}
	void OnDestroy(){
		if(timerMove != null) TimerScript.Timer.StopTimer(timerMove);
		OptimizationScript.Instance.UnregisterOnChangePosition(OnChangePosition);
	}
	void OnChangePosition(Vector3 pos){
		if( ( (pos - tr.position).sqrMagnitude < OptimizationScript.Instance.CalculatedRadius)){
	        if(work == false){
	        	work= true;
		        ChangeDirection();
	        }
		}else{
			work = false;
			rb.velocity = new Vector2(0, 0);
		}
	}
}
