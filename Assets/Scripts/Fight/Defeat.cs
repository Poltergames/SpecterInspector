using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defeat : MonoBehaviour
{
    GameObject Player;
    GameObject Enemy;
    public static Defeat instance;

    public GameObject incorpButton;
    public GameObject possesButton;
    public GameObject astralButton;
    public GameObject MusicSwitch;
    public GameObject healthBarUI;

    Vector3 playerOldPos;
    Vector3 EnemyOldPos;

    Quaternion playerOldRot;
    Quaternion EnemyOldRot;

    Quaternion playerBattleRot = Quaternion.Euler(0, 270, 0);
    Quaternion enemyBattleRot = Quaternion.Euler(0, 90, 0);

    public GameObject MainCamObject;
    protected Camera MainCam;
    public GameObject BattleCamObject;
    protected Camera BattleCam;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        instance = this;
        MainCam = MainCamObject.GetComponent<Camera>();
        BattleCam = BattleCamObject.GetComponent<Camera>();
        MusicSwitch = GameObject.Find("combatMusic");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void battleOver(int enemyHealth){

        Player.GetComponent<Renderer>().enabled = false;
        Player.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = true;

        MusicSwitch.GetComponent<AudioManager>().overworldMusic();

        Player = CombatPositions.instance.Player;
        Enemy = CombatPositions.instance.Enemy;

        if(0 >= enemyHealth){
           // Enemy.SetActive(false);
            //ProgressKeeper.instance.Progression += 1;
            ProgressKeeper.instance.ProgressInc();
        }
        print("battleOVerhasbeenrun");
        ProjectileSpawner.instance.spawning = false;
        InCombat.instance.combatting = false;

        //enables movement
        Player.GetComponent<PlayerMovement>().enabled = true;
        Player.GetComponent<Boundary>().enabled = true;

        //switches camera
        BattleCam.enabled = false;
        MainCam.enabled = true;

        //deactivates hud
        incorpButton.SetActive(false);
        possesButton.SetActive(false);
        astralButton.SetActive(false);
        healthBarUI.SetActive(false);

        //saves positions
        playerOldPos = Player.transform.position;
        EnemyOldPos = Enemy.transform.position;
        //Saves rotations
        playerOldRot = Player.transform.rotation;
        EnemyOldRot = Enemy.transform.rotation;

        StopAllCoroutines();
        StartCoroutine(battlePositions(5));
    }

    IEnumerator battlePositions(int atime){
        //
        float smooth = 5.0f;
        for (float t = 0.0f; t <= 1.1f; t += Time.deltaTime)
        {
            //slows down last bit so they rise dramatically
            if( 0.95f < t ){
                t -= (Time.deltaTime*0.95f);
            }
            //Rotation
            //Player.transform.rotation = Quaternion.Slerp(playerBattleRot, CombatPositions.instance.playerOldRot, t);
            Enemy.transform.rotation = Quaternion.Slerp(enemyBattleRot, CombatPositions.instance.EnemyOldRot, t);

            //teleports participatns
            Vector3 playerTar = CombatPositions.instance.playerOldPos;
            playerTar = new Vector3( playerTar.x+2, playerTar.y, playerTar.z );
            Vector3 enemyTar = CombatPositions.instance.EnemyOldPos;

            Player.transform.position = Vector3.Lerp(playerOldPos, playerTar, t);
            Enemy.transform.position = Vector3.Lerp(EnemyOldPos, enemyTar, t);
            //Debug.Log(t);
        }

        yield return new WaitForFixedUpdate();
    }
}
