using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [HideInInspector]
    public Vector3 startingPosition;

    private float minCameraX = 0f, maxCameraX = 14f;

    [HideInInspector]
    public bool isFollowing;
    [HideInInspector]
    public Transform birdToFollow;

	// Use this for initialization
	void Awake () {
        startingPosition = transform.position;

	}
	
	// Update is called once per frame
	void Update () {
        if (isFollowing)
        {
            if (birdToFollow != null)
            {
                var birdPosition = birdToFollow.position;
                float x = Mathf.Clamp(birdPosition.x, minCameraX, maxCameraX);
                transform.position = new Vector3(x, startingPosition.y, startingPosition.z);
            }
            else {
                isFollowing = false;
            }
        }
	}
}
