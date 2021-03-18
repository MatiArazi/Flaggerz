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
            Instantiate(star, GeneratePoint(Random.Range(8000, 12000)), star.transform.rotation, FindObjectOfType<Planet>().transform);
        }   
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= spawnCoin)
        {
            Instantiate(coin, GeneratePoint(radius), coin.transform.rotation);
            spawnCoin += Random.Range(5, 10);
        }
        if (time >= spawnObject)
        {
            int index = Random.Range(0, objects.Length);
            Instantiate(objects[index], GeneratePoint(radius), objects[index].transform.rotation);
            spawnObject += Random.Range(12.5f, 20);
        }
    }

    Vector3 GeneratePoint(float radius)
    {
        float r;
        r = Mathf.Pow(radius, 2);
        x = Random.Range(-radius, radius);
        r -= Mathf.Pow(x, 2);

        float interval = Mathf.Sqrt(r);
        y = Random.Range(-interval, interval);
        r -= Mathf.Pow(y, 2);
        z = Mathf.Sqrt(r);

        float opposite;
        opposite = Random.Range(0, 100);
        if (opposite > 50) x = -x;
        opposite = Random.Range(0, 100);
        if (opposite > 50) y = -y;
        opposite = Random.Range(0, 100);
        if (opposite > 50) z = -z;

        return new Vector3(x, y, z);
    }
}
