using System;
using UnityEngine;

public class Magic : MonoBehaviour
{
    [SerializeField] private  Player player;
    [SerializeField] private float spellRange;
    [SerializeField] private LayerMask  obstacleLayer;
    [SerializeField] private float spellCooldown;
    [SerializeField] private float playerRadius = 1.5f;

    public bool CanCast => Time.time >= nextCastTime;
    private float nextCastTime;

    public void AnimationFinished()
    {
        player.AnimationFinsihed();
        CastSpell();
    }

    void CastSpell()
    {
        Teleport();
    }

    private void Teleport()
    {
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
}