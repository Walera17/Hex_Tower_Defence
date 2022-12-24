using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Color highlightedColor;

    private bool occupied;
    readonly List<Vector3> adjacentHexPositions = new List<Vector3>();
    readonly List<Hex> adjacentHexes = new List<Hex>();
    private Color normalColor4;
    private Color normalColor2;
    Material[] materials;

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
        materials = meshRenderer.materials;
        normalColor2 = materials[2].color;
        normalColor4 = materials[4].color;
        SetAdjacentHexPositions();
    }

    public void SetMaterial(bool highlight = true)
    {
        if (highlight)
        {
            materials[2].color = highlightedColor;
            materials[4].color = highlightedColor;
        }
        else if (!occupied)
        {
            materials[2].color = normalColor2;
            materials[4].color = normalColor4;
        }
    }

    public void SetOccupied(bool occupiedState = true)
    {
        occupied = occupiedState;
        SetMaterial(occupiedState);
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

    public Hex GetAdjacentHexOfPosition(Vector3 position)
    {
        for (int i = 0; i < adjacentHexPositions.Count; i++)
        {
            if ((position - adjacentHexPositions[i]).sqrMagnitude < 0.05f)
                return adjacentHexes[i];
        }

        return null;
    }

    public void SetOccupiedPlayer(Color color, bool occupiedState = true)
    {
        occupied = occupiedState;
        materials[2].color = color;
        materials[4].color = color;
    }
}