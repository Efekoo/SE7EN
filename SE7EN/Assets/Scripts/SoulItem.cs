using UnityEngine;

public class SoulItem : MonoBehaviour
{

    public int pointValue = 1;


    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {

            FindAnyObjectByType<ScoreManager>().AddScore(pointValue);

            Destroy(gameObject);

        }

    }

}