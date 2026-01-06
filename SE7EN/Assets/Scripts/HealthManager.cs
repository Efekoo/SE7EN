using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{

    public int health = 3;


    public Text healthText;


    public void TakeDamage(int damageAmount)
    {

        health = health - damageAmount;

        UpdateText();

    }


    public void Heal(int healAmount)
    {

        health = health + healAmount;

        UpdateText();

    }


    void Update()
    {

        UpdateText();

    }


    void UpdateText()
    {

        if (healthText != null)
        {
            healthText.text = "Kalp: " + health;
        }

    }

}