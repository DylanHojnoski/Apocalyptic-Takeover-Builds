using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseScreen;


    public void OnPause()
    {
        GameIsPaused = true;
        Time.timeScale = 0f;
        pauseScreen.SetActive(true);
        FindObjectOfType<AudioManager>().Play("PressButton");
    }

    public void OnResume()
    {
        GameIsPaused = false;
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
        FindObjectOfType<AudioManager>().Play("PressButton");
    }   
}

