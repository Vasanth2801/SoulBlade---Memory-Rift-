using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [Header("General Settings")]
    public float patrolSpeed;

    [Header("Patrol Settings")]
    public float grouchCheckDitance = 0.7f;
    public LayerMask groundLayer;
}