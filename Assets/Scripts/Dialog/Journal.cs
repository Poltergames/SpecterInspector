using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Journal : MonoBehaviour
{
    public GameObject MrJournal;
    public GameObject CluesText;
    public GameObject BiosText;

    bool toggle = true;

    int Progression = 1;
    string clueText;
    string bioText;

    public TextAsset CluesCSV = null; // Reference of CSV file
    public TextAsset BiosCSV = null;
    private char lineSeperater = '\n'; // It defines line seperate character
    private char fieldSeperator = ','; // It defines field seperate chracter

    string[] biosLines;
    string[] biosColumns;
    string[] lines;
    //private char progSeperator = ':';
    // Start is called before the first frame update
    void Start()
    {
        //puts each row into lines
        lines = CluesCSV.text.Split (lineSeperater);
    }

    // Update is called once per frame
    void Update(){
        //opens and closes journal
        if (Input.GetKeyDown("j"))
        {
            MrJournal.SetActive(toggle);
            toggle = !toggle;
        }
    }
    void updateDialog()
    {

        //updates journal clues
        //if( Progression != ProgressKeeper.instance.Progression ){

        Progression = ProgressKeeper.instance.Progression;



        //splits current line into rows
        string[] columns = lines[Progression-1].Split(fieldSeperator);

        if( 0 < columns[2].Length && 1 < Progression){
            //prepares relevant text
            columns[2] = columns[2].Replace('&', '\"');
            clueText = columns[1] + columns[2].Replace('|', ',') + "\n\n";

            //concatenates text
            CluesText.GetComponent<TMPro.TextMeshProUGUI>().text += clueText;
            //print(clueText);
        }
        //}
    }
    public void updateBios(int charColumn){

        string bioText = "";

        //puts each row into lines
        biosLines = BiosCSV.text.Split (lineSeperater);

        //splits current line into rows
        biosColumns = biosLines[1].Split(fieldSeperator);

        if ( biosColumns[charColumn].Length > 0 ){

            bioText = biosColumns[charColumn].Replace('|', ',') + "\n";
            //biosColumns[charColumn] = "";

            //next line
            biosColumns = biosLines[2].Split(fieldSeperator);
            bioText += biosColumns[charColumn].Replace('|', ',') + "\n";

            //next line
            biosColumns = biosLines[3].Split(fieldSeperator);
            bioText += biosColumns[charColumn].Replace('|', ',') + "\n\n";

            //splits the column into lines
            biosLines = biosColumns[charColumn].Split(lineSeperater);


            BiosText.GetComponent<TMPro.TextMeshProUGUI>().text += bioText;
        }
    }
}
