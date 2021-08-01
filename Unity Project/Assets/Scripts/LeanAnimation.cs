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

    public IEnumerator PopSize(float rescale = 1.35f)
    {
        LeanTween.cancel(gameObject);
        transform.localScale = Vector3.one;
        LeanTween.scale(gameObject, Vector3.one * rescale , .2f);
        yield return new WaitForSeconds(.20f);
        LeanTween.scale(gameObject, Vector3.one, .10f);
        yield return new WaitForSeconds(.10f);
        LeanTween.scale(gameObject, Vector3.one * rescale / 2 , .05f);
        yield return new WaitForSeconds(.05f);
        LeanTween.scale(gameObject, Vector3.one, .10f);
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
            StartCoroutine(PopSize(1.4f));
            yield return new WaitForSeconds(1f);
        }

    }

    public void PlayerLoses()
    {
        StartCoroutine("Player");
    }

    IEnumerator Player(){

        LeanTween.cancel(gameObject);
        LeanTween.moveLocal(gameObject, new Vector3(0, 21.6f, -10), 0.2f);
        LeanTween.rotateLocal(gameObject,new Vector3(75, 0, 0), 0.2f);
        LeanTween.color(gameObject, new Color(17,19,22,1), 1.5f);
        yield return new WaitForSeconds(0.2f);
        LeanTween.moveLocal(gameObject, new Vector3(-13, 7, -3), 0.6f);
        LeanTween.rotateLocal(gameObject,new Vector3(19.2f, 68.6f, 0), 0.6f);
        yield return new WaitForSeconds(0.6f);
        LeanTween.moveLocal(gameObject, new Vector3(-3, 5, 18), 0.6f);
        LeanTween.rotateLocal(gameObject,new Vector3(0, 174, 0), 0.6f);
        yield return new WaitForSeconds(0.6f);
        LeanTween.moveLocal(gameObject, new Vector3(2, 4.64f, 17.8f), 4.8f);
        LeanTween.rotateLocal(gameObject,new Vector3(0, 189, 0), 4.8f);
    }

}
