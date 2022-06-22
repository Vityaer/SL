using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoltController : MonoBehaviour {
	public int Quantity = 0;
	public GameObject UIBolt;
	public Text textCountBolt;
	public void AddBolt(int addCount = 1){
		Quantity += addCount;
		UpdateInfo();
	}
	public void RemoveBolt(){
		if(Quantity > 0){ Quantity -= 1; }
		UpdateInfo();
	}
	private void UpdateInfo(){
		UIBolt.SetActive(Quantity > 0);
		textCountBolt.text = string.Concat("x ", Quantity.ToString());
	}
}
