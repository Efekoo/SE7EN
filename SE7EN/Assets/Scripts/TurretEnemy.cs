using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TurretEnemy : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;

    public float detectionRadius = 6f;
    public float fireCooldown = 1f;
    public float projectileSpeed = 8f;
    public float projectileLifetime = 4f;

    Transform target;
    float nextFireTime;

    void Awake()
    {
        EnsureTriggerCollider();
    }

    void OnValidate()
    {
        EnsureTriggerCollider();
    }

    void Update()
    {
        if (target == null || projectilePrefab == null)
        {
            return;
        }

        if (Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + fireCooldown;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.transform == target)
        {
            target = null;
        }
    }

    void Fire()
    {
        Vector3 origin = firePoint != null ? firePoint.position : transform.position;
        Vector2 direction = target != null ? (target.position - origin).normalized : Vector2.left;

        GameObject projectile = Instantiate(projectilePrefab, origin, Quaternion.identity);

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = projectile.AddComponent<Rigidbody2D>();
        }

        rb.gravityScale = 0f;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.linearVelocity = direction * projectileSpeed;

        TurretProjectile turretProjectile = projectile.GetComponent<TurretProjectile>();
        if (turretProjectile == null)
        {
            turretProjectile = projectile.AddComponent<TurretProjectile>();
        }

        turretProjectile.lifetime = projectileLifetime;
    }

    void EnsureTriggerCollider()
    {
        Collider2D col = GetComponent<Collider2D>();
        if (col == null)
        {
            col = gameObject.AddComponent<CircleCollider2D>();
        }

        col.isTrigger = true;

        CircleCollider2D circle = col as CircleCollider2D;
        if (circle != null)
        {
            circle.radius = detectionRadius;
        }
    }
}
