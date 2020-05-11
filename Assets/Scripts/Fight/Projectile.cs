using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject battleManager;
    public Projectile script;
    public GameObject target;

    public float RateOfTurn = 1.5f;
    public float RateOfForward = 15f;
    public float RateOfSpin = 8f;
    public float lifeTime = 15f;

    public static Projectile instance;
    //bool running = true;
    // Start is called before the first frame update
    void Start()
    {
        if( transform.position.y > 200){
            instance = this;
            //print("wfdjndfsjn");
        }
        battleManager = GameObject.Find("battleManager");
        //script = battleManager.GetComponent<inBattle>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //RateOfForward = 15f;
        //print(transform.position.y );
        if( transform.position.y < 400){
            var newRotation = Quaternion.LookRotation((target.transform.position - transform.position).normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, RateOfTurn * Time.deltaTime);

            transform.position += transform.forward * RateOfForward * Time.deltaTime;
            StartCoroutine(timeLimit(lifeTime));
        }
        if( transform.position.y < 90 ){
            Destroy(gameObject);
        }
        //deletes fireballs that turn around to come back
        if( transform.rotation.eulerAngles.y > 180 ||  transform.rotation.eulerAngles.y < 0){
            Destroy(gameObject);
        }
        //Rotate fireballs
        Transform Yellow = gameObject.transform.GetChild(0);
        Transform Orange = gameObject.transform.GetChild(1);
        Transform Red = gameObject.transform.GetChild(2);

        Yellow.transform.Rotate( RateOfSpin, 0, 0);
        Orange.transform.Rotate( -1f * RateOfSpin, 0, 0);
        Red.transform.Rotate( RateOfSpin, 0, 0);
    }
    void OnTriggerEnter (Collider target){

        if( "Player" == target.GetComponent<Collider>().gameObject.name){
            //print("OW!");
            InCombat.instance.playerHealth -=1;
            //print("Player health: " + InCombat.instance.playerHealth);
        }
        if( transform.position.y > 90){
            Destroy(gameObject);
        }
    }

    //float aValue, float aTime, GameObject PlayerObj

    IEnumerator timeLimit( float limit ){
        yield return new WaitForSeconds(limit);

        Destroy(gameObject);
    }


}
