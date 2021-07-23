using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject star;
    public int numstar;
    public GameObject coin;
    public GameObject[] objects;
    float x, y, z;
    public float radius = 15;
    float time = 0f;
    float spawnCoin = 5f;
    float spawnObject = 20f;

    void Start()
    {
     for(int i = 0; i<numstar; i++)
        {
            Instantiate(star, GeneratePoint(UnityEngine.Random.Range(8000, 12000)), star.transform.rotation, FindObjectOfType<Planet>().transform);
        }   
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime * Convert.ToInt32(FindObjectOfType<GameManager2>().jugando);
        if(time >= spawnCoin)
        {
            SpawnCoin();
        }
        if (time >= spawnObject)
        {
            int index = UnityEngine.Random.Range(0, objects.Length);
            SpawnObject(objects[index]);
        }
    }

    Vector3 GeneratePoint(float radius)
    {
        float r;
        r = Mathf.Pow(radius, 2);
        x = UnityEngine.Random.Range(-radius, radius);
        r -= Mathf.Pow(x, 2);

        float interval = Mathf.Sqrt(r);
        y = UnityEngine.Random.Range(-interval, interval);
        r -= Mathf.Pow(y, 2);
        z = Mathf.Sqrt(r);

        float opposite;
        opposite = UnityEngine.Random.Range(0, 100);
        if (opposite > 50) x = -x;
        opposite = UnityEngine.Random.Range(0, 100);
        if (opposite > 50) y = -y;
        opposite = UnityEngine.Random.Range(0, 100);
        if (opposite > 50) z = -z;

        return new Vector3(x, y, z);
    }

    public void SpawnCoin()
    {
        Instantiate(coin, GeneratePoint(radius), coin.transform.rotation);
        spawnCoin += UnityEngine.Random.Range(5, 10);
    }

    public void SpawnObject(GameObject objectt)
    {
        Instantiate(objectt, GeneratePoint(radius), objectt.transform.rotation);
        spawnObject += UnityEngine.Random.Range(12.5f, 20);
    }
}
