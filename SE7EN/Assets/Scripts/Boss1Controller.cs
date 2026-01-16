using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Boss1Controller : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float attackCooldown = 1f;
    public float burstInterval = 0.2f;
    public int burstCount = 2;
    public int attackDamage = 1;
    public float hitStunTime = 0.3f;

    public Collider2D detectionCollider;
    public Collider2D attackCollider;

    public Boss1AnimationController animationController;
    public BossVisibilityController visibilityController;

    Rigidbody2D rb;
    Transform target;
    float nextAttackTime;
    int burstHits;
    bool isHit;
    float hitTimer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (animationController == null)
        {
            animationController = GetComponent<Boss1AnimationController>();
        }

        if (visibilityController == null)
        {
            visibilityController = GetComponent<BossVisibilityController>();
        }

        EnsureTriggerCollider(detectionCollider);
        EnsureTriggerCollider(attackCollider);
    }

    void Update()
    {
        if (isHit)
        {
            hitTimer -= Time.deltaTime;
            if (hitTimer <= 0f)
            {
                isHit = false;
                if (animationController != null)
                {
                    animationController.SetHit(false);
                }
            }
        }

        bool hasTarget = target != null;
        if (!hasTarget)
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            return;
        }

        Collider2D targetCollider = target.GetComponent<Collider2D>();
        bool inAttackRange = attackCollider != null && targetCollider != null && attackCollider.IsTouching(targetCollider);
        if (animationController != null)
        {
            animationController.SetAttacking(inAttackRange);
            if (inAttackRange && !isHit)
            {
                animationController.PlayAttack();
            }
        }
        if (inAttackRange)
        {
            TryAttack();
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        }
        else
        {
            MoveTowardsTarget();
        }
    }

    void MoveTowardsTarget()
    {
        if (target == null)
        {
            return;
        }

        float direction = Mathf.Sign(target.position.x - transform.position.x);
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);

        if (animationController != null)
        {
            animationController.SetFacing(direction);
            if (!isHit)
            {
                animationController.PlayWalk();
            }
        }
    }

    void TryAttack()
    {
        if (Time.time < nextAttackTime)
        {
            return;
        }

        burstHits++;
        if (burstHits >= burstCount)
        {
            burstHits = 0;
            nextAttackTime = Time.time + attackCooldown;
        }
        else
        {
            nextAttackTime = Time.time + burstInterval;
        }

        if (animationController != null)
        {
            animationController.SetAttacking(true);
            animationController.PlayAttack();
        }

        HealthManager health = FindAnyObjectByType<HealthManager>();
        if (health != null)
        {
            health.TakeDamage(attackDamage);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform;
            if (visibilityController != null)
            {
                visibilityController.Reveal();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.transform == target)
        {
            target = null;
            if (animationController != null)
            {
                animationController.SetAttacking(false);
            }
        }
    }

    public void TakeHit()
    {
        isHit = true;
        hitTimer = hitStunTime;
        if (animationController != null)
        {
            animationController.SetHit(true);
            animationController.PlayHit();
        }
    }

    void EnsureTriggerCollider(Collider2D col)
    {
        if (col == null)
        {
            return;
        }

        col.isTrigger = true;
    }
}
