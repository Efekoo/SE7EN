using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TurretProjectile : MonoBehaviour
{
    public float lifetime = 4f;
    public bool destroyOnHit = true;
    public int damageAmount = 1;

    float lifeTimer;

    void Awake()
    {
        EnsureCollider();
        EnsureSprite();
    }

    void Update()
    {
        lifeTimer += Time.deltaTime;
        if (lifeTimer >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!destroyOnHit)
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            HealthManager healthManager = FindAnyObjectByType<HealthManager>();
            if (healthManager != null)
            {
                healthManager.TakeDamage(damageAmount);
            }

            Destroy(gameObject);
            return;
        }

        if (!other.isTrigger)
        {
            Destroy(gameObject);
        }
    }

    void EnsureCollider()
    {
        Collider2D col = GetComponent<Collider2D>();
        if (col == null)
        {
            col = gameObject.AddComponent<CircleCollider2D>();
        }

        col.isTrigger = true;
    }

    void EnsureSprite()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer == null)
        {
            renderer = gameObject.AddComponent<SpriteRenderer>();
        }

        if (renderer.sprite == null)
        {
            renderer.sprite = CreateCircleSprite();
        }

        renderer.color = Color.white;
    }

    Sprite CreateCircleSprite()
    {
        const int size = 16;
        Texture2D texture = new Texture2D(size, size, TextureFormat.ARGB32, false);
        texture.filterMode = FilterMode.Point;

        float r = (size - 1) / 2f;
        Vector2 center = new Vector2(r, r);

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float dist = Vector2.Distance(new Vector2(x, y), center);
                Color color = dist <= r ? Color.white : new Color(0, 0, 0, 0);
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();

        return Sprite.Create(texture, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), 16f);
    }
}
