using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float maxHealth = 100;

    private float health;

    void OnEnable()
    {
        health = maxHealth;
        agent.SetDestination(EnemySpawner.basePos);
    }

    public void TakeDamage(float damage = 10)
    {
        health -= damage;
        if (health <= 0)
            gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == EnemySpawner.basePlayer)
            gameObject.SetActive(false);
    }
}