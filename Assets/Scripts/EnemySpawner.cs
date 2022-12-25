using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float maxTimer = 1f;
    [SerializeField] private Transform enemiesParent;

    public Dictionary<string, Queue<GameObject>> poolDictionary;

    public List<ObjectPool> pools;

    private readonly List<string> enemyTypes = new List<string>();
    public List<string> EnemyTypes => enemyTypes;

    private float timer;

    public static Vector3 basePos;
    public static GameObject basePlayer;


    void Start()
    {
        timer = maxTimer;
        FindStaticDataObject();
        CreatePooling();
    }

    private void FindStaticDataObject()
    {
        basePlayer = GameObject.Find("Base");
        basePos = basePlayer.transform.position;
    }

    private void CreatePooling()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (ObjectPool objectPool in pools)
        {
            EnemyTypes.Add(objectPool.type);
        }

        InstantiateEnemies();
    }

    private void InstantiateEnemies()
    {
        GameObject item;
        int amount;

        foreach (ObjectPool objectPool in pools)
        {
            Queue<GameObject> tempQueue = new Queue<GameObject>();

            amount = objectPool.amount;

            for (int i = 0; i < amount; i++)
            {
                item = Instantiate(objectPool.prefab, enemiesParent);
                item.SetActive(false);
                tempQueue.Enqueue(item);
            }

            poolDictionary.Add(objectPool.type, tempQueue);
        }
    }

    void EnableEnemies(string type, Vector3 position)
    {
        GameObject enemy = poolDictionary[type].Dequeue();
        enemy.transform.position = position;
        enemy.SetActive(true);
        poolDictionary[type].Enqueue(enemy);
    }

    void Update()
    {
        if (timer >= 0)
            timer -= Time.deltaTime;
        else
        {
            Vector3 position = transform.position + Vector3.up + Vector3.ProjectOnPlane(Random.onUnitSphere, Vector3.up).normalized * 2f;
            EnableEnemies(enemyTypes[Random.Range(0, enemyTypes.Count)], position);
            timer = maxTimer;
        }
    }
}

[System.Serializable]
public class ObjectPool
{
    public string type;
    public int amount;
    public GameObject prefab;
}