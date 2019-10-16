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

    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = transform.position - player.transform.position;
        playerRigidbody = player.GetComponent<Rigidbody>();
        originalRotation = transform.eulerAngles;

        if (lighting != null)
            lightingOffset = lighting.transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        transform.position = player.transform.position + cameraOffset;

        float velocity = playerRigidbody.velocity.magnitude * XRotationMultiplier;
        transform.eulerAngles = new Vector3(originalRotation.x - velocity, originalRotation.y, originalRotation.z);

        if (lighting != null)
            lighting.transform.position = player.transform.position + lightingOffset;
    }
}
