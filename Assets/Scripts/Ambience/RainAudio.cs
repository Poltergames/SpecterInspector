using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainAudio : MonoBehaviour
{

    public AudioSource RainAudioSource;
    public AudioClip insideRain;
    public AudioClip outsideRain;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeRainTrack(bool inside){

        if(inside){
            RainAudioSource.clip = insideRain;
            RainAudioSource.volume = 0.8f;
            RainAudioSource.Play();
        }else{
            RainAudioSource.clip = outsideRain;
            RainAudioSource.volume = 0.2f;
            RainAudioSource.Play();
        }
    }
}
