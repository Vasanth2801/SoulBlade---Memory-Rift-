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

    [Header("Sound")]
    public AudioData attackSound;
    public AudioData hitSound;

    private AudioManager audioManager;

    public bool CanAttack => Time.time >= nextAttackTime;
    private float nextAttackTime;

    public void Start()
    {
        audioManager = ServiceLocator.Get<AudioManager>();
    }

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

        audioManager.PlaySFX(attackSound);

        nextAttackTime = Time.time + attackCoolDown;

        Collider2D enemy = Physics2D.OverlapCircle(attackPoint.position, attackradius, enemylayer);

        if (enemy != null)
        {
            audioManager.PlaySFX(hitSound);
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