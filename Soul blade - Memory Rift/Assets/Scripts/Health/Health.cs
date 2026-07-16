using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action<Vector2> onDamaged;
    public event Action onDeath;

    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void ChangeHealth(int amount, Vector2 sourcePosition)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0)
        {
            onDeath?.Invoke();
        }
        else if(amount <= 0)
        {
            onDamaged?.Invoke(sourcePosition);
        }
    }
}