using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2018
* VERSION: 1.0
* SCRIPT: Bird Class
*/


public class Bird : MonoBehaviour {

    public BirdState birdState { get; set; }

    private float delayDestroyTime = 2f;

    private TrailRenderer trailRenderer;
    private Rigidbody2D myRigidbody;
    private CircleCollider2D myCollider;
    private AudioSource audioSource;

    private bool waitingToDestroy = false;

    private GameObject lastThrowTrail;
	// Use this for initialization
	void Awake () {

        InitializeVariables();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.Instance.slingshot.StopTrajectorySimulation();
    }

    //TODO change to coroutine to optimize
    private void FixedUpdate()
    {
        if (birdState == BirdState.Thrown && myRigidbody.velocity.sqrMagnitude <= GameVariables.MinVelocity) {
            if (!waitingToDestroy)
            {
                waitingToDestroy = true;
                StartCoroutine(DestroyAfterDelay(delayDestroyTime));
            }
        }
    }

    void InitializeVariables()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<CircleCollider2D>();
        audioSource = GetComponent<AudioSource>();

        trailRenderer.enabled = false;
        trailRenderer.sortingLayerName = "Foreground";

        myRigidbody.isKinematic = true;
        myCollider.radius = GameVariables.BirdColliderRadiusBig;

        birdState = BirdState.BeforeThrown;
    }

    public void OnThrow()
    {
        audioSource.Play();
        trailRenderer.enabled = true;
        myRigidbody.isKinematic = false;
        myCollider.radius = GameVariables.BirdColliderRadiusNormal;
        birdState = BirdState.Thrown;
    }



    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }


}
