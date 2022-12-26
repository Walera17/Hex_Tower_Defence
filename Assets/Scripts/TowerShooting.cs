using System.Collections.Generic;
using UnityEngine;

public class TowerShooting : MonoBehaviour
{
    [SerializeField] private Transform bullet;
    [SerializeField] private Transform shootingPointPivot;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private float maxTimer = 0.5f;
    [SerializeField] private float maxDistance = 15f;

    public List<Enemy> enemies = new List<Enemy>();

    private Transform targetEnemy;
    private float timer;

    private void Start()
    {
        timer = maxTimer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
            enemies.Add(enemy);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
            enemies.Remove(enemy);
    }

    private void Update()
    {
        Aim();
        if (timer >= 0)
            timer -= Time.deltaTime;
    }

    private void Aim()
    {
        int index = 0;
        int count = enemies.Count;

        while (index < count)
        {
            if (!enemies[index].gameObject.activeSelf)
            {
                enemies.RemoveAt(index);
                count--;
                continue;
            }

            if (targetEnemy != null && IsVisual(targetEnemy, out RaycastHit targetEnemyHit))
            {
                if (targetEnemyHit.transform.TryGetComponent(out Enemy target) && targetEnemy == target.transform)
                {
                    LookAtEnemy();
                    if (timer < 0)
                        ShootBullet(targetEnemy);
                    return;
                }
            }

            if (!IsVisual(enemies[index].transform, out RaycastHit hit))
            {
                index++;
                continue;
            }

            if (hit.transform.TryGetComponent(out Enemy enemy))
            {
                targetEnemy = enemy.transform;
                LookAtEnemy();

                if (timer < 0)
                    ShootBullet(hit.transform);

                return;
            }

            index++;
        }
    }

    private bool IsVisual(Transform enemyTransform, out RaycastHit hit)
    {
        return Physics.Raycast(shootingPoint.position, enemyTransform.position - shootingPoint.position,
            out hit, maxDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore);
    }

    private void LookAtEnemy()
    {
        if (timer > 0.2f) return;

        transform.LookAt(new Vector3(targetEnemy.position.x, transform.position.y, targetEnemy.position.z));
        shootingPointPivot.LookAt(targetEnemy);
    }

    private void ShootBullet(Transform target)
    {
        Instantiate(bullet, shootingPoint.position, shootingPoint.rotation).GetComponent<Bullet>().SetDestination(target);
        timer = maxTimer;
    }
}