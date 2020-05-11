using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public Transform prefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnArmy());
		//print("test");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
    }
	
	IEnumerator spawnArmy(){
				
		while(true){
			
			Instantiate(prefab, new Vector3(62, 0, 2), Quaternion.identity);
			//originally a bug the vast numbers of escapees was deemed a feature
			yield return new WaitForSeconds(5);
		}
	}
}
