using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class Continue : MonoBehaviour
{
    public GameObject gameOver;
    public GameObject MainGameUI;
    public GameObject pistol;
    public GameObject player;
    public bool isActive = true;
    public GameObject adButton;

    public void ContinueGame()
    {
        player.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<Collider2D>().enabled = true;
        pistol.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<Player>().RestoreHealth();
        MainGameUI.SetActive(true);
        gameOver.SetActive(false);
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        for(int  i = 0; i < enemy.Length; i++)
        {
            Destroy(enemy[i].gameObject);
        }
        Time.timeScale = 1f;
        adButton.gameObject.SetActive(false);
    }
}
