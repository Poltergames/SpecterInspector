﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    public InspectorSpector playableCharacter;

    public enum TurnState
    {
        PROCESSING,
        ADDTOLIST,
        WAITING,
        SELECTING,
        ACTION,
        DEAD
    }

    public TurnState currentState;

    //Active Turn Battle - Bar
    private float cur_cooldown  = 0f;
    private float max_cooldown = 20f;
    public Image ProgressBar; 
    void Start()
    {
        currentState = TurnState.PROCESSING;
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case (TurnState.PROCESSING):
                UpgradeActiveTurnBattle();
                break;

            case (TurnState.ADDTOLIST):

                break;
            case (TurnState.WAITING):

                break;

            case (TurnState.SELECTING):

                break;
            case (TurnState.ACTION):

                break;

            case (TurnState.DEAD):

                break; 
                
        }

        void UpgradeActiveTurnBattle()
        {
            cur_cooldown = cur_cooldown + Time.deltaTime;
            float calc_cooldown = cur_cooldown / max_cooldown;
            ProgressBar.transform.localScale = new Vector3(Mathf.Clamp(calc_cooldown, 0, 1), ProgressBar.transform.localScale.y, ProgressBar.transform.localScale.z);
            if(cur_cooldown >= max_cooldown)
            {
                currentState = TurnState.ADDTOLIST;
            }
        }
    }
}
