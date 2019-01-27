using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2019
* VERSION: 1.0
* SCRIPT: States Class
*/

    /// <summary>
    /// 
    /// </summary>
public enum SlingshotState
    {
        Idle,
        UserPulling,
        BirdFlying,
        Reloading
    }

/// <summary>
/// 
/// </summary>
    public enum GameState
    {
        InMenu,
        InGame,
        GameOver
    }

/// <summary>
/// 
/// </summary>
    public enum BirdState
    {
        Idle,
        Ready,
        Thrown
    }

