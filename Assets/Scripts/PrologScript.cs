using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrologScript : MonoBehaviour {

	public Text HistoryText;
	public int idText;
	public int NumLanguage;
	string[] arrayString;
	bool end = false;
	public AudioClip SoundMouseEnter;
    public AudioClip SoundMouseClick;
    private Text textComponent;
    public Text textSkipProlog;
    Coroutine textPrint;
	void Start () {
		audio = GameObject.Find("Sounds").GetComponent<AudioSource>();
		textComponent = GameObject.Find("Skip").GetComponent<Text>();
		NumLanguage = (Information.Instance != null) ? Information.Instance.LanguageNumber : 1;
		if(NumLanguage == 0){
			textSkipProlog.text = "Пропустить";
	        arrayString = new string[]{"Жил да был рыцарь по имени\nРустер. Далеко-далеко жил, у\nсамого северного моря.\nТам стоял его замок – маяк.\nДа только без толку – корабли это\nместо не посещали уже много лет!\nДаже мимо не ходили.","Рустер был очень бедный – даже\nконя купить не мог. Потому ходил пешком.\nКогда отец умер, он оставил\nРустеру шлем, щит, меч, две медных монеты, и Коня.\nПросто коня, без имени.","Вскоре Король в далекой Столице Королевства объявил рыцарский турнир.\nНаградой за победу была солидная сумма, и поцелуй Принцессы!\nРустер решил, что терять ему все равно нечего.","И он отправился в путешествие.\nТочнее, поехал верхом. Не доехал. В нескольких милях от столицы\nКонь ускакал на небеса к\nстарому хозяину- Рустеру старшему.","Наш герой, погоревав недолго,\nпришел на турнир пешим, и проиграл\nв первом же состязании. «Все-таки было что терять» – грустно подумал Рустер,\nлатая истершуюся от долгой ходьбы обувь.\nДо дома было еще недели"," четыре пути по осенней грязи,\nи близилась зима…. Но одно Рустер все же получил на\nтом турнире – однажды увидев,\nон не мог забыть Принцессу. Она была прекрасна, как божий дар!","Да только… Как бы не умереть\nс голоду по дороге в голодный дом?…\nКогда Рустер вернулся домой,\nон с порога узнал ужасную новость. Принцессу похитили! Ужасный Дракон унес девушку далеко в горы и заточил в черной башне!","Король кинулся в погоню,\nно пропал вместе со всей\nсвоей армией в Огромном Лесу,\nтак и не добравшись до цели…","И казалось, никому теперь не было\nдела до несчастной девушки… Кроме нашего бедного и слабого,\nно очень благородного и решительного\nСэра Рустера!","Отважный рыцарь отправляется на\nлегендарный подвиг! Судьбы героев отныне в твоих руках, уважаемый игрок!\nДобро пожаловать в\n"};
		}else{
			textSkipProlog.text = "Skip";
	        arrayString = new string[]{"There lived a knight named\nRooster. He lived far\nfar away from the very northern sea.\nThere stood his castle - a lighthouse.","Yes, but to no avail - \nthe ships have not visited this place\nfor many years!\n","Even past did not go.\nRooster was very poor -\nhe could not even buy a horse.","Because went on foot.\nWhen his father died, he left a helmet,","a shield, a sword,\ntwo copper coins,\nand a horse to Rooster.","Just a horse, without a name.","Shortly after the death of his father,\nthe King in the faraway\nCapital of the Kingdom\nannounced a joust.","The reward for the victory was a solid\namount, and kiss the Princess!\nRooster decided that he still\nhad nothing to lose.","And he went on a journey.\nMore precisely, I went on horseback.\nDid not reach.\n","A few miles from the capital,\nthe Horse rode off to heaven to meet\nthe old master, Rooster the eldest.","Our hero, having grieved for long,\ncame to the tournament on foot,\nand lost in the first match.", "Still, there was something to lose, \nthought Rooster sadly, patching his\nshoes, which were being\nworn out from a long walk.","The house was still four weeks through\nthe autumn mud, and winter\nwas nearing...","But Rooster did get one thing at that\ntournament - once he saw, he could\nnot forget the Princess.","She was beautiful as a gift from God!\nBut only ... How not to die of hunger\non the way to a hungry house?...","When Rooster returned home,\nhe heard terrible news from the doorway.","Princess kidnapped!","The terrible Dragon took\nthe girl far into the mountains\nand imprisoned in\nthe black tower!","The king rushed in pursuit, but\ndisappeared along with his entire army\nin the Great Forest, and without\nreaching the goal...","He called the faithful lords for help,\nand the faithful lords immediately\ndecided to help!","Everyone wanted to shoulder the burden\nof power, and to select the\nmost worthy candidate, they\nbegan to divide the land.","And it seemed that no one now had\nto do with the unhappy girl...","Except for our poor and weak,\nbut very noble and decisive \nSir Rooster!","The brave knight is sent\nto the legendary feat!","The fate of the heroes\nis now in your hands,\ndear player!","Welcome to the\n"};
		}
		textPrint = StartCoroutine(WriteMessage(arrayString[0]));
	}
	void Update(){
		if(Screen.width > 1000){
			textComponent.fontSize = 18;
		}
	}
	public void NextDialog(){
		if(textPrint != null){
			StopCoroutine(textPrint);
			textPrint = null;
		}
		if(!end){
			if (idText < arrayString.Length){
				idText += 1;
				textPrint = StartCoroutine(WriteMessage(arrayString[idText]));
			}
			if(idText == arrayString.Length -1){
				end = true;
				if(NumLanguage == 0){
					GameObject.Find("Skip").GetComponent<Text>().text = "В игру";
				}else{
					GameObject.Find("Skip").GetComponent<Text>().text = "Play";
				}
			}
		}else{
			PlayGame();
		}
	}
	AudioSource audio;
	public void PlayMouseEnter(){
        audio.PlayOneShot(SoundMouseEnter);
    }
    public void PlayMouseClick(){
        audio.PlayOneShot(SoundMouseClick);
    }
    
	IEnumerator WriteMessage(string Message){
        HistoryText.text = "";
        char[] sentense = Message.ToCharArray();
        for(int i=0;i<sentense.Length; i++){
            HistoryText.text += sentense[i];
            if((i == sentense.Length-1)&&(idText == arrayString.Length -1)){
            	HistoryText.text += "<color=#ff0000ff>Standard Legend!</color>";
            }
            yield return null;
        }
    }
    public void PlayGame(){
        FadeInOut.sceneStarting = false;
		FadeInOut.nextLevel = "Level1";
	    FadeInOut.sceneEnd = true;
	}
}
