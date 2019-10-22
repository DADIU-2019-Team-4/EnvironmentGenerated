using System;
using UnityEngine;

public class InputController2 : MonoBehaviour
{
    private Vector3 firstPosition;
    private Vector3 lastPosition;

    private bool trackMouse;

    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    /// <summary>
    /// Update function.
    /// </summary>
    public void Update()
    {
        HandleInput();
    }


    /// <summary>
    /// Handles the input.
    /// </summary>
    private void HandleInput()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        PcInput();
#elif UNITY_ANDROID || UNITY_IOS
        MobileInput();
#endif
    }


    /// <summary>
    /// Handles mobile input.
    /// </summary>
    private void MobileInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                firstPosition = touch.position;
                lastPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                lastPosition = touch.position;
                playerMovement.ContinuousMovement(lastPosition);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                playerMovement.Timer = 0;
                playerMovement.ResetDash();
            }
        }
    }


    /// <summary>
    /// Handles pc input.
    /// </summary>
    private void PcInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            trackMouse = true;
            firstPosition = Input.mousePosition;
            lastPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            trackMouse = false;
            playerMovement.ResetDash();
            playerMovement.Timer = 0;
        }       

        if (trackMouse)
        {
            lastPosition = Input.mousePosition;
            playerMovement.ContinuousMovement(lastPosition);
        }
    }
}
