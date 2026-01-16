using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Boss1AnimationController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Sprite[] walkFrames;
    public Sprite[] attackFrames;
    public Sprite[] hitFrames;
    public Sprite[] deathFrames;

    public float walkFps = 8f;
    public float attackFps = 10f;
    public float hitFps = 10f;
    public float deathFps = 8f;

    enum AnimState
    {
        Idle,
        Walk,
        Attack,
        Hit,
        Death,
    }

    AnimState state;
    int frameIndex;
    float frameTimer;

    bool facingRight;
    bool isDead;
    bool isHit;
    bool isAttacking;

    void Awake()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    public void SetFacing(float direction)
    {
        if (direction == 0f)
        {
            return;
        }

        facingRight = direction > 0f;
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = facingRight;
        }
    }

    public void SetDead(bool dead)
    {
        isDead = dead;
    }

    public void SetHit(bool hit)
    {
        isHit = hit;
    }

    public void SetAttacking(bool attacking)
    {
        isAttacking = attacking;
    }

    public void PlayWalk()
    {
        if (isDead)
        {
            return;
        }

        if (isAttacking)
        {
            PlayAttack();
            return;
        }

        if (isHit)
        {
            PlayHit();
            return;
        }

        Animate(AnimState.Walk, walkFrames, walkFps);
    }

    public void PlayAttack()
    {
        if (isDead)
        {
            return;
        }

        Animate(AnimState.Attack, attackFrames, attackFps);
    }

    public void PlayHit()
    {
        if (isDead)
        {
            return;
        }

        Animate(AnimState.Hit, hitFrames, hitFps);
    }

    public void PlayDeath()
    {
        Animate(AnimState.Death, deathFrames, deathFps, true);
    }

    void Animate(AnimState newState, Sprite[] frames, float fps, bool clampLast = false)
    {
        if (frames == null || frames.Length == 0 || spriteRenderer == null)
        {
            return;
        }

        if (state != newState)
        {
            state = newState;
            frameIndex = 0;
            frameTimer = 0f;
        }

        float frameDuration = 1f / Mathf.Max(1f, fps);
        frameTimer += Time.deltaTime;

        while (frameTimer >= frameDuration)
        {
            frameTimer -= frameDuration;
            frameIndex++;

            if (frameIndex >= frames.Length)
            {
                frameIndex = clampLast ? frames.Length - 1 : 0;
            }
        }

        spriteRenderer.sprite = frames[frameIndex];
    }
}
