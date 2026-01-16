using UnityEngine;

public class BossRevealOnDetect : MonoBehaviour
{
    public BossVisibilityController visibilityController;

    void Awake()
    {
        if (visibilityController == null)
        {
            visibilityController = GetComponentInParent<BossVisibilityController>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        if (visibilityController != null)
        {
            visibilityController.Reveal();
        }
    }
}
