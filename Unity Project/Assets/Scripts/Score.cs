using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public Animator canvaAnimator;
    public static int score = 0;
    public Text scoreText;
    public Text scoreText2;
    public TMP_Text hScoreText;
    int newRecord = 0;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        hScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        scoreText.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
        scoreText2.text = score.ToString();
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            newRecord++;
            hScoreText.text = score.ToString();
            scoreText.color = new Color(235f/255f, 219f/255f, 120f/255f);
            scoreText2.color = new Color(235f/255f, 219f/255f, 120f/255f);
        }
        if (newRecord == 1)
        {
            FindObjectOfType<AudioManager>().soundUpgrade();
            newRecord++;
        }

    }

    public void notAnimateScore()
    {
        canvaAnimator.SetBool("Score", false);
    }
}
