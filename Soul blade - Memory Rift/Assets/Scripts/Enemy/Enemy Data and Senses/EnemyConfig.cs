using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Enemy/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [Header("General Settings")]
    public float turnThreshold = 0.5f;

    [Header("Patrol Settings")]
    public float patrolSpeed;
    public float grouchCheckDistance = 0.7f;
    public LayerMask groundLayer;

    [Header("Wall Settings")]
    public float wallCheckDistance = 0.7f;
    public LayerMask wallLayer;

    [Header("Chase Range")]
    public float chaseSpeed = 4f;
    public float chaseRange = 5f;
    public LayerMask targetLayer;
}