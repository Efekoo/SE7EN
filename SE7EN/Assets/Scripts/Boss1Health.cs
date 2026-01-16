using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss1Health : MonoBehaviour
{
    public int maxHealth = 8;
    public int currentHealth = 8;
    public AudioClip damageClip;
    public float damageVolume = 0.8f;
    public bool loadNextSceneOnDeath;
    public string nextSceneName = "";
    public bool isFinalBoss = false;

    public Boss1AnimationController animationController;
    public Boss1Controller controller;
    public Collider2D[] colliders;

    void Awake()
    {
        if (animationController == null)
        {
            animationController = GetComponent<Boss1AnimationController>();
        }

        if (controller == null)
        {
            controller = GetComponent<Boss1Controller>();
        }

        if (colliders == null || colliders.Length == 0)
        {
            colliders = GetComponentsInChildren<Collider2D>();
        }

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public void TakeDamage(int amount)
    {
        if (currentHealth <= 0)
        {
            return;
        }

        currentHealth = Mathf.Max(0, currentHealth - amount);
        if (amount > 0 && damageClip != null)
        {
            AudioSource.PlayClipAtPoint(damageClip, transform.position, damageVolume);
        }
        if (currentHealth == 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (animationController != null)
        {
            animationController.SetDead(true);
            animationController.PlayDeath();
        }

        if (controller != null)
        {
            controller.enabled = false;
        }

        if (colliders != null)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].enabled = false;
            }
        }

        if (isFinalBoss)
        {
            YouWinScreenController youWinController = FindObjectOfType<YouWinScreenController>();
            if (youWinController != null)
            {
                youWinController.Show();
            }
        }
        else if (loadNextSceneOnDeath && !string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
        }
    }
}
