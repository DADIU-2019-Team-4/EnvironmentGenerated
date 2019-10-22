using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private float minSwipeDistanceInPercentage = 0.10f;
    private float verticalSwipeDistance;
    private float horizontalSwipeDistance;

    private readonly Vector3[] firstPosition = new Vector3[2];
    private readonly Vector3[] lastPosition = new Vector3[2];
    private bool hasSwiped;

    private bool trackMouse;

    private PlayerMovement playerMovement;
    private float timer;

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Start()
    {
        verticalSwipeDistance = Screen.height * minSwipeDistanceInPercentage;
        horizontalSwipeDistance = Screen.width * minSwipeDistanceInPercentage;
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
        Touch[] touches = Input.touches;
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (touches[i].phase == TouchPhase.Began)
            {
                firstPosition[i] = touches[i].position;
                lastPosition[i] = touches[i].position;
            }
            else if (touches[i].phase == TouchPhase.Stationary)
            {
                timer += Time.deltaTime;
                if (timer >= playerMovement.ChargeThreshold)
                {
                    playerMovement.ChargeDash();
                    playerMovement.IsDashCharged = true;
                }
            }
            else if (touches[i].phase == TouchPhase.Moved)
            {
                if (!hasSwiped)
                {
                    lastPosition[i] = touches[i].position;
                    CheckSwipe(i);
                }
            }
            else if (touches[i].phase == TouchPhase.Ended)
            {
                hasSwiped = false;
                timer = 0;
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
            firstPosition[0] = Input.mousePosition;
            lastPosition[0] = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            timer += Time.deltaTime;
            if (timer >= playerMovement.ChargeThreshold)
            {
                playerMovement.ChargeDash();
                playerMovement.IsDashCharged = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            trackMouse = false;
            timer = 0;
            playerMovement.ResetDash();
        }       

        if (trackMouse)
        {
            lastPosition[0] = Input.mousePosition;
            CheckSwipe(0);
        }
    }


    /// <summary>
    /// Checks how to player has swiped.
    /// </summary>
    private void CheckSwipe(int i)
    {
        Vector3 directionVector = lastPosition[i] - firstPosition[i];

        if (SwipedLongEnough(directionVector)) return;

        if (Mathf.Abs(directionVector.x) > Mathf.Abs(directionVector.y))
        {
            if (playerMovement.IsDashCharged)
                playerMovement.StartDash(HorizontalSwipe(i));
            else
                playerMovement.StartMove(HorizontalSwipe(i));
        }
        else
        {
            if (playerMovement.IsDashCharged)
                playerMovement.StartDash(VerticalSwipe(i));
            else
                playerMovement.StartMove(VerticalSwipe(i));
        }

        hasSwiped = true;
    }


    private bool SwipedLongEnough(Vector3 direction)
    {
        return (!(Math.Abs(direction.x) > horizontalSwipeDistance) &&
            !(Math.Abs(direction.y) > verticalSwipeDistance));
    }


    private Vector3 HorizontalSwipe(int i)
    {
        if (lastPosition[i].x > firstPosition[i].x)
            return Vector3.right;

        return Vector3.left;
    }


    private Vector3 VerticalSwipe(int i)
    {
        if (lastPosition[i].y > firstPosition[i].y)
            return Vector3.forward;

        return Vector3.back;
    }
}
