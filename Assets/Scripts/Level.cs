using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2019
* VERSION: 1.0
* SCRIPT: Level Class
*/

/// <summary>
/// This class holds references to objects that other 
/// classes need to access. It also ensures that GameManager
/// calls "OnLevelStart" each time the scene is loaded.
/// (GameManager does not destroy on load so start only runs once)
/// </summary>
public class Level : MonoBehaviour
{
    //store reference to birds array
    [SerializeField]
    private Bird[] _birds;
    public Bird[] Birds { get { return _birds; } }

    //store reference to enemy array
    [SerializeField]
    private Enemy[] _enemies;
    public Enemy[] Enemies { get { return _enemies; } }

    //store reference to UI Control
    [SerializeField]
    private UIControl _uiControl;
    public UIControl UIControl { get { return _uiControl; } }
    
    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {
        //run on level start when scene loads
        GameManager.Instance.OnLevelStart(this);
    }


}
