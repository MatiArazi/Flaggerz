using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject explosion;
    public float speed = 2f;
    public bool endGame = false;
    float delay = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.tag = "Untagged";
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
        delay -=  Time.deltaTime;
        if(delay <= 0)
        {
            transform.tag = "Flag";
        }
        
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player" && transform.tag == "Flag")
        {
            rb.constraints = RigidbodyConstraints.None;
            Instantiate(explosion, col.transform.position, col.transform.rotation);
            rb.AddExplosionForce(300, transform.position, 1);
            FindObjectOfType<AudioManager>().soundExplosion();
            if (FindObjectOfType<ShieldText>().shield)
            {
                FindObjectOfType<ShieldText>().shield = false;
            }else
            {
                FindObjectOfType<GameManager>().EndGame();
               // col.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                col.GetComponent<Rigidbody>().AddExplosionForce(300, transform.position, 1);
            }
            
        }
    }
}
