using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    bool optionsOn = false;
    public GameObject OptionsMenu;
    public GameObject PauseMenu;
    bool fullScreen;

    void Start(){
        PlayerPrefs.DeleteAll();

        PlayerPrefs.SetFloat("volume", 0.5f);
        PlayerPrefs.SetInt("quality", 1);
        PlayerPrefs.SetInt("xRes", Screen.currentResolution.width);
        PlayerPrefs.SetInt("yRes", Screen.currentResolution.height);

        //AudioListener.volume = 0.5f;
        //PlayerPrefs.SetInt("fullscreen", 1);

        AudioListener.volume = PlayerPrefs.GetFloat("volume");
        fullScreen = ( PlayerPrefs.GetInt("fullscreen") == 1);
        Screen.SetResolution(PlayerPrefs.GetInt("xRes"), PlayerPrefs.GetInt("yRes"), fullScreen);
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("quality"));
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Overworld");
        Time.timeScale = 1f;
    }

    public void OptionsToggle(){
        optionsOn = !optionsOn;
        PauseMenu.SetActive(!optionsOn);
        OptionsMenu.SetActive(optionsOn);

        print(PlayerPrefs.GetFloat("volume"));
        print( PlayerPrefs.GetInt("fullscreen"));
        print(PlayerPrefs.GetInt("xRes"));
        print(PlayerPrefs.GetInt("yRes"));
        print(PlayerPrefs.GetInt("quality"));

    }

    public void QuitGame()
    {
        Debug.Log("You're quitting the game.");
        Application.Quit();
    }
}
