using UnityEngine;

/// <summary>
/// Tower that automatically targets and shoots enemies within range
/// </summary>
public class Tower : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float range = 3f;
    [SerializeField] private float fireRate = 1f; // shots per second
    [SerializeField] private int damage = 1;

    [Header("Detection")]
    [SerializeField] private LayerMask enemyLayer; // Set in Inspector to "Enemy" layer

    [Header("References")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint; // Where bullets spawn from

    private float fireCooldown = 0f;
    private Collider2D[] hitResults = new Collider2D[20]; // Reusable array for performance

    private void Update()
    {
        fireCooldown -= Time.deltaTime;
        
        if (fireCooldown <= 0f)
        {
            TryShoot();
        }
    }

    void TryShoot()
    {
        // Find enemies in range using NonAlloc for better performance
        int hitCount = Physics2D.OverlapCircleNonAlloc(
            transform.position, 
            range, 
            hitResults,
            enemyLayer
        );

        if (hitCount == 0)
            return;

        // Find nearest enemy
        Transform nearestTarget = null;
        float nearestDist = Mathf.Infinity;

        for (int i = 0; i < hitCount; i++)
        {
            Collider2D hit = hitResults[i];
            
            // Double-check tag (belt and suspenders approach)
            if (!hit.CompareTag(GameTags.Enemy))
                continue;

            float dist = Vector2.Distance(transform.position, hit.transform.position);
            if (dist < nearestDist)
            {
                nearestDist = dist;
                nearestTarget = hit.transform;
            }
        }

        if (nearestTarget != null)
        {
            Shoot(nearestTarget);
            fireCooldown = 1f / fireRate;
        }
    }

    void Shoot(Transform target)
    {
        if (bulletPrefab == null)
        {
            Debug.LogError("Tower: bulletPrefab is not assigned!");
            return;
        }

        // Use fire point if available, otherwise use tower center
        Transform spawnPoint = firePoint != null ? firePoint : transform;

        GameObject bulletGO = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        
        if (bullet != null)
        {
            bullet.Initialize(target, damage);
        }
        else
        {
            Debug.LogError("Tower: Bullet prefab missing Bullet component!");
            Destroy(bulletGO);
        }
    }

    // Visualize range in Editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    #region Public Getters
    public float GetRange() => range;
    public int GetDamage() => damage;
    public float GetFireRate() => fireRate;
    #endregion
}