using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{

    public int maxHealth = 5;
    public int startHealth = 3;
    public int health = 3;
    public AudioClip damageClip;
    public float damageVolume = 0.8f;

    bool isDead;


    public Text healthText;

    void Awake()
    {
        health = Mathf.Clamp(startHealth, 0, maxHealth);
    }


    public void TakeDamage(int damageAmount)
    {

        health = Mathf.Max(0, health - damageAmount);

        UpdateText();
        CheckDeath();

        if (damageAmount > 0 && damageClip != null)
        {
            AudioSource.PlayClipAtPoint(damageClip, transform.position, damageVolume);
        }

    }


    public void Heal(int healAmount)
    {

        health = Mathf.Min(maxHealth, health + healAmount);

        UpdateText();

        CheckDeath();

    }


    void Update()
    {

        UpdateText();
        CheckDeath();

    }


    void UpdateText()
    {

        if (healthText != null)
        {
            healthText.text = "Kalp: " + health;
        }

    }

    void CheckDeath()
    {
        if (isDead || health > 0)
        {
            return;
        }

        isDead = true;

        GameOverController controller = FindAnyObjectByType<GameOverController>();
        if (controller != null)
        {
            controller.Show();
        }
    }

}
