using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2018
* VERSION: 1.0
* SCRIPT: Pool Class
*/

/// <summary>
/// 
/// </summary>
public class Pool : MonoBehaviour {
    [SerializeField]
    private GameObject _prefabToPool;
    [SerializeField]
    private int _poolSize = 50;

    private Queue<GameObject> _objectsQueue = new Queue<GameObject>();
    private List<GameObject> _objectPool = new List<GameObject>();

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Awake()
    {
        GrowPool();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public GameObject GetObject()
    {
        if (_objectsQueue.Count == 0)
        {
            GrowPool();
        }

        var pooledObject = _objectsQueue.Dequeue();

        return pooledObject;
    }

    /// <summary>
    /// 
    /// </summary>
    private void GrowPool()
    {
        int lastPoolSize = _objectPool.Count;
        for (int i = 0; i < _poolSize; i++)
        {
            var pooledObject = Instantiate(_prefabToPool);
            pooledObject.name += " " + (i + lastPoolSize );
            pooledObject.transform.parent = transform;
            pooledObject.AddComponent<PoolMember>();

            //TODO maybe set on disable event
            pooledObject.GetComponent<PoolMember>().OnDestroyEvent += () => AddObjectToAvailable(pooledObject);


            //add to pool
            _objectPool.Add(pooledObject);

            pooledObject.SetActive(false);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pooledObject"></param>
    private void AddObjectToAvailable(GameObject pooledObject)
    {
        _objectsQueue.Enqueue(pooledObject);
    }

    public void DisableAfterDelay(GameObject objectToDisable, float delay)
    {

    }

    
}
