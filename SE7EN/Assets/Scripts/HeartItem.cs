using UnityEngine;

public class HeartItem : MonoBehaviour
{

    public int healAmount = 1;


    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {

            FindAnyObjectByType<HealthManager>().Heal(healAmount);

            Destroy(gameObject);

        }

    }

}