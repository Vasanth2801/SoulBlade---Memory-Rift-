using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [Header("General Settings")]
    public float turnThershold = 0.5f;

    [Header("Patrol Speed")]
    public float patrolSpeed = 5f;
    public float groundCheckDistance = 0.7f;
    public LayerMask groundLayer;

    [Header("Wall Check Settings")]
    public float wallCheckDistance = 0.7f;
    public LayerMask wallLayer;

    [Header("Chase Settings")]
    public float chaseSpeed = 7f;
    public float chaseRange = 7f;
    public LayerMask targetLayer;

    [Header("Melee Attack Settings")]
    public float meleeRange = 1.15f;
    public int meleeDamage = 2;
    public float meleeCooldown = 1f;

    [Header("Ranged Attack Settings")]
    public float rangedRange = 5f;
    public int rangedDamage = 1;
    public float rangedCooldown = 1.15f;
    public GameObject projectilePrefab;
    public float projectileSpeed = 12f;
    public float projectileLifeTime = 3f;


    [Header("Damaged")]
    public float knockbackDuration = 0.3f;
    public float knockbackForce = 25f;
}