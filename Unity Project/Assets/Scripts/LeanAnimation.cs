using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public void PopSize(float rescale = 1.75f)
    {
        LeanTween.cancel(gameObject);
        transform.localScale = Vector3.one;
        LeanTween.scale(gameObject, Vector3.one * rescale , .35f);
        LeanTween.scale(gameObject, Vector3.one, .35f);
    }

    public void StartCounter(TMP_Text counter)
    {
        StartCoroutine("Counter", counter);
    }
    IEnumerator Counter(TMP_Text counter)
    {
        for (int time = 3; time >=0; time--)
        {
            counter.text = Convert.ToString(time);
            PopSize(1.75f);
            yield return new WaitForSeconds(1f);
        }

    }

}
