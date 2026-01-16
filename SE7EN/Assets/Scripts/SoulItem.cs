using UnityEngine;

public class SoulItem : MonoBehaviour
{

    public int pointValue = 1;
    public AudioClip pickupClip;
    public float pickupVolume = 0.8f;


    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {

            FindAnyObjectByType<ScoreManager>().AddScore(pointValue);

            if (pickupClip != null)
            {
                AudioSource.PlayClipAtPoint(pickupClip, transform.position, pickupVolume);
            }

            Destroy(gameObject);

        }

    }

}
