using System;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2018
* VERSION: 1.0
* SCRIPT: Pool Member Class
*/

/// <summary>
/// 
/// </summary>
public class PoolMember : MonoBehaviour {

    //
    public event Action OnDestroyEvent;

    /// <summary>
    /// 
    /// </summary>
    private void OnDisable()
    {
        if (OnDestroyEvent != null)
            OnDestroyEvent();
    }


}
