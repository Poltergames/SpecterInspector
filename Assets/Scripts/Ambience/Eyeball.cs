using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Eyeball : MonoBehaviour
{

    //looking
    public GameObject target;
    public float rotSpeed = 10.0f;
    public float movSpeed = 0.5f;
    public float newTarget = 2f;
    Vector3 hover;

    public float height = 10;

    //movement
    float maxX;
	float minX;
	float maxZ;
	float minZ;

    float xTarget;
    float zTarget;

    Vector3 moveTo;
    public bool targeting = true;
    public bool roaming = true;

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(changeTarget());
    }
    void FixedUpdate()
    {
        //random roaming
        if(roaming){
            transform.position = Vector3.Lerp(transform.position, moveTo, movSpeed * Time.deltaTime);
        }
        //looking at nearest NPC
        if(targeting){
    		target = FindClosestEnemy();
    		if( null != target){

    			hover = (target.transform.position);

                //looks at target
    			var newRotation = Quaternion.LookRotation(hover - transform.position);
    			transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotSpeed * Time.deltaTime);
    		}
        }
    }
    IEnumerator changeTarget(){
        while(true){
            yield return new WaitForSeconds(newTarget);
            maxX = Boundary.instance.maxX-10;
            minX = Boundary.instance.minX+10;
            maxZ = Boundary.instance.maxZ-10;
            minZ = Boundary.instance.minZ+10;
            xTarget = Random.Range(minX, maxX);
            zTarget = Random.Range(minZ, maxZ);

            moveTo = new Vector3(xTarget, height, zTarget);
            //print(moveTo.y);
        }
        yield return new WaitForFixedUpdate();
    }

    public GameObject FindClosestEnemy()
    {
        //GameObject[] NPC;
        //NPC = GameObject.FindGameObjectsWithTag("NPC");
        GameObject[] gos = GameObject.FindGameObjectsWithTag("EyeballTarget");
        //GameObject[] gos = NPC.Concat(decoy).ToArray();

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
