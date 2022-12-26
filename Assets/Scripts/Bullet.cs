using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;

    private Transform targetPos;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    public void SetDestination(Transform target)
    {
        targetPos = target;
    }
}