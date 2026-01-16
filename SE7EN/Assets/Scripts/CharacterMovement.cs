using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;


[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{

    public float moveSpeed = 6f;


    public float acceleration = 20f;
    public float deceleration = 20f;


    public float jumpForce = 12f;


    public float gravityScale = 2f;


    public float jumpCutMultiplier = 0.5f;


    public float coyoteTime = 0.2f;
    float coyoteTimeCounter;


    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;
    public float groundCheckSkin = 0.05f;

    public float stepHeight = 0.2f;
    public float stepCheckDistance = 0.1f;
    public float stepRayYOffset = 0.02f;

    Rigidbody2D rb;
    Collider2D col;
    PlayerControls controls;


    Vector2 moveInput;
    bool isGrounded;
    bool canDoubleJump;

    public Vector2 MoveInput => moveInput;
    public bool IsGrounded => isGrounded;


    Collider2D currentPlatform;
    bool isFalling = false;


    void Awake()
    {

        controls = new PlayerControls();


        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;


        controls.Player.Jump.performed += ctx => HandleJump();

    }


    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }


    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        rb.gravityScale = gravityScale;

    }


    void Update()
    {

        rb.gravityScale = gravityScale;


        CheckGround();


        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter = coyoteTimeCounter - Time.deltaTime;
        }


        Move();

        HandleStepUp();


        if (moveInput.y < -0.5f && isGrounded && !isFalling)
        {

            StartCoroutine(DisableCollision());

        }


        if (!controls.Player.Jump.IsPressed())
        {

            if (rb.linearVelocity.y > 0)
            {

                Vector2 kesikHiz = rb.linearVelocity;
                kesikHiz.y = kesikHiz.y * jumpCutMultiplier;
                rb.linearVelocity = kesikHiz;

            }

        }

    }


    void Move()
    {

        float targetSpeed = moveInput.x * moveSpeed;

        float speedDif = targetSpeed - rb.linearVelocity.x;

        float accelRate = 0;


        if (Mathf.Abs(targetSpeed) > 0.01f)
        {
            accelRate = acceleration;
        }
        else
        {
            accelRate = deceleration;
        }


        float movement = speedDif * accelRate * Time.deltaTime;


        Vector2 yeniHiz = rb.linearVelocity;
        yeniHiz.x = yeniHiz.x + movement;
        rb.linearVelocity = yeniHiz;

    }

    void HandleStepUp()
    {
        if (col == null || !isGrounded)
        {
            return;
        }

        if (Mathf.Abs(moveInput.x) < 0.1f)
        {
            return;
        }

        float direction = Mathf.Sign(moveInput.x);
        Vector2 dir = new Vector2(direction, 0f);

        Bounds bounds = col.bounds;
        Vector2 lowerOrigin = new Vector2(bounds.center.x, bounds.min.y + stepRayYOffset);
        Vector2 upperOrigin = new Vector2(bounds.center.x, bounds.min.y + stepHeight);

        RaycastHit2D lowerHit = Physics2D.Raycast(lowerOrigin, dir, stepCheckDistance, groundLayer);
        RaycastHit2D upperHit = Physics2D.Raycast(upperOrigin, dir, stepCheckDistance, groundLayer);

        if (lowerHit.collider != null && upperHit.collider == null)
        {
            rb.position = new Vector2(rb.position.x, rb.position.y + stepHeight);
        }
    }


    void HandleJump()
    {

        if (coyoteTimeCounter > 0f)
        {

            Jump();
            coyoteTimeCounter = 0f;

        }
        else
        {

            if (canDoubleJump)
            {
                Jump();
                canDoubleJump = false;
            }

        }

    }


    void Jump()
    {

        Vector2 ziplamaVektoru = rb.linearVelocity;

        ziplamaVektoru.y = jumpForce;

        rb.linearVelocity = ziplamaVektoru;

    }


    void CheckGround()
    {

        Vector2 checkPos = GetGroundCheckPosition();

        currentPlatform = Physics2D.OverlapCircle(checkPos, groundRadius, groundLayer);

        isGrounded = currentPlatform != null;


        if (isGrounded)
        {
            canDoubleJump = true;
        }

    }



    IEnumerator DisableCollision()
    {

        isFalling = true;



        Collider2D geciciPlatform = currentPlatform;


        yield return new WaitForSeconds(0.15f);


        if (geciciPlatform != null)
        {
            Physics2D.IgnoreCollision(col, geciciPlatform, true);
        }


        yield return new WaitForSeconds(0.4f);


        if (geciciPlatform != null)
        {
            Physics2D.IgnoreCollision(col, geciciPlatform, false);
        }


        isFalling = false;

    }


    Vector2 GetGroundCheckPosition()
    {

        if (groundCheck != null)
        {
            return groundCheck.position;
        }


        if (col != null)
        {
            float altOffset = col.bounds.extents.y + groundCheckSkin;
            return (Vector2)transform.position + Vector2.down * altOffset;
        }


        return (Vector2)transform.position + Vector2.down * (groundRadius + groundCheckSkin);

    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 pos = GetGroundCheckPosition();
        Gizmos.DrawWireSphere(pos, groundRadius);
    }

}
