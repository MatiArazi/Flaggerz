using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanAnimation : MonoBehaviour
{
    float tweenTime = 0.25f;

    public void Abrir()
    {
        LeanTween.cancel(gameObject);
        transform.localScale = Vector3.zero;
        LeanTween.scale(gameObject, Vector3.one, tweenTime);
    }
    public void Cerrar()
    {
        LeanTween.cancel(gameObject);
        transform.localScale = Vector3.one;
        LeanTween.scale(gameObject, Vector3.zero, tweenTime);
    }

}
