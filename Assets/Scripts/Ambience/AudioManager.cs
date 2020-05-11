using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Create another public void method to create a new music track, call that method when you want it played, once you're done playing the mayor's theme, to call the Overworld method.

    public AudioSource BGM;
    public AudioClip combatTrack;
    public AudioClip Overworld;
    private AudioManager theAM;
    public bool localCombatting = false;

    // Start is called before the first frame update
    void Start()
    {
        {
            if (BGM == null)
                BGM = GetComponent<AudioSource>();
        }
    }


    public void combatMusic()
    {
        StartCoroutine(changeTrack(combatTrack));
    }

    public void overworldMusic()
    {
        StartCoroutine(changeTrack(Overworld));
    }
    // Update is called once per frame
   /* void Update()
    {
        if (localCombatting != InCombat.instance.combatting)
        {
            localCombatting = InCombat.instance.combatting;
            //theAM.ChangeBGM(newTrack);
            if (localCombatting){
                StartCoroutine(changeTrack(combatTrack));
            } else
            {
                StartCoroutine(changeTrack(Overworld));
            }
            Debug.Log("You've changed the music.");
        }
        //else
        //{
          //  theAM.ChangeBGM(Overworld);
        //}
    }

    /*public void ChangeBGM(AudioClip music)
    {
        if (BGM.clip.name == music.name)
            return;

        BGM.Stop();
        BGM.clip = music;
        BGM.Play();
    }

   /* public void playSound()
    {
        if (!BGM.isPlaying)
        {
            BGM.volume = 1;
            BGM.Play();
        }
    }

    public void stopSound()
    {
        if (BGM.isPlaying)
        {
            BGM.Stop();
        }
    }
    */

    IEnumerator changeTrack(AudioClip newTrack)
    {
        float newVolume = 0f;
        print("Coroutine started");

        while (0.01 < BGM.volume)
        {
            BGM.volume = Mathf.Lerp(BGM.volume, 0.0f, Time.deltaTime*2);
            //print(BGM.volume);
            yield return new WaitForFixedUpdate();
        }

        BGM.clip = newTrack;
        BGM.Play();

        newVolume = 1.0f;
        //print("You're entering next one");
        while (0.19 > BGM.volume)
        {
            BGM.volume = Mathf.Lerp(BGM.volume, 0.2f, Time.deltaTime*2);
            yield return new WaitForFixedUpdate();
        }




        yield return new WaitForFixedUpdate();
    }


}
