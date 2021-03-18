using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinText : MonoBehaviour
{
    int coins;
    public TMP_Text coinText;
    public Animator canvaAnimator;
    // Start is called before the first frame update
    void Start()
    {
        coins = PlayerPrefs.GetInt("Coins", 0);
        coinText.text = PlayerPrefs.GetInt("Coins", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        int actualCoins = coins;
        coinText.text = PlayerPrefs.GetInt("Coins", 0).ToString();
    }

    public void getCoin()
    {
        canvaAnimator.SetBool("Coins", true);
        canvaAnimator.SetBool("Coins", false);
    }
}
