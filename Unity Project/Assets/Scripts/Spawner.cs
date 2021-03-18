using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Animator canvaAnimator;
    public Transform ziggyTransform;
    public GameObject prefab;
    public float spawnSpeed;
    float time = 0f;
    private float spawn = 1.5f;
    public static bool changeScale = false;
    public static float scale = 1f;
    int flagCounter = -10;
    public static int smallFlagCounter;
    float gamePaused = 1;
    private void Start()
    {
        scale = 1f;
        time = 0f;
    }
    void Update()
    {
        if (changeScale)
        {
            flagCounter = Score.score;
            changeScale = false;
        }

        if (Score.score <= flagCounter + smallFlagCounter)
        {
            scale = 0.5f;
        }
        else
        {
            scale = 1f;
        }

        time += Time.deltaTime;

        if (time >= spawn)
        {
            SpawnFlag();
            Score.score++;
            canvaAnimator.SetTrigger("Score");
            spawn += spawnSpeed;
        }
}

    void SpawnFlag()
    {
        Debug.Log("Spawnn" + scale);
        prefab.transform.localScale *= scale;
        Instantiate(prefab, ziggyTransform.position, ziggyTransform.rotation);
        FindObjectOfType<AudioManager>().soundFlag();
        prefab.transform.localScale /= scale;

    }
}
