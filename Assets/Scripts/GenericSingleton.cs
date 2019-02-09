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
        
    private static T _instance;
    [SerializeField]
    private bool _destroyOnLoad = false;

    //publicly accessible reference to the instance
    public static T Instance
    {
        get
        {
            //check if an instance already exists
            if (_instance == null)
            {
                //if not create new instance
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    _instance = obj.AddComponent<T>();
                }
                
            }
            //if instance exists return instance
            return _instance;
        }
    }

    /// <summary>
    /// Called on awake, before the start function
    /// </summary>
    public virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            if (!_destroyOnLoad)
            {
                DontDestroyOnLoad(this.gameObject);
            }
        }
        else if(_instance != this)
        {
            Destroy(gameObject);
        }
    }
}

