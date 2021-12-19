using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    //This abstract class used to make other deriving class as Singleton 

    private static T instance;
    
    public static T Instance
    {
        get
        {
            if (instance == null)
                Debug.Log("<color=red>Error: </color>MonoSingleton is null");

            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
            instance = this as T;
    }

}
