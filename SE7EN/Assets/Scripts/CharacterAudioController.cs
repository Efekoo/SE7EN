using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterAudioController : MonoBehaviour
{
    public AudioSource walkSource;
    public AudioSource jumpSource;
    public AudioClip walkClip;
    public AudioClip jumpClip;

    public float walkThreshold = 0.1f;
    public float jumpVelocityThreshold = 0.1f;
    public float walkPitch = 1.2f;

    Rigidbody2D rb;
    CharacterMovement movement;
    bool wasGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<CharacterMovement>();

        if (walkSource == null || jumpSource == null)
        {
            AudioSource[] sources = GetComponents<AudioSource>();
            if (walkSource == null && sources.Length > 0)
            {
                walkSource = sources[0];
            }
            if (jumpSource == null && sources.Length > 1)
            {
                jumpSource = sources[1];
            }
        }

            if (walkSource != null)
            {
                walkSource.loop = true;
                walkSource.playOnAwake = false;
                walkSource.pitch = walkPitch;
            }

        if (jumpSource != null)
        {
            jumpSource.loop = false;
            jumpSource.playOnAwake = false;
        }
    }

    void Update()
    {
        if (rb == null)
        {
            return;
        }

        bool grounded = movement != null ? movement.IsGrounded : Mathf.Abs(rb.linearVelocity.y) < jumpVelocityThreshold;
        float speed = Mathf.Abs(rb.linearVelocity.x);

        if (!grounded && wasGrounded)
        {
            PlayJump();
        }

        if (grounded && speed > walkThreshold)
        {
            PlayWalk();
        }
        else
        {
            StopWalk();
        }

        wasGrounded = grounded;
    }

    void PlayWalk()
    {
        if (walkSource == null)
        {
            return;
        }

        if (walkClip != null && walkSource.clip != walkClip)
        {
            walkSource.clip = walkClip;
        }

        if (!walkSource.isPlaying)
        {
            walkSource.Play();
        }
    }

    void StopWalk()
    {
        if (walkSource != null && walkSource.isPlaying)
        {
            walkSource.Stop();
        }
    }

    void PlayJump()
    {
        if (jumpClip == null)
        {
            return;
        }

        if (jumpSource != null)
        {
            jumpSource.PlayOneShot(jumpClip);
        }
        else if (walkSource != null)
        {
            walkSource.PlayOneShot(jumpClip);
        }
    }
}
