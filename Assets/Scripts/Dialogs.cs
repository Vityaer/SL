using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using DG.Tweening;

public class Dialogs : MonoBehaviour {
    private static Dialogs instance;
    public static Dialogs Instance{get => instance;}
	public Canvas quitMenu;
	public Text Dialog;
	public Image imgRooster;
	public Image imgRogue;
    public Image imgGuy;
    public Image imgBerserk;
    public Image imgArchi;
    public Image imgDragon;
    public Image imgLeo;
    public Image imgNecromanser;
    public Image imgSmith;
    public Image imgArcher;
    public Image imgSceleton;
    public Image imgShaman;
    public Image imgTeacher;
    public Image imgTeacherLeft;
    public Image imgGirlIdle;
    public Image imgGirlWorry;
    public Image imgRoosterFlipX;
    public Image imgWarrior;
    public Image imgKing;
    public Image imgCapitan;
    public Image imgDeadPriest;
    public Image imgDeadKing;
    public Image imgHanger;
    public Image imgPrincess; 
    bool leve5_diplomat = false;
    bool leve4_toulet = false;
    bool theEnd = false;
    public int i = 0;
    private int i_len = 1;
    public int start;

    public Information information;
    public int LanguageNumber;
    private String outText;

    string[] arrayString = new string[] {"1","2"};
	string[] arrayWho = new string[]{"1","2"};
    string[] arrayOption = new string[]{"1","2"};
    bool Options;
    bool GameOver;
    private int[] DialogsIndex = new int[100];
    int Index = 0;
    bool flag = true;
    bool trigger = false; 
    public bool EndMessage = false;
    bool _skip = false; 
    private Text skipTextComponent;
    void Awake(){
        instance = this;        
    }
    void Start () {
        skipTextComponent = GameObject.Find("Skip").GetComponent<Text>();
        quitMenu = GameObject.Find("Dialogs").GetComponent<Canvas>();
        Dialog = GameObject.Find("TextDialog").GetComponent<Text>();   

        information = Information.Instance;
        quitMenu.enabled = false;
        quitMenu = quitMenu.GetComponent<Canvas>();
    	Dialog = Dialog.GetComponent<Text>();

        LanguageNumber = (information != null) ? information.LanguageNumber : 0;
        DialogsIndex   = (information != null) ? information.DialogsIndex : new int[100];
        if(LanguageNumber == 0){
            skipTextComponent.text = "далее - \"E\"";
        }else{
            skipTextComponent.text = "next - \"E\"";
        }
	}
	
	void Update () {
        if(_skip){
            if(skipTextComponent.enabled){
                if (Input.GetKeyUp(KeyCode.E)){
                    skip();
                }        
            }else{
                if(Options){
                    if(Input.GetKeyUp(KeyCode.Alpha1) || Input.GetKeyUp(KeyCode.Keypad1)){
                        createDialog(arrayOption[0]);
                    }
                    if(Input.GetKeyUp(KeyCode.Alpha2) || Input.GetKeyUp(KeyCode.Keypad2)){
                        createDialog(arrayOption[1]);
                    }
                    if(arrayOption.Length == 3){
                        if(Input.GetKeyUp(KeyCode.Alpha3) || Input.GetKeyUp(KeyCode.Keypad3)){
                            createDialog(arrayOption[2]);
                        }   
                    }
                }else{
                    if (Input.GetKeyUp(KeyCode.E)){
                        if(skipTextComponent.enabled){
                            skip();
                        }
                    }
                }
            }
        }
    }
	public void skip(){
        i_len = arrayString.Length;
            i++;
            Events();
            if(GameOver && (i == i_len)){
                Application.LoadLevel("GameOver");
            }
            if(!Options){
                if(i<i_len){
                    StartWrite(arrayString[i]);
                    WhoSpeakAvater();
                }else{
                    DialogsOff();
                    if(theEnd){
                        ObserverDialog(true);
                        FadeInOut.nextLevel = "TheEnd";
                        FadeInOut.sceneEnd = true;
                    }
                }
            }else{
                skipTextComponent.enabled = false;
                if(i == i_len-1) EndMessage = true;
                if(i < arrayString.Length){
                    StartWrite(arrayString[i]);
                }else{
                    DialogsOff();
                }
                WhoSpeakAvater();
            }
        if(leve4_toulet){
            skipTextComponent.enabled = true;
            leve4_toulet = false;
        }    
    }
    public delegate void Del(bool isDialog);
    public Del dels;
    public void RegisterOnDialog(Del d){dels += d; }
    public void UnRegisterOnDialog(Del d){ dels -= d; }
    private void ObserverDialog(bool isDialog){
        if(dels != null) dels(isDialog);
    }
//more more dialogs    
	public void OpenDialog(string nameObject){
    	if (nameObject == "Dialog_Level1_Start"){
            if(!trigger){
                trigger = true;
                start = 4;
                ReadInArrayDialogs();
                if(flag){
                    if(LanguageNumber == 0){
                        arrayString = new string[]{"Я приветствую тебя, игрок!\n Добро пожаловать в мой мир!","Пойдем, я все покажу!\n Жми « " + GameObject.Find("TextButtonRight").GetComponent<Text>().text + " »"};
                    }
                    if(LanguageNumber == 1){
                        arrayString = new string[]{"HI greet you, player!\n Welcome to my world!","Let's go, I'll show it!\n Press « " + GameObject.Find("TextButtonRight").GetComponent<Text>().text + " »"};
                    }
                        arrayWho = new string[]{"Rooster","Rooster"};
                        WriteInArrayDialogs();
                }
                trigger = false;
            }
        }
        if (nameObject == "Dialog_Level1_Stump"){
            if(!trigger){
                trigger = true;
                start = 1;
                ReadInArrayDialogs();
                if(flag){
                    if(LanguageNumber == 0){
                    arrayString = new string[]{"Б****!!! Арчи опять не выкорчевал\n гребаный пень!","ЭТО ДВУХМЕРНОЕ ПРОСТРАНСТВО!!!\n Я ЕГО НЕ ОБОЙДУ, АРЧИ!!!\nЛадно, не пугайся.","Жми на « " + GameObject.Find("TextButtonJump").GetComponent<Text>().text + " » и одновременно\n« " + GameObject.Find("TextButtonRight").GetComponent<Text>().text + " », и я\n перепорхну его как бабочка!"};
                    }
                    if(LanguageNumber == 1){
                        arrayString = new string[]{"F***!!! Archie again did not uproot\n a fucking stump!","THIS IS A TWO-DIMENSIONAL SPACE !!!\n I WILL NOT KEEP IT, ARCHIE !!!\n Okay, do not be scared.","Press on « " + GameObject.Find("TextButtonJump").GetComponent<Text>().text + " » and at the same time \n« " + GameObject.Find("TextButtonRight").GetComponent<Text>().text + " », and I \n flutter it like a butterfly!"};
                    }
                    arrayWho = new string[]{"Rooster","Rooster","Rooster"};
                    WriteInArrayDialogs();
                }
                trigger = false;
            }
        }
        if (nameObject == "Dialog_Level1_Smith"){
            if(!trigger){
                trigger = true;
                start = 3;
                ReadInArrayDialogs();
                if(flag){
                    if(LanguageNumber == 0){
                    arrayString = new string[]{"Меган! Отлично выглядишь! ","Мой лорд!","Ну что, мои новые тяжелые латы и двуручный меч готовы?","Мечтайте! Чтобы починить ваш щит и меч после турнира,\nмне придется отпилить и перековать часть наковальни. Стали и железа нет, и денег нет…","Что верно то верно.\nДенег нет, но вы держитесь!\nПопытаюсь это исправить…","Вы встретите очень много врагов. У них будут разные доспехи.\nВполне возможно, и тяжелые. Но суть не в тяжести!","Легкий кожанный доспех даст вам скорость, и от пары ЛЮБЫХ ударов точно увернетесь.","Кольчуга и шлем, что были у вас на турнире, хороший вариант. Не слишком медленно, можно чередовать с разным оружием,\nда и защита неплохая!","Ну а тяжелые латы – это медленно. Но зато зверей и небольшого оружия можно не боятся. Не пробъют вовсе. Будете как танк. Крутая тема.","Доспехи на вас сейчас.. эээ… вы без трусов и шлема, в принципе, неплохой вариант. Ничто не сковывает движение… но лучше найдите хоть какую-нибудь одежду.","Ясно! Спасибо Меган! Постараюсь добыть железа!", "Удачи, рыцарь! Твоя Принцесса тебя ждет!\n Ах, да, чуть не забыла, чтобы поднять оружие, жми « " + GameObject.Find("TextButtonGetUp").GetComponent<Text>().text + " »"};
                    }
                    if(LanguageNumber == 1){
                        arrayString = new string[]{"Megan! You look great!", "My lord!", "Well, my new heavy armor and two-handed sword are ready?", "Dream! To fix your shield and sword after the tournament, I will have to saw and forge part of the anvil. Steel and iron are not present, and there is no money ..."," What's true is true.\nThere is no money, but you hold on!\nI will try to fix it ... "," You will meet a lot of enemies. They will have different armor. \nIt is possible, and But the essence is not in weight!","Light leather armor will give you speed, and from a couple of ANY blows will definitely dodge."," Chain and helmet that were in your tournament, the chorus It's not too slow, you can alternate with different weapons, \nand protection is not bad! "," Well, heavy armor is slow, but you can not be afraid of animals and small weapons. . "," Armor is on you now .. uh ... you are without panties and a helmet, in principle, a good option. There is nothing to hold the movement ... but better find at least some clothes.", "Thank you, Megan!"," Good luck, knight! Your Princess is waiting for you! \n Oh, yes, I almost forgot to pick up weapons, press « " + GameObject.Find("TextButtonGetUp").GetComponent<Text>().text + " »"};
                    }
                    arrayWho = new string[]{"Rooster","Smith","Rooster","Smith","Rooster","Smith","Smith","Smith","Smith","Smith","Rooster","Smith"};
                    WriteInArrayDialogs();
                }
                trigger = false;
            }    
        }
        if (nameObject == "Dialog_Level1_Teacher"){
            if(!trigger){
                trigger = true;
                start = 5;
                ReadInArrayDialogs();
                if(flag){
                    if(LanguageNumber == 0){
                    arrayString = new string[]{"Это мой тренер, сэр Черная Акула.\nОн бывший пират. Был лучшим на рутрекере.\nДоброе утро, тренер!","Йо-хо-хо!!! Доброе, сэр!\n Как вы?","Отлично!","Акула, мне надо потренироваться перед опасным квестом. Где мое любимое пугало для битья?","Вон оно, никуда не убежало!"};
                    }
                   if(LanguageNumber == 1){
                        arrayString = new string[]{"This is my trainer, Sir Black Shark.\nHe is a former pirate. He was the best on the rutreker.\nGood morning, coach!"," Yo-ho-ho! Kind, sir!\nHow are you? "," Good! "," Shark, I need to practice in front of a dangerous quest.\nWhere is my favorite scarecrow?", "There it is, it did not escape anywhere!"};
                    }
                    arrayWho = new string[]{"Rooster","Teacher","Rooster","Rooster","Teacher"};
                    WriteInArrayDialogs();
                }
                trigger = false;
            }   
        }
        if (nameObject == "Dialog_Level1_Fight1"){
            if(!trigger){
                trigger = true;
                start = 6;
                ReadInArrayDialogs();
                if(flag){
                    if(LanguageNumber == 0){
                    arrayString = new string[]{"Жми кнопку « " + GameObject.Find("TextButtonStrike").GetComponent<Text>().text + " », и я ему втащу!"};
                    }
                   if(LanguageNumber == 1){
                        arrayString = new string[]{"Press « " + GameObject.Find("TextButtonStrike").GetComponent<Text>().text + " » and I'll hit it!"};
                    }
                    arrayWho = new string[]{"Rooster"};
                    WriteInArrayDialogs();
                }
                trigger = false;
            }
        }
        if (nameObject == "Dialog_Level1_Fight2(Clone)"){
            if(!trigger){
                trigger = true;
                start = 7;
                GameObject.FindWithTag("Player").GetComponent<Animator>().Play("Idle");
                ReadInArrayDialogs();
                if(flag){
                    if(LanguageNumber == 0){
                    arrayString = new string[]{"ЧТО ЗА?!","Нет времени думать, сэр!\nУклоняйся!\nУклоняйся и БЕЙ!!!","Пришла пора отомстить за каждый удар!!!"};
                    }
                   if(LanguageNumber == 1){
                        arrayString = new string[]{"WTF?", "There's no time to think, sir!\n Dodger!\nDodger and hit!!!", "It's time to take revenge for every stroke!!!"};
                    }
                    arrayWho = new string[]{"Rooster","Teacher","Guy"};
                    WriteInArrayDialogs();
                }
                trigger = false;
            }   
        }
        if (nameObject == "Dialog_Level1_Fight_Finish(Clone)"){
            if(!trigger){
                trigger = true;
                 start = 8;
                ReadInArrayDialogs();
                if(flag){
                    GameObject player = GameObject.FindWithTag("Player");
                    player.GetComponent<RoosterScript>().isFacingRight = false;
                    player.GetComponent<RoosterScript>().Flip();
                    player.GetComponent<Animator>().SetBool("Attack", false);
                    player.GetComponent<RoosterScript>().Attack = false;
                    if(LanguageNumber == 0){
                    arrayString = new string[]{"Тренер, это что сейчас было?","Черная магия… Тебе предстоит еще не одно столкновение с подобным. Используй разное оружие, рыцарь, не зацикливайся на чем-то одном. Комбинируй!","Помни все, чему я тебя учил. Особенно о том, чтобы закрыться щитом нужно нажать кнопку « " + GameObject.Find("TextButtonBlock").GetComponent<Text>().text + " »","Я понял, учитель!","Удачи, Рустер! Ты мой лучший ученик!","Еще бы. Я твой единственный ученик."};
                    }
                   if(LanguageNumber == 1){
                        arrayString = new string[]{"Coach, what was that now?", "Black magic ... You have more than one encounter with this one, use different weapons, knight, do not focus on one thing. Combine!","Remember everything that I especially to close the shield you need to press the « " + GameObject.Find("TextButtonBlock").GetComponent<Text>().text + " »"," I understood! ","Good luck, Rouster, you are my best pupil! "," Yes, I am your only pupil."};
                    }
                    arrayWho = new string[]{"RoosterFlipX","TeacherLeft","TeacherLeft","RoosterFlipX","TeacherLeft","RoosterFlipX"};
                    WriteInArrayDialogs();
                }
                trigger = false;
            }
        }
        if (nameObject == "Dialog_Level1_People1"){
            if(!trigger){
                trigger = true;
                 start = 10;
                ReadInArrayDialogs();
                if(flag){
                    if(LanguageNumber == 0){
                    arrayString = new string[]{"Арчи…","Сэр, я вчера начал выкорчевывать пень.\nПоявились разработчики, и сказали, что он должен остаться на месте.\nСказали, «Он пригодится для обучения игрока». Что я мог поделать?","Понятно… Арчи, а куда подевалось твое пугало?","Пропало ночью, сэр. Но его не украли…\nНе думайте, что я брежу… Но я бъюсь об заклад,\nчто оно само убежало ночью. Я видел его следы! Вам показать?","Все в порядке, я верю. Какие еще новости?","Разбойники, сэр. Они обнаглели настолько, что приходят по ночам прямо в деревню, воруют и грабят все и всех подряд. Но это еще было терпимо…","Пока пару дней тому не похитили дочь нашего кузнеца Ниссона. Лиам сам отправился спасать ее… Но боюсь, у него нет шансов!","Бесславные ублюдки… Я постараюсь ее спасти!", "Лорд, будьте аккуратны, особенно с кострами."};
                    }
                   if(LanguageNumber == 1){
                        arrayString = new string[]{"Archie ...","Sir, I started to uproot the stump yesterday.\nThe developers appeared and said that he should stay put.\nSaid, «It will come in handy for the player's training.»\nWhat could I do?", "Ok... Archie and where did your scarecrow go?","It disappeared at night, sir, but it was not stolen ...\nDo not think that I'm shivering ... But I'm betting that it itself ran away at night.\nI saw his tracks, show you?" , "All right, I believe.\nWhat other news? "," Robbers, sir. They became so insolent that they came straight to the village at night, stealing and robbing everyone and everyone. "," As long as a couple of days ago not kidnapped daughter of our blacksmith Neeson. Liam himself to rescue her ... But I'm afraid he has no chance","Inglourious Basterds ... I will try to save her! ", "Be careful with bonfire!"};
                    }
                    arrayWho = new string[]{"Rooster","Archi","Rooster","Archi","Rooster","Archi","Archi","Rooster","Archi","Rooster", "Archi"};
                    WriteInArrayDialogs();
                }
                trigger = false;
            }
        }
        if (nameObject == "Dialog_Level1_People2"){
            if(!trigger){
                trigger = true;
                 start = 11;
                ReadInArrayDialogs();
                if(flag){
                    if(LanguageNumber == 0){
                    arrayString = new string[]{"Сэр! Мы ставили ловушку на негодяев. Она у крайней березы, за пнем, с монеткой в виде приманки. Не угодите в нее сами!","Спасибо! А теперь пойди и выкорчуй пень Арчи! Разработчиков я беру на себя!"};
                    }
                   if(LanguageNumber == 1){
                        arrayString = new string[]{"Sir, we set a trap for the scoundrels, it's at the birch, behind the stump, with a coin in the form of bait.\nDo not get yourself into it! "," Thank you, and now go and root Archie's stump! I take care of the developers! "};
                    }
                    arrayWho = new string[]{"Archi","Rooster"};
                    WriteInArrayDialogs();
                }
                trigger = false;
            }
        }
        if (nameObject == "Dialog_Level1_River"){
            if(!trigger){
                trigger = true;
                 start = 12;
                ReadInArrayDialogs();
                if(flag){
                    if(LanguageNumber == 0){
                    arrayString = new string[]{"Здешние реки наполнены пираньями!\nБлаго крестьяне придумали прыгать по бревнам."};
                    }
                   if(LanguageNumber == 1){
                        arrayString = new string[]{"The rivers here are filled with piranhas!\nThe peasants have thought up to jump on logs."};
                    }
                    arrayWho = new string[]{"Rooster"};
                    WriteInArrayDialogs();
                }
                trigger = false;
            }
        }

        if (nameObject == "Dialog_Level2_WelcomeToForest"){
            if(!trigger){
                trigger = true;
                 start = 16;
                ReadInArrayDialogs();
                if(flag){
                    if(LanguageNumber == 0){
                    arrayString = new string[]{"Добро пожаловать в Королевский лес, игрок. Когда – то он был настолько хорошо, что в нем жили эльфы…", "Пока туда не пришли лучшие представители человечества, прячась от королевского правосудия. Он кишмя – кишит разбойниками.","Королю слишком далеко сюда добираться, чтобы наводить порядок,\n а местные лорды предпочитают междоусобные войны друг с другом, и мало того, они даже сотрудничают с бандитами."};
                    }
                   if(LanguageNumber == 1){
                        arrayString = new string[]{"Welcome to the Royal Forest, a player, once it was so good that the elves lived in it ...", "Until the best representatives of humanity came here, hiding from the royal justice.","Far to get here to clean up, \n and local lords prefer internecine wars with each other, and moreover, they even cooperate with bandits. "};
                    }
                    arrayWho = new string[]{"Rooster","Rooster","Rooster"};
                    WriteInArrayDialogs();
                }
                trigger = false;
            }
        }

        if (nameObject == "Dialog_Level2_Gop"){
            if(!trigger){
                trigger = true;
                 start = 17;
                ReadInArrayDialogs();
                if(flag){
                    if(LanguageNumber == 0){
                    arrayString = new string[]{"Гоп-стоп, петушара!","Сейчас тебе член отрежу!","Хочешь фокус покажу? Вот этот меч.Сейчас окажется.У тебя в голове. Но я пацифист – и дам тебе шанс сдаться.","Пошел ты, петух!!!"};
                    }
                   if(LanguageNumber == 1){
                        arrayString = new string[]{"Stop, cock!", "Now I will cut off your dick!", "Do you want to see the focus? This is the sword. Now it will turn out to be in your head.\nBut I'm a pacifist and I'll give you a chance to surrender."," Fuck you, cock!!!"};
                    }
                    arrayWho = new string[]{"Rogue","Rogue","Rooster","Rogue"};
                    WriteInArrayDialogs();
                }
                trigger = false;
            }
        }
        if (nameObject == "Dialog_Level2_Archer"){
            if(!trigger){
                trigger = true;
                 start = 19;
                ReadInArrayDialogs();
                if(flag){
                    if(LanguageNumber == 0){
                    arrayString = new string[]{"А ну-ка вспомни, петушня, кто был самым крутым во Властелине колец? И еще более крутым в Хоббите? Подсказка: «Пираты карибского моря».","Режиссер.","Леголас, идиот! Его играл Орландо Блум, я дал тебе подсказку! Ты тупица, я прикончу тебя как раненую россомаху!!!"};
                    }
                   if(LanguageNumber == 1){
                       arrayString = new string[]{"Well, remember, cock, who was the coolest in the Lord of the Rings? And even steeper in the Hobbit? Hint: «Pirates of the Caribbean Sea.»", "Director.", "Legolas, you are idiot! He played Orlando Bloom, I gave you a clue! You stupid, I'll kill you like a wounded wolverine!!!"};
                    }
                    arrayWho = new string[]{"Archer","Rooster","Archer"};
                    WriteInArrayDialogs();
                }
                trigger = false;
            }
        }
        if (nameObject == "Dialog_Level2_Archer_Finish(Clone)"){
            if(!trigger){
                trigger = true;
                start = 20;
                ReadInArrayDialogs();
                if(flag){
                    if(LanguageNumber == 0){
                    arrayString = new string[]{"Идиот это ты, если забыл про фильм «Люди Х»"};
                    }
                   if(LanguageNumber == 1){
                        arrayString = new string[]{"Idiot is you, if you forgot about the film «X-men»"};
                    }
                    arrayWho = new string[]{"Rooster"};
                    WriteInArrayDialogs();
                }
                trigger = false;
            }
        }
        if (nameObject == "Dialog_Level2_Shaman(Clone)"){
            if(!trigger){
                trigger = true;
                start = 21;
                ReadInArrayDialogs();
                if(flag){
                    if(LanguageNumber == 0){
                    arrayString = new string[]{"Отомстите за меня, волчары!!!","Проклятье! Ладно, это не люди.\nА значит, я не нарушу рыцарских обетов, если побегу от них.\nПричем очень быстро!!!"};
                    }
                   if(LanguageNumber == 1){
                        arrayString = new string[]{"Get revenge for me, wolves!!!", "Damn, okay, it's not people.\nAnd so I will not break the knight vows if I run from them.\nWell, very quickly!!!"};
                    }
                    arrayWho = new string[]{"Shaman","Rooster"};
                    WriteInArrayDialogs();
                }
                trigger = false;
            }
        }
        if (nameObject == "Dialog_Level2_Fight_Guy_Vs_Rogue"){
            if(!trigger){
                trigger = true;
                start = 22;
                ReadInArrayDialogs();
                if(flag){
                    if(LanguageNumber == 0){
                    arrayString = new string[]{"Парни, я ни хрена не понимаю!!! Какого черта эти мешки с песком ожили?! Потрениться хотел, и на тебе!","Пришла пора отомстить за каждый удар, чернь!"};
                    }
                   if(LanguageNumber == 1){
                        arrayString = new string[]{"Guys, I do not understand shit! What the hell did these sandbags come to life for? I wanted to practice, and on you!", "It's time to take revenge for every strike, mob!"};
                    }
                    arrayWho = new string[]{"Rogue","Guy"};
                    GameObject.Find("GuyVS").GetComponent<GuyScript>().enabled = true;
                    GameObject.Find("RogueVS").GetComponent<RogueScript>().enabled = true;
                    WriteInArrayDialogs();
                }
                trigger = false;
            }
        }
        if (nameObject == "Dialog_Level2_Woman"){
            if(!trigger){
                trigger = true;
                start = 23;
                ReadInArrayDialogs();
                if(flag){
                    if(LanguageNumber == 0){
                    arrayString = new string[]{"Какая симпатичная! Я хочу ее первый!","Черта с два!!! Я сегодня подстрелил обед, значит, я первый и развлекаюсь!","Давай раз на раз, ближний бой, кулаки?! Поглядим, кто из нас мужчина!!!","Иди ты, придурок! Думаешь, я такой тупой? Слабо на луках?!","А ну разойдись, шпана! Мой ствол понравится этой деве!","Ну мы же с лучником ее добыли…","Раз на раз?!!!","Хорошо, берсерк. Я после тебя…","Благое дело творите, братья! От вас она родит сильных детей, почитающих истинных богов! Я вас благословляю!","Кто-нибудь… Помогите!!!"};
                    }
                   if(LanguageNumber == 1){
                        arrayString = new string[]{"What a pretty one! I want her first!","Today I shot dinner, which means that I am the first to have fun!","Come on once, close fight, fists?! Let's see which of us is a man!!! "," Go, you moron! You think I'm so stupid? Weakly on the bows?","Well, go away, punks! My dick will like this maid!","Well, we got it with an archer...","Let's fight?"," Well, berserk. I am after you ...","You do good work, brothers! From you, she will give birth to strong children who worship true gods! I bless you!","Somebody ... Help !!!"};
                    }
                    arrayWho = new string[]{"Rogue","Archer","Rogue","Archer","Berserk","Rogue","Berserk","Rogue","Shaman","GirlWorry"};
                    WriteInArrayDialogs();
                }
                trigger = false;
            }
        }

        if (nameObject == "Dialog_Level2_Woman2"){
            if(!trigger){
                trigger = true;
                start = 24;
                Options = true;
                ReadInArrayDialogs();
                if(flag){
                    if(LanguageNumber == 0){
                    arrayString = new string[]{"Ты положил кучу моих ребят… Ты хороший воин. Когда-то я тоже служил лорду, и сражался с такими как ты на одной стороне.","Я тебя уважаю, и предлагаю прекратить эту бессмысленную резню. Ты оставляешь нас в покое – мы без боя пропускаем тебя дальше.","А девушка?","Она останется с нами.\nПарни живут в трудных условиях…\nДевочка хоть немного скрасит им жизнь.","1.  Эх, устал мечом махать, а ты не принцесса…\n    Хорошо, делайте что хотите, только вон с моей дороги!!!\n2.  Я рыцарь. Я поклялся защищать слабых, женщин и детей от любого зла.\nЯ не уйду без нее. (бой)"};
                    }
                   if(LanguageNumber == 1){
                        arrayString = new string[]{"You put a bunch of my guys... You're a good warrior, I used to serve as a lord, and I fought like you on the same side.", "I respect you, and I suggest you stop this senseless slaughter. You leave us alone - we without a fight, let's pass you on."," And the girl? "," She will stay with us.\nBoys live in difficult conditions...\nThe girl will at least lighten their lives."," 1. Tired of swinging his sword, and you're not a princess...\nWell, do what you want, just get out of my way!!!\n2. I'm a knight, I vowed to protect the weak, women and children from any evil. \nI will not leave without her. (Battle)"};
                    }
                    arrayOption = new string[]{"Dialog_Level2_Woman2_var_1","Dialog_Level2_Woman2_var_2"};
                    arrayWho = new string[]{"Berserk","Berserk","Rooster","Berserk","Rooster"};
                    WriteInArrayDialogs();
                }
                trigger = false;
            }
        }
        if (nameObject == "Dialog_Level2_Woman_End(Clone)"){
            Information.Instance.OptionGirl = true;
            if(!trigger){
                trigger = true;
                start = 25;
                ReadInArrayDialogs();
                GameObject Player = GameObject.FindWithTag("Player");
                if(Player.GetComponent<RoosterScript>()){
                    if(Player.GetComponent<RoosterScript>().isFacingRight){
                        Player.GetComponent<RoosterScript>().isFacingRight = false;
                        Player.GetComponent<RoosterScript>().Attack = false;
                        Player.GetComponent<RoosterScript>().Flip();
                    }
                } 
                if(Player.GetComponent<RoosterScriptWithShield>()){
                    if(Player.GetComponent<RoosterScriptWithShield>().isFacingRight){
                        Player.GetComponent<Animator>().Play("Idle_left");
                        Player.GetComponent<RoosterScriptWithShield>().isFacingRight = false;
                        Player.GetComponent<RoosterScriptWithShield>().Attack = false;
                    }
                } 
                Player.GetComponent<Animator>().SetBool("Attack", false);
                
                if(flag){
                    if(LanguageNumber == 0){
                    arrayString = new string[]{"Спасибо, мой лорд!","Беги домой, здесь опасно."};
                    }
                   if(LanguageNumber == 1){
                        arrayString = new string[]{"Thank you, my lord!", "Run home, it's dangerous here."};
                    }
                    arrayWho = new string[]{"GirlIdle","RoosterFlipX"};
                    WriteInArrayDialogs();
                }
                GameObject.Find("Girl").GetComponent<SpriteRenderer>().DOFade(0f, 2f);
                Destroy(GameObject.Find("Girl"), 8f);
                trigger = false;
            }
        }
        if (nameObject == "Dialog_Level2_Armor"){
            if(!trigger){
                trigger = true;
                start = 18;
                ReadInArrayDialogs();
                if(flag){
                    SteamAchievementsScript.Instance.UnlockAchievment("ACH_HEAVY_ARMOR");
                    if(LanguageNumber == 0){
                    arrayString = new string[]{"О - офигеть! да это же тяжёлая броня!","Хотеть!"};
                    }
                   if(LanguageNumber == 1){
                        arrayString = new string[]{"Oh - go crazy! Yes, it's heavy armor!","I want!"};
                    }
                    arrayWho = new string[]{"Rooster", "Rooster"};
                    WriteInArrayDialogs();
                }
                trigger = false;
            }
        }
        if (nameObject == "Dialog_Level2_Finish"){
            if(!trigger){
                trigger = true;
                start = 26;
                ReadInArrayDialogs();
                if(flag){
                    if(LanguageNumber == 0){
                    arrayString = new string[]{"Я попал в ловушку, проклятье!"};
                    }
                   if(LanguageNumber == 1){
                        arrayString = new string[]{"I was trapped, curse!"};
                    }
                    arrayWho = new string[]{"Rooster"};
                    WriteInArrayDialogs();
                }
                trigger = false;
            }
        }
        if (nameObject == "Dialog_Level3_Start"){
            if(!trigger){
                trigger = true;
                Options = true;
                if(LanguageNumber == 0){
                    arrayString = new string[]{"Только гляньте,\nеще один тупой рыцарь угодил в эту ловушку.\nДо чего же они тупые!","Эй, ты, свинья!\nСнимай свою одежду и отдавай оружие!!!","Что будем делать?\n1. Отдать броню и оружие\n2. Пошёл ты, говнюк!"};
                }
               if(LanguageNumber == 1){
                    arrayString = new string[]{"Just look,\nas one blunt knight fell into this trap.\nThey are stupid!", "Hey, cock!\nRemove your clothes and give the weapon!!!", "What are we going to do?\n1. Give up the armor and weapons\n2. Fuck you!"};
                }
                Options = true;
                arrayOption= new string[]{"Dialog_Level3_Start_var_1","Dialog_Level3_Start_var_2"};
                arrayWho = new string[]{"Rogue","Rogue","Rooster"};
                WriteInArrayDialogs();
                trigger = false;
            }
        }
        if (nameObject == "Dialog_Level3_Start_Escape(Clone)"){
            if(!trigger){
                trigger = true;
                 start = 28;
                ReadInArrayDialogs();
                if(flag){
                    if(LanguageNumber == 0){
                    arrayString = new string[]{"Если я не выберусь отсюда – мне конец! Нужно что-нибудь придумать!!!"};
                    }
                   if(LanguageNumber == 1){
                        arrayString = new string[]{"If I do not get out of here - I'm dead! I need to think of something !!!"};
                    }
                    arrayWho = new string[]{"Rooster"};
                    WriteInArrayDialogs();
                }
                trigger = false;
            }
        }
        if (nameObject == "Dialog_Level3_Dying_Warrior"){
            if(!trigger){
                trigger = true;
                start = 29;
                ReadInArrayDialogs();
                Options = true;
                if(flag){
                    if(LanguageNumber == 0){
                        arrayString = new string[]{"Это же солдат из Королевской армии! Ты как, боец?","… Я умираю… Нас разбили…\nПостоянные засады… Нас изматывали атаками много дней подряд…\nмы постоянно теряли людей…","Кха! (кашляет кровью)\nРыцарь, у меня есть к тебе последняя просьба…\nГлоток воды…","Дать воды?\n1. да\n 2. нет"};
                    }
                   if(LanguageNumber == 1){
                        arrayString = new string[]{"It's a soldier from the Royal Army! Are you like a soldier?", "...I'm dying...We were defeated...\nConstant ambushes...We were harassed for many days...\nwe constantly lost people...", "Knight, I have a last favor to ask you...\n...water...", "Give him some water?\n1. Yes \n 2. No"};
                    }
                    arrayOption = new string[]{"Dialog_Level3_Dying_Warrior_var_1","Dialog_Level3_Dying_Warrior_var_2"};
                    arrayWho = new string[]{"Rooster","Warrior","Warrior","Rooster"};
                    WriteInArrayDialogs();
                }
                trigger = false;
            }
        }
        if (nameObject == "Dialog_Level3_Strelok"){
            if(!trigger){
                trigger = true;
                Options = false;
                GameObject.Find("CapitanShooters").GetComponent<CapitanShooters>().enabled = true;
                if(LanguageNumber == 0){
                    arrayString = new string[]{"Я даже не буду с тобой разговаривать, пещерный человек, ибо в моих руках – прогресс, а в твоих – дубина. Ты мамонт, и я с тобой покончу!!!"};
                }
                if(LanguageNumber == 1){
                    arrayString = new string[]{"I will not even talk to you, caveman, for in my hands - progress, and in yours - a cudgel. You're a mammoth, and I'll kill you!"};
                }
                arrayWho = new string[]{"CapitanShooters"};
                WriteInArrayDialogs();
            }
        }
        if (nameObject == "Dialog_Level3_Boss_Start"){
            if(!trigger){
                trigger = true;
                if(LanguageNumber == 0){
                arrayString = new string[]{"Склонись передо мною, я твой Король!","1. …\n2. Ты не Король"};
                }
                if(LanguageNumber == 1){
                    arrayString = new string[]{"Bow, I'm your King!", "1. …\n2. You are not King"};
                }
                Options = true;
                arrayOption = new string[]{"Dialog_Level3_Boss1_var_1","Dialog_Level3_Boss1_var_2"};
                arrayWho = new string[]{"Hanger","Rooster"};
                WriteInArrayDialogs();
            }
        }
        if (nameObject == "Dialog_Level3_Boss_End(Clone)"){
            if(!trigger){
                trigger = true;
                start = 32;
                ReadInArrayDialogs();
                if(flag){
                    if(LanguageNumber == 0){
                    arrayString = new string[]{"Кто ты, рыцарь?","Я сэр Рустер из замка – Маяка, смотритель северного моря.","Я умираю… Я слишком стар, чтобы победить мои полученые раны… Но я счастлив, что в Королевстве еще живут настоящие герои!","Я просто делаю то, что велит долг…","Ты самый достойный… Ты идешь спасать мою дочь?","Так точно, ваше величество!","Ты справишься, я знаю… Некогда я сразил этого Дракона…\nКогда был молод как ты… Я нанес ему несколько тяжелых ранений,\nв том числе отрубил крыло…","Он уполз тогда, и я верил, что он издох…\nНо он выжил, и теперь хочет вернуть свой трон!...\nОн ему не достанется, ты его остановишь…\nОстановишь и станешь новым Королем","Что?!","Вот мой королевский перстень…\nА вот грамота, подписаная мной…","Трон твой, когда ты вернешься с Принцессой…\nЛорды подчинятся! Они всегда боятся драконоубийц… Удачи!"};
                    }
                   if(LanguageNumber == 1){
                        arrayString = new string[]{"Who are you, knight?", "I Sir Ruster from the castle - Lighthouse, caretaker of the northern sea.", "I'm dying...I'm too old to defeat my injuries...But I'm happy that the real heroes still live in the Kingdom!" , "I just do what my duty says...", "You're the most worthy...Are you going to save my daughter?", "So sure, Your Majesty!", "You'll manage, I know...Once I slain this Dragon...\nWhen was young as you...I've dealt him some heavy wounds,\nincluding chopping off the wing...","He crawled away then, and I believed that he died...\nHow he survived, and now wants to regain his throne!...\nHe's he will not get it, you stop him...\nStop and become a new King...","What?!","Here is my royal ring...\nAnd the letter signed by me...","Throne is yours when you return with the Princess...\nLords They are always afraid of dragons...Good luck!"};
                    }
                    arrayWho = new string[]{"King","Rooster","King","Rooster","King","Rooster","King","King","Rooster","King","King"};
                    WriteInArrayDialogs();
                }
            }
        }
        if (nameObject == "Dialog_Level4_Toulet"){
            if(!trigger){
                trigger = true;
                leve4_toulet = true;
                if(flag){
                    if(LanguageNumber == 0){
                    arrayString = new string[]{"Пацаны, схожу отолью.", "АААА! Я провалился!"};
                    }
                    if(LanguageNumber == 1){
                        arrayString = new string[]{"Boys, I'll piss", "A! I failed!"};
                    }
                    skipTextComponent.enabled = false;
                    GameObject.Find("RogueGoInToilet").GetComponent<RogueGoInToilet>().enabled = true; 
                    arrayWho = new string[]{"Rogue","Rogue"};
                    WriteInArrayDialogs();
                }
                trigger = false;
            }
        }
        if (nameObject == "Dialog_Level4_Torch"){
            if(!trigger){
                trigger = true;
                start = 34;
                ReadInArrayDialogs();
                if(flag){
                    if(LanguageNumber == 0){
                    arrayString = new string[]{"Так, на этом факеле написана инструкция.", "- Ударь по мне.\n- Жди.\n- Враги повержены! Ну или побиты.(Смотри по ситуации)"};
                    }
                    if(LanguageNumber == 1){
                        arrayString = new string[]{"So, on this torch the instruction is written", "- Hit on me.\n- Wait.\n- Enemies defeated! Well, or injured. (See the situation)"};
                    }
                    arrayWho = new string[]{"Rooster","Rooster"};
                    WriteInArrayDialogs();
                }
                trigger = false;
            }
        }
        if (nameObject == "Dialog_Level4_Captive"){
            if(!trigger){
                trigger = true;
                if(LanguageNumber == 0){
                    if(Information.Instance.OptionGirl){
                        arrayString = new string[]{"Здравствуй, ты перебил стражу чтобы спасти меня?\nЯ уже потерял надежду и думал помру здесь.\nБольшое спасибо тебе.\nЧем я могу отплатить тебе за спасение?", "Думаю мне пригодятся твои навыки лучника.","Хорошо, но мне сначала нужно спасти дочь, она в плену у разбойников. Ты не видел их лагерь?","Я уже спас твою дочь, не переживай за неё.","Благодарю тебя достойный рыцарь, я помогу одолеть помощников дракона."};
                        arrayWho = new string[]{"Archer","Rooster","Archer","Rooster","Archer"};
                    }else{
                        Options = true;
                        arrayString = new string[]{"Здравствуй, ты перебил стражу чтобы спасти меня?\nЯ уже потерял надежду и думал помру здесь.\nБольшое спасибо тебе.\nЧем я могу отплатить тебе за спасение?", "Думаю мне пригодятся твои навыки лучника.","Хорошо, но мне сначала нужно спасти дочь, она в плену у разбойников. Ты не видел их лагерь?","Что ответить?\n 1. Я договорился с ними чтобы они мне пропустили,а я не лез в их дела\n 2. Я ничего не видел"};
                        arrayOption = new string[]{"Dialog_Level4_Captive_var_1","Dialog_Level4_Captive_var_2"};
                        arrayWho = new string[]{"Archer","Rooster","Archer"};
                    }
                }
               if(LanguageNumber == 1){
                    if(Information.Instance.OptionGirl){
                        arrayString = new string[]{"Hello, did you interrupt the guards to save me?\nI have already lost hope and thought I'll die here.\nMuch thanks to you.\nWhat can I do to pay you for salvation?", "I think your archer skills will come in handy.", "Okay, but I first need to save my daughter, she is a prisoner of robbers, have you seen their camp?","I have already saved your daughter, do not worry about her.","I thank you a worthy knight, I will help defeat the dragon's assistants."};
                        arrayWho = new string[]{"Archer","Rooster","Archer","Rooster","Archer"};
                    }else{
                        Options = true;
                        arrayString = new string[]{"Hello, did you interrupt the guards to save me?\nI have already lost hope and thought I'll die here.\nMuch thanks to you.\nWhat can I do to pay you for salvation?", "I think your archer skills will come in handy.", "Okay, but I first need to save my daughter, she is a prisoner of robbers, have you seen their camp?"," What to answer?\n1. I arranged with them so that they missed me, but I did not climb into their business \n2. I did not did not see"};
                        arrayOption = new string[]{"Dialog_Level4_Captive_var_1","Dialog_Level4_Captive_var_2"};
                        arrayWho = new string[]{"Archer","Rooster","Archer"};
                    }
                }
                WriteInArrayDialogs();
            }
        }       
        if (nameObject == "Dialog_Level5_Start"){
            if(!trigger){
                trigger = true;
                start = 37;
                ReadInArrayDialogs();
                if(flag){
                    if(LanguageNumber == 0){
                        arrayString = new string[]{"Непонятно почему, но мертвым здесь не спится.\nПлохое место для прогулок под луной и семейного отдыха.","Кстати, вон первый житель этих мест, на некроманта похож, пойдём поговорим с ним."};
                    }
                   if(LanguageNumber == 1){
                        arrayString = new string[]{"It is not clear why, but the dead can not sleep here.\nA bad place for walking under the moon and family vacation.", "By the way, the first inhabitant of these places won’t look like a necromancer, let's go talk to him."};
                    }
                    arrayWho = new string[]{"Rooster","Rooster"};
                    WriteInArrayDialogs();
                }
                trigger = false;
            }
        }
        
        if (nameObject == "Dialog_Level5_Necr_1"){
            if(!trigger){
                trigger = true;
                start = 38;
                ReadInArrayDialogs();
                if(flag){
                    if(LanguageNumber == 0){
                    arrayString = new string[]{"Тьма… Наполняет меня! Я прокачал некроманта до 81 – го уровня, и теперь!...","Тебя никто не любит?","Ага…","Это больно?","Очень.","Ты наверняка пожалел, что ступил на эту дорожку. ","Да…","Никогда не поздно остановиться.","Ты прав… Я вижу белый свет! Спасибо, смертный…"};
                    }
                    if(LanguageNumber == 1){
                        arrayString = new string[]{"Darkness...Fills me! I pumped the necromancer up to level 81, and now!...", "Does nobody love you?", "Yes...", "Does it hurt?", "Very...", "Surely I regretted stepping on this path.","...Yes..."," It is never too late to stop. "," You're right ... I see the white light! Thank you, mortal..."};
                    }
                    arrayWho = new string[]{"Necromancer","Rooster","Necromancer","Rooster","Necromancer","Rooster","Necromancer","Rooster","Necromancer"};
                    WriteInArrayDialogs();
                    leve5_diplomat = true;
                }else{
                    GameObject.Find("SoulNecr").GetComponent<SoulNecr>().enabled = true;
                }    
            }
        }
        if (nameObject == "Dialog_Level5_Necr_1_Finish(Clone)"){
            if(!trigger){
                start = 39;
                trigger = true;
                ReadInArrayDialogs();
                if(flag){
                    if(LanguageNumber == 0){
                    arrayString = new string[]{"Всегда качал в третьих дипломатию."};
                    }
                   if(LanguageNumber == 1){
                        arrayString = new string[]{"I always studied diplomacy in HoMM3"};
                    }
                    arrayWho = new string[]{"Rooster"};
                    WriteInArrayDialogs();
                }    
            }
            DialogsOff();
        }
        if (nameObject == "Dialog_Level5_Necr_2"){
            if(!trigger){
                trigger = true;
                start = 40;
                ReadInArrayDialogs();
                if(flag){
                    if(LanguageNumber == 0){
                    arrayString = new string[]{"Знаешь, никогда не поздно остановиться.","Что?","Ты случайно не видишь белый свет?","Я вижу, из тебя получится отличный зомби!"};
                    }
                   if(LanguageNumber == 1){
                        arrayString = new string[]{"You know, it's never too late to stop.","What?","Do you not see the divine light?","I see... You will be a great zombie!"};
                    }
                    arrayWho = new string[]{"Rooster","Necromancer","Rooster","Necromancer"};
                    WriteInArrayDialogs();
                }
                trigger = false;
            }
        }
        if (nameObject == "Dialog_Level5_Wizard"){
            if(!trigger){
                trigger = true;
                start = 80;
                ReadInArrayDialogs();
                if(flag){
                    if(LanguageNumber == 0){
                    arrayString = new string[]{"Похоже на мантию мага! Наконец-то, я нашёл её. На лейбле написано не стирать в воде выше 60 градусов.","И ещё если нажать  « " + GameObject.Find("TextButtonMagic1").GetComponent<Text>().text + " » то посох шмальнёт ледяной стрелой, если  « " + GameObject.Find("TextButtonMagic2").GetComponent<Text>().text + " », то огненным шаром, а если нажать  « " + GameObject.Find("TextButtonMagic3").GetComponent<Text>().text + " », то восстановит XP.","А скока XP то? эээ, только цена написана, 4 MP, 8 MP, 15 MP соответственно.","А брать то где ваше MP?, что за нелюди шили?","Оу, ещё можно создать магический щит кнопкой « " + GameObject.Find("TextButtonBlock").GetComponent<Text>().text + " » защищающий от атак со всех сторон, нанося урон противникам, круть!"};
                    }
                   if(LanguageNumber == 1){
                        arrayString = new string[]{"This is a mage mage! I finally found it. The label says «not to wash it in hot water»","Press  « " + GameObject.Find("TextButtonMagic1").GetComponent<Text>().text + " »  for shoot an ice arrow. Press « " + GameObject.Find("TextButtonMagic2").GetComponent<Text>().text + " » for fireball. Press « " + GameObject.Find("TextButtonMagic3").GetComponent<Text>().text + " » for wound healing.", "4 MP, 8 MP, 15 MP respectively.","Where to mine MP?", "Press « " + GameObject.Find("TextButtonBlock").GetComponent<Text>().text + " » for create shield."};
                    }
                    arrayWho = new string[]{"Rooster","Rooster","Rooster","Rooster","Rooster"};
                    WriteInArrayDialogs();
                }
                trigger = false;
            }
        }
        if (nameObject == "Dialog_Level5_Boss1"){
            if(!trigger){
                trigger = true;
                Options = true;
                if(LanguageNumber == 0){
                arrayString = new string[]{"Я приветствую тебя в божьем храме, рыцарь!\nТы пришел на причастие?\nУ меня как раз есть свежая плоть и кровь…\n человечина, если не против? Ахахаха!!!","1.Что, твою мать, здесь происходит?!\n2. Смотри ответ номер 3.\n3. Смотри ответ номер 1"};
                }
               if(LanguageNumber == 1){
                    arrayString = new string[]{"I greet you in the temple of God, knight!\nHave you come to communion?\nI just have fresh flesh and blood…\nhuman flesh, if not against it? Ahahaha !!!","1. WTF!?\n2. See answer 3\n3. See answer 1"};
                }
                arrayOption = new string[]{"Level5_Boss1_1","Level5_Boss1_1","Level5_Boss1_1"};
                arrayWho = new string[]{"DeadPriest","Rooster"};
                WriteInArrayDialogs();
            }
        }
        if (nameObject == "Dialog_Level6_Boss1"){
            if(!trigger){
                trigger = true;
                Options = true;
                if(LanguageNumber == 0){
                arrayString = new string[]{"Как долго я спал… Ты пришел сражаться… За меня или против меня?","1.  А что если за тебя?\n2.  Против. (бой)"};
                }
               if(LanguageNumber == 1){
                    arrayString = new string[]{"How long have I slept ... Did you come to fight ... with me or against me?","1. with you\n2. Vs.(the battle)"};
                }
                arrayOption = new string[]{"Dialog_Level6_Boss2_1","Dialog_Level6_Boss2_2"};
                arrayWho = new string[]{"DeadKing","Rooster"};
                WriteInArrayDialogs();
            }
        }
        if (nameObject == "Level7_Cold"){
            if(!trigger){
                trigger = true;
                if(LanguageNumber == 0){
                arrayString = new string[]{"Здесь очень холодно, не дай мне замерзнуть.","Для этого мне достаточно отогреваться в домах на земле, деревьях и костры неплохо греют"};
                }
               if(LanguageNumber == 1){
                    arrayString = new string[]{"It is very cold here, do not let me freeze.","Bask in homes and near and fires"};
                }
                arrayWho = new string[]{"Rooster","Rooster"};
                WriteInArrayDialogs();
            }
        }
        if (nameObject == "Dialog_Level7_Finish"){
            if(!trigger){
                trigger = true;
                start = 45;
                ReadInArrayDialogs();
                if(flag){
                    if(LanguageNumber == 0){
                    arrayString = new string[]{"В дали вы видите огромную зеленую ящерицу, пускающую огонь во все стороны.\nНу прям как во всех сказках, настоящий дракон!","Дубиной по башке ей сейчас зазвездим!"};
                    }
                   if(LanguageNumber == 1){
                        arrayString = new string[]{"In the distance you see a huge green lizard firing in all directions.\nWell, just like in fairy tales, a real dragon!","Beat him!"};
                    }
                    arrayWho = new string[]{"Rooster","Rooster"};
                    WriteInArrayDialogs();
                }
                trigger = false;
            }
        }
        if (nameObject == "Dialog_Level8_Balist"){
            if(!trigger){
                trigger = true;
                if(flag){
                    if(LanguageNumber == 0){
                    arrayString = new string[]{"Кто-то пытался убить дракона из балисты...","Возможно, я смогу ею воспользоваться, чтобы ослабить змеюку!"};
                    }
                    if(LanguageNumber == 1){
                        arrayString = new string[]{"Someone tried to kill the dragon from the balista ...","Perhaps I can use it to weaken the snake!"};
                    }
                    arrayOption = new string[]{"Dialog_Level8_Dragon_1","Dialog_Level8_Dragon_2"};
                    arrayWho = new string[]{"Rooster","Rooster"};
                    WriteInArrayDialogs();
                }
            }
        }
        if (nameObject == "Dialog_Level8_Dragon"){
            if(!trigger){
                trigger = true;
                if(flag){
                    Options = true;
                    if(LanguageNumber == 0){
                    arrayString = new string[]{"Чудовище пожаловало!","Кажется, ты кое – что перепутал!","Разве?! А ну, скажи – скольких ты убил, пока забрался на эту гору?","Они были плохими парнями…","Но они были людьми! Твоими сородичами… Твоя чешуя – латы! Твои когти и зубы – мечи и кинжалы! Твои крылья – ноги и стремление… Ты такой же Дракон, как и я. Нам ни к чему сражаться.","Ты похитил Принцессу!","Забирай ее! Она твоя.","И это все? Ты меня так просто отпустишь?...","Не совсем… У меня есть еще одно щедрое предложение. Ты сильнейший из людей! Такие мне сгодятся, станешь моим полководцем – Рыцарем Дракона!","Мы вернем мой трон, а ты станешь моей правой рукой, и поможешь мне править!... И думать нечего, Рустер!","1. Я согласен. Отдай ее.\n2. Ты сотни лет угнетал мой народ… Ты пожирал наших женщин, и убивал мужчин…\nЯ покончу с тобой!(В бой!)"};
                    }
                   if(LanguageNumber == 1){
                        arrayString = new string[]{"Monster has come!","Are you not confused?","how many people did you kill before you climbed this mountain?","They were bad guys...","But they were human! Your relatives ... Your scales - armor! Your claws and teeth are swords and daggers! Your wings are legs and aspiration... You are the same Dragon as me. We have nothing to fight.","You stole the Princess!","Take her! She's yours.","And it's all? Will you just let me go so easily?","Not really ... I have another generous offer. You are the strongest of people! These will fit me, you will become my commander - the Knight of the Dragon!","We will return my throne, and you will become my right hand, and help me to rule! ... And there is nothing to think, Rooster!","1. I agree.\n2.I will kill you! (In battle!)"};
                    }
                    arrayOption = new string[]{"Dialog_Level8_Dragon_1","Dialog_Level8_Dragon_2"};
                    arrayWho = new string[]{"Dragon","Rooster","Dragon","Rooster","Dragon","Rooster","Dragon","Rooster","Dragon","Dragon","Rooster"};
                    WriteInArrayDialogs();
                }
            }
        }
        if (nameObject == "Epilog"){
            if(!trigger){
                trigger = true;
                if(flag){
                    Options = true;
                    if(LanguageNumber == 0){
                    arrayString = new string[]{"Ты спас меня! Тебя прислал Сэр Лонселот?","Моя Принцесса… Я помню вас с самого турнира!\nВаша красота сияла над ристалищем ярче солнца!","Мой герой(нежно протянула она)\nСэр Лонселот прислал за мной верного слугу\nРустер, так, кажется, тебя зовут?...","Вы меня помните?!","Конечно. Но это не важно, приведи коня, я поскачу к моему Лонселотику!","Что делать?\n 1. Привести коня\n 2. ээ.. погодь...так не пойдёт\n 3. Развернуться и уйти"};
                    }
                    if(LanguageNumber == 1){
                        arrayString = new string[]{"You saved me! Sir Loncelot sent you?","My Princess ... I remember you from the tournament itself!\nYour beauty shone over the lists brighter than the sun!","My hero(she gently stretched)\nSir Loncelot sent for me a faithful servant\nIs your name Rooster?","You remember me?!","Of course. But it does not matter, bring the horse, I will jump to my Loncelotics!","What to do? \n1. Bring a horse \n2. uh .. wait ... it won't go that way \n3. Turn around and leave"};
                    }
                    arrayOption = new string[]{"Dialog_Epilog_option_1","Dialog_Epilog_option_2","Dialog_Epilog_option_3"};
                    arrayWho = new string[]{"Princess","Rooster","Princess","Rooster","Princess","Rooster"};
                    WriteInArrayDialogs();
                }
            }
        }  
    }
    public void OpenDialog(Dialogue dialogue){
        if(!trigger){
            trigger = true;
            arrayString = dialogue.GetArrayLine(LanguageNumber).ToArray();
            arrayWho = dialogue.GetArraySpeakers(LanguageNumber).ToArray();
            WriteInArrayDialogs();
        }
    }
//end dialogs    
    Coroutine writeCoroutine;
    void StartWrite(string str){
        if(writeCoroutine != null){
            StopCoroutine(writeCoroutine);
            writeCoroutine = null;
        }
        writeCoroutine = StartCoroutine(WriteMessage(str));
    }
    public void SpeakWord(){
        EndMessage = false;
        quitMenu.enabled = true;
        i = 0;
        _skip = false;
        StartWrite(arrayString[0]);
        WhoSpeakAvater();
    }
    
    void ReadInArrayDialogs(){
        flag = true;
        for(var i = 0;i < DialogsIndex.Length; i++){
            if(start == DialogsIndex[i]){
                flag = false; 
            }
        }
        information.DialogsIndex = DialogsIndex;
    }

    void WhoSpeakAvater(){
        imgRooster.enabled = false;
        imgRogue.enabled = false;
        imgArchi.enabled = false;
        imgBerserk.enabled = false;
        imgArcher.enabled = false;
        imgShaman.enabled = false;
        imgGuy.enabled = false;
        imgSmith.enabled = false;
        imgLeo.enabled = false;
        imgDragon.enabled = false;
        imgTeacher.enabled = false;
        imgGirlIdle.enabled = false;
        imgGirlWorry.enabled = false;
        imgRoosterFlipX.enabled = false;
        imgSceleton.enabled = false;
        imgNecromanser.enabled = false;
        imgTeacherLeft.enabled = false;
        imgWarrior.enabled = false;
        imgKing.enabled = false;
        imgCapitan.enabled = false;
        imgDeadPriest.enabled = false;
        imgDeadKing.enabled = false;
        imgHanger.enabled = false;
        if(GameObject.Find("imgPrincess")) 
            imgPrincess.enabled = false;
        switch(arrayWho[i]){
            case "Rooster":
                imgRooster.enabled = true;
                break;
            case "Rogue":
                imgRogue.enabled = true;
                break;
            case "Teacher":
                imgTeacher.enabled = true;
                break;
            case "TeacherLeft":
                imgTeacherLeft.enabled = true;
                break;
            case "Guy":
                imgGuy.enabled = true;
                break;
            case "GirlWorry":
                imgGirlWorry.enabled = true;
                break;
            case "GirlIdle":
                imgGirlIdle.enabled = true;
                break;
            case "RoosterFlipX":
                imgRoosterFlipX.enabled = true;
                break;
            case "Archi":
                imgArchi.enabled = true;
                break;
            case "Archer":
                imgArcher.enabled = true;
                break;
            case "Shaman":
                imgShaman.enabled = true;
                break;
            case "Berserk":
                imgBerserk.enabled = true;
                break;
            case "Dragon":
                imgDragon.enabled = true;
                break;
            case "Smith":
                imgSmith.enabled = true;
                break;
            case "Leo":
                imgLeo.enabled = true;
                break;
            case "Sceleton":
                imgSceleton.enabled = true;
                break;
            case "Necromancer":
                imgNecromanser.enabled = true;
                break;
            case "Hanger":
                imgHanger.enabled = true;
                break;
            case "DeadKing":
                imgDeadKing.enabled = true;
                break;
            case "DeadPriest":
                imgDeadPriest.enabled = true;
                break;
            case "CapitanShooters":
                imgCapitan.enabled = true;
                break;
            case "King":
                imgKing.enabled = true;
                break;
            case "Princess":
                imgPrincess.enabled = true;
                break;
            case "Warrior":
                imgWarrior.enabled = true;
                break;
            default:
                break;                                         
        }                        
    }
    void WriteInArrayDialogs(){
        ObserverDialog(true);
        SpeakWord();
        Index = 0;
        for(var i = 0;i < DialogsIndex.Length; i++){
            if(0 == DialogsIndex[i]){
                Index = i;
                break; 
            }
        }
        DialogsIndex[Index] = start;
        PointForCameraScript.Instance.SetCameraToDialog();
    }
    void Events(){
        if(start == 24){
            if(i == 1){
                skipTextComponent.enabled = true;
            }
        }
    }
    void createDialog(string Name){
        if(writeCoroutine != null){
            StopCoroutine(writeCoroutine);
            writeCoroutine = null;
        }
        switch (Name){
            case "Dialog_Level8_Dragon_1":
                Dragon1_1();                
                break;
            case "Dialog_Level8_Dragon_2":    
                Dragon1_2();
                break;
            case "Dialog_Level3_Dying_Warrior_var_1":
                Warrior1_1();
                break;
            case "Dialog_Level3_Dying_Warrior_var_2":
                Warrior1_2();
                break;
            case "Dialog_Level2_Woman2_var_1":
                Woman2_1();
                break;
            case "Dialog_Level2_Woman2_var_2":
                Woman2_2();
                break;
            case "Dialog_Level3_Start_var_1":
                Level3_Start_1();
                break;
            case "Dialog_Level3_Start_var_2":
                Level3_Start_2();
                break;
            case "Dialog_Level3_Boss1_var_1":
                Level3_Boss1_1();
                break;
            case "Dialog_Level3_Boss1_var_2":
                Level3_Boss2_2();
                break;
            case "Level3_Boss2_1":
                Level3_Boss2_1();
                break;
            case "Level3_Boss2_2":
                Level3_Boss2_2();
                break; 
            case "Level3_Boss3_1":
                Level3_Boss3_1();
                break;
            case "Level3_Boss3_2":
                Level3_Boss2_1();
                break;
            case "Level3_Boss4_1":
                Level3_Boss4_1();
                break;
            case "Level3_Boss4_2":
                Level3_Boss2_1();
                break;
            case "Level3_Boss5_1":
                Level3_Boss5_1();
                break;
            case "Level3_Boss5_2":
                Level3_Boss2_1();
                break;
            case "Level3_Boss6_1":
                Level3_Boss6_1();
                break;
            case "Level3_Boss6_2":
                Level3_Boss2_1();
                break;
            case "Level3_Boss7_1":
                Level3_Boss7_1();
                break;
            case "Level3_Boss7_2":
                Level3_Boss2_1();
                break;
            case "Dialog_Level4_Captive_var_1":
                Dialog_Level4_Captive_var_1();
                break;
            case "Dialog_Level4_Captive_var_2":
                Dialog_Level4_Captive_var_2();
                break;
            case "Level5_Boss1_1":
                Level5_Boss1_1();
                break;
            case "Level5_Boss2_1":
                Level5_Boss2_1();
                break;
            case "Level5_Boss2_2":
                Level5_Boss5_2();
                break;
            case "Level5_Boss3_1":
                Level5_Boss5_2();
                break;
            case "Level5_Boss3_2":
                Level5_Boss3_2();
                break;
            case "Level5_Boss4_1":
                Level5_Boss4_1();
                break;
            case "Level5_Boss4_2":
                Level5_Boss5_2();
                break;
            case "Level5_Boss5_1":
                Level5_Boss5_1();
                break;
            case "Level5_Boss5_2":
                Level5_Boss5_2();
                break;
            case "Dialog_Level6_Boss2_1":
                Dialog_Level6_Boss2_1();
                break;
            case "Dialog_Level6_Boss2_2":
                Dialog_Level6_Boss3_2();
                break;
            case "Dialog_Level6_Boss3_1":
                Dialog_Level6_Boss3_1();
                break;
            case "Dialog_Level6_Boss3_2":
                Dialog_Level6_Boss3_2();
                break;
            case "Dialog_Epilog_option_1":
                Dialog_Epilog_option_1();
                break;
            case "Dialog_Epilog_option_2":
                Dialog_Epilog_option_2();
                break;
            case "Dialog_Epilog_option_3":
                Dialog_Epilog_option_3();
                break;             
        }
    }
/*Dialogs for option*/
    void Dragon1_1(){
        Options = false;
        GameOver = true;
        information.CauseOfDeath = "Leve8_Boss_Dialog";
        if(LanguageNumber == 0){
        arrayString = new string[]{"Ты настоящий Дракон,\nхоть и человек! Забирай ее!\nВозвращайся в свой замок.","Через три ночи на твоем берегу\nвысадится моя армия наемников –\nты поведешь их на столицу!\nБудь готов!","Нет… нет!!!\nЯ не пойду с тобой!","Теперь ты моя."};
        }
       if(LanguageNumber == 1){
            arrayString = new string[]{"You're a real dragon,\neven though a man! Take her!\nGo back to your castle.","After three nights, my army of\nmercenaries will land on your shore\n- you will lead them to the\ncapital! Be ready!","No no!!!\nI will not go with you!","Now you belong to me."};
        }
        arrayWho = new string[]{"Dragon","Dragon","Princess","Rooster"};
        WriteInArrayDialogs();
    }
    void Dragon1_2(){
        Options = false;
        GameObject.Find("DragonBoss").GetComponent<DragonBoss>().enabled = true;
        if(LanguageNumber == 0){
        arrayString = new string[]{"Я СОКРУШУ ТЕБЯ ЧЕЛОВЕЧИШКА!!!"};
        }
       if(LanguageNumber == 1){
            arrayString = new string[]{"I'll ruin your man!!!"};
        }
        arrayWho = new string[]{"Dragon"};
        WriteInArrayDialogs();
    }
    void Warrior1_1(){
        Options = false;
        if(LanguageNumber == 0){
            arrayString = new string[]{"Благодарю… (судорожно пьет)","Что с Королем? Он жив?","Должен быть жив… Его предал палач…\nОказалось, он главарь всех преступников…\nОн нас сдал… Отомсти за нас, воин!!!... (умирает)"};
            arrayWho = new string[]{"Warrior","Rooster","Warrior"};
        }
        if(LanguageNumber == 1){
            arrayString = new string[]{"Thank you...\n(convulsive drink)","What's up with the king?\nIs he alive?","Must be alive... He was betrayed\nby the executioner ...\nIt turned out that he was the\nleader of all the criminals","... He passed us ... Avenge us, warrior !!! ... (dies)"};
            arrayWho = new string[]{"Warrior","Rooster","Warrior","Warrior"};
        }
        WriteInArrayDialogs();
    }
    void Warrior1_2(){
        Options = false;
        if(LanguageNumber == 0){
            arrayString = new string[]{"Жаль… (умирает)"};
        }
       if(LanguageNumber == 1){
            arrayString = new string[]{"It is a pity...(dies)"};
        }
        arrayWho = new string[]{"Warrior"};
        WriteInArrayDialogs();
    }
    void Level3_Start_1(){
        Options = false;
        Destroy(GameObject.FindWithTag("Player"));
        information.armor = 0;
        information.weapon = 0;
        information.ChangeSkin();
        DialogsOff();
    }
    void Level3_Start_2(){
        Options = true;
        if(LanguageNumber == 0){
            arrayString = new string[]{"...(Выродок бросил в вас большой камень)...\n(-1 к показателю HP)","Что будем делать дальше?\n1.Отдать вещи\n2...(Показать средний палец)..."};
        }
        if(LanguageNumber == 1){
            arrayString = new string[]{"... (Geek threw a big stone at you) ... (- 1 HP)","What will we do next?\n1. To give things\n2. ... (Show middle finger) ..."};
        }
        Information.Instance.CauseOfDeath = "Level3_Start_Dialog";
        GameObject.FindWithTag("Player").GetComponent<PlayerHP>().GetClearDamage(1);
        if(GameObject.FindWithTag("Player").GetComponent<PlayerHP>().HP == 0){
            DialogsOff();
        }else{
            arrayOption = new string[]{"Dialog_Level3_Start_var_1","Dialog_Level3_Start_var_2"};
            arrayWho = new string[]{"Rooster","Rooster"};
            WriteInArrayDialogs();
        } 
    }
    void Woman2_1(){
        Options = false;
        Information.Instance.OptionGirl = false;
        GameObject[] EnemyForGirl = GameObject.FindGameObjectsWithTag("EnemyForGirl");
        foreach(GameObject enemy in EnemyForGirl){
            if(enemy.GetComponent<RogueScript>()){
                enemy.GetComponent<RogueScript>().enabled = false;
            }
            if(enemy.GetComponent<BerserkScript>()){
                enemy.GetComponent<BerserkScript>().enabled = false;
            }
            if(enemy.GetComponent<ShamanScript>()){
                enemy.GetComponent<ShamanScript>().enabled = false;
            }
            if(enemy.GetComponent<Archer>()){
                enemy.GetComponent<Archer>().enabled = false;
            }
            enemy.GetComponent<Rigidbody2D>().gravityScale = 0;
            enemy.GetComponent<BoxCollider2D>().enabled = false;
            enemy.GetComponent<CircleCollider2D>().enabled = false;
        }
        DialogsOff();
    }
    void Woman2_2(){
        Options = false;
        Information.Instance.OptionGirl = true;
        GameObject.Find("Berserk_for_speak").GetComponent<BerserkScript>().enabled = true;
        GameObject.Find("Shaman").GetComponent<ShamanScript>().enabled = true;
        DialogsOff();
    }
    void Level3_Boss1_1(){
        Options = true;
        if(LanguageNumber == 0){
            arrayString = new string[]{"Ты не видишь королевскую мантию?!\nНе видишь моих подданых?!\nЗа твою дерзость я лично тебя казню!","1. Попробуй! (бой)\n2.  Что здесь вообще происходит?"};
        }
        if(LanguageNumber == 1){
            arrayString = new string[]{"Do you not see the royal mantle?!\nDo not see my subjects?\nFor your audacity, I personally execute you!","1. Try it! (battle)\n2. What is going on here at all?"};
        }
        arrayOption = new string[]{"Level3_Boss2_1","Level3_Boss2_2"};
        arrayWho = new string[]{"Hanger","Rooster"};
        WriteInArrayDialogs();
    }
    void Level3_Boss2_2(){
        Options = true;
        if(LanguageNumber == 0){
        arrayString = new string[]{"Ты не видишь? Восторжествовала справедливость!","1. Справедливость?\n2. Ты просто преступник и предатель. К бою!"};
        }
       if(LanguageNumber == 1){
            arrayString = new string[]{"You can not see? Justice triumphed!","1. Justice?\n2. You are just a criminal and a traitor. To battle!"};
        }
        arrayOption = new string[]{"Level3_Boss3_1","Level3_Boss3_2"};
        arrayWho = new string[]{"Hanger","Rooster"};
        WriteInArrayDialogs();
    }
    void Level3_Boss2_1(){
        GameObject.Find("HangMan").GetComponent<HangManScript>().enabled = true;
        DialogsOff();
    }
    void Level3_Boss3_1(){
        Options = true;
        if(LanguageNumber == 0){
            arrayString = new string[]{"Много лет это ничтожество… (плюет на Короля в клетке) … величало себя Королем!\nОн утопал в роскоши, он издавал законы,\nи думал что имеет на это право благодаря славе драконоубийцы!","1. А разве это не так?\n2.  Ты посмел оскорбить моего короля! За это я предам тебя справедливому наказанию! (бой)"};
        }
        if(LanguageNumber == 1){
            arrayString = new string[]{"For many years this is a nonentity ... (spits on the King in a cage) ... called himself a King!\nHe drowned in luxury, he enacted laws,\nand thought he had a right to do so thanks to the glory of the dragonkiller!","1. Isn't that right?\n2. You dared insult my king! For this I will give you a just punishment! (battle)"};
        }
        arrayOption = new string[]{"Level3_Boss4_1","Level3_Boss4_2"};
        arrayWho = new string[]{"Hanger","Rooster"};
        WriteInArrayDialogs();
    }
    void Level3_Boss4_1(){
        Options = true;
        if(LanguageNumber == 0){
            arrayString = new string[]{"Не так! Его власть держалась благодаря мне! Я исполнял его волю! Я устрашал и заставлял подчинятся! Любая власть держится на страхе и насилии, и потому палачи более других достойны корон. Так честно!","1.    В твоих словах есть доля истины…\n2.  Власть держится на справедливости и преданности! Ты предатель и убийца, не более! (бой)"};
        }
        if(LanguageNumber == 1){
            arrayString = new string[]{"Not this way! His power held thanks to me! I did his will! I frightened and forced to obey! Any power rests on fear and violence, and therefore the executioners more than others are worthy of crowns. So honest!","1. There is some truth in your words...\n2. Power rests on justice and loyalty! You are a traitor and a murderer, no more! (the battle)"};
        }
        arrayOption = new string[]{"Level3_Boss5_1","Level3_Boss5_2"};
        arrayWho = new string[]{"Hanger","Rooster"};
        WriteInArrayDialogs();
    }
    void Level3_Boss5_1(){
        Options = true;
        if(LanguageNumber == 0){
            arrayString = new string[]{"Ты умный рыцарь. И ты тоже убийца, и ты понимаешь, что я прав. Нам нечего делить, присоединяйся к нам! У нас много достойных ребят!","1. Хорошо. Если ты меня пропустишь, мне есть кого спасать.\n2.  Нет. Я рыцарь, давший клятву, а не бандит! (бой)"};
        }
        if(LanguageNumber == 1){
            arrayString = new string[]{"You are a smart knight. And you are also a murderer, and you understand that I am right. We have nothing to share, join us! We have a lot of decent guys!","1. Good. If you miss me, I have someone to save.\n2. Not. I'm a knight who swore an oath, not a thug! (battle)"};
        }
        arrayOption = new string[]{"Level3_Boss6_1","Level3_Boss6_2"};
        arrayWho = new string[]{"Hanger","Rooster"};
        WriteInArrayDialogs();
    }
    void Level3_Boss6_1(){
        Options = true;
        if(LanguageNumber == 0){
            arrayString = new string[]{"Одно условие, рыцарь. Мне нужно знать, что ты честен.\nУбей старика, и я скажу тебе, как спасти девку из башни дракона!","Что будем делать?\n1. Убить старика-Короля.\n2.  Это слишком! (бой)"};
        }
        if(LanguageNumber == 1){
            arrayString = new string[]{"One condition, knight. I need to know that you are honest.\nKill the old man, and I will tell you how to save the girl from the dragon tower!","What are we going to do?\n1. Kill the old King.\n2. That's too much! (battle)"};
        }
        arrayOption = new string[]{"Level3_Boss7_1","Level3_Boss7_2"};
        arrayWho = new string[]{"Hanger","Rooster"};
        WriteInArrayDialogs();
    }
    void Level3_Boss7_1(){
        Options = false;
        Information.Instance.CauseOfDeath = "Level3_Boss_Dialog";
        if(LanguageNumber == 0){
            arrayString = new string[]{"В моей стране одни бандиты! Чтоб вас всех Дракон сожрал!!!"};
        }
        if(LanguageNumber == 1){
            arrayString = new string[]{"In my country, some gangsters! So that all of you dragon gobbled up!!!"};
        }
        arrayWho = new string[]{"King"};
        GameOver = true;
        WriteInArrayDialogs();
    }
    void Dialog_Epilog_option_1(){
        Options = false;
        if(LanguageNumber == 0){
        arrayString = new string[]{"Спасибо, сэр Рустер, я распоряжусь чтобы вас озолотили за вашу преданность.","Как пожелаете, моя королева!"};
        arrayWho = new string[]{"Princess","Rooster"};
        }
       if(LanguageNumber == 1){
            arrayString = new string[]{"Thank you, Sir Rooster, I will order you to be ashamed for your loyalty.","I'll take it to sir Lancelot myself, he will be happy.","As you wish, my queen!"};
            arrayWho = new string[]{"Princess","Princess","Rooster"};
        }
        theEnd = true;
        WriteInArrayDialogs();
    }
    void Dialog_Epilog_option_2(){
        Options = false;
        if(LanguageNumber == 0){
        arrayString = new string[]{"Что? Ты думал что всё как в сказках будет? Да ты посмотри, кто я, и кто... ты.","Пфф... ты по факту был должен спасти меня и не ждать награду", "Тьфу...,уйди с глаз долой"};
        }
       if(LanguageNumber == 1){
            arrayString = new string[]{"What? Did you think that everything will be like in fairy tales? Yes, you see who I am, and who ... you.","...you were in fact supposed to save me and not wait for a reward.","Get out!"};
        }
        arrayWho = new string[]{"Princess","Princess","Princess"};
        theEnd = true;
        WriteInArrayDialogs();
    }
    void Dialog_Epilog_option_3(){
        Options = false;
        if(LanguageNumber == 0){
        arrayString = new string[]{"Стой, крестьянин! Как ты смеешь поворачиваться ко мне спиной!?", "КАЗНИТЬ ПОДЛЕЦА!!!"};
        }
       if(LanguageNumber == 1){
            arrayString = new string[]{"Stop, farmer! How dare you turn your back on me!?","SECURING CREATURES!!!"};
        }
        arrayWho = new string[]{"Princess","Princess"};
        theEnd = true;
        WriteInArrayDialogs();
    }
    void Dialog_Level4_Captive_var_1(){
        Options = false;
        if(LanguageNumber == 0){
        arrayString = new string[]{"Ах ты ублюдок! Я убью тебя!"};
        }
       if(LanguageNumber == 1){
            arrayString = new string[]{"Oh you bastard! I'll kill you!"};
        }
        GameObject.Find("ArcherInJail").GetComponent<Archer>().enabled = true;
        arrayWho = new string[]{"Archer"};
        WriteInArrayDialogs();
    }
    void Dialog_Level4_Captive_var_2(){
        Options = false;
        if(LanguageNumber == 0){
        arrayString = new string[]{"Прости, но мне нужно отправляться на поиски, если успею, то приду на помощь."};
        }
       if(LanguageNumber == 1){
            arrayString = new string[]{"I'm sorry, but I need to go searching, if I have time, I will come to the rescue."};
        }
        arrayWho = new string[]{"Archer"};
        WriteInArrayDialogs();
    }
    void Level5_Boss1_1(){
        Options = true;
        if(LanguageNumber == 0){
        arrayString = new string[]{"А вот ругаться в церкви – плохо! Тебя не учили?!","Ты скелет. Нежить. Зло. О каком боге ты говоришь?","О том, с которым ты скоро встретишься! АХАХАХАХА!!! Можешь исповедаться, я отпущу твои грехи!","Что делать?\n1. Исповедаться.\n2. Уничтожить нечисть! (бой)"};
        }
       if(LanguageNumber == 1){
            arrayString = new string[]{"But swearing in the church - bad! You have not been taught?","You are a skeleton. Undead Evil. What god are you talking about?","About the one with whom you will meet soon! AHAHAHAHA !!! You can confess, I will forgive your sins!","What to do? \n1. Confess. \n2. Destroy the evil! (battle)"};
        }
        arrayOption = new string[]{"Level5_Boss2_1","Level5_Boss2_2"};
        arrayWho = new string[]{"DeadPriest","Rooster","DeadPriest","Rooster"};
        WriteInArrayDialogs();
    }
    void Level5_Boss2_1(){
        Options = true;
        if(LanguageNumber == 0){
            arrayString = new string[]{"Я согрешил. Я убил много людей и… нелюдей, пока добрался сюда… Они были плохими парнями, но все же…","Продолжай, не сын мой.","А еще я недоволен своей нищетой, и в глубине души желаю богатства…","Я слушаю, продолжай!","И я желаю деву, которую спасаю… Она классная и меня очень возбуждает… Пожалуй, все.","Хм… Ты все правильно делаешь. Мне нечего тебе отпускать. Ты итак точно попадешь в ад.","1. … Чем я только что занимался? (бой)\n2.  Спасибо, святой мертвец."};
        }
        if(LanguageNumber == 1){
            arrayString = new string[]{"I have sinned. I killed a lot of people and ... inhumans until I got here ... They were the bad guys, but still ...","Go on, not my son.","I am also dissatisfied with my poverty, and in my heart I want wealth...","I listen, go on!","And I wish the maiden I am saving ... She is awesome and excites me very much ... Perhaps that's all.","Hmm ... You're doing the right thing. I have nothing to let you go. You’re going to hell for sure.","1. ... What did I just do? (battle)\n2. Thank you, holy dead."};
        }
        arrayOption = new string[]{"Level5_Boss3_1","Level5_Boss3_2"};
        arrayWho = new string[]{"Rooster","DeadPriest","Rooster","DeadPriest","Rooster","DeadPriest","Rooster"};
        WriteInArrayDialogs();
    }
    void Level5_Boss3_2(){
        Options = true;
        if(LanguageNumber == 0){
            arrayString = new string[]{"Не за что, рыцарь. Нам требуются воины, не хочешь присоединиться?","Что ответить?\n1. Да, что для этого нужно?\n2. Нет! (бой)"};
        }
        if(LanguageNumber == 1){
            arrayString = new string[]{"You're welcome, knight. We need warriors, don't you want to join us?","What to answer? \n1. Yes, what is needed for this?\n2. NO! (battle)"};
        }
        arrayOption = new string[]{"Level5_Boss4_1","Level5_Boss4_2"};
        arrayWho = new string[]{"DeadPriest","Rooster"};
        WriteInArrayDialogs();
    }
    void Level5_Boss4_1(){
        Options = true;
        if(LanguageNumber == 0){
            arrayString = new string[]{"Нужно умереть.","Наш выбор?\n1. Умереть\n2. Сражаться"};
        }
        if(LanguageNumber == 1){
            arrayString = new string[]{"Must die.","Our choice? \n1. Die \n2. Fight"};
        }
        arrayOption = new string[]{"Level5_Boss5_1","Level5_Boss5_2"};
        arrayWho = new string[]{"DeadPriest","Rooster"};
        WriteInArrayDialogs();
    }
    void Level5_Boss5_1(){
        Information.Instance.CauseOfDeath = "Leve5_Boss_Dialog";
        GameOver = true;
        DialogsOff();
    }
    void Level5_Boss5_2(){
        GameObject.Find("DeathPriest").GetComponent<DeadPriest>().enabled = true;
        DialogsOff();
    }
    void Dialog_Level6_Boss2_1(){
        Options = true;
        if(LanguageNumber == 0){
            arrayString = new string[]{"Я сделаю тебя рыцарем смерти. Все, что от тебя потребуется – истреблять живых…","Всех? Даже любимую девушку и котят?","Даже любимую и котят…\nТвое сердце остановится, все чувства уйдут…\nОстанется только ненависть к любой жизни…","Все, что дышит, все, что рождается и растет,\nвсе – попадет под твой меч!\nИ наступит эпоха смерти и тишины…","1.  Ок. Стать рыцарем смерти.\n2.  Нет! Я сражаюсь за жизнь!... и за котят! (бой)"};
        }
        if(LanguageNumber == 1){
            arrayString = new string[]{"I will make you a death knight. All that is required of you is to exterminate the living...","Everyone? Even a girlfriend and kittens?","Even your beloved and kittens ... \nTwo heart will stop, all feelings will go away ... \nIt will only hate any life ...","Everything that breathes, everything that is born and grows, \nall - will fall under your sword! \nAnd the era of death and silence will come ...","1. Ok. Become a death knight. \n2. Not! I fight for life! ... and for kittens! (battle)"};
        }
        arrayOption = new string[]{"Dialog_Level6_Boss3_1","Dialog_Level6_Boss3_2"};
        arrayWho = new string[]{"DeadKing","Rooster","DeadKing","DeadKing","Rooster"};
        WriteInArrayDialogs();
    }
    void Dialog_Level6_Boss3_1(){
        Information.Instance.CauseOfDeath = "Leve6_Boss_Dialog";
        GameOver = true;
        DialogsOff();
    }
    void Dialog_Level6_Boss3_2(){
        GameObject.Find("DeadKing").GetComponent<DeadKing>().enabled = true;
        DialogsOff();
    }
    void DialogsOff(){
        Options = false;
        trigger = false;
        quitMenu.enabled = false;
        if(leve5_diplomat){
            GameObject.Find("SoulNecr").GetComponent<SoulNecr>().enabled = true;
            leve5_diplomat = false;
        }
        PointForCameraScript.Instance.SetSimpleCamera();
        ObserverDialog(false);
    }
// end dialogs option    
    IEnumerator WriteMessage(string Message){
        Dialog.text = "";
        char[] sentense = Message.ToCharArray();
        for(int i=0;i<sentense.Length; i++){
            Dialog.text += sentense[i];
            if(i == sentense.Length-1){
                _skip = true;
                if(!EndMessage){
                    skipTextComponent.enabled = true;
                }else{
                    if(Options){
                        skipTextComponent.enabled = false;
                    }
                }
                if(leve4_toulet) skipTextComponent.enabled = false;
            }
            yield return null;
        }
    }
}
