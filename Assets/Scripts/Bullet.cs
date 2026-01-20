using UnityEngine;

/// <summary>
/// Projectile that moves toward target and deals damage on hit
/// </summary>
public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    
    private int damage;
    private Transform target;

    /// <summary>
    /// Initialize the bullet with target and damage
    /// Called by Tower when spawning
    /// </summary>
    public void Initialize(Transform target, int damage)
    {
        this.target = target;
        this.damage = damage;
    }

    private void Update()
    {
        // Destroy if target is dead/destroyed
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Move toward target
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        // Check if close enough to hit
        if (Vector3.Distance(transform.position, target.position) < 0.05f)
        {
            HitTarget();
        }
    }

    void HitTarget()
    {
        // Verify target still has Enemy component
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}