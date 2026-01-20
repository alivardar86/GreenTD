using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject enemyPrefab;   // Hangi enemy'yi spawn edeceğiz
    public Transform spawnPoint;     // Nereden çıksın
    public Transform[] waypoints; 
    [Header("Wave Settings")]
    public int enemiesPerWave = 5;       // Her wave'de kaç enemy
    public float timeBetweenEnemies = 0.7f; // Aynı wave içindeki spawn aralığı
    public float timeBetweenWaves = 5f;     // Wave'ler arası bekleme

    private int currentWave = 0;

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (true) // Şimdilik sonsuz, ileride sınırlayacağız
        {
            currentWave++;
            Debug.Log("Wave " + currentWave + " başladı");
            if (LivesUI.Instance != null)
{
    LivesUI.Instance.SetWave(currentWave);
}
            // Bu wave içindeki enemy'leri yolla
            for (int i = 0; i < enemiesPerWave; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(timeBetweenEnemies);
            }

            Debug.Log("Wave " + currentWave + " bitti");

            // Bir sonraki wave'den önce bekle
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    void SpawnEnemy()
{
    if (enemyPrefab == null || spawnPoint == null)
    {
        Debug.LogWarning("WaveSpawner: enemyPrefab veya spawnPoint atanmadı!");
        return;
    }

    GameObject enemyGO = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

    // 🔹 Spawn edilen enemy'e waypoint'leri ver
    Enemy enemy = enemyGO.GetComponent<Enemy>();
    if (enemy != null)
    {
        enemy.waypoints = waypoints;
    }
}
}
