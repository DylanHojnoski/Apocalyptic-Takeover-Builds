using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score = null;
    [SerializeField] TextMeshProUGUI highScore = null;
    int points = 0;

    void Start()
    {
        points = 0;
        highScore.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    void Update()
    {
        score.text = "Score: " + PlayerPrefs.GetInt("Score", points).ToString();
    }

    public void AddPoints(int addPoints)
    {
        points += addPoints;
        PlayerPrefs.SetInt("Score", points);
        PlayerPrefs.SetInt("GlobalScore", PlayerPrefs.GetInt("GlobalScore") + addPoints);
        if (points > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", points);
            PlayGamesController.PostToLeaderboard();
        }
    }

    public void Reset()
    {
        PlayerPrefs.DeleteKey("HighScore");
        highScore.text = "0";
    }

}
