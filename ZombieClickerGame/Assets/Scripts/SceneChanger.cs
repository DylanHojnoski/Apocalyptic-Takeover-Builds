using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class SceneChanger : MonoBehaviour
{
    public Animator transition;


    public float transitionTime = 1f;
    private void Awake()
    {
        Time.timeScale = 1f;
    }
    private void Start()
    {
        Time.timeScale = 1f;
    }
    public void TimeChange()
    {
        Time.timeScale = 1f;
    }
    public void Transistion(string scene)
    {
        Time.timeScale = 1f;
        Pause.GameIsPaused = false;
        StartCoroutine(LoadScene(scene));    
        FindObjectOfType<AudioManager>().Play("PressButton");
    }
  
    IEnumerator LoadScene(string sceneName)
    {
        Time.timeScale = 1f;
        transition.SetTrigger("Start");

        yield return new WaitForSecondsRealtime(transitionTime);

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void ToMainMenu()
    {
        Pause.GameIsPaused = false;
        Time.timeScale = 1f;
        Transistion("MainMenu");
    }

    public void ToMainGame()
    {
        Pause.GameIsPaused = false;
        Time.timeScale = 1f;
        Transistion("MainGame");
    }

    public void ToShop()
    {
        Pause.GameIsPaused = false;
        Time.timeScale = 1f;
        Transistion("Shop");
    }

    public void ToSettings()
    {
        Pause.GameIsPaused = false;
        Time.timeScale = 1f;
        Transistion("Settings");
    }

    public void ToGameOver()
    {
        Transistion("GameOver");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void PlayAgain()
    {
        Pause.GameIsPaused = false;
        Time.timeScale = 1f;
        //Transistion("MainGame");
          float test = Random.Range(0, 10);
          if (test > 7)
          {
              ads ads = new ads();
              ads.Ad();
          }
          else
          {
              Transistion("MainGame");
          } 
    }
}
