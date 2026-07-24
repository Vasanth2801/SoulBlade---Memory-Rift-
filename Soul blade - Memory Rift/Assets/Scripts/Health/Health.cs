using System;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action<Vector2> onDamaged;
    public event Action<Vector2> onDeath;

    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;

    [Header("Pop up")]
    public GameObject healthPopup;

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

        if (healthPopup != null)
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
}