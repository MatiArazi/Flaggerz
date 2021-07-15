using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject explosion;
    public bool endGame = false;
    
 
    IEnumerator ForceAndExplode()
    {
        // suspend execution for 5 seconds
        rb.constraints = RigidbodyConstraints.None;
        rb.AddExplosionForce(300, transform.position, 1);
        //FindObjectOfType<AudioManager>().soundExplosion();
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            Debug.Log("Game over");
            FindObjectOfType<GameManager2>().End();
            /*
            if (FindObjectOfType<ShieldText>().shield)
            {
                FindObjectOfType<ShieldText>().shield = false;
            }else
            {
                FindObjectOfType<GameManager>().EndGame();
               // col.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                //col.GetComponent<Rigidbody>().AddExplosionForce(300, transform.position, 1);
            }
            */
            Instantiate(explosion, col.transform.position, col.transform.rotation);
            StartCoroutine("ForceAndExplode");
        }
    }
}
