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


    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Player")
        {
            Instantiate(particles, transform.position, transform.rotation);
            transform.gameObject.SetActive(false);
            FindObjectOfType<FlagSpawner>().smallFlagCounter = counter;
            FindObjectOfType<AudioManager>().soundObjects();
        } else if (col.transform.tag == "Flag")
        {
            FindObjectOfType<ObjectSpawner>().SpawnObject(gameObject);
            Destroy(gameObject);
        }
    }
}

