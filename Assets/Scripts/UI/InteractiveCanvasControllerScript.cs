using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class InteractiveCanvasControllerScript : MonoBehaviour{

	private Canvas canvas;
	private Transform tr;
	public Text textMessage;
	public Image radialProgressBar;
	private bool visible = false, proccess = false;
	void Awake(){
		instance = this; 
		canvas = GetComponent<Canvas>();
		tr = GetComponent<Transform>();
	} 
	public void NewPosition(Vector3 pos){
		if(visible == false && proccess == false){
			proccess = true;
			textMessage.DOFade(1f, 0.25f).OnComplete(FinishVisible);
			radialProgressBar.DOFade(1f, 0.2f);
			tr.position = pos;
			tr.LookAt(Camera.main.transform.position);
		}
	}
	public void NewPosition(Vector3 pos, string message){
		textMessage.text = message;
		NewPosition(pos);
	}

	Coroutine coroutineOffInteractive;
	public void OffInteractive(){
		if(coroutineOffInteractive != null){
			StopCoroutine(coroutineOffInteractive);
			coroutineOffInteractive = null;
		}
		coroutineOffInteractive = StartCoroutine(ICoroutineOffInteractive());
	}
	IEnumerator ICoroutineOffInteractive(){
		while(visible == false && proccess == true){
			yield return null;
		}
		if(visible == true && proccess == false){
			proccess = true;
			textMessage.DOFade(0f, 0.25f).OnComplete(FinishOffVisible);
			radialProgressBar.DOFade(0f, 0.2f);
		}
	}
	public void FinishOffVisible(){
		visible = false;
		proccess = false;
	}

	public void FinishVisible(){
		visible  = true;
		proccess = false;
	}
	private static InteractiveCanvasControllerScript instance;
	public  static InteractiveCanvasControllerScript Instance{get => instance;}


}
