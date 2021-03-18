using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShieldText : MonoBehaviour
{
    public Gradient gradient;
    public GameObject sphere;
    public Material shieldMat;
    public bool shielded = false;
    public bool shield = false;
    public float shieldTime;
    float time = 0f;
    

    // Update is called once per frame
    void Update()
    {
        if (shielded)
        {
            shielded = false;
            shield = true;
            time = shieldTime;
        }
        if(time > 0 && shield)
        {
            time -= Time.deltaTime;
            sphere.SetActive(true);
            var tempColor = gradient.Evaluate(time / shieldTime);
            tempColor.a = 0.5f;
            shieldMat.color = tempColor;
            
        } else
        {
            time = 0f;
            shield = false;
            sphere.SetActive(false);
        }
    }
}
