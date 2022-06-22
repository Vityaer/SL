using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ManaBar : MonoBehaviour {
	public int MP;
	int MP1;
	public Image ImageMP;
	public Text MPBar;
    public String outMP;
    private RoosterScriptWizard roosterScriptWizard;
	void Start () {
		roosterScriptWizard = GetComponent<RoosterScriptWizard>();
		MP = roosterScriptWizard.MP;
		MP1 = MP;
		ImageMP = GameObject.Find("ImageMPBar").GetComponent<Image>(); 
        ImageMP.enabled = true;
        MPBar = GameObject.Find("MPBar").GetComponent<Text>();
		MPBar.enabled = true;
		DrawChange();
	}
	
	void Update () {
		MP = roosterScriptWizard.MP;
		if(MP1 != MP){
			DrawChange();
		}	
	}
	public void DrawChange(){
        outMP = "x";
        outMP += MP.ToString();
        MPBar.text = outMP;
    }
}
