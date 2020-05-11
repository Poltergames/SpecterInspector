using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int moveSpeed;
    public GameObject mainCam;

    void Start()
    {
        moveSpeed = 10;
        mainCam.transform.rotation = Quaternion.Euler(30, -90, 0);
		//this.gameObject.GetComponent<PlayerMovement>().enabled = false;
    }

    void Update(){
        //cheat key
        if ( Input.GetKeyDown( KeyCode.Equals ) )
        {
            print("cheater");
            ProgressKeeper.instance.ProgressInc();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mainCam.transform.position = new Vector3 (gameObject.transform.position.x + 25, gameObject.transform.position.y + 18, gameObject.transform.position.z);

        if (Input.GetKey("w"))
        {
            //gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 5);
			transform.position += Vector3.left * moveSpeed * Time.deltaTime;
			//transform.eulerAngles = new Vector3(0, 270, 0);
			float smooth = 5.0f;
			Quaternion target = Quaternion.Euler(0, 270, 0);
			transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * smooth);
        }
        if (Input.GetKey("s"))
        {
			transform.position += Vector3.right * moveSpeed * Time.deltaTime;
			//transform.eulerAngles = new Vector3(0, 90, 0);
			float smooth = 5.0f;
			Quaternion target = Quaternion.Euler(0, 90, 0);
			transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * smooth);
        }
		if (Input.GetKey("d"))
        {
			transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
			//transform.eulerAngles = new Vector3(0, 0, 0);
			float smooth = 5.0f;
			Quaternion target = Quaternion.Euler(0, 0, 0);
			transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * smooth);
        }

        if (Input.GetKey("a"))
        {
			transform.position += Vector3.back * moveSpeed * Time.deltaTime;
			//transform.eulerAngles = new Vector3(0, 180, 0);
			float smooth = 5.0f;
			Quaternion target = Quaternion.Euler(0, 180, 0);
			transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * smooth);
        }
		if (Input.GetKey("space") && 65 > this.transform.position.y)
        {
			transform.position += Vector3.up * moveSpeed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.LeftControl) && -65 < this.transform.position.y)
        {
			transform.position += Vector3.down * moveSpeed * Time.deltaTime;
		}
   // transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed, 0f, Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed);
    }
}
