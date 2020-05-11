using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateMachine : MonoBehaviour
{

    public enum PerformAction
    {
        WAIT,
        TAKEACTION,
        PERFORMACTION
    }
    public PerformAction battleStates;

    public List<HandleTurn> PerformList = new List<HandleTurn>();

    public List<GameObject> HeroesInCombat = new List<GameObject>();

    public List<GameObject> EnemiesInCombat = new List<GameObject>();

    void Start()
    {
        battleStates = PerformAction.WAIT;
        EnemiesInCombat.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        HeroesInCombat.AddRange(GameObject.FindGameObjectsWithTag("Hero"));
    }

    // Update is called once per frame
    void Update()
    {
        switch (battleStates)
        {
            case (PerformAction.WAIT):

                break;
            case (PerformAction.TAKEACTION):

                break;
            case (PerformAction.PERFORMACTION):

                break;

        }
    }

    public void CollectActions(HandleTurn input)
    {
        PerformList.Add(input);
    }
   
}
