using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosion;
    public float delay = 0.5f;
    bool startCountDown = false;

    //public int[] bombPrices;
    public float[] bombRadius;
    (int, float)[] bombUpgrades;
    public float force = 700f;
    public float radius;
    // Start is called before the first frame update
    void Start()
    {
       int bombLevel = PlayerPrefs.GetInt("BombLevel", 0);
       if(bombLevel > bombRadius.Length - 1)
        {
            bombLevel = bombRadius.Length -1;
        }
       radius = bombRadius[bombLevel];
       PlayerPrefs.SetFloat("BombRadius", radius);

    }

    // Update is called once per frame
    void Update()
    {
        if (startCountDown && FindObjectOfType<GameManager2>().jugando)
        {
            delay -= Time.deltaTime;
            transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        }

        if(delay <= 0)
        {
            startCountDown = false;
            Explode();
        }
    }

    /*
    public void Explode(Transform transformExplosion)
    {
        Instantiate(explosion, transformExplosion.position, transformExplosion.rotation);
        GameObject.Find("CollisionSphere").GetComponent<SphereCollider>().enabled = true;
        Collider[] colliders = Physics.OverlapSphere(transform.position, 6000);
        foreach(Collider nearbyObj in colliders)
        {
            Debug.Log("Viene la bombaa");
            //nearbyObj.GetComponent<Flag>().Explode();
            if(nearbyObj.tag == "Flag")
            {   
                Debug.Log("Viene la bombaa, voy a explotar(soy flag)");
                nearbyObj.GetComponent<Flag>().Explode();
            }
        }
        Destroy(gameObject);
    }*/

    public void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        GameObject explosionObject = Instantiate(explosion, transform.position, transform.rotation);
        Collider[] touchedObjects = Physics.OverlapSphere(transform.position, radius);
        Debug.Log("exlpoding");
        Debug.Log("There are " + touchedObjects.Length + " touched objects");
        Instantiate(explosion);
        FindObjectOfType<AudioManager>().soundExplosion();
        foreach (Collider touchedObject in touchedObjects)
        {   
            Transform flagTransform = touchedObject.transform.parent;
            if(flagTransform != null)
            {
                GameObject flag = flagTransform.gameObject;
                if(flag.tag == "Flag")
                {
                    Debug.Log("Flag ka boooom");
                    flag.GetComponent<Flag>().Explode(transform, radius);
                }
            }
        }
        //Destroy(explosionObject);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("boom");
            FindObjectOfType<AudioManager>().soundObjects();
            startCountDown = true;
        }
    }
}
