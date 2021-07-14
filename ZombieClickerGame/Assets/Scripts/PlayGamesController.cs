using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class PlayGamesController : MonoBehaviour
{
    private void Awake()
    {
        AuthenticateUser();
    }
    void Start()
    {
       
    }

    void Update()
    {
        
    }

    void AuthenticateUser()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate(); 
        Social.localUser.Authenticate((bool success) =>
        {
            if (success == true)
            {
                Debug.Log("Success");
            }
            else
            {
                Debug.Log("Error");
            }
        });
    }

    public static void PostToLeaderboard()
    {
        Social.ReportScore(PlayerPrefs.GetInt("HighScore", 0), "CgkIwOyZiv8eEAIQAA", (bool success) =>
        {
            if (success)
            {
                Debug.Log("Posted to leaderboard");
            }
            else
            {
                Debug.Log("Error posting to leaderboard");
            }
        });
    }

    public void ShowLeaderboardUI()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkIwOyZiv8eEAIQAA");
    }
}
