using UnityEngine;
using UnityEngine.Rendering;

public class PlayerStatsController : MonoBehaviour
{
    [Header("References to player progression Manager")]
    public ProgressionManager ProgressionManager;
    public Health health;
    public Combat combat;
    public Magic magic;

    private Attributes BaseAttributes => ProgressionManager.baseAttributes;
        
    private void Start()
    {
        ApplyAllStats();
    }

    private void OnEnable() => ProgressionManager.OnStatsChanged += ApplyAllStats;
    private void OnDisable() => ProgressionManager.OnStatsChanged -= ApplyAllStats;

    private void ApplyAllStats()
    {
        ApplyHealthStats();
        ApplyCombatStats();
        ApplyMagicStats();
    }

    void ApplyHealthStats()
    {
        health.ChangeMaxHealth(Stats.MaxHealth(BaseAttributes));
    }

    void ApplyCombatStats()
    {
        combat.SetStats(Stats.AttackDamage(BaseAttributes),Stats.CritChance(BaseAttributes));
    }

    void ApplyMagicStats()
    {
        magic.SetStats(Stats.SpellPower(BaseAttributes));
    }
}