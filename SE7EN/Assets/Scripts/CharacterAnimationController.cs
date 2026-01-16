using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterAnimationController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] idleFrames;
    public Sprite[] walkFrames;
    public Sprite[] runFrames;
    public Sprite[] crouchFrames;
    public Sprite[] jumpFrames;
    public Sprite[] attackFrames;

    public float idleFps = 6f;
    public float walkFps = 10f;
    public float runFps = 12f;
    public float crouchFps = 8f;
    public float jumpFps = 10f;
    public float attackFps = 12f;
    public float attackDuration = 0.2f;

    public float runThreshold = 6f;
    public float moveThreshold = 0.1f;
    public float jumpVelocityThreshold = 0.1f;

    enum AnimState
    {
        Idle,
        Walk,
        Run,
        Crouch,
        Jump,
        Attack,
    }

    AnimState state;
    int frameIndex;
    float frameTimer;

    CharacterMovement movement;
    Rigidbody2D rb;
    float attackTimer;

    void Awake()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        movement = GetComponent<CharacterMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (spriteRenderer == null || rb == null)
        {
            return;
        }

        if (attackTimer > 0f && attackFrames != null && attackFrames.Length > 0)
        {
            attackTimer -= Time.deltaTime;
            Animate(AnimState.Attack, attackFrames, attackFps);
            return;
        }

        Vector2 input = movement != null ? movement.MoveInput : Vector2.zero;
        bool grounded = movement != null ? movement.IsGrounded : Mathf.Abs(rb.linearVelocity.y) < jumpVelocityThreshold;

        float vx = rb.linearVelocity.x;
        float vy = rb.linearVelocity.y;

        if (Mathf.Abs(vx) > moveThreshold)
        {
            // West-facing frames are used; flip when moving right.
            spriteRenderer.flipX = vx > 0f;
        }

        if (jumpFrames != null && jumpFrames.Length > 0 && (!grounded || Mathf.Abs(vy) > jumpVelocityThreshold))
        {
            Animate(AnimState.Jump, jumpFrames, jumpFps);
            return;
        }

        if (grounded && input.y < -0.5f && crouchFrames != null && crouchFrames.Length > 0)
        {
            Animate(AnimState.Crouch, crouchFrames, crouchFps);
            return;
        }

        float speed = Mathf.Abs(vx);

        if (speed > runThreshold && runFrames != null && runFrames.Length > 0)
        {
            Animate(AnimState.Run, runFrames, runFps);
            return;
        }

        if (speed > moveThreshold && walkFrames != null && walkFrames.Length > 0)
        {
            Animate(AnimState.Walk, walkFrames, walkFps);
            return;
        }

        if (idleFrames != null && idleFrames.Length > 0)
        {
            Animate(AnimState.Idle, idleFrames, idleFps);
        }
    }

    public void TriggerAttack()
    {
        if (attackFrames == null || attackFrames.Length == 0)
        {
            return;
        }

        attackTimer = Mathf.Max(attackDuration, attackFrames.Length / Mathf.Max(1f, attackFps));
        Animate(AnimState.Attack, attackFrames, attackFps);
    }

    void SetState(AnimState newState)
    {
        if (state == newState)
        {
            return;
        }

        state = newState;
        frameIndex = 0;
        frameTimer = 0f;
    }

    void Animate(AnimState newState, Sprite[] frames, float fps)
    {
        SetState(newState);

        if (frames == null || frames.Length == 0)
        {
            return;
        }

        float frameDuration = 1f / Mathf.Max(1f, fps);
        frameTimer += Time.deltaTime;

        while (frameTimer >= frameDuration)
        {
            frameTimer -= frameDuration;
            frameIndex = (frameIndex + 1) % frames.Length;
        }

        spriteRenderer.sprite = frames[frameIndex];
    }
}
