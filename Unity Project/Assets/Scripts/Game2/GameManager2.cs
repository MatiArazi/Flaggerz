using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager2 : MonoBehaviour
{
    public Behaviour[] componentsToDisable;
    public TMP_Text scoreText;
    public TMP_Text scoreText2;
    public TMP_Text highScoreText;
    public TMP_Text coinText;
    public TMP_Text counterText;
    public Image replayImage;
    public Gradient camaraGradient;
    public Gradient shieldGradient;
    public AudioSource song;
    public Material shieldMat;
    public bool jugando = true;
    bool terminado = false;
    public float timerShield = 5f;
    public float timeShield = 5f;
    float replayCounter = 5f;
    int score = 0;
    int coins = 0;
    Color tempCamColor, tempShieldColor;

    private void Start()
    {
        coins = PlayerPrefs.GetInt("Coins", 0);
        coinText.text = Convert.ToString(coins);
        highScoreText.text = Convert.ToString(PlayerPrefs.GetInt("HighScore", 0));
        song.mute = PlayerPrefs.GetInt("Music", 1) != 1;
        PlayerPrefs.SetInt("SceneName", 1);
    }
    private void Update() 
    {
        song.mute = PlayerPrefs.GetInt("Music", 1) != 1;
        if(jugando)
        {
            tempCamColor = camaraGradient.Evaluate((float)score / 100f);
            FindObjectOfType<Camera>().backgroundColor = tempCamColor;

            if(timerShield > 0)
            {   
                timerShield -= Time.deltaTime;
                GameObject shield = GameObject.Find("Shield");
                shield.transform.localScale = new Vector3(7.5f,7.5f,7.5f);
                tempShieldColor = shieldGradient.Evaluate(timerShield / timeShield);
                tempShieldColor.a = 0.5f;
                shieldMat.color = tempShieldColor;
            }else 
            {
                GameObject.Find("Shield").transform.localScale = Vector3.zero;
            }
        }

        if(terminado)
        {
            replayCounter -= Time.deltaTime;
            replayImage.fillAmount = (5f - replayCounter) / 5f;
            if(replayCounter <= 0){
                ReStart();
            }
        }
    }
    public void Pause()
    {
        jugando = false;
        Debug.Log("Pause");
        EnableComponents(false);
        if(GameObject.Find("ContinuePlaying").transform.localScale == Vector3.one)
        {
            GameObject.Find("ContinuePlaying").GetComponent<LeanAnimation>().Cerrar();
            StopCoroutine("Preparing");
        }
    }

    public void Resume()
    {
        Debug.Log("Resume");
        StartCoroutine("Preparing");
    }

    public void Menu()
    {
        Debug.Log("Menu");
        PlayerPrefs.SetInt("SceneName", 1);
        SceneManager.LoadScene(0);
    }

    public void End()
    {
        if(jugando)
        {
            jugando = false;
            EnableComponents(false);
            StopCoroutine("Preparing");
            StartCoroutine("EndAnimation");
        }
    }

    IEnumerator EndAnimation()
    {
        GameObject.Find("Main Camera").GetComponent<LeanAnimation>().PlayerLoses();
        FindObjectOfType<Camera>().backgroundColor = new Color(.066f,.074f,.086f);
        yield return new WaitForSeconds(1);
        GameObject.Find("GamePanel").GetComponent<LeanAnimation>().Cerrar();
        GameObject.Find("EndPanel").GetComponent<LeanAnimation>().Abrir();
        terminado = true;
    }

    public void ContinuePlaying()
    {
        if(!jugando)
        {
            GameObject.Find("GamePanel").GetComponent<LeanAnimation>().Abrir();
            GameObject.Find("EndPanel").GetComponent<LeanAnimation>().Cerrar();
            LeanTween.cancel(GameObject.Find("Main Camera"));
            GameObject.Find("Main Camera").GetComponent<Transform>().localPosition = new Vector3(0, 60, 5);
            GameObject.Find("Main Camera").GetComponent<Transform>().localRotation = Quaternion.Euler(90f, 0f, 0f);
            FindObjectOfType<Camera>().backgroundColor = tempCamColor;
            StartCoroutine("Preparing");
            timeShield = 6;
            timerShield = 6;
            terminado = false;
            replayCounter = 5;
        }
    }

    public void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EnableComponents(bool enabled)
    { 
        FindObjectOfType<PlayerMovement2>().canMove = enabled;
        FindObjectOfType<FlagSpawner>().canSpawn = enabled;
        if(!enabled)
        {
            GameObject.Find("Player").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        } else 
        {
            GameObject.Find("Player").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;   
            GameObject.Find("Player").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;   
        }
    }

    IEnumerator Preparing()
    {
        GameObject.Find("ContinuePlaying").GetComponent<LeanAnimation>().Abrir();
        GameObject.Find("Counter").GetComponent<LeanAnimation>().StartCounter(counterText);
        yield return new WaitForSeconds(3);
        GameObject.Find("ContinuePlaying").GetComponent<LeanAnimation>().Cerrar();
        EnableComponents(true);
        jugando = true;
    }

    public void Score()
    {
        score ++;
        scoreText.text = Convert.ToString(score);
        scoreText2.text = Convert.ToString(score);
        StartCoroutine(GameObject.Find("Score").GetComponent<LeanAnimation>().PopSize());
        if(score > PlayerPrefs.GetInt("HighScore", 0))
        {   
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = score.ToString();
            scoreText.color = new Color(235f/255f, 219f/255f, 120f/255f);
            StartCoroutine(GameObject.Find("HighScore").GetComponent<LeanAnimation>().PopSize());
        }
    }

    public void Coin()
    { 
        coins ++;
        PlayerPrefs.SetInt("Coins", coins);
        coinText.text = Convert.ToString(coins);
        StartCoroutine(GameObject.Find("Coins").GetComponent<LeanAnimation>().PopSize());
    }
}
