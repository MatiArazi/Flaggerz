using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Toggle musicToggle;
    public Toggle soundFXToggle;
    // Start is called before the first frame update
    void Start()
    {
        musicToggle.isOn = PlayerPrefs.GetInt("Music", 1) == 1;
        soundFXToggle.isOn = PlayerPrefs.GetInt("SoundFX", 1) == 1;
    }


    public void setMusic(bool isOn)
    {
        if (isOn) PlayerPrefs.SetInt("Music", 1);
        else PlayerPrefs.SetInt("Music", 0);
    }

    public void setSoundFX(bool isOn)
    {
        if (isOn) PlayerPrefs.SetInt("SoundFX", 1);
        else PlayerPrefs.SetInt("SoundFX", 0);
    }

    public void tiktok()
    {
        openUrl("https://docs.unity3d.com/ScriptReference/Application.OpenURL.html");
    }

    void openUrl(string url)
    {
        Application.OpenURL(url);
    }
}
