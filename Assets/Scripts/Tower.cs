using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Attack Settings")]
    public float range = 3f;
    public float fireRate = 1f; // saniyede 1 atış
    public int damage = 1;

    [Header("References")]
    public GameObject bulletPrefab;
    public Transform firePoint; // merminin çıkacağı nokta

    private float fireCooldown = 0f;

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
        // Range içindeki enemy'leri bul
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range);

        Transform nearestTarget = null;
        float nearestDist = Mathf.Infinity;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                float dist = Vector2.Distance(transform.position, hit.transform.position);
                if (dist < nearestDist)
                {
                    nearestDist = dist;
                    nearestTarget = hit.transform;
                }
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
        // Fire point yoksa tower merkezini kullan
        Transform spawnPoint = firePoint != null ? firePoint : transform;

        GameObject bulletGO = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.target = target;
            bullet.damage = damage;
        }
    }

    // Editorde range'i görmek için
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
