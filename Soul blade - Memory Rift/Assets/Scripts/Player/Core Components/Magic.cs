using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private  Player player;
    [SerializeField] private SpellUIManager spellUIManager;

    [Header("Spell State")]
    [SerializeField] private List<SpellSO> availableSpells = new List<SpellSO>();
    [SerializeField] private int currentIndex = 0;
    public SpellSO CurrentSpell => availableSpells.Count > 0 ? availableSpells[currentIndex] : null;

    [Header("Spark Settings")]
    [SerializeField] private GameObject sparkFX;
    [SerializeField] private int damage;
    [SerializeField] private float damageRadius;
    [SerializeField] private LayerMask enemyLayer;

    public bool CanCast => Time.time >= nextCastTime;
    private float nextCastTime;

    private void Start()
    {
        spellUIManager.ShowSpells(availableSpells);
        HighlightCurrentSpell();
    }

    public void LearnSpell(SpellSO spellSO)
    {
        if(!availableSpells.Contains(spellSO))
        {
            availableSpells.Add(spellSO);
        }

        currentIndex = Mathf.Clamp(currentIndex, 0, availableSpells.Count - 1);

        spellUIManager.ShowSpells(availableSpells);

        if(availableSpells.Count > 0)
        {
            HighlightCurrentSpell();
        }
    }

    public void NextSpell()
    {
        if(availableSpells.Count == 0)
        {
            return;
        }

        currentIndex = (currentIndex + 1) % availableSpells.Count;
        HighlightCurrentSpell();
    }

    public void PreviousSpell()
    {
        if (availableSpells.Count == 0)
        {
            return;
        }

        currentIndex = (currentIndex - 1 + availableSpells.Count) % availableSpells.Count;
        HighlightCurrentSpell();
    }

    private void HighlightCurrentSpell()
    {
        if(CurrentSpell != null)
        {
            spellUIManager.HighlightSpell(CurrentSpell);
        }
    }

    public void AnimationFinished()
    {
        player.AnimationFinsihed();
        CastSpell();
    }

    void CastSpell()
    {
        if (!CanCast || CurrentSpell == null)
        {
            return;
        }

        CurrentSpell.Cast(player);

        nextCastTime = Time.time + CurrentSpell.coolDown;

        spellUIManager.TriggerCooldown(CurrentSpell, CurrentSpell.coolDown);
    }
}