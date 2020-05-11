using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class DialogPrint : MonoBehaviour
{
    //GameObject CanvasUI;
    public Texture DialogImage;
    GameObject DialogUI;
    GameObject TextUI;
    GameObject ImageUI;
    GameObject Journal;
    public TextAsset csvFile = null; // Reference of CSV file
    private char lineSeperater = '\n'; // It defines line seperate character
    private char fieldSeperator = ','; // It defines field seperate chracter
    private char progSeperator = '%';
    public bool combatting = false;
    public bool keepTalking = false;

    GameObject Player;
    public GameObject ClueLight;
    GameObject NPC;

    bool progressing = false;
    bool biosPrinted = false;

    public int CharColumn = 1;
    public int Progression = 1;
    string toPrint;

    public static DialogPrint instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        NPC = gameObject;
        //CanvasUI = GameObject.Find("CanvasUI");
        DialogUI = ProgressKeeper.instance.DialogUI;
        TextUI = ProgressKeeper.instance.DialogUIText;
        ImageUI = DialogUI.transform.Find("RawImage").gameObject;
        Player =  GameObject.Find("Player");
        ClueLight = gameObject.transform.GetChild(0).gameObject;
        //print(ClueLight);
        Journal = GameObject.Find("Journal").gameObject;

        csvFile = ProgressKeeper.instance.DefcsvFile;

    }

    // Update is called once per frame
    public void updateDialog()
    {
        Progression = ProgressKeeper.instance.Progression;

        //puts each row into lines
        string[] lines = csvFile.text.Split (lineSeperater);

        //splits current line into rows
        string[] columns = lines[Progression].Split(fieldSeperator);

        string[] prog = columns[CharColumn].Split(progSeperator);


        toPrint = prog[0];
        //print(prog.Length);
        progressing = (prog.Length == 2 || prog.Length == 3);
        keepTalking = (prog.Length == 3);
        combatting = (prog.Length == 4);

        toPrint = toPrint.Replace('&', '\"');
        toPrint = toPrint.Replace('|', ',');

        if(string.Compare("*", toPrint) == 0) {

            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<Renderer>().enabled = false;
            ClueLight.SetActive(false);
        }else{
            gameObject.GetComponent<Collider>().enabled = true;
            gameObject.GetComponent<Renderer>().enabled = true;
        }
        if( prog.Length > 1 ){
            //turn on spotlight
            ClueLight.SetActive(true);
        }else{
            ClueLight.SetActive(false);
        }

    }
    void OnTriggerEnter (Collider collision){

        if( "Player" == collision.GetComponent<Collider>().gameObject.name){

            SayDialog();
        }
    }
    /* Allows the player to walk out of a conversation, but then they might miss important info
    void OnTriggerExit (Collider collision){

        if( "Player" == collision.GetComponent<Collider>().gameObject.name){

            DialogUI.gameObject.SetActive(false);
        }
    }*/

    public void SayDialog(){

        ProgressKeeper.instance.talker = null;

        ProgressKeeper.instance.combatting = combatting;
        ProgressKeeper.instance.enemy = gameObject;

        if( keepTalking == true){
            ProgressKeeper.instance.talker = gameObject;
        }

        Player.GetComponent<PlayerMovement>().enabled = false;
        //checks for moving progression
        ImageUI.GetComponent<RawImage>().texture = DialogImage;

        if( true == progressing){
            TextUI.GetComponent<TMPro.TextMeshProUGUI>().color = Color.blue;
            //ProgressKeeper.instance.Progression += 1;
            ProgressKeeper.instance.progressing = true;
        }else if( true == combatting ){
            TextUI.GetComponent<TMPro.TextMeshProUGUI>().color = Color.red;
            //ProgressKeeper.instance.Progression += 1;
        }else{
            TextUI.GetComponent<TMPro.TextMeshProUGUI>().color = Color.white;
        }
        //print(toPrint);
        TextUI.GetComponent<TMPro.TextMeshProUGUI>().text = toPrint;
        DialogUI.gameObject.SetActive(true);

        if( !biosPrinted ){
            Journal.GetComponent<Journal>().updateBios(CharColumn);
            biosPrinted = true;
        }
    }

}
