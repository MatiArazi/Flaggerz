using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject explosion;
    
 
    IEnumerator ForceAndExplode(Transform expPos, float radius)
    {
        // suspend execution for 5 seconds
        transform.tag = "Untagged";
        rb.constraints = RigidbodyConstraints.None;
        rb.AddExplosionForce(900, expPos.position, radius);
        FindObjectOfType<AudioManager>().soundExplosion();
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    public void Explode(Transform expPos, float radius){
        StartCoroutine(ForceAndExplode(expPos, radius));
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player" && transform.tag == "Flag")
        {
            if(FindObjectOfType<GameManager2>().timerShield > 0){
                FindObjectOfType<GameManager2>().timerShield = 0;
            } else
            {
                Debug.Log("Game over");
                FindObjectOfType<GameManager2>().End();
            }
            Instantiate(explosion, col.transform.position, col.transform.rotation);
            Explode(col.transform, 10f);
        }
    }
}
