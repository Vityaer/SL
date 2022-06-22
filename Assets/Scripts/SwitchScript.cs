using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using HelpFuction;

public class SwitchScript : MonoBehaviour {
	[SerializeField] private bool ready = false;
	[SerializeField] private Sprite spriteOn, spriteOff; 
	[SerializeField] private SpriteRenderer renderer;
	[SerializeField] private AudioClip SoundMouseClick; 
	[SerializeField] private AudioSource audio;
	[SerializeField] private Transform tr;
	private Vector3 delta = new Vector3(0, 1f, 0);
    GameTimer timerIgrone;
    public bool ignorePush = false;
    private bool playerCollision = false;
	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.GetComponent<PlayerHP>() != null){
			playerCollision = true;
			InteractiveCanvasControllerScript.Instance.NewPosition(tr.position + delta, GameInput.Key.FindKey("F").ToString());
		}
	}
	void Update(){
		if(playerCollision){
			if(GameInput.Key.GetKeyDown("F")){
					Debug.Log("push");
				if(!ignorePush){
					Debug.Log("action");
					ignorePush = true;
					ready = !ready;
					renderer.sprite = ready? spriteOn : spriteOff;
					PlayMouseClick();
					OnChange();
				}
			}
		}
	}
	void OnTriggerExit2D(Collider2D col){
		if(col.gameObject.GetComponent<PlayerHP>() != null){
			playerCollision = false;
			InteractiveCanvasControllerScript.Instance.OffInteractive();
		}
	}
	private void PlayMouseClick(){
       audio.PlayOneShot(SoundMouseClick);
    }
    private Action observerChange;
    public void RegisterOnChange(Action d){
    	observerChange += d;
    }
    public void UnregisterOnChange(Action d){
    	observerChange -= d;
    }
	[ContextMenu("Change")]
    private void OnChange(){
    	if(observerChange != null)
    		observerChange();
    		Debug.Log("start ignore");
    	timerIgrone = TimerScript.Timer.StartTimer(0.75f, StopIgnogePush);
    }
    void StopIgnogePush(){
    		Debug.Log("finish ignore");
    	ignorePush = false;
    }
	void OnDestroy(){
		if(timerIgrone != null) TimerScript.Timer.StopTimer(timerIgrone);
	}

}
