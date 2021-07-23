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
    public TMP_Text highScoreText;
    public TMP_Text coinText;
    public TMP_Text counterText;
    public Image replayImage;
    public Gradient camaraGradient;
    public bool jugando = true;
    public float timeShield = 5f;
    float replayCounter = 5f;
    int score = 0;
    int coins = 0;

    private void Start()
    {
        coins = PlayerPrefs.GetInt("Coins", 0);
        coinText.text = Convert.ToString(coins);
        highScoreText.text = Convert.ToString(PlayerPrefs.GetInt("HighScore", 0));
    }
    private void Update() 
    {
        if(timeShield > 0)
        {
            timeShield -= Time.deltaTime * Convert.ToInt32(jugando);
            GameObject.Find("Shield").transform.localScale = Vector3.one * 7.5f;
        }else 
        {
            GameObject.Find("Shield").transform.localScale = Vector3.zero;
        }

        if(!jugando)
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
        SceneManager.LoadScene(0);
    }

    public void End()
    {
        if(jugando)
        {
            jugando = false;
            EnableComponents(false);
            StopCoroutine("Preparing");
            GameObject.Find("GamePanel").GetComponent<LeanAnimation>().Cerrar();
            GameObject.Find("EndPanel").GetComponent<LeanAnimation>().Abrir();
        }
    }

    public void ContinuePlaying()
    {
        if(!jugando)
        {
            GameObject.Find("GamePanel").GetComponent<LeanAnimation>().Abrir();
            GameObject.Find("EndPanel").GetComponent<LeanAnimation>().Cerrar();
            StartCoroutine("Preparing");
            timeShield = 3;
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
        GameObject.Find("Score").GetComponent<LeanAnimation>().PopSize();
        if(score > PlayerPrefs.GetInt("HighScore", 0))
        {   
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = score.ToString();
            scoreText.color = new Color(235f/255f, 219f/255f, 120f/255f);
            GameObject.Find("HighScore").GetComponent<LeanAnimation>().PopSize(2.25f);
        }
    }

    public void Coin()
    { 
        coins ++;
        PlayerPrefs.SetInt("Coins", coins);
        coinText.text = Convert.ToString(coins);
        GameObject.Find("Coins").GetComponent<LeanAnimation>().PopSize();
    }
}
