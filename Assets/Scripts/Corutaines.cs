using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Corutaines : MonoBehaviour
{
    public static Corutaines Instance = null; // Экземпляр объекта

    public void Awake()
    {
        if (Instance == null)
        { 
            Instance = this; 
        }
        else if (Instance == this)
        { 
            Destroy(gameObject); 
        }

        DontDestroyOnLoad(gameObject);
    }
    public Coroutine StartRutine(IEnumerator rutine)
    {
        return this.StartCoroutine(rutine);
    }
    public void StopRutine(IEnumerator rutine)
    {
        this.StopCoroutine(rutine);
    }
}


