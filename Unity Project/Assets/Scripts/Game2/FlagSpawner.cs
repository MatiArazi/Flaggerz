using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagSpawner : MonoBehaviour
{
    public GameObject flag;
    public Transform playerTransform;
    public bool canSpawn = true;
    float intervalTime = 3f, waitTime = .5f;
    bool finishInterval = false, allowedToSpawn;
    Vector3 realScale, spawnPosition;
    Quaternion spawnRotation;
    public int smallFlagCounter;
    // Update is called once per frame
    void Start()
    {
        realScale = flag.transform.localScale;
    }
    void Update()
    {
        allowedToSpawn = canSpawn && FindObjectOfType<GameManager2>().jugando && FindObjectOfType<PlayerMovement2>().canMove;
        intervalTime -= Time.deltaTime * Convert.ToInt32(allowedToSpawn) * Convert.ToInt32(!finishInterval);
        waitTime -= Time.deltaTime * Convert.ToInt32(allowedToSpawn) * Convert.ToInt32(finishInterval);
        if (intervalTime <= 0.0f)
        {
            spawnPosition = playerTransform.position;
            spawnRotation = playerTransform.rotation;
            
            finishInterval = true;
            intervalTime = 3f;
        }
        if(waitTime <= 0.0f)
        {   
            if(smallFlagCounter > 0){
                flag.transform.localScale = realScale / 2;
                smallFlagCounter--;
            } else 
            {
                flag.transform.localScale = realScale;
            }
            Instantiate(flag, spawnPosition, spawnRotation);
            FindObjectOfType<GameManager2>().Score();
            waitTime = .5f;
            
            finishInterval = false;
        }
    }
}
