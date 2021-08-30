using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectsShop : MonoBehaviour
{
    public Button bombButton;
    public Button shieldButton;
    public Button smallFlagButton;
    public TMP_Text bombInfo;
    public TMP_Text shieldInfo;
    public TMP_Text smallFlagInfo;
    public int[] bombPrices;
    public int[] shieldPrices;
    public int[] smallFlagPrices;
    public float[] bombRadius;
    public float[] shieldTime;
    public int[] smallFlagCounter;


    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("BombRadius", bombRadius[PlayerPrefs.GetInt("BombLevel", 0)]);
        PlayerPrefs.SetFloat("ShieldTime", shieldTime[PlayerPrefs.GetInt("ShieldLevel", 0)]);
        PlayerPrefs.SetInt("SmallFlagCounter", smallFlagCounter[PlayerPrefs.GetInt("SmallFlagLevel", 0)]);

        if(bombPrices[PlayerPrefs.GetInt("BombLevel", 0)] == -1)
        {
            bombButton.GetComponentInChildren<BuyBtn>().OnlyText(bombButton.gameObject,"MAX");
            bombButton.interactable = false;
        } 
            
        else 
        {
            bombButton.GetComponentInChildren<TMP_Text>().text = bombPrices[PlayerPrefs.GetInt("BombLevel", 0)].ToString();
            bombButton.interactable = !(bombPrices[PlayerPrefs.GetInt("BombLevel", 0)] > PlayerPrefs.GetInt("Coins", 0));
        }

        if(shieldPrices[PlayerPrefs.GetInt("ShieldLevel", 0)] == -1)
        {
            shieldButton.GetComponentInChildren<BuyBtn>().OnlyText(shieldButton.gameObject,"MAX");
            shieldButton.interactable = false;
        } 
        else 
        {
            shieldButton.GetComponentInChildren<TMP_Text>().text = shieldPrices[PlayerPrefs.GetInt("ShieldLevel", 0)].ToString();
            shieldButton.interactable = !(shieldPrices[PlayerPrefs.GetInt("ShieldLevel", 0)] > PlayerPrefs.GetInt("Coins", 0));
        }

        if(smallFlagPrices[PlayerPrefs.GetInt("SmallFlagLevel", 0)] == -1) 
        {
            smallFlagButton.GetComponentInChildren<BuyBtn>().OnlyText(smallFlagButton.gameObject,"MAX");
            smallFlagButton.interactable = false;
        }
        else 
        {
            smallFlagButton.GetComponentInChildren<TMP_Text>().text = smallFlagPrices[PlayerPrefs.GetInt("SmallFlagLevel", 0)].ToString();
            smallFlagButton.interactable = !(smallFlagPrices[PlayerPrefs.GetInt("SmallFlagLevel", 0)] > PlayerPrefs.GetInt("Coins", 0));
        }
        
        
        

        bombInfo.GetComponentInChildren<TMP_Text>().text = "Current explosion radius: " + PlayerPrefs.GetFloat("BombRadius", 0).ToString();
        shieldInfo.GetComponentInChildren<TMP_Text>().text = "Current shield time: " + PlayerPrefs.GetFloat("ShieldTime", 5f).ToString() + " seconds";
        smallFlagInfo.GetComponentInChildren<TMP_Text>().text = "Current number of small flags: " + PlayerPrefs.GetInt("SmallFlagCounter", 1).ToString(); ;
    }

    public void UpgradeBomb()
    {
        UpgradeObject("BombLevel", bombPrices);
    }

    public void UpgradeShield()
    {
        UpgradeObject("ShieldLevel", shieldPrices);
    }

    public void UpgradeSmallFlag()
    {
        UpgradeObject("SmallFlagLevel", smallFlagPrices);
    }


    void UpgradeObject(string prefab, int[] prices)
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        int level = PlayerPrefs.GetInt(prefab, 0);
        if (level < prices.Length - 1)
        {
            if (coins >= prices[level])
            {
                PlayerPrefs.SetInt(prefab, level + 1);
                PlayerPrefs.SetInt("Coins", coins - prices[level]);
                FindObjectOfType<AudioManager>().upgrade.pitch = 1f;
                FindObjectOfType<AudioManager>().soundUpgrade();
            }
            else
            {
                FindObjectOfType<AudioManager>().soundError();
            }
        }
    }
}
