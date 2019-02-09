using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2019
* VERSION: 1.0
* SCRIPT: Slingshot Class
*/

public class Slingshot : MonoBehaviour
{
    //store reference to currently "loaded" bird
    [SerializeField]
    private Bird _birdToThrow;
    public Bird BirdToThrow { get { return _birdToThrow; } }

    //reference to slingshot launch point
    [SerializeField]
    private Transform _launchPoint;

    //adjustable throw force
    [SerializeField]
    private float _throwForce = 6f;
    private SlingshotState _state;


    [SerializeField]
    private float _dragThreshold = 1.5f;

    //used to keep track of current bird in bird array
    private int _birdArrayIndex = 0;

    [SerializeField]
    private Level _level;

    //time it takes for next bird to be loaded into slingshot
    [SerializeField]
    private float _reloadTime = 2f;

    /// <summary>
    ///  Start is called before the first frame update
    /// </summary>
    void Start()
    {
        if (_level == null)
        {
            _level = FindObjectOfType<Level>();
        }
        _state = SlingshotState.Idle;
        
        //TODO possibly move somewhere better
        _birdToThrow = _level.Birds[_birdArrayIndex];
    }

    /// <summary>
    /// Called on mouse/touch down (from InputManager)
    /// </summary>
    public void OnMouseDown()
    {
        //must be in idle state
        if (_state == SlingshotState.Idle)
            _state = SlingshotState.UserPulling;
    }

    /// <summary>
    /// Called on mouse down/hold (from InputManager)
    /// Calls pullback function
    /// </summary>
    /// <param name="mousePosition">Takes in mouse position to calculate pullback</param>
    public void OnMouseHold(Vector3 mousePosition)
    {
        if(_state == SlingshotState.UserPulling)
            PullBack(mousePosition);
    }

    /// <summary>
    /// Called on Touch/Mouse release (from InputController)
    /// Calculates drag distance and calls/cancels bird throw
    /// </summary>
    /// <param name="mousePosition"></param>
    public void OnMouseRelease(Vector3 mousePosition)
    {
        if (_state != SlingshotState.UserPulling)
            return;
        float distance = Vector3.Distance(_launchPoint.position, _birdToThrow.transform.position);
        if (distance > _dragThreshold)
        {
            ThrowBird(distance);
            _state = SlingshotState.BirdFlying;
            GetNextBird();
        }
        else
        {
            //cancel throw
            //TODO Move Back to position
            _birdToThrow.transform.position = _launchPoint.position;
            _state = SlingshotState.Idle;
            return;
        }
        
    }

    /// <summary>
    /// Calculates throw velocity and calls OnThrow method on current
    /// birdToThrow
    /// </summary>
    /// <param name="distance"></param>
    public void ThrowBird(float distance)
    {
        //calculate velocity
        Vector3 velocity = _launchPoint.position - _birdToThrow.transform.position;
        //apply multipliers
        Vector2 throwVelocity = new Vector2(velocity.x, velocity.y) * _throwForce * distance;

        //throw bird with calculated velocity
        _birdToThrow.OnThrow(throwVelocity);
    }

    /// <summary>
    /// Moves and bird according to mouse drag and restricts
    /// position relative to launch point
    /// </summary>
    /// <param name="mousePosition">Pass in mouse position (world space)</param>
    public void PullBack(Vector3 mousePosition)
    {
        Vector3 position = mousePosition;
        position.z = 0;

        if (Vector3.Distance(position, _launchPoint.position) > 1.5f)
        {
            Vector3 maxPosition = (position - _launchPoint.position).normalized * 1.5f + _launchPoint.position;
            _birdToThrow.transform.position = maxPosition;
        }
        else
        {
            _birdToThrow.transform.position = position;
        }
    }

    /// <summary>
    /// Gets next bird in array and reloads
    /// </summary>
    private void GetNextBird()
    {
        _birdArrayIndex++;
        //check if within array range
        if (_birdArrayIndex >= _level.Birds.Length)
        {
            //no more birds
            //game over (wait for score)
            return;
        }
        //assign next bird
        _birdToThrow = _level.Birds[_birdArrayIndex];
        _state = SlingshotState.Reloading;

        //start reload process
        StartCoroutine(ReloadRoutine());
    }

    /// <summary>
    /// Coroutine the moves next bird into slingshot launch position
    /// </summary>
    /// <returns></returns>
    IEnumerator ReloadRoutine()
    {
        //move using DoTween move
        _birdToThrow.transform.DOMove(_launchPoint.transform.position, _reloadTime);
        //calculate time to wait
        float timeToWait = Time.time + _reloadTime;
        //wait until time passed
        while (timeToWait > Time.time)
        {
            yield return null;
        }
        //now ready to throw again
        _state = SlingshotState.Idle;
    }
}
