using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : MonoBehaviour
{
    public GameObject battleManager;
    public Projectile script;
    public GameObject target;

    public float RateOfTurn = 3f;
    public float RateOfForward = 10.1f;
    public float RateOfSpin = 8f;

    public bool offensive = false;

    public static Pet instance;
	//bool running = true;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        battleManager = GameObject.Find("battleManager");
        instance = this;
        //script = battleManager.GetComponent<inBattle>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if( offensive == true && null != FindClosestEnemy() ){

            target = FindClosestEnemy();
            RateOfTurn = 30f;
            RateOfForward = 30f;
            gameObject.GetComponent<TrailRenderer>().enabled = true;
        }else{

            target = GameObject.Find("Player");
            RateOfTurn = 3f;
            RateOfForward = 10.1f;
            gameObject.GetComponent<TrailRenderer>().enabled = false;
        }

        var newRotation = Quaternion.LookRotation((target.transform.position - transform.position).normalized);
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
    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Projectile");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
