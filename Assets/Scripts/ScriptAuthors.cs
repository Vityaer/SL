using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptAuthors : MonoBehaviour {
	
	public Text HistoryText;
    bool end = false;
    public int idText;
    public GameObject ButtonMainMenu;
    public AudioClip SoundMouseEnter;
    public AudioClip SoundMouseClick; 
    public int NumLanguage;
    string[] arrayString;
	// Use this for initialization
	void Start () {
		HistoryText = HistoryText.GetComponent<Text>();
		if(Information.Instance)
			NumLanguage = Information.Instance.LanguageNumber;
        if(NumLanguage == 0){
			arrayString = new string[] {"Авторы идеи:\nВладислав Чикотеев\nЕрмаков Виктор","Сценарий:\nВладислав Чикотеев","Режиссер-сценарист:\nЕрмаков Виктор","Дизайнеры:\nВладислав Чикотеев\nЕкатерина Хисамутдинова","Программист:\nЕрмаков Виктор", "Тестировщик:\nЕкатерина Хисамутдинова","Игрок:\n ты :)","Спасибо что играли в\n     Standart Legend!"};
		}
		if(NumLanguage == 1){
			arrayString = new string[] {"Authors of the idea:\nVladislav Chikoteev\nViktor Ermakov","Screenplay:\nVladislav Chikoteev","Screenwriter:\nViktor Ermakov","Designers:\nVladislav Chikoteev\nEkaterina Khisamutdinova","Programmer:\nViktor Ermakov","Tester:\nEkaterina Khisamutdinova","Player:\n You! :)","Thanks for playing!"};
		}
		StartCoroutine(WriteMessage(arrayString[0]));
	}
	
	public void NextDialog(){
		StopAllCoroutines();
		if(!end){
			if (idText < arrayString.Length){
				idText += 1;
				StartCoroutine(WriteMessage(arrayString[idText]));
			}
			if(idText == arrayString.Length -1){
				end = true;
				GameObject.Find("ButtonSkip").SetActive(false);
				ButtonMainMenu.SetActive(true);
			}
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
    public void PlayMouseEnter(){
        GameObject.Find("Sounds").GetComponent<AudioSource>().PlayOneShot(SoundMouseEnter);
    }
    public void PlayMouseClick(){
        GameObject.Find("Sounds").GetComponent<AudioSource>().PlayOneShot(SoundMouseClick);
    }
}
