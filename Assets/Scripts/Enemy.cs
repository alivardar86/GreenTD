using UnityEngine;

/// <summary>
/// Enemy that follows waypoints and can take damage
/// </summary>
public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 2f;
    public Transform[] waypoints; // Set by WaveSpawner

    private int waypointIndex = 0;

    [Header("Health")]
    [SerializeField] private int maxHealth = 10;
    private int currentHealth;

    [Header("Rewards")]
    [SerializeField] private int goldReward = 1;

    private void Start()
    {
        currentHealth = maxHealth;

        // Move to first waypoint
        if (waypoints != null && waypoints.Length > 0)
        {
            transform.position = waypoints[0].position;
        }
        else
        {
            Debug.LogWarning("Enemy: Waypoints array is empty or null!");
        }
    }

    private void Update()
    {
        MoveAlongPath();
    }

    void MoveAlongPath()
    {
        if (waypoints == null || waypoints.Length == 0)
            return;

        if (waypointIndex >= waypoints.Length)
            return;

        Transform targetWaypoint = waypoints[waypointIndex];

        // Move toward current waypoint
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetWaypoint.position,
            speed * Time.deltaTime
        );

        // Check if reached waypoint
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.05f)
        {
            waypointIndex++;
        }
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
        {
            Debug.LogWarning("Enemy: Cannot take negative damage!");
            return;
        }

        currentHealth -= damage;
        
        Debug.Log($"Enemy took {damage} damage. Current HP: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Award gold to player
        if (GoldManager.Instance != null)
        {
            GoldManager.Instance.AddGold(goldReward);
        }

        Destroy(gameObject);
    }

    #region Public Getters
    public int GetCurrentHealth() => currentHealth;
    public int GetMaxHealth() => maxHealth;
    public float GetHealthPercentage() => (float)currentHealth / maxHealth;
    #endregion
}