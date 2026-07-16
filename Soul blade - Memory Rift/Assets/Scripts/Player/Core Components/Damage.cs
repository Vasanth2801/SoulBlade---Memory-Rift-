using Unity.VisualScripting;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Player player;

    [Header("Knockback Settings")]
    public float knockbackForce = 20f;
    public float knockbackDuration = 0.3f;  

    private void OnEnable()
    {
        health.onDamaged += HandleDamage;
        health.onDeath += HandleDeath;
    }

    private void OnDisable()
    {
        health.onDamaged -= HandleDamage;
        health.onDeath += HandleDeath;
    }

    void HandleDamage(Vector2 sourcePosition)
    {
        int knockbackDirection = 0;
        knockbackDirection = transform.position.x > sourcePosition.x ? 1 : -1;

        player.damagedState.SetParameters(knockbackDirection);
        player.ChangeState(player.damagedState);
    }

    void HandleDeath()
    {
        
    }
}
