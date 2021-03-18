using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallFlagObj : MonoBehaviour
{
    public GameObject particles;
    public int[] smallFlagCounters;
    int smallFlagLevel;
    int counter;

    void Start()
    {
        smallFlagLevel = PlayerPrefs.GetInt("SmallFlagLevel", 0);
        if (smallFlagLevel > smallFlagCounters.Length - 1) smallFlagLevel = smallFlagCounters.Length - 1;
        counter = smallFlagCounters[smallFlagLevel];
        PlayerPrefs.SetInt("SmallFlagCounter", counter);
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Instantiate(particles, transform.position, transform.rotation);
            transform.gameObject.SetActive(false);
            Spawner.changeScale = true;
            Spawner.smallFlagCounter = counter;
            FindObjectOfType<AudioManager>().soundObjects();
        }
    }
}

