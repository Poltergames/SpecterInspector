using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
	public GameObject Player;

	public float maxHeight = 2.5f;
	public float minHeight = 1.5f;

	public float maxX = 70f;
	public float minX = -50f;

	public float maxZ = 40f;
	public float minZ = -86f;

	float xWidth = 120f;
	float zWidth = 126f;

	public static Boundary instance;

	bool fullScreen;
    // Start is called before the first frame update
    void Start()
    {
		xWidth = maxX - minX;
		zWidth = maxZ - minZ;
        Player = GameObject.Find("Player");
		instance = this;


        AudioListener.volume = PlayerPrefs.GetFloat("volume");
        fullScreen = ( PlayerPrefs.GetInt("fullscreen") == 1);
        Screen.SetResolution(PlayerPrefs.GetInt("xRes"), PlayerPrefs.GetInt("yRes"), fullScreen);
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("quality"));
    }

    // Update is called once per frame
    void Update()
    {
        //X axis (forward back)
		if( maxX < transform.position.x ){
			transform.position = new Vector3(transform.position.x - xWidth, transform.position.y, transform.position.z);
		}else if( minX > transform.position.x ){
			transform.position = new Vector3(transform.position.x + xWidth, transform.position.y, transform.position.z);
		}
		//Y axis (up down)
		if( maxHeight < transform.position.y ){
			//transform.position = new Vector3(transform.position.x, transform.position.y-120, transform.position.z);
			transform.position += Vector3.up * ((transform.position.y) * -1) * Time.deltaTime;
		}
		if( minHeight > transform.position.y ){
			//transform.position = new Vector3(transform.position.x, transform.position.y+120, transform.position.z);
			transform.position += Vector3.up * ((transform.position.y-4) * (-0.25f)) * Time.deltaTime;
		}
		//Z axis (left right)
		if( maxZ < transform.position.z ){
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - zWidth);
		}else if( minZ > transform.position.z ){
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + zWidth);
		}
    }
}
