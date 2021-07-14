using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public GameObject soundEffectButton;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("SoundEffects", 1) == 1)
        {
            soundEffectButton.GetComponent<Toggle>().isOn = true;
        }
        else if(PlayerPrefs.GetInt("SoundEffects", 1) == 0)
        {
            soundEffectButton.GetComponent<Toggle>().isOn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IsSoundEffects(bool isSoundEffects)
    {
        if (isSoundEffects)
        {
            PlayerPrefs.SetInt("SoundEffects", 1);
            FindObjectOfType<AudioManager>().Play("PressButton");
        }
        else if (!isSoundEffects)
        {
            PlayerPrefs.SetInt("SoundEffects", 0);
        }
    }
}
