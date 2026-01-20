using System.Collections;
using UnityEngine;

/// <summary>
/// Spawns waves of enemies with configurable settings
/// </summary>
public class WaveSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform[] waypoints;

    [Header("Wave Settings")]
    [SerializeField] private int totalWaves = 20; // Total number of waves
    [SerializeField] private int enemiesPerWave = 5;
    [SerializeField] private float timeBetweenEnemies = 0.7f; // Spawn interval within wave
    [SerializeField] private float timeBetweenWaves = 5f; // Delay between waves

    private int currentWave = 0;
    private bool isSpawning = false;

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        isSpawning = true;

        // Spawn all waves
        for (int wave = 1; wave <= totalWaves; wave++)
        {
            currentWave = wave;
            
            Debug.Log($"Wave {currentWave} started");
            UpdateWaveUI();

            // Spawn enemies in this wave
            for (int i = 0; i < enemiesPerWave; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(timeBetweenEnemies);
            }

            Debug.Log($"Wave {currentWave} completed");

            // Wait before next wave (but not after final wave)
            if (wave < totalWaves)
            {
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }

        // All waves completed
        Debug.Log("All waves completed! Victory!");
        isSpawning = false;
        
        // TODO: Trigger victory screen
        // GameEvents.OnVictory?.Invoke();
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("WaveSpawner: enemyPrefab is not assigned!");
            return;
        }

        if (spawnPoint == null)
        {
            Debug.LogError("WaveSpawner: spawnPoint is not assigned!");
            return;
        }

        // Instantiate enemy
        GameObject enemyGO = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        // Assign waypoints to enemy
        Enemy enemy = enemyGO.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.waypoints = waypoints;
        }
        else
        {
            Debug.LogError("WaveSpawner: Enemy prefab missing Enemy component!");
        }
    }

    void UpdateWaveUI()
    {
        if (LivesUI.Instance != null)
        {
            LivesUI.Instance.SetWave(currentWave);
        }
    }

    #region Public Getters
    public int GetCurrentWave() => currentWave;
    public int GetTotalWaves() => totalWaves;
    public bool IsSpawning() => isSpawning;
    #endregion
}