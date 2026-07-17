using UnityEngine;

public class EnemySenses : MonoBehaviour
{
    [SerializeField] private EnemyConfig config;
    [SerializeField] private Enemy enemy;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform attackPoint;

    public bool IsAtCliff()
    {
        return !Physics2D.Raycast(groundCheck.position, Vector2.down, config.groundCheckDistance, config.groundLayer);
    }

    public bool IsHittingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right, config.wallCheckDistance, config.wallLayer);
    }

    public Transform GetTarget()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, config.chaseRange, config.targetLayer);
        if(!hit)
        {
            return null;
        }

        Player player = hit.GetComponent<Player>();
        if(player.currentState == player.deathState)
        {
            return null;
        }

        return hit.transform;
    }

    public bool IsInMeleeRange(Transform target)
    {
        if(!target)
        {
            return false;
        }

        float distance = Vector2.Distance(target.position, attackPoint.position);
        return distance <= config.meleeRange;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * config.groundCheckDistance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.right * enemy.FacingDirection * config.wallCheckDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position,config.chaseRange);

        Gizmos.color = Color.aquamarine;
        Gizmos.DrawWireSphere(attackPoint.position, config.meleeRange);
    }
}