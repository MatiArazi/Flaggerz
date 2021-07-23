using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject explosion;
    
 
    IEnumerator ForceAndExplode()
    {
        // suspend execution for 5 seconds
        transform.tag = "Untagged";
        rb.constraints = RigidbodyConstraints.None;
        rb.AddExplosionForce(300, transform.position, 1);
        //FindObjectOfType<AudioManager>().soundExplosion();
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    public void Explode(){
        StartCoroutine("ForceAndExplode");
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player" && transform.tag == "Flag")
        {
            if(FindObjectOfType<GameManager2>().timeShield > 0){
                FindObjectOfType<GameManager2>().timeShield = 0;
            } else
            {
                Debug.Log("Game over");
                FindObjectOfType<GameManager2>().End();
            }
            Instantiate(explosion, col.transform.position, col.transform.rotation);
            StartCoroutine("ForceAndExplode");
        }
    }
}
