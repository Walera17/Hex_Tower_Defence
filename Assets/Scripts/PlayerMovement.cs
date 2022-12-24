using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    private static Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
    }

    public void MoveToInput(RaycastHit hit)
    {
        if (hit.transform.TryGetComponent(out Hex hex))
        {
            if (hex.GetOccupied()) return;

            Vector3 position = hit.transform.position;

            SetDirection(position);
        }
    }

    public static bool CameraToMouseRay(Vector2 position, out RaycastHit hit)
    {
        Ray ray = cam.ScreenPointToRay(position);

        return Physics.Raycast(ray, out hit);
    }

    public void SetDirection(Vector3 position)
    {
        position.y = transform.position.y;
        agent.SetDestination(position);
    }
}