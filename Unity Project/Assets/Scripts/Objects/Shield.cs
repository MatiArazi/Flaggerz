using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
   // public TimeBar timeBar;
    public float[] shieldTime;
    public GameObject particles;
    int shieldLevel;
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        shieldLevel = PlayerPrefs.GetInt("ShieldLevel", 0);
        time = shieldTime[shieldLevel];
        PlayerPrefs.SetFloat("ShieldTime", time);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.CompareTag("Player"))
        {
            Instantiate(particles, transform.position, transform.rotation);
            FindObjectOfType<GameManager2>().timeShield = 5;
            FindObjectOfType<AudioManager>().soundObjects();
            Destroy(gameObject);
        } else if (col.transform.tag == "Flag")
        {
            FindObjectOfType<ObjectSpawner>().SpawnObject(gameObject);
            Destroy(gameObject);
        }
    }
}
