using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    private static  Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
    }

    public void MoveToInput(RaycastHit hit)
    {
        GameObject target = hit.transform.gameObject;
        Vector3 position = target.transform.position;
        position.y = transform.position.y;
        agent.SetDestination(position);
    }

    public static bool CameraToMouseRay(Vector2 position, out RaycastHit hit)
    {
        Ray ray = cam.ScreenPointToRay(position);

        return Physics.Raycast(ray, out hit);
    }
}