using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectsShop : MonoBehaviour
{
    public TMP_Text bombButton;
    public TMP_Text shieldButton;
    public TMP_Text smallFlagButton;
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

        if(bombPrices[PlayerPrefs.GetInt("BombLevel", 0)] == -1) bombButton.text = "MAX";
        else bombButton.text = bombPrices[PlayerPrefs.GetInt("BombLevel", 0)].ToString();

        if(shieldPrices[PlayerPrefs.GetInt("ShieldLevel", 0)] == -1) shieldButton.text = "MAX";
        else shieldButton.text = shieldPrices[PlayerPrefs.GetInt("ShieldLevel", 0)].ToString();

        if(smallFlagPrices[PlayerPrefs.GetInt("SmallFlagLevel", 0)] == -1) smallFlagButton.text = "MAX";
        else smallFlagButton.text = smallFlagPrices[PlayerPrefs.GetInt("SmallFlagLevel", 0)].ToString();

        if(bombPrices[PlayerPrefs.GetInt("BombLevel", 0)] > PlayerPrefs.GetInt("Coins", 0))
        {
            bombButton.color = new Color32(3,140,140, 90);
        }
        if (shieldPrices[PlayerPrefs.GetInt("ShieldLevel", 0)] > PlayerPrefs.GetInt("Coins", 0))
        {
            shieldButton.color = new Color32(3, 140, 140, 90);
        }
        if (smallFlagPrices[PlayerPrefs.GetInt("SmallFlagLevel", 0)] > PlayerPrefs.GetInt("Coins", 0))
        {
            smallFlagButton.color = new Color32(3, 140, 140, 90);
        }


        bombInfo.text = "Current explosion radius: " + PlayerPrefs.GetFloat("BombRadius", 0).ToString();
        shieldInfo.text = "Current shield time: " + PlayerPrefs.GetFloat("ShieldTime", 5f).ToString() + " seconds";
        smallFlagInfo.text = "Current number of small flags: " + PlayerPrefs.GetInt("SmallFlagCounter", 1).ToString(); ;
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
