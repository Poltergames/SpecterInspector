using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatCollide : MonoBehaviour
{
	public GameObject Player;
	//public battlePrep script;
	GameObject battlePrep;

	private CombatPositions CombatPositions;


    void Start()
    {
		Player = GameObject.Find("Player");
		CombatPositions = GameObject.Find("battlePrep").GetComponent<CombatPositions>();
		/*
		Enemy_c = gameObject.GetComponent<Collider>();
		Player_c = Player.GetComponent<Collider>();
*/
		//BattleCamera = GameObject.Find("BattleCamera");
		//script = BattleCamera.GetComponent<battleScript>;
	}
	void Update()
    {
		/*if (Enemy_c.bounds.Intersects(Player_c.bounds)){					//this one never returns
			SceneManager.LoadScene ("Combat Scenario");
		}*/
	}

	void OnTriggerEnter (Collider collision){
		print("combat collide");
		if( "Player" == collision.GetComponent<Collider>().gameObject.name){

			//script.battleStart(Player, gameObject);
			CombatPositions.battleStart(Player, gameObject);
		}
	}
}
