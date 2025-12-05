using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float acceleration = 20f;
    public float deceleration = 20f;
    public float jumpForce = 12f;
    public float jumpCutMultiplier = 0.5f;

    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;
    public float groundCheckSkin = 0.05f;

    Rigidbody2D rb;
    Collider2D col;
    PlayerControls controls;

    Vector2 moveInput;
    bool isGrounded;
    bool canDoubleJump;

    void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        controls.Player.Jump.performed += ctx => HandleJump();
    }

    void OnEnable() => controls.Enable();
    void OnDisable() => controls.Disable();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        CheckGround();
        Move();

        if (!controls.Player.Jump.IsPressed() && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * jumpCutMultiplier);
        }
    }

    void Move()
    {
        float targetSpeed = moveInput.x * moveSpeed;

        float speedDif = targetSpeed - rb.linearVelocity.x;
        float accelRate = Mathf.Abs(targetSpeed) > 0.01f ? acceleration : deceleration;

        float movement = speedDif * accelRate * Time.deltaTime;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x + movement, rb.linearVelocity.y);
    }

    void HandleJump()
    {
        if (isGrounded)
        {
            Jump();
        }
        else if (canDoubleJump)
        {
            Jump();
            canDoubleJump = false;
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    void CheckGround()
    {
        var checkPos = GetGroundCheckPosition();
        isGrounded = Physics2D.OverlapCircle(checkPos, groundRadius, groundLayer);

        if (isGrounded)
            canDoubleJump = true;
    }

    Vector2 GetGroundCheckPosition()
    {
        if (groundCheck != null && groundCheck != transform)
            return groundCheck.position;

        if (col != null)
        {
            var downOffset = col.bounds.extents.y + groundCheckSkin;
            return (Vector2)transform.position + Vector2.down * downOffset;
        }

        return (Vector2)transform.position + Vector2.down * (groundRadius + groundCheckSkin);
    }
}
