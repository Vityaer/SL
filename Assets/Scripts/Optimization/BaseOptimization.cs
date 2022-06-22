using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseOptimization : MonoBehaviour{
	[SerializeField] private OptimizationObserver observer;
	protected void Start(){
		if(observer == null) observer = GetComponent<OptimizationObserver>();
		observer.RegisterOnChangePosition(OnChangePosition);
		OnChangePosition(observer.flag);
	}
	private void OnChangePosition(bool flag){
		gameObject.SetActive(flag);
	}
	void OnDestroy(){ observer.UnregisterOnChangePosition(OnChangePosition); }
	public void Off(){ observer.UnregisterOnChangePosition(OnChangePosition); }
}