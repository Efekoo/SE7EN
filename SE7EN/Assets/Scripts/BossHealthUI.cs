using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    public Boss1Health bossHealth;
    public BossVisibilityController visibilityController;
    public Image healthImage;
    public Sprite[] heartStates;
    public bool hideUntilBossRevealed = true;

    void Awake()
    {
        if (healthImage == null)
        {
            healthImage = GetComponent<Image>();
        }

        if (hideUntilBossRevealed && healthImage != null)
        {
            healthImage.enabled = false;
        }
    }

    void Update()
    {
        if (bossHealth == null)
        {
            bossHealth = FindAnyObjectByType<Boss1Health>();
        }

        if (bossHealth != null && visibilityController == null)
        {
            visibilityController = bossHealth.GetComponent<BossVisibilityController>();
        }

        if (bossHealth == null || healthImage == null || heartStates == null || heartStates.Length == 0)
        {
            return;
        }

        if (hideUntilBossRevealed && visibilityController != null && !visibilityController.IsRevealed)
        {
            healthImage.enabled = false;
            return;
        }

        healthImage.enabled = true;

        int clamped = Mathf.Clamp(bossHealth.currentHealth, 0, bossHealth.maxHealth);
        int step = Mathf.CeilToInt(clamped / 2f);
        int index = Mathf.Clamp(step, 0, heartStates.Length - 1);
        healthImage.sprite = heartStates[index];
    }
}
