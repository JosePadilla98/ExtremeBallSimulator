using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
            if (Instance == null)
                Debug.LogError("This should never happen (" + typeof(T) + ")!");
        }
        else
            Debug.LogError("There is already a singleton for " + typeof(T) + "!");
    }
}