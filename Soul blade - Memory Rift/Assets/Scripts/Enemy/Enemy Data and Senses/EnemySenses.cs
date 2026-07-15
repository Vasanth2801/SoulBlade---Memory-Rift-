using System;
using UnityEngine;

public class EnemySenses : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private EnemyConfig config;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform attackPoint;

    public bool IsAtCliff()
    {
        return !Physics2D.Raycast(groundCheck.position, Vector2.down, config.grouchCheckDistance, config.groundLayer);   
    }

    public bool IsHittingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right, config.wallCheckDistance, config.wallLayer);
    }

    public Transform GetChaseTarget()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, config.chaseRange, config.targetLayer);

        if(!hit)
        {
            return null;
        }

        return hit.transform;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * config.grouchCheckDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.right * enemy.FacingDirection * config.wallCheckDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, config.chaseRange);
    }
}
