using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyState : MonoBehaviour
{
    private BattleStateMachine BSM;
    public Enemy Enemy;

    public enum TurnState
    {
        PROCESSING,
        CHOOSEACTION,
        WAITING,
        ACTION,
        DEAD
    }

    public TurnState currentState;
    //Active Turn Battle - Bar
    private float cur_cooldown = 0f;
    private float max_cooldown = 5f;
    //gameobject's movement

    private Vector3 startposition;

    void Start()
    {
        currentState = TurnState.PROCESSING;
        BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
        startposition = transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        //Switch case that determines what part the turns are in.
        switch (currentState)
        {
            case (TurnState.PROCESSING):
                UpgradeActiveTurnBattle();
                break;


            case (TurnState.CHOOSEACTION):
                ChooseAction();
                currentState = TurnState.WAITING;
                break;

            case (TurnState.WAITING):
                //idle state
                break;


            case (TurnState.ACTION):

                break;

            case (TurnState.DEAD):

                break;

        }
        //responsible for managing the bars of the enemy
        void UpgradeActiveTurnBattle()
        {
            cur_cooldown = cur_cooldown + Time.deltaTime;

            if (cur_cooldown >= max_cooldown)
            {
                currentState = TurnState.CHOOSEACTION;
            }
        }
    }

    void ChooseAction()
    {
        HandleTurn myAttack = new HandleTurn();
        myAttack.Attacker = Enemy.name;
        //myAttack.AttackersGameObject = this.gameObject;
        myAttack.AttackersTarget = BSM.HeroesInCombat[Random.Range(0, BSM.HeroesInCombat.Count)];
        BSM.CollectActions (myAttack);
    }

    
}