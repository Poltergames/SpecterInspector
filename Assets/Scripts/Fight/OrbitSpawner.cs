using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitSpawner : MonoBehaviour
{
    public bool spawning = true;// {get; set;} = false;

    float XRand = 0;
    float YRand = 0;
    public float xRange = 10f;
    public float yRange = 6f;
    float rotRand = 0;
    public Transform prefab;

    public static OrbitSpawner instance;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnArmy());
		instance = this;
    }

    // Update is called once per frame
    void LateUpdate()
    {
    }

	IEnumerator spawnArmy(){

        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Orbiter");

		while(gos.Length < 400){
            if(spawning){
                gos = GameObject.FindGameObjectsWithTag("Orbiter");
                //print(gos.Length);
                //print( 100f/(400f-gos.Length));
                yield return new WaitForSeconds( 100f/(400f-gos.Length) );
                XRand = Random.Range(-xRange, xRange);
                YRand = Random.Range(-yRange, yRange);
                //-10 to 10
                //0 to 20
                //0 to 180
                rotRand = (((XRand + 10f) * 9f) - 180f) * -1f;

    			Instantiate(prefab, new Vector3(146, 130 + YRand, 0 + XRand), Quaternion.Euler(0, rotRand, 0), gameObject.transform );
    			//originally a bug the vast numbers of escapees was deemed a feature
            }
            yield return new WaitForFixedUpdate();
		}
	}
}
