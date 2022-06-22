using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarScript : MonoBehaviour{
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private Transform tr;
	[SerializeField] private bool close = true;
	Coroutine coroutineMove;
	private Vector3 closePos;
	private Vector3 openPos;
	[SerializeField] private float speed = 1f;
	[SerializeField] private List<SwitchScript> switchScripts = new List<SwitchScript>();
    [SerializeField] private MovePlatform movePlatformScript;   

	void Start(){
		closePos = tr.position;
		openPos  = closePos + new Vector3(0, -2f, 0); 
		if(close != true) tr.position = openPos;
		foreach(SwitchScript script in switchScripts){
			script?.RegisterOnChange(Change);
		}
	}
	public void Change(){
		if(close == true){ Open(); } else { Close(); }
	}
	private void Open(){
		ClearCoroutineMove();
		coroutineMove = StartCoroutine(IMove(false));
	}
	private void Close(){
		ClearCoroutineMove();
		coroutineMove = StartCoroutine(IMove(true));
	}
	IEnumerator IMove(bool flag){
		if(this.close != flag){
			Vector3 target = flag ? closePos : openPos;
			Vector2 move = new Vector2(0, (target.y  > tr.position.y) ? speed : -speed); 
			rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
			while(((flag == true) && (target.y > tr.position.y)) ||((flag == false)&&(target.y < tr.position.y))){
	            movePlatformScript.ChangeVelocityObject(move); 
				rb.velocity = move;
				yield return null;
			}
		}
		rb.velocity = new Vector2(0, 0);
		rb.constraints = RigidbodyConstraints2D.FreezeAll;
		this.close = flag;
		coroutineMove = null;
	}
	void ClearCoroutineMove(){
		if(coroutineMove != null){
			StopCoroutine(coroutineMove);
			coroutineMove = null;
			close = !close;
		}
	}
}
