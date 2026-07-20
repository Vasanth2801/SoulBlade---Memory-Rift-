using System;
using UnityEngine;

public class Magic : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private  Player player;
    [SerializeField] private SpellSO currentSpell;

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
        if (!CanCast || currentSpell == null)
        {
            return;
        }

        currentSpell.Cast(player);

        nextCastTime = Time.time + currentSpell.coolDown;
    }
}