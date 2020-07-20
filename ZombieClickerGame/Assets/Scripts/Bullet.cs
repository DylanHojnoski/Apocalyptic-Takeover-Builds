using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage = 1;
    private int objectsHit = 0;
    public int setPeircing = 0;
    private GameObject enemyHit;
    public Transform particleSpawn;
    public GameObject fireBullet;
    public GameObject iceBullet;
    public GameObject explosion;
    public Transform onHitBlood;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("buyFasterBullets") == 1 && PlayerPrefs.GetInt("isFasterBulletsEquipped", 0) == 1)
        {
            speed = 25f;
        }
    }

    void Start()
    {
        rb.velocity = transform.right * speed;

        if(PlayerPrefs.GetInt("buyFireBullets", 0) == 1 && PlayerPrefs.GetInt("isFireBulletsEquipped", 0) == 1)
        {
            fireBullet.SetActive(true);
        }
        if (PlayerPrefs.GetInt("buyIceBullets", 0) == 1 && PlayerPrefs.GetInt("isIceBulletsEquipped", 0) == 1)
        {
            iceBullet.SetActive(true);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {

        Zombie zombie = hitInfo.GetComponent<Zombie>();
        Transform spawn = zombie.transform;
        if (zombie != null)
        {
            GameObject.Instantiate(onHitBlood, gameObject.transform.position, Quaternion.identity, zombie.transform);
            if (PlayerPrefs.GetInt("buyPenetratingBullets") == 1 && PlayerPrefs.GetInt("isPenetratingBulletsEquipped", 0) == 1)
            {
                FireBullets(hitInfo, zombie);
                IceBullets(hitInfo, zombie);
                PenetratingBullets(hitInfo, zombie);
                ExplosiveBullets(hitInfo, zombie, spawn);
            }
            else
            {
                zombie.TakeDamage(damage);
                Destroy(gameObject);
                ExplosiveBullets(hitInfo, zombie, spawn);
                FireBullets(hitInfo, zombie);
                IceBullets(hitInfo, zombie);
            }
        }
    }

        public void PenetratingBullets(Collider2D hitInfo, Zombie zombie)
    {
        if (objectsHit < 1)
        {
            zombie.TakeDamage(damage);
            enemyHit = hitInfo.gameObject;
            objectsHit++;
        }
        else if (objectsHit == 1 && zombie.gameObject != enemyHit)
        {
            zombie.TakeDamage(damage);
            Destroy(gameObject);
            objectsHit = 0;
            enemyHit = null;
        }
    }

    public void FireBullets(Collider2D hitInfo, Zombie zombie)
    {
        if (PlayerPrefs.GetInt("buyFireBullets", 0) == 1 && PlayerPrefs.GetInt("isFireBulletsEquipped", 0) == 1)
        {
            zombie.isOnFire = true;
        }
    }

    public void IceBullets(Collider2D hitInfo, Zombie zombie)
    {
        if (PlayerPrefs.GetInt("buyIceBullets", 0) == 1 && PlayerPrefs.GetInt("isIceBulletsEquipped", 0) == 1)
        {
            zombie.Ice();
        }
    }

    public void ExplosiveBullets(Collider2D hitInfo, Zombie zombie, Transform spawn)
    {
        if (PlayerPrefs.GetInt("buyExplosiveBullets", 0) == 1 && PlayerPrefs.GetInt("isExplosiveBulletsEquipped", 0) == 1)
        {
            float radius = 1;
            GameObject.Instantiate(explosion, spawn.position, Quaternion.identity);
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(spawn.position, radius);
            int i = 0;
            while (i < hitColliders.Length)
            {
                if (hitColliders[i].gameObject != zombie.gameObject)
                {
                    hitColliders[i].GetComponent<Zombie>().TakeDamage(damage);
                }
                i++;
            }
        }
    }
}
