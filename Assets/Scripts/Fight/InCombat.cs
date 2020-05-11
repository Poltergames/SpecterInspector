using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InCombat : MonoBehaviour
{

    public GameObject Player = null;// {get; set;}
    GameObject Enemy;// {get; set;}
    public GameObject Clone;

    public int playerHealth = 5;
	public int enemyHealth = 5;
    public Transform prefab;
    Button mybut;
    float moveSpeed = 5.0f;

    public bool combatting = false;

    public static InCombat instance;

    private Defeat Defeat;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Player = GameObject.Find("Player");
        Defeat = GameObject.Find("battleEnder").GetComponent<Defeat>();
    }

    // Update is called once per frame
    void Update()
    {
        if(true == combatting){
            if (Input.GetKey("w") && Player.transform.position.y < 106.5)
            {
    			Player.transform.position += Vector3.up * moveSpeed * Time.deltaTime;
    			//transform.eulerAngles = new Vector3(0, 270, 0);
    			//float smooth = 5.0f;
            }
            if (Input.GetKey("s") && Player.transform.position.y > 100)
            {
    			Player.transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    			//transform.eulerAngles = new Vector3(0, 90, 0);
    			//float smooth = 5.0f;
            }
    		if (Input.GetKey("d") && Player.transform.position.z < 8)
            {
    			Player.transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
    			//transform.eulerAngles = new Vector3(0, 0, 0);
    			//float smooth = 5.0f;
            }
            if (Input.GetKey("a") && Player.transform.position.z > -8)
            {
    			Player.transform.position += Vector3.back * moveSpeed * Time.deltaTime;
    			//transform.eulerAngles = new Vector3(0, 180, 0);
    			//float smooth = 5.0f;
            }

            if( 0 >= playerHealth || 0 >= enemyHealth ){

                combatting = false;
                Defeat.battleOver(enemyHealth);

            }
        }

        if(null != CombatPositions.instance.Player){
            Player = CombatPositions.instance.Player;
        }
        //finds astral projection clones
        Clone = GameObject.Find("Player(Clone)");
        if( null != Clone ){
            Clone.transform.position += Clone.transform.forward * 20 * Time.deltaTime;
            Clone.transform.rotation = Quaternion.Euler(0, 270, 0);

            //Fading clone doesnt seem to work
            //FadeTo(0.01f, 0.01f, Clone);

            if(-45 > Clone.transform.position.x){
                Destroy(Clone);
            }
        }


    }

    public void PlayerChoose(int choose)
    {
        //gets player from combatpositions
        //Player = CombatPositions.instance.Player;

        switch (choose)
		{
        //incorpreal
		case 2:

            //StartCoroutine(FadeTo(0.0f, 1.0f));
            Player.GetComponent<Collider>().enabled = false;
            mybut = CombatPositions.instance.incorpButton.GetComponent<Button>();
            StartCoroutine(FadeTo(0.01f, 0.5f, Player));
            StartCoroutine(enableButton(mybut, 4.0f));
            StartCoroutine(incorpUndo());
			break;
        //possession
		case 1:
			print ("Your spirit buddy goes on the offensive!");
            mybut = CombatPositions.instance.possesButton.GetComponent<Button>();
            Pet.instance.offensive = true;
            StartCoroutine(enableButton(mybut, 10.0f));
            StartCoroutine(petRecall());
			break;
        //astral projection
		case 0:
            enemyHealth -= 1;
            Instantiate(prefab, new Vector3(Player.transform.position.x-2, Player.transform.position.y, Player.transform.position.z), Quaternion.identity);

            mybut = CombatPositions.instance.astralButton.GetComponent<Button>();
            StartCoroutine(enableButton(mybut, 10.0f));
			break;

		default:
			print ("Incorrect intelligence level.");
			break;
		}
    }
    IEnumerator enableButton(Button mybut, float time){
        mybut.interactable = false;
        float j = 0f;
        while( j < time ){
            j += Time.deltaTime;
            mybut.gameObject.transform.GetChild(0).GetComponent<Image>().fillAmount = j / time;
            yield return new WaitForFixedUpdate();
        }
        //yield return new WaitForSeconds(time);
        mybut.interactable = true;
        yield return new WaitForFixedUpdate();
    }

    IEnumerator incorpUndo(){
        yield return new WaitForSeconds(2);
        Player.GetComponent<Collider>().enabled = true;
        StartCoroutine(FadeTo(1.0f, 0.5f, Player));
    }
    IEnumerator petRecall(){
        yield return new WaitForSeconds(5);
        Pet.instance.offensive = false;
    }

    IEnumerator FadeTo(float aValue, float aTime, GameObject PlayerObj)
    {
        //print("Fading");
        Renderer renderToFade; // The renderer you wish to edit materials on
        renderToFade = PlayerObj.GetComponent<Renderer>();
        for (float t = 0.0f; t <= 1.1f; t += Time.deltaTime / aTime)
        {
            int j = 0;
            foreach (Material mat in renderToFade.materials ){
                j++;
                if( 10 < j ){ //stops it from running through thousands of materials
                    break;
                }
                //skips transparent materials
                //if( 0.5f < mat.color.a ){

                    float alpha = Mathf.Abs(aValue-1.0f);
                    Color invisible = new Color (mat.color.r, mat.color.b, mat.color.g, Mathf.Lerp(alpha,aValue,t));
                    mat.SetColor("_BaseColor", invisible);
                    //Debug.Log(mat.color.a);
                //}
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
