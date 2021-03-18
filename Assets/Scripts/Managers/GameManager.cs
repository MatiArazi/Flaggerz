using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static bool isEnded = false;
    public PlayerMovement player;
    public Rigidbody rb;
    public Spawner spwaner;
    public Animator playerAnimator;
    public Animator canvaAnimator;
    public float timeScale = 0f;
    float time = 0f;
    Transform playerDiePos;
    bool isContinuing = false;
    bool shield = false;
    public AudioSource audioSource;

    private void Start()
    {
        audioSource.mute = PlayerPrefs.GetInt("Music", 1) != 1;
    }

    private void Update()
    {
        audioSource.mute = PlayerPrefs.GetInt("Music", 1) != 1;

        Debug.Log(Time.timeScale);
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        if (isEnded)
        {
            time += Time.deltaTime;
            FindObjectOfType<PauseMenu>().timeScale = timeScale;
            if(isContinuing)
            {
                canvaAnimator.SetBool("End", true);
                isEnded = false;
                
                if (Input.touchCount > 0)
                {
                    if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                    {
                        return;
                    }
                    if (Input.touches[0].phase == TouchPhase.Began)
                    {
                        Restart();
                    }
                }
                if (Input.anyKeyDown)
                {
                    Restart();
                }
            }
        }

        if (shield)
        {
            FindObjectOfType<ShieldText>().shielded = true;
            FindObjectOfType<ShieldText>().shieldTime = 4.5f;
            shield = false;
        }
    }
    
    public void EndGame()
    {
        playerDiePos = transform;
        if (isEnded)
        {
            return;
        }
        isEnded = true;
        isContinuing = false;
        player.enabled = false;
        spwaner.enabled = false;
        playerAnimator.SetBool("EndGame", true);
        
    }

    public void Continue()
    {
        Time.timeScale = 1;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        playerAnimator.SetBool("EndGame", false);
        canvaAnimator.SetBool("End", false);
        isEnded = false;
        player.enabled = true;
        spwaner.enabled = true;
        transform.Equals(playerDiePos);
        shield = true;
    }
    
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void continueAnimation()
    {
        isContinuing = true;
    }

    public void muteMusic()
    {
        bool mute = !audioSource.mute;
        audioSource.mute = mute;

    }
}


