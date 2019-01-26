using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    //public enum GameState { InMenu, ReadyForThrow, Throw, Scoring, Finished }

    public enum SlingshotState
    {
        Idle,
        UserPulling,
        BirdFlying,
        Reloading
    }

    public enum GameState
    {
        InMenu,
        InGame,
        GameOver
    }

    public enum BirdState
    {
        Idle,
        Ready,
        Thrown
    }

