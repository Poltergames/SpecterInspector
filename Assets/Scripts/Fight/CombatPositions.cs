using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatPositions : MonoBehaviour
{
    public GameObject Player {get; set;} = null;
    public GameObject Pet;
    public GameObject Enemy;// {get; set;}
    public GameObject MusicSwitch;

    public GameObject MainCamObject;
    protected Camera MainCam;
    public GameObject BattleCamObject;
    protected Camera BattleCam;

    public GameObject incorpButton;
    public GameObject possesButton;
    public GameObject astralButton;
    public GameObject healthBarUI;

    public Quaternion playerBattleRot = Quaternion.Euler(0, 270, 0);
    public Quaternion enemyBattleRot = Quaternion.Euler(0, 90, 0);

    public Vector3 playerOldPos;
    public Vector3 EnemyOldPos;

    public Quaternion playerOldRot;
    public Quaternion EnemyOldRot;

    public static CombatPositions instance;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Pet = GameObject.Find("Pet");
        instance = this;
        MainCam = MainCamObject.GetComponent<Camera>();
        BattleCam = BattleCamObject.GetComponent<Camera>();
        //MusicSwitch = GameObject.Find("Audio Source");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void battleStart(GameObject refPlayer, GameObject refEnemy){

        /*Player.GetComponent<Renderer>().enabled = true;
        Player.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = false;*/

        MusicSwitch.GetComponent<AudioManager>().combatMusic();

        InCombat.instance.playerHealth = 5;
        InCombat.instance.enemyHealth = 5;

        print("battle positions");
        Player = refPlayer;
        Enemy = refEnemy;

        //disables movement
        Player.GetComponent<PlayerMovement>().enabled = false;
        Player.GetComponent<Boundary>().enabled = false;

        //switches camera
        BattleCam.enabled = true;
        MainCam.enabled = false;

        //activates hud
        incorpButton.SetActive(true);
        possesButton.SetActive(true);
        astralButton.SetActive(true);
        healthBarUI.SetActive(true);

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
        //float smooth = 5.0f;
        for (float t = 0.0f; t <=1.10f; t += Time.deltaTime)
        {
            //slows down last bit so they rise dramatically
            if( 0.95f < t ){
                t -= (Time.deltaTime*0.95f);
            }
            //Rotation
            Player.transform.rotation = Quaternion.Slerp(playerOldRot, playerBattleRot, t);
            Enemy.transform.rotation = Quaternion.Slerp(EnemyOldRot, enemyBattleRot, t);

            //teleports participatns
            Vector3 playerTar = new Vector3 (-5f, 100f, 0f);
            Vector3 enemyTar = new Vector3 (-47f, 100f, 0f);

            Player.transform.position = Vector3.Lerp(playerOldPos, playerTar, t);
            Enemy.transform.position = Vector3.Lerp(EnemyOldPos, enemyTar, t);
            Pet.transform.position = new Vector3 (-5f, 97f, 0f);
            //Debug.Log(t);
            yield return new WaitForFixedUpdate();
        }
        ProjectileSpawner.instance.spawning = true;
        InCombat.instance.combatting = true;

        Player.GetComponent<Renderer>().enabled = true;
        Player.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = false;

        yield return new WaitForFixedUpdate();
    }
}
