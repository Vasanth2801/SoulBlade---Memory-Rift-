using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action<int, int> OnHealthChanged;
    public event Action<Vector2> onDamaged;
    public event Action<Vector2> onDeath;

    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;

    [Header("Pop up")]
    public GameObject healthPopup;

    public void ChangeHealth(int amount, Vector2 sourcePosition, bool showPopup = true)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        if (healthPopup != null && showPopup)
        {
            var popup = Instantiate(healthPopup, transform.position, Quaternion.identity);
            popup.GetComponent<HealthPopup>().Setup(amount);
        }

        if (currentHealth <= 0)
        {
            onDeath?.Invoke(sourcePosition);
        }
        else if(amount <= 0)
        {
            onDamaged?.Invoke(sourcePosition);
        }
    }

    public void ChangeMaxHealth(int newMaxHealth)
    {
        int difference = newMaxHealth - maxHealth;

        maxHealth += difference;

        ChangeHealth(difference, Vector2.zero, false);
    }
}