using System.Runtime.CompilerServices;
using UnityEngine;

public class Combat : MonoBehaviour
{

    [Header("Attack Settings")]
    [SerializeField] private int damage;
    [SerializeField] private float attackradius = 0.5f;
    [SerializeField] private float attackCoolDown = 1f;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemylayer;

    [SerializeField] private Player player;

    public bool CanAttack => Time.time >= nextAttackTime;
    private float nextAttackTime;

    public void AttackAnimationFinsihed()
    {
        player.AttackAnimationFinsihed();
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
            enemy.GetComponent<Health>().ChangeHealth(-damage);
        }
    }
}
