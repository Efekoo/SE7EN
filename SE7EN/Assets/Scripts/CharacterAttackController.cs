using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAttackController : MonoBehaviour
{
    public BoxCollider2D attackCollider;
    public Vector2 attackOffset = new Vector2(0.6f, 0f);
    public LayerMask hitLayers = ~0;
    public float attackCooldown = 0.25f;

    public CharacterAnimationController animationController;
    public SpriteRenderer spriteRenderer;

    ContactFilter2D hitFilter;
    readonly Collider2D[] hitBuffer = new Collider2D[8];
    float nextAttackTime;

    void Awake()
    {
        if (animationController == null)
        {
            animationController = GetComponent<CharacterAnimationController>();
        }

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (attackCollider == null)
        {
            attackCollider = GetComponentInChildren<BoxCollider2D>();
        }

        hitFilter = new ContactFilter2D();
        hitFilter.useLayerMask = true;
        hitFilter.useTriggers = true;
        hitFilter.SetLayerMask(hitLayers);
    }

    void Update()
    {
        if (Keyboard.current == null)
        {
            return;
        }

        hitFilter.SetLayerMask(hitLayers);
        UpdateAttackCollider();

        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            if (Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    void UpdateAttackCollider()
    {
        if (attackCollider == null || spriteRenderer == null)
        {
            return;
        }

        float direction = spriteRenderer.flipX ? -1f : 1f;
        Vector3 localPos = attackCollider.transform.localPosition;
        localPos.x = Mathf.Abs(attackOffset.x) * direction;
        localPos.y = attackOffset.y;
        attackCollider.transform.localPosition = localPos;
    }

    void Attack()
    {
        if (animationController != null)
        {
            animationController.TriggerAttack();
        }

        if (attackCollider == null)
        {
            return;
        }

        int hitCount = attackCollider.Overlap(hitFilter, hitBuffer);
        for (int i = 0; i < hitCount; i++)
        {
            Boss1Health boss = hitBuffer[i].GetComponentInParent<Boss1Health>();
            if (boss != null)
            {
                boss.TakeDamage(1);
                break;
            }
        }
    }
}
