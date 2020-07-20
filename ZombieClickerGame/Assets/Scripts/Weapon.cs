using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public GameObject reload;
    private Vector2 lookDirection;
    private float lookAngle;
    public int maxBullets = 10;
    private int numBullets;
    public TextMeshProUGUI magazine = null;
    public float offset;
    public GameObject projectile;
    public Transform shotPoint;
    public float startTimeBtwShots;
    private float timeBtwShots;
    public Transform shotParticles;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        if(PlayerPrefs.GetInt("buyBullets", 0) == 1 && PlayerPrefs.GetInt("isBulletsEquipped", 0) == 1)
        {
            maxBullets = 15;
        }

        if(PlayerPrefs.GetInt("buyFireRate", 0) == 1 && PlayerPrefs.GetInt("isFireRateEquipped", 0) == 1)
        {
            timeBtwShots = .1f;
        }
        numBullets = maxBullets;
    }
    void Update ()
    {
          Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
          float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
          transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

          if(timeBtwShots <= 0)
          {
            if (Input.GetMouseButtonDown(0) && numBullets > 0)
            {
                Instantiate(projectile, shotPoint.position, transform.rotation);
                Transform.Instantiate(shotParticles, shotPoint.position, transform.rotation);
                FindObjectOfType<AudioManager>().Play("GunShoot");
                numBullets--;
                timeBtwShots = startTimeBtwShots;
            }
            else if(Input.GetMouseButtonDown(0) && numBullets <= 0 )
            {
                FindObjectOfType<AudioManager>().Play("OutOfBullets");
            }
          }
          else
        {
            timeBtwShots -= Time.deltaTime;
        }
           
        magazine.text = numBullets + " / " + maxBullets;
    }

    public void Reload()
    {
        numBullets = maxBullets;
        transform.rotation = Quaternion.Euler(0f, 0f, 90f);
    }
}
