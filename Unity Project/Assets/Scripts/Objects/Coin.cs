using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject particles;

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Instantiate(particles, transform.position, transform.rotation);
            FindObjectOfType<GameManager2>().Coin();
            FindObjectOfType<AudioManager>().soundCoin();
            Destroy(gameObject);
        } else if (col.tag == "Flag")
        {
            Destroy(gameObject);
            FindObjectOfType<ObjectSpawner>().SpawnCoin();
        }
    }
}
