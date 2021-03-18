using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotateSpeed = 3f;
    float time = 0f;
    float x = 1f, y = -0.75f, z = 0.85f;
    bool axis = true;
    void Update()
    {
        time += Time.deltaTime;
        if(time > 1.5f)
        {
            if (axis)
            {
                y = -y;
            }else
            {
                z = -z;
            }
            axis = !axis;
            time = 0f;
        }
        transform.Rotate(rotateSpeed * x * Time.deltaTime, rotateSpeed * y * Time.deltaTime, rotateSpeed * z * Time.deltaTime);
    }
}
