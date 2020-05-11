using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    //public GameObject Pet;
    int[] xRes;
    int[] yRes;
    bool fullScreen = true;
    //string defaultRes;
    //public GameObject resolution;
    // Start is called before the first frame update
    void Start()
    {
        xRes = new int[]{1280, 1920, 2560, Screen.currentResolution.width};
        yRes = new int[]{720, 1080, 1440, Screen.currentResolution.height};
        //defaultRes = Screen.currentResolution.w + "x" +Screen.currentResolution.h;
        //print(Screen.currentResolution);
        //Pet = GameObject.Find("Pet");
        //resolution.GetComponent<TMPro.TextMeshProUGUI>().dropdown

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void changeQuality(int level){

        QualitySettings.SetQualityLevel(level);
        PlayerPrefs.SetInt("quality", level);
    }

    public void changeResolution(int level){

        Screen.SetResolution( xRes[level], yRes[level], fullScreen);
        PlayerPrefs.SetInt("xRes", xRes[level]);
        PlayerPrefs.SetInt("yRes", yRes[level]);
    }
    public void changeVolume(float level){

        PlayerPrefs.SetFloat("volume", level);
        AudioListener.volume = level;

    }
    public void toggleFullscreen(){

        Screen.fullScreen = !Screen.fullScreen;
        fullScreen = !Screen.fullScreen;
        //converts bool true false into int 1 0
        PlayerPrefs.SetInt("fullscreen", (fullScreen ? 1 : 0));
    }
}
