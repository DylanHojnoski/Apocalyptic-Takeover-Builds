using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Timeline;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 1f;
    
    void Update()
    {
        toPlayer();
    }

    public void toPlayer()
    {
        Vector2 currentPos = GameObject.FindGameObjectWithTag("Enemy").transform.position;
        Vector2 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        transform.position = Vector2.MoveTowards(transform.position , playerPos , speed * Time.deltaTime);
    }
}
