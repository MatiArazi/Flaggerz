using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;


public class LoadingBar : MonoBehaviour
{
    int sceneName;
    private void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        sceneName = PlayerPrefs.GetInt("SceneName", 1);
        MobileAds.Initialize(initStatus => { });

        StartCoroutine(LoadingScreen());

    }
    public Slider slider;

    AsyncOperation async;

    IEnumerator LoadingScreen()
    {
        async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;

        while(async.isDone == false)
        {
            slider.value = async.progress;
            if(async.progress == .9f)
            {
                slider.value = 1f;
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
