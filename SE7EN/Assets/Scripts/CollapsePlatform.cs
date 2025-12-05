using UnityEngine;

public class CollapsePlatform : MonoBehaviour
{
    public float delay = 0.3f;
    public float destroyDelay = 2f;

    bool triggered = false;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (triggered) return;

        if (collision.collider.CompareTag("Player"))
        {
            triggered = true;
            Invoke(nameof(Fall), delay);
        }
    }

    void Fall()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        Invoke(nameof(Delete), destroyDelay);
    }

    void Delete()
    {
        Destroy(gameObject);
    }
}
