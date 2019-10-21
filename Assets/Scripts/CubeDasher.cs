using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDasher : MonoBehaviour
{
    Rigidbody rg;
    private Vector3 SpawnPosition;
    private readonly int MaxNegativeY = -5;

    public int moveMultiplier = 10;
    public int dashVelStrength = 2000;
    public float dashSeconds = 0.3f;

    //private Vector3 previousFramePosition;

    private Transform CameraTransform;

    private bool isDashing;

    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();
        SpawnPosition = rg.position;
        CameraTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rg.position.y < MaxNegativeY)
        {
            //rg.position = previousFramePosition = SpawnPosition;
            rg.position = SpawnPosition;
            rg.velocity = Vector3.zero;
            return;
        }

        //previousFramePosition = transform.position;
    }


    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0) && !isDashing)
            StartDash();
        else if (isDashing)
            WhileDash();
        else if (!isDashing)
            BasicMovement();
    }

    private void StartDash()
    {
        isDashing = true;
        Vector3 dashDirection;

        if (rg.velocity == Vector3.zero)
            dashDirection = Mathf.Sin(CameraTransform.eulerAngles.y * Mathf.Deg2Rad) * Vector3.right + Mathf.Cos(CameraTransform.eulerAngles.y * Mathf.Deg2Rad) * Vector3.forward;
        else
            dashDirection = rg.velocity.normalized;

        rg.velocity = Vector3.zero;
        rg.AddForce(dashDirection * dashVelStrength);
        StartCoroutine(Recontrol());
    }

    private void WhileDash()
    {
        // Not influenced by camera alone.
        // Only influenced by controls (with relative camera).
        // You are only allowed to turn, not slow down.

        // Get the current direction -> normalize -> find whether to dash left/right -> set new (normalized angle) -> multiply velocity length with new angle.



    }

    private void BasicMovement()
    {
        rg.velocity = new Vector3(
            moveMultiplier * (Mathf.Sin(CameraTransform.eulerAngles.y * Mathf.Deg2Rad) * Input.GetAxis("Vertical") + Mathf.Cos(CameraTransform.eulerAngles.y * Mathf.Deg2Rad) * Input.GetAxis("Horizontal")),
            rg.velocity.y,
            moveMultiplier * (Mathf.Cos(CameraTransform.eulerAngles.y * Mathf.Deg2Rad) * Input.GetAxis("Vertical") - Mathf.Sin(CameraTransform.eulerAngles.y * Mathf.Deg2Rad) * Input.GetAxis("Horizontal")));
    }

    IEnumerator Recontrol()
    {
        yield return new WaitForSeconds(dashSeconds);
        isDashing = false;
    }




}
