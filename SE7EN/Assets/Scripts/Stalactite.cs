using UnityEngine;

using UnityEngine;
    
    public class Stalactite : MonoBehaviour
     {
         public float speed = 5f;
         public int damage = 1;
         private Rigidbody2D rb;
    
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            // Move downwards at a constant speed
            rb.linearVelocity = new Vector2(0, -speed);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
             // Did it collide with the player?
            if (other.CompareTag("Player"))
                 {
                    // Find the HealthManager script that manages the player's health
                HealthManager healthManager = other.GetComponent<HealthManager>();
                     if (healthManager != null)
                         {
                             // Deal damage
                    healthManager.TakeDamage(damage);
                         }
                     // Destroy the stalactite
               Destroy(gameObject);
                 }
         }

        void Update()
        {
             // Destroy itself if it falls too far below the screen (for performance)
            if (transform.position.y < -10f)
                {
                    Destroy(gameObject);
                 }
         }
 }