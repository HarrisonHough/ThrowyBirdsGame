using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour {

    private Vector3 slingshotMiddleVector;

    [HideInInspector]
    public SlingshotState slingshotState;

    public Transform leftSlingshotOrigin, rightSlingshotOrigin;

    public LineRenderer slingshotLineRenderer1, slingshotLineRenderer2, trajectoryLineRenderer;

    [HideInInspector]
    public GameObject birdToThrow;

    public Transform birdWaitPosition;

    public float throwSpeed;

    [HideInInspector]
    public float timeSinceThrown;

    public delegate void BirdThrown();
    public event BirdThrown birdthrown;


    void Awake() {
        slingshotLineRenderer1.sortingLayerName = "Foreground";
        slingshotLineRenderer2.sortingLayerName = "Foreground";
        trajectoryLineRenderer.sortingLayerName = "Foreground";

        slingshotState = SlingshotState.Idle;
        slingshotLineRenderer1.SetPosition(0, leftSlingshotOrigin.position);
        slingshotLineRenderer2.SetPosition(0, rightSlingshotOrigin.position);

        slingshotMiddleVector = new Vector3((leftSlingshotOrigin.position.x + rightSlingshotOrigin.position.x) / 2, (leftSlingshotOrigin.position.y + rightSlingshotOrigin.position.y) / 2, 0);
    }

    // Update is called once per frame
    void Update() {
        switch (slingshotState)
        {
            case SlingshotState.Idle:

                InitializeBird();

                DisplaySlingShotLineRenderers();

                //TODO fix for performance
                if (Input.GetMouseButton(0))
                {
                    Vector3 location = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    if (birdToThrow.GetComponent<CircleCollider2D>() == Physics2D.OverlapPoint(location))
                    {
                        slingshotState = SlingshotState.UserPulling;
                    }
                }
                break;
            case SlingshotState.UserPulling:

                DisplaySlingShotLineRenderers();

                if (Input.GetMouseButton(0))
                {
                    Vector3 location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    location.z = 0;

                    if (Vector3.Distance(location, slingshotMiddleVector) > 1.5f)
                    {
                        var maxPosition = (location - slingshotMiddleVector).normalized * 1.5f + slingshotMiddleVector;
                        birdToThrow.transform.position = maxPosition;
                    }
                    else
                    {
                        birdToThrow.transform.position = location;
                    }
                    var distance = Vector3.Distance(slingshotMiddleVector, birdToThrow.transform.position);
                    DisplayTrajectoryLineRenderer(distance);
                }
                else //No Tap
                {
                    SetTrajectoryLineRendererActive(true);
                    timeSinceThrown = Time.time;

                    float distance = Vector3.Distance(slingshotMiddleVector, birdToThrow.transform.position);
                    if (distance > 1)
                    {
                        SetSlingshotLineRenderer(false);
                        slingshotState = SlingshotState.BirdFlying;

                        ThrowBird(distance);
                    }
                    else
                    {
                        birdToThrow.transform.positionTo(distance / 10, birdWaitPosition.position);
                        InitializeBird();
                    }


                }
                break;

        }
    }

    private void InitializeBird()
    {
        birdToThrow.transform.position = birdWaitPosition.position;
        slingshotState = SlingshotState.Idle;
        SetSlingshotLineRenderer(true);
    }

    void SetSlingshotLineRenderer(bool active)
    {
        slingshotLineRenderer1.enabled = active;
        slingshotLineRenderer2.enabled = active;
    }

    void DisplaySlingShotLineRenderers()
    {
        slingshotLineRenderer1.SetPosition(1, birdToThrow.transform.position);
        slingshotLineRenderer2.SetPosition(1, birdToThrow.transform.position);

    }

    void SetTrajectoryLineRendererActive(bool active)
    {
        trajectoryLineRenderer.enabled = active;
    }

    void DisplayTrajectoryLineRenderer(float distance)
    {
        SetTrajectoryLineRendererActive(true);

        Vector3 v = slingshotMiddleVector - birdToThrow.transform.position;
        int segmentCount = 25;

        Vector2[] segments = new Vector2[segmentCount];

        segments[0] = birdToThrow.transform.position;

        Vector2 segVelocity = new Vector2(v.x, v.y) * throwSpeed * distance;

        for (int i = 1; i < segmentCount; i++)
        {
            float time = i * Time.fixedDeltaTime * 5f;
            segments[i] = segments[0] + segVelocity * time + 0.5f * Physics2D.gravity * Mathf.Pow(time, 2);
        }

        //CHECK
        //trajectoryLineRenderer.SetVertexCount( segmentCount);
        trajectoryLineRenderer.positionCount = segmentCount;
        for (int i = 0; i < segmentCount; i++)
        {
            trajectoryLineRenderer.SetPosition(i, segments[i]);
        }
    }

    private void ThrowBird(float distance)
    {
        Vector3 velocity = slingshotMiddleVector - birdToThrow.transform.position;

        birdToThrow.GetComponent<Bird>().OnThrow();

        //TODO move to bird if possible
        birdToThrow.GetComponent<Rigidbody>().velocity = new Vector2(velocity.x, velocity.y) * throwSpeed * distance;

        if (birdthrown != null)
            birdthrown();
    }


}
