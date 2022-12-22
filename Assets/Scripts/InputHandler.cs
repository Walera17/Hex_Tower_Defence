using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;

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
        if (Input.touches[0].phase == TouchPhase.Began)
            MoveToHit();
    }

    void EvaluatePersonalComputerInputs()
    {
        if (Input.GetMouseButtonDown(0))
            MoveToHit();
    }

    private void MoveToHit()
    {
        if (PlayerMovement.CameraToMouseRay(Input.mousePosition, out RaycastHit hit))
            playerMovement.MoveToInput(hit);
    }
}