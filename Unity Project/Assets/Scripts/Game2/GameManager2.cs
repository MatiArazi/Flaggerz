using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager2 : MonoBehaviour
{
    public Behaviour[] componentsToDisable;
    bool jugando = true;
    public void Pause()
    {
        Debug.Log("Pause");
    }

    public void Resume()
    {
        Debug.Log("Resume");
    }

    public void Menu()
    {
        Debug.Log("Menu");
    }

    public void End()
    {
        if(jugando)
        {
            jugando = false;
            EnableComponents(false);
            GameObject.Find("GamePanel").GetComponent<LeanAnimation>().Cerrar();
            GameObject.Find("EndPanel").GetComponent<LeanAnimation>().Abrir();
        }
    }

    public void ContinuePlaying()
    {
        if(!jugando)
        {
            jugando = true;
            EnableComponents(true);
            GameObject.Find("GamePanel").GetComponent<LeanAnimation>().Abrir();
            GameObject.Find("EndPanel").GetComponent<LeanAnimation>().Cerrar();
        }
    }

    public void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EnableComponents(bool enabled)
    { 
         foreach (var component in componentsToDisable)
        {
            component.enabled = enabled;
        }
        if(!enabled)
        {
            GameObject.Find("Ziggy").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        } else 
        {
            GameObject.Find("Ziggy").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;   
            GameObject.Find("Ziggy").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;   
        }
    }
}
