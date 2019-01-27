using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2019
* VERSION: 1.0
* SCRIPT: Input Controller Class
*/

/// <summary>
/// 
/// </summary>
public class InputController : MonoBehaviour
{
    //store reference to slingshot
    [SerializeField]
    private Slingshot slingshot;

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        GetInput();
    }

    /// <summary>
    /// Get user input depending on game state
    /// </summary>
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

    /// <summary>
    /// Handles input for slingshot control
    /// </summary>
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

    /// <summary>
    /// 
    /// </summary>
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
