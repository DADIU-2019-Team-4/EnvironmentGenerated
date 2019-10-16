using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentBuildingBlock : MonoBehaviour
{
    private Vector3 originalPosition;
    private int BuildSpeed = 33;
    private static int DistanceToTrigger = 4;
    private GameObject player;
    private bool wasTriggered = false;
    private bool isMoving = false;

    private int spawnDistance = 5;
    private Vector3 spawnDirection;

    private Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        GetComponent<MeshRenderer>().enabled = false;

        int random = Random.Range(0, 6);
        switch (random)
        {
            case 0:
                spawnDirection = Vector3.back;
                break;

            case 1:
                spawnDirection = Vector3.forward;
                break;

            case 2:
                spawnDirection = Vector3.left;
                break;

            case 3:
                spawnDirection = Vector3.right;
                break;

            case 4:
                spawnDirection = Vector3.up;
                break;

            case 5:
            default:
                spawnDirection = Vector3.down;
                break;

        }
        //spawnDirection = Vector3.down;


        moveDirection = -spawnDirection;
        spawnDirection *= spawnDistance;
        transform.position += spawnDirection;
    }

    // Update is called once per frame
    void Update()
    {
        if (!wasTriggered && (player.transform.position - originalPosition).magnitude < DistanceToTrigger)
        {
            wasTriggered = isMoving = true;
            GetComponent<MeshRenderer>().enabled = true;
        }

        if (isMoving)
        {
            transform.position += (BuildSpeed / 100f) * moveDirection;
            if ((transform.position - originalPosition).magnitude < 0.25)
            {
                isMoving = false;
                transform.position = originalPosition;
                GetComponent<BoxCollider>().enabled = true;
            }
        }

    }



}
