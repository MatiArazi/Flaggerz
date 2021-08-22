using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    private Vector3 firstTouch, lastTouch;
    public TMP_Text highScore;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
       // SceneManager.LoadSceneAsync("GameScene").allowSceneActivation = false;
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.mute = PlayerPrefs.GetInt("Music", 1) != 1;

        if (Input.touchCount > 1 || Input.anyKeyDown)
        {
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }else
            {
              //  Play();
            }
           
        }
        

    }

    public void Play()
    {
        //SceneManager.LoadSceneAsync("GameScene").allowSceneActivation = true;
        PlayerPrefs.SetInt("SceneName", 2);
        SceneManager.LoadScene("LoadingScene");
    }

    
    public void Restart()
    {
        PlayerPrefs.DeleteAll();
    }

    public void Get1000Coins()
    {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + 1000);
    }

    public void GetCoins()
    {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + 15);
    }

   
}


