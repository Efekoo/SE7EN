using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    public Boss1Health bossHealth;
    public Image healthImage;
    public Sprite[] heartStates;

    void Awake()
    {
        if (healthImage == null)
        {
            healthImage = GetComponent<Image>();
        }
    }

    void Update()
    {
        if (bossHealth == null)
        {
            bossHealth = FindAnyObjectByType<Boss1Health>();
        }

        if (bossHealth == null || healthImage == null || heartStates == null || heartStates.Length == 0)
        {
            return;
        }

        int clamped = Mathf.Clamp(bossHealth.currentHealth, 0, bossHealth.maxHealth);
        int step = Mathf.CeilToInt(clamped / 2f);
        int index = Mathf.Clamp(step, 0, heartStates.Length - 1);
        healthImage.sprite = heartStates[index];
    }
}
