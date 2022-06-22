using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Xml;
using UnityEngine.Audio;


public class ScriptTheEnd : MonoBehaviour {

	public Text HistoryText;
    bool end = false;
    public int idText;
    public GameObject ButtonMainMenu;
    public GameObject ButtonAutors;
    public AudioClip SoundMouseEnter;
    public AudioClip SoundMouseClick; 
    public int NumLanguage = 1;
    string[] arrayString;
    Coroutine speakCoroutine = null;
	void Start () {
        if(Information.Instance)
            NumLanguage = Information.Instance.LanguageNumber;
        if(NumLanguage == 0){
            arrayString = new string[] {"Рустер вернулся в свой замок.","Там его уже поджидал отряд мечников, которым было приказано казнить героя.","Король Лонселот захватил власть и боялся потерять трон.","Он знал что бывший король отдал свой перстень Рустеру и решил расправиться с конкурентом на престол.","Так и закончилась славная\nжизнь нашего Героя.","Сэр Лонселот с принцессой\nправили долго и мудро,\nи оставили после себя\nмогучий род Королей и Королев!","И жили они\nдолго и счастливо!"};
        }
        if(NumLanguage == 1){
            arrayString = new string[] {"Rooster returned to his castle.","There, a detachment of swordsmen,\nwho were ordered to execute the hero,\nwas waiting for him.","King Loncelot seized power and was afraid of losing the throne.","He knew that the former\nking had given his ring to Rooster\nand decided to make short\nwork of his rival to the throne.","And so ended the\nglorious life of our Hero.","Sir Loncelot and the princess\nruled long and wisely, and left behind\na mighty family of Kings and Queens!","And they lived happily\never after!"};
        }
		HistoryText = HistoryText.GetComponent<Text>();
		speakCoroutine = StartCoroutine(WriteMessage(arrayString[0]));
	}
	
	public void NextDialog(){
        if(idText != arrayString.Length - 1){
            if(speakCoroutine != null){
                StopCoroutine(speakCoroutine);
                speakCoroutine = null;
            }
    		if(!end){
    			if (idText < arrayString.Length - 1){
    				idText += 1;
                    if(speakCoroutine == null)
        				speakCoroutine = StartCoroutine(WriteMessage(arrayString[idText]));
    			}
    		}
        }else{
            ButtonMainMenu.SetActive(true);
            ButtonAutors.SetActive(true);
        }
	}
	IEnumerator WriteMessage(string Message){
        HistoryText.text = "";
        char[] sentense = Message.ToCharArray();
        for(int i=0;i<sentense.Length; i++){
            HistoryText.text += sentense[i];
            yield return new WaitForSeconds(0.025f);
        }
    }
    public void InMainMenu(){
        FadeInOut.nextLevel = "Scene0";
        FadeInOut.sceneEnd = true;
    }
    public void InAuthors(){
        FadeInOut.nextLevel = "Authors";
        FadeInOut.sceneEnd = true;
    }
    public void PlayMouseEnter(){
        GameObject.Find("Sounds").GetComponent<AudioSource>().PlayOneShot(SoundMouseEnter);
    }
    public void PlayMouseClick(){
        GameObject.Find("Sounds").GetComponent<AudioSource>().PlayOneShot(SoundMouseClick);
    }
}
