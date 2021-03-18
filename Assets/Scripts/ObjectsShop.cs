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

        bombButton.text = bombPrices[PlayerPrefs.GetInt("BombLevel", 0)].ToString();
        shieldButton.text = shieldPrices[PlayerPrefs.GetInt("ShieldLevel", 0)].ToString();
        smallFlagButton.text = smallFlagPrices[PlayerPrefs.GetInt("SmallFlagLevel", 0)].ToString();

        bombInfo.text = "Explosion radius: " + PlayerPrefs.GetFloat("BombRadius", 0).ToString();
        shieldInfo.text = "Shield Time: " + PlayerPrefs.GetFloat("ShieldTime", 5f).ToString() + " seconds";
        smallFlagInfo.text = "Number of small flags: " + PlayerPrefs.GetInt("SmallFlagCounter", 1).ToString(); ;
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
                FindObjectOfType<AudioManager>().upgrade.pitch = .5f;
                FindObjectOfType<AudioManager>().soundUpgrade();
            }
        }
    }
}
