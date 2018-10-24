using UnityEngine;

/// <summary>
/// Controls the Laser Sight for the player's aim
/// </summary>
public class TrajectorySimulation : MonoBehaviour
{
    // Reference to the LineRenderer we will use to display the simulated path
    [SerializeField]
    private LineRenderer sightLine;

    [SerializeField]
    private float power = 0;
    [SerializeField]
    private Vector3 startPosition = Vector3.zero;
    [SerializeField]
    private Vector3 direction = Vector3.zero;

    public Color StartColor;
    public Color EndColor;

    // Number of segments to calculate - more gives a smoother line
    public int segmentCount = 20;

    // Length scale for each segment
    public float segmentScale = 1;

    // gameobject we're actually pointing at (may be useful for highlighting a target, etc.)
    private Collider _hitObject;
    public Collider hitObject { get { return _hitObject; } }

    [SerializeField]
    private bool active = false;

    void FixedUpdate()
    {
        if (active)
            SimulatePath();
    }

    public void EnableAndCalculateTrajectory(Vector3 startPosition, Vector3 direction, float power)
    {
        segmentCount = 20;
        this.startPosition = startPosition;
        this.direction = direction;
        this.power = power;
        active = true;
    }

    public void DisableSimulation()
    {
        active = false;
        segmentCount = 20;
    }

    /// <summary>
    /// Simulate the path of a launched ball.
    /// Slight errors are inherent in the numerical method used.
    /// </summary>
    void SimulatePath()
    {
        Vector3[] segments = new Vector3[segmentCount];

        // The first line point is wherever the player's cannon, etc is
        segments[0] = startPosition;

        // The initial velocity
        Vector3 segVelocity = direction * power * Time.deltaTime;

        // reset our hit object
        _hitObject = null;
        int maxSegmentCount = segmentCount;
        for (int i = 1; i < segmentCount; i++)
        {
            // Time it takes to traverse one segment of length segScale (careful if velocity is zero)
            float segTime = (segVelocity.sqrMagnitude != 0) ? segmentScale / segVelocity.magnitude : 0;

            // Add velocity from gravity for this segment's timestep
            segVelocity = segVelocity + Physics.gravity * segTime;

            // Check to see if we're going to hit a physics object
            RaycastHit hit;
            if (Physics.Raycast(segments[i - 1], segVelocity, out hit, segmentScale))
            {
                // remember who we hit
                _hitObject = hit.collider;

                // set next position to the position where we hit the physics object
                segments[i] = segments[i - 1] + segVelocity.normalized * hit.distance;
                // correct ending velocity, since we didn't actually travel an entire segment
                segVelocity = segVelocity - Physics.gravity * (segmentScale - hit.distance) / segVelocity.magnitude;
                // flip the velocity to simulate a bounce
                segVelocity = Vector3.Reflect(segVelocity, hit.normal);

                Debug.Log("Collided with " + hit.collider.gameObject.name);

                segmentCount = i +1;

                /*
				 * Here you could check if the object hit by the Raycast had some property - was 
				 * sticky, would cause the ball to explode, or was another ball in the air for 
				 * instance. You could then end the simulation by setting all further points to 
				 * this last point and then breaking this for loop.
				 */
            }
            // If our raycast hit no objects, then set the next position to the last one plus v*t
            else
            {
                Debug.Log("Collided with NOTHING ");
                segments[i] = segments[i - 1] + segVelocity * segTime;
            }
        }

        // At the end, apply our simulations to the LineRenderer

        // Set the colour of our path to the colour of the next ball
       
        //StartColor.a = 1;
        //EndColor.a = 0;
        sightLine.startColor = StartColor;
        sightLine.endColor = EndColor;

        sightLine.positionCount = segmentCount;

        for (int i = 0; i < segmentCount; i++)
            sightLine.SetPosition(i, segments[i]);
    }
}