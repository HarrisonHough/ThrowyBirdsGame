using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private Slingshot slingshot;


    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        switch (GameManager.Instance.CurrentState)
        {
            case GameState.InMenu:
                
                break;
            case GameState.InGame:
                SlingShotControls();
                break;
            case GameState.GameOver:
                break;
        }
        
    }

    private void SlingShotControls()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (slingshot.BirdToThrow.IsCursorOverBird(location))
                slingshot.OnMouseDown();

        }
        if (Input.GetMouseButton(0))
        {
            Vector3 location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            slingshot.OnMouseHold(location);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector3 location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            slingshot.OnMouseRelease(location);
        }
    }

    private void CameraMovementControls()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

        }

        if (Input.touchCount == 2)
        {

        }
    }
}
