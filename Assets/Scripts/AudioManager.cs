using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioSource button;
    public AudioSource upgrade;
    public AudioSource flag;
    public AudioSource coin;
    public AudioSource explosion;
    public AudioSource objects;

    public void soundButton()
    {
        if(PlayerPrefs.GetInt("SoundFX", 0) == 1)
        {
            button.Play();
            Debug.Log("sound!!");
        }
    }

    public void soundUpgrade()
    {
        if (PlayerPrefs.GetInt("SoundFX", 0) == 1)
        {
            upgrade.Play();
        }
    }

    public void soundFlag()
    {
        if (PlayerPrefs.GetInt("SoundFX", 0) == 1)
        {
            flag.pitch = Random.RandomRange(.8f, 1.2f);
            flag.Play();
        }
    }

    public void soundCoin()
    {
        if (PlayerPrefs.GetInt("SoundFX", 0) == 1)
        {
            coin.pitch = Random.RandomRange(.8f, 1.2f);
            coin.Play();
        }
    }

    public void soundExplosion()
    {
        if (PlayerPrefs.GetInt("SoundFX", 0) == 1)
        {
            explosion.Play();
        }
    }

    public void soundObjects()
    {
        if (PlayerPrefs.GetInt("SoundFX", 0) == 1)
        {
            objects.pitch = 1.8f;
            objects.Play();
        }
    }
}
