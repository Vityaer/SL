using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptimizationObserver : MonoBehaviour{

	public delegate void Del(bool flag);
	private Del observerChange;
   	[SerializeField] protected Transform tr; 
	void Awake(){
		flag = true;
		if(tr == null) tr = GetComponent<Transform>();
		OptimizationScript.Instance.RegisterOnChangePosition(OnChangePosition);
		OnChangePosition(OptimizationScript.Instance.GetPlayerPosition);
	}
	public void RegisterOnChangePosition(Del d){ observerChange += d; }
	public void UnregisterOnChangePosition(Del d){ observerChange -= d; }
	private void OnChangePosition(Vector3 pos){
		distance = (pos - tr.position).sqrMagnitude;
        if(flag != ( (pos - tr.position).sqrMagnitude < OptimizationScript.Instance.CalculatedRadius)){
        	flag = !flag; 
			if(observerChange != null)
				observerChange(flag);
        }
	}
	public float distance = 0f; 
	void OnDestroy(){OptimizationScript.Instance.UnregisterOnChangePosition(OnChangePosition); }
	Vector3 oldPos = new Vector3();
	public bool flag = true; 
}