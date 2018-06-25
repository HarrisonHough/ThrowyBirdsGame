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
    public event BirdThrown birdthron;

	
	void Awake () {
        slingshotLineRenderer1.sortingLayerName = "Foreground";
        slingshotLineRenderer2.sortingLayerName = "Foreground";
        trajectoryLineRenderer.sortingLayerName = "Foreground";

        slingshotState = SlingshotState.Idle;
        slingshotLineRenderer1.SetPosition(0,leftSlingshotOrigin.position);
        slingshotLineRenderer2.SetPosition(0, rightSlingshotOrigin.position);

        slingshotMiddleVector = new Vector3((leftSlingshotOrigin.position.x + rightSlingshotOrigin.position.x)/2, (leftSlingshotOrigin.position.y + rightSlingshotOrigin.position.y)/2, 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
