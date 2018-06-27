using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroll : MonoBehaviour {

    public float parallaxFactor;
    private Vector3 previousCameraPosition;

	// Use this for initialization
	void Start () {
        previousCameraPosition = Camera.main.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 delta = Camera.main.transform.position - previousCameraPosition;
        delta.y = 0f;
        delta.x = 0f;
        transform.position += delta / parallaxFactor;

        previousCameraPosition = Camera.main.transform.position;
	}
}
