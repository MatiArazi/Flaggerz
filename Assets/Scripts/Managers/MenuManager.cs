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
    public bool showLevels = false;
    public TMP_Text highScore;
    public Animator canvaAnimator;
    public Animator camaraAnimator;
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
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                firstTouch = touch.position;
                lastTouch = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lastTouch = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lastTouch = touch.position;
            }
            Debug.Log(firstTouch.y);
        }
       /* if (lastTouch.y - firstTouch.y >= 150)
        {
            showLevels = true;
        }
        if(firstTouch.y - lastTouch.y >= 150)
        {
            showLevels = false;

        }*/

        canvaAnimator.SetBool("ShowLevels", showLevels);
        camaraAnimator.SetBool("ShowLevels", showLevels);

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
        SceneManager.LoadScene("GameScene");
    }

    public void Shop(bool isShoping)
    {
        canvaAnimator.SetBool("ShowShop", isShoping);
       // camaraAnimator.SetBool("ShowShop", isShoping);
    }

    public void Options(bool isInOptions)
    {
        canvaAnimator.SetBool("ShowOptions", isInOptions);
        camaraAnimator.SetBool("ShowOptions", isInOptions);
    }

    public void Restart()
    {
        PlayerPrefs.DeleteAll();
    }

    public void GetCoins()
    {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + 15);
    }

   
}


