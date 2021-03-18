using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject particles;
    int coins = 0;
    void Start()
    {
        coins = PlayerPrefs.GetInt("Coins", 0);
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            coins++;
            FindObjectOfType<CoinText>().getCoin();
            Instantiate(particles, transform.position, transform.rotation);
            PlayerPrefs.SetInt("Coins", coins);
            FindObjectOfType<AudioManager>().soundCoin();
            Destroy(gameObject);
            

        }
    }
}
