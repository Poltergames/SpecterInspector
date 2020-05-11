using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressKeeper : MonoBehaviour
{
    public static ProgressKeeper instance;
    public GameObject DialogUI;
    public GameObject battlePrep;

    public int Progression = 1;
    public TextAsset DefcsvFile; // Reference of CSV file

    GameObject Player;
    public GameObject enemy;
    public bool combatting = false;
    public bool progressing = false;
    public GameObject talker = null;
    public GameObject DialogUIText;

    private CombatPositions CombatPositions;

    public GameObject[] listNPCs;

    // Start is called before the first frame update
    void Start()
    {
        Player =  GameObject.Find("Player");
        instance = this;
        listNPCs = GameObject.FindGameObjectsWithTag("NPC");

        Invoke("DelayedStart", .01f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void DelayedStart(){
        foreach (GameObject NPC in listNPCs)
        {
            NPC.SendMessage("updateDialog");
        }
    }

    public void ProgressInc(){

        Progression += 1;
        //print(Progression);
        foreach (GameObject NPC in listNPCs)
        {
            if (null != NPC)
            {
                NPC.SendMessage("updateDialog");
            }
        }
    }
    public void CloseDialog(){


        DialogUI.gameObject.SetActive(false);
        Player.GetComponent<PlayerMovement>().enabled = true;
        //print(combatting);
        if ( progressing ){

            ProgressInc();
            progressing = false;
        }
        if( combatting == true){

            //start combat
            //GameObject enemy = DialogPrint.instance.gameObject;
            CombatPositions = battlePrep.GetComponent<CombatPositions>();
            CombatPositions.battleStart(Player, enemy);

        }else if( talker != null){

            talker.GetComponent<DialogPrint>().SayDialog();
        }
    }

}
