using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody rg;
    private Vector3 SpawnPosition;
    private int MaxNegativeY = -5;

    public int moveMultiplier = 3;

    private Vector3 previousFramePosition;
    private Life Life;

    private Transform CameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();
        SpawnPosition = rg.position;
        Life = gameObject.GetComponent<Life>();
        CameraTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rg.position.y < MaxNegativeY)
        {
            rg.position = previousFramePosition = SpawnPosition;
            rg.velocity = Vector3.zero;
            return;
        }

        Life.UpdateDistanceTravelled((previousFramePosition - transform.position).magnitude);
        previousFramePosition = transform.position;
    }

    private void FixedUpdate()
    {
        Vector3 force = new Vector3(
            Mathf.Sin(CameraTransform.eulerAngles.y * Mathf.Deg2Rad) * Input.GetAxis("Vertical") + Mathf.Cos(CameraTransform.eulerAngles.y * Mathf.Deg2Rad) * Input.GetAxis("Horizontal"),
            0.0f,
            Mathf.Cos(CameraTransform.eulerAngles.y * Mathf.Deg2Rad) * Input.GetAxis("Vertical") - Mathf.Sin(CameraTransform.eulerAngles.y * Mathf.Deg2Rad) * Input.GetAxis("Horizontal"));
        rg.AddForce(moveMultiplier * force);
    }


}
