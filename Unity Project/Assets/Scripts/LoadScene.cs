using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Start the coroutine
        StartCoroutine(AsyncLoading());
    }




    IEnumerator AsyncLoading()
    {
        yield return new WaitForSeconds(1f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        //Prevent automatic switching when loading is complete
        operation.allowSceneActivation = true;

        yield return operation;

    }
    }
