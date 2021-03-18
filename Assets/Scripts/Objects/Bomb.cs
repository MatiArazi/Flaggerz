using System.Collections;
using System.Collections.Generic;
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
        if (startCountDown)
        {
            delay -= Time.deltaTime;
            transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
        }

        if(delay <= 0)
        {
            Explode(transform);
        }
    }

    public void Explode(Transform transformExplosion)
    {
        Instantiate(explosion, transformExplosion.position, transformExplosion.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider nearbyObj in colliders)
        {
            Rigidbody rb = nearbyObj.GetComponent<Rigidbody>();
            if(rb != null)
            {
                if(rb.gameObject.tag == "Flag")
                {
                    rb.constraints = RigidbodyConstraints.None;
                    rb.useGravity = true;
                    rb.AddExplosionForce(force, transform.position, radius);
                    FindObjectOfType<AudioManager>().soundExplosion();
                }
            }
        }

        new WaitForSeconds(2);
        foreach (Collider nearbyObj in colliders)
        {
            Rigidbody rb = nearbyObj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                if (rb.gameObject.tag == "Flag")
                {
                    Destroy(nearbyObj);
                }
            }
        }

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
