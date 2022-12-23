using UnityEngine;

public class TowerSpawn : MonoBehaviour
{
    private GameObject tower;

    private void Start()
    {
        tower = Resources.Load<GameObject>("Tower");
    }

    public void SpawnTower(RaycastHit hit)
    {
        Vector3 point = hit.transform.position;
        point.y = hit.point.y;

        Instantiate(tower, point, hit.transform.localRotation);
    }
}