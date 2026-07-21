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

    private Dictionary<SpellSO, float> spellCooldowns = new Dictionary<SpellSO, float>();

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

        if(!spellCooldowns.ContainsKey(spellSO))
        {
            spellCooldowns[spellSO] = 0;
        }

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

    public bool CanCast(SpellSO spellSO)
    {
        return Time.time >= spellCooldowns[spellSO];
    }

    void CastSpell()
    {
        if (!CanCast(CurrentSpell) || CurrentSpell == null)
        {
            return;
        }

        CurrentSpell.Cast(player);

        spellCooldowns[CurrentSpell] = Time.time + CurrentSpell.coolDown;
        spellUIManager.TriggerCooldown(CurrentSpell, CurrentSpell.coolDown);
    }
}