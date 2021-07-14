using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI displayMoney = null;
    public int money;

    void Start()
    {
      //  PlayerPrefs.SetInt("Money", 0);
        money = PlayerPrefs.GetInt("Money", 0);
    }

    void Update()
    {
        displayMoney.text = "$" + PlayerPrefs.GetInt("Money", 0);
    }

    public void addMoney(int addMoney)
    {
        money += addMoney;
        PlayerPrefs.SetInt("Money", money);
        FindObjectOfType<AudioManager>().Play("GainMoney");
    }

    public void reduceMoney(int subtractMoney)
    {
        money -= subtractMoney;
        PlayerPrefs.SetInt("Money", money);
    }
}
