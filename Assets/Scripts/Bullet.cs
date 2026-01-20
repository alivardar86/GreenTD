using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 2;
    public Transform target;

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Hedefe doğru ilerle
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        // Çok yaklaştıysak vurmuş say
        if (Vector3.Distance(transform.position, target.position) < 0.05f)
        {
            HitTarget();
        }
    }

    void HitTarget()
    {
        // Enemy script'ini bul ve damage ver
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
