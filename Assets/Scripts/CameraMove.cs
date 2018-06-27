using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    private float dragSpeed = 0.01f;
    private float timeDragStarted;
    private Vector3 previousPosition;

    public Slingshot slingshot;
	
	// Update is called once per frame
	void Update () {

        //TODO check and add clear comments
        if (slingshot.slingshotState == SlingshotState.Idle && GameManager.gamestate == GameState.Playing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                timeDragStarted = Time.time;
                dragSpeed = 0f;
                previousPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0) && Time.time - timeDragStarted > 0.005f)
            {
                Vector3 input = Input.mousePosition;
                float deltaX = (previousPosition.x - input.x) * dragSpeed;
                float deltaY = (previousPosition.y - input.y) * dragSpeed;

                float newX = Mathf.Clamp(transform.position.x + deltaX, 0, 14f);
                float newY = Mathf.Clamp(transform.position.y + deltaY, 0, 2.7f);

                transform.position = new Vector3(newX, newY, transform.position.z);

                previousPosition = input;

                if (dragSpeed < 0.1f)
                {
                    dragSpeed += 0.002f;
                }
            }
        }
	}
}
