using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {

    public BirdState birdState { get; set; }

    private float delayDestroyTime = 2f;

    private LineRenderer lineRenderer;
    private Rigidbody2D myRigidbody;
    private CircleCollider2D myCollider;
    private AudioSource audioSource;

    private bool waitingToDestroy = false;
	// Use this for initialization
	void Awake () {

        InitializeVariables();
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
        lineRenderer = GetComponent<LineRenderer>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<CircleCollider2D>();
        audioSource = GetComponent<AudioSource>();

        lineRenderer.enabled = false;
        lineRenderer.sortingLayerName = "Foreground";

        myRigidbody.isKinematic = true;
        myCollider.radius = GameVariables.BirdColliderRadiusBig;

        birdState = BirdState.BeforeThrown;
    }

    public void OnThrow()
    {
        audioSource.Play();
        lineRenderer.enabled = true;
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
