using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagSpawner : MonoBehaviour
{
    public GameObject flag;
    public Transform playerTransform;
    public bool canSpawn = true;
    float intervalTimer = 2.5f, waitTimer = .25f, intervalTime = 2.5f;
    bool finishInterval = false, allowedToSpawn;
    Vector3 realScale, spawnPosition;
    Quaternion spawnRotation;
    public int smallFlagCounter;
    // Update is called once per frame
    void Start()
    {
        realScale = Vector3.one * 1.5f;// flag.transform.localScale;
    }
    void Update()
    {
        allowedToSpawn = canSpawn && FindObjectOfType<GameManager2>().jugando && FindObjectOfType<PlayerMovement2>().canMove;
        intervalTimer -= Time.deltaTime * Convert.ToInt32(allowedToSpawn) * Convert.ToInt32(!finishInterval);
        waitTimer -= Time.deltaTime * Convert.ToInt32(allowedToSpawn) * Convert.ToInt32(finishInterval);
        if (intervalTimer <= 0.0f)
        {
            spawnPosition = playerTransform.position;
            spawnRotation = playerTransform.rotation;
            
            finishInterval = true;
            intervalTimer = intervalTime;
        }
        if(waitTimer <= 0.0f)
        {   
            if(smallFlagCounter > 0){
                flag.transform.localScale = realScale / 1.5f;
                smallFlagCounter--;
            } else 
            {
                flag.transform.localScale = realScale;
            }
            Instantiate(flag, spawnPosition, spawnRotation);
            FindObjectOfType<GameManager2>().Score();
            FindObjectOfType<AudioManager>().soundFlag();
            waitTimer = .25f;
            if(intervalTime > 1.5f)
            {
                intervalTime -= 0.01f;
            }
            
            finishInterval = false;
        }
    }
}
