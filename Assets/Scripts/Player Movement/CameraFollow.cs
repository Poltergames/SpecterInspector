using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	//public Transform target;

	public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
		transform.rotation = Quaternion.Euler(30, -90, 0);
		gameObject.GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
    }

    // Update is called once per frame
    void Update()
    {
		print(gameObject.GetComponent<Camera>().depthTextureMode);
       transform.position = new Vector3 (Player.transform.position.x + 25, Player.transform.position.y + 18, Player.transform.position.z);


		/*
		//private var lastObject : Transform;
		private Transform lastObject;
		function Update () {
			try{
				lastObject.renderer.material.color.a = 1;
			}catch(e){}
			public RaycastHit hit;
			//var hit : RaycastHit;
			if (!Physics.Linecast (transform.position, target.position, hit))
				{
				lastObject=hit.transform;
				lastObject.renderer.material.color.a = 0.2;
			}
		}*/
    }
}
