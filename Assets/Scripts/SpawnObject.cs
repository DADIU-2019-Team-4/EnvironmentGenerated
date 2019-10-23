using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnObject : MonoBehaviour
{
    public GameObject objectToSpawn;
    private Grid grid;

    // Start is called before the first frame update
    void Start()
    {
        grid = FindObjectOfType<Grid>();
        Vector3Int cell = grid.WorldToCell(transform.position);
        transform.position = grid.GetCellCenterWorld(cell);

        Instantiate(objectToSpawn, transform.position, Quaternion.identity, transform);
    }
}
