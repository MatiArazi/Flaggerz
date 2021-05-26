using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotaAnimation : MonoBehaviour
{
    // Start is called before the first frame update

    PotaTween abrir;
    PotaTween cerrar;
    void Start()
    {
        abrir = PotaTween.Create(gameObject, 0);
        cerrar = PotaTween.Create(gameObject, 0);
        abrir.Duration = 0.25f;
        cerrar.Duration = 0.25f;
    }

    public void Abrir()
    {
        abrir.SetScale(Vector3.zero, Vector3.one);
        abrir.Play();
    }
    public void Cerrar()
    {
        cerrar.SetScale(Vector3.one, Vector3.zero);
        cerrar.Play();
    }
    
}
