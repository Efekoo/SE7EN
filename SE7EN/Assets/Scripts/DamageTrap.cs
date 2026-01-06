using UnityEngine;

public class DamageTrap : MonoBehaviour
{

    public int damageAmount = 1;


    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.CompareTag("Player"))
        {

            FindAnyObjectByType<HealthManager>().TakeDamage(damageAmount);

        }

    }

}