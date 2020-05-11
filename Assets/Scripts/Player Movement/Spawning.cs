using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
	
	public float spawnEffectTime = 2;
    public float pause = 1;
    public AnimationCurve fadeIn;

    ParticleSystem ps;
    float timer = 0;
    Renderer _renderer;
	Shader dissolve;
    int shaderProperty;
	
    // Start is called before the first frame update
    void Start()
    {
		//disables movement and hides the inspector
        this.gameObject.GetComponent<PlayerMovement>().enabled = false;
		Renderer[] rs = GetComponentsInChildren<Renderer>();
		foreach(Renderer r in rs){
			//r.enabled = false;
		}
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
