using UnityEngine;

public class Hex : MonoBehaviour
{
    private bool occupied;

    void Start()
    {
    }

    void Update()
    {
    }

    public void SetOccupied(bool occupiedState = true)  
    {
        occupied = occupiedState;
    }

    public bool  GetOccupied()
    {
        return occupied;
    }
}