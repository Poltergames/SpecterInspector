using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class CloakHouse : MonoBehaviour
{
	//public GameObject House;
	public GameObject Player;
	Collider House_c, Player_c;
	public float minimum = 1.0F;
	public float maximum =  0.0F;
	bool faded = false;
	GameObject RainAudio;

	//fading alpha method stolen from internet
	IEnumerator FadeTo(float aValue, float aTime, float lumens)
	{
		//dim lights
		foreach(Transform child in this.transform)
		{
            if (null != child.GetComponent<HDAdditionalLightData>())
            {
                child.GetComponent<HDAdditionalLightData>().intensity = lumens;
				//print(child.GetComponent<Light>().intensity );
            }
        }

		Renderer renderToFade; // The renderer you wish to edit materials on
		renderToFade = transform.GetComponent<Renderer>();
		for (float t = 0.0f; t <= 1.1f; t += Time.deltaTime / aTime)
		{
			int j = 0;
			foreach (Material mat in renderToFade.materials ){
				j++;
				if( 10 < j ) //stops it from running through thousands of materials
				break;
				//skips transparent materials
				if( 0.5f < mat.color.a ){

					float alpha = Mathf.Abs(aValue-1.0f);
					Color invisible = new Color (mat.color.r, mat.color.b, mat.color.g, Mathf.Lerp(alpha,aValue,t));
					mat.SetColor("_BaseColor", invisible);
					//Debug.Log(mat.color.a);
				}
			}
			yield return new WaitForFixedUpdate();
		}

		/*
		float alpha = transform.GetComponent<Renderer>().material.color.a;

		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
		Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha,aValue,t));

		transform.GetComponent<Renderer>().material.color = newColor;
		}*/
	}

	void Start()
	{
		//TranMat = (Material)Resources.Load("TranMat", typeof(Material));
		//OpMat = (Material)Resources.Load("OpMat", typeof(Material));
		//rend = GetComponent<Renderer> ();

		Player = GameObject.Find("Player");
		RainAudio = GameObject.Find("Rain Audio");
		House_c = gameObject.GetComponent<Collider>();
		Player_c = Player.GetComponent<Collider>();
		//this.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f); // last value sets transparency 0-1
	}
	void Update()
	{/*
		//float lerp = Mathf.PingPong(Time.time, 2.0f) / 2.0f;
		// rend.material.Lerp(TranMat, OpMat, lerp);

		//checks if house is intersected and if it has already faded it
		if (House_c.bounds.Intersects(Player_c.bounds) && false == faded)
		{
		//House.transform.localScale = new Vector3(0, 0, 0); //flickers
		//House.SetActive(false);							//this one never returns
		//gameObject.GetComponent<Renderer>().enabled = false; //makes object disappear instantly
		//this.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f); // last value sets transparency 0-1
		StopAllCoroutines();
		Debug.Log("Fading");
		StartCoroutine(FadeTo(0.0f, 1.0f));
		faded = true;

		//checks if house is no longer intersected and if it is currently faded
		}else if ( !House_c.bounds.Intersects(Player_c.bounds) &&  true == faded )
		{
		StopAllCoroutines();
		StartCoroutine(FadeTo(1.0f, 1.0f));
		faded = false;
		}*/
	}
	void OnTriggerEnter (Collider Player_c){

		if( "Player" == Player_c.GetComponent<Collider>().gameObject.name){

			GameObject Player = Player_c.gameObject;
			Player.GetComponent<Renderer>().enabled = true;
			Player.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = false;

			Player.SendMessage("changeRainTrack", true);
			StopAllCoroutines();
			StartCoroutine(FadeTo(0.0f, 1.0f, 500f));
		}
	}
	void OnTriggerExit (Collider Player_c){

		if( "Player" == Player_c.GetComponent<Collider>().gameObject.name){

			GameObject Player = Player_c.gameObject;
			Player.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = true;
			Player.GetComponent<Renderer>().enabled = false;
			Player.SendMessage("changeRainTrack", false);
			StopAllCoroutines();
			StartCoroutine(FadeTo(1.0f, 1.0f, 5000f));
		}
	}

}
