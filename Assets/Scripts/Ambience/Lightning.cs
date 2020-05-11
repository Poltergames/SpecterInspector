using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Lightning : MonoBehaviour
{
    float randomWait = 5f;
    AudioSource audioSource;
    public GameObject dirLight;

    public float bright = 1f;
    public float duration = 1f;
    public float delay = 1f;
    public float dim = 0.1f;

    public AudioClip thunder1;
    public AudioClip thunder2;
    public AudioClip thunder3;
    public AudioClip thunder4;

    AudioClip[] thunders;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        thunders = new AudioClip[] {thunder1, thunder2, thunder3, thunder4};
        StartCoroutine(waitForLight());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator waitForLight(){
        while( true){
            yield return new WaitForSeconds(randomWait);
            randomWait = Random.Range(30f, 120f);
            StartCoroutine(flash());
            yield return new WaitForFixedUpdate();
        }
    }

    public IEnumerator flash(){

        print("flash");
        //lumens = Random.Range;
        dirLight.GetComponent<HDAdditionalLightData>().intensity = bright;
        yield return new WaitForSeconds(duration);
        dirLight.GetComponent<HDAdditionalLightData>().intensity = dim;

        yield return new WaitForSeconds(delay);
        audioSource.clip = thunders[Random.Range(1,4)];
        audioSource.Play();
        yield return new WaitForFixedUpdate();
    }
}
