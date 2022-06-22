using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageTranslate : MonoBehaviour
{
	public string Language0;
	public string Language1;
    void Start(){
    		if(Information.Instance != null){
				if(Information.Instance.LanguageNumber == 0){
					GetComponent<Text>().text = Language0;
				}else{
					GetComponent<Text>().text = Language1;
				}        
    		}else{
				GetComponent<Text>().text = Language0;
    		}
    }
}
