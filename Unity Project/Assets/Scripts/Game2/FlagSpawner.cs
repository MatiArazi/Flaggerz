using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagSpawner : MonoBehaviour
{
    public GameObject flag;
    public Transform playerTransform;
    bool canSpawn = true;
    // Update is called once per frame
    void Update()
    {
        if(canSpawn && FindObjectOfType<PlayerMovement1>().canMove){
            StartCoroutine("spawn");
        }
    }

    IEnumerator spawn() {
        canSpawn = false;
        yield return new WaitForSeconds(3);
        Vector3 spawnPosition = playerTransform.position;
        Quaternion spawnRotation = playerTransform.rotation;
        yield return new WaitForSeconds(0.5f);
        Instantiate(flag, spawnPosition, spawnRotation);
        canSpawn = true;
    }
}
