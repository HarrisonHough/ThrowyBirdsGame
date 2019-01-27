using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2018
* VERSION: 1.0
* SCRIPT: Generic Singleton Class 
*/

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class GenericSingleton<T> : MonoBehaviour where T : Component
{
        
    private static T instance;
    [SerializeField]
    private bool destroyOnLoad = false;

    //publicly accessible reference to the instance
    public static T Instance
    {
        get
        {
            //check if an instance already exists
            if (instance == null)
            {
                //if not create new instance
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    instance = obj.AddComponent<T>();
                }
                
            }
            //if instance exists return instance
            return instance;
        }
    }

    /// <summary>
    /// Called on awake, before the start function
    /// </summary>
    public virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            if (!destroyOnLoad)
            {
                DontDestroyOnLoad(this.gameObject);
            }
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }
}

