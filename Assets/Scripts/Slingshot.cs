using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Slingshot : MonoBehaviour
{
    [SerializeField]
    private Bird birdToThrow;
    public Bird BirdToThrow { get { return birdToThrow; } }

    [SerializeField]
    private Transform launchPoint;

    public float throwForce;
    private SlingshotState state;

    [SerializeField]
    private float dragThreshold = 1.5f;

    private int birdArrayIndex = 0;

    [SerializeField]
    private Level level;

    [SerializeField]
    private float reloadTime = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        if (level == null)
        {
            level = FindObjectOfType<Level>();
        }
        state = SlingshotState.Idle;
        
        //TODO possibly move somewhere better
        birdToThrow = level.Birds[birdArrayIndex];
    }

    public void OnMouseDown()
    {
        if (state == SlingshotState.Idle)
            state = SlingshotState.UserPulling;
    }

    public void OnMouseHold(Vector3 mousePosition)
    {
        if(state == SlingshotState.UserPulling)
            PullBack(mousePosition);
    }

    public void OnMouseRelease(Vector3 mousePosition)
    {
        if (state != SlingshotState.UserPulling)
            return;
        float distance = Vector3.Distance(launchPoint.position, birdToThrow.transform.position);
        if (distance > dragThreshold)
        {
            ThrowBird(distance);
            state = SlingshotState.BirdFlying;
            GetNextBird();
        }
        else
        {
            //cancel throw
            //TODO Move Back to position
            birdToThrow.transform.position = launchPoint.position;
            state = SlingshotState.Idle;
            return;
        }
        
    }

    public void ThrowBird(float distance)
    {
        Vector3 velocity = launchPoint.position - birdToThrow.transform.position;
        Vector2 throwVelocity = new Vector2(velocity.x, velocity.y) * throwForce * distance;

        birdToThrow.OnThrow(throwVelocity);


        //if (birdThrown != null)
            //birdThrown();
    }


    public void PullBack(Vector3 mousePosition)
    {
        Vector3 position = mousePosition;
        position.z = 0;

        if (Vector3.Distance(position, launchPoint.position) > 1.5f)
        {
            Vector3 maxPosition = (position - launchPoint.position).normalized * 1.5f + launchPoint.position;
            birdToThrow.transform.position = maxPosition;
        }
        else
        {
            birdToThrow.transform.position = position;
        }
    }

    private void GetNextBird()
    {
        birdArrayIndex++;
        if (birdArrayIndex >= level.Birds.Length)
        {
            //no more birds
            //game over (wait for score)
            return;
        }
        //assign next bird
        birdToThrow = level.Birds[birdArrayIndex];
        state = SlingshotState.Reloading;

        StartCoroutine(ReloadRoutine());
    }

    IEnumerator ReloadRoutine()
    {
        birdToThrow.transform.DOMove(launchPoint.transform.position, reloadTime);
        float timeToWait = Time.time + reloadTime;
        while (timeToWait > Time.time)
        {
            yield return null;
        }
        state = SlingshotState.Idle;

        
    }
}
