using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhyDeath : MonoBehaviour {

	public string CauseOfDeath;
	public Text HistoryText;
    bool end = false;
    public int idText;
    public GameObject ButtonMainMenu;
    public GameObject ButtonRestart;
	public int NumLanguage;
	string[] arrayString;
	// Use this for initialization
	void Start () {
		if(HistoryText == null) {Destroy(this);} else{
			HistoryText = HistoryText.GetComponent<Text>();
			CauseOfDeath = Information.Instance.CauseOfDeath;
			NumLanguage = Information.Instance.LanguageNumber;
			ChangeInformation();
			StartCoroutine(WriteMessage(arrayString[0]));
			if(idText == arrayString.Length -1){
				end = true;
				GameObject.Find("ButtonSkip").SetActive(false);
				ButtonRestart.SetActive(true);
				ButtonMainMenu.SetActive(true);
			}
		}
	}
	void ChangeInformation(){
		switch (CauseOfDeath){
			case "Guy":
				if(NumLanguage == 0){
					int num = Random.Range(0,3);
					switch(num){
						case 0:
							arrayString = new string[]{"И героя убивает пугало...\n","Неудивительно, у него же палка в жопе","Хотя у всех бывают ошибки..."};
							break;
						case 1:
							arrayString = new string[]{"Какое злое пугало!\n","Докапалось же!"};
							break;
						case 2:
							arrayString = new string[]{"Смерть от пугала?","Но он же из соломы!", "Сожжём его!"};
							break;		
					}
				}else{
					arrayString = new string[]{"And the hero is killed by\na scarecrow...\nReally? ","Some kind of weak..."," Although everyone\nhas mistakes..."};
				}
				break;
			case "Sceleton":
				if(NumLanguage == 0){
					arrayString = new string[]{"Скелет не самым ловким\nдвижением разрезает\nгероя на куски","Будь аккуратен с ними"};
				}else{
					arrayString = new string[]{"The skeleton is not the most\ndeft movement cuts\nthe hero into pieces", "Be careful with them"};
				}
				break;
			case "Rogue":
				if(NumLanguage == 0){
					int num = Random.Range(0,3);
					switch(num){
						case 0:
							arrayString = new string[]{"Гоп-стоп оказался опаснее\nчем я думал","Надо подкачаться и навалять\n этому гопарю или дома посидеть?"};
							break;
						case 1:
							arrayString = new string[]{"Эти разбойники звери какие-то!","А я ведь был готов уже отдать все свои деньги"};
							break;
						case 2:
							arrayString = new string[]{"Я думал они чтят Робин Гуда", "Нужно было брать меньше монет с собой"};
							break;	
					}		
				}else{
				arrayString = new string[]{"Gop-stop turned out to be\nmore dangerous than I thought","Should we pump up and\npitch this gangster or\nsit at home?"};
				}
				break;
			case "Arrow":
				if(NumLanguage == 0){
				arrayString = new string[]{"В его ахиллесову\nпятую точку попала стрела...","Это не круто как-то,\nнадо переписать этот кусок"};
				}else{
				arrayString = new string[]{"An arrow hit his\nAchilles' fifth point... "," It's not cool somehow,\nthis part needs\nto be rewritten"};

				}
				break;
			case "Bonfire":
				if(NumLanguage == 0){
				arrayString = new string[]{"Он горел как Жанна Дарк","Пара мальчуганов\nуспело пожарить сардельки с хлебом,\nпока он догорал"};
				}else{
				arrayString = new string[]{"It burns like Jeanne d'Arc","A couple of boys\nmanaged to fry sausages,\nwhile he was burning down"};

				}
				break;
			case "Capitan":
				if(NumLanguage == 0){
				arrayString = new string[]{"Наш герой проиграл одному\nиз сильнейших бойцов","Ну не так уж и позорно,\nно как же принцесса?"};
				}else{
				arrayString = new string[]{"Our hero lost to one\nof the strongest fighters"," Well, not so disgraceful,\nbut what about the princess?"};

				}
				break;
			case "CapitanShooters":
				if(NumLanguage == 0){
				arrayString = new string[]{"Штык-нож в печень, никто не вечен","Думаю герою\nнадо найти щит где-нибудь", "Ну или пушку!\nЧто? Нет пушки? Печалька:("};
				}else{
				arrayString = new string[]{"Bayonet in the liver, no one is forever"," I think the hero needs to\nfind a shield somewhere"," Well, or a gun!\nWhat? No guns?\nSadness : ("};

				}
				break;
			case "Danger":
				if(NumLanguage == 0){
					int num = Random.Range(0,3);
					switch(num){
						case 0:
							arrayString = new string[]{"Герой погиб жалкой смертью","Даже комментировать не хочу"};
							break;
						case 1:
							arrayString = new string[]{"Эти грёбанные разбойники наставили своих ловушек! Так нечестно!"};
							break;
						case 2:
							arrayString = new string[]{"Нужно быть осторожнее в следующий раз"};
							break;
					}
				}else{
				arrayString = new string[]{"The hero died\na pitiful death ","I do not even\nwant to comment"};

				}
				break;
			case "DeadKing":
				if(NumLanguage == 0){
				arrayString = new string[]{"Герой сражался достойно...","Однако, пал от руки\nскелета - старика, \nкоторый только проснулся\nи не размялся"};
				}else{
				arrayString = new string[]{"The hero fought\nwith dignity..."," However, he fell from\nthe hand of a skeleton\n- an old man who just woke up\nand did not warm up"};

				}
				break;
			case "DeadPriest":
				if(NumLanguage == 0){
				arrayString = new string[]{"Герой отправлен\nв ад не святым отцом", "Возможно, он как\nбратья Винчестеры\nвернётся обратно"};
				}else{
				arrayString = new string[]{"The hero is\nsent to hell\nby an unholy father","Perhaps he,\nlike the Winchesters brothers,\nwill be back"};

				}
				break;
			case "DragonMouse":
				if(NumLanguage == 0){
				arrayString = new string[]{"Огромная ящерица\nпрокусила кольчугу","Кусь..."};
				}else{
				arrayString = new string[]{"The huge lizard has bitten\nthrough the chainmail"};

				}
				break;
			case "DragonHand":
				if(NumLanguage == 0){
				arrayString = new string[]{"Бдышь!","Огромная ящерица смахнула\nгероя как муху с рукава"};
				}else{
				arrayString = new string[]{"A huge lizard brushed away a\nhero like a fly from a sleeve"};

				}
				break;
			case "FireBall":
				if(NumLanguage == 0){
					int num = Random.Range(0,3);
					switch(num){
						case 0:
							arrayString = new string[]{"Шальной огненный шар\nпролетал мимо"};
							break;
						case 1:
							arrayString = new string[]{"Огненный шар пожрал последнего рыцаря..."};
							break;
						case 2:
							arrayString = new string[]{"Вот это был ад конечно! От такого горящего шара не убежать"};
							break;
					}
				}else{
				arrayString = new string[]{"Crazy fireball flying past"};

				}
				break;
			case "HangManRun":
				if(NumLanguage == 0){
				arrayString = new string[]{"Здоровяк сшиб с ног\nнашего героя","Он потерял сознание\nи был не очень красиво убит"};
				}else{
				arrayString = new string[]{"The big man knocked down\nour hero's feet","He lost consciousness\nand was not very\nnicely killed"};

				}
				break;
			case "Wolf":
				if(NumLanguage == 0){
				arrayString = new string[]{"Милая собачка покусала","Да, да, даже герои\nбоятся собак"};
				}else{
				arrayString = new string[]{"Cute dog bitten","Yes, yes,\neven heroically dogs"};

				}
				break;
			case "HangMan":
				if(NumLanguage == 0){
				arrayString = new string[]{"Топор прошел сквозь\nголову героя как\nгорячий нож сквозь масло"};
				}else{
				arrayString = new string[]{"The ax passed through\nthe head of the hero like\na hot knife through butter"};

				}
				break;
			case "Spirit":
				if(NumLanguage == 0){
				arrayString = new string[]{"Призрак проводил в свой мир"};
				}else{
				arrayString = new string[]{"The ghost spent in his world"};

				}
				break;
			case "Leve6_Boss_Dialog":
				if(NumLanguage == 0){
				arrayString = new string[]{"Рустер отказался от жизни,\nи стал рыцарем смерти.","Он методично принялся за дело.\nИ оказался лучшим из лучших.","Разбойники, зайчики,\nптички, цветочки, трава…\n","Все переставало жить\nпосле встречи с ним.\nДракона Рустер убил одной левой.","Принцесса молила его о пощаде,\nно Рустер\nпронзил ее сердце мечом…","Так же пали монах Леопольд,\nкузница Меган, крестьяне,\nАрчи…","Они кричали,\nчто он их друг и лорд,\nно он уже не был им…","Он уничтожил все, что жило.\nСтарый Король сел на\nтрон в Столице.","И наступила эпоха\nвеликой тишины..."};
				}else{
				arrayString = new string[]{"Rooster gave up life and\nbecame a death knight.","He methodically set to work.\nAnd he turned out to be the best of the best. ","Rogues, bunnies,\nbirds, flowers,\ngrass... ","Everything stopped living\nafter meeting him. Dragon Rooster\nkilled with one hand."," The princess begged him for\nmercy, but Rooster pierced her\nheart with a sword...","Monk Leopold, forge Megan,\npeasants, Archie..."," They shouted that he\nwas their friend and lord,\nbut he was no longer him...","He destroyed everything\nthat lived. The Old King sat\ndown natron in the Capital."," And the era of great\nsilence began..."};

				}
				break;
			case "Leve8_Boss_Dialog":
				if(NumLanguage == 0){
				arrayString = new string[]{"Вернувшись с гор,\nРустер насильно женил\nна себе Принцессу.","После он дождался армию Дракона\n– и после долгой и кровавой\nвойны захватил Столицу.","Когда туда вернулся Дракон,\nРустер ждал его на троне.","Жажда власти переполняла обоих,\nи они сошлись в смертельной\nсхватке за власть и богатство…","Они бились\nнесколько дней и ночей\nподряд","Но никто из них так\nи не победил – оба нанесли\nдруг-другу смертельные раны","И умерли в огромной\nлуже крови…","И до сих пор поется песня\nв Королевстве – о схватке\nдвух Драконов…","Конец."};
				}else{
				arrayString = new string[]{"After returning from\nthe mountains, Rooster forcibly\nmarried the Princess to himself.","Then he waited for the\nDragon Army - and after a long and\nbloody war, he captured the Capital.","When the Dragon returned,\nRooster was waiting for\nhim on the throne.","The thirst for power\noverwhelmed both of them, and\nthey came together in a mortal\nfight for power and wealth..."," They fought for several\ndays and nights in a row","But none of them won\n they both inflicted fatal\nwounds on each other","And they died in a huge\npool of blood ..."," And the song is still sung\nin the Kingdom - about\nthe clash of two Dragons...","The End."};

				}
				break;
			case "Level3_Start_Dialog":
				if(NumLanguage == 0){
				arrayString = new string[]{"Игрок не хотел\nследовать сюжету","Какой жадный игрок..."};
				}else{
				arrayString = new string[]{"The player did not want\nto follow the scenario", "What a greedy player..."};

				}
				break;
			case "Level3_Boss_Dialog":
				if(NumLanguage == 0){
				arrayString = new string[]{"Рустер убил плененого Короля\nи стал цареубийцой\nи клятвопреступником.","Однако, разбойники все равно\nего предали и убили ради монет\nкоторые он так долго собирал."};
				}else{
				arrayString = new string[]{"Rooster killed the captive\nKing and became the regicide\nand oath-breaker.","However, the robbers still\nbetrayed and killed him for\nthe coins he had collected\nfor so long."};

				}
				break;
			case "Leve5_Boss_Dialog":
				if(NumLanguage == 0){
				arrayString = new string[]{"После смерти, Рустера\nвоскресили некроманты,\nи он стал рыцарем – зомби.","По неизвестным причинам\nон был очень туп","Гораздо тупее\nсвоих мертвых собратьев.","Когда Старый Король\nповел их штурмовать Столицу,\nРустер-зомби упал в яму.","И не смог из нее выбраться.","И непонятно, было\nэто удачей или неудачей.","Ведь с гор спустился Дракон,\nи сжег всех мертвецов.","Кроме Рустера.","А Рустера Дракон посадил\nна цепь и сделал\nсвоим рабом – шутом.","В общем-то,\nконец."};
				}else{
				arrayString = new string[]{"After death, Rooster\nwas resurrected by necromancers,\nand he became a zombie knight."," For unknown reasons,\nhe was very stupid","Much dumber than his\ndead counterparts.","When the Old King led\nthem to storm the Capital,\nRooster-zombies fell\ninto a hole."," And I could not\nget out of it."," And it is not clear\nwhether it was a success\nor a failure. "," After all, the Dragon\ndescended from the mountains\nand burned all the dead. "," Besides Rooster."," And Rooster the Dragon\nplanted on the chain and\nmade his jester."," In general,\nthe end."};

				}
				break;
			case "Bullet":
				if(NumLanguage == 0){
				arrayString = new string[]{"Герой не был\nактером индийского кина\nпоэтому пули ловить руками не умел"};
				}else{
				arrayString = new string[]{"The hero was not an actor\nof Indian kin so he\ncouldn’t catch bullets"};

				}
				break;	
			case "Magic":
				if(NumLanguage == 0){
				arrayString = new string[]{"Это магия!"};
				}else{
				arrayString = new string[]{"It\'s a magic!"};

				}
				break;
			case "TempCold":
				if(NumLanguage == 0){
			arrayString = new string[]{"Вы замерзли, печалька:("};
				}else{
				arrayString = new string[]{"You are cold,\nsadness : ("};

				}
				break;
			case "TempHot":
				if(NumLanguage == 0){
				arrayString = new string[]{"Он горел как Жанна Дарк","Пара мальчуганов\nуспели пожарить\nсарделки с хлебом,\nпока он догорал"};
				}else{
				arrayString = new string[]{"It burns like Jeanne d'Arc","A couple of boys\nmanaged to fry sausages,\nwhile he was burning down"};
				}
				break;
			case "Water":
				if(NumLanguage == 0){
				arrayString = new string[]{"Вы утонули\nбревна какое-то сложное место!"};
				}else{
				arrayString = new string[]{"You drowned.\nLogs is a\ntricky place!"};

				}
				break;	
			default:
				if(NumLanguage == 0){
				arrayString = new string[]{"Вскрытие показало\n- больной спал"};
				}else{
				arrayString = new string[]{"An autopsy revealed\n- the patient was asleep"};

				}
				break;																												
		}
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
				ButtonRestart.SetActive(true);
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
}
