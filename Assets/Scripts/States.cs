using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2018
* VERSION: 1.0
* SCRIPT: States Class Class (Enums)
*/


public enum SlingshotState {
    Idle,
    UserPulling,
    BirdFlying
}

public enum GameState
{
    Start,
    BirdMovingToSlingshot,
    Playing,
    Won,
    Lost
}

public enum BirdState {
    BeforeThrown,
    Thrown
}

