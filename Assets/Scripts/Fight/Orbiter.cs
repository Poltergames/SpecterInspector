using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbiter : MonoBehaviour
{
    public GameObject battleManager;
    public Projectile script;
    public GameObject target;

    public float RateOfTurn = 20f;
    public float RateOfForward = 20f;
    public float RateOfSpin = 8f;
    public float orbitAngle = 90f;
	//bool running = true;
    // Start is called before the first frame update
    void Start()
    {
        battleManager = GameObject.Find("battleManager");
        //script = battleManager.GetComponent<inBattle>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if( transform.position.y < 400  && QualitySettings.GetQualityLevel() > 0){
            var newRotation = Quaternion.LookRotation((target.transform.position - transform.position).normalized);
            newRotation *= Quaternion.Euler(Vector3.up * orbitAngle);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, RateOfTurn * Time.deltaTime);


    		transform.position += transform.forward * RateOfForward * Time.deltaTime;
            //Rotate fireballs
            Transform Yellow = gameObject.transform.GetChild(0);
            Transform Orange = gameObject.transform.GetChild(1);
            Transform Red = gameObject.transform.GetChild(2);

            Yellow.transform.Rotate( RateOfSpin, 0, 0);
            Orange.transform.Rotate( -1f * RateOfSpin, 0, 0);
            Red.transform.Rotate( RateOfSpin, 0, 0);
        }
    }


}
