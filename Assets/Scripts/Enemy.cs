using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    public Transform[] waypoints;
    public float speed = 2f;

    private int waypointIndex = 0;

    [Header("Health")]
    public int maxHealth = 10;
    private int currentHealth;

    private void Start()
    {
        // Canı başlat
        currentHealth = maxHealth;

        // Başlangıç pozisyonu
        if (waypoints.Length > 0)
        {
            transform.position = waypoints[0].position;
        }
        else
        {
            Debug.LogWarning("Enemy: Waypoints array is empty!");
        }
    }

    private void Update()
    {
        MoveAlongPath();
    }

    void MoveAlongPath()
    {
        if (waypoints.Length == 0) return;
        if (waypointIndex >= waypoints.Length) return;

        Transform target = waypoints[waypointIndex];

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target.position) < 0.05f)
        {
            waypointIndex++;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy took damage. Current HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
         // Şimdilik her enemy 1 gold versin
    if (GoldManager.Instance != null)
    {
        GoldManager.Instance.AddGold(1);
    }

    Destroy(gameObject);
    }
}
