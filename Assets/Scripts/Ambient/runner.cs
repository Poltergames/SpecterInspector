using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runner : MonoBehaviour
{
	public GameObject target;
	//bool running = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		//print(transform.position.y );
		if( transform.position.y > -1){
			
			//Look
			var newRotation = Quaternion.LookRotation(target.transform.position - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 5 * Time.deltaTime);
			
			transform.position += transform.forward * 20 * Time.deltaTime;
			//transform.position += transform.right * Random.Range(-5, 5) * Time.deltaTime;
		}
    }
	void OnTriggerEnter (Collider target){
		
		Destroy(gameObject);
	}
}