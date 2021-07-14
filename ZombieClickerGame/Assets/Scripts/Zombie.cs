using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Zombie : MonoBehaviour 
{
    public int health = 1;
    public int addPoints = 1;
    private SpriteRenderer mySpriteRenderer;
    public int addMoney = 1;
    public Transform blood;
    private float wait = 2.5f;
    public bool isOnFire = false;
    public int fireDamage = 1;
    public Transform particleSpawn;
    public GameObject fire;
    public float speed = 1f;
    public bool ice = false;
    public GameObject iceParticles;
    private bool isIce = false;
    public GameObject explosion;
    private Vector2 playerPos;


    private void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        float horizontal = transform.position.x;
        if (horizontal > 0)
        {
            mySpriteRenderer.flipX = true;
        }
    }

    private void Update()
    {
        toPlayer();
        if (isOnFire)
        {
            fire.SetActive(isOnFire);
            SpriteRenderer fireSprite = fire.GetComponent<SpriteRenderer>();
            fireSprite.sortingLayerID = mySpriteRenderer.sortingLayerID;
            fireSprite.sortingOrder = mySpriteRenderer.sortingOrder + 1;
            wait -= Time.deltaTime;
            if (wait <= 0)
            {
                TakeDamage(fireDamage);
                wait = 2.5f;
            }

        }

        ParticleSystemRenderer iceSprite = iceParticles.GetComponent<ParticleSystemRenderer>();
        iceSprite.sortingOrder = mySpriteRenderer.sortingOrder + 1;
    }

    public void toPlayer()
    {
        Vector2 currentPos = GameObject.FindGameObjectWithTag("Enemy").transform.position;
        transform.position = Vector2.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
    }

    public void TakeDamage (int damage)
    {
        health -= damage;
        FindObjectOfType<AudioManager>().Play("ZombieHurt");
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        FindObjectOfType<Score>().AddPoints(addPoints);
        DropMoney();
        Transform.Instantiate(blood, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void DestroyEntity()
    {
        Destroy(gameObject);
    }

    public void DropMoney()
    {
        float num = Random.Range(0f, 10f);
        if (num > 8f)
        {
            GameObject money = GameObject.FindGameObjectWithTag("EventSystem");
            money.GetComponent<Money>().addMoney(addMoney);
        }
    }

    public void Fire(bool isOnFire)
    {
        wait = 1;
        if(wait <= 0)
        {
            TakeDamage(fireDamage);
            wait = 3;
        }
    }

    public void Ice()
    {
        if(!isIce)
        {
            speed *= .65f;
            iceParticles.SetActive(true);
            ParticleSystemRenderer iceSprite = iceParticles.GetComponent<ParticleSystemRenderer>();
            iceSprite.sortingOrder = mySpriteRenderer.sortingOrder + 2;
            isIce = true;
        }
    }
}
