using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectorySimulation2D : MonoBehaviour {

    public LayerMask collisionLayer;

    [SerializeField]
    private LineRenderer sightLine;

    [SerializeField]
    private float power = 0f;
    [SerializeField]
    private Vector2 startPosition = Vector2.zero;
    [SerializeField]
    private Vector2 direction;

    [SerializeField]
    private int segmentCount = 20;

    [SerializeField]
    private float segmentScale = 1;

    // 
    private Collider2D hitObject;
    private Collider2D HitObject { get { return hitObject; } }

    [SerializeField]
    private bool active = false;
    // Use this for initialization
    void Start () {
		
	}

    private void FixedUpdate()
    {
        if (active)
            SimulatePath();
    }

    public void EnableAndCalculateTrajectory(Vector2 startPosition, Vector2 direction, float power)
    {
        this.startPosition = startPosition;
        this.direction = direction;
        this.power = power;
        active = true;
    }

    public void EnableAndCalculateTrajectory(Vector2 startPosition, Vector2 direction, float power, LineRenderer sightLine)
    {
        this.startPosition = startPosition;
        this.direction = direction;
        this.power = power;
        this.sightLine = sightLine;
        active = true;
    }

    public void DisableSimulation()
    {
        active = false;
    }

    void SimulatePath()
    {
        Vector2[] segments = new Vector2[segmentCount];

        // The first line point is wherever the player's cannon, etc is
        segments[0] = startPosition;

        // The initial velocity
        Vector2 segVelocity = direction * power * Time.deltaTime;

        // reset our hit object
        hitObject = null;
        int maxSegmentCount = segmentCount;
        for (int i = 1; i < segmentCount; i++)
        {
            // Time it takes to traverse one segment of length segScale (careful if velocity is zero)
            float segTime = (segVelocity.sqrMagnitude != 0) ? segmentScale / segVelocity.magnitude : 0;

            // Add velocity from gravity for this segment's timestep
            segVelocity = segVelocity + Physics2D.gravity * segTime;

            if (i >= maxSegmentCount)
            {
                segments[i] = segments[maxSegmentCount];
            }

            // Check to see if we're going to hit a physics object
            RaycastHit2D hit = Physics2D.Raycast(segments[i-1], segVelocity, segmentScale, collisionLayer);
            //if (Physics2D.Raycast(segments[i - 1], segVelocity, out hit, segmentScale))
            if (hit)
            {
                // remember who we hit
                hitObject = hit.collider;

                // set next position to the position where we hit the physics object
                segments[i] = segments[i - 1] + segVelocity.normalized * hit.distance;

                // segments[0] + segVelocity * time + 0.5f * Physics2D.gravity * Mathf.Pow(time, 2)

                // correct ending velocity, since we didn't actually travel an entire segment
                segVelocity = segVelocity - Physics2D.gravity * (segmentScale - hit.distance) / segVelocity.magnitude;
                // flip the velocity to simulate a bounce
                segVelocity = Vector3.Reflect(segVelocity, hit.normal);

                maxSegmentCount = i + 1;
            }
            // If our raycast hit no objects, then set the next position to the last one plus v*t
            else
            {
                segments[i] = segments[i - 1] + segVelocity * segTime;
            }
        }

        // At the end, apply our simulations to the LineRenderer
      
        sightLine.positionCount = segmentCount;

        for (int i = 0; i < segmentCount; i++)
            sightLine.SetPosition(i, segments[i]);
    }
}
