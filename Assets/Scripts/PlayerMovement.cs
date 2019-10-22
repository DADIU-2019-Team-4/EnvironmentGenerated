using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int DashMultiplier = 2000;
    public int MoveMultiplier = 200;
    public float CoolDownValue = 0.3f;
    public float ChargeThreshold = 0.25f;
    public float DashDuration = 0.1f;
    public float MoveDuration = 0.2f;

    private Rigidbody rigidBody;
    private Material material;
    private Vector3 spawnPos;
    private readonly int maxNegativeY = -5;
    private bool isMoving;
    private float colorValue = 1;

    public bool IsDashCharged { get; set; }

    // Start is called before the first frame update
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        material = GetComponent<Renderer>().material;
        spawnPos = rigidBody.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (rigidBody.position.y < maxNegativeY)
        {
            rigidBody.position = spawnPos;
            rigidBody.velocity = Vector3.zero;
        }
    }

    public void ChargeDash()
    {
        material.color = new Color(1, colorValue, colorValue, 1);
        colorValue -= 0.05f;
    }

    public void StartDash(Vector3 dashDirection)
    {
        if (isMoving)
            return;

        isMoving = true;

        rigidBody.velocity = Vector3.zero;
        rigidBody.AddForce(dashDirection * DashMultiplier);

        ResetDash();

        StartCoroutine(StartCoolDown(DashDuration));
    }

    public void ResetDash()
    {
        material.SetColor("_Color", Color.white);
        colorValue = 1f;
        IsDashCharged = false;
    }

    public void StartMove(Vector3 moveDirection)
    {
        if (isMoving)
            return;

        isMoving = true;

        rigidBody.velocity = Vector3.zero;
        rigidBody.AddForce(moveDirection * MoveMultiplier);
        StartCoroutine(StartCoolDown(MoveDuration));
    }

    IEnumerator StartCoolDown(float duration)
    {
        yield return  new WaitForSeconds(duration);
        rigidBody.velocity = Vector3.zero;

        yield return new WaitForSeconds(CoolDownValue);
        isMoving = false;
    }
}
