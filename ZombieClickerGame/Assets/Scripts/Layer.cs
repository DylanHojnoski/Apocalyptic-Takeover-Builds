using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer : MonoBehaviour
{
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.position.y  <= -2)
        {
            sprite.sortingLayerName = "9";
        }
        else if(gameObject.transform.position.y <= -1.5)
        {
            sprite.sortingLayerName = "8";
        }
        else if (gameObject.transform.position.y <= -1)
        {
            sprite.sortingLayerName = "7";
        }
        else if (gameObject.transform.position.y <= -.5)
        {
            sprite.sortingLayerName = "6";
        }
        else if (gameObject.transform.position.y <= 0)
        {
            sprite.sortingLayerName = "5";
        }
        else if (gameObject.transform.position.y <= .5)
        {
            sprite.sortingLayerName = "4";
        }
        else if (gameObject.transform.position.y <= 1)
        {
            sprite.sortingLayerName = "3";
        }
        else if (gameObject.transform.position.y <= 1.5)
        {
            sprite.sortingLayerName = "2";
        }
        else if (gameObject.transform.position.y <= 2)
        {
            sprite.sortingLayerName = "1";
        }
    }
}
