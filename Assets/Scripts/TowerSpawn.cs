using System.Collections.Generic;
using UnityEngine;

public class TowerSpawn : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;

    private GameObject tower;
    private Vector3 towerPosition;
    private Quaternion towerRotation;
    private Vector3 towerAdjacentPosition;
    private bool isCreateTower;

    private void Start()
    {
        tower = Resources.Load<GameObject>("Tower");
    }

    private void Update()
    {
        if (isCreateTower && (towerAdjacentPosition - playerMovement.transform.position).sqrMagnitude < 0.01f)
            InstantiateTower();
    }

    public void SpawnTower(RaycastHit hit)
    {
        if (hit.transform.TryGetComponent(out Hex hex))
        {
            hex.SetOccupied();

            Vector3 point = hit.transform.position;
            point.y = hit.point.y;

            towerPosition = point;
            towerRotation = hit.transform.localRotation;

            if (GetAdjacentPosition(hex, out Vector3 newPlayerPosition))
            {
                towerAdjacentPosition = newPlayerPosition;
                towerAdjacentPosition.y = playerMovement.transform.position.y;
                playerMovement.SetDirection(newPlayerPosition);
                isCreateTower = true;
            }
        }
    }

    bool GetAdjacentPosition(Hex hex,  out Vector3 position)
    {
        Vector3 playerPosition = playerMovement.transform.position;
        List<Vector3> adjacentClosestPositions = new List<Vector3>(hex.GetAdjacentClosestPosition());

        if (adjacentClosestPositions.Count > 0)
        {
            if (adjacentClosestPositions.Count > 1)
                adjacentClosestPositions.Sort((a, b) => (a - playerPosition).sqrMagnitude.CompareTo((b - playerPosition).sqrMagnitude));

            position = adjacentClosestPositions[0];
            return true;
        }

        position = playerPosition;
        return false;
    }

    public void InstantiateTower()
    {
        isCreateTower = false;
        Instantiate(tower, towerPosition, towerRotation);
    }
}