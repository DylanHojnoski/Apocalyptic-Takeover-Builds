using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Shop : MonoBehaviour
{
    [Header("List of items sold")]
    [SerializeField] private ShopItem[] shopItem;

    [Header("References")]
    [SerializeField] private Transform shopContainer;
    [SerializeField] private GameObject shopItemPrefab;
    public GameObject resetScreen;

    private void Start()
    {
        PopulateShop();
    }

    private void PopulateShop()
    {
        for(int i = 0; i < shopItem.Length; i++)
        {
            ShopItem si = shopItem[i];
            GameObject itemObject = Instantiate(shopItemPrefab, shopContainer);

            itemObject.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(si, itemObject));

            itemObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = si.itemName;
            itemObject.transform.GetChild(1).GetComponent<Image>().sprite = si.sprite;
            if (PlayerPrefs.GetInt(si.buyItem, 0) == 1 && PlayerPrefs.GetInt(si.equippedItem, 0) == 1)
            {
                itemObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Equipped";
            }
            else if (PlayerPrefs.GetInt(si.buyItem, 0) == 1 && PlayerPrefs.GetInt(si.equippedItem, 0) == 0)
            {
                itemObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Unequipped";
            }
            else
            {
                itemObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "$" + si.cost;
            }
        }
    }

    private void OnButtonClick(ShopItem item, GameObject itemObject)
    {
        if (PlayerPrefs.GetInt("Money", 0) >= item.cost && PlayerPrefs.GetInt(item.buyItem, 0) == 0)
        {
            itemObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Equipped";
            FindObjectOfType<AudioManager>().Play("BuyUpgrade");
            PlayerPrefs.SetInt(item.buyItem, 1);
            PlayerPrefs.SetInt(item.equippedItem, 1);
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money", 0) - item.cost);
        }
        else if(PlayerPrefs.GetInt(item.buyItem, 0) == 1 && PlayerPrefs.GetInt(item.equippedItem, 0) == 1)
        {
            FindObjectOfType<AudioManager>().Play("PressButton");
            PlayerPrefs.SetInt(item.equippedItem, 0);
            itemObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Unequipped";
        }
        else if (PlayerPrefs.GetInt(item.buyItem, 0) == 1 && PlayerPrefs.GetInt(item.equippedItem, 0) == 0)
        {
            FindObjectOfType<AudioManager>().Play("PressButton");
            PlayerPrefs.SetInt(item.equippedItem, 1);
            itemObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Equipped";
        }

    }

    public void ResetUpgrades()
    {
        Time.timeScale = 1f;
        for (int i = 0; i < shopItem.Length; i++)
        {
            PlayerPrefs.SetInt(shopItem[i].buyItem, 0);
            shopContainer.GetChild(i).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "$" + shopItem[i].cost;
        }
        resetScreenOff();
    }

    public void resetScreenOn()
    {
        resetScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void resetScreenOff()
    {
        resetScreen.SetActive(false);
        Time.timeScale = 1f;
    }
}
