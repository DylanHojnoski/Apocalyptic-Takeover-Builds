using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject gameOver;
    public GameObject MainGameUI;
    public GameObject pistol;
    public GameObject adButton;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        pistol.GetComponent<SpriteRenderer>().enabled = false;
        MainGameUI.SetActive(false);
        gameOver.SetActive(true);
        Time.timeScale = 0f;
    }
}
