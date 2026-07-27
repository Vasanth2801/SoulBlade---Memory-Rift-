using System;
using UnityEngine;

public class Combat : MonoBehaviour
{
    [Header("Attack Settings")]
    public int damage;
    private float critChance;
    [SerializeField] private float critMultiplier;
    [SerializeField] private float attackradius = 0.5f;
    [SerializeField] private float attackCoolDown = 1f;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemylayer;

    [SerializeField] private Player player;
    [SerializeField] private Animator hitFX;

    public bool CanAttack => Time.time >= nextAttackTime;
    private float nextAttackTime;

    public void AttackAnimationFinsihed()
    {
        player.AnimationFinsihed();
    }

    public void SetStats(int damage, float critChance)
    {
        this.damage = damage;
        this.critChance = critChance;
    }

    public void Attack()
    {
        if(!CanAttack)
        {
            return;
        }

        nextAttackTime = Time.time + attackCoolDown;

        Collider2D enemy = Physics2D.OverlapCircle(attackPoint.position, attackradius, enemylayer);

        if (enemy != null)
        {
            hitFX.Play("Hit");
            int realDamage = damage;
            if (UnityEngine.Random.value < critChance)
            {
                realDamage = Mathf.RoundToInt(realDamage * critMultiplier);
            }
            enemy.GetComponent<Health>().ChangeHealth(-realDamage, transform.position);
        }
    }
}