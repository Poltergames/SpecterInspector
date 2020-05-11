using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPS : MonoBehaviour
{ 

    enum elements { Incorporeal = 1, Possession = 2, AstralRecovery = 3 }

    private int playerChoose = 1;
    private int botChoose = -1;

    private bool playersTurn = true;


    void Update()
    {
        if (playersTurn && playerChoose == -1) return;
        
        else
        {
            BotChoose();
            CheckWinner();
            playerChoose = -1;
            playersTurn = true; 
        }

    }

    void CheckWinner()
    {
        if(playerChoose == botChoose )
        {
            Debug.Log("You've struck the enemy.");
        }

        if (playerChoose == (int)elements.Incorporeal && botChoose == (int)elements.Incorporeal)
        {
            //Nothing happens.
            Debug.Log("Nothing happens.");
        }

        else if (playerChoose == (int)elements.Possession && botChoose == (int)elements.Incorporeal)
        {
            //The player misses. 
            Debug.Log("You've missed your attack!");

        } else if (playerChoose == (int)elements.AstralRecovery && botChoose == (int)elements.Possession)
        {
            //Player takes double damage.
            Debug.Log("You've taken double damage.");

        }

        


    }
    
    public void PlayerChoose(int choose)
    {
        playerChoose = choose;
        playersTurn = false; //It is now the AI's turn.
    }
    public void BotChoose()
    {
       botChoose  = Random.Range(1, 3);
        
    }
}
