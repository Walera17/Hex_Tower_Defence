using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    private static Camera cam;
    private static int layers;

    private Hex prevActiveHex;

    public Hex PrevActiveHex => prevActiveHex;

    private void Start()
    {
        layers = LayerMask.GetMask("Hex", "Tower");
        cam = Camera.main;
    }

    public void MoveToInput(RaycastHit hit)
    {
        if (hit.transform.TryGetComponent(out Hex hex))
        {
            if (hex.GetOccupied()) return;

            ActivateHex(hex);

            Vector3 position = hit.transform.position;

            SetDirection(position);
        }
    }

    public void ActivateHex(Hex hex)
    {
        if (PrevActiveHex != null)
            PrevActiveHex.SetOccupied(false);

        prevActiveHex = hex;
        PrevActiveHex.SetOccupiedPlayer(Color.cyan);
    }

    public static bool CameraToMouseRay(Vector2 position, out RaycastHit hit)
    {
        Ray ray = cam.ScreenPointToRay(position);

        return Physics.Raycast(ray, out hit, Mathf.Infinity, layers);
    }

    public void SetDirection(Vector3 position)
    {
        position.y = transform.position.y;
        agent.SetDestination(position);
    }

    public void SetDirection(Hex hex, Vector3 newPlayerPosition)
    {
        Hex hexOfPosition = hex.GetAdjacentHexOfPosition(newPlayerPosition);
        ActivateHex(hexOfPosition);

        SetDirection(newPlayerPosition);
    }
}