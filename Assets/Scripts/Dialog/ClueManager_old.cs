using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueManager : MonoBehaviour
{
    public GameObject Clue1;
    public GameObject Clue2;
    public GameObject Clue3;
    public GameObject Clue4;
    public GameObject Clue5;
    public GameObject Clue6;
    public GameObject Clue7;
    public GameObject Clue8;
    public GameObject Clue9;
    public GameObject Clue10;

    int Progression;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Progression = ProgressKeeper.instance.Progression;
        switch (Progression)
        {
        case 10:
            Clue1.gameObject.SetActive(true);
            break;
        case 11:
            Clue1.gameObject.SetActive(false);
            break;
        default:
            //print ("Incorrect intelligence level.");
            break;
        }
    }
}
