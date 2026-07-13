using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Health health;

    private void OnEnable()
    {
        health.onDamaged += HandleDamage;
    }

    private void OnDisable()
    {
        health.onDamaged -= HandleDamage;
    }

    void HandleDamage()
    {
        anim.SetTrigger("Hit");
    }
}