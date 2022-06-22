using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level8CheckOption : MonoBehaviour {

	void Start () {
        if(Information.Instance.OptionGirl){
        	GetComponent<EnemyHP>().GetDamage(100f);
        }
	}
}
