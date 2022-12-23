using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private TowerSpawn towerSpawn;

    private bool isTouching;
    private float touchTimer;

    private void Update()
    {
#if UNITY_STANDALONE_WIN
        EvaluatePersonalComputerInputs();
#else
        if (Input.touchCount > 0)
            EvaluateMobileInputs();
#endif
    }

    void EvaluateMobileInputs()
    {
        if (!CameraToMouseRay(out RaycastHit hit)) return;

        if (Input.touches[0].phase == TouchPhase.Began)
            isTouching = true;
        else if (Input.touches[0].phase == TouchPhase.Stationary && isTouching)
            touchTimer += Time.deltaTime;

        if ((Input.touches[0].phase == TouchPhase.Canceled || Input.touches[0].phase == TouchPhase.Ended || touchTimer > 0.3f) && isTouching)
        {
            if (touchTimer > 0.3f)
                towerSpawn.SpawnTower(hit);
            else
                playerMovement.MoveToInput(hit);

            isTouching = false;
        }
    }

    void EvaluatePersonalComputerInputs()
    {
        if (!CameraToMouseRay(out RaycastHit hit)) return;

        if (Input.GetMouseButtonDown(0))
            playerMovement.MoveToInput(hit);
        else if (Input.GetMouseButtonDown(1))
            towerSpawn.SpawnTower(hit);
    }

    private bool CameraToMouseRay(out RaycastHit hit)
    {
        return PlayerMovement.CameraToMouseRay(Input.mousePosition, out hit);
    }
}