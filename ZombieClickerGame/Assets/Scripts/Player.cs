using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int health = 1;
    public int armor = 0;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullheart;
    public Sprite emptyHeart;
    public Image[] armorArray;
    public Sprite bodyArmor;
    public Sprite noBodyArmor;
    public Transform blood;
    public GameObject gameOver;
    public GameObject MainGameUI;
    public GameObject pistol;
    public GameObject adButton;
    private bool time0 = false;


    private SpriteRenderer mySpriteRenderer;

    private void Start()
    {
        Time.timeScale = 1f;
        if(PlayerPrefs.GetInt("buyArmor", 0) == 1 && PlayerPrefs.GetInt("isArmorEquipped", 0) == 1)
        {
            armor = 1;
        }
    }
    private void Update()
    {
        if (!time0)
        {
            if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < 0)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.transform.localScale = new Vector2(-10, 10);
                GameObject weapon = GameObject.FindGameObjectWithTag("Weapon");
                mySpriteRenderer = weapon.GetComponent<SpriteRenderer>();
                mySpriteRenderer.flipX = true;
                mySpriteRenderer.flipY = true;
                weapon.transform.localPosition = new Vector2(-.02f, -.015f);
                GameObject firePoint = GameObject.FindGameObjectWithTag("FirePoint");
                firePoint.transform.localPosition = new Vector2(-0.0725f, -0.018f);
            }
            else
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.transform.localScale = new Vector2(10, 10);
                GameObject weapon = GameObject.FindGameObjectWithTag("Weapon");
                mySpriteRenderer = weapon.GetComponent<SpriteRenderer>();
                mySpriteRenderer.flipX = false;
                mySpriteRenderer.flipY = false;
                weapon.transform.localPosition = new Vector2(.02f, .015f);
                GameObject firePoint = GameObject.FindGameObjectWithTag("FirePoint");
                firePoint.transform.localPosition = new Vector2(0.0725f, 0.018f);
            }
        }

        if (health <= 0)
        {
            Die();
        }

        if(health > numOfHearts)
        {
            health = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < health)
            {
                hearts[i].sprite = fullheart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        for (int i = 0; i < armorArray.Length; i++)
        {
            if (i < armor)
            {
                armorArray[i].sprite = bodyArmor;
            }
            else
            {
                armorArray[i].sprite = noBodyArmor;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {      
        Zombie zombie = col.GetComponent<Zombie>();
        if (zombie != null)
        {
            FindObjectOfType<AudioManager>().Play("PlayerHurt");
            if (armor <= 0)
            {
                health--;
            }
            else
            {
                armor--;
            }
            zombie.DestroyEntity();
        }   
    }

    public void Die()
    {

        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        pistol.GetComponent<SpriteRenderer>().enabled = false;
        MainGameUI.SetActive(false);
        gameOver.SetActive(true);
        Time.timeScale = 0f;
        time0 = true;
    }

    public void RestoreHealth()
    {
        health = 1;
        if (PlayerPrefs.GetInt("buyArmor", 0) == 1 && PlayerPrefs.GetInt("isArmorEquipped", 0) == 1)
        {
            armor = 1;
        }
        time0 = false;
    }

    IEnumerator GameOver(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        gameOver.SetActive(true);
        MainGameUI.SetActive(false);
        gameOver.SetActive(true);
        Time.timeScale = 0f;
    }
}
