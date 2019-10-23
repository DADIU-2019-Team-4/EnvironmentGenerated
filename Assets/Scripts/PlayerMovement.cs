using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float CoolDownValue = 0.1f;
    public float ChargeThreshold = 0.25f;
    public float DashDuration = 0.1f;
    public float MoveDuration = 0.2f;

    public int MoveDistance = 1;
    public int DashDistance = 2;

    public Transform StartLocation;

    private Rigidbody rigidBody;
    private Material material;
    private Vector3 spawnPos;
    private readonly int maxNegativeY = -5;
    private bool isMoving;
    private float colorValue = 1;

    private Vector3 lastPosition;
    private Grid grid;
    private TrailRenderer trailRenderer;

    public float Timer { get; set; }

    public bool IsDashCharged { get; set; }

    // Start is called before the first frame update
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        material = GetComponent<Renderer>().material;
        grid = FindObjectOfType<Grid>();
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.enabled = false;
        spawnPos = rigidBody.position;

        if (StartLocation == null)
            StartLocation = transform;

        Vector3Int cell = grid.WorldToCell(StartLocation.position);
        transform.position = grid.GetCellCenterWorld(cell);
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

    public void StartMove(Vector3Int moveDirection)
    {
        if (isMoving)
            return;

        var startCell = grid.WorldToCell(transform.position);
        var difference = moveDirection * MoveDistance;
        var targetCell = startCell + difference;

        StartCoroutine(MoveRoutine(targetCell, MoveDuration));
    }

    public void StartDash(Vector3Int dashDirection)
    {
        if (isMoving)
            return;

        trailRenderer.enabled = true;

        var startCell = grid.WorldToCell(transform.position);
        var difference = dashDirection * DashDistance;
        var targetCell = startCell + difference;
    
        StartCoroutine(MoveRoutine(targetCell, DashDuration));

        ResetDash();
    }

    public void ResetDash()
    {
        material.SetColor("_Color", Color.black);
        colorValue = 1f;
        IsDashCharged = false;
    }

    private IEnumerator MoveRoutine(Vector3Int target, float duration)
    {
        isMoving = true;

        var toPosition = grid.GetCellCenterWorld(target);
        rigidBody.DOMove(toPosition, duration);

        yield return new WaitForSeconds(duration);

        trailRenderer.enabled = false;

        yield return new WaitForSeconds(CoolDownValue);
        isMoving = false;
    }
}
