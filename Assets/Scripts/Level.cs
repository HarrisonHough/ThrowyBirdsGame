﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    private Bird[] birds;
    public Bird[] Birds { get { return birds; } }

    [SerializeField]
    private Enemy[] enemies;
    public Enemy[] Enemies { get { return enemies; } }
    [SerializeField]
    private UIControl uiControl;
    public UIControl UIControl { get { return uiControl; } }


    // Start is called before the first frame update
    void Awake()
    {
        GameManager.Instance.OnLevelStart(this);
    }


}
