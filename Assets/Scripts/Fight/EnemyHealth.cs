using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHealth : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.GetChild(1).GetComponent<Slider>().value = InCombat.instance.playerHealth / 5f;
        gameObject.transform.GetChild(0).GetComponent<Slider>().value = InCombat.instance.enemyHealth / 5f;



    }

}
