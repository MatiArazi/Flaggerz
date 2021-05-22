using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    int touches = 0;
    public bool gamePaused = false;
    public Animator canvasAnimator;
    public GameObject ligth;
    public float timeScale = 0;

    private void Start()
    {
        timeScale = 0;
    }
    private void Update()
    {
        if ((Input.touchCount > 0 || Input.anyKey) && !gamePaused && !GameManager.isEnded)
        {
            timeScale = 1;
        }
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = timeScale * 0.02f;

    }

    public void Pause()
    {
        timeScale = 0;
        gamePaused = true;
        canvasAnimator.SetBool("Pause", true);
        canvasAnimator.SetBool("Options", false);
        ligth.SetActive(false);
    }

    public void Resume()
    {
        canvasAnimator.SetBool("Pause", false);
        gamePaused = false;
        timeScale = 1;
        Time.fixedDeltaTime = 1;
        ligth.SetActive(true);
    }

    public void Options()
    {
        canvasAnimator.SetBool("Pause", false);
        canvasAnimator.SetBool("Options", true);
    }

    public void Menu()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 1f;
        SceneManager.LoadScene(0);
    }
}
