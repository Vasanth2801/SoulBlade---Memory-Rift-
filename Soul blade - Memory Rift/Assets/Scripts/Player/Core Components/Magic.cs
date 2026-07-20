using System;
using UnityEngine;

public class Magic : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private  Player player;

    [Header("Teleport Settings")]
    [SerializeField] private float spellRange;
    [SerializeField] private LayerMask  obstacleLayer;
    [SerializeField] private float spellCooldown;
    [SerializeField] private float playerRadius = 1.5f;

    [Header("Spark Settings")]
    [SerializeField] private GameObject sparkFX;
    [SerializeField] private int damage;
    [SerializeField] private float damageRadius;
    [SerializeField] private LayerMask enemyLayer;

    public bool CanCast => Time.time >= nextCastTime;
    private float nextCastTime;

    public void AnimationFinished()
    {
        player.AnimationFinsihed();
        CastSpell();
    }

    void CastSpell()
    {
        //Teleport();
        Spark();
    }

    private void Teleport()
    {
        if(!CanCast)
        {
            return;
        }
        Vector2 direction = new Vector2(player.facingDirection, 0);
        Vector2 targetPosition = (Vector2)player.transform.position + direction * spellRange;

        Collider2D hit = Physics2D.OverlapCircle(targetPosition, playerRadius, obstacleLayer);

        if(hit != null)
        {
            float step = 0.1f;
            Vector2 adjustedPosition = targetPosition;

            while(hit != null && Vector2.Distance(adjustedPosition,player.transform.position) > 0)
            {
                adjustedPosition -= direction * step;
                hit = Physics2D.OverlapCircle(adjustedPosition,playerRadius, obstacleLayer);
            }
            targetPosition = adjustedPosition;
        }

        player.transform.position = targetPosition;
        nextCastTime = Time.time + spellCooldown;
    }

    private void Spark()
    {
        if (!CanCast)
        {
            return;
        }

        Collider2D[] enemies = Physics2D.OverlapCircleAll(player.transform.position,damageRadius, enemyLayer);

        foreach(Collider2D enemy in enemies)
        {
            Health health = enemy.GetComponent<Health>();

            if(health != null )
            {
                health.ChangeHealth(-damage, enemy.transform.position);
            }

            if(sparkFX != null)
            {
                GameObject newFX = Instantiate(sparkFX, enemy.transform.position,Quaternion.identity);
                Destroy(newFX,2);
            }
        }

        nextCastTime = Time.time + spellCooldown;
    }
}