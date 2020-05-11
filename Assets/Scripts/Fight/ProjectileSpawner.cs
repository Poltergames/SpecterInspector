using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public bool spawning = false;// {get; set;} = false;

    float XRand = 0;
    float YRand = 0;
    float rotRand = 0;
    public Transform prefab;
    GameObject player;

    public Material red;
    public Material orange;
    public Material yellow;

    public static ProjectileSpawner instance;
    public float rate = 1;
    public int cheat = 0;
    //  19  41  51

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnArmy());
		instance = this;
        player =  GameObject.Find("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {

    }
    IEnumerator spawnArmy(){

		while(true){
            //print((Mathf.Abs(ProgressKeeper.instance.Progression-26))/7);
            //rate = (Mathf.Abs(ProgressKeeper.instance.Progression-26))/7;

            yield return new WaitForSeconds(rate);

            switch (ProgressKeeper.instance.Progression + cheat)
            {
            case 41:
                rate = 0.5f;
                break;
            case 51:
                rate = 1f/3f;
                break;

            default:
                if( ProgressKeeper.instance.Progression + cheat > 100){
                    rate = 0f;
                }else{
                    rate = 1f;
                }
                break;
            }

            if(spawning){

                XRand = Random.Range(-10.0f, 10.0f);
                YRand = Random.Range(-6.0f, 6.0f);
                //-10 to 10
                //0 to 20
                //0 to 180
                rotRand = (((XRand + 10f) * 9f) - 180f) * -1f;

    			Instantiate(prefab, new Vector3(-47, 105 + YRand, 0 + XRand), Quaternion.Euler(0, rotRand, 0) );
    			//originally a bug the vast numbers of escapees was deemed a feature
            }
            yield return new WaitForFixedUpdate();
		}
	}
    /*IEnumerator stealArmy(){
        while(true){
            if(spawning){
                yield return new WaitForSeconds(1f);
                GameObject newProj = FindClosestEnemy();
                newProj.tag = "Projectile";

                Transform Yellow = newProj.transform.GetChild(0);
                Transform Orange = newProj.transform.GetChild(1);
                Transform Red = newProj.transform.GetChild(2);

                Red.GetComponent<Renderer>().material = red;
                Orange.GetComponent<Renderer>().material = orange;
                Yellow.GetComponent<Renderer>().material = yellow;

                var newRotation = Quaternion.LookRotation((player.transform.position - newProj.transform.position).normalized);
                newProj.transform.rotation = Quaternion.Slerp(newProj.transform.rotation, newRotation, 50 * Time.deltaTime);

                newProj.GetComponent<Orbiter>().enabled = false;
                newProj.GetComponent<Projectile>().enabled = true;

        //convert materials
            }
            yield return new WaitForFixedUpdate();
        }
    }*/

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Orbiter");
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
