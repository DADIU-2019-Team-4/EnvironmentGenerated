using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public GameObject player;
    private Rigidbody playerRigidbody;

    private Vector3 cameraOffset;
    private Vector3 originalRotation;

    public float XRotationMultiplier;

    public GameObject lighting;
    private Vector3 lightingOffset;

    private float cameraRot = 1f;
    private float baseDistance;
    private float MouseXMultiplier = 2f;

    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = transform.position - player.transform.position;
        playerRigidbody = player.GetComponent<Rigidbody>();
        originalRotation = transform.eulerAngles;

        if (lighting != null)
            lightingOffset = lighting.transform.position - player.transform.position;

        baseDistance = -transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void FixedUpdate()
    {

    }

    private void LateUpdate()
    {
        //transform.position = player.transform.position + cameraOffset;

        //float velocity = playerRigidbody.velocity.magnitude * XRotationMultiplier;
        //transform.eulerAngles = new Vector3(originalRotation.x - velocity, originalRotation.y, originalRotation.z);

        Vector3 inputCameraRotation = cameraRot * (new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X") * MouseXMultiplier, 0));
        transform.eulerAngles += inputCameraRotation;
        transform.position = player.transform.position +
            new Vector3(
            Mathf.Cos(transform.eulerAngles.x * Mathf.Deg2Rad) * -Mathf.Sin(transform.eulerAngles.y * Mathf.Deg2Rad) * baseDistance,
            Mathf.Sin(transform.eulerAngles.x * Mathf.Deg2Rad) * baseDistance,
            Mathf.Cos(transform.eulerAngles.x * Mathf.Deg2Rad) * -Mathf.Cos(transform.eulerAngles.y * Mathf.Deg2Rad) * baseDistance);

        if (lighting != null)
            lighting.transform.position = player.transform.position + lightingOffset;

    }
}
