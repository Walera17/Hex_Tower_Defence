using UnityEngine;

public class HexPainter : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Hex hex) || hex == playerMovement.PrevActiveHex) return;

        hex.SetOccupiedPlayer(new Color(0f, 0.59f, 0.7f));
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out Hex hex)) return;

        hex.SetOccupied(false);
    }
}