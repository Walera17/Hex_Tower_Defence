using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
    private bool occupied;
    readonly List<Vector3> adjacentHexPositions = new List<Vector3>();
    readonly List<Hex> adjacentHexes = new List<Hex>();

    private static readonly Vector3[] directions =
    {
        Vector3.right,
        Vector3.forward - Vector3.right,
        Vector3.forward - Vector3.left,
        Vector3.left,
        Vector3.back - Vector3.left,
        Vector3.back - Vector3.right
    };

    void Start()
    {
        SetAdjacentHexPositions();
    }

    public void SetOccupied(bool occupiedState = true)
    {
        occupied = occupiedState;
    }

    public bool GetOccupied()
    {
        return occupied;
    }

    void SetAdjacentHexPositions()
    {
        foreach (Vector3 direction in directions)
        {
            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, 3.6f) && hit.transform.TryGetComponent(out Hex hex))
            {
                adjacentHexes.Add(hex);
                adjacentHexPositions.Add(hex.transform.position);
            }
        }
    }

    public IEnumerable<Vector3> GetAdjacentClosestPosition()
    {
        for (int i = 0; i < adjacentHexes.Count; i++)
        {
            if (!adjacentHexes[i].GetOccupied())
                yield return adjacentHexPositions[i];
        }
    }
}