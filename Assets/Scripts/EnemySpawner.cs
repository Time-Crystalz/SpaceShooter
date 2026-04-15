using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Vector3[] spawnPoints;
    [SerializeField] private int poolSize = 3;
    [SerializeField] private float timeBetweenWaves = 2f; 
    private Queue<GameObject> enemyQueue = new Queue<GameObject>();
    private int activeEnemyCount = 0;
    public static EnemySpawner Instance{get;private set;}
    private bool isSpawning = false; 
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        SpawnWave();
    }
    public void SpawnWave()
    {
        
        if (isSpawning == true)
        {
            return;
        }
        if (activeEnemyCount > 0)
        {
            return;
        }
        StartCoroutine(SpawnRoutine());
    }
    private IEnumerator SpawnRoutine()
    {
        isSpawning = true; 
        yield return new WaitForSeconds(timeBetweenWaves); 
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy;
            if (enemyQueue.Count > 0)
            {
                enemy = enemyQueue.Dequeue();
            }
            else
            {
                enemy = Instantiate(enemyPrefab);
            }
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                enemyComponent.Spawner = this;
            }
            Health health = enemy.GetComponent<Health>();
            if (health != null)
            {
                health.ResetHealth();
            }
            enemy.transform.position = GetSpawnPoint();
            enemy.SetActive(true);
            activeEnemyCount++;
        }
        isSpawning = false; 
    }
    public void ReturnToPool(GameObject enemy)
    {
        enemy.SetActive(false);
        enemyQueue.Enqueue(enemy);
        activeEnemyCount--;
        
        if (activeEnemyCount <= 0)
        {
            SpawnWave(); 
        }
    }
    private Vector3 GetSpawnPoint()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        return spawnPoints[randomIndex];
    }
}
