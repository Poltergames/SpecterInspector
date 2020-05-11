using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffect2 : MonoBehaviour {

    public float spawnEffectTime = 2;
    public float pause = 1;
    public AnimationCurve fadeIn;
	bool done = false;
	public GameObject Player;
    float timer = 0;
    Renderer _renderer;
	
    int shaderProperty;

	void Start ()
    {
		
        shaderProperty = Shader.PropertyToID("_cutoff");
        _renderer = GetComponent<Renderer>();
		Player = GameObject.Find("Player");
		
    }
	
	void Update ()
    {
		if(false == done){
			rings();
		}
        
    }
	
	void rings(){
		
		if (timer < spawnEffectTime + pause)
        {
            timer += Time.deltaTime;
        }
        else
        {
            //timer = 0;
			done = true;
			Player.gameObject.GetComponent<PlayerMovement>().enabled = true;
        }


        _renderer.material.SetFloat(shaderProperty, fadeIn.Evaluate( Mathf.InverseLerp(0, spawnEffectTime, timer)));
        
		//done = true;
		
	}
}
