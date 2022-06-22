using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeScript : MonoBehaviour{
	[SerializeField] private Animator anim;
	[SerializeField] private bool open = true;
	[SerializeField] private SwitchScript switchScript;
	void Start(){
		anim.SetBool("Move", open);
		if(open){ anim.Play("OpenState"); }else{ anim.Play("CloseState");}
		switchScript?.RegisterOnChange(Change);
	}
	public void Change(){
		Debug.Log("работай");
		open = !open;
		anim.SetBool("Move", open);
	}
}
