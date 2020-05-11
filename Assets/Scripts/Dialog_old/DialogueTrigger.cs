using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
	
	public GameObject Player;
    Collider House_c, Player_c;
	bool started = false;
	
	public AudioSource speaking;

	public Dialogue dialogue;
	
	void Start(){
		
		speaking = GetComponent<AudioSource>();
		Player = GameObject.Find("Player");
		House_c = gameObject.GetComponent<Collider>();
		Player_c = Player.GetComponent<Collider>();
		speaking.Stop();
	}

	public void TriggerDialogue ()
	{
		//FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}
	void Update(){
		
		if (House_c.bounds.Intersects(Player_c.bounds) && false == started)
		{
			FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
			started = true;
			speaking.Play();
		}
	}

}
